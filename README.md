# PutridParrot.Fractions

[![Build PutridParrot.Fractions](https://github.com/putridparrot/PutridParrot.Fractions/actions/workflows/build.yml/badge.svg)](https://github.com/putridparrot/PutridParrot.Fractions/actions/workflows/build.yml)
[![NuGet version (PutridParrot.Fractions)](https://img.shields.io/nuget/v/PutridParrot.Fractions.svg?style=flat-square)](https://www.nuget.org/packages/PutridParrot.Fractions/)
[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/putridparrot/PutridParrot.Fractions/blob/master/LICENSE.md)
[![GitHub Releases](https://img.shields.io/github/release/putridparrot/PutridParrot.Fractions.svg)](https://github.com/putridparrot/PutridParrot.Fractions/releases)
[![GitHub Issues](https://img.shields.io/github/issues/putridparrot/PutridParrot.Fractions.svg)](https://github.com/putridparrot/PutridParrot.Fractions/issues)


A simple Fractions class/library. Includes several constructors which allow passing of the numerator, denominator or a decimal point value which is converted to fractional or a string which is converted from a decimal representation or fractional representation.

Includes comparison operators, mathematical operations, can be cast to a decimal point number.

_Note: Will update to use .NET 7's INumber interface when that's released._

## Usage

Install the latest package from nuget, for example by running 

```
dotnet add package PutridParrot.Fractions --version 1.0.1-alpha
```

In usages we simply use something like the following

```
using PutridParrot.Fractions;

var fraction1 = new Fraction(1, 2);
var fraction2 = new Fraction(10, 20);

if (fraction1 == fraction2)
{
    // they are equivalent
}
```

