namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
        return new HugeNumber(-value.Mantissa, value.Denominator, value.Exponent);
    }

    /// <summary>
    /// Negates this instance.
    /// </summary>
    /// <returns>
    /// The result of this instance multiplied by negative one (-1).
    /// </returns>
    public HugeNumber Negate() => Negate(this);

    /// <summary>
    /// Negates a specified <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
    public static HugeNumber operator -(HugeNumber value) => Negate(value);
}
