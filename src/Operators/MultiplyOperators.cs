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

    /// <summary>
    /// Multiplies two specified <see cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, decimal right) => Multiply(left, (HugeNumber)right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(decimal left, HugeNumber right) => Multiply((HugeNumber)left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="double"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, double right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="double"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(double left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="long"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, long right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="long"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(long left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, ulong right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(ulong left, HugeNumber right) => Multiply(left, right);
}
