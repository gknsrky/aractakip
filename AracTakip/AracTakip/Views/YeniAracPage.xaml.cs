using AracTakip.Models; // Sadece ihtiyacýmýz olan using ifadesi

namespace AracTakip.Views;

public partial class YeniAracPage : ContentPage
{
    public YeniAracPage()
    {
        InitializeComponent();
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        try
        {
            // Yýl ve Kilometre alanlarýnýn sayýsal olup olmadýðýný kontrol ediyoruz.
            // Bu, int.Parse'dan daha güvenlidir ve programýn çökmesini engeller.
            if (!int.TryParse(YilEntry.Text, out int yil))
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir Yýl giriniz.", "Tamam");
                return;
            }

            if (!int.TryParse(KmEntry.Text, out int kilometre))
            {
                await DisplayAlert("Hata", "Lütfen geçerli bir Kilometre giriniz.", "Tamam");
                return;
            }

            // Yeni bir 'Arac' nesnesi oluþturup, formdaki verilerle dolduruyoruz.
            var yeniArac = new Arac
            {
                // Plakayý her zaman büyük harf ve boþluksuz olarak kaydediyoruz.
                Plaka = PlakaEntry.Text?.Trim().ToUpper() ?? string.Empty,
                Marka = MarkaEntry.Text?.Trim() ?? string.Empty,
                Model = ModelEntry.Text?.Trim() ?? string.Empty,
                Yil = yil,
                GuncelKilometre = kilometre,

                // Yeni eklediðimiz alanlarý da dolduruyoruz.
                ArvCihazNo = ArvEntry.Text?.Trim() ?? string.Empty,
                SasiNo = SasiEntry.Text?.Trim() ?? string.Empty,
                MotorNo = MotorEntry.Text?.Trim() ?? string.Empty,
                RuhsatSeriNo = RuhsatSeriEntry.Text?.Trim() ?? string.Empty,
                RuhsatSahibiFirma = FirmaEntry.Text?.Trim() ?? string.Empty,

                // DÜZELTME: '??' operatörleri buradan kaldýrýldý.
                VizeBitisTarihi = VizePicker.Date,
                KaskoBitisTarihi = KaskoPicker.Date,
                SigortaBitisTarihi = SigortaPicker.Date,

                Notlar = NotlarEditor.Text?.Trim() ?? string.Empty
            };

            // TODO: Bu 'yeniArac' nesnesini Firebase'e kaydetme kodu buraya gelecek.

            // Þimdilik, iþlemin baþarýlý olduðunu bir uyarý ile gösteriyoruz.
            await DisplayAlert("Baþarýlý", $"{yeniArac.Plaka} plakalý araç baþarýyla oluþturuldu.", "Harika!");

            // Ýþlem bittikten sonra, bir önceki sayfaya geri dönüyoruz.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Beklenmedik bir hata olursa kullanýcýyý bilgilendiriyoruz.
            await DisplayAlert("Beklenmedik Hata", "Bir sorun oluþtu: " + ex.Message, "Tamam");
        }
    }
}