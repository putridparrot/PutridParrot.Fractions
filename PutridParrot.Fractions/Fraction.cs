using System.Globalization;

namespace PutridParrot.Fractions;

/// <summary>
/// Fraction class
/// </summary>
public class Fraction : 
    IComparable, IComparable<Fraction>, IEquatable<Fraction>,
    IFormattable
    //ISpanFormattable
{
    /// <summary>
    /// Creates a fraction with the supplied numerator and denominator
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="simplify"></param>
    /// <exception cref="DivideByZeroException">If the denominator is zero a divide by zero exception occurs</exception>
    public Fraction(int numerator, int denominator, bool simplify = true)
    {
        if (denominator == 0)
            throw new DivideByZeroException(nameof(denominator));

        Numerator = numerator;
        Denominator = denominator;

        IfSimplify(simplify);
    }

    /// <summary>
    /// Creates a fraction with a numerator over 1
    /// </summary>
    /// <param name="numerator"></param>
    public Fraction(int numerator)
    {
        Numerator = numerator;
        Denominator = 1;
    }

    /// <summary>
    /// Creates a fraction from a decimal
    /// </summary>
    /// <param name="value"></param>
    /// <param name="simplify"></param>
    /// <exception cref="DivideByZeroException">If the value produces a denominator that is zero a divide by zero exception occurs</exception>
    public Fraction(double value, bool simplify = true)
    {
        var fraction = ToFraction(value);
        Numerator = fraction.Numerator;
        Denominator = fraction.Denominator;

        if (Denominator == 0)
            throw new DivideByZeroException(nameof(Denominator));

        IfSimplify(simplify);
    }

    /// <summary>
    /// Creates a fraction from a string representation of a Fraction
    /// or decimal
    /// </summary>
    /// <param name="value"></param>
    /// <param name="simplify"></param>
    /// <exception cref="DivideByZeroException">If the value produces a denominator that is zero a divide by zero exception occurs</exception>
    public Fraction(string value, bool simplify = true)
    {
        var fraction = ToFraction(value);
        Numerator = fraction.Numerator;
        Denominator = fraction.Denominator;

        if (Denominator == 0)
            throw new DivideByZeroException(nameof(Denominator));

        IfSimplify(simplify);
    }

    /// <summary>
    /// Creates a fraction from a Fraction as a numerator and an
    /// integer denominator
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="simplify"></param>
    /// <exception cref="DivideByZeroException">If the value produces a denominator that is zero a divide by zero exception occurs</exception>
    public Fraction(Fraction numerator, int denominator, bool simplify = true) :
        this(numerator.Numerator, numerator.Denominator * denominator, simplify)
    {
    }

    /// <summary>
    /// Create a fraction from a fraction numerator and fraction denominator
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="simplify"></param>
    public Fraction(Fraction numerator, Fraction denominator, bool simplify = true) :
        this(numerator.Numerator * denominator.Denominator, numerator.Denominator * denominator.Numerator, simplify)
    {
    }

    /// <summary>
    /// Get the numerator
    /// </summary>
    public int Numerator { get; private set; }
    /// <summary>
    /// Get the denominator
    /// </summary>
    public int Denominator { get; private set; }

    /// <summary>
    /// Create a fraction from a string optionally using
    /// a specific culture (for the decimal point character)
    /// </summary>
    /// <param name="value"></param>
    /// <param name="cultureInfo"></param>
    /// <returns></returns>
    public static Fraction ToFraction(string value, CultureInfo? cultureInfo = null)
    {
        // TODO: Need some proper error handling
        var split = value.Split('/');
        if (split.Length == 2)
        {
            return new Fraction(Int32.Parse(split[0]), Int32.Parse(split[1]));
        }

        cultureInfo ??= CultureInfo.CurrentCulture;
        return ToFraction(Double.Parse(value, cultureInfo), cultureInfo);
    }

    /// <summary>
    /// Create a fraction from a double
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static Fraction ToFraction(double value) => 
        ToFraction(value, null);

    private static Fraction ToFraction(double value, CultureInfo? cultureInfo)
    {
        if (value % 1 == 0) // if value is a whole number
        {
            return new Fraction((int)value);
        }

        cultureInfo ??= CultureInfo.CurrentCulture;
        var decimalPoint = cultureInfo.NumberFormat.NumberDecimalSeparator;

        checked
        {
            var s = value.ToString(CultureInfo.CurrentCulture);
            var dp = s.IndexOf(decimalPoint, StringComparison.InvariantCulture) + 1;
            var multiple = (int)Math.Pow(10, s.Length - dp);
            return new Fraction((int)Math.Round(value * multiple), multiple);
        }
    }

    /// <summary>
    /// Make the fraction positive
    /// </summary>
    /// <param name="fraction"></param>
    /// <returns></returns>
    //public static Fraction operator +(Fraction fraction) => fraction;

    /// <summary>
    /// Negate the fraction
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    public static Fraction operator -(Fraction a) => new(-a.Numerator, a.Denominator, false);

    /// <summary>
    /// Add two fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator +(Fraction a, Fraction b)
        => new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator, false);

    /// <summary>
    /// Add a double to a fraction
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator +(Fraction a, double b) => 
        a + new Fraction(b);

    /// <summary>
    /// Subtract two fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator -(Fraction a, Fraction b)
        => a + -b;

    /// <summary>
    /// Subtract a double from the fraction
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator -(Fraction a, double b) =>
        a - new Fraction(b);

    /// <summary>
    /// Multiply two fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator *(Fraction a, Fraction b)
        => new(a.Numerator * b.Numerator, a.Denominator * b.Denominator, false);

    /// <summary>
    /// Multiply a fraction by a double
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator *(Fraction a, double b) =>
        a * new Fraction(b);

    /// <summary>
    /// Divide two fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator /(Fraction a, Fraction b) =>
        new (a.Numerator * b.Denominator, a.Denominator * b.Numerator, false);

    /// <summary>
    /// Divide a fraction by a double
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator /(Fraction a, double b) =>
        a / new Fraction(b);


    /// <summary>
    /// Implicit cast from a fraction to a double representing the decimal of the fraction.
    /// </summary>
    /// <param name="fraction"></param>
    public static implicit operator double(Fraction? fraction) => 
        fraction is not null ? (double)fraction.Numerator / fraction.Denominator : 0.0;

    /// <summary>
    /// Get a fraction as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{Numerator}/{Denominator}";

    /// <summary>
    /// Get a fraction using format and format provider
    /// </summary>
    /// <param name="format"></param>
    /// <param name="formatProvider"></param>
    /// <returns></returns>
    /// <exception cref="FormatException"></exception>
    public string ToString(string? format, IFormatProvider? formatProvider)
    {
        if (String.IsNullOrEmpty(format))
            format = "S";
        if (formatProvider == null)
            formatProvider = CultureInfo.CurrentCulture;

        switch (format.ToUpperInvariant())
        {
            case "S": // Standard
                return $"{Numerator.ToString(formatProvider)}/{Denominator.ToString(formatProvider)}";
            case "D": // Denominator
                return Denominator.ToString(formatProvider);
            case "N": // Numerator
                return Numerator.ToString(formatProvider);
            default:
                throw new FormatException($"Format {format} is not supported");
        }
    }

    //public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
    //{
    //    throw new NotImplementedException();
    //}

    /// <summary>
    /// Compare this fraction with the supplied object
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public int CompareTo(object? obj)
    {
        return CompareTo(obj as Fraction);
    }

    private static int CompareTo(Fraction? fraction1, Fraction? fraction2) => fraction1 switch
    {
        null when fraction2 is null => 0,
        _ => fraction1!.CompareTo(fraction2)
    };

    /// <summary>
    /// Compares this fraction the the supplied, other, fraction
    /// </summary>
    /// <param name="other"></param>
    /// <returns>&gt;1 if this fraction is larger than the other, &lt;1 if it's smaller or 0 if they're the same</returns>
    public int CompareTo(Fraction? other)
    {
        if (other is null)
            return 1;
        if(this == other)
            return 0;

        var a = Numerator;
        var b = Denominator;
        var c = other.Numerator;
        var d = other.Denominator;

        var y = a * d - b * c;
        return y > 0 ? 1 : -1;
    }

    private void IfSimplify(bool simplify)
    {
        if (simplify)
        {
            Simplify();
        }
    }

    /// <summary>
    /// Simplify the fraction, if possible, producing a fraction with the
    /// lowest common denominator
    /// </summary>
    /// <returns>This fraction mutated to it's simplified state</returns>
    public Fraction Simplify()
    {
        var g = GetCommonDivisor(Numerator, Denominator);
        if (Denominator < 0)
        {
            g = -g;
        }

        Numerator /= g;
        Denominator /= g;

        return this;
    }

    /// <summary>
    /// Gets the common divisor for a numerator and denominator
    /// 
    /// https://en.wikipedia.org/wiki/Greatest_common_divisor
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <returns></returns>
    public static int GetCommonDivisor(int numerator, int denominator)
    {
        return numerator == 0 ? Math.Abs(denominator) : GetCommonDivisor(denominator % numerator, numerator);
    }

    /// <summary>
    /// Compare fractions for equality
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator ==(Fraction fraction1, Fraction fraction2) =>
        Equals(fraction1, fraction2);

    /// <summary>
    /// Compare fractions for inequality
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator !=(Fraction fraction1, Fraction fraction2) => 
        !Equals(fraction1, fraction2);

    /// <summary>
    /// Compares if fraction1 is less than fraction2
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator <(Fraction fraction1, Fraction fraction2) =>
        CompareTo(fraction1, fraction2) < 0;

    /// <summary>
    /// Compares if fraction1 is greater than fraction2
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator >(Fraction fraction1, Fraction fraction2) =>
        CompareTo(fraction1, fraction2) > 0;

    /// <summary>
    /// Compares if fraction1 is less than or equal to fraction2
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator <=(Fraction fraction1, Fraction fraction2) =>
        CompareTo(fraction1, fraction2) <= 0;

    /// <summary>
    /// Compares if fraction1 is greater than or equal to fraction2
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator >=(Fraction fraction1, Fraction fraction2) =>
        CompareTo(fraction1, fraction2) >= 0;

    /// <summary>
    /// Compare fractions for equality
    /// </summary>
    /// <param name="other"></param>
    /// <returns></returns>
    public bool Equals(Fraction? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Numerator == other.Numerator && Denominator == other.Denominator;
    }

    /// <summary>
    /// Compare fractions for equality
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
        {
            return false;
        }

        if (ReferenceEquals(this, obj))
        {
            return true;
        }

        return obj.GetType() == GetType() && Equals((Fraction)obj);
    }

    public override int GetHashCode() => (Numerator, Denominator).GetHashCode();
}