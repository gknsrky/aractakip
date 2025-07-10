namespace AracTakip.Models
{
    public class Kullanici
    {
        /// <summary>
        /// Firebase Authentication tarafından verilen eşsiz kullanıcı ID’si
        /// </summary>
        public string Uid { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının e-posta adresi (giriş için kullanılır)
        /// </summary>
        public string Eposta { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcının adı ve soyadı
        /// </summary>
        public string AdSoyad { get; set; } = string.Empty;

        /// <summary>
        /// Kullanıcı rolü (admin: tam yetkili, viewer: sadece görüntüleyici)
        /// </summary>
        public string Rol { get; set; } = "viewer"; // veya "admin"
    }
}
