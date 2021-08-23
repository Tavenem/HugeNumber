namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Gets the absolute value of a <see cref="HugeNumber"/> object.
    /// </summary>
    /// <param name="value">A number.</param>
    /// <returns>The absolute value of <paramref name="value"/>.</returns>
    public static HugeNumber Abs(HugeNumber value)
        => new(Math.Abs(value.Mantissa), value.Denominator, value.Exponent);

    /// <summary>
    /// Gets the absolute value of this instance.
    /// </summary>
    /// <returns>The absolute value of this instance.</returns>
    public HugeNumber Abs() => Abs(this);

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
}
