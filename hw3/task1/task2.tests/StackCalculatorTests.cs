namespace Hw2Task2.Tests;

public class StackCalculatorTests
{
    [Test]
    public void Evaluate_ListStack_Empty_ThrowsIncorrectStackException()
    {
        ListStack<StackElement> stack = new();
        Assert.Throws<IncorrectStackException>(() => StackCalculator.Evaluate((stack)));
    }

    [Test]
    public void Evaluate_ListStack_SumOfOneAndTwo_ResultIsThree()
    {
        ListStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(1));
        stack.Push(new StackElement.Number(2));
        stack.Push(new StackElement.Binop(BinopType.Add));
        Assert.That(StackCalculator.Evaluate(stack), Is.EqualTo((Fraction)3));
    }

    [Test]
    public void Evaluate_ListStack_FiveMinusFive_ResultIsZero()
    {
        ListStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(5));
        stack.Push(new StackElement.Number(5));
        stack.Push(new StackElement.Binop(BinopType.Sub));
        Assert.That(StackCalculator.Evaluate(stack), Is.EqualTo((Fraction)0));
    }

    [Test]
    public void Evaluate_ArrayStack_SixMultipliedByTen_ResultIsSixty()
    {
        ArrayStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(6));
        stack.Push(new StackElement.Number(10));
        stack.Push(new StackElement.Binop(BinopType.Mul));
        Assert.That(StackCalculator.Evaluate(stack), Is.EqualTo((Fraction)60));
    }

    [Test]
    public void Evaluate_ArrayStack_FiveDivideByThree_ResultIsFiveThrees()
    {
        Fraction expectedResult = (Fraction)5 / 3;
        ArrayStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(5));
        stack.Push(new StackElement.Number(3));
        stack.Push(new StackElement.Binop(BinopType.Div));
        Assert.That(StackCalculator.Evaluate(stack), Is.EqualTo(expectedResult));
    }

    [Test]
    public void Evaluate_ArrayStack_NotEnoughElement_ThrowsIncorrectStackException()
    {
        ArrayStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(3));
        stack.Push(new StackElement.Binop(BinopType.Div));
        Assert.Throws<IncorrectStackException>(() => StackCalculator.Evaluate(stack));
    }

    [Test]
    public void Evaluate_ArrayStack_OneDivideByZero_ThrowsException()
    {
        ArrayStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(3));
        stack.Push(new StackElement.Number(0));
        stack.Push(new StackElement.Binop(BinopType.Div));
        Assert.Throws<DivideByZeroException>(() => StackCalculator.Evaluate((stack)));
    }

    [Test]
    public void Evaluate_IncorrectStack_ThrowsIncorrectStackException()
    {
        ArrayStack<StackElement> stack = new();
        stack.Push(new StackElement.Binop(BinopType.Div));
        stack.Push(new StackElement.Binop(BinopType.Mul));
        stack.Push(new StackElement.Binop(BinopType.Add));
        stack.Push(new StackElement.Number(10));
        Assert.Throws<IncorrectStackException>(() => StackCalculator.Evaluate((stack)));
    }

    [Test]
    public void Evaluate_ArrayStack_TooManylement_ThrowsIncorrectStackException()
    {
        ArrayStack<StackElement> stack = new();
        stack.Push(new StackElement.Number(3));
        stack.Push(new StackElement.Binop(BinopType.Div));
        stack.Push(new StackElement.Number(3));
        stack.Push(new StackElement.Number(3));
        Assert.Throws<IncorrectStackException>(() => StackCalculator.Evaluate(stack));
    }
}