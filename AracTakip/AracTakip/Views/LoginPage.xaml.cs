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
            await DisplayAlert("Hata", "E-posta ve �ifre alanlar� bo� b�rak�lamaz.", "Tamam");
            return;
        }

        try
        {
            // Servisimizdeki LoginAsync metodunu �a��r�yoruz. Art�k bir token bekliyoruz.
            string userToken = await _firebaseService.LoginAsync(email, password);

            if (!string.IsNullOrEmpty(userToken))
            {
                // Giri� ba�ar�l�ysa ve token al�nd�ysa ana sayfaya y�nlendir.
                Application.Current.MainPage = new AppShell();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Giri� Ba�ar�s�z", ex.Message, "Tekrar Dene");
        }
    }
}