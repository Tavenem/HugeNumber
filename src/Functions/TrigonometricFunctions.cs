namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Computes the arc-cosine of a value.
    /// </summary>
    /// <param name="x">
    /// The value, in radians, whose arc-cosine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-cosine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arccos(x)</c> in the interval <c>[+0, +π]</c> radians.
    /// </remarks>
    public static HugeNumber Acos(HugeNumber x)
    {
        if (x.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return HugeNumberConstants.HalfPi;
        }

        return 2 * Atan((1 - x.Square()).Sqrt() / (1 + x));
    }

    /// <summary>
    /// Computes the arc-cosine of this value.
    /// </summary>
    /// <returns>
    /// The arc-cosine of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>arccos(x)</c> in the interval <c>[+0, +π]</c> radians.
    /// </remarks>
    public HugeNumber Acos() => Acos(this);

    /// <summary>
    /// Computes the arc-cosine of a value and divides the result by <c>π</c>.
    /// </summary>
    /// <param name="x">
    /// The value whose arc-cosine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-cosine of <paramref name="x"/>, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arccos(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public static HugeNumber AcosPi(HugeNumber x)
    {
        if (x.Mantissa == 0)
        {
            return HugeNumberConstants.Half;
        }

        return x > Zero
            ? Acos(x) / Pi
            : One - (Acos(x.Negate()) / Pi);
    }

    /// <summary>
    /// Computes the arc-cosine of this value and divides the result by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The arc-cosine of this value, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arccos(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public HugeNumber AcosPi() => AcosPi(this);

    /// <summary>
    /// Computes the arc-sine of a value.
    /// </summary>
    /// <param name="x">
    /// The value, in radians, whose arc-sine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-sine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arcsin(x)</c> in the interval <c>[-π/2, +π/2]</c> radians.
    /// </remarks>
    public static HugeNumber Asin(HugeNumber x)
    {
        if (x.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return Zero;
        }

        return Atan(x / (1 - x.Square()).Sqrt());
    }

    /// <summary>
    /// Computes the arc-sine of this value.
    /// </summary>
    /// <returns>
    /// The arc-sine of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>arcsin(x)</c> in the interval <c>[-π/2, +π/2]</c> radians.
    /// </remarks>
    public HugeNumber Asin() => Asin(this);

    /// <summary>
    /// Computes the arc-sine of a value and divides the result by <c>π</c>.
    /// </summary>
    /// <param name="x">
    /// The value whose arc-sine is to be computed.
    /// </param>
    /// <returns>
    /// The arc-sine of <paramref name="x"/>, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arcsin(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public static HugeNumber AsinPi(HugeNumber x)
    {
        if (x.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return Zero;
        }
        if (x == One)
        {
            return HugeNumberConstants.Half;
        }
        if (x == NegativeOne)
        {
            return -HugeNumberConstants.Half;
        }

        return AtanPi(x / (1 - x.Square()).Sqrt());
    }

    /// <summary>
    /// Computes the arc-sine of this value and divides the result by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The arc-sine of this value, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arcsin(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public HugeNumber AsinPi() => AsinPi(this);

    /// <summary>
    /// Computes the arc-tangent of a value.
    /// </summary>
    /// <param name="x">The value, in radians, whose arc-tangent is to be computed.</param>
    /// <returns>
    /// The arc-tangent of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arctan(x)</c> in the interval <c>[-π/2, +π/2]</c> radians.
    /// </remarks>
    public static HugeNumber Atan(HugeNumber x)
    {
        if (x.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return Zero;
        }
        if (x == One)
        {
            return HugeNumberConstants.QuarterPi;
        }
        if (x.IsNegativeInfinity())
        {
            return -HugeNumberConstants.HalfPi;
        }
        if (x.IsPositiveInfinity())
        {
            return HugeNumberConstants.HalfPi;
        }
        if (x.IsNegative())
        {
            return Atan(x.Negate()).Negate();
        }
        if (x > One)
        {
            return HugeNumberConstants.HalfPi - Atan(One / x);
        }
        if (x > _TwoMinusRoot3)
        {
            return HugeNumberConstants.Two * Atan(x / (One + Sqrt(One + (x * x))));
        }

        var subtract = true;
        var power = x.Cube();
        var square = x.Square();
        var denominator = new HugeNumber(3);
        var two = new HugeNumber(2);
        var result = x - (power / denominator);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            denominator += two;

            if (subtract)
            {
                result -= power / denominator;
            }
            else
            {
                result += power / denominator;
            }
        }
        while (lastResult != result);

        return result;
    }

    /// <summary>
    /// Computes the arc-tangent of this value.
    /// </summary>
    /// <returns>
    /// The arc-tangent of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>arctan(x)</c> in the interval <c>[-π/2, +π/2]</c> radians.
    /// </remarks>
    public HugeNumber Atan() => Atan(this);

    /// <summary>
    /// Computes the arc-tangent of a value and divides the result by <c>π</c>.
    /// </summary>
    /// <param name="x">The value whose arc-tangent is to be computed.</param>
    /// <returns>
    /// The arc-tangent of <paramref name="x"/>, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arctan(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public static HugeNumber AtanPi(HugeNumber x)
    {
        if (x.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return Zero;
        }
        if (x == One)
        {
            return HugeNumberConstants.Fourth;
        }
        if (x.IsNegativeInfinity())
        {
            return -HugeNumberConstants.Half;
        }
        if (x.IsPositiveInfinity())
        {
            return HugeNumberConstants.Half;
        }
        if (x.IsNegative())
        {
            return -AtanPi(-x);
        }
        if (x > One)
        {
            return HugeNumberConstants.Half - AtanPi(One / x);
        }
        if (x > _TwoMinusRoot3)
        {
            return HugeNumberConstants.Two * AtanPi(x / (One + Sqrt(One + (x * x))));
        }

        var subtract = true;
        var power = x.Cube();
        var square = x.Square();
        var denominator = new HugeNumber(3);
        var two = new HugeNumber(2);
        var result = x - (power / denominator);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            denominator += two;

            if (subtract)
            {
                result -= power / denominator;
            }
            else
            {
                result += power / denominator;
            }
        }
        while (lastResult != result);

        return result / Pi;
    }

    /// <summary>
    /// Computes the arc-tangent of this value and divides the result by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The arc-tangent of this value, divided by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>arctan(x) / π</c> in the interval <c>[-0.5, +0.5]</c>.
    /// </remarks>
    public HugeNumber AtanPi() => AtanPi(this);

    /// <summary>
    /// Computes the cosine of a value.
    /// </summary>
    /// <param name="x">The value, in radians, whose cosine is to be computed.</param>
    /// <returns>
    /// The cosine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>cos(x)</c>.
    /// </remarks>
    public static HugeNumber Cos(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return NaN;
        }
        if (x < 0)
        {
            return Cos(-x);
        }
        if (x > HugeNumberConstants.TwoPi)
        {
            return Cos(x - (HugeNumberConstants.TwoPi * (x / HugeNumberConstants.TwoPi).Floor()));
        }
        if (x > HugeNumberConstants.ThreeHalvesPi)
        {
            return Cos(HugeNumberConstants.TwoPi - x);
        }
        if (x > Pi)
        {
            return -Cos(x - Pi);
        }
        if (x > HugeNumberConstants.HalfPi)
        {
            return -Cos(Pi - x);
        }
        if (x > HugeNumberConstants.QuarterPi)
        {
            return Sin(HugeNumberConstants.HalfPi - x);
        }

        var subtract = true;
        var square = x.Square();
        var power = square;
        var factorialDigit = 2;
        var factorial = HugeNumberConstants.Two;
        var result = 1 - (power / factorial);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            factorial *= ++factorialDigit;
            factorial *= ++factorialDigit;

            if (subtract)
            {
                result -= power / factorial;
            }
            else
            {
                result += power / factorial;
            }
        }
        while (lastResult != result);

        return result;
    }

    /// <summary>
    /// Computes the cosine of this value.
    /// </summary>
    /// <returns>
    /// The cosine of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>cos(x)</c>.
    /// </remarks>
    public HugeNumber Cos() => Cos(this);

    /// <summary>
    /// Computes the cosine of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <param name="x">The value, in half-revolutions, whose cosine is to be computed.</param>
    /// <returns>
    /// The cosine of <paramref name="x"/> multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>cos(πx)</c>.
    /// </remarks>
    public static HugeNumber CosPi(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return NaN;
        }
        if (x.Abs() < HugeNumberConstants.Fourth)
        {
            return Cos(x * Pi);
        }
        if (x.IsNegative())
        {
            x = x.Negate();
        }

        var invert = false;
        var floor = x.Floor();
        if (floor == x)
        {
            return invert ? NegativeOne : One;
        }
        if (floor.IsOddInteger())
        {
            invert = true;
        }

        var rem = x - floor;
        if (rem > HugeNumberConstants.Half)
        {
            rem = One - rem;
            invert = !invert;
        }
        else if (rem == HugeNumberConstants.Half)
        {
            return Zero;
        }

        var cos = Cos(rem * Pi);
        return invert ? cos.Negate() : cos;
    }

    /// <summary>
    /// Computes the cosine of this value multipled by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The cosine of this value multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>cos(πx)</c>.
    /// </remarks>
    public HugeNumber CosPi() => CosPi(this);

    /// <summary>
    /// Computes the sine of a value.
    /// </summary>
    /// <param name="x">The value, in radians, whose sine is to be computed.</param>
    /// <returns>
    /// The sine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>sin(x)</c>.
    /// </remarks>
    public static HugeNumber Sin(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return NaN;
        }
        if (x < 0)
        {
            return -Sin(-x);
        }
        if (x > HugeNumberConstants.TwoPi)
        {
            return Sin(x - (HugeNumberConstants.TwoPi * (x / HugeNumberConstants.TwoPi).Floor()));
        }
        if (x > HugeNumberConstants.ThreeHalvesPi)
        {
            return -Sin(HugeNumberConstants.TwoPi - x);
        }
        if (x > Pi)
        {
            return -Sin(x - Pi);
        }
        if (x > HugeNumberConstants.HalfPi)
        {
            return Sin(Pi - x);
        }
        if (x > HugeNumberConstants.QuarterPi)
        {
            return Cos(HugeNumberConstants.HalfPi - x);
        }

        var subtract = true;
        var square = x.Square();
        var power = square * x;
        var factorialDigit = 3;
        var factorial = new HugeNumber(6);
        var result = x - (power / factorial);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            factorial *= ++factorialDigit;
            factorial *= ++factorialDigit;

            if (subtract)
            {
                result -= power / factorial;
            }
            else
            {
                result += power / factorial;
            }
        }
        while (lastResult != result);

        return result;
    }

    /// <summary>
    /// Computes the sine of this value.
    /// </summary>
    /// <returns>
    /// The sine of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>sin(x)</c>.
    /// </remarks>
    public HugeNumber Sin() => Sin(this);

    /// <summary>
    /// Computes the sine and cosine of a value.
    /// </summary>
    /// <param name="x">The value, in radians, whose sine and cosine are to be computed.</param>
    /// <returns>
    /// The sine and cosine of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>(sin(x), cos(x))</c>.
    /// </remarks>
    public static (HugeNumber Sin, HugeNumber Cos) SinCos(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return (NaN, NaN);
        }
        if (x < 0)
        {
            return (-Sin(-x), Cos(-x));
        }
        if (x > HugeNumberConstants.TwoPi)
        {
            return (Sin(x - (HugeNumberConstants.TwoPi * (x / HugeNumberConstants.TwoPi).Floor())),
                Cos(x - (HugeNumberConstants.TwoPi * (x / HugeNumberConstants.TwoPi).Floor())));
        }
        if (x > HugeNumberConstants.ThreeHalvesPi)
        {
            return (-Sin(HugeNumberConstants.TwoPi - x),
                Cos(HugeNumberConstants.TwoPi - x));
        }
        if (x > Pi)
        {
            return (-Sin(x - Pi), -Cos(x - Pi));
        }
        if (x > HugeNumberConstants.HalfPi)
        {
            return (Sin(Pi - x), -Cos(Pi - x));
        }
        if (x > HugeNumberConstants.QuarterPi)
        {
            return (Cos(HugeNumberConstants.HalfPi - x),
                Sin(HugeNumberConstants.HalfPi - x));
        }

        var subtract = true;
        var square = x.Square();
        var sinPower = square * x;
        var sinFactorialDigit = 3;
        var sinFactorial = new HugeNumber(6);
        var sinResult = x - (sinPower / sinFactorial);
        HugeNumber lastSinResult, lastCosResult;
        do
        {
            lastSinResult = sinResult;

            subtract = !subtract;
            sinPower *= square;
            sinFactorial *= ++sinFactorialDigit;
            sinFactorial *= ++sinFactorialDigit;

            if (subtract)
            {
                sinResult -= sinPower / sinFactorial;
            }
            else
            {
                sinResult += sinPower / sinFactorial;
            }
        }
        while (lastSinResult != sinResult);

        subtract = true;
        var cosPower = square;
        var cosFactorialDigit = 2;
        var cosFactorial = HugeNumberConstants.Two;
        var cosResult = 1 - (cosPower / cosFactorial);
        do
        {
            lastCosResult = cosResult;

            subtract = !subtract;
            cosPower *= square;
            cosFactorial *= ++cosFactorialDigit;
            cosFactorial *= ++cosFactorialDigit;

            if (subtract)
            {
                cosResult -= cosPower / cosFactorial;
            }
            else
            {
                cosResult += cosPower / cosFactorial;
            }
        }
        while (lastCosResult != cosResult);

        return (sinResult, cosResult);
    }

    /// <summary>
    /// Computes the sine and cosine of this value.
    /// </summary>
    /// <returns>
    /// The sine and cosine of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>(sin(x), cos(x))</c>.
    /// </remarks>
    public (HugeNumber Sin, HugeNumber Cos) SinCos() => SinCos(this);

    /// <summary>
    /// Computes the sine and cosine of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <param name="x">
    /// The value, in half-revolutions, that is multipled by <c>π</c> before computing its sine and
    /// cosine.
    /// </param>
    /// <returns>
    /// The sine and cosine of <paramref name="x"/> multipled-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>(sin(πx), cos(πx))</c>.
    /// </remarks>
    public static (HugeNumber SinPi, HugeNumber CosPi) SinCosPi(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return (NaN, NaN);
        }
        if (x.IsNegative())
        {
            var negated = x.Negate();
            return (-SinPi(negated), CosPi(negated));
        }

        if (x < HugeNumberConstants.Fourth)
        {
            var xPi = x * Pi;
            return (Sin(xPi), Cos(xPi));
        }

        HugeNumber sinResult, cosResult;

        var invertCos = false;
        var floor = x.Floor();
        if (floor == x)
        {
            cosResult = invertCos ? NegativeOne : One;
        }
        else
        {
            if (floor.IsOddInteger())
            {
                invertCos = true;
            }

            var rem = x - floor;
            if (rem == HugeNumberConstants.Half)
            {
                cosResult = Zero;
            }
            else
            {
                if (rem > HugeNumberConstants.Half)
                {
                    rem = One - rem;
                    invertCos = !invertCos;
                }

                var cos = Cos(rem * Pi);
                cosResult = invertCos ? cos.Negate() : cos;
            }
        }

        if (x < HugeNumberConstants.Half)
        {
            sinResult = Sin(x * Pi);
        }
        else
        {
            var invertSin = false;
            if (x < One)
            {
                invertSin = true;
                x = x.Negate();
                floor = floor.Negate();
            }

            if (floor == x)
            {
                sinResult = invertSin ? NegativeZero : Zero;
            }
            else
            {
                if (floor.IsOddInteger())
                {
                    invertSin = !invertSin;
                }

                var rem = x - floor;
                if (rem == HugeNumberConstants.Half)
                {
                    sinResult = invertSin ? NegativeOne : One;
                }
                else
                {
                    if (rem > HugeNumberConstants.Half)
                    {
                        rem = One - rem;
                    }

                    var sin = Sin(rem * Pi);
                    sinResult = invertSin ? sin.Negate() : sin;
                }
            }
        }

        return (sinResult, cosResult);
    }

    /// <summary>
    /// Computes the sine and cosine of this value multipled by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The sine and cosine of this value multipled-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>(sin(πx), cos(πx))</c>.
    /// </remarks>
    public (HugeNumber SinPi, HugeNumber CosPi) SinCosPi() => SinCosPi(this);

    /// <summary>
    /// Computes the sine of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <param name="x">
    /// The value, in half-revolutions, that is multipled by <c>π</c> before computing its sine.
    /// </param>
    /// <returns>
    /// The sine of <paramref name="x"/> multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>sin(πx)</c>.
    /// </remarks>
    public static HugeNumber SinPi(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return NaN;
        }
        if (x.IsNegative())
        {
            return SinPi(x.Negate()).Negate();
        }
        if (x < HugeNumberConstants.Half)
        {
            return Sin(x * Pi);
        }

        var invert = false;
        if (x < One)
        {
            invert = true;
            x = x.Negate();
        }

        var floor = x.Floor();
        if (floor == x)
        {
            return invert ? NegativeZero : Zero;
        }
        if (floor.IsOddInteger())
        {
            invert = !invert;
        }

        var rem = x - floor;
        if (rem == HugeNumberConstants.Half)
        {
            return invert ? NegativeOne : One;
        }
        else if (rem > HugeNumberConstants.Half)
        {
            rem = One - rem;
        }

        var sin = Sin(rem * Pi);
        return invert ? sin.Negate() : sin;
    }

    /// <summary>
    /// Computes the sine of this value multipled by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The sine of this value multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>sin(πx)</c>.
    /// </remarks>
    public HugeNumber SinPi() => SinPi(this);

    /// <summary>
    /// Computes the tangent of a value.
    /// </summary>
    /// <param name="x">The value, in radians, whose tangent is to be computed.</param>
    /// <returns>
    /// The tangent of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// This computes <c>tan(x)</c>.
    /// </remarks>
    public static HugeNumber Tan(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return NaN;
        }
        if (x < 0)
        {
            return -Tan(-x);
        }
        if (x > Pi)
        {
            return Tan(x - (Pi * (x / Pi).Floor()));
        }
        if (x > HugeNumberConstants.ThreeQuartersPi)
        {
            return 1 / -Tan(Pi - x);
        }
        if (x > HugeNumberConstants.HalfPi)
        {
            return -Tan(Pi - x);
        }
        if (x > HugeNumberConstants.QuarterPi)
        {
            return 1 / Tan(HugeNumberConstants.HalfPi - x);
        }
        if (x > HugeNumberConstants.EighthPi)
        {
            var tanHalf = Tan(x / HugeNumberConstants.Two);
            return HugeNumberConstants.Two * tanHalf / (One - (tanHalf * tanHalf));
        }

        var taylorSeriesIndex = 0;
        var square = x.Square();
        var power = square * x;
        var result = x + (_TangentTaylorSeries[taylorSeriesIndex] * power);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            power *= square;
            result += power * _TangentTaylorSeries[++taylorSeriesIndex];
        }
        while (lastResult != result && taylorSeriesIndex < _TangentTaylorSeries.Length - 1);

        return result;
    }

    /// <summary>
    /// Computes the tangent of this value.
    /// </summary>
    /// <returns>
    /// The tangent of this value.
    /// </returns>
    /// <remarks>
    /// This computes <c>tan(x)</c>.
    /// </remarks>
    public HugeNumber Tan() => Tan(this);

    /// <summary>
    /// Computes the tangent of a value that has been multipled by <c>π</c>.
    /// </summary>
    /// <param name="x">
    /// The value, in half-revolutions, that is multipled by <c>π</c> before computing its tangent.
    /// </param>
    /// <returns>
    /// The tangent of <paramref name="x"/> multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>tan(πx)</c>.
    /// </remarks>
    public static HugeNumber TanPi(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsInfinity())
        {
            return NaN;
        }
        if (x.Abs() < HugeNumberConstants.Half)
        {
            return Tan(x * Pi);
        }

        var invert = false;
        if (x.IsNegative())
        {
            x = x.Negate();
            invert = true;
        }

        var floor = x.Floor();
        if (floor == x)
        {
            if (floor.IsOddInteger())
            {
                invert = !invert;
            }
            return invert ? NegativeZero : Zero;
        }

        var rem = x - floor;
        if (rem == HugeNumberConstants.Half)
        {
            if (floor.IsOddInteger())
            {
                invert = !invert;
            }
            return invert ? NegativeInfinity : PositiveInfinity;
        }
        else if (rem > HugeNumberConstants.Half)
        {
            rem = One - rem;
            invert = !invert;
        }

        var tan = Tan(rem * Pi);
        return invert ? tan.Negate() : tan;
    }

    /// <summary>
    /// Computes the tangent of this value multipled by <c>π</c>.
    /// </summary>
    /// <returns>
    /// The tangent of this value multiplied-by <c>π</c>.
    /// </returns>
    /// <remarks>
    /// This computes <c>tan(πx)</c>.
    /// </remarks>
    public HugeNumber TanPi() => TanPi(this);
}
