namespace Hw2Task2;

/// <summary>
/// Class for Rational numers with infinite precision.
/// </summary>
public readonly struct Fraction
{
    private readonly int num;
    private readonly int den;

    private Fraction(int numerator, int denominator)
    {
        if (denominator == 0)
        {
            throw new ArgumentException("Denominator cannot be zero.", nameof(denominator));
        }

        this.num = numerator;
        this.den = denominator;
    }

    /// <summary>
    /// Implicit converter from int to <see cref="Fraction"/> class.
    /// </summary>
    /// <param name="number"> Number to convert. </param>
    public static implicit operator Fraction(int number)
        => new Fraction(number, 1);

    /// <summary>
    /// Opposite fraction.
    /// </summary>
    /// <param name="a"> Number to use operator on. </param>
    /// <returns> Number opposite to a. </returns>
    public static Fraction operator -(Fraction a)
        => new Fraction(-a.num, a.den);

    /// <summary>
    /// Sum of fractions.
    /// </summary>
    /// <param name="a"> First number. </param>
    /// <param name="b"> Second number. </param>
    /// <returns> Sum of a and b. </returns>
    public static Fraction operator +(Fraction a, Fraction b)
        => new Fraction((a.num * b.den) + (b.num * a.den), a.den * b.den).Simplify();

    /// <summary>
    /// Substraction of fractions.
    /// </summary>
    /// <param name="a"> Decreasing number. </param>
    /// <param name="b"> Subtrahend number. </param>
    /// <returns> Result of substracting b from a. </returns>
    public static Fraction operator -(Fraction a, Fraction b)
        => a + (-b);

    /// <summary>
    /// Multiplication of fractions.
    /// </summary>
    /// <param name="a"> First multiplier. </param>
    /// <param name="b"> Second multiplier. </param>
    /// <returns> Result of multiplying a by b. </returns>
    public static Fraction operator *(Fraction a, Fraction b)
        => new Fraction(a.num * b.num, a.den * b.den).Simplify();

    /// <summary>
    /// Division of fractions.
    /// </summary>
    /// <param name="a"> numerator. </param>
    /// <param name="b"> denominator. </param>
    /// <exception cref="DivideByZeroException"> Thrown when b is zero. </exception>
    /// <returns> Result of dividing a by b. </returns>
    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.num == 0)
        {
            throw new DivideByZeroException();
        }

        return new Fraction(a.num * b.den, a.den * b.num).Simplify();
    }

    /// <summary>
    /// Get simplified form of fraction (where numerator and denominator have no common divisors).
    /// </summary>
    /// <returns> Simplified form of fraction. </returns>
    public Fraction Simplify()
    {
        int numerator = this.num;
        int denominator = this.den;

        int maxCommonDivisor = (int)float.Min(float.Sqrt(numerator), float.Sqrt(denominator)) + 1;

        for (int divisor = 2; divisor < maxCommonDivisor; divisor++)
        {
            if (numerator % divisor != 0 || denominator % divisor != 0)
            {
                continue;
            }

            do
            {
                numerator /= divisor;
                denominator /= divisor;
            }
            while (numerator % divisor == 0 && denominator % divisor == 0);
            maxCommonDivisor = (int)float.Min(float.Sqrt(numerator), float.Sqrt(denominator)) + 1;
        }

        return new Fraction(numerator, denominator);
    }

    /// <summary>
    /// Convert <see cref="Fraction"/> class to string.
    /// </summary>
    /// <returns> String in form of &lt;numerator&gt; / &lt;denominator&gt;. </returns>
    public override string ToString()
        => $"{this.num} / {this.den}";
}
