![build](https://img.shields.io/github/workflow/status/Tavenem/HugeNumber/publish/main) [![NuGet downloads](https://img.shields.io/nuget/dt/Tavenem.HugeNumber)](https://www.nuget.org/packages/Tavenem.HugeNumber/)

Tavenem.HugeNumber
==

Tavenem.HugeNumber provides a struct which allows efficient recording of values in the range
±999999999999999999x10<sup>±32767</sup>. Also allows representing positive or negative infinity,
and NaN (not-a-number).

Values have at most 18 significant digits in the mantissa, and 5 in the exponent. These limits are
fixed independently of one another; they do not trade off, as with the standard floating-point
types. I.e. you cannot have only one significent digit in the mantissa and thereby gain 22 in the
exponent.

Despite the ability to record floating-point values, `HugeNumber` values are internally stored as
integral pairs of mantissa and exponent. Therefore, arithmatic operations between `HugeNumber` values
which represent integers are not subject to floating point errors. For example, `new
HugeNumber(5) * 2 / 2 == 5` is always true. This also applies to rational
floating point values: `new HugeNumber(10) / 4 == new Number (100) / 40` is also guaranteed
to be true.

Note that imprecision is still possible when performing arithmatic operations or comparisons between
*irrational* floating point values, or when converting to and from non-HugeNumber floating point
types such as float and double. For example, `new HugeNumber(1) / new HugeNumber(3) == 1.0 / 3.0` is
*not* guaranteed to be true, nor is `new HugeNumber(2) / new HugeNumber (6) / 2 == new HugeNumber(1)
/ new HugeNumber(3)`. These *may* evaluate to true, but this is not guaranteed. The usual caveats
and safeguards typically employed when performing floating point math and/or comparisons should be
applied to HugeNumber instances which represent irrational values, or have been converted from a
non-HugeNumber floating point value.

## Installation

Tavenem.HugeNumber is available as a [NuGet package](https://www.nuget.org/packages/Tavenem.HugeNumber/).

## Roadmap

Tavenem.HugeNumber is a relatively stable library which sees minimal development. Although additions and bugfixes are always possible, no specific updates are planned at this time.

## Contributing

Contributions are always welcome. Please carefully read the [contributing](docs/CONTRIBUTING.md) document to learn more before submitting issues or pull requests.

## Code of conduct

Please read the [code of conduct](docs/CODE_OF_CONDUCT.md) before engaging with our community, including but not limited to submitting or replying to an issue or pull request.