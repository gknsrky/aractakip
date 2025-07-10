namespace AracTakip.Views;

public partial class YeniArizaPage : ContentPage
{
    private readonly string _aracId;

    public YeniArizaPage(string aracId)
    {
        InitializeComponent();
        _aracId = aracId;
    }
}