using System.Diagnostics.CodeAnalysis;
using NUnit.Framework;
using PutridParrot.Fractions;
using System.Globalization;

namespace Tests.PutridParrot.Fractions;

[ExcludeFromCodeCoverage]
public class FractionTests
{

    [TestCase(10, -8, -5, 4)]
    public void Fraction_FromNumeratorDenominator(int numerator, int denominator, int expectedNumerator, int expectedDenominator)
    {
        var fraction = new Fraction(numerator, denominator);
        Assert.That(fraction.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(1, 7, 5, 1, 35)]
    public void Fraction_FromFractionAndDenominator(int fractionNumerator, int fractionDenominator, 
        int denominator, int expectedNumerator, int expectedDenominator)
    {
        var fraction = new Fraction(new Fraction(fractionNumerator, fractionDenominator), denominator);
        Assert.That(fraction.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(1, 7, 2, 3, 3, 14)]
    public void Fraction_FromFractions(int fractionNumerator1, int fractionDenominator1,
        int fractionNumerator2, int fractionDenominator2,
        int expectedNumerator, int expectedDenominator)
    {
        var fraction = new Fraction(
            new Fraction(fractionNumerator1, fractionDenominator1), 
            new Fraction(fractionNumerator2, fractionDenominator2));
        Assert.That(fraction.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase("314", 314, 1)]
    [TestCase("-35/4", -35, 4)]
    [TestCase("3.1415", 6283, 2000)]
    [TestCase("-47e-2", -47, 100)]
    [TestCase("1.47", 147, 100)]
    [TestCase("1", 1, 1)]
    [TestCase("1.0", 1, 1)]
    [TestCase("0.5", 1, 2)]

    public void Fraction_FromString(string s, int numerator, int denominator)
    {
        var fraction = new Fraction(s);
        Assert.That(fraction.Numerator, Is.EqualTo(numerator));
        Assert.That(fraction.Denominator, Is.EqualTo(denominator));
    }

    [TestCase(1.47, 147, 100)]
    public void Fraction_FromDouble(double d, int numerator, int denominator)
    {
        var fraction = new Fraction(d);
        Assert.That(fraction.Numerator, Is.EqualTo(numerator));
        Assert.That(fraction.Denominator, Is.EqualTo(denominator));
    }

    [TestCase(5, 5, 1)]
    public void Fraction_FromInt(int d, int numerator, int denominator)
    {
        var fraction = new Fraction(d);
        Assert.That(fraction.Numerator, Is.EqualTo(numerator));
        Assert.That(fraction.Denominator, Is.EqualTo(denominator));
    }


    [TestCase(5, 4, 1, 2, 14, 8)]
    public void Addition_Tests(int numerator1, int denominator1, 
        int numerator2, int denominator2, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        var fraction3 = fraction1 + fraction2;

        Assert.That(fraction3.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction3.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 0.5, 14, 8)]
    public void Addition_WithDouble_Tests(int numerator1, int denominator1,
        double value, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = fraction1 + value;

        Assert.That(fraction2.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction2.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 2, 13, 4)]
    public void Addition_WithInt_Tests(int numerator1, int denominator1,
        int value, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = fraction1 + value;

        Assert.That(fraction2.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction2.Denominator, Is.EqualTo(expectedDenominator));
    }


    [TestCase(5, 4, -5, 4)]
    public void Negate_Test1(int numerator, int denominator, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator, denominator);
        var fraction2 = -fraction1;

        Assert.That(fraction2.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction2.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 1, 2, 6, 8)]
    public void Subtract_Tests(int numerator1, int denominator1,
        int numerator2, int denominator2, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        var result = fraction1 - fraction2;

        Assert.That(result.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(result.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 0.5, 6, 8)]
    public void Subtract_WithDouble_Tests(int numerator1, int denominator1,
        double value, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var result = fraction1 - value;

        Assert.That(result.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(result.Denominator, Is.EqualTo(expectedDenominator));
    }


    [TestCase(5, 4, 1, 2, 5, 8)]
    public void Multiply_Tests(int numerator1, int denominator1,
        int numerator2, int denominator2, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        var result = fraction1 * fraction2;

        Assert.That(result.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(result.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 0.5, 5, 8)]
    public void Multiply_WithDouble_Tests(int numerator1, int denominator1,
        double value, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var result = fraction1 * value;

        Assert.That(result.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(result.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 1, 2, 10, 4)]
    public void Divide_Test1(int numerator1, int denominator1,
        int numerator2, int denominator2, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        var fraction3 = fraction1 / fraction2;

        Assert.That(fraction3.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(fraction3.Denominator, Is.EqualTo(expectedDenominator));
    }

    [TestCase(5, 4, 0.5, 10, 4)]
    public void Divide_WithDouble_Test(int numerator1, int denominator1,
        double value, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var result = fraction1 / value;

        Assert.That(result.Numerator, Is.EqualTo(expectedNumerator));
        Assert.That(result.Denominator, Is.EqualTo(expectedDenominator));
    }

    [Test]
    public void Constructor_ByZero_ExpectException()
    {
        Assert.Throws<DivideByZeroException>(() => new Fraction(1, 0));
    }

    [TestCase(5, 4, 1, 2, 5, 8)]
    public void Equivalent_Tests(int numerator1, int denominator1,
        int numerator2, int denominator2, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        var fraction3 = fraction1 * fraction2;

        Assert.That(fraction3, Is.EqualTo(new Fraction(expectedNumerator, expectedDenominator)));
    }

    [TestCase(5, 4, 1, 2, 5, 12)]
    public void NotEquivalent_Tests(int numerator1, int denominator1,
        int numerator2, int denominator2, int expectedNumerator, int expectedDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        var fraction3 = fraction1 * fraction2;

        Assert.That(fraction3, Is.Not.EqualTo(new Fraction(expectedNumerator, expectedDenominator)));
    }

    [TestCase(1, 2, 1, 2)]
    [TestCase(6, 24, 1, 4)]
    [TestCase(10, 35, 2, 7)]
    [TestCase(8, 12, 2, 3)]
    [TestCase(8, 24, 1, 3)]
    [TestCase(3, 12, 1, 4)]
    [TestCase(14, 49, 2, 7)]
    [TestCase(52, 130, 2, 5)]
    public void Simplify_TestVariousFractions(int numerator, int denominator, int reducedNumerator,
        int reducedDenominator)
    {
        var fraction1 = new Fraction(numerator, denominator);
        var fraction2 = new Fraction(reducedNumerator, reducedDenominator);

        Assert.That(fraction1.Numerator, Is.EqualTo(fraction2.Numerator));
        Assert.That(fraction1.Denominator, Is.EqualTo(fraction2.Denominator));
    }

    [TestCase(3, -6, -1, 2)]
    public void Positive_TestVariousFractions(int numerator, int denominator, int reducedNumerator,
        int reducedDenominator)
    {
        var fraction1 = new Fraction(numerator, denominator);
        //var fraction2 = +fraction1;

        Assert.That(fraction1.Numerator, Is.EqualTo(reducedNumerator));
        Assert.That(fraction1.Denominator, Is.EqualTo(reducedDenominator));
    }

    [TestCase(1, 2, 1, 6, 6, 2)]
    [TestCase(1, 8, 1, 4, 4, 8)]
    public void Divide_TestVariousFractions(int numerator1, int denominator1,
        int numerator2, int denominator2, int resultNumerator, int resultDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);

        var result = fraction1 / fraction2;

        Assert.That(result.Numerator, Is.EqualTo(resultNumerator));
        Assert.That(result.Denominator, Is.EqualTo(resultDenominator));
    }

    [TestCase(1, 2, 2, 5, 2, 10)]
    [TestCase(1, 3, 9, 16, 9, 48)]
    [TestCase(2, 3, 5, 1, 10, 3)]
    public void Multiply_TestVariousFractions(int numerator1, int denominator1,
        int numerator2, int denominator2, int resultNumerator, int resultDenominator)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);

        var result = fraction1 * fraction2;

        Assert.That(result.Numerator, Is.EqualTo(resultNumerator));
        Assert.That(result.Denominator, Is.EqualTo(resultDenominator));
    }

    [TestCase(100, 863, 0.11587485515643106)]
    [TestCase(1, 2, 0.5)]
    public void CastToDouble(int numerator, int denominator, double expected)
    {
        var fraction = new Fraction(numerator, denominator);
        Assert.That((double)fraction, Is.EqualTo(expected));
    }

    [TestCase(1, 2, 1, 2)]
    [TestCase(1, 2, 24, 48)]
    [TestCase(1, 5, 5, 25)]
    public void EqualityTests_AllTrue(int numerator1, int denominator1, int numerator2, int denominator2)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        Assert.That(fraction1 == fraction2);
    }

    [TestCase(1, 3, 24, 48)]
    [TestCase(1, 6, 5, 25)]
    public void EqualityTests_AllFalse(int numerator1, int denominator1, int numerator2, int denominator2)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        Assert.That(fraction1 != fraction2);
    }

    [TestCase(1, 2, 24, 48)]
    [TestCase(1, 5, 5, 25)]
    public void LessThanOrEqualTests_AllTrue(int numerator1, int denominator1, int numerator2, int denominator2)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        Assert.That(fraction1 <= fraction2);
    }

    [TestCase(1, 200, 24, 48)]
    [TestCase(1, 500, 5, 25)]
    public void LessThanTests_AllTrue(int numerator1, int denominator1, int numerator2, int denominator2)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        Assert.That(fraction1 < fraction2);
    }


