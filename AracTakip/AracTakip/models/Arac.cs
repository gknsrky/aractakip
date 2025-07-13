namespace AracTakip.Models
{
    public class Arac
    {
        public string Id { get; set; } = string.Empty;
        public string Plaka { get; set; } = string.Empty;
        public string Marka { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Yil { get; set; }
        public int GuncelKilometre { get; set; }
        public string ArvCihazNo { get; set; } = string.Empty;
        public string SasiNo { get; set; } = string.Empty;
        public string MotorNo { get; set; } = string.Empty;
        public string RuhsatSeriNo { get; set; } = string.Empty;
        public string RuhsatSahibiFirma { get; set; } = string.Empty;
        public string AtananKullanici { get; set; }
        public DateTime? VizeBitisTarihi { get; set; }
        public DateTime? KaskoBitisTarihi { get; set; }
        public DateTime? SigortaBitisTarihi { get; set; }
        public string Notlar { get; set; } = string.Empty;
    }
}