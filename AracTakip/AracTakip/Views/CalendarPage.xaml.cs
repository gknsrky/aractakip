// using ifadeleri dosyan�n en ba��nda yer almal�
using System.Collections.ObjectModel;

// namespace tan�m�, s�n�f� sarmalamal�
namespace AracTakip.Views;

// S�n�f tan�m� 'public partial class' �eklinde olmal�
public partial class CalendarPage : ContentPage
{
    public CalendarPage()
    {
        InitializeComponent();
        // LoadCalendarEvents(); // �imdilik bu metodu �a��rmayal�m
    }

    private void LoadCalendarEvents()
    {
        // �nceki denemelerimizden kalan kod.
        // �imdilik buras� bo� kals�n veya yorumlu olsun, ��nk� derleniyordu.
        /*
        var aracListesi = new List<AracTakip.Models.Arac> { ... };
        ...
        */
    }
}