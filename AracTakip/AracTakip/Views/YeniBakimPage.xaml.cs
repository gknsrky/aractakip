namespace AracTakip.Views;

public partial class YeniBakimPage : ContentPage
{
    private readonly string _aracId;

    // Bu olu�turucu, bir �nceki sayfadan g�nderilen arac�n kimli�ini (Id) al�r.
    public YeniBakimPage(string aracId)
    {
        InitializeComponent();
        _aracId = aracId;

        // Sayfan�n ba�l���na plaka vb. eklemek i�in bu Id'yi kullanabiliriz (ileride).
    }
}