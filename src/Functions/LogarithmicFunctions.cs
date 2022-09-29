namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of a specified number.
    /// </summary>
    /// <param name="x">The number whose logarithm is to be found.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The natural logarithm of <paramref name="x"/>; that is, ln <paramref name="x"/>, or
    /// log<sub>e</sub> <paramref name="x"/>
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber Log(HugeNumber x)
    {
        if (x.IsNaN() || Sign(x) < 0)
        {
            return NaN;
        }
        else if (x.Mantissa == 0 || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        x = ToDenominator(x, 1);
        if (x.Exponent >= 0
            && long.MaxValue - x.MantissaDigits + 1 < x.Exponent)
        {
            return PositiveInfinity;
        }
        else if (x.Exponent < 0
            && x.Exponent < long.MinValue + x.MantissaDigits - 1)
        {
            return PositiveInfinity;
        }
        else
        {
            var z = (double)x.Mantissa;
            var mantissaDigits = (int)x.MantissaDigits;
            var exponent = (int)x.Exponent;
            if (mantissaDigits > 1)
            {
                if (exponent < 0)
                {
                    var place = Math.Min(mantissaDigits - 1, -exponent);
                    z /= Math.Pow(10, place);
                    exponent += place;
                    mantissaDigits -= place;
                }
                if (mantissaDigits > 1)
                {
                    z /= Math.Pow(10, mantissaDigits - 1);
                    exponent += mantissaDigits - 1;
                }
            }

            double result, term;
            var newResult = 0.0;
            var total = 0.0;
            var k = 0;
            do
            {
                result = newResult;
                term = Math.Pow((z - 1) / (z + 1), (2 * k) + 1) / ((2 * k) + 1);
                total += term;
                newResult = 2 * total;
                k++;
            } while (!double.IsInfinity(newResult) && !newResult.IsNearlyEqualTo(result) && k < int.MaxValue);
            return (exponent * HugeNumberConstants.Ln10) + newResult;
        }
    }

    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of this value.
    /// </summary>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>this value</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The natural logarithm of this value; that is, ln or
    /// log<sub>e</sub>
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public HugeNumber Log() => Log(this);

    /// <summary>
    /// Computes the logarithm of a value in the specified base.
    /// </summary>
    /// <param name="x">The value whose logarithm is to be computed.</param>
    /// <param name="newBase">The base in which the logarithm is to be computed.</param>
    /// <returns>
    /// One of the values in the following table. (+Infinity denotes
    /// <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/>, -Infinity denotes
    /// <see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/>, and NaN denotes
    /// <see cref="IFloatingPointIeee754{TSelf}.NaN"/>.)
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description><paramref name="newBase"/> Return value</description>
    /// </listheader>
    /// <item>
    /// <term><paramref name="x"/> &gt; 0</term>
    /// <description>
    /// (0 &lt; newBase &lt; 1) -or- (newBase &gt; 1) log<sub>newBase</sub>(<paramref name="x"/>)
    /// </description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> &lt; 0</term>
    /// <description>(any value) NaN</description>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <description>newBase &lt; 0 NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> != 1</term>
    /// <description>newBase = 0 NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> != 1</term>
    /// <description>newBase = +Infinity NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = NaN</term>
    /// <description>(any value) NaN</description>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <description>newBase = NaN NaN</description>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <description>newBase = 1 NaN</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 0</term>
    /// <description>0 &lt; newBase &lt; 1 +Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 0</term>
    /// <description>newBase &gt; 1 -Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = +Infinity</term>
    /// <description>0 &lt; newBase &lt; 1 -Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = +Infinity</term>
    /// <description>newBase &gt; 1 +Infinity</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 1</term>
    /// <description>newBase = 0 0</description>
    /// </item>
    /// <item>
    /// <term><paramref name="x"/> = 1</term>
    /// <description>newBase = +Infinity 0</description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber Log(HugeNumber x, HugeNumber newBase)
    {
        if (x.IsNaN()
            || newBase.IsNaN()
            || newBase == 1
            || Sign(x) < 0
            || (x != 1 && (newBase.Mantissa == 0 || newBase.IsInfinity())))
        {
            return NaN;
        }
        else if (x.Mantissa == 0 || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(x) / Log(newBase);
        }
    }

    /// <summary>
    /// Computes the base-10 logarithm of a value.
    /// </summary>
    /// <param name="x">The value whose base-10 logarithm is to be computed.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 10 log of <paramref name="x"/>; that is, log<sub>10</sub><paramref name="x"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber Log10(HugeNumber x)
    {
        if (x.IsNaN() || Sign(x) < 0)
        {
            return NaN;
        }
        else if (x.Mantissa == 0 || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(x) / HugeNumberConstants.Ln10;
        }
    }

    /// <summary>
    /// Computes the base-10 logarithm of this value.
    /// </summary>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>this value</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 10 log of this value; that is, log<sub>10</sub>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public HugeNumber Log10() => Log10(this);

    /// <summary>
    /// Computes the base-10 logarithm of a value plus one.
    /// </summary>
    /// <param name="x">
    /// The value to which one is added before computing the base-10 logarithm.
    /// </param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 10 log of <paramref name="x"/> plus one; that is, log<sub>10</sub>(<paramref name="x"/>+1).
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber Log10P1(HugeNumber x)
    {
        if (x.IsNaN() || x < NegativeOne)
        {
            return NaN;
        }
        else if (x == NegativeOne || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(x + One) / HugeNumberConstants.Ln10;
        }
    }

    /// <summary>
    /// Computes the base-10 logarithm of this value plus one.
    /// </summary>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>this value</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 10 log of this value plus one; that is, log<sub>10</sub>(x+1).
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public HugeNumber Log10P1() => Log10P1(this);

    /// <summary>
    /// Computes the base-2 logarithm of a value.
    /// </summary>
    /// <param name="x">The value whose base-2 logarithm is to be computed.</param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 2 log of <paramref name="x"/>; that is, log<sub>2</sub><paramref name="x"/>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber Log2(HugeNumber x)
    {
        if (x.IsNaN() || Sign(x) < 0)
        {
            return NaN;
        }
        else if (x.Mantissa == 0 || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(x) / HugeNumberConstants.Ln2;
        }
    }

    /// <summary>
    /// Computes the base-2 logarithm of this value.
    /// </summary>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>this value</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 2 log of this value; that is, log<sub>2</sub>.
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public HugeNumber Log2() => Log2(this);

    /// <summary>
    /// Computes the base-2 logarithm of a value plus one.
    /// </summary>
    /// <param name="x">
    /// The value to which one is added before computing the base-2 logarithm.
    /// </param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 2 log of <paramref name="x"/> plus one; that is, log<sub>2</sub>(<paramref name="x"/>+1).
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber Log2P1(HugeNumber x)
    {
        if (x.IsNaN() || x < NegativeOne)
        {
            return NaN;
        }
        else if (x == NegativeOne || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(x + One) / HugeNumberConstants.Ln2;
        }
    }

    /// <summary>
    /// Computes the base-2 logarithm of this value plus one.
    /// </summary>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>this value</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The base 2 log of this value> plus one; that is, log<sub>2</sub>(x+1).
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public HugeNumber Log2P1() => Log2P1(this);

    /// <summary>
    /// Returns the natural (base <c>e</c>) logarithm of a value plus one.
    /// </summary>
    /// <param name="x">
    /// The value to which one is added before computing the natural logarithm.
    /// </param>
    /// <returns>
    /// One of the values in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="x"/> parameter</term>
    /// <description>Return value</description>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <description>
    /// The natural logarithm of <paramref name="x"/>; that is, ln(<paramref name="x"/>+1), or
    /// log<sub>e</sub>(<paramref name="x"/>+1)
    /// </description>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NegativeInfinity"/></description>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.NaN"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.NaN"/></description>
    /// </item>
    /// <item>
    /// <term>Equal to <see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></term>
    /// <description><see cref="IFloatingPointIeee754{TSelf}.PositiveInfinity"/></description>
    /// </item>
    /// </list>
    /// </returns>
    public static HugeNumber LogP1(HugeNumber x)
    {
        if (x.IsNaN() || x < NegativeOne)
        {
            return NaN;
        }
        else if (x == NegativeOne || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        return Log(x + One);
    }
}
