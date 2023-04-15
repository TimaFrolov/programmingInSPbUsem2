namespace hw7;

using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

/// <summary>
/// App class.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Initialize app.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Run when framework initialization completed.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        if (this.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new Views.MainWindow
            {
                DataContext = new ViewModels.MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}