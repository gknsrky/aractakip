using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Licensing; // BU YENİ using İFADESİ GEREKLİ

namespace AracTakip;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // DOĞRU YÖNTEM: Lisans anahtarını en başta, burada kaydediyoruz.
        SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCeUx3TXxbf1x1ZFRGal5VTnZWUiweQnxTdEBjXn5fcXRXQmBVWUxyX0leYw==");

        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            // Bu metodun içi boş kalacak şekilde, orijinal haliyle kullanıyoruz.
            .ConfigureSyncfusionCore();

        return builder.Build();
    }
}