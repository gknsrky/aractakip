using AracTakip.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace AracTakip.Services;

public class FirebaseService
{
    // ... Constructor ve LoginAsync, AracEkleAsync, GetAraclarAsync metotları burada aynı kalıyor ...
    #region Mevcut Metotlar
    private readonly string _apiKey;
    private readonly HttpClient _httpClient;
    public string IdToken { get; private set; }

    public FirebaseService(string apiKey, string storageBucket, string firestoreProjectId)
    {
        _apiKey = apiKey;
        _httpClient = new HttpClient();
    }

    public async Task<string> LoginAsync(string email, string password)
    {
        var requestUrl = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}";
        var requestData = new { email, password, returnSecureToken = true };
        var response = await _httpClient.PostAsJsonAsync(requestUrl, requestData);
        if (!response.IsSuccessStatusCode) { throw new Exception("E-posta veya şifre hatalı."); }
        var authResponse = await response.Content.ReadFromJsonAsync<FirebaseAuthResponse>();
        IdToken = authResponse?.IdToken;
        return IdToken;
    }

    public async Task AracEkleAsync(Arac arac)
    {
        if (string.IsNullOrEmpty(IdToken)) { throw new Exception("Kullanıcı girişi yapılmamış."); }
        var requestUrl = $"https://firestore.googleapis.com/v1/projects/aractakip-d8b16/databases/(default)/documents/araclar/{arac.Plaka}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdToken);
        var firestoreDocument = new { fields = new { Plaka = new { stringValue = arac.Plaka }, Marka = new { stringValue = arac.Marka }, Model = new { stringValue = arac.Model }, Yil = new { integerValue = arac.Yil.ToString() }, GuncelKilometre = new { integerValue = arac.GuncelKilometre.ToString() }, SasiNo = new { stringValue = arac.SasiNo ?? "" }, MotorNo = new { stringValue = arac.MotorNo ?? "" }, RuhsatSeriNo = new { stringValue = arac.RuhsatSeriNo ?? "" }, RuhsatSahibiFirma = new { stringValue = arac.RuhsatSahibiFirma ?? "" }, ArvCihazNo = new { stringValue = arac.ArvCihazNo ?? "" }, Notlar = new { stringValue = arac.Notlar ?? "" }, VizeBitisTarihi = arac.VizeBitisTarihi.HasValue ? new { timestampValue = arac.VizeBitisTarihi.Value.ToUniversalTime().ToString("o") } : null, KaskoBitisTarihi = arac.KaskoBitisTarihi.HasValue ? new { timestampValue = arac.KaskoBitisTarihi.Value.ToUniversalTime().ToString("o") } : null, SigortaBitisTarihi = arac.SigortaBitisTarihi.HasValue ? new { timestampValue = arac.SigortaBitisTarihi.Value.ToUniversalTime().ToString("o") } : null } };
        var options = new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull };
        var json = JsonSerializer.Serialize(firestoreDocument, options);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PatchAsync(requestUrl, content);
        if (!response.IsSuccessStatusCode) { var errorContent = await response.Content.ReadAsStringAsync(); throw new Exception($"Firestore'a araç eklenirken bir hata oluştu: {errorContent}"); }
    }
    public async Task BakimEkleAsync(string aracPlaka, BakimKaydi bakim)
    {
        if (string.IsNullOrEmpty(IdToken) || string.IsNullOrEmpty(aracPlaka))
        {
            throw new Exception("Kullanıcı girişi veya araç plakası eksik.");
        }

        // Bir aracın altındaki "BakimKayitlari" koleksiyonuna yeni bir doküman ekliyoruz.
        // Sona eklediğimiz "?key=" parametresi, API anahtarımızı gerektirir.
        // Firebase, doküman ID'sini kendisi otomatik oluşturacaktır.
        var requestUrl = $"https://firestore.googleapis.com/v1/projects/aractakip-d8b16/databases/(default)/documents/araclar/{aracPlaka}/BakimKayitlari?key={_apiKey}";

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IdToken);

        // BakimKaydi nesnesini Firestore'un anlayacağı formata çeviriyoruz.
        var firestoreDocument = new
        {
            fields = new
            {
                Aciklama = new { stringValue = bakim.Aciklama },
                Tarih = new { timestampValue = bakim.Tarih.ToUniversalTime().ToString("o") },
                Maliyet = new { doubleValue = bakim.Maliyet }
            }
        };

        var json = JsonSerializer.Serialize(firestoreDocument);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // POST metodu, koleksiyona yeni bir doküman ekler.
        var response = await _httpClient.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Bakım kaydı eklenirken bir hata oluştu: {errorContent}");
        }

    }
    public async Task ArizaEkleAsync(string aracPlaka, ArizaKaydi ariza)
    {
        if (string.IsNullOrEmpty(IdToken) || string.IsNullOrEmpty(aracPlaka))
        {
            throw new Exception("Kullanıcı girişi veya araç plakası eksik.");
        }

        // Bu sefer "ArizaKayitlari" alt koleksiyonuna yeni bir doküman ekliyoruz.
        var requestUrl = $"https://firestore.googleapis.com/v1/projects/aractakip-d8b16/databases/(default)/documents/araclar/{aracPlaka}/ArizaKayitlari?key={_apiKey}";

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IdToken);

        // ArizaKaydi nesnesini Firestore'un anlayacağı formata çeviriyoruz.
        var firestoreDocument = new
        {
            fields = new
            {
                Aciklama = new { stringValue = ariza.Aciklama },
                Tarih = new { timestampValue = ariza.Tarih.ToUniversalTime().ToString("o") },
                Maliyet = new { doubleValue = ariza.Maliyet }
            }
        };

        var json = JsonSerializer.Serialize(firestoreDocument);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Arıza kaydı eklenirken bir hata oluştu: {errorContent}");
        }
    }
    public async Task KazaEkleAsync(string aracPlaka, KazaKaydi kaza)
    {
        if (string.IsNullOrEmpty(IdToken) || string.IsNullOrEmpty(aracPlaka))
        {
            throw new Exception("Kullanıcı girişi veya araç plakası eksik.");
        }

        // "KazaKayitlari" alt koleksiyonuna yeni bir doküman ekliyoruz.
        var requestUrl = $"https://firestore.googleapis.com/v1/projects/aractakip-d8b16/databases/(default)/documents/araclar/{aracPlaka}/KazaKayitlari?key={_apiKey}";

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", IdToken);

        // KazaKaydi nesnesini Firestore'un anlayacağı formata çeviriyoruz.
        var firestoreDocument = new
        {
            fields = new
            {
                Aciklama = new { stringValue = kaza.Aciklama },
                Tarih = new { timestampValue = kaza.Tarih.ToUniversalTime().ToString("o") },
                Tutar = new { doubleValue = kaza.Tutar } // Kaza modelinde Tutar kullanıyoruz
            }
        };

        var json = JsonSerializer.Serialize(firestoreDocument);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await _httpClient.PostAsync(requestUrl, content);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Kaza kaydı eklenirken bir hata oluştu: {errorContent}");
        }
    }
    public async Task<List<Arac>> GetAraclarAsync()
    {
        if (string.IsNullOrEmpty(IdToken)) return new List<Arac>();
        var requestUrl = $"https://firestore.googleapis.com/v1/projects/aractakip-d8b16/databases/(default)/documents/araclar";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdToken);
        var response = await _httpClient.GetAsync(requestUrl);
        if (!response.IsSuccessStatusCode) return new List<Arac>();
        var jsonResponse = await response.Content.ReadAsStringAsync();
        var aracListesi = new List<Arac>();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        var firestoreResponse = JsonSerializer.Deserialize<FirestoreQueryResponse<AracFields>>(jsonResponse, options);
        if (firestoreResponse?.Documents != null) { foreach (var doc in firestoreResponse.Documents) { if (doc.Fields != null) aracListesi.Add(doc.Fields.ToArac()); } }
        return aracListesi;
    }
    #endregion

    // ---- YENİ EKLENEN METOTLAR ----

    // Belirli bir aracın alt koleksiyonunu getiren genel bir metot
    private async Task<T> GetSubCollectionAsync<T>(string aracPlaka, string subCollectionName)
    {
        var requestUrl = $"https://firestore.googleapis.com/v1/projects/aractakip-d8b16/databases/(default)/documents/araclar/{aracPlaka}/{subCollectionName}";
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", IdToken);
        var response = await _httpClient.GetAsync(requestUrl);
        if (!response.IsSuccessStatusCode) return default(T);

        var jsonResponse = await response.Content.ReadAsStringAsync();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        return JsonSerializer.Deserialize<T>(jsonResponse, options);
    }

    public async Task<List<BakimKaydi>> GetBakimKayitlariAsync(string aracPlaka)
    {
        var response = await GetSubCollectionAsync<FirestoreQueryResponse<BakimKaydiFields>>(aracPlaka, "BakimKayitlari");
        return response?.Documents?.Select(d => d.Fields.ToBakimKaydi()).ToList() ?? new List<BakimKaydi>();
    }

    public async Task<List<KazaKaydi>> GetKazaKayitlariAsync(string aracPlaka)
    {
        var response = await GetSubCollectionAsync<FirestoreQueryResponse<KazaKaydiFields>>(aracPlaka, "KazaKayitlari");
        return response?.Documents?.Select(d => d.Fields.ToKazaKaydi()).ToList() ?? new List<KazaKaydi>();
    }

    public async Task<List<ArizaKaydi>> GetArizaKayitlariAsync(string aracPlaka)
    {
        var response = await GetSubCollectionAsync<FirestoreQueryResponse<ArizaKaydiFields>>(aracPlaka, "ArizaKayitlari");
        return response?.Documents?.Select(d => d.Fields.ToArizaKaydi()).ToList() ?? new List<ArizaKaydi>();
    }
}


