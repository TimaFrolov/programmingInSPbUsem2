namespace hw7.Calculator;

using System;

/// <summary>
/// Class representing calculator.
/// </summary>
public class Calculator
{
    private int previousNum = 0;
    private Binop binop = Binop.None;
    private bool lastClickedWasBinop = false;

    /// <summary>
    /// Gets a value indicating whether it was division by zero error after last operation.
    /// </summary>
    /// <value>true if it was, false otherwise.</value>
    public bool WasDivisonByZeroError { get; private set; } = false;

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

        if (this.lastClickedWasBinop || this.WasDivisonByZeroError)
        {
            this.WasDivisonByZeroError = false;
            this.Num = num;
        }
        else if (this.Num < int.MaxValue / 10)
        {
            this.Num = (this.Num * 10) + num;
        }

        this.lastClickedWasBinop = false;
    }

    /// <summary>
    /// Call when binary operation button clicked.
    /// </summary>
    /// <param name="button">Button that clicked.</param>
    public void BinopButtonClicked(Binop button)
    {
        if (this.WasDivisonByZeroError)
        {
            return;
        }

        if (!this.lastClickedWasBinop)
        {
            this.EmitBinop();
        }

        this.Binop = button;
        this.lastClickedWasBinop = true;
    }

    /// <summary>
    /// Call when erase button clicked.
    /// </summary>
    public void EraseButtonClicked()
    {
        this.WasDivisonByZeroError = false;
        this.Num = 0;
        this.lastClickedWasBinop = false;
    }

    /// <summary>
    /// Call when equal button clicked.
    /// </summary>
    public void EqualButtonClicked()
    {
        if (this.WasDivisonByZeroError)
        {
            return;
        }

        this.EmitBinop();
        this.Binop = Binop.None;
        this.lastClickedWasBinop = false;
    }

    private void EmitBinop()
    {
        switch (this.Binop)
        {
            case Binop.Div:
                if (this.Num != 0)
                {
                    this.Num = this.previousNum / this.Num;
                }
                else
                {
                    this.Num = 0;
                    this.WasDivisonByZeroError = true;
                }

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
