namespace AracTakip.Behaviors;

// Bu Behavior, herhangi bir metin giriş kontrolüne (Entry, Editor) eklenebilir.
public class ToUpperTextBehavior : Behavior<InputView>
{
    // Behavior kontrole eklendiğinde bu metot çalışır.
    protected override void OnAttachedTo(InputView view)
    {
        base.OnAttachedTo(view);
        // Kontrolün TextChanged olayına abone oluyoruz.
        view.TextChanged += OnTextChanged;
    }

    // Behavior kontrolden çıkarıldığında bu metot çalışır.
    protected override void OnDetachingFrom(InputView view)
    {
        base.OnDetachingFrom(view);
        // Bellek sızıntısını önlemek için aboneliği sonlandırıyoruz.
        view.TextChanged -= OnTextChanged;
    }

    // Metin her değiştiğinde bu metot tetiklenir.
    private void OnTextChanged(object sender, TextChangedEventArgs e)
    {
        if (sender is InputView inputView)
        {
            // Gelen yeni metni alıp büyük harfe çeviriyoruz
            // ve kontrolün metnini bu yeni değerle güncelliyoruz.
            inputView.Text = e.NewTextValue?.ToUpper();
        }
    }
}