using Tavenem.Mathematics;

namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the cube root of a specified number.
    /// </summary>
    /// <param name="value">The number whose cube root is to be found.</param>
    /// <returns>
    /// The cube root of <paramref name="value"/>, or <see cref="NaN"/> if <paramref
    /// name="value"/> is <see cref="NaN"/>.
    /// </returns>
    /// <remarks>
    /// Note: this method never returns a rational fraction, even when <paramref name="value"/> has
    /// a cube root which could be represented as a rational fraction.
    /// </remarks>
    public static HugeNumber Cbrt(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.Mantissa == 0
            || value.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        if (value == One)
        {
            return value;
        }
        if (value.IsNegativeInfinity())
        {
            return NegativeInfinity;
        }
        if (value == NegativeOne)
        {
            return NegativeOne;
        }

        if (value.IsNegative())
        {
            return -Cbrt(-value);
        }

        return Exp(HugeNumberConstants.Third * Log(value));
    }

    /// <summary>
    /// Returns the cube root of this instance.
    /// </summary>
    /// <returns>
    /// The cube root of this instance, or <see cref="NaN"/> if this instance is <see
    /// cref="NaN"/>.
    /// </returns>
    public HugeNumber Cbrt() => Cbrt(this);

    /// <summary>
    /// Returns e raised to the specified power.
    /// </summary>
    /// <param name="value">A number specifying a power.</param>
    /// <returns>
    /// The number e raised to the power <paramref name="value"/>. If <paramref name="value"/>
    /// equals <see cref="NaN"/> or <see cref="PositiveInfinity"/>, that value is
    /// returned. If <paramref name="value"/> equals <see cref="NegativeInfinity"/>, 0 is
    /// returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// e is a mathematical constant whose value is approximately 2.71828182845904524.
    /// </para>
    /// <para>
    /// Use the <see cref="Pow(HugeNumber, HugeNumber)"/> method to calculate powers of other bases.
    /// </para>
    /// <para>
    /// <see cref="Exp(HugeNumber)"/> is the inverse of <see cref="Log(HugeNumber)"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber Exp(HugeNumber value)
    {
        if (value.IsNaN() || value.IsPositiveInfinity())
        {
            return value;
        }
        else if (value.IsNegativeInfinity())
        {
            return Zero;
        }
        else if (value.Mantissa == 0)
        {
            return 1;
        }
        else if (value == 1)
        {
            return E;
        }

        value = ToDenominator(value, 1);
        var result = 1 + value;
        if (result.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        var numerator = value * value;
        if (numerator.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        var denominator = new HugeNumber(2);
        var nextDenominatorDigit = 3;
        HugeNumber newResult;
        do
        {
            newResult = result + (numerator / denominator);
            if (newResult == result)
            {
                return result;
            }
            numerator *= value;
            if (numerator.IsPositiveInfinity())
            {
                return PositiveInfinity;
            }
            denominator *= nextDenominatorDigit;
            if (denominator.IsPositiveInfinity())
            {
                return result;
            }
            nextDenominatorDigit++;
            result = newResult;
        } while (nextDenominatorDigit <= int.MaxValue);
        return result;
    }

    /// <summary>
    /// Returns e raised to the power of this instance.
    /// </summary>
    /// <returns>
    /// The number e raised to the power of this instance. If this instance equals <see
    /// cref="NaN"/> or <see cref="PositiveInfinity"/>, that value is returned. If
    /// this instance equals <see cref="NegativeInfinity"/>, 0 is returned.
    /// </returns>
    /// <remarks>
    /// <para>
    /// e is a mathematical constant whose value is approximately 2.71828182845904524.
    /// </para>
    /// <para>
    /// Use the <see cref="Pow(HugeNumber)"/> method to calculate powers of other bases.
    /// </para>
    /// <para>
    /// <see cref="Exp(HugeNumber)"/> is the inverse of <see cref="Log(HugeNumber)"/>.
    /// </para>
    /// </remarks>
    public HugeNumber Exp() => Exp(this);

    /// <summary>
    /// Returns (x * y) + z, rounded as one ternary operation.
    /// </summary>
    /// <param name="x">The number to be multiplied with <paramref name="y"/>.</param>
    /// <param name="y">The number to be multiplied with <paramref name="x"/>.</param>
    /// <param name="z">The number to be added to the result of <paramref name="x"/> multiplied by <paramref name="y"/>.</param>
    /// <returns>
    /// (x * y) + z, rounded as one ternary operation.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This computes <c>(x * y)</c> as if to infinite precision, adds <paramref name="z"/>
    /// to that result as if to infinite precision, and finally rounds to the nearest representable value.
    /// </para>
    /// <para>
    /// This differs from the non-fused sequence which would compute <c>(x * y)</c> as if to infinite precision,
    /// round the result to the nearest representable value, add <paramref name="z"/> to the rounded result
    /// as if to infinite precision, and finally round to the nearest representable value.
    /// </para>
    /// </remarks>
    public static HugeNumber FusedMultiplyAdd(HugeNumber x, HugeNumber y, HugeNumber z)
    {
        if (x.IsNaN() || y.IsNaN() || z.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0 || y.Mantissa == 0)
        {
            return z;
        }
        if (x.IsInfinity() || y.IsInfinity())
        {
            if (Sign(x) == Sign(y))
            {
                return z.IsNegativeInfinity()
                    ? Zero
                    : PositiveInfinity;
            }
            return z.IsPositiveInfinity()
                ? Zero
                : NegativeInfinity;
        }
        if (z.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        if (z.IsNegativeInfinity())
        {
            return NegativeInfinity;
        }

        if (long.MaxValue / Math.Abs(x.Mantissa) < Math.Abs(y.Mantissa))
        {
            x = ToDenominator(x, 1);
            y = ToDenominator(y, 1);
        }
        else
        {
            var numerator = x.Mantissa * y.Mantissa;
            var denominator = (ulong)x.Denominator * y.Denominator;
            var greatestCommonFactor = GreatestCommonFactor(numerator, denominator);
            if (greatestCommonFactor > 1)
            {
                numerator /= (long)greatestCommonFactor;
                denominator /= greatestCommonFactor;
            }
            if (denominator > ushort.MaxValue
                || numerator > MAX_MANTISSA)
            {
                x = ToDenominator(x, 1);
                y = ToDenominator(y, 1);
            }
            else
            {
                return new HugeNumber(numerator, (ushort)denominator, (short)(x.Exponent + y.Exponent))
                    + z;
            }
        }

        var leftMantissa = (decimal)x.Mantissa;
        var leftExponent = x.Exponent;
        var rightMantissa = (decimal)y.Mantissa;
        var rightExponent = y.Exponent;
        while (MAX_MANTISSA / Math.Abs(leftMantissa) < Math.Abs(rightMantissa))
        {
            if (leftExponent + rightExponent > MAX_EXPONENT - 2)
            {
                return Sign(x) == Sign(y)
                    ? PositiveInfinity
                    : NegativeInfinity;
            }
            if (leftMantissa % 10 == 0)
            {
                leftMantissa /= 10;
                leftExponent++;
            }
            else if (rightMantissa % 10 == 0)
            {
                rightMantissa /= 10;
                rightExponent++;
            }
            else
            {
                leftMantissa /= 10;
                leftExponent++;
                rightMantissa /= 10;
                rightExponent++;
            }
        }

        var product = (HugeNumber)(leftMantissa * rightMantissa);
        var productExponent = (short)(leftExponent + rightExponent);

        var productMantissa = product.Mantissa;
        var productMantissaDigits = product.MantissaDigits;
        productExponent = (short)(productExponent + product.Exponent);
        Reduce(ref productMantissa, ref productExponent, ref productMantissaDigits);

        int comparison;
        // When either value is zero, exponents can be disregarded; only the sign of the
        // mantissa matters for the purpose of comparison.
        if (productMantissa == 0 || z.Mantissa == 0)
        {
            comparison = productMantissa.CompareTo(z.Mantissa);
        }
        else
        {
            var adjustedExponent = productExponent + (productMantissaDigits - 1);
            var otherAdjustedExponent = z.Exponent + (z.MantissaDigits - 1);

            // simple comparison between equal exponents
            if (adjustedExponent == otherAdjustedExponent)
            {
                var adjustedMantissa = productMantissa * Math.Pow(10, productExponent);
                var otherAdjustedMantissa = z.Mantissa * Math.Pow(10, z.Exponent);
                comparison = adjustedMantissa.CompareTo(otherAdjustedMantissa);
            }
            // simple cases with opposite signs
            else if (productMantissa < 0 && z.Mantissa > 0)
            {
                comparison = -1;
            }
            else if (z.Mantissa < 0 && productMantissa > 0)
            {
                comparison = 1;
            }
            // account for comparisons involving negative exponents
            else if (adjustedExponent < 0)
            {
                if (otherAdjustedExponent < 0)
                {
                    comparison = adjustedExponent.CompareTo(otherAdjustedExponent);
                }
                else
                {
                    comparison = productMantissa > 0 ? -1 : 1;
                }
            }
            else if (otherAdjustedExponent < 0)
            {
                comparison = productMantissa > 0 ? 1 : -1;
            }
            else
            {
                // direct comparison of exponents covers all other cases (mantissa comparison is
                // irrelevant when there is a difference in magnitude)
                comparison = adjustedExponent.CompareTo(otherAdjustedExponent) * (productMantissa < 0 ? -1 : 1);
            }
        }

        long smallerMantissa, largerMantissa;
        byte smallerMantissaDigits, largerMantissaDigits;
        short smallerExponent, largerExponent;
        ushort smallerDenominator, largerDenominator;
        if (comparison >= 0)
        {
            smallerMantissa = z.Mantissa;
            smallerMantissaDigits = z.MantissaDigits;
            smallerExponent = z.Exponent;
            smallerDenominator = z.Denominator;
            largerMantissa = productMantissa;
            largerMantissaDigits = productMantissaDigits;
            largerExponent = productExponent;
            largerDenominator = 1;
        }
        else
        {
            smallerMantissa = productMantissa;
            smallerMantissaDigits = productMantissaDigits;
            smallerExponent = productExponent;
            smallerDenominator = 1;
            largerMantissa = z.Mantissa;
            largerMantissaDigits = z.MantissaDigits;
            largerExponent = z.Exponent;
            largerDenominator = z.Denominator;
        }

        if (smallerDenominator > 1
            || largerDenominator > 1)
        {
            var leastCommonMultiple = LeastCommonMultiple(smallerDenominator, largerDenominator);
            var smaller = ToDenominator(
                new HugeNumber(smallerMantissa, smallerDenominator, smallerExponent),
                leastCommonMultiple ?? 1);
            var larger = ToDenominator(
                new HugeNumber(largerMantissa, largerDenominator, largerExponent),
                leastCommonMultiple ?? 1);
            if (smaller.Denominator != larger.Denominator)
            {
                smaller = ToDenominator(smaller, 1);
                larger = ToDenominator(larger, 1);
            }
            smallerMantissa = smaller.Mantissa;
            smallerMantissaDigits = smaller.MantissaDigits;
            smallerExponent = smaller.Exponent;

            largerMantissa = larger.Mantissa;
            largerMantissaDigits = larger.MantissaDigits;
            largerDenominator = larger.Denominator;
            largerExponent = larger.Exponent;
        }

        // Shift the smaller value into the exponent base of the larger, even if that
        // extinguishes all precision.
        while (smallerMantissa != 0 && smallerExponent < largerExponent)
        {
            if (largerMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
            {
                largerMantissa *= 10;
                largerMantissaDigits++;
                largerExponent--;
            }
            else
            {
                smallerMantissa /= 10;
                smallerMantissaDigits--;
                smallerExponent++;
            }
        }
        if (smallerExponent < largerExponent)
        {
            return new HugeNumber(largerMantissa, largerDenominator, largerExponent);
        }
        while (smallerExponent > largerExponent)
        {
            if (largerMantissa % 10 == 0)
            {
                largerMantissa /= 10;
                largerMantissaDigits--;
                largerExponent++;
            }
            else if (smallerMantissaDigits > 0)
            {
                smallerMantissa *= 10;
                smallerMantissaDigits++;
                smallerExponent--;
            }
            else
            {
                break;
            }
        }
        if (smallerExponent > largerExponent)
        {
            return new HugeNumber(largerMantissa, largerDenominator, largerExponent);
        }

        if (MAX_MANTISSA - smallerMantissa < largerMantissa)
        {
            if (largerExponent == MAX_EXPONENT)
            {
                return PositiveInfinity;
            }
            largerMantissa /= 10;
            largerExponent++;
            smallerMantissa /= 10;
        }

        return ReduceFraction(new HugeNumber(
            largerMantissa + smallerMantissa,
            largerDenominator,
            largerExponent));
    }

    /// <summary>
    /// Inverts the given value.
    /// </summary>
    /// <param name="x">The value to invert.</param>
    /// <returns>
    /// <para>
    /// If the value is <see cref="NaN"/>, returns <see cref="NaN"/>.
    /// The given value, inverted.
    /// </para>
    /// <para>
    /// If the value is <see cref="PositiveInfinity"/>, <see cref="Zero"/> is returned.
    /// </para>
    /// <para>
    /// If the value is <see cref="NegativeInfinity"/>, <see cref="NegativeZero"/> is returned.
    /// </para>
    /// <para>
    /// If the value is <see cref="Zero"/> or <see cref="NegativeZero"/>, <see cref="NaN"/> is
    /// returned.
    /// </para>
    /// <para>
    /// If the value represents an integer or a rational fraction, and the numerator is &lt;=
    /// <see cref="ushort.MaxValue"/>, the result is a rational fraction with the numerator and the
    /// denominator swapped, and the exponent's sign reversed.
    /// </para>
    /// <para>
    /// In all other cases, the result of one divided by the value is returned.
    /// </para>
    /// </returns>
    public static HugeNumber Invert(HugeNumber x)
    {
        if (IsNaN(x))
        {
            return NaN;
        }
        if (IsPositiveInfinity(x))
        {
            return Zero;
        }
        if (IsNegativeInfinity(x))
        {
            return NegativeZero;
        }
        if (x.Mantissa == 0)
        {
            return NaN;
        }
        if (Math.Abs(x.Mantissa) <= ushort.MaxValue
            && (x.Denominator > 1
            || x.Exponent >= 0))
        {
            return new(x.Denominator, (ushort)x.Mantissa, (short)-x.Exponent);
        }
        return One / x;
    }

    /// <summary>
    /// Inverts this value.
    /// </summary>
    /// <returns>
    /// <para>
    /// If the value is <see cref="NaN"/>, returns <see cref="NaN"/>.
    /// The given value, inverted.
    /// </para>
    /// <para>
    /// If the value is <see cref="PositiveInfinity"/>, <see cref="Zero"/> is returned.
    /// </para>
    /// <para>
    /// If the value is <see cref="NegativeInfinity"/>, <see cref="NegativeZero"/> is returned.
    /// </para>
    /// <para>
    /// If the value is <see cref="Zero"/> or <see cref="NegativeZero"/>, <see cref="NaN"/> is
    /// returned.
    /// </para>
    /// <para>
    /// If the value represents an integer or a rational fraction, and the numerator is &lt;=
    /// <see cref="ushort.MaxValue"/>, the result is a rational fraction with the numerator and the
    /// denominator swapped, and the exponent's sign reversed.
    /// </para>
    /// <para>
    /// In all other cases, the result of one divided by the value is returned.
    /// </para>
    /// </returns>
    public HugeNumber Invert() => Invert(this);

    /// <summary>
    /// Raises an <see cref="HugeNumber"/> value to the power of a specified value.
    /// </summary>
    /// <param name="value">The number to raise to the <paramref name="exponent"/>
    /// power.</param>
    /// <param name="exponent">The exponent to raise <paramref name="value"/> by.</param>
    /// <returns>
    /// The result of raising <paramref name="value"/> to the <paramref name="exponent"/> power.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="value"/> is <see cref="NaN"/> or <paramref name="exponent"/> is
    /// negative, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/>, the result is 1 if
    /// <paramref name="exponent"/> is 0, otherwise <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// If <paramref name="value"/> is <see cref="NegativeInfinity"/>, the result is 1 if
    /// <paramref name="exponent"/> is 0, <see cref="PositiveInfinity"/> if <paramref
    /// name="exponent"/> is even, or <see cref="NegativeInfinity"/> if exponent is odd.
    /// </para>
    /// <para>
    /// Note: this method never returns a rational fraction unless <paramref name="exponent"/> is 1,
    /// even when <paramref name="value"/> is a rational fraction and the result could be
    /// represented as a rational fraction. Both <see cref="Square(HugeNumber)"/> and
    /// <see cref="Cube(HugeNumber)"/> will return rational fractions when possible, and can be used
    /// in place of this method for powers of 2 and 3, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Pow(HugeNumber value, HugeNumber exponent)
    {
        if (value.IsNaN()
            || exponent.IsNaN())
        {
            return NaN;
        }
        if (exponent.Mantissa == 0)
        {
            return One;
        }
        if (value.Mantissa == 0)
        {
            return exponent.IsNegative()
                ? PositiveInfinity
                : Zero;
        }
        if (value == One
            || exponent == One)
        {
            return value;
        }
        if (value.IsPositiveInfinity())
        {
            return exponent.IsNegative()
                ? Zero
                : PositiveInfinity;
        }
        if (value.IsNegative()
            && exponent.Exponent < 0
            && (One / exponent).Exponent < 0)
        {
            return NaN;
        }
        if (value.IsNegativeInfinity())
        {
            if (exponent.IsNegative())
            {
                return Zero;
            }
            else if (exponent.Exponent < 0 || exponent % 2 != 0)
            {
                return NegativeInfinity;
            }
            else
            {
                return PositiveInfinity;
            }
        }
        if (exponent.IsPositiveInfinity())
        {
            if (value == NegativeOne)
            {
                return NaN;
            }
            else if (value.Abs() > One)
            {
                return PositiveInfinity;
            }
            else
            {
                return Zero;
            }
        }
        if (exponent.IsNegativeInfinity())
        {
            if (value == NegativeOne)
            {
                return NaN;
            }
            else if (value.Abs() > One)
            {
                return Zero;
            }
            else
            {
                return NaN;
            }
        }
        if (value == NegativeOne)
        {
            return exponent.Exponent < 0 || exponent % 2 != 0
                ? NegativeOne
                : One;
        }

        if (value.IsNegative())
        {
            return -Pow(-value, exponent);
        }

        if (exponent.IsNegative())
        {
            return One / Pow(value, -exponent);
        }

        return Exp(exponent * Log(value));
    }

    /// <summary>
    /// Raises this instance to the power of a specified value.
    /// </summary>
    /// <param name="exponent">The exponent by which to raise this instance.</param>
    /// <returns>
    /// The result of raising this instance to the <paramref name="exponent"/> power.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this instance is <see cref="NaN"/> or <paramref name="exponent"/> is negative, the
    /// result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If this instance is <see cref="PositiveInfinity"/>, the result is 1 if
    /// <paramref name="exponent"/> is 0, otherwise <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// If this instance is <see cref="NegativeInfinity"/>, the result is 1 if
    /// <paramref name="exponent"/> is 0, <see cref="PositiveInfinity"/> if <paramref
    /// name="exponent"/> is even, or <see cref="NegativeInfinity"/> if exponent is odd.
    /// </para>
    /// </remarks>
    public HugeNumber Pow(HugeNumber exponent) => Pow(this, exponent);

    /// <summary>
    /// Returns x * 2^n computed efficiently.
    /// </summary>
    /// <typeparam name="TInteger">The type of <paramref name="n"/>.</typeparam>
    /// <param name="x">A <see cref="HugeNumber"/> that specifies the base value.</param>
    /// <param name="n">An integer that specifies the power.</param>
    /// <returns>
    /// x * 2^n computed efficiently.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <typeparamref name="TInteger"/> must be convertible to <see cref="int"/>.
    /// </exception>
    public static HugeNumber ScaleB<TInteger>(HugeNumber x, TInteger n)
        where TInteger : IBinaryInteger<TInteger> => x * Math.ScaleB(1, (int)(object)n);

    /// <summary>
    /// Returns the square root of a specified number.
    /// </summary>
    /// <param name="value">The number whose square root is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="value"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Zero or positive</term>
    /// <description>The positive square root of <paramref name="value"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="NaN"/></description>
    /// </item>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <description><see cref="NaN"/></description>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <description><see cref="PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// Note: this method never returns a rational fraction, even when <paramref name="value"/> has
    /// a square root which could be represented as a rational fraction.
    /// </remarks>
    public static HugeNumber Sqrt(HugeNumber value)
    {
        if (value.IsNaN()
            || value.IsNegative())
        {
            return NaN;
        }
        if (value.Mantissa == 0
            || value == One
            || value.IsPositiveInfinity())
        {
            return value;
        }

        return Exp(HugeNumberConstants.Half * Log(value));
    }

    /// <summary>
    /// Returns the square root of this instance.
    /// </summary>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>This instance</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Zero or positive</term>
    /// <description>The positive square root of this instance</description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="NaN"/></description>
    /// </item>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <description><see cref="NaN"/></description>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <description><see cref="PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public HugeNumber Sqrt() => Sqrt(this);
}
