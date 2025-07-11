using AracTakip.Models;
using AracTakip.Views;
using System.Collections.ObjectModel;

namespace AracTakip;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Arac> Araclar { get; set; } = new();
    private List<Arac> TumAraclar { get; set; } = new(); // Arama için

    public MainPage()
    {
        InitializeComponent(); // Bu satırın çalışabilmesi için MainPage.xaml dosyasının doğru şekilde tanımlanmış olması gerekir.

        // Dummy veri
        TumAraclar = new List<Arac>
        {
            new Arac { Plaka = "34 ABC 123", Marka = "Ford", Model = "Transit", GuncelKilometre = 123000 },
            new Arac { Plaka = "06 DEF 456", Marka = "Fiat", Model = "Doblo", GuncelKilometre = 87000 }
        };

        foreach (var arac in TumAraclar)
            Araclar.Add(arac);

        BindingContext = this;
    }


    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string filtre = e.NewTextValue?.ToLower() ?? "";
        var sonuc = TumAraclar.Where(a =>
            a.Plaka.ToLower().Contains(filtre) ||
            a.Marka.ToLower().Contains(filtre) ||
            a.Model.ToLower().Contains(filtre));

        Araclar.Clear();
        foreach (var arac in sonuc)
            Araclar.Add(arac);
    }

    private async void AracListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Arac secilenArac)
        {
            await Navigation.PushAsync(new AracDetayPage(secilenArac));
            (sender as CollectionView).SelectedItem = null;
        }
    }

    private async void OnYeniAracEkleClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new YeniAracPage());
    }

    private async void OnTakvimClicked(object sender, EventArgs e)
    {
        // Artık bizi yeni oluşturduğumuz takvim sayfamıza yönlendiriyor.
        // using AracTakip.Views; satırının dosyanın en üstünde olduğundan emin ol.
        await Navigation.PushAsync(new CalendarPage());
    }
}
