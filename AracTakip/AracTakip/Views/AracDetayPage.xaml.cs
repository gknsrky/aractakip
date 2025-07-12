using AracTakip.Models;
using AracTakip.Views;
using Google.Cloud.Firestore;
using System.Collections.ObjectModel;

namespace AracTakip.Views;

public partial class AracDetayPage : ContentPage
{
    public Arac DetayArac { get; set; }
    public ObservableCollection<ArizaKaydi> ArizaKayitlari { get; set; } = new();
    public ObservableCollection<BakimKaydi> BakimKayitlari { get; set; } = new();
    public ObservableCollection<KazaKaydi> KazaKayitlari { get; set; } = new();

    public AracDetayPage(Arac arac)
    {
        InitializeComponent();
        DetayArac = arac;
        BindingContext = this;
        LoadKayitlarAsync();
    }

    private async void LoadKayitlarAsync()
    {
        var db = FirestoreDb.Create("aractakip-ee2de"); // TODO: Firebase proje ID'n ile deðiþtir

        // Arýza Kayýtlarý
        var arizaRef = db.Collection("Araclar").Document(DetayArac.Id).Collection("ArizaKayitlari");
        var arizaSnap = await arizaRef.GetSnapshotAsync();
        foreach (var doc in arizaSnap.Documents)
            ArizaKayitlari.Add(doc.ConvertTo<ArizaKaydi>());

        // Bakým Kayýtlarý
        var bakimRef = db.Collection("Araclar").Document(DetayArac.Id).Collection("BakimKayitlari");
        var bakimSnap = await bakimRef.GetSnapshotAsync();
        foreach (var doc in bakimSnap.Documents)
            BakimKayitlari.Add(doc.ConvertTo<BakimKaydi>());

        // Kaza Kayýtlarý
        var kazaRef = db.Collection("Araclar").Document(DetayArac.Id).Collection("KazaKayitlari");
        var kazaSnap = await kazaRef.GetSnapshotAsync();
        foreach (var doc in kazaSnap.Documents)
            KazaKayitlari.Add(doc.ConvertTo<KazaKaydi>());
    }

    private async void BakimEkleButton_Clicked(object sender, EventArgs e)
    {
        if (DetayArac != null)
        {
            await Navigation.PushAsync(new YeniBakimPage(DetayArac.Id));
        }
    }

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