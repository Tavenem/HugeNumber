![build](https://img.shields.io/github/workflow/status/Tavenem/HugeNumber/publish/main) [![NuGet downloads](https://img.shields.io/nuget/dt/Tavenem.HugeNumber)](https://www.nuget.org/packages/Tavenem.HugeNumber/)

Tavenem.HugeNumber
==

Tavenem.HugeNumber provides a struct which allows efficient recording of values in the range
±999999999999999999x10<sup>±32767</sup>. Also allows representing positive or negative infinity,
and NaN (not-a-number).

Note: the parameterless constructor returns `NaN` (not zero, as might be expected). To obtain zero, use the static propety `Zero`.

## Rational Fractions

Rational fractions cannot be constructed directly. All constructors accept a mantissa, or a
mantissa and an exponent. Conversions from floating point types always result in a floating
point value. Even an apparently simple value such as 1.5 is assumed to be irrational, since the
binary representation of decimal values can often be irrational, and therefore no assumptions
are made.

In order to represent a rational fraction with a HugeNumber, first construct (or cast) an
integral value as a HugeNumber, then perform a division operation with another integral value.
Mathematical operations between two rational fractions, or between a rational fraction and an
integral value, will also result in another rational fraction (unless the result is too large).
For example: `new HugeNumber(2) / 3` will result in the rational fraction 2/3 (i.e.
*not* an approximation such as 0.6666...).

Rational fractions can have a denominator no larger than [`ushort.MaxValue`](https://learn.microsoft.com/en-us/dotnet/api/system.uint16.maxvalue). Smaller
fractional values are represented as a mantissa and negative exponent (with a denominator of 1).

Rational fractions may also have exponents (positive or negative). The smallest HugeNumber
greater than zero (`Epsilon`) is therefore (1/65535)e-32767.

## Precision

Values have at most 18 significant digits in the mantissa, and 5 in the exponent. These limits are
fixed independently of one another; they do not trade off, as with the standard floating-point
types. I.e. you cannot have only one significent digit in the mantissa and thereby gain 22 in the
exponent.

Despite the ability to record floating-point values, `HugeNumber` values are internally stored as an
integral mantissa, exponent, and denominator. Therefore, arithmatic operations between `HugeNumber`
values which represent integers or rational fractions are not subject to floating point errors. For
example, `new HugeNumber(5) * 2 / 2 == 5` is always true.

This also applies to rational fractions: `new HugeNumber(10) / 4 == new Number (100) / 40` is also guaranteed to be true.

It also applies to rational floating point values too large to be represented as fractions:
`new HugeNumber(1, -20) / 4 == new Number (1, -19) / 40` is also guaranteed to be
true.

Note that floating point errors are still possible when performing arithmatic operations or
comparisons between *irrational* floating point values, or fractional values too large or
small, or with too many significant digits, to be represented as rational fractions. For
example, `new HugeNumber(2).Sqrt().Square() == 2` is *not* guaranteed to be true. It *may* evaluate to true, but this is not
guaranteed. The usual caveats and safeguards typically employed when performing floating
point math and/or comparisons should be applied to HugeNumber instances which do not represent
integral values or rational fractions. The method `IsNotRational(HugeNumber)` can
be used to determine whether a number is not integral or a rational fraction, in order to
determine if such safeguards are required.

## Installation

Tavenem.HugeNumber is available as a [NuGet package](https://www.nuget.org/packages/Tavenem.HugeNumber/).

## Roadmap

Tavenem.HugeNumber is a relatively stable library. Although additions and bugfixes are possible at any time, release should generally be expected to folow the .NET release cycle, with one or more preview releases during a framework preview, and a new stable release coinciding with the release of a new .NET framework major version.

## Contributing

Contributions are always welcome. Please carefully read the [contributing](docs/CONTRIBUTING.md) document to learn more before submitting issues or pull requests.

## Code of conduct

Please read the [code of conduct](docs/CODE_OF_CONDUCT.md) before engaging with our community, including but not limited to submitting or replying to an issue or pull request.