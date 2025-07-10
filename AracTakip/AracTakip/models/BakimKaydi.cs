namespace AracTakip.Models
{
    public class BakimKaydi
    {
        public string Id { get; set; }
        public string AracId { get; set; }
        public DateTime BakimTarihi { get; set; }
        public int BakimYapildigiKilometre { get; set; }
        public string YapilanIslemler { get; set; }
        public string FaturaFotografUrl { get; set; }
        public double Tutar { get; set; }
    }
}