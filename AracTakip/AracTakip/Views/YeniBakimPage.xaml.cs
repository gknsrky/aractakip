namespace AracTakip.Views;

public partial class YeniBakimPage : ContentPage
{
    private readonly string _aracId;

    // Bu oluþturucu, bir önceki sayfadan gönderilen aracýn kimliðini (Id) alýr.
    public YeniBakimPage(string aracId)
    {
        InitializeComponent();
        _aracId = aracId;

        // Sayfanýn baþlýðýna plaka vb. eklemek için bu Id'yi kullanabiliriz (ileride).
    }
}