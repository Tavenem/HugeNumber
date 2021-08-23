namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
            return NegativeInfinity;
        }
        if (smaller.IsNegativeInfinity())
        {
            return NegativeInfinity;
        }

        if (smaller.Denominator > 1
            || larger.Denominator > 1)
        {
            if ((smaller.Denominator == 1 && smaller.Exponent < 0)
                || (larger.Denominator == 1 && larger.Exponent < 0))
            {
                smaller = ToDenominator(smaller, 1);
                larger = ToDenominator(larger, 1);
            }
            else
            {
                var leastCommonMultiple = LeastCommonMultiple(smaller.Denominator, larger.Denominator);
                smaller = ToDenominator(smaller, leastCommonMultiple ?? 1);
                larger = ToDenominator(larger, leastCommonMultiple ?? 1);
                if (smaller.Denominator != larger.Denominator)
                {
                    smaller = ToDenominator(smaller, 1);
                    larger = ToDenominator(larger, 1);
                }
            }
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

        return ReduceFraction(new HugeNumber(
            smallerMantissa + largerMantissa,
            larger.Denominator,
            largerExponent));
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

        if (left.Denominator > 1
            || right.Denominator > 1)
        {
            if ((left.Denominator == 1 && left.Exponent < 0)
                || (right.Denominator == 1 && right.Exponent < 0))
            {
                left = ToDenominator(left, 1);
                right = ToDenominator(right, 1);
            }
            else
            {
                var leastCommonMultiple = LeastCommonMultiple(left.Denominator, right.Denominator);
                left = ToDenominator(left, leastCommonMultiple ?? 1);
                right = ToDenominator(right, leastCommonMultiple ?? 1);
                if (left.Denominator != right.Denominator)
                {
                    left = ToDenominator(left, 1);
                    right = ToDenominator(right, 1);
                }
            }
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

        return ReduceFraction(new HugeNumber(
            leftMantissa - rightMantissa,
            left.Denominator,
            leftExponent));
    }
}
