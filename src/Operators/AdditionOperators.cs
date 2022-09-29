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
    /// Adds the values of two specified <see cref="HugeNumber"/> objects.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, decimal right) => Add(left, (HugeNumber)right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(decimal left, HugeNumber right) => Add((HugeNumber)left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, double right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(double left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="long"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, long right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="long"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(long left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="ulong"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, ulong right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="ulong"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(ulong left, HugeNumber right) => Add(left, right);
}
