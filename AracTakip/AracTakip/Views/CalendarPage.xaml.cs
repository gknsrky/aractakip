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
            if (arac.VizeBitisTarihi.HasValue) _tumEtkinlikler.Add(new Etkinlik { Tarih = arac.VizeBitisTarihi.Value, Plaka = arac.Plaka, Aciklama = "Vize Biti�" });
            if (arac.SigortaBitisTarihi.HasValue) _tumEtkinlikler.Add(new Etkinlik { Tarih = arac.SigortaBitisTarihi.Value, Plaka = arac.Plaka, Aciklama = "Sigorta Biti�" });
            if (arac.KaskoBitisTarihi.HasValue) _tumEtkinlikler.Add(new Etkinlik { Tarih = arac.KaskoBitisTarihi.Value, Plaka = arac.Plaka, Aciklama = "Kasko Biti�" });
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
            // �zel aral�k modu
            startDate = StartDatePicker.Date;
            endDate = EndDatePicker.Date;
            DateRangeLabel.Text = $"{startDate:dd.MM.yyyy} - {endDate:dd.MM.yyyy} Aral���";
        }
        else
        {
            // Varsay�lan mod: Gelecek 7 g�n
            startDate = DateTime.Today;
            endDate = startDate.AddDays(7);
            DateRangeLabel.Text = "Gelecek 7 G�n�n Etkinlikleri";
        }

        var sonuclar = _tumEtkinlikler.Where(e =>
            e.Tarih.Date >= startDate && e.Tarih.Date <= endDate &&
            ((VizeFilter.IsChecked && e.Aciklama == "Vize Biti�") ||
             (SigortaFilter.IsChecked && e.Aciklama == "Sigorta Biti�") ||
             (KaskoFilter.IsChecked && e.Aciklama == "Kasko Biti�"))
        ).OrderBy(e => e.Tarih).ToList();

        GosterilecekEtkinlikler.Clear();
        foreach (var etkinlik in sonuclar)
        {
            GosterilecekEtkinlikler.Add(etkinlik);
        }

        ResultLabel.IsVisible = !sonuclar.Any();
        EventsListView.IsVisible = sonuclar.Any();
    }

    // Checkbox'lar de�i�ti�inde filtreyi uygula
    private void Filter_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        if (_tumEtkinlikler == null) return;
        ApplyFilters();
    }

    // �zel tarih se�ildi�inde filtreyi uygula
    private void Filter_DateSelected(object sender, DateChangedEventArgs e)
    {
        if (_tumEtkinlikler == null) return;
        ApplyFilters();
    }

    // Anahtar (Switch) durumu de�i�ti�inde hem tarih se�icileri g�ster/gizle hem de filtreyi uygula
    private void DateRangeSwitch_Toggled(object sender, ToggledEventArgs e)
    {
        CustomDateRangeGrid.IsVisible = e.Value;
        if (_tumEtkinlikler == null) return;
        ApplyFilters();
    }
}