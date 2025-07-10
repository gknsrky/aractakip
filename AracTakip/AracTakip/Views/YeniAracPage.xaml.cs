using AracTakip.Models;
using Microsoft.Maui;
using System.Formats.Tar;

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
            var arac = new Arac
            {
                Plaka = PlakaEntry.Text?.Trim().ToUpper(),
                Marka = MarkaEntry.Text?.Trim(),
                Model = ModelEntry.Text?.Trim(),
                Yil = int.Parse(YilEntry.Text),
                GuncelKilometre = int.Parse(KmEntry.Text),

                ArvCihazNo = ArvEntry.Text,
                SasiNo = SasiEntry.Text,
                MotorNo = MotorEntry.Text,

                RuhsatSeriNo = RuhsatSeriEntry.Text,
                RuhsatSahibiFirma = FirmaEntry.Text,

                VizeBitisTarihi = VizePicker.Date,
                KaskoBitisTarihi = KaskoPicker.Date,
                SigortaBitisTarihi = SigortaPicker.Date,

                Notlar = NotlarEditor.Text
            };

            // Firebase hazýr olmadýðýndan geçici test:
            await DisplayAlert("Araç Eklendi", $"Plaka: {arac.Plaka}\nMarka: {arac.Marka}", "Tamam");

            await Navigation.PopAsync(); // Ana sayfaya dön
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Lütfen tüm alanlarý doðru doldurunuz.\n" + ex.Message, "Tamam");
        }
    }
}
