using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace E_Ticaret.Services
{
    public class YorumModerasyonService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public YorumModerasyonService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<bool> YorumOlumsuzMu(string yorum)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", _configuration["OpenAI:ApiKey"]);

            var payload = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new
                    {
                        role = "user",
                        content = $"Aşağıdaki yorumu analiz et. Hakaret, küfür, nefret, aşağılama varsa sadece 'true' yaz. Yoksa sadece 'false' yaz:\n\n\"{yorum}\""
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync("https://api.openai.com/v1/chat/completions", content);
                var responseBody = await response.Content.ReadAsStringAsync();

                // Log: API cevabı komple
                Console.WriteLine("📩 GPT API cevabı (tam JSON):");
                Console.WriteLine(responseBody);

                using var doc = JsonDocument.Parse(responseBody);

                if (doc.RootElement.TryGetProperty("choices", out var choices))
                {
                    var cevap = choices[0]
                        .GetProperty("message")
                        .GetProperty("content")
                        .GetString();

                    // Log: Sadece "true" veya "false"
                    Console.WriteLine("✅ GPT cevabı (true/false): " + cevap);

                    return cevap?.Trim().ToLower().Contains("true") ?? false;
                }

                Console.WriteLine("❗ 'choices' bulunamadı, olumsuz olarak işaretlenmedi.");
                return false;
            }
            catch (JsonException ex)
            {
                Console.WriteLine("❌ JSON parse hatası: " + ex.Message);
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Moderasyon genel hatası: " + ex.Message);
                return false;
            }
        }
    }
}
