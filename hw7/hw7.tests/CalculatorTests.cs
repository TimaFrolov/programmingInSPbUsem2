namespace hw7.tests;

using Calculator;

public class CalculatorTests
{
    private Calculator calculator;

    [SetUp]
    public void Setup()
    {
        this.calculator = new Calculator();
    }

    [Test]
    public void OnePlusOneIsTwo()
    {
        calculator.NumButtonClicked(1);
        calculator.BinopButtonClicked(Binop.Add);
        calculator.NumButtonClicked(1);
        calculator.EqualButtonClicked();
        Assert.That(calculator.Num, Is.EqualTo(2));
    }

    [Test]
    public void NumOneClickBinopAddClickEqualClickResultsInTwo()
    {
        calculator.NumButtonClicked(1);
        calculator.BinopButtonClicked(Binop.Add);
        calculator.EqualButtonClicked();
        Assert.That(calculator.Num, Is.EqualTo(2));
    }

    [Test]
    public void OnePlusTwoMultipliedBySevenIsTwentyOne()
    {
        calculator.NumButtonClicked(1);
        calculator.BinopButtonClicked(Binop.Add);
        calculator.NumButtonClicked(2);
        calculator.BinopButtonClicked(Binop.Mul);
        calculator.NumButtonClicked(7);
        calculator.EqualButtonClicked();
        Assert.That(calculator.Num, Is.EqualTo(21));
    }

    [Test]
    public void DivisionByZeroGivesError()
    {
        calculator.NumButtonClicked(1);
        calculator.BinopButtonClicked(Binop.Div);
        calculator.NumButtonClicked(0);
        calculator.EqualButtonClicked();
        Assert.That(calculator.WasDivisonByZeroError, Is.EqualTo(true));
    }

    [Test]
    public void SevenDivideByTwoIsThree()
    {
        calculator.NumButtonClicked(7);
        calculator.BinopButtonClicked(Binop.Div);
        calculator.NumButtonClicked(2);
        calculator.EqualButtonClicked();
        Assert.That(calculator.Num, Is.EqualTo(3));
    }

    [Test]
    public void TooBigInt()
    {
        for (int i = 0; i < 100; i++)
        {
            calculator.NumButtonClicked(1);
        }
        Assert.That(calculator.Num, Is.EqualTo(1111111111));
    }
}
