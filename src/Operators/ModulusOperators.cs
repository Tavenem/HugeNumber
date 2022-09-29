namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
    /// Returns the remainder that results from division with two specified <see
    /// cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, HugeNumber divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="decimal"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see
    /// cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, decimal divisor) => Mod(dividend, (HugeNumber)divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="decimal"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(decimal dividend, HugeNumber divisor) => Mod((HugeNumber)dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="double"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, double divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="double"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(double dividend, HugeNumber divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="long"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see
    /// cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, long divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="long"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(long dividend, HugeNumber divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="ulong"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see
    /// cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, ulong divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="ulong"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(ulong dividend, HugeNumber divisor) => Mod(dividend, divisor);
}
