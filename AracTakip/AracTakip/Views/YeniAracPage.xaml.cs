using AracTakip.Models;
using AracTakip.Services;

namespace AracTakip.Views;

public partial class YeniAracPage : ContentPage
{
    private readonly FirebaseService _firebaseService;
    private bool _isEditMode = false;

    // Yeni Araç Ekleme için
    public YeniAracPage()
    {
        InitializeComponent();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
        Title = "Yeni Araç Ekle";
    }

    // Araç Düzenleme için
    public YeniAracPage(Arac aracToEdit)
    {
        InitializeComponent();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
        Title = "Aracý Düzenle";
        _isEditMode = true;

        PlakaEntry.Text = aracToEdit.Plaka;
        PlakaEntry.IsEnabled = false;
        MarkaEntry.Text = aracToEdit.Marka;
        ModelEntry.Text = aracToEdit.Model;
        YilEntry.Text = aracToEdit.Yil.ToString();
        KmEntry.Text = aracToEdit.GuncelKilometre.ToString();
        SasiEntry.Text = aracToEdit.SasiNo;
        MotorEntry.Text = aracToEdit.MotorNo;
        RuhsatSeriEntry.Text = aracToEdit.RuhsatSeriNo;
        // DÜZELTME 1: 'aracToedit' -> 'aracToEdit' olarak düzeltildi.
        FirmaEntry.Text = aracToEdit.RuhsatSahibiFirma;
        // DÜZELTME 2: 'ArvCihazNo' -> 'ArvEntry' olarak düzeltildi.
        ArvEntry.Text = aracToEdit.ArvCihazNo;
        NotlarEditor.Text = aracToEdit.Notlar;
        VizePicker.Date = aracToEdit.VizeBitisTarihi ?? DateTime.Now;
        KaskoPicker.Date = aracToEdit.KaskoBitisTarihi ?? DateTime.Now;
        SigortaPicker.Date = aracToEdit.SigortaBitisTarihi ?? DateTime.Now;
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        // ... (Doðrulama kodlarý ayný kalýyor) ...

        try
        {
            // YENÝ ARAC NESNESÝNÝN OLUÞTURULDUÐU BLOÐUN DOÐRU HALÝ
            var arac = new Arac
            {
                Plaka = PlakaEntry.Text.Trim().ToUpper(),
                Marka = MarkaEntry.Text.Trim(),
                Model = ModelEntry.Text.Trim(),
                Yil = int.Parse(YilEntry.Text),
                GuncelKilometre = int.Parse(KmEntry.Text),
                SasiNo = SasiEntry.Text?.Trim(),
                MotorNo = MotorEntry.Text?.Trim(),
                RuhsatSeriNo = RuhsatSeriEntry.Text?.Trim(),
                RuhsatSahibiFirma = FirmaEntry.Text?.Trim(),
                AtananKullanici = KullaniciEntry.Text?.Trim() ?? string.Empty,
                ArvCihazNo = ArvEntry.Text?.Trim(),
                Notlar = NotlarEditor.Text?.Trim(),
                VizeBitisTarihi = VizePicker.Date,
                KaskoBitisTarihi = KaskoPicker.Date,
                SigortaBitisTarihi = SigortaPicker.Date
            };

            await _firebaseService.AracEkleAsync(arac);

            var message = _isEditMode ? "Araç baþarýyla güncellendi." : "Araç baþarýyla kaydedildi.";
            await DisplayAlert("Baþarýlý", message, "Harika!");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "Ýþlem sýrasýnda bir sorun oluþtu: " + ex.Message, "Tamam");
        }
    }
}