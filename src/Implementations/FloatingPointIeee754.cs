namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Computes the arc-tangent for the quotient of two values.
    /// </summary>
    /// <param name="y">The y-coordinate of a point.</param>
    /// <param name="x">The x-coordinate of a point.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = <paramref name="y"/> /
    /// <paramref name="x"/>, where (<paramref name="x"/>, <paramref name="y"/>) is a point in the
    /// Cartesian plane. Observe the following:
    /// </para>
    /// <list type="bullet">
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 1, 0 &lt; θ &lt;
    /// π/2.</description>
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 2, π/2 &lt; θ ≤
    /// π.</description>
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 3, -π &lt; θ &lt;
    /// -π/2.</description>
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 4, -π/2 &lt; θ
    /// &lt; 0.</description>
    /// </list>
    /// <para>
    /// For points on the boundaries of the quadrants, the return value is the following:
    /// </para>
    /// <list type="bullet">
    /// <description>If y is 0 and x is not negative, θ = 0.</description>
    /// <description>If y is 0 and x is negative, θ = π.</description>
    /// <description>If y is positive and x is 0, θ = π/2.</description>
    /// <description>If y is negative and x is 0, θ = -π/2.</description>
    /// <description>If y is 0 and x is 0, θ = 0.</description>
    /// </list>
    /// <para>
    /// The results when either value is infinity vary.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Case</term>
    /// <description>Result</description>
    /// </listheader>
    /// <item>
    /// <term>x is finite, y is <see cref="PositiveInfinity"/></term>
    /// <description>0</description>
    /// </item>
    /// <item>
    /// <term>x is finite, y is <see cref="NegativeInfinity"/></term>
    /// <description>π</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="PositiveInfinity"/>, y is finite and non-zero</term>
    /// <description>π/2</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="NegativeInfinity"/>, y is finite and non-zero</term>
    /// <description>-π/2</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="PositiveInfinity"/>, y is <see cref="PositiveInfinity"/></term>
    /// <description>π/4</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="NegativeInfinity"/>, y is <see cref="PositiveInfinity"/></term>
    /// <description>-π/4</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="PositiveInfinity"/>, y is <see cref="NegativeInfinity"/></term>
    /// <description>3π/4</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="NegativeInfinity"/>, y is <see cref="NegativeInfinity"/></term>
    /// <description>-3π/4</description>
    /// </item>
    /// </list>
    /// <para>
    /// If <paramref name="x"/> or <paramref name="y"/> is <see cref="NaN"/>, or if <paramref
    /// name="x"/> and <paramref name="y"/> are either <see cref="PositiveInfinity"/> or <see
    /// cref="NegativeInfinity"/>, the method returns <see cref="NaN"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The return value is the angle in the Cartesian plane formed by the x-axis, and a vector
    /// starting from the origin, (0,0), and terminating at the point, (x,y).
    /// </para>
    /// <para>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform boxing conversions and use the native <see cref="Math"/> operation instead (e.g.
    /// <c>(Number)Math.Atan2((double)y, (double)x)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Atan2(HugeNumber y, HugeNumber x)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return y >= Zero ? Zero : Pi;
        }
        if (y.Mantissa == 0)
        {
            return x >= Zero ? HugeNumberConstants.HalfPi : -HugeNumberConstants.HalfPi;
        }
        if (y.IsPositiveInfinity())
        {
            if (x.IsPositiveInfinity())
            {
                return HugeNumberConstants.QuarterPi;
            }
            else if (x.IsNegativeInfinity())
            {
                return -HugeNumberConstants.QuarterPi;
            }
            else
            {
                return Zero;
            }
        }
        if (y.IsNegativeInfinity())
        {
            if (x.IsPositiveInfinity())
            {
                return HugeNumberConstants.ThreeQuartersPi;
            }
            else if (x.IsNegativeInfinity())
            {
                return -HugeNumberConstants.ThreeQuartersPi;
            }
            else if (x >= Zero)
            {
                return Pi;
            }
            else
            {
                return -Pi;
            }
        }
        if (x.IsPositiveInfinity())
        {
            return HugeNumberConstants.HalfPi;
        }
        if (x.IsNegativeInfinity())
        {
            return -HugeNumberConstants.HalfPi;
        }
        if (x > Zero)
        {
            return Atan(y / x);
        }
        if (y > Zero)
        {
            return Atan(y / x) + Pi;
        }
        return Atan(y / x) - Pi;
    }

    /// <summary>
    /// Computes the arc-tangent for the quotient of two values and divides the result by <c>π</c>.
    /// </summary>
    /// <param name="y">The y-coordinate of a point.</param>
    /// <param name="x">The x-coordinate of a point.</param>
    /// <returns>
    /// <para>
    /// The arc-tangent of y divided-by x, divided by <c>π</c>.
    /// </para>
    /// <para>
    /// See <see cref="Atan2(HugeNumber, HugeNumber)"/> for the expected results of specific values.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// This computes <c>arctan(y / x) / π</c> in the interval <c>[-1, +1]</c>.
    /// </para>
    /// <para>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform boxing conversions and use the native <see cref="double"/> operation instead (e.g.
    /// <c>(Number)double.Atan2Pi((double)y, (double)x)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Atan2Pi(HugeNumber y, HugeNumber x)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return y >= Zero ? x : One; // returning x preserves negative zero.
        }
        if (y.Mantissa == 0)
        {
            return x >= Zero ? HugeNumberConstants.Half : -HugeNumberConstants.Half;
        }
        if (y.IsPositiveInfinity())
        {
            if (x.IsPositiveInfinity())
            {
                return HugeNumberConstants.Fourth;
            }
            else if (x.IsNegativeInfinity())
            {
                return -HugeNumberConstants.Fourth;
            }
            else if (x.IsNegative())
            {
                return NegativeZero;
            }
            else
            {
                return Zero;
            }
        }
        if (y.IsNegativeInfinity())
        {
            if (x.IsPositiveInfinity())
            {
                return HugeNumberConstants.ThreeFourths;
            }
            else if (x.IsNegativeInfinity())
            {
                return -HugeNumberConstants.ThreeFourths;
            }
            else if (x >= Zero)
            {
                return One;
            }
            else
            {
                return NegativeOne;
            }
        }
        if (x.IsPositiveInfinity())
        {
            return HugeNumberConstants.Half;
        }
        if (x.IsNegativeInfinity())
        {
            return -HugeNumberConstants.Half;
        }
        if (x > Zero)
        {
            return AtanPi(y / x);
        }
        if (y > Zero)
        {
            return AtanPi(y / x) + One;
        }
        return AtanPi(y / x) - One;
    }

    /// <summary>
    /// Decrements a value to the smallest value that compares less than a given value.
    /// </summary>
    /// <param name="x">The value to be bitwise decremented.</param>
    /// <returns>
    /// <para>
    /// The smallest value that compares less than <paramref name="x"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NegativeInfinity"/> if <paramref name="x"/> is <see cref="NegativeInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="x"/> is <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public static HugeNumber BitDecrement(HugeNumber x) => x.IsPositiveInfinity()
        ? MaxValue
        : x - GetEpsilon(x);

    /// <summary>
    /// Decrements a value to the smallest value that compares less than this value.
    /// </summary>
    /// <returns>
    /// <para>
    /// The smallest value that compares less than this.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NegativeInfinity"/> if this is <see cref="NegativeInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if this is <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public HugeNumber BitDecrement() => BitDecrement(this);

    /// <summary>
    /// Increments a value to the smallest value that compares greater than a given value.
    /// </summary>
    /// <param name="x">The value to be bitwise incremented.</param>
    /// <returns>
    /// <para>
    /// The smallest value that compares greater than <paramref name="x"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="PositiveInfinity"/> if <paramref name="x"/> is <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="x"/> is <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public static HugeNumber BitIncrement(HugeNumber x) => x.IsNegativeInfinity()
        ? MinValue
        : x + GetEpsilon(x);

    /// <summary>
    /// Increments a value to the smallest value that compares greater than this value.
    /// </summary>
    /// <returns>
    /// <para>
    /// The smallest value that compares greater than this value.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="PositiveInfinity"/> if this value is <see cref="PositiveInfinity"/>.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if this value is <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public HugeNumber BitIncrement() => BitIncrement(this);

    /// <summary>
    /// Computes the fused multiply-add of three values.
    /// </summary>
    /// <param name="left">The value which <paramref name="right"/> multiplies.</param>
    /// <param name="right">The value which multiplies <paramref name="left"/>.</param>
    /// <param name="addend">
    /// The value that is added to the product of <paramref name="left"/> and <paramref name="right"/>.
    /// </param>
    /// <returns>
    /// The result of <paramref name="left"/> times <paramref name="right"/> plus <paramref
    /// name="addend"/> computed as one ternary operation.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This computes <c>(x * y)</c> as if to infinite precision, adds <paramref name="addend"/>
    /// to that result as if to infinite precision, and finally rounds to the nearest representable value.
    /// </para>
    /// <para>
    /// This differs from the non-fused sequence which would compute <c>(x * y)</c> as if to infinite precision,
    /// round the result to the nearest representable value, add <paramref name="addend"/> to the rounded result
    /// as if to infinite precision, and finally round to the nearest representable value.
    /// </para>
    /// </remarks>
    public static HugeNumber FusedMultiplyAdd(HugeNumber left, HugeNumber right, HugeNumber addend)
    {
        if (left.IsNaN() || right.IsNaN() || addend.IsNaN())
        {
            return NaN;
        }
        if (left.Mantissa == 0 || right.Mantissa == 0)
        {
            return addend;
        }
        if (left.IsInfinity() || right.IsInfinity())
        {
            if (Sign(left) == Sign(right))
            {
                return addend.IsNegativeInfinity()
                    ? Zero
                    : PositiveInfinity;
            }
            return addend.IsPositiveInfinity()
                ? Zero
                : NegativeInfinity;
        }
        if (addend.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        if (addend.IsNegativeInfinity())
        {
            return NegativeInfinity;
        }

        if (long.MaxValue / Math.Abs(left.Mantissa) < Math.Abs(right.Mantissa))
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
                return new HugeNumber(numerator, (ushort)denominator, (short)(left.Exponent + right.Exponent))
                    + addend;
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

        var product = (HugeNumber)(leftMantissa * rightMantissa);
        var productExponent = (short)(leftExponent + rightExponent);

        var productMantissa = product.Mantissa;
        var productMantissaDigits = product.MantissaDigits;
        productExponent = (short)(productExponent + product.Exponent);
        Reduce(ref productMantissa, ref productExponent, ref productMantissaDigits);

        int comparison;
        // When either value is zero, exponents can be disregarded; only the sign of the
        // mantissa matters for the purpose of comparison.
        if (productMantissa == 0 || addend.Mantissa == 0)
        {
            comparison = productMantissa.CompareTo(addend.Mantissa);
        }
        else
        {
            var adjustedExponent = productExponent + (productMantissaDigits - 1);
            var otherAdjustedExponent = addend.Exponent + (addend.MantissaDigits - 1);

            // simple comparison between equal exponents
            if (adjustedExponent == otherAdjustedExponent)
            {
                var adjustedMantissa = productMantissa * Math.Pow(10, productExponent);
                var otherAdjustedMantissa = addend.Mantissa * Math.Pow(10, addend.Exponent);
                comparison = adjustedMantissa.CompareTo(otherAdjustedMantissa);
            }
            // simple cases with opposite signs
            else if (productMantissa < 0 && addend.Mantissa > 0)
            {
                comparison = -1;
            }
            else if (addend.Mantissa < 0 && productMantissa > 0)
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
            smallerMantissa = addend.Mantissa;
            smallerMantissaDigits = addend.MantissaDigits;
            smallerExponent = addend.Exponent;
            smallerDenominator = addend.Denominator;
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
            largerMantissa = addend.Mantissa;
            largerMantissaDigits = addend.MantissaDigits;
            largerExponent = addend.Exponent;
            largerDenominator = addend.Denominator;
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
    /// Computes the remainder of two values as specified by IEEE 754.
    /// </summary>
    /// <param name="left">The value which <paramref name="right"/> divides.</param>
    /// <param name="right">The value which divides <paramref name="left"/>.</param>
    /// <returns>
    /// <para>
    /// A number equal to <paramref name="left"/> - (<paramref name="right"/> Q), where Q is the
    /// quotient of <paramref name="left"/> / <paramref name="right"/> rounded to the nearest
    /// integer (if <paramref name="left"/> / <paramref name="right"/> falls halfway between two
    /// integers, the even integer is returned).
    /// </para>
    /// <para>
    /// If <paramref name="left"/> - (<paramref name="right"/> Q) is zero, the value +0 is returned
    /// if <paramref name="left"/> is positive, or -0 if <paramref name="left"/> is negative.
    /// </para>
    /// <para>
    /// If <paramref name="right"/> = 0, <see cref="NaN"/> is returned.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// This operation complies with the remainder operation defined in Section 5.1 of ANSI/IEEE Std
    /// 754-1985; IEEE Standard for Binary Floating-Point Arithmetic; Institute of Electrical and
    /// Electronics Engineers, Inc; 1985.
    /// </para>
    /// <para>
    /// The IEEERemainder method is not the same as the remainder operator. Although both return the
    /// remainder after division, the formulas they use are different.
    /// </para>
    /// </remarks>
    public static HugeNumber Ieee754Remainder(HugeNumber left, HugeNumber right)
    {
        if (right.IsNaN())
        {
            return NaN;
        }
        var result = left - (right * (left / right).Round());
        if (result.Mantissa == 0)
        {
            return left.IsPositive()
                ? Zero
                : NegativeZero;
        }
        return result;
    }

    /// <summary>
    /// Computes the integer (base 2) logarithm of a value.
    /// </summary>
    /// <param name="x">The value whose integer logarithm is to be computed.</param>
    /// <returns>
    /// <para>
    /// The integer (base 2) logarithm of <paramref name="x"/>.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The base 2 integer log of <paramref name="x"/>; that is, (int)log2(<paramref name="x"/>).
    /// </term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="int.MinValue"/></term>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="NaN"/> or <see cref="PositiveInfinity"/> or <see
    /// cref="NegativeInfinity"/></term>
    /// <term><see cref="int.MaxValue"/></term>
    /// </item>
    /// </list>
    /// </returns>
    public static int ILogB(HugeNumber x)
    {
        if (x.Mantissa == 0)
        {
            return int.MinValue;
        }
        if (x.IsNaN() || x.IsInfinity())
        {
            return int.MaxValue;
        }
        return Math.ILogB((double)x);
    }

    /// <summary>
    /// Computes the reciprocal of the given value.
    /// </summary>
    /// <param name="x">The value whose reciprocal is computed.</param>
    /// <returns>
    /// <para>
    /// The reciprocal of the given value.
    /// </para>
    /// <para>
    /// If the value is <see cref="NaN"/>, returns <see cref="NaN"/>.
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
    public static HugeNumber Reciprocal(HugeNumber x)
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
    /// Computes the reciprocal of this value.
    /// </summary>
    /// <returns>
    /// <para>
    /// The reciprocal of this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NaN"/>, returns <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If this value is <see cref="PositiveInfinity"/>, <see cref="Zero"/> is returned.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, <see cref="NegativeZero"/> is returned.
    /// </para>
    /// <para>
    /// If this value is <see cref="Zero"/> or <see cref="NegativeZero"/>, <see cref="NaN"/> is
    /// returned.
    /// </para>
    /// <para>
    /// If this value represents an integer or a rational fraction, and the numerator is &lt;=
    /// <see cref="ushort.MaxValue"/>, the result is a rational fraction with the numerator and the
    /// denominator swapped, and the exponent's sign reversed.
    /// </para>
    /// <para>
    /// In all other cases, the result of one divided by this value is returned.
    /// </para>
    /// </returns>
    public HugeNumber Reciprocal() => Reciprocal(this);

    /// <summary>Computes an estimate of the reciprocal of a value.</summary>
    /// <param name="x">The value whose estimate of the reciprocal is to be computed.</param>
    /// <returns>An estimate of the reciprocal of <paramref name="x" />.</returns>
    public static HugeNumber ReciprocalEstimate(HugeNumber x) => Reciprocal(x);

    /// <summary>
    /// Computes the product of a value and its base-radix raised to the specified power.
    /// </summary>
    /// <param name="x">
    /// The value which base-radix raised to the power of <paramref name="n"/> multiplies.
    /// </param>
    /// <param name="n">
    /// The value to which base-radix is raised before multipliying <paramref name="x"/>.
    /// </param>
    /// <returns>
    /// The product of <paramref name="x"/> and base-radix raised to the power of <paramref
    /// name="n"/>.
    /// </returns>
    /// <remarks>
    /// The <see cref="Radix"/> of <see cref="HugeNumber"/> is 10 (unlike most <see
    /// cref="IFloatingPointIeee754{TSelf}"/> types, which use 2). Therefore, this method will
    /// produce very different results from the equivalent method implementation on, e.g. <see
    /// cref="double"/>.
    /// </remarks>
    public static HugeNumber ScaleB(HugeNumber x, int n)
        => x * Pow(HugeNumberConstants.Ten, n);
}
