using AracTakip.Views;

namespace AracTakip;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState activationState)
    {
        // Penceremizi, başlangıç sayfası olarak LoginPage'i içeren bir NavigationPage
        // ile doğrudan kendimiz oluşturuyoruz.
        var window = new Window(new NavigationPage(new LoginPage()));

        window.Title = "Araç Takip";

        // Gelecekte pencere ile ilgili ilk ayarlar (boyut vb.) burada yapılabilir.

        return window;
    }
}