// ---- YARDIMCI SINIFLARIN SON HALİ (FirebaseService sınıfının DIŞI) ----

#region Yardımcı Sınıflar
public class FirebaseAuthResponse { [JsonPropertyName("idToken")] public string IdToken { get; set; } }

// Artık Generic (Genel) bir yapı kullanıyoruz
public class FirestoreQueryResponse<T> { public List<FirestoreDocument<T>> Documents { get; set; } }
public class FirestoreDocument<T> { public T Fields { get; set; } }

// Alanları ve Değerleri Tanımlayan Sınıflar
public class StringValue { [JsonPropertyName("stringValue")] public string Value { get; set; } }
public class IntegerValue { [JsonPropertyName("integerValue")] public string Value { get; set; } }
public class TimestampValue { [JsonPropertyName("timestampValue")] public DateTime? Value { get; set; } }
public class DoubleValue { [JsonPropertyName("doubleValue")] public double Value { get; set; } }
// Arac Sınıfı için Veri Yapıları
// Arac Sınıfı için Veri Yapıları
public class AracFields
{
    public StringValue Plaka { get; set; }
    public StringValue Marka { get; set; }
    public StringValue Model { get; set; }
    public IntegerValue Yil { get; set; }
    public IntegerValue GuncelKilometre { get; set; }
    public StringValue SasiNo { get; set; }
    public StringValue MotorNo { get; set; }
    public StringValue RuhsatSeriNo { get; set; }
    public StringValue RuhsatSahibiFirma { get; set; }
    public StringValue ArvCihazNo { get; set; }
    public StringValue Notlar { get; set; }
    public StringValue AtananKullanici { get; set; }
    public TimestampValue VizeBitisTarihi { get; set; }
    public TimestampValue KaskoBitisTarihi { get; set; }
    public TimestampValue SigortaBitisTarihi { get; set; }

