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
    /// Creates a Fraction with the supplied numerator and denominator
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="simplify"></param>
    public Fraction(int numerator, int denominator, bool simplify = true)
    {
        if (denominator == 0)
            throw new DivideByZeroException(nameof(denominator));

        Numerator = numerator;
        Denominator = denominator;

        IfSimplify(simplify);
    }

    /// <summary>
    /// Creates a Fraction with a numerator over 1
    /// </summary>
    /// <param name="numerator"></param>
    public Fraction(int numerator)
    {
        Numerator = numerator;
        Denominator = 1;
    }

    /// <summary>
    /// Creates a Fraction from a decimal
    /// </summary>
    /// <param name="value"></param>
    /// <param name="simplify"></param>
    /// <exception cref="DivideByZeroException"></exception>
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
    /// Creates a Fraction from a string representation of a Fraction
    /// or decimal
    /// </summary>
    /// <param name="value"></param>
    /// <param name="simplify"></param>
    /// <exception cref="DivideByZeroException"></exception>
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
    /// Creates a Fraction from a Fraction as a numerator and an
    /// integer denominator
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="simplify"></param>
    public Fraction(Fraction numerator, int denominator, bool simplify = true) :
        this(numerator.Numerator, numerator.Denominator * denominator, simplify)
    {
    }

    /// <summary>
    /// Create a Fraction from a Fraction numerator and Fraction denominator
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
    /// Add two Fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator +(Fraction a, Fraction b)
        => new(a.Numerator * b.Denominator + b.Numerator * a.Denominator, a.Denominator * b.Denominator, false);

    /// <summary>
    /// Subtract two Fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator -(Fraction a, Fraction b)
        => a + -b;

    /// <summary>
    /// Multiply two Fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static Fraction operator *(Fraction a, Fraction b)
        => new(a.Numerator * b.Numerator, a.Denominator * b.Denominator, false);

    /// <summary>
    /// Divide Fractions
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    /// <exception cref="DivideByZeroException"></exception>
    public static Fraction operator /(Fraction a, Fraction b)
    {
        if (b.Numerator == 0)
        {
            throw new DivideByZeroException();
        }
        return new Fraction(a.Numerator * b.Denominator, a.Denominator * b.Numerator, false);
    }

    public static implicit operator double(Fraction? fraction) => 
        fraction is not null ? (double)fraction.Numerator / fraction.Denominator : 0.0;

    /// <summary>
    /// Get a Fraction as a string
    /// </summary>
    /// <returns></returns>
    public override string ToString() => $"{Numerator}/{Denominator}";

    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// Console.WriteLine("{0:N}/{0:D}", new Fraction(3, 4);
    /// </example>
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

    public int CompareTo(object? obj)
    {
        return CompareTo(obj as Fraction);
    }

    private static int CompareTo(Fraction? fraction1, Fraction? fraction2) => fraction1 switch
    {
        null when fraction2 is null => 0,
        _ => fraction1!.CompareTo(fraction2)
    };

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
    /// Simplify the Fraction if possible, producing
    /// a Fraction with the lowest common denominator
    /// </summary>
    /// <returns></returns>
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

    private static Fraction ToFraction(string value)
    {
        // TODO: Need some proper error handling
        var split = value.Split('/');
        if (split.Length == 2)
        {
            return new Fraction(Int32.Parse(split[0]), Int32.Parse(split[1]));
        }

        return ToFraction(Double.Parse(value));
    }

    private static Fraction ToFraction(double value)
    {
        if (value % 1 == 0) // if value is a whole number
        {
            return new Fraction((int)value);
        }

        checked
        {
            var s = value.ToString();
            var dp = s.IndexOf(".") + 1;
            var multiple = (int)Math.Pow(10, s.Length - dp);
            return new Fraction((int)Math.Round(value * multiple), multiple);
        }
    }

    /// <summary>
    /// Compare Fractions for equality
    /// </summary>
    /// <param name="fraction1"></param>
    /// <param name="fraction2"></param>
    /// <returns></returns>
    public static bool operator ==(Fraction fraction1, Fraction fraction2) =>
        Equals(fraction1, fraction2);

    /// <summary>
    /// Compare Fractions for inequality
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
    /// Compare Fractions for equality
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
    /// Compare Fractions for equality
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