    [TestCase(1, 2, 24, 48)]
    [TestCase(1, 5, 5, 25)]
    public void GreaterThanOrEqualTests_AllTrue(int numerator1, int denominator1, int numerator2, int denominator2)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        Assert.That(fraction1 >= fraction2);
    }

    [TestCase(1, 2, 24, 148)]
    [TestCase(1, 5, 5, 125)]
    public void GreaterThanTests_AllTrue(int numerator1, int denominator1, int numerator2, int denominator2)
    {
        var fraction1 = new Fraction(numerator1, denominator1);
        var fraction2 = new Fraction(numerator2, denominator2);
        Assert.That(fraction1 > fraction2);
    }

    [TestCase(10, 12, "5/6")]
    [TestCase(1, 6, "1/6")]
    public void ToString_Tests(int numerator, int denominator, string s)
    {
        var fraction = new Fraction(numerator, denominator);
        Assert.That(fraction.ToString(), Is.EqualTo(s));
    }

    [TestCase(10, 12, "5", "N")]
    [TestCase(1, 6, "1", "N")]
    [TestCase(10, 12, "6", "D")]
    [TestCase(1, 8, "8", "D")]
    public void ToStringWithFormat_Tests(int numerator, int denominator, string s, string format)
    {
        var fraction = new Fraction(numerator, denominator);
        Assert.That(fraction.ToString(format, null), Is.EqualTo(s));
    }

    [Test, SetCulture("de-de")]
    public void Fraction_UsingCultureWithGermanDecimalPointCharacter()
    {
        // when converting from a string we use the decimal point separator
        var germanCultureValue = "1,47";
        var fraction = new Fraction(germanCultureValue);

        Assert.That(fraction.Numerator, Is.EqualTo(147));
        Assert.That(fraction.Denominator, Is.EqualTo(100));
    }

    [Test, SetCulture("en-GB")]
    public void Fraction_UsingCultureWithBritishDecimalPointCharacter()
    {
        // when converting from a string we use the decimal point separator
        var britishCultureValue = "1.47";
        var fraction = new Fraction(britishCultureValue);

        Assert.That(fraction.Numerator, Is.EqualTo(147));
        Assert.That(fraction.Denominator, Is.EqualTo(100));
    }

    [Test]
    public void ToFraction_UsingCultureWithGermanDecimalPointCharacter()
    {
        // when converting from a string we use the decimal point separator
        var germanCultureValue = "1,47";
        var fraction = Fraction.ToFraction(germanCultureValue, new CultureInfo("de-DE"));

        Assert.That(fraction.Numerator, Is.EqualTo(147));
        Assert.That(fraction.Denominator, Is.EqualTo(100));
    }

}