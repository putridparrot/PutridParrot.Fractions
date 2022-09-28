using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PutridParrot.Fractions;

namespace Tests.PutridParrot.Fractions;

public class GreatestCommonDivisorTests
{
    [TestCase(24, 40, 8)]
    [TestCase(1, 1, 1)]
    [TestCase(1, 800, 1)]
    [TestCase(11, 37, 1)]
    [TestCase(3, 5, 1)]
    [TestCase(16, 4, 4)]
    [TestCase(-3, 9, 3)]
    [TestCase(9, -3, 3)]
    [TestCase(3, -9, 3)]
    [TestCase(-3, -9, 3)]
    public void Tests(int numerator, int denominator, int expected)
    {
        Assert.That(Fraction.GetCommonDivisor(numerator, denominator), Is.EqualTo(expected));
    }

}