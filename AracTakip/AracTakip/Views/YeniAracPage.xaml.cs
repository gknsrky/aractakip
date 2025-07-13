using AracTakip.Models;
using AracTakip.Services;

namespace AracTakip.Views;

public partial class YeniAracPage : ContentPage
{
    private readonly FirebaseService _firebaseService;
    private bool _isEditMode = false;

    // Yeni Ara� Ekleme i�in
    public YeniAracPage()
    {
        InitializeComponent();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
        Title = "Yeni Ara� Ekle";
    }

    // Ara� D�zenleme i�in
    public YeniAracPage(Arac aracToEdit)
    {
        InitializeComponent();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
        Title = "Arac� D�zenle";
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
        // D�ZELTME 1: 'aracToedit' -> 'aracToEdit' olarak d�zeltildi.
        FirmaEntry.Text = aracToEdit.RuhsatSahibiFirma;
        // D�ZELTME 2: 'ArvCihazNo' -> 'ArvEntry' olarak d�zeltildi.
        ArvEntry.Text = aracToEdit.ArvCihazNo;
        NotlarEditor.Text = aracToEdit.Notlar;
        VizePicker.Date = aracToEdit.VizeBitisTarihi ?? DateTime.Now;
        KaskoPicker.Date = aracToEdit.KaskoBitisTarihi ?? DateTime.Now;
        SigortaPicker.Date = aracToEdit.SigortaBitisTarihi ?? DateTime.Now;
    }

    private async void OnKaydetClicked(object sender, EventArgs e)
    {
        // ... (Do�rulama kodlar� ayn� kal�yor) ...

        try
        {
            // YEN� ARAC NESNES�N�N OLU�TURULDU�U BLO�UN DO�RU HAL�
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

            var message = _isEditMode ? "Ara� ba�ar�yla g�ncellendi." : "Ara� ba�ar�yla kaydedildi.";
            await DisplayAlert("Ba�ar�l�", message, "Harika!");

            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Hata", "��lem s�ras�nda bir sorun olu�tu: " + ex.Message, "Tamam");
        }
    }
}