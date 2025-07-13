using AracTakip.Models;
using AracTakip.Services;
using AracTakip.Views;
using System.Collections.ObjectModel;

namespace AracTakip;

public partial class MainPage : ContentPage
{
    public ObservableCollection<Arac> Araclar { get; set; }
    private List<Arac> _tumAraclar;
    private readonly FirebaseService _firebaseService;

    public MainPage()
    {
        InitializeComponent();
        Araclar = new ObservableCollection<Arac>();
        _tumAraclar = new List<Arac>();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
        this.BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await VerileriYukle();
    }

    private async Task VerileriYukle()
    {
        try
        {
            _tumAraclar = await _firebaseService.GetAraclarAsync();

            Araclar.Clear();
            foreach (var arac in _tumAraclar)
            {
                Araclar.Add(arac);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Araçlar yüklenirken bir sorun oluştu: " + ex.Message, "Tamam");
        }
    }

    // Arama metodunun içini de basit bir filtreleme ile doldurdum.
    private void OnSearchTextChanged(object sender, TextChangedEventArgs e)
    {
        string filtre = e.NewTextValue?.ToLower() ?? "";
        var sonuc = _tumAraclar.Where(a =>
            (a.Plaka?.ToLower() ?? "").Contains(filtre) ||
            (a.Marka?.ToLower() ?? "").Contains(filtre) ||
            (a.Model?.ToLower() ?? "").Contains(filtre));

        Araclar.Clear();
        foreach (var arac in sonuc)
            Araclar.Add(arac);
    }

    // 2. HATANIN ÇÖZÜMÜ: Bu metodun içini dolduruyoruz.
    private async void AracListesi_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Arac secilenArac)
        {
            // Tıklanan aracın detay sayfasına yönlendiriyoruz.
            await Navigation.PushAsync(new AracDetayPage(secilenArac));

            // Seçimi geri temizliyoruz ki aynı elemana tekrar tıklanabilsin.
            (sender as CollectionView).SelectedItem = null;
        }
    }

    // 1. HATANIN ÇÖZÜMÜ: Bu metodun içini dolduruyoruz.
    private async void OnYeniAracEkleClicked(object sender, EventArgs e)
    {
        // Yeni araç ekleme sayfasına yönlendiriyoruz.
        await Navigation.PushAsync(new YeniAracPage());
    }

    private async void OnTakvimClicked(object sender, EventArgs e)
    {
        // Takvim sayfasına yönlendiriyoruz.
        await Navigation.PushAsync(new CalendarPage());
    }
}