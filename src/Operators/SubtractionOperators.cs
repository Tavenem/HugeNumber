namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from another <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="decimal"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, decimal right) => Subtract(left, (HugeNumber)right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(decimal left, HugeNumber right) => Subtract((HugeNumber)left, right);

    /// <summary>
    /// Subtracts a <see cref="double"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, double right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="double"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(double left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="long"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, long right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="long"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(long left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="ulong"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, ulong right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(ulong left, HugeNumber right) => Subtract(left, right);
}
