namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns <c>e</c> raised to the specified power.
    /// </summary>
    /// <param name="x">The power to which <c>e</c> is raised.</param>
    /// <returns>
    /// The number <c>e</c> raised to the power of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result
    /// will be <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NegativeInfinity"/>, the result will be <see
    /// cref="Zero"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber Exp(HugeNumber x)
    {
        if (x.IsNaN() || x.IsPositiveInfinity())
        {
            return x;
        }
        else if (x.IsNegativeInfinity())
        {
            return Zero;
        }
        else if (x.Mantissa == 0)
        {
            return One;
        }
        else if (x == One)
        {
            return E;
        }

        x = ToDenominator(x, 1);
        var result = One + x;
        if (result.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        var numerator = x * x;
        if (numerator.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        var denominator = new HugeNumber(2);
        var nextDenominatorDigit = 3;
        HugeNumber newResult;
        do
        {
            newResult = result + (numerator / denominator);
            if (newResult == result)
            {
                return result;
            }
            numerator *= x;
            if (numerator.IsPositiveInfinity())
            {
                return PositiveInfinity;
            }
            denominator *= nextDenominatorDigit;
            if (denominator.IsPositiveInfinity())
            {
                return result;
            }
            nextDenominatorDigit++;
            result = newResult;
        } while (nextDenominatorDigit <= int.MaxValue);
        return result;
    }

    /// <summary>
    /// Returns <c>e</c> raised to the power of this value.
    /// </summary>
    /// <returns>
    /// The number <c>e</c> raised to the power of this value.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this value is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result will be
    /// this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, the result will be <see cref="Zero"/>.
    /// </para>
    /// </remarks>
    public HugeNumber Exp() => Exp(this);

    /// <summary>
    /// Computes <c>10</c> raised to a given power.
    /// </summary>
    /// <param name="x">The power to which <c>10</c> is raised.</param>
    /// <returns>
    /// <c>10</c> raised to the power of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result
    /// will be <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NegativeInfinity"/>, the result will be <see
    /// cref="Zero"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber Exp10(HugeNumber x)
    {
        if (x.IsNaN() || x.IsPositiveInfinity())
        {
            return x;
        }
        else if (x.IsNegativeInfinity())
        {
            return Zero;
        }
        else if (x.Mantissa == 0)
        {
            return One;
        }
        else if (x == One)
        {
            return HugeNumberConstants.Ten;
        }
        else if (x == NegativeOne)
        {
            return One;
        }

        if (x > MAX_EXPONENT + (MANTISSA_SIGNIFICANT_DIGITS - 2))
        {
            return PositiveInfinity;
        }

        // whole number
        if (x.IsInteger()
            && x.IsPositive())
        {
            var intX = (int)x;
            var exp = intX - (MANTISSA_SIGNIFICANT_DIGITS - 1);
            return new HugeNumber(ulong.Parse("1" + new string('0', intX - exp)), exp);
        }

        // rational fraction
        if (x.IsInteger()
            && x.IsNegative()
            && x > -6)
        {
            return new HugeNumber(1, ushort.Parse("1" + new string('0', (int)x.Negate() - 1)));
        }

        return HugeNumberConstants.Ten.Pow(x);
    }

    /// <summary>
    /// Computes <c>10</c> raised to the power of this value.
    /// </summary>
    /// <returns>
    /// <c>10</c> raised to the power of this value.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this value is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result will be
    /// this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, the result will be <see cref="Zero"/>.
    /// </para>
    /// </remarks>
    public HugeNumber Exp10() => Exp10(this);

    /// <summary>
    /// Computes <c>10</c> raised to a given power and subtracts one.
    /// </summary>
    /// <param name="x">The power to which <c>10</c> is raised.</param>
    /// <returns>
    /// <c>10</c> raised to the power of <paramref name="x"/>, minus <c>1</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result
    /// will be <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NegativeInfinity"/>, the result will be <see
    /// cref="Zero"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber Exp10M1(HugeNumber x) => Exp10(x) - One;

    /// <summary>
    /// Computes <c>10</c> raised to the power of this value and subtracts one.
    /// </summary>
    /// <returns>
    /// <c>10</c> raised to the power of this value, minus <c>1</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this value is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result will be
    /// this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, the result will be <see cref="Zero"/>.
    /// </para>
    /// </remarks>
    public HugeNumber Exp10M1() => Exp10M1(this);

    /// <summary>
    /// Computes <c>2</c> raised to a given power.
    /// </summary>
    /// <param name="x">The power to which <c>2</c> is raised.</param>
    /// <returns>
    /// <c>2</c> raised to the power of <paramref name="x"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result
    /// will be <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NegativeInfinity"/>, the result will be <see
    /// cref="Zero"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber Exp2(HugeNumber x)
    {
        if (x.IsNaN() || x.IsPositiveInfinity())
        {
            return x;
        }
        else if (x.IsNegativeInfinity())
        {
            return Zero;
        }
        else if (x.Mantissa == 0)
        {
            return One;
        }
        else if (x == One)
        {
            return HugeNumberConstants.Two;
        }

        if (x.IsInteger()
            && x.IsPositive()
            && x < 63)
        {
            return new HugeNumber(1L << (int)x);
        }

        return HugeNumberConstants.Two.Pow(x);
    }

    /// <summary>
    /// Computes <c>2</c> raised to the power of this value.
    /// </summary>
    /// <returns>
    /// <c>2</c> raised to the power of this value.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this value is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result will be
    /// this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, the result will be <see cref="Zero"/>.
    /// </para>
    /// </remarks>
    public HugeNumber Exp2() => Exp2(this);

    /// <summary>
    /// Computes <c>2</c> raised to a given power and subtracts one.
    /// </summary>
    /// <param name="x">The power to which <c>2</c> is raised.</param>
    /// <returns>
    /// <c>2</c> raised to the power of <paramref name="x"/>, minus <c>1</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result
    /// will be <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NegativeInfinity"/>, the result will be <see
    /// cref="Zero"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber Exp2M1(HugeNumber x)
    {
        if (x.IsNaN() || x.IsPositiveInfinity())
        {
            return x;
        }
        else if (x.IsNegativeInfinity())
        {
            return NegativeOne;
        }
        else if (x.Mantissa == 0)
        {
            return Zero;
        }
        else if (x == One)
        {
            return One;
        }

        if (x.IsInteger()
            && x.IsPositive()
            && x < 63)
        {
            return new HugeNumber((1L << (int)x) - 1);
        }

        return HugeNumberConstants.Two.Pow(x) - One;
    }

    /// <summary>
    /// Computes <c>2</c> raised to the power of this value and subtracts one.
    /// </summary>
    /// <returns>
    /// <c>2</c> raised to the power of this value, minus <c>1</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this value is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result will be
    /// this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, the result will be <see cref="Zero"/>.
    /// </para>
    /// </remarks>
    public HugeNumber Exp2M1() => Exp2M1(this);

    /// <summary>
    /// Returns <c>e</c> raised to the specified power and subtracts one.
    /// </summary>
    /// <param name="x">The power to which <c>e</c> is raised.</param>
    /// <returns>
    /// The number <c>e</c> raised to the power of <paramref name="x"/>, minus <c>1</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result
    /// will be <paramref name="x"/>.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is <see cref="NegativeInfinity"/>, the result will be <see
    /// cref="Zero"/>.
    /// </para>
    /// </remarks>
    public static HugeNumber ExpM1(HugeNumber x)
    {
        if (x.IsNaN() || x.IsPositiveInfinity())
        {
            return x;
        }
        else if (x.IsNegativeInfinity())
        {
            return NegativeOne;
        }
        else if (x.Mantissa == 0)
        {
            return Zero;
        }
        else if (x == One)
        {
            return E - One;
        }

        x = ToDenominator(x, 1);
        var result = One + x;
        if (result.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        var numerator = x * x;
        if (numerator.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        var denominator = new HugeNumber(2);
        var nextDenominatorDigit = 3;
        HugeNumber newResult;
        do
        {
            newResult = result + (numerator / denominator);
            if (newResult == result)
            {
                return result - One;
            }
            numerator *= x;
            if (numerator.IsPositiveInfinity())
            {
                return PositiveInfinity;
            }
            denominator *= nextDenominatorDigit;
            if (denominator.IsPositiveInfinity())
            {
                return result - One;
            }
            nextDenominatorDigit++;
            result = newResult;
        } while (nextDenominatorDigit <= int.MaxValue);
        return result - One;
    }

    /// <summary>
    /// Returns <c>e</c> raised to the power of this value and subtracts one.
    /// </summary>
    /// <returns>
    /// The number <c>e</c> raised to the power of this value, minus <c>1</c>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// If this value is <see cref="NaN"/> or <see cref="PositiveInfinity"/>, the result will be
    /// this value.
    /// </para>
    /// <para>
    /// If this value is <see cref="NegativeInfinity"/>, the result will be <see cref="Zero"/>.
    /// </para>
    /// </remarks>
    public HugeNumber ExpM1() => ExpM1(this);
}
