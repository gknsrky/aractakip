using AracTakip.Models;
using AracTakip.Services;

namespace AracTakip.Views;

public partial class YeniKazaPage : ContentPage
{
    private readonly string _aracPlaka;
    private readonly FirebaseService _firebaseService;

    public YeniKazaPage(string aracPlaka)
    {
        InitializeComponent();
        _aracPlaka = aracPlaka;
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(AciklamaEditor.Text) || string.IsNullOrWhiteSpace(TutarEntry.Text))
        {
            await DisplayAlert("Hata", "L�tfen t�m alanlar� doldurun.", "Tamam");
            return;
        }

        if (!double.TryParse(TutarEntry.Text, out double tutar))
        {
            await DisplayAlert("Hata", "L�tfen Tutar alan�na ge�erli bir say� girin.", "Tamam");
            return;
        }

        try
        {
            var yeniKaza = new KazaKaydi
            {
                Tarih = KazaTarihiPicker.Date,
                Aciklama = AciklamaEditor.Text,
                Tutar = tutar
            };

            await _firebaseService.KazaEkleAsync(_aracPlaka, yeniKaza);

            await DisplayAlert("Ba�ar�l�", "Kaza kayd� ba�ar�yla eklendi.", "Harika!");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", ex.Message, "Tamam");
        }
    }
}