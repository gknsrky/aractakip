namespace AracTakip.Models
{
    public class Arac
    {
        public string Id { get; set; }
        public string Plaka { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yil { get; set; }
        public int GuncelKilometre { get; set; }
        public DateTime VizeBitisTarihi { get; set; }
        public DateTime KaskoBitisTarihi { get; set; }
        public DateTime SigortaBitisTarihi { get; set; }
        public string AtananSurucuAdi { get; set; }
        public string Notlar { get; set; }
    }
}