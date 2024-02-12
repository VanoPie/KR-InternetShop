using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace InternetShop;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
            desktop.MainWindow.Icon =
                new WindowIcon(@"C:\Users\VanoP\Desktop\Новая папка\Practica\InternetShop\InternetShop\obj\icon.ico");
        }
        base.OnFrameworkInitializationCompleted();
    }
}