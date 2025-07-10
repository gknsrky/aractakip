namespace AracTakip.Models
{
    public class KazaKaydi
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string AracId { get; set; } = string.Empty;

        public DateTime KazaTarihi { get; set; } = DateTime.Now;
        public string Aciklama { get; set; } = string.Empty;

        // Çoklu görsel ve belge desteği
        public List<string> FotografUrls { get; set; } = new();         // Kaza anı fotoğrafları
        public List<string> FaturaDosyaUrls { get; set; } = new();      // Fatura JPG / PDF dosyaları

        // Maliyet kalemleri
        public double ParcaMaliyeti { get; set; }
        public double IscilikMaliyeti { get; set; }

        public double ToplamTutar => ParcaMaliyeti + IscilikMaliyeti;
    }
}
