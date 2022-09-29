namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
    /// finite value, the result is 0 if its sign matches <paramref name="divisor"/>, and
    /// <see cref="NegativeZero"/> if they have opposite signs.
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
            else if (dividend.Mantissa > 0)
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
            return dividend;
        }
        if (dividend.IsInfinity())
        {
            return (dividend.Mantissa < 0) == (divisor.Mantissa < 0)
                ? PositiveInfinity
                : NegativeInfinity;
        }
        if (divisor.IsInfinity())
        {
            return (dividend.Mantissa < 0) == (divisor.Mantissa < 0)
                ? NegativeZero
                : Zero;
        }

        if (dividend.Exponent - divisor.Exponent is < MIN_EXPONENT or > MAX_EXPONENT)
        {
            return (dividend.Mantissa < 0) == (divisor.Mantissa < 0)
                ? PositiveInfinity
                : NegativeInfinity;
        }

        if ((dividend.Denominator == 1 && dividend.Exponent < 0)
            || (divisor.Denominator == 1 && divisor.Exponent < 0)
            || long.MaxValue / dividend.Denominator < Math.Abs(divisor.Mantissa)
            || long.MaxValue / Math.Abs(dividend.Mantissa) < divisor.Denominator)
        {
            dividend = ToDenominator(dividend, 1);
            divisor = ToDenominator(divisor, 1);
        }
        else
        {
            var numerator = dividend.Mantissa * divisor.Denominator;
            var denominator = dividend.Denominator * (ulong)Math.Abs(divisor.Mantissa);
            var greatestCommonFactor = GreatestCommonFactor(numerator, denominator);
            if (greatestCommonFactor > 1)
            {
                numerator /= (long)greatestCommonFactor;
                denominator /= greatestCommonFactor;
            }
            if (denominator > ushort.MaxValue
                || numerator > MAX_MANTISSA)
            {
                dividend = ToDenominator(dividend, 1);
                divisor = ToDenominator(divisor, 1);
            }
            else
            {
                if (divisor.Mantissa < 0 && dividend.Mantissa < 0)
                {
                    numerator *= -1;
                }
                return new(numerator, (ushort)denominator, (short)(dividend.Exponent - divisor.Exponent));
            }
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
    /// Divides a specified <see cref="HugeNumber"/> value by another specified <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see cref="NaN"/>,
    /// the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result is
    /// infinite, with a sign that matches <paramref name="dividend"/>.
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
    public static HugeNumber operator /(HugeNumber dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="decimal"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="dividend"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
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
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, decimal divisor) => Divide(dividend, (HugeNumber)divisor);

    /// <summary>
    /// Divides a specified <see cref="decimal"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="divisor"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
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
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(decimal dividend, HugeNumber divisor) => Divide((HugeNumber)dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="double"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see cref="NaN"/>,
    /// the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result is
    /// infinite, with a sign that matches <paramref name="dividend"/>.
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
    public static HugeNumber operator /(HugeNumber dividend, double divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="double"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see cref="NaN"/>,
    /// the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result is
    /// infinite, with a sign that matches <paramref name="dividend"/>.
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
    public static HugeNumber operator /(double dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="long"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="dividend"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
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
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, long divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="long"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="divisor"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
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
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(long dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="ulong"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="dividend"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
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
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, ulong divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="ulong"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="divisor"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
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
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(ulong dividend, HugeNumber divisor) => Divide(dividend, divisor);
}
