using System.Collections.Generic; // Bu satırı eklemeyi unutmayın

namespace AracTakip.Models
{
    public class KazaKaydi
    {
        public string Id { get; set; }
        public string AracId { get; set; }
        public DateTime KazaTarihi { get; set; }
        public string Aciklama { get; set; }
        public List<string> FotografUrls { get; set; }
    }
}