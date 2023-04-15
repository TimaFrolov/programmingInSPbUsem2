namespace hw7.Calculator;

/// <summary>
/// Class representing calculator.
/// </summary>
public class Calculator
{
    private int previousNum = 0;
    private Binop binop = Binop.None;
    private Button lastClicked = Button.None;

    private enum Button
    {
        None,
        Num,
        Binop,
    }

    /// <summary>
    /// Gets current num on display.
    /// </summary>
    /// <value>Num on display.</value>
    public int Num { get; private set; }

    /// <summary>
    /// Gets current binary operation selected.
    /// </summary>
    /// <value>Selected binary operation.</value>
    public Binop Binop
    {
        get => this.binop;
        private set
        {
            this.previousNum = this.Num;
            this.binop = value;
        }
    }

    /// <summary>
    /// Call when number button clicked.
    /// </summary>
    /// <param name="num">Button that clicked. Should be between 0 and 9.</param>
    public void NumButtonClicked(int num)
    {
        if (num < 0 || num > 9)
        {
            return;
        }

        if (this.lastClicked == Button.Binop)
        {
            this.Num = num;
        }
        else
        {
            this.Num = (this.Num * 10) + num;
        }

        this.lastClicked = Button.Num;
    }

    /// <summary>
    /// Call when binary operation button clicked.
    /// </summary>
    /// <param name="button">Button that clicked.</param>
    public void BinopButtonClicked(Binop button)
    {
        if (this.lastClicked != Button.Binop)
        {
            this.EmitBinop();
        }

        this.Binop = button;
        this.lastClicked = Button.Binop;
    }

    /// <summary>
    /// Call when erase button clicked.
    /// </summary>
    public void EraseButtonClicked()
    {
        this.Num = 0;
        this.lastClicked = Button.None;
    }

    /// <summary>
    /// Call when equal button clicked.
    /// </summary>
    public void EqualButtonClicked()
    {
        this.EmitBinop();
        this.Binop = Binop.None;
        this.lastClicked = Button.None;
    }

    private void EmitBinop()
    {
        switch (this.Binop)
        {
            case Binop.Div:
                this.Num = this.previousNum / this.Num;
                break;
            case Binop.Mul:
                this.Num = this.previousNum * this.Num;
                break;
            case Binop.Add:
                this.Num = this.previousNum + this.Num;
                break;
            case Binop.Sub:
                this.Num = this.previousNum - this.Num;
                break;
        }
    }
}

/// <summary>
/// Binary operation type.
/// </summary>
public enum Binop
{
    /// <summary>None.</summary>
    None,

    /// <summary>"/"</summary>
    Div,

    /// <summary>"*"</summary>
    Mul,

    /// <summary>"+"</summary>
    Add,

    /// <summary>"-"</summary>
    Sub,
}