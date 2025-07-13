using AracTakip.Models;
using AracTakip.Services;

namespace AracTakip.Views;

public partial class YeniArizaPage : ContentPage
{
    private readonly string _aracPlaka;
    private readonly FirebaseService _firebaseService;

    public YeniArizaPage(string aracPlaka)
    {
        InitializeComponent();
        _aracPlaka = aracPlaka;
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        // Basit bir maliyet hesaplamasý
        double.TryParse(ParcaMaliyetEntry.Text, out double parcaMaliyeti);
        double.TryParse(IscilikMaliyetEntry.Text, out double iscilikMaliyeti);
        double toplamMaliyet = parcaMaliyeti + iscilikMaliyeti;

        try
        {
            var yeniAriza = new ArizaKaydi
            {
                Tarih = ArizaTarihiPicker.Date,
                // Ýki açýklamayý birleþtirerek tek bir alana kaydediyoruz
                Aciklama = $"ARIZA: {ArizaAciklamaEditor.Text}\nYAPILAN ONARIM: {OnarimAciklamaEditor.Text}",
                Maliyet = toplamMaliyet
            };

            await _firebaseService.ArizaEkleAsync(_aracPlaka, yeniAriza);

            await DisplayAlert("Baþarýlý", "Arýza kaydý baþarýyla eklendi.", "Harika!");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", ex.Message, "Tamam");
        }
    }
}