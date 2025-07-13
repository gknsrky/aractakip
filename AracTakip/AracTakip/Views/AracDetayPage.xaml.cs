using AracTakip.Models;
using AracTakip.Services;
using System.Collections.ObjectModel;

namespace AracTakip.Views;

public partial class AracDetayPage : ContentPage
{
    public Arac DetayArac { get; set; }
    public ObservableCollection<BakimKaydi> BakimKayitlari { get; set; }
    public ObservableCollection<KazaKaydi> KazaKayitlari { get; set; }
    public ObservableCollection<ArizaKaydi> ArizaKayitlari { get; set; }

    private readonly FirebaseService _firebaseService;

    public AracDetayPage(Arac arac)
    {
        InitializeComponent();
        DetayArac = arac;

        BakimKayitlari = new ObservableCollection<BakimKaydi>();
        KazaKayitlari = new ObservableCollection<KazaKaydi>();
        ArizaKayitlari = new ObservableCollection<ArizaKaydi>();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();

        this.BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAllDataAsync();
    }

    private async Task LoadAllDataAsync()
    {
        if (DetayArac == null || string.IsNullOrEmpty(DetayArac.Plaka)) return;

        try
        {
            var bakimTask = _firebaseService.GetBakimKayitlariAsync(DetayArac.Plaka);
            var kazaTask = _firebaseService.GetKazaKayitlariAsync(DetayArac.Plaka);
            var arizaTask = _firebaseService.GetArizaKayitlariAsync(DetayArac.Plaka);

            await Task.WhenAll(bakimTask, kazaTask, arizaTask);

            BakimKayitlari.Clear();
            foreach (var bakim in bakimTask.Result) { BakimKayitlari.Add(bakim); }

            KazaKayitlari.Clear();
            foreach (var kaza in kazaTask.Result) { KazaKayitlari.Add(kaza); }

            ArizaKayitlari.Clear();
            foreach (var ariza in arizaTask.Result) { ArizaKayitlari.Add(ariza); }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Kayýtlar yüklenirken bir sorun oluþtu: " + ex.Message, "Tamam");
        }
    }
    
    // BUTON METOTLARI GÜNCELLENDÝ
    private async void BakimEkleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
            await Navigation.PushAsync(new YeniBakimPage(DetayArac.Plaka));
    }

    private async void KazaEkleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
            await Navigation.PushAsync(new YeniKazaPage(DetayArac.Plaka));
    }
    private async void DuzenleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
        {
            // YeniAracPage'i, düzenleme için olan constructor'ýný kullanarak açýyoruz
            // ve düzenlenecek olan aracý ona gönderiyoruz.
            await Navigation.PushAsync(new YeniAracPage(DetayArac));
        }
    }
    private async void ArizaEkleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
            await Navigation.PushAsync(new YeniArizaPage(DetayArac.Plaka));
    }
}
