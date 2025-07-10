namespace AracTakip.Models
{
    public class BakimKaydi
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string AracId { get; set; } = string.Empty;

        public DateTime BakimTarihi { get; set; } = DateTime.Now;
        public int BakimYapildigiKilometre { get; set; }

        public string YapilanIslemler { get; set; } = string.Empty;

        public string FaturaFotografUrl { get; set; } = string.Empty; // JPG/PNG
        public string FaturaPdfUrl { get; set; } = string.Empty;      // PDF

        public double ParcaMaliyeti { get; set; }
        public double IscilikMaliyeti { get; set; }

        public double ToplamTutar => ParcaMaliyeti + IscilikMaliyeti;
    }
}
