using FsCheck;
using PutridParrot.Fractions;
using System.Diagnostics.CodeAnalysis;
using PropertyAttribute = FsCheck.NUnit.PropertyAttribute;

namespace Tests.PutridParrot.Fractions;

[ExcludeFromCodeCoverage]
public class PropertyTests
{
    [Property]
    public void FromDoubleToFractionAndBack()
    {
        Prop.ForAll<NormalFloat>(number =>
        {
            // truncate to 4dp
            var truncated = Double.Parse(number.Item.ToString("0.0000"));
            var fraction = new Fraction(truncated);
            var result = (double)fraction;
            return Is.EqualTo(result).Within(0.0001).ApplyTo(truncated).IsSuccess;
        }).QuickCheckThrowOnFailure();
    }

    [Property]
    public void FromDoubleAsStringToFractionAndBack()
    {
        Prop.ForAll<NormalFloat>(number =>
        {
            // truncate to 4dp
            var truncated = number.Item.ToString("0.0000");
            var fraction = new Fraction(truncated);
            var result = (double)fraction;
            return Is.EqualTo(result.ToString("0.0000")).Within(0.0001).ApplyTo(truncated).IsSuccess;
        }).QuickCheckThrowOnFailure();
    }
}