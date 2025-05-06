using Microsoft.AspNetCore.Http;
using System.Text.Json;
using E_Ticaret.Models.Entities;

namespace E_Ticaret.OrtakKullanim
{
    public class CompareService
    {
        private const string SessionKey = "CompareList";
        private const string KategoriKey = "CompareKategoriId";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CompareService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public List<int> GetCompareList()
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var data = session.GetString(SessionKey);

            if (string.IsNullOrEmpty(data))
                return new List<int>();

            return JsonSerializer.Deserialize<List<int>>(data) ?? new List<int>();
        }
        public int GetCompareCount()
        {
            return GetCompareList().Count;
        }

        public void AddToCompare(Urun urun)
        {
            var list = GetCompareList();
            var session = _httpContextAccessor.HttpContext.Session;

            // İlk ürün ekleniyorsa kategori ID'yi kaydet
            if (!list.Any())
            {
                session.SetInt32(KategoriKey, urun.KategoriId ?? 0);
            }
            else
            {
                var kategoriId = session.GetInt32(KategoriKey);
                if (kategoriId != urun.KategoriId)
                {
                    throw new InvalidOperationException("Sadece aynı kategoriden ürünler karşılaştırılabilir.");
                }
            }

            if (!list.Contains(urun.Id) && list.Count < 5)
            {
                list.Add(urun.Id);
                SaveCompareList(list);
            }
        }

        public void RemoveFromCompare(int urunId)
        {
            var list = GetCompareList();
            list.Remove(urunId);
            SaveCompareList(list);

            // Liste tamamen boşaldıysa kategori de silinsin
            if (!list.Any())
            {
                _httpContextAccessor.HttpContext.Session.Remove(KategoriKey);
            }
        }

        public void ClearCompareList()
        {
            _httpContextAccessor.HttpContext.Session.Remove(SessionKey);
            _httpContextAccessor.HttpContext.Session.Remove(KategoriKey);
        }

        private void SaveCompareList(List<int> list)
        {
            var session = _httpContextAccessor.HttpContext.Session;
            var jsonData = JsonSerializer.Serialize(list);
            session.SetString(SessionKey, jsonData);
        }
    }
}
