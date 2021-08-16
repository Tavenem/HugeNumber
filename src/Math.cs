namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Gets the absolute value of a <see cref="HugeNumber"/> object.
    /// </summary>
    /// <param name="value">A number.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    public static HugeNumber Abs(HugeNumber value)
        => new(Math.Abs(value.Mantissa), value.Exponent);

    /// <summary>
    /// Gets the absolute value of this instance.
    /// </summary>
    /// <returns>The absolute value of this instance.</returns>
    public HugeNumber Abs() => Abs(this);

    /// <summary>
    /// Adds two <see cref="HugeNumber"/> values and returns the result.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber Add(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }

        HugeNumber larger, smaller;
        if (left >= right)
        {
            larger = left;
            smaller = right;
        }
        else
        {
            larger = right;
            smaller = left;
        }

        if (larger.IsPositiveInfinity())
        {
            return smaller.IsNegativeInfinity()
                ? Zero
                : PositiveInfinity;
        }
        if (larger.IsNegativeInfinity())
        {
            return smaller.IsPositiveInfinity()
                ? Zero
                : NegativeInfinity;
        }
        if (smaller.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        if (smaller.IsNegativeInfinity())
        {
            return NegativeInfinity;
        }

        var smallerMantissa = smaller.Mantissa;
        var smallerMantissaDigits = smaller.MantissaDigits;
        var smallerExponent = smaller.Exponent;
        var largerMantissa = larger.Mantissa;
        var largerMantissaDigits = larger.MantissaDigits;
        var largerExponent = larger.Exponent;
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
            return larger;
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
            return larger;
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

        return new HugeNumber(largerMantissa + smallerMantissa, largerExponent);
    }

    /// <summary>
    /// Returns the next smallest value that compares less than <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>
    /// <para>
    /// The next smallest value that compares less than <paramref name="value"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NegativeInfinity"/> if <paramref name="value"/> equals <see cref="NegativeInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> equals <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public static HugeNumber BitDecrement(HugeNumber value) => value.IsPositiveInfinity()
        ? MaxValue
        : value - GetEpsilon(value);

    /// <summary>
    /// Returns the next smallest value that compares less than this instance.
    /// </summary>
    /// <returns>
    /// <para>
    /// The next smallest value that compares less than this instance.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="PositiveInfinity"/> if this instance equals <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if this instance equals <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public HugeNumber BitDecrement() => BitDecrement(this);

    /// <summary>
    /// Returns the next largest value that compares greater than <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>
    /// <para>
    /// The next largest value that compares greater than <paramref name="value"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="PositiveInfinity"/> if <paramref name="value"/> equals <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> equals <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public static HugeNumber BitIncrement(HugeNumber value) => value.IsNegativeInfinity()
        ? MinValue
        : value + GetEpsilon(value);

    /// <summary>
    /// Returns the next largest value that compares greater than this instance.
    /// </summary>
    /// <returns>
    /// <para>
    /// The next largest value that compares greater than this instance.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="PositiveInfinity"/> if this instance equals <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if this instance equals <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public HugeNumber BitIncrement() => BitIncrement(this);

    /// <summary>
    /// Gets a value whose absolute value equals that of the <paramref name="first"/> given
    /// input, but whose sign is the same as the <paramref name="second"/> given input.
    /// </summary>
    /// <param name="first">The value whose absolute value will be copied.</param>
    /// <param name="second">The value whose sign will be copied.</param>
    /// <returns>A value whose absolute value equals that of the <paramref name="first"/> given
    /// input, but whose sign is the same as the <paramref name="second"/> given
    /// input.</returns>
    public static HugeNumber CopySign(HugeNumber first, HugeNumber second)
    {
        if (second.IsNegative())
        {
            if (first.IsNegative())
            {
                return first;
            }
            else
            {
                return first.Negate();
            }
        }
        else if (first.IsNegative())
        {
            return first.Negate();
        }
        else
        {
            return first;
        }
    }

    /// <summary>
    /// Gets a value whose absolute value equals that of this instance, but whose sign is the
    /// same as the given <paramref name="value"/>.
    /// </summary>
    /// <param name="value">The value whose sign will be copied.</param>
    /// <returns>A value whose absolute value equals that of this instance, but whose sign is the
    /// same as the given <paramref name="value"/>.</returns>
    public HugeNumber CopySign(HugeNumber value) => CopySign(this, value);

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
    /// Returns the cube root of a specified number.
    /// </summary>
    /// <param name="value">The number whose cube root is to be found.</param>
    /// <returns>
    /// The cube root of <paramref name="value"/>, or <see cref="NaN"/> if <paramref
    /// name="value"/> is <see cref="NaN"/>.
    /// </returns>
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

        return Exp(Third * Log(value));
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
    /// Decrements the given <paramref name="value"/> by no less than 1, and at least the
    /// minimum amount required to produce a distinct value.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>The given <paramref name="value"/> decremented by no less than 1, and at least
    /// enough to produce a value distinct from the given <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
    /// the mantissa, a simple <c>x - 1</c> or <c>x--</c> operation may not produce a distinct
    /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
    /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
    /// </para>
    /// <para>
    /// For example, <c>new Number(1, 200) - 1</c> will not result in a different value than
    /// <c>new Number(1, 200)</c>. In this case, the minimum representable value smaller than
    /// <c>new Number(1, 200)</c> is <c>new Number(9.99999999999999999, 199)</c> (or
    /// equivalently, <c>new Number(999999999999999999, 182)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Decrement(HugeNumber value) => value.IsPositiveInfinity()
        ? MaxValue
        : value - Max(One, GetEpsilon(value));

    /// <summary>
    /// Decrements this instance by no less than 1, and at least the minimum amount required to
    /// produce a distinct value.
    /// </summary>
    /// <returns>This instance decremented by no less than 1, and at least enough to produce a
    /// value distinct from this instance.</returns>
    /// <remarks>
    /// <para>
    /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
    /// the mantissa, a simple <c>x - 1</c> or <c>x--</c> operation may not produce a distinct
    /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
    /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
    /// </para>
    /// <para>
    /// For example, <c>new Number(1, 200) - 1</c> will not result in a different value than
    /// <c>new Number(1, 200)</c>. In this case, the minimum representable value smaller than
    /// <c>new Number(1, 200)</c> is <c>new Number(9.99999999999999999, 199)</c> (or
    /// equivalently, <c>new Number(999999999999999999, 182)</c>).
    /// </para>
    /// </remarks>
    public HugeNumber Decrement() => Decrement(this);

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
    /// finite value, the result is 0.
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
            else if (dividend.IsPositive())
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
            return Zero;
        }
        if (dividend.IsInfinity())
        {
            return Sign(dividend) == Sign(divisor)
                ? PositiveInfinity
                : NegativeInfinity;
        }
        if (divisor.IsInfinity())
        {
            return Zero;
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
        else
        {
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
        if (comparison >= 0)
        {
            smallerMantissa = z.Mantissa;
            smallerMantissaDigits = z.MantissaDigits;
            smallerExponent = z.Exponent;
            largerMantissa = productMantissa;
            largerMantissaDigits = productMantissaDigits;
            largerExponent = productExponent;
        }
        else
        {
            smallerMantissa = productMantissa;
            smallerMantissaDigits = productMantissaDigits;
            smallerExponent = productExponent;
            largerMantissa = z.Mantissa;
            largerMantissaDigits = z.MantissaDigits;
            largerExponent = z.Exponent;
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
            return new HugeNumber(largerMantissa, largerExponent);
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
            return new HugeNumber(largerMantissa, largerExponent);
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

        return new HugeNumber(largerMantissa + smallerMantissa, largerExponent);
    }

    /// <summary>
    /// Gets the smallest value which will produce a distinct value from the given <paramref
    /// name="value"/> when added to it or subtracted from it.
    /// </summary>
    /// <returns>The smallest value which will produce a distinct value from the given <paramref
    /// name="value"/> when added to it or subtracted from it.</returns>
    public static HugeNumber GetEpsilon(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.Mantissa == 0)
        {
            return Epsilon;
        }
        if (value.IsInfinity())
        {
            return PositiveInfinity;
        }
        var mantissaDigits = value.MantissaDigits;
        var exp = value.Exponent;
        if (mantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
        {
            exp -= (short)(MANTISSA_SIGNIFICANT_DIGITS - mantissaDigits);
        }
        return new HugeNumber(1, exp);
    }

    /// <summary>
    /// Gets the smallest value which will produce a distinct value from this instance when
    /// added to it or subtracted from it.
    /// </summary>
    /// <returns>The smallest value which will produce a distinct value from this instance when
    /// added to it or subtracted from it.</returns>
    public HugeNumber GetEpsilon() => GetEpsilon(this);

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
    /// Returns the base 2 integer logarithm of a specified number.
    /// </summary>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>
    /// <para>
    /// One of the values in the following table.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The base 2 integer log of <paramref name="x"/>; that is, (<typeparamref name="TInteger"/>)log2(<paramref name="x"/>).
    /// </term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The minimum value of <typeparamref name="TInteger"/></term>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="NaN"/> or <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/></term>
    /// <term>The maximum value of <typeparamref name="TInteger"/></term>
    /// </item>
    /// </list>
    /// </returns>
    public static TInteger ILogB<TInteger>(HugeNumber x)
        where TInteger : IBinaryInteger<TInteger>
    {
        if (x.Mantissa == 0)
        {
            if (typeof(TInteger) == typeof(byte))
            {
                return (TInteger)(object)byte.MinValue;
            }
            else if (typeof(TInteger) == typeof(short))
            {
                return (TInteger)(object)short.MinValue;
            }
            else if (typeof(TInteger) == typeof(int))
            {
                return (TInteger)(object)int.MinValue;
            }
            else if (typeof(TInteger) == typeof(long))
            {
                return (TInteger)(object)long.MinValue;
            }
            else if (typeof(TInteger) == typeof(nint))
            {
                return (TInteger)(object)nint.MinValue;
            }
            else if (typeof(TInteger) == typeof(sbyte))
            {
                return (TInteger)(object)sbyte.MinValue;
            }
            else if (typeof(TInteger) == typeof(ushort))
            {
                return (TInteger)(object)ushort.MinValue;
            }
            else if (typeof(TInteger) == typeof(uint))
            {
                return (TInteger)(object)uint.MinValue;
            }
            else if (typeof(TInteger) == typeof(ulong))
            {
                return (TInteger)(object)ulong.MinValue;
            }
            else if (typeof(TInteger) == typeof(nuint))
            {
                return (TInteger)(object)nuint.MinValue;
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(TInteger).Name} is unsupported.");
            }
        }
        if (x.IsNaN() || x.IsInfinity())
        {
            if (typeof(TInteger) == typeof(byte))
            {
                return (TInteger)(object)byte.MaxValue;
            }
            else if (typeof(TInteger) == typeof(short))
            {
                return (TInteger)(object)short.MaxValue;
            }
            else if (typeof(TInteger) == typeof(int))
            {
                return (TInteger)(object)int.MaxValue;
            }
            else if (typeof(TInteger) == typeof(long))
            {
                return (TInteger)(object)long.MaxValue;
            }
            else if (typeof(TInteger) == typeof(nint))
            {
                return (TInteger)(object)nint.MaxValue;
            }
            else if (typeof(TInteger) == typeof(sbyte))
            {
                return (TInteger)(object)sbyte.MaxValue;
            }
            else if (typeof(TInteger) == typeof(ushort))
            {
                return (TInteger)(object)ushort.MaxValue;
            }
            else if (typeof(TInteger) == typeof(uint))
            {
                return (TInteger)(object)uint.MaxValue;
            }
            else if (typeof(TInteger) == typeof(ulong))
            {
                return (TInteger)(object)ulong.MaxValue;
            }
            else if (typeof(TInteger) == typeof(nuint))
            {
                return (TInteger)(object)nuint.MaxValue;
            }
            else
            {
                throw new NotSupportedException($"Type {typeof(TInteger).Name} is unsupported.");
            }
        }
        var value = Math.ILogB((double)x);
        if (typeof(TInteger) == typeof(byte))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(short))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(int))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(long))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(nint))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(sbyte))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(ushort))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(uint))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(ulong))
        {
            return (TInteger)(object)value;
        }
        else if (typeof(TInteger) == typeof(nuint))
        {
            return (TInteger)(object)value;
        }
        else
        {
            throw new NotSupportedException($"Type {typeof(TInteger).Name} is unsupported.");
        }
    }

    /// <summary>
    /// Increments the given <paramref name="value"/> by no less than 1, and at least the
    /// minimum amount required to produce a distinct value.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>The given <paramref name="value"/> incremented by no less than 1, and at least
    /// enough to produce a value distinct from the given <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
    /// the mantissa, a simple <c>x + 1</c> or <c>x++</c> operation may not produce a distinct
    /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
    /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
    /// </para>
    /// <para>
    /// For example, <c>new Number(1, 200) + 1</c> will not result in a different value than
    /// <c>new Number(1, 200)</c>. In this case, the minimum representable value larger than
    /// <c>new Number(1, 200)</c> is <c>new Number(1.00000000000000001, 200)</c> (or
    /// equivalently, <c>new Number(100000000000000001, 183)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Increment(HugeNumber value) => value.IsNegativeInfinity()
        ? MinValue
        : value + Max(One, GetEpsilon(value));

    /// <summary>
    /// Increments this instance by no less than 1, and at least the minimum amount required to
    /// produce a distinct value.
    /// </summary>
    /// <returns>This instance incremented by no less than 1, and at least enough to produce a
    /// value distinct from this instance.</returns>
    /// <remarks>
    /// <para>
    /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
    /// the mantissa, a simple <c>x + 1</c> or <c>x++</c> operation may not produce a distinct
    /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
    /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
    /// </para>
    /// <para>
    /// For example, <c>new Number(1, 200) + 1</c> will not result in a different value than
    /// <c>new Number(1, 200)</c>. In this case, the minimum representable value larger than
    /// <c>new Number(1, 200)</c> is <c>new Number(1.00000000000000001, 200)</c> (or
    /// equivalently, <c>new Number(100000000000000001, 183)</c>).
    /// </para>
    /// </remarks>
    public HugeNumber Increment() => Increment(this);

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
    /// cref="NaN"/> if the weight cannot be computed for the given
    /// parameters.</returns>
    /// <remarks>
    /// <see cref="NaN"/> will be returned if the given values are nearly
    /// equal, but the given result is not also nearly equal to them, since the calculation in
    /// that case would require a division by zero.
    /// </remarks>
    public static HugeNumber InverseLerp(HugeNumber first, HugeNumber second, HugeNumber result)
    {
        var difference = second - first;
        if (difference.Mantissa == 0)
        {
            if (result == first)
            {
                return new HugeNumber(5, -1);
            }
            else
            {
                return NaN;
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between this value and the <paramref name="other"/> value.
    /// </summary>
    /// <param name="other">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between this instance
    /// and <paramref name="other"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between this instance and <paramref name="other"/>; or <see cref="NaN"/>
    /// if the weight cannot be computed for the given parameters.</returns>
    /// <remarks>
    /// <see cref="NaN"/> will be returned if the given values are nearly equal, but the given
    /// result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public HugeNumber InverseLerp(HugeNumber other, HugeNumber result) => InverseLerp(this, other, result);

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public static HugeNumber Lerp(HugeNumber first, HugeNumber second, HugeNumber amount)
        => first + ((second - first) * amount);

    /// <summary>
    /// Linearly interpolates between this value and another based on the given weighting.
    /// </summary>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from this instance as <paramref name="second"/>,
    /// rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as this
    /// instance, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public HugeNumber Lerp(HugeNumber second, HugeNumber amount) => Lerp(this, second, amount);

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
            return Zero;
        }
        if (left.IsInfinity() || right.IsInfinity())
        {
            return Sign(left) == Sign(right)
                ? PositiveInfinity
                : NegativeInfinity;
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
    /// Negates a specified <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>
    /// The result of the <paramref name="value"/> parameter multiplied by negative one (-1).
    /// </returns>
    public static HugeNumber Negate(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.IsPositiveInfinity())
        {
            return NegativeInfinity;
        }
        if (value.IsNegativeInfinity())
        {
            return PositiveInfinity;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }
        return new HugeNumber(-value.Mantissa, value.Exponent);
    }

    /// <summary>
    /// Negates this instance.
    /// </summary>
    /// <returns>
    /// The result of this instance multiplied by negative one (-1).
    /// </returns>
    public HugeNumber Negate() => Negate(this);

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

        return Exp(Half * Log(value));
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

    /// <summary>
    /// Subtracts one <see cref="HugeNumber"/> value from another and returns the result.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber Subtract(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }
        if (left.IsPositiveInfinity())
        {
            return right.IsPositiveInfinity()
                ? Zero
                : PositiveInfinity;
        }
        if (left.IsNegativeInfinity())
        {
            return right.IsNegativeInfinity()
                ? Zero
                : NegativeInfinity;
        }
        if (right.IsPositiveInfinity())
        {
            return NegativeInfinity;
        }
        if (right.IsNegativeInfinity())
        {
            return PositiveInfinity;
        }

        // Shift the left value into the exponent base of the right, even if that
        // extinguishes all precision.
        var leftMantissa = left.Mantissa;
        var leftMantissaDigits = left.MantissaDigits;
        var leftExponent = left.Exponent;
        var rightMantissa = right.Mantissa;
        var rightMantissaDigits = right.MantissaDigits;
        var rightExponent = right.Exponent;
        while (leftMantissa != 0 && leftExponent < rightExponent)
        {
            if (rightMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS
                && rightExponent > MIN_EXPONENT)
            {
                rightMantissa *= 10;
                rightMantissaDigits++;
                rightExponent--;
            }
            else
            {
                leftMantissa /= 10;
                leftMantissaDigits--;
                leftExponent++;
            }
        }
        if (leftExponent < rightExponent)
        {
            return -right;
        }
        while (leftExponent > rightExponent && leftMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
        {
            if (rightMantissa % 10 == 0)
            {
                rightMantissa /= 10;
                rightMantissaDigits--;
                rightExponent++;
            }
            else
            {
                leftMantissa *= 10;
                leftMantissaDigits++;
                leftExponent--;
            }
        }
        // If the left value could not be shifted to the base of right, shift the right value to
        // the base of left.
        if (leftExponent > rightExponent)
        {
            while (rightMantissa != 0 && rightExponent < leftExponent)
            {
                if (leftMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
                {
                    leftMantissa *= 10;
                    leftMantissaDigits++;
                    leftExponent--;
                }
                else
                {
                    rightMantissa /= 10;
                    rightMantissaDigits--;
                    rightExponent++;
                }
            }
            if (rightExponent < leftExponent)
            {
                return left;
            }
        }

        return new HugeNumber(leftMantissa - rightMantissa, leftExponent);
    }
}
