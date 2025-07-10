using AracTakip.Models;
using AracTakip.Views; // Bu using ifadesini de ekleyelim.

namespace AracTakip.Views;

public partial class AracDetayPage : ContentPage
{
    public Arac DetayArac { get; set; }

    public AracDetayPage(Arac arac)
    {
        InitializeComponent();
        DetayArac = arac;
        BindingContext = DetayArac; // Bu kullaným da tamamen doðrudur.
    }

    private async void BakimEkleButton_Clicked(object sender, EventArgs e)
    {
        // DÜZELTME: 'SecilenArac' yerine 'DetayArac' kullanýyoruz.
        if (DetayArac != null)
        {
            // DÜZELTME: 'SecilenArac.Id' yerine 'DetayArac.Id' kullanýyoruz.
            await Navigation.PushAsync(new YeniBakimPage(DetayArac.Id));
        }
    }

    // Bu metotlarýnýza dokunmadým, olduðu gibi duruyorlar.
    private async void KazaEkleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
        {
            await Navigation.PushAsync(new YeniKazaPage(DetayArac.Id));
        }
    }

    private async void ArizaEkleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
        {
            await Navigation.PushAsync(new YeniArizaPage(DetayArac.Id));
        }
    }
}