using AracTakip.Services;

namespace AracTakip;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        // ... (Mevcut Syncfusion lisans kodun burada)

        var builder = MauiApp.CreateBuilder();

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
            ;

        // FirebaseService manuel kayıt
        builder.Services.AddSingleton<FirebaseService>(sp =>
            new FirebaseService(
                apiKey: "AIzaSyAiEEdxNaIf3z6yihTxJnVOyC6tVCJ-ZCc",
                storageBucket: "aractakip-d8b16.firebasestorage.app",
                firestoreProjectId: "aractakip-d8b16"
            )
        );

        // YENİ EKLENEN BLOK: Entry alt çizgisini kaldırmak için
#if WINDOWS
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
            handler.PlatformView.Padding = new Microsoft.UI.Xaml.Thickness(0);
        });
#endif

        return builder.Build();
    }
}