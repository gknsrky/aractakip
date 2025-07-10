namespace AracTakip.Models
{
    public class ArizaKaydi
    {
        public string Id { get; set; } = string.Empty;
        public string AracId { get; set; } = string.Empty;
        public DateTime ArizaTarihi { get; set; }
        public string ArizaAciklamasi { get; set; } = string.Empty;
        public string YapilanOnarim { get; set; } = string.Empty;
        public double ParcaMaliyeti { get; set; }
        public double IscilikMaliyeti { get; set; }
        public string Durum { get; set; } = "Açık";
    }
}