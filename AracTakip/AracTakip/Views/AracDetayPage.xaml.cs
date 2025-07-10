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
        BindingContext = DetayArac; // Bu kullan�m da tamamen do�rudur.
    }

    private async void BakimEkleButton_Clicked(object sender, EventArgs e)
    {
        // D�ZELTME: 'SecilenArac' yerine 'DetayArac' kullan�yoruz.
        if (DetayArac != null)
        {
            // D�ZELTME: 'SecilenArac.Id' yerine 'DetayArac.Id' kullan�yoruz.
            await Navigation.PushAsync(new YeniBakimPage(DetayArac.Id));
        }
    }

    // Bu metotlar�n�za dokunmad�m, oldu�u gibi duruyorlar.
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