namespace AracTakip.Models
{
    public class Arac
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        // Temel bilgiler
        public string Plaka { get; set; } = string.Empty;
        public string Marka { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int Yil { get; set; }
        public int GuncelKilometre { get; set; }

        // Teknik bilgiler
        public string ArvCihazNo { get; set; } = string.Empty;
        public string SasiNo { get; set; } = string.Empty;
        public string MotorNo { get; set; } = string.Empty;

        // Ruhsat bilgileri
        public string RuhsatSeriNo { get; set; } = string.Empty;
        public string RuhsatSahibiFirma { get; set; } = string.Empty;

        // Evrak geçerlilik tarihleri
        public DateTime VizeBitisTarihi { get; set; }
        public DateTime KaskoBitisTarihi { get; set; }
        public DateTime SigortaBitisTarihi { get; set; }

        // Diğer bilgiler
        public string AtananSurucuAdi { get; set; } = string.Empty;
        public string Notlar { get; set; } = string.Empty;
    }
}
