using AracTakip.Models; // Sadece ihtiyac�m�z olan using ifadesi

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
            // Y�l ve Kilometre alanlar�n�n say�sal olup olmad���n� kontrol ediyoruz.
            // Bu, int.Parse'dan daha g�venlidir ve program�n ��kmesini engeller.
            if (!int.TryParse(YilEntry.Text, out int yil))
            {
                await DisplayAlert("Hata", "L�tfen ge�erli bir Y�l giriniz.", "Tamam");
                return;
            }

            if (!int.TryParse(KmEntry.Text, out int kilometre))
            {
                await DisplayAlert("Hata", "L�tfen ge�erli bir Kilometre giriniz.", "Tamam");
                return;
            }

            // Yeni bir 'Arac' nesnesi olu�turup, formdaki verilerle dolduruyoruz.
            var yeniArac = new Arac
            {
                // Plakay� her zaman b�y�k harf ve bo�luksuz olarak kaydediyoruz.
                Plaka = PlakaEntry.Text?.Trim().ToUpper() ?? string.Empty,
                Marka = MarkaEntry.Text?.Trim() ?? string.Empty,
                Model = ModelEntry.Text?.Trim() ?? string.Empty,
                Yil = yil,
                GuncelKilometre = kilometre,

                // Yeni ekledi�imiz alanlar� da dolduruyoruz.
                ArvCihazNo = ArvEntry.Text?.Trim() ?? string.Empty,
                SasiNo = SasiEntry.Text?.Trim() ?? string.Empty,
                MotorNo = MotorEntry.Text?.Trim() ?? string.Empty,
                RuhsatSeriNo = RuhsatSeriEntry.Text?.Trim() ?? string.Empty,
                RuhsatSahibiFirma = FirmaEntry.Text?.Trim() ?? string.Empty,

                // D�ZELTME: '??' operat�rleri buradan kald�r�ld�.
                VizeBitisTarihi = VizePicker.Date,
                KaskoBitisTarihi = KaskoPicker.Date,
                SigortaBitisTarihi = SigortaPicker.Date,

                Notlar = NotlarEditor.Text?.Trim() ?? string.Empty
            };

            // TODO: Bu 'yeniArac' nesnesini Firebase'e kaydetme kodu buraya gelecek.

            // �imdilik, i�lemin ba�ar�l� oldu�unu bir uyar� ile g�steriyoruz.
            await DisplayAlert("Ba�ar�l�", $"{yeniArac.Plaka} plakal� ara� ba�ar�yla olu�turuldu.", "Harika!");

            // ��lem bittikten sonra, bir �nceki sayfaya geri d�n�yoruz.
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            // Beklenmedik bir hata olursa kullan�c�y� bilgilendiriyoruz.
            await DisplayAlert("Beklenmedik Hata", "Bir sorun olu�tu: " + ex.Message, "Tamam");
        }
    }
}