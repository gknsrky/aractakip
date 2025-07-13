using AracTakip.Models;
using AracTakip.Services;
using System.Collections.ObjectModel;

namespace AracTakip.Views;

public class Etkinlik
{
    public DateTime Tarih { get; set; }
    public string Plaka { get; set; }
    public string Aciklama { get; set; }
}

public partial class CalendarPage : ContentPage
{
    private List<Etkinlik> _tumEtkinlikler = new();
    public ObservableCollection<Etkinlik> GosterilecekEtkinlikler { get; set; }
    private readonly FirebaseService _firebaseService;

    public CalendarPage()
    {
        InitializeComponent();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
        GosterilecekEtkinlikler = new ObservableCollection<Etkinlik>();
        this.BindingContext = this;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await LoadAndFilterEvents();
    }

    private async Task LoadAndFilterEvents()
    {
        LoadingIndicator.IsVisible = true;
        EventsListView.IsVisible = false;

        _tumEtkinlikler.Clear();
        var aracListesi = await _firebaseService.GetAraclarAsync();

        foreach (var arac in aracListesi)
        {
            if (arac.VizeBitisTarihi.HasValue) _tumEtkinlikler.Add(new Etkinlik { Tarih = arac.VizeBitisTarihi.Value, Plaka = arac.Plaka, Aciklama = "Vize Bitiþ" });
            if (arac.SigortaBitisTarihi.HasValue) _tumEtkinlikler.Add(new Etkinlik { Tarih = arac.SigortaBitisTarihi.Value, Plaka = arac.Plaka, Aciklama = "Sigorta Bitiþ" });
            if (arac.KaskoBitisTarihi.HasValue) _tumEtkinlikler.Add(new Etkinlik { Tarih = arac.KaskoBitisTarihi.Value, Plaka = arac.Plaka, Aciklama = "Kasko Bitiþ" });
        }

        ApplyFilters();
        LoadingIndicator.IsVisible = false;
    }

    private void ApplyFilters()
    {
        DateTime startDate;
        DateTime endDate;

        if (DateRangeSwitch.IsToggled)
        {
            // Özel aralýk modu
            startDate = StartDatePicker.Date;
            endDate = EndDatePicker.Date;
            DateRangeLabel.Text = $"{startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} Aralýðý";
        }
        else
        {
            // Varsayýlan mod: Gelecek 7 gün
            startDate = DateTime.Today;
            endDate = startDate.AddDays(7);
            DateRangeLabel.Text = "Gelecek 7 Günün Etkinlikleri";
        }

        var sonuclar = _tumEtkinlikler.Where(e =>
            e.Tarih.Date >= startDate && e.Tarih.Date <= endDate &&
            ((VizeFilter.IsChecked && e.Aciklama == "Vize Bitiþ") ||
             (SigortaFilter.IsChecked && e.Aciklama == "Sigorta Bitiþ") ||
             (KaskoFilter.IsChecked && e.Aciklama == "Kasko Bitiþ"))
        ).OrderBy(e => e.Tarih).ToList();

        GosterilecekEtkinlikler.Clear();
        foreach (var etkinlik in sonuclar)
        {
            GosterilecekEtkinlikler.Add(etkinlik);
        }

        ResultLabel.IsVisible = !sonuclar.Any();
        EventsListView.IsVisible = sonuclar.Any();
    }

    // Checkbox'lar deðiþtiðinde filtreyi uygula
    private void Filter_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_tumEtkinlikler == null) return;
        ApplyFilters();
    }

    // Özel tarih seçildiðinde filtreyi uygula
    private void Filter_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (_tumEtkinlikler == null) return;
        ApplyFilters();
    }

    // Anahtar (Switch) durumu deðiþtiðinde hem tarih seçicileri göster/gizle hem de filtreyi uygula
    private void DateRangeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        CustomDateRangeGrid.IsVisible = e.Value;
        if (_tumEtkinlikler == null) return;
        ApplyFilters();
    }
}