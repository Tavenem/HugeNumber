namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// A fast implementation of cubing (a number raised to the power of 3).
    /// </summary>
    /// <param name="value">The value to cube.</param>
    /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
    public static HugeNumber Cube(HugeNumber value)
    {
        if (value.IsNaN()
            || value.IsInfinity()
            || value.Mantissa == 0)
        {
            return value;
        }
        return value * value * value;
    }

    /// <summary>
    /// A fast implementation of cubing (a number raised to the power of 3).
    /// </summary>
    /// <returns>This instance cubed (raised to the power of 3).</returns>
    public HugeNumber Cube() => Cube(this);

    /// <summary>
    /// Divides one <see cref="HugeNumber"/> value by another and returns the result.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The quotient of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see
    /// cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0 if its sign matches <paramref name="divisor"/>, and
    /// <see cref="NegativeZero"/> if they have opposite signs.
    /// </para>
    /// </remarks>
    public static HugeNumber Divide(HugeNumber dividend, HugeNumber divisor)
    {
        if (dividend.IsNaN() || divisor.IsNaN())
        {
            return NaN;
        }
        if (divisor.Mantissa == 0)
        {
            if (dividend.Mantissa == 0)
            {
                return NaN;
            }
            else if (dividend.Mantissa > 0)
            {
                return PositiveInfinity;
            }
            else
            {
                return NegativeInfinity;
            }
        }
        if (dividend.Mantissa == 0)
        {
            return dividend;
        }
        if (dividend.IsInfinity())
        {
            return (dividend.Mantissa < 0) == (divisor.Mantissa < 0)
                ? PositiveInfinity
                : NegativeInfinity;
        }
        if (divisor.IsInfinity())
        {
            return (dividend.Mantissa < 0) == (divisor.Mantissa < 0)
                ? NegativeZero
                : Zero;
        }

        if (dividend.Exponent - divisor.Exponent is < MIN_EXPONENT or > MAX_EXPONENT)
        {
            return (dividend.Mantissa < 0) == (divisor.Mantissa < 0)
                ? PositiveInfinity
                : NegativeInfinity;
        }

        if ((dividend.Denominator == 1 && dividend.Exponent < 0)
            || (divisor.Denominator == 1 && divisor.Exponent < 0)
            || long.MaxValue / dividend.Denominator < Math.Abs(divisor.Mantissa)
            || long.MaxValue / Math.Abs(dividend.Mantissa) < divisor.Denominator)
        {
            dividend = ToDenominator(dividend, 1);
            divisor = ToDenominator(divisor, 1);
        }
        else
        {
            var numerator = dividend.Mantissa * divisor.Denominator;
            var denominator = dividend.Denominator * (ulong)Math.Abs(divisor.Mantissa);
            var greatestCommonFactor = GreatestCommonFactor(numerator, denominator);
            if (greatestCommonFactor > 1)
            {
                numerator /= (long)greatestCommonFactor;
                denominator /= greatestCommonFactor;
            }
            if (denominator > ushort.MaxValue
                || numerator > MAX_MANTISSA)
            {
                dividend = ToDenominator(dividend, 1);
                divisor = ToDenominator(divisor, 1);
            }
            else
            {
                if (divisor.Mantissa < 0 && dividend.Mantissa < 0)
                {
                    numerator *= -1;
                }
                return new(numerator, (ushort)denominator, (short)(dividend.Exponent - divisor.Exponent));
            }
        }

        var dividendMantissa = (decimal)dividend.Mantissa;
        var dividendExponent = dividend.Exponent;
        var divisorMantissa = (decimal)divisor.Mantissa;
        var divisorExponent = divisor.Exponent;

        while (Math.Abs(divisorMantissa) < Math.Abs(dividendMantissa) / decimal.MaxValue)
        {
            if (dividendMantissa % 10 == 0)
            {
                dividendMantissa /= 10;
                dividendExponent++;
            }
            else
            {
                divisorMantissa *= 10;
                divisorExponent--;
            }
        }

        return new HugeNumber(dividendMantissa / divisorMantissa, dividendExponent - divisorExponent);
    }

    /// <summary>
    /// Divides one <see cref="HugeNumber"/> value by another and returns the result.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The quotient and remainder of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see
    /// cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static (HugeNumber Quotient, HugeNumber Remainder) DivRem(HugeNumber dividend, HugeNumber divisor)
    {
        var div = dividend / divisor;
        var result = div.Floor();
        return (result, div - result);
    }

    /// <summary>
    /// Returns the remainder resulting from the division of a specified number by another specified number.
    /// </summary>
    /// <param name="x">A dividend.</param>
    /// <param name="y">A divisor.</param>
    /// <returns>
    /// <para>
    /// A number equal to <paramref name="x"/> - (<paramref name="y"/> Q), where Q is the quotient of
    /// <paramref name="x"/> / <paramref name="y"/> rounded to the nearest integer (if
    /// <paramref name="x"/> / <paramref name="y"/> falls halfway between two integers, the even integer is returned).
    /// </para>
    /// <para>
    /// If <paramref name="x"/> - (<paramref name="y"/> Q) is zero, the value +0 is returned if
    /// <paramref name="x"/> is positive, or -0 if <paramref name="x"/> is negative.
    /// </para>
    /// <para>
    /// If <paramref name="y"/> = 0, <see cref="NaN"/> is returned.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// This operation complies with the remainder operation defined in Section 5.1 of ANSI/IEEE Std 754-1985;
    /// IEEE Standard for Binary Floating-Point Arithmetic; Institute of Electrical and Electronics Engineers, Inc; 1985.
    /// </para>
    /// <para>
    /// The IEEERemainder method is not the same as the remainder operator. Although both return the remainder after division,
    /// the formulas they use are different.
    /// </para>
    /// </remarks>
    public static HugeNumber IEEERemainder(HugeNumber x, HugeNumber y)
    {
        if (y.IsNaN())
        {
            return NaN;
        }
        var result = x - (y * (x / y).Round());
        if (result.Mantissa == 0)
        {
            return x.IsPositive()
                ? Zero
                : NegativeZero;
        }
        return result;
    }

    /// <summary>
    /// Divides one <see cref="HugeNumber"/> value by another and returns the remainder.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see
    /// cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is zero.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is zero.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber Mod(HugeNumber dividend, HugeNumber divisor)
    {
        if (dividend.IsNaN() || divisor.IsNaN())
        {
            return NaN;
        }
        if (divisor.Mantissa == 0)
        {
            return dividend.Mantissa == 0 ? NaN : Zero;
        }
        if (dividend.Mantissa == 0 || dividend.IsInfinity() || divisor.IsInfinity())
        {
            return Zero;
        }

        dividend = ToDenominator(dividend, 1);
        divisor = ToDenominator(divisor, 1);
        var dividendMantissa = dividend.Mantissa;
        var dividendExponent = dividend.Exponent;
        var significantDigits = dividend.MantissaDigits;
        while (significantDigits < MANTISSA_SIGNIFICANT_DIGITS
            && dividendExponent > MIN_EXPONENT
            && dividendMantissa % divisor.Mantissa != 0)
        {
            dividendMantissa *= 10;
            dividendExponent--;
            significantDigits++;
        }

        var quotient = dividendMantissa / divisor.Mantissa;
        var remainder = dividendMantissa % divisor.Mantissa;
        if (remainder >= dividendMantissa / 2)
        {
            quotient++;
        }

        if (dividendExponent - divisor.Exponent < 0)
        {
            return new HugeNumber(quotient, dividendExponent - divisor.Exponent);
        }

        return new HugeNumber(remainder, dividendExponent - divisor.Exponent);
    }

    /// <summary>
    /// Returns the product of two <see cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="left">The first number to multiply.</param>
    /// <param name="right">The second number to multiply.</param>
    /// <returns>
    /// The product of the <paramref name="left"/> and <paramref name="right"/> parameters.
    /// </returns>
    public static HugeNumber Multiply(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }
        if (left.Mantissa == 0 || right.Mantissa == 0)
        {
            return (left.Exponent < 0) != (right.Exponent < 0)
                ? NegativeZero
                : Zero;
        }
        if (left.IsInfinity()
            || right.IsInfinity()
            || left.Exponent + right.Exponent is < MIN_EXPONENT or > MAX_EXPONENT)
        {
            return (left.Mantissa < 0) == (right.Mantissa < 0)
                ? PositiveInfinity
                : NegativeInfinity;
        }

        if ((left.Denominator == 1 && left.Exponent < 0)
            || (right.Denominator == 1 && right.Exponent < 0)
            || long.MaxValue / Math.Abs(left.Mantissa) < Math.Abs(right.Mantissa))
        {
            left = ToDenominator(left, 1);
            right = ToDenominator(right, 1);
        }
        else
        {
            var numerator = left.Mantissa * right.Mantissa;
            var denominator = (ulong)left.Denominator * right.Denominator;
            var greatestCommonFactor = GreatestCommonFactor(numerator, denominator);
            if (greatestCommonFactor > 1)
            {
                numerator /= (long)greatestCommonFactor;
                denominator /= greatestCommonFactor;
            }
            if (denominator > ushort.MaxValue
                || numerator > MAX_MANTISSA)
            {
                left = ToDenominator(left, 1);
                right = ToDenominator(right, 1);
            }
            else
            {
                return new(numerator, (ushort)denominator, (short)(left.Exponent + right.Exponent));
            }
        }

        var leftMantissa = (decimal)left.Mantissa;
        var leftExponent = left.Exponent;
        var rightMantissa = (decimal)right.Mantissa;
        var rightExponent = right.Exponent;
        while (MAX_MANTISSA / Math.Abs(leftMantissa) < Math.Abs(rightMantissa))
        {
            if (leftExponent + rightExponent > MAX_EXPONENT - 2)
            {
                return Sign(left) == Sign(right)
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

        return new HugeNumber(leftMantissa * rightMantissa, leftExponent + rightExponent);
    }

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <param name="value">The value to square.</param>
    /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
    public static HugeNumber Square(HugeNumber value)
    {
        if (value.IsNaN()
            || value.Mantissa == 0)
        {
            return value;
        }
        if (value.IsPositiveInfinity())
        {
            return value;
        }
        if (value.IsNegativeInfinity())
        {
            return PositiveInfinity;
        }
        return value * value;
    }

    /// <summary>
    /// A fast implementation of squaring (a number raised to the power of 2).
    /// </summary>
    /// <returns>This instance squared (raised to the power of 2).</returns>
    public HugeNumber Square() => Square(this);
}
