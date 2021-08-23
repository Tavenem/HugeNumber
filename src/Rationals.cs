namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Determines the greatest common factor of the two given values.
    /// </summary>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <returns>The greatest common factor of the two values.</returns>
    public static ushort GreatestCommonFactor(ushort value1, ushort value2)
    {
        while (value2 != 0)
        {
            var tmp = value2;
            value2 = (ushort)(value1 % value2);
            value1 = tmp;
        }
        return value1;
    }

    /// <summary>
    /// Determines the greatest common factor of the two given values.
    /// </summary>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <returns>The greatest common factor of the two values.</returns>
    public static ulong GreatestCommonFactor(ulong value1, ulong value2)
    {
        while (value2 != 0)
        {
            var tmp = value2;
            value2 = value1 % value2;
            value1 = tmp;
        }
        return value1;
    }

    /// <summary>
    /// Determines the greatest common factor of the two given values.
    /// </summary>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <returns>The greatest common factor of the two values.</returns>
    public static ulong GreatestCommonFactor(long value1, ulong value2)
        => GreatestCommonFactor((ulong)Math.Abs(value1), value2);

    /// <summary>
    /// Determines the least common multiple of the two given values.
    /// </summary>
    /// <param name="value1">The first value.</param>
    /// <param name="value2">The second value.</param>
    /// <returns>
    /// The least common multiple of the two values; or <see langword="null"/> if no factor smaller
    /// than <see cref="ushort.MaxValue"/> exists.
    /// </returns>
    public static ushort? LeastCommonMultiple(ushort value1, ushort value2)
    {
        var result = (uint)value1 / GreatestCommonFactor(value1, value2) * value2;
        if (result > ushort.MaxValue)
        {
            return null;
        }
        return (ushort)result;
    }

    /// <summary>
    /// <para>
    /// Converts a <see cref="HugeNumber"/> which represents a rational fraction to an equivalent
    /// decimal value.
    /// </para>
    /// <para>
    /// If the given <paramref name="value"/> is not a rational fraction, it is returned unchanged.
    /// </para>
    /// </summary>
    /// <param name="value">A value to convert.</param>
    /// <returns>
    /// A <see cref="HugeNumber"/> equivalent to <paramref name="value"/> which does not represent a
    /// rational fraction.
    /// </returns>
    public static HugeNumber ToDecimal(HugeNumber value)
        => ToDenominator(value, 1);

    internal static HugeNumber ToDenominator(HugeNumber value, ushort denominator)
    {
        if (value.Denominator == denominator)
        {
            return value;
        }

        if (value.IsNotRational())
        {
            return value;
        }

        if (denominator == 0)
        {
            if (value.Mantissa == 0)
            {
                return NaN;
            }
            return value.Mantissa > 0
                ? PositiveInfinity
                : NegativeInfinity;
        }

        if (denominator == 1)
        {
            return new((decimal)value.Mantissa / value.Denominator, value.Exponent);
        }

        if (denominator < value.Denominator)
        {
            if (value.Denominator % denominator != 0)
            {
                return new((decimal)value.Mantissa / value.Denominator, value.Exponent);
            }
            var divisor = value.Denominator / denominator;
            if (value.Mantissa % divisor != 0)
            {
                return new((decimal)value.Mantissa / value.Denominator, value.Exponent);
            }
            return new(value.Mantissa / divisor, denominator, value.Exponent);
        }

        if (denominator % value.Denominator != 0)
        {
            return new((decimal)value.Mantissa / value.Denominator, value.Exponent);
        }

        var multiplier = denominator / value.Denominator;
        if (MAX_MANTISSA / multiplier < value.Mantissa)
        {
            return new((decimal)value.Mantissa / value.Denominator, value.Exponent);
        }
        return new(value.Mantissa * multiplier, denominator, value.Exponent);
    }

    private static HugeNumber ReduceFraction(HugeNumber value)
    {
        var mantissa = value.Mantissa;
        var denominator = value.Denominator;
        if (denominator > 1)
        {
            var factor = GreatestCommonFactor(mantissa, denominator);
            if (factor > 1)
            {
                mantissa /= (long)factor;
                denominator /= (ushort)factor;
                return new HugeNumber(mantissa, denominator, value.Exponent);
            }
        }
        return value;
    }
}
