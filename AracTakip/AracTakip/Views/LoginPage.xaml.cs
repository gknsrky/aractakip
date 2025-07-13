using AracTakip.Services;

namespace AracTakip.Views;

public partial class LoginPage : ContentPage
{
    private readonly FirebaseService _firebaseService;

    public LoginPage()
    {
        InitializeComponent();
        _firebaseService = IPlatformApplication.Current.Services.GetService<FirebaseService>();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text?.Trim();

        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            await DisplayAlert("Hata", "E-posta ve þifre alanlarý boþ býrakýlamaz.", "Tamam");
            return;
        }

        try
        {
            // Servisimizdeki LoginAsync metodunu çaðýrýyoruz. Artýk bir token bekliyoruz.
            string userToken = await _firebaseService.LoginAsync(email, password);

            if (!string.IsNullOrEmpty(userToken))
            {
                // Giriþ baþarýlýysa ve token alýndýysa ana sayfaya yönlendir.
                Application.Current.MainPage = new AppShell();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Giriþ Baþarýsýz", ex.Message, "Tekrar Dene");
        }
    }
}