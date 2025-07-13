using AracTakip.Models;
using AracTakip.Services;

namespace AracTakip.Views;

public partial class YeniBakimPage : ContentPage
{
    private readonly string _aracPlaka;
    private readonly FirebaseService _firebaseService;

    public YeniBakimPage(string aracPlaka)
    {
        InitializeComponent();
        _aracPlaka = aracPlaka;
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(AciklamaEditor.Text) || string.IsNullOrWhiteSpace(MaliyetEntry.Text))
        {
            await DisplayAlert("Hata", "L�tfen t�m alanlar� doldurun.", "Tamam");
            return;
        }

        if (!double.TryParse(MaliyetEntry.Text, out double maliyet))
        {
            await DisplayAlert("Hata", "L�tfen Maliyet alan�na ge�erli bir say� girin.", "Tamam");
            return;
        }

        try
        {
            var yeniBakim = new BakimKaydi
            {
                Tarih = BakimTarihiPicker.Date,
                Aciklama = AciklamaEditor.Text, // Art�k AciklamaEditor'� tan�yacak
                Maliyet = maliyet
            };

            await _firebaseService.BakimEkleAsync(_aracPlaka, yeniBakim);

            await DisplayAlert("Ba�ar�l�", "Bak�m kayd� ba�ar�yla eklendi.", "Harika!");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", ex.Message, "Tamam");
        }
    }
}