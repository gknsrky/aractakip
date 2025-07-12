using Syncfusion.Maui.Core.Hosting;
using Syncfusion.Licensing;
using AracTakip.Services;

namespace AracTakip;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // Syncfusion Lisans Anahtarı
        SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1JEaF5cXmRCeUx3TXxbf1x1ZFRGal5VTnZWUiweQnxTdEBjXn5fcXRXQmBVWUxyX0leYw==");

        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            .ConfigureSyncfusionCore();

        // FirebaseService manuel kayıt
        builder.Services.AddSingleton<FirebaseService>(sp =>
            new FirebaseService(
                apiKey: "AIzaSyAiEEdxNaIf3z6yihTxJnVOyC6tVCJ-ZCc",
                storageBucket: "aractakip-d8b16.firebasestorage.app",
                firestoreProjectId: "aractakip-d8b16"
            )
        );

        return builder.Build();
    }
}