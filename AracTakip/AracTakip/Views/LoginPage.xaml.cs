using System;
using Microsoft.Maui.Storage;

namespace AracTakip.Views;

public partial class LoginPage : ContentPage
{
    public LoginPage()
    {
        InitializeComponent();
    }

    private async void OnLoginClicked(object sender, EventArgs e)
    {
        var email = EmailEntry.Text?.Trim();
        var password = PasswordEntry.Text;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
        {
            ErrorLabel.Text = "Lütfen e-posta ve þifre giriniz.";
            ErrorLabel.IsVisible = true;
            return;
        }

        // Firebase yerine sahte giriþ (Firebase hazýr olunca entegre edilir)
        bool isLoginSuccessful = true;

        if (isLoginSuccessful)
        {
            if (RememberMeCheckbox.IsChecked)
            {
                Preferences.Set("email", email);
                Preferences.Set("password", password);
                Preferences.Set("isRemembered", true);
            }

            await Navigation.PushAsync(new MainPage()); // Dummy olarak MainPage'e geç
        }
        else
        {
            ErrorLabel.Text = "Giriþ baþarýsýz.";
            ErrorLabel.IsVisible = true;
        }
    }
}
