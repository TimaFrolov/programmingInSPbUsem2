namespace hw7.ViewModels;

using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

using Calculator;

/// <summary>
/// View model for main window.
/// </summary>
public class MainWindowViewModel : ReactiveObject
{
    private const string LitButtonColor = "#C0C0C0";
    private const string DimButtonColor = "#80C080";

    private Calculator calculator = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="MainWindowViewModel"/> class.
    /// </summary>
    public MainWindowViewModel()
    {
        this.OnButton0Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(0)));
        this.OnButton1Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(1)));
        this.OnButton2Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(2)));
        this.OnButton3Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(3)));
        this.OnButton4Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(4)));
        this.OnButton5Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(5)));
        this.OnButton6Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(6)));
        this.OnButton7Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(7)));
        this.OnButton8Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(8)));
        this.OnButton9Command = ReactiveCommand.Create(() => this.OnButton(new Button.Num(9)));
        this.OnButtonDivCommand = ReactiveCommand.Create(() => this.OnButton(new Button.Binary(Binop.Div)));
        this.OnButtonMulCommand = ReactiveCommand.Create(() => this.OnButton(new Button.Binary(Binop.Mul)));
        this.OnButtonAddCommand = ReactiveCommand.Create(() => this.OnButton(new Button.Binary(Binop.Add)));
        this.OnButtonSubCommand = ReactiveCommand.Create(() => this.OnButton(new Button.Binary(Binop.Sub)));
        this.OnButtonEraseCommand = ReactiveCommand.Create(() => this.OnButton(new Button.Erase()));
        this.OnButtonEqualCommand = ReactiveCommand.Create(() => this.OnButton(new Button.Equal()));

        this.WhenAnyValue(o => o.CurrentButton).Subscribe(o => this.RaisePropertyChanged(nameof(this.DivButtonColor)));
        this.WhenAnyValue(o => o.CurrentButton).Subscribe(o => this.RaisePropertyChanged(nameof(this.MulButtonColor)));
        this.WhenAnyValue(o => o.CurrentButton).Subscribe(o => this.RaisePropertyChanged(nameof(this.AddButtonColor)));
        this.WhenAnyValue(o => o.CurrentButton).Subscribe(o => this.RaisePropertyChanged(nameof(this.SubButtonColor)));
    }

    /// <summary>
    /// Gets text that should be on display.
    /// </summary>
    public string Display => this.calculator.WasDivisonByZeroError ? "Division by zero error!" : this.calculator.Num.ToString();

    /// <summary>
    /// Gets color of div button written in HEX.
    /// </summary>
    public string DivButtonColor => this.CurrentButton == Binop.Div ? LitButtonColor : DimButtonColor;

    /// <summary>
    /// Gets color of mul button written in HEX.
    /// </summary>
    public string MulButtonColor => this.CurrentButton == Binop.Mul ? LitButtonColor : DimButtonColor;

    /// <summary>
    /// Gets color of add button written in HEX.
    /// </summary>
    public string AddButtonColor => this.CurrentButton == Binop.Add ? LitButtonColor : DimButtonColor;

    /// <summary>
    /// Gets color of sub button written in HEX.
    /// </summary>
    public string SubButtonColor => this.CurrentButton == Binop.Sub ? LitButtonColor : DimButtonColor;

    /// <summary>
    /// Gets command should be done on button 0 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton0Command { get; }

    /// <summary>
    /// Gets command should be done on button 1 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton1Command { get; }

    /// <summary>
    /// Gets command should be done on button 2 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton2Command { get; }

    /// <summary>
    /// Gets command should be done on button 3 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton3Command { get; }

    /// <summary>
    /// Gets command should be done on button 4 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton4Command { get; }

    /// <summary>
    /// Gets command should be done on button 5 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton5Command { get; }

    /// <summary>
    /// Gets command should be done on button 6 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton6Command { get; }

    /// <summary>
    /// Gets command should be done on button 7 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton7Command { get; }

    /// <summary>
    /// Gets command should be done on button 8 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton8Command { get; }

    /// <summary>
    /// Gets command should be done on button 9 click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButton9Command { get; }

    /// <summary>
    /// Gets command should be done on button / click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButtonDivCommand { get; }

    /// <summary>
    /// Gets command should be done on button * click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButtonMulCommand { get; }

    /// <summary>
    /// Gets command should be done on button + click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButtonAddCommand { get; }

    /// <summary>
    /// Gets command should be done on button - click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButtonSubCommand { get; }

    /// <summary>
    /// Gets command should be done on button C click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButtonEraseCommand { get; }

    /// <summary>
    /// Gets command should be done on button = click.
    /// </summary>
    /// <value>Command to execute.</value>
    public ICommand OnButtonEqualCommand { get; }

    private Binop CurrentButton => this.calculator.Binop;

    private void OnButton(Button button)
    {
        switch (button)
        {
            case Button.Num num:
                this.calculator.NumButtonClicked(num.value);
                break;
            case Button.Binary bin:
                this.calculator.BinopButtonClicked(bin.type);
                break;
            case Button.Erase _:
                this.calculator.EraseButtonClicked();
                break;
            case Button.Equal _:
                this.calculator.EqualButtonClicked();
                break;
        }

        this.RaisePropertyChanged(nameof(this.Display));
        this.RaisePropertyChanged(nameof(this.CurrentButton));
    }

    private record Button
    {
        public record Num(int value)
        : Button();

        public record Binary(Binop type)
        : Button();

        public record Erase()
        : Button();

        public record Equal()
        : Button();

        private Button()
        {
        }
    }
}
