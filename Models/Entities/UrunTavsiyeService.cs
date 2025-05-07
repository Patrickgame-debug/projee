using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using E_Ticaret.Models.Entities;
using Microsoft.EntityFrameworkCore;
using E_Ticaret.DataContext;

namespace E_Ticaret.Services
{
    public class UrunTavsiyeService
    {
        private readonly DatabaseContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public UrunTavsiyeService(
            DatabaseContext context,
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<List<Urun>> OnerilenUrunleriGetirAsync(Kullanici kullanici)
        {
            // Kullanıcı kontrolü
            var kullaniciGuidStr = _httpContextAccessor.HttpContext?.User?.FindFirst("KullaniciGuid")?.Value;
            if (string.IsNullOrEmpty(kullaniciGuidStr))
                return new List<Urun>();

            var kullaniciGuid = Guid.Parse(kullaniciGuidStr);
            var veritabanindanKullanici = await _context.Kullancilar
                .Include(k => k.Siparisler)
                    .ThenInclude(s => s.SiparisDetaylari)
                .FirstOrDefaultAsync(k => k.KullaniciGuid == kullaniciGuid);

            if (veritabanindanKullanici == null || veritabanindanKullanici.Siparisler == null)
                return new List<Urun>();

            // Son alınan ürün adları
            var sonUrunAdlari = veritabanindanKullanici.Siparisler
                .SelectMany(s => s.SiparisDetaylari)
                .OrderByDescending(s => s.Id)
                .Select(s => s.Urun)
                .Where(u => u != null)
                .Select(u => u.UrunAdi)
                .Distinct()
                .Take(3)
                .ToList();

            if (!sonUrunAdlari.Any())
                return new List<Urun>();

            // ChatGPT prompt
            var prompt = $"Aşağıdaki ürünleri satın alan birine benzer ürünler öner. Sadece ürün adlarını virgülle ayırarak ver: {string.Join(", ", sonUrunAdlari)}";

            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _configuration["OpenAI:ApiKey"]);

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[] { new { role = "user", content = prompt } }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);

            if (!response.IsSuccessStatusCode)
                return new List<Urun>();

            var responseBody = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(responseBody);

            var cevap = doc.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

            var onerilenAdlar = cevap?
                .Split(',')
                .Select(x => x.Trim())
                .Where(x => !string.IsNullOrEmpty(x))
                .ToList();

            if (onerilenAdlar == null || !onerilenAdlar.Any())
                return new List<Urun>();

            return await _context.Urunler
                .Where(u => onerilenAdlar.Contains(u.UrunAdi))
                .ToListAsync();
        }
    }
}