    public Arac ToArac() => new Arac
    {
        Plaka = Plaka?.Value,
        Marka = Marka?.Value,
        Model = Model?.Value,
        Yil = Convert.ToInt32(Yil?.Value ?? "0"),
        GuncelKilometre = Convert.ToInt32(GuncelKilometre?.Value ?? "0"),
        SasiNo = SasiNo?.Value,
        MotorNo = MotorNo?.Value,
        RuhsatSeriNo = RuhsatSeriNo?.Value,
        RuhsatSahibiFirma = RuhsatSahibiFirma?.Value,
        ArvCihazNo = ArvCihazNo?.Value,
        Notlar = Notlar?.Value,
        AtananKullanici = AtananKullanici?.Value,
        VizeBitisTarihi = VizeBitisTarihi?.Value,
        KaskoBitisTarihi = KaskoBitisTarihi?.Value,
        SigortaBitisTarihi = SigortaBitisTarihi?.Value
    };
}

// BakimKaydiFields güncellendi
public class BakimKaydiFields
{
    public StringValue Aciklama { get; set; }
    public TimestampValue Tarih { get; set; }
    public DoubleValue Maliyet { get; set; } // IntegerValue -> DoubleValue olarak değiştirildi
    public BakimKaydi ToBakimKaydi() => new BakimKaydi { Aciklama = Aciklama?.Value, Tarih = Tarih?.Value ?? DateTime.MinValue, Maliyet = Maliyet?.Value ?? 0 };
}

// KazaKaydiFields güncellendi
public class KazaKaydiFields
{
    public StringValue Aciklama { get; set; }
    public TimestampValue Tarih { get; set; }
    public DoubleValue Tutar { get; set; } // IntegerValue -> DoubleValue olarak değiştirildi
    public KazaKaydi ToKazaKaydi() => new KazaKaydi { Aciklama = Aciklama?.Value, Tarih = Tarih?.Value ?? DateTime.MinValue, Tutar = Tutar?.Value ?? 0 };
}

// ArizaKaydiFields güncellendi
public class ArizaKaydiFields
{
    public StringValue Aciklama { get; set; }
    public TimestampValue Tarih { get; set; }
    public DoubleValue Maliyet { get; set; } // IntegerValue -> DoubleValue olarak değiştirildi
    public ArizaKaydi ToArizaKaydi() => new ArizaKaydi { Aciklama = Aciklama?.Value, Tarih = Tarih?.Value ?? DateTime.MinValue, Maliyet = Maliyet?.Value ?? 0 };
}
#endregion