namespace E_Ticaret.Models.Entities
{
    public class UrunDetayModeli
    {

        public Urun urun { get; set; }
        public List<Urun>? ilgiliurunler { get; set; }



        // 05.05.2025 YENİ EKLEDİM

        // Yorumlar ve analizler
        public List<Yorum>? yorumlar { get; set; }
        public double? ortalamaPuan { get; set; }
        public int? toplamYorum { get; set; }
        public Dictionary<int, int>? puanDagilimi { get; set; } = new();

    }
}
