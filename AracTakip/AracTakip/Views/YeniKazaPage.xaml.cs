namespace AracTakip.Views;

public partial class YeniKazaPage : ContentPage
{
    private readonly string _aracId;

    public YeniKazaPage(string aracId)
    {
        InitializeComponent();
        _aracId = aracId;
    }
}