using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace E_Ticaret.Services
{
    public class AdminVeriAnalizService
    {
        private readonly DatabaseContext _context;
        private readonly HttpClient _httpClient;

        public AdminVeriAnalizService(DatabaseContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", "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"); // 🔑 Buraya kendi OpenAI API key’ini yaz.
        }

        // 1️⃣ Kullanıcının geçmiş ürünlerini çek
        public async Task<List<Urun>> GetKullanicininGecmisUrunleriAsync(int kullaniciId)
        {
            var urunler = await _context.Siparisler
                .Where(s => s.KullaniciId == kullaniciId)
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(sd => sd.Urun)
                        .ThenInclude(u => u.Kategori)
                .SelectMany(s => s.SiparisDetaylari.Select(sd => sd.Urun))
                .Distinct()
                .ToListAsync();

            return urunler;
        }

        // 2️⃣ ChatGPT’ye gönderilecek prompt hazırla ve öneri al
        public async Task<string> GetUrunOnerisiMetniAsync(int kullaniciId)
        {
            var urunler = await GetKullanicininGecmisUrunleriAsync(kullaniciId);

            if (urunler == null || !urunler.Any())
                return "Kullanıcının geçmiş siparişi bulunamadı.";

            var urunListesi = string.Join(", ", urunler.Select(u => $"{u.UrunAdi} ({u.Kategori?.KategoriAdi})"));

            var prompt = $"""
                Kullanıcı geçmişte şu ürünleri satın aldı: {urunListesi}.
                Bu kullanıcıya benzer kategorilerden 3 ürün öner. 
                Her öneri için neden uygun olduğunu açıklayan bir metin oluştur.
            """;

            var requestBody = new
            {
                model = "gpt-4", // veya gpt-3.5-turbo
                messages = new[]
                {
                    new { role = "user", content = prompt }
                }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            string cevap = result?.choices[0]?.message?.content;

            return cevap ?? "ChatGPT'den yanıt alınamadı.";
        }
        public async Task<string> GetSatisOzetiMetniAsync()
        {
            var baslangicTarihi = new DateTime(2025, 5, 1);
            var bitisTarihi = DateTime.Now;

            var satislar = await _context.Siparisler
                .Where(s => s.SiparisTarihi >= baslangicTarihi && s.SiparisTarihi <= bitisTarihi)
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(sd => sd.Urun)
                        .ThenInclude(u => u.Kategori)
                .ToListAsync();

            if (!satislar.Any())
                return "Bu tarih aralığında satış bulunamadı.";

            var toplamSatis = satislar.Count;
            var toplamTutar = satislar.Sum(s => s.ToplamTutar);

            var urunDetaylari = satislar.SelectMany(s => s.SiparisDetaylari).ToList();

            var enCokSatanUrun = urunDetaylari
                .GroupBy(sd => sd.Urun.UrunAdi)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Bilinmiyor";

            var enCokSatanKategori = urunDetaylari
                .GroupBy(sd => sd.Urun.Kategori?.KategoriAdi ?? "Bilinmiyor")
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Bilinmiyor";

            var prompt = $"""
    Aşağıdaki veriler ışığında bir satış özeti raporu hazırla:

    - Başlangıç tarihi: {baslangicTarihi:dd.MM.yyyy}
    - Bitiş tarihi: {bitisTarihi:dd.MM.yyyy}
    - Toplam satış: {toplamSatis} adet
    - Toplam ciro: {toplamTutar} ₺
    - En çok satan ürün: {enCokSatanUrun}
    - En çok satan kategori: {enCokSatanKategori}

    Yöneticilere uygun profesyonel ve kısa bir metin üret.
    """;

            // GPT API İsteği
            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            var body = new
            {
                model = "gpt-4",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            string cevap = result?.choices[0]?.message?.content;

            return cevap ?? "ChatGPT'den yanıt alınamadı.";
        }



        public async Task<object> GetGenelSatisOzetiAsync()
        {
            var siparisler = await _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(sd => sd.Urun)
                        .ThenInclude(u => u.Kategori)
                .ToListAsync();

            var toplamTutar = siparisler.Sum(s => s.ToplamTutar);
            var toplamSiparis = siparisler.Count;
            var ortalamaSiparisDegeri = toplamSiparis > 0 ? toplamTutar / toplamSiparis : 0;

            var urunGrubu = siparisler
                .SelectMany(s => s.SiparisDetaylari)
                .GroupBy(sd => sd.Urun?.UrunAdi)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Bilinmiyor";

            var kategoriGrubu = siparisler
                .SelectMany(s => s.SiparisDetaylari)
                .GroupBy(sd => sd.Urun?.Kategori?.KategoriAdi)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Bilinmiyor";

            return new
            {
                ToplamTutar = toplamTutar,
                ToplamSiparis = toplamSiparis,
                OrtalamaSiparisDegeri = ortalamaSiparisDegeri,
                EnCokSatanUrun = urunGrubu,
                EnCokSatanKategori = kategoriGrubu
            };
        }

        public async Task<string> GetGenelSatisOnerisiAsync()
        {
            var siparisler = await _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(sd => sd.Urun)
                        .ThenInclude(u => u.Kategori)
                .ToListAsync();

            if (!siparisler.Any())
                return "Henüz satış verisi bulunamadı.";

            var urunler = siparisler
                .SelectMany(s => s.SiparisDetaylari)
                .Select(sd => new { sd.Urun.UrunAdi, Kategori = sd.Urun.Kategori?.KategoriAdi })
                .ToList();

            var urunListesi = string.Join(", ", urunler.Select(u => $"{u.UrunAdi} ({u.Kategori})"));

            var prompt = $"""
        Aşağıdaki ürünler geçmiş satışlara göre en çok satılanlar arasında:
        {urunListesi}

        Bu veriye dayanarak yöneticilere önerilecek 3 yeni ürün kategorisi ve bu kategorilerin neden önerildiğini içeren kısa açıklamalar üret.
        """;

            var requestBody = new
            {
                model = "gpt-4",
                messages = new[]
                {
            new { role = "user", content = prompt }
        }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            string cevap = result?.choices[0]?.message?.content;

            return cevap ?? "ChatGPT'den yanıt alınamadı.";
        }




        public async Task<string> GetKampanyaOnerisiAsync()
        {
            var enCokSatan = await _context.SiparisDetaylar
                .GroupBy(sd => sd.Urun.Kategori.KategoriAdi)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            var enAzSatan = await _context.SiparisDetaylar
                .GroupBy(sd => sd.Urun.Kategori.KategoriAdi)
                .OrderBy(g => g.Count())
                .Select(g => g.Key)
                .FirstOrDefaultAsync();

            var toplamMusteri = await _context.Kullancilar.CountAsync();

            var prompt = $"""
Bir e-ticaret sitesinin satış verileri aşağıda verilmiştir:

- En çok satan kategori: {enCokSatan}
- En az satan kategori: {enAzSatan}
- Toplam müşteri sayısı: {toplamMusteri}

Bu verilere göre 2 adet dinamik kampanya fikri üret.
Kampanyalar:
- Uygulanabilir olsun (gerçek kampanya gibi düşün),
- Kısa ve dikkat çekici başlık içersin (örnek: “2 Al 1 Öde”, “%30 Sepette İndirim” gibi),
- Hedef: satışları artırmak ve düşük performanslı kategorileri canlandırmak.

Profesyonel bir pazarlama uzmanı gibi yaz, yaratıcı ol.
""";

            var content = new StringContent(JsonConvert.SerializeObject(new
            {
                model = "gpt-4",
                messages = new[] { new { role = "user", content = prompt } }
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseJson);

            return result?.choices[0]?.message?.content ?? "Yanıt alınamadı.";
        }



        public async Task<string> GetSatisTahminRaporuAsync()
        {
            var sonIkiHafta = DateTime.Today.AddDays(-14);
            var siparisler = await _context.Siparisler
                .Where(s => s.SiparisTarihi >= sonIkiHafta)
                .ToListAsync();

            var gunlukVeri = siparisler
                .GroupBy(s => s.SiparisTarihi.Date)
                .Select(g => new
                {
                    Tarih = g.Key.ToString("dd.MM.yyyy"),
                    ToplamTutar = g.Sum(s => s.ToplamTutar)
                })
                .OrderBy(x => x.Tarih)
                .ToList();

            var satisSatirlari = string.Join("\n", gunlukVeri.Select(x => $"- {x.Tarih}: {x.ToplamTutar} ₺"));

            var prompt = $"""
Elimde son 14 güne ait günlük satış verisi var:
{satisSatirlari}

Bu verilere göre:
1. Önümüzdeki 7 gün için satış tahmini yap.
2. Tahmini toplam ciroyu belirt.
3. Satışları artırmak için uygulanabilir bir öneri ver (örneğin: kampanya, popüler ürün vurgusu, stok uyarısı).
4. Raporu yöneticilere hitap edecek kısa ve profesyonel bir dilde oluştur.
""";


            var requestBody = new
            {
                model = "gpt-4",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var json = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(json);
            return result?.choices[0]?.message?.content ?? "Yanıt alınamadı.";
        }


        public async Task<string> GetGenelDurumAnaliziAsync()
        {
            var baslangic = DateTime.Today.AddDays(-7);
            var bitis = DateTime.Today;

            var siparisler = await _context.Siparisler
                .Where(s => s.SiparisTarihi >= baslangic && s.SiparisTarihi <= bitis)
                .Include(s => s.SiparisDetaylari)
                .ThenInclude(sd => sd.Urun)
                .ThenInclude(u => u.Kategori)
                .ToListAsync();

            var toplamMusteri = await _context.Kullancilar.CountAsync();
            var toplamCiro = siparisler.Sum(s => s.ToplamTutar);
            var ortalamaGunlukSatis = toplamCiro / 7;
            var ortalamaSiparis = siparisler.Any() ? siparisler.Average(s => s.ToplamTutar) : 0;

            var enCokKategori = siparisler
                .SelectMany(s => s.SiparisDetaylari)
                .Where(sd => sd.Urun?.Kategori != null)
                .GroupBy(sd => sd.Urun.Kategori.KategoriAdi)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key ?? "Yok";

            var enAzKategori = siparisler
                .SelectMany(s => s.SiparisDetaylari)
                .Where(sd => sd.Urun?.Kategori != null)
                .GroupBy(sd => sd.Urun.Kategori.KategoriAdi)
                .OrderBy(g => g.Count())
                .FirstOrDefault()?.Key ?? "Yok";

            var enCokMusteri = siparisler
                .GroupBy(s => s.Kullanici?.Adi + " " + s.Kullanici?.Soyadi)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();

            var enCokMusteriAdi = enCokMusteri?.Key ?? "Bilinmiyor";
            var enCokSiparis = enCokMusteri?.Count() ?? 0;

            var prompt = $"""
Elimde bir e-ticaret sitesinin son 7 güne ait performans verileri var:

- Toplam müşteri sayısı: {toplamMusteri}
- Toplam ciro: {toplamCiro.ToString("C2")}
- Ortalama günlük satış: {ortalamaGunlukSatis.ToString("C2")}
- Ortalama sipariş değeri: {ortalamaSiparis.ToString("C2")}
- En çok satan kategori: {enCokKategori}
- En az satan kategori: {enAzKategori}
- En çok sipariş veren müşteri: {enCokMusteriAdi} ({enCokSiparis} sipariş)

Bu verilere göre mevcut performansı analiz et.
Satış trendleri, müşteri ilgisi ve ürün dengesi hakkında kısa, profesyonel ve yönetime uygun bir özet sun.
Gerekiyorsa öneriler de ekle.
""";


            var requestBody = new
            {
                model = "gpt-4",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var content = new StringContent(JsonConvert.SerializeObject(requestBody), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            string cevap = result?.choices[0]?.message?.content;

            return cevap ?? "ChatGPT'den analiz alınamadı.";
        }



    }

}
