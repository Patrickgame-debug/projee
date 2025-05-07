using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace E_Ticaret.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGptController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly HttpClient _httpClient;

        public ChatGptController(DatabaseContext context)
        {
            _context = context;
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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"); // 🔑 OpenAI API Key
        }

        [HttpPost("urunonerisi")]
        public async Task<IActionResult> UrunOnerisi([FromBody] ChatGptMesajModel mesajModel)
        {
            var oyunUrunleri = _context.Urunler
                .Where(u => u.Kategori.KategoriAdi.Contains("oyun") ||
                            u.UrunAdi.Contains("oyun") ||
                            u.Aciklama.Contains("oyun"))
                .Select(u => $"{u.UrunAdi} - {u.Aciklama} ({u.Fiyat}₺)")
                .ToList();

            var urunListesi = string.Join("\n", oyunUrunleri);

            string prompt = $"""
    Kullanıcı: {mesajModel.Mesaj}

    Elimde aşağıdaki oyun bilgisayarları var:
    {urunListesi}

    Bu listeden 2 veya 3 ürünü öner. Neden önerdiğini açıkla. 
    Kullanıcıya yardımcı olacak, kısa ve sade bir cevap yaz.
    """;

            var body = new
            {
                model = "gpt-4",
                messages = new[] {
            new { role = "user", content = prompt }
        }
            };

            var json = JsonConvert.SerializeObject(body);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://api.openai.com/v1/chat/completions", content);
            var responseJson = await response.Content.ReadAsStringAsync();

            dynamic result = JsonConvert.DeserializeObject(responseJson);
            string gptCevap = result?.choices?[0]?.message?.content ?? "Şu anda öneri veremiyorum.";

            return Ok(gptCevap);
        }

    }

    public class ChatGptMesajModel
    {
        public string Mesaj { get; set; }
    }
}
