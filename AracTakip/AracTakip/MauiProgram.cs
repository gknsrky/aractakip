using Microsoft.Extensions.Logging;
using UraniumUI;
using UraniumUI.Material.Extensions; // Bu using ifadesi önemli!

namespace AracTakip;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseUraniumUIMaterial() // Bu metot hem base UI'ı hem de Material'ı başlatır.
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");

                // Fontları eklemenin doğru ve güncel yolu.
                fonts.AddMaterialIconFonts();
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}