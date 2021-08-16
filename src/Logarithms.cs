namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the natural (base e) logarithm of a specified number.
    /// </summary>
    /// <param name="value">The number whose logarithm is to be found.</param>
    /// <returns>
    /// The natural (base e) logarithm of <paramref name="value"/>, as shown in the table in the
    /// Remarks section.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// The natural log of <paramref name="value"/> is out of range of the <see cref="double"/>
    /// data type.
    /// </exception>
    /// <remarks>
    /// <para>The <paramref name="value"/> parameter is specified as a base 10 number.</para>
    /// <para>
    /// The precise return value of this method depends on the value of <paramref
    /// name="value"/>, as the following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Sign of <paramref name="value"/> parameter</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The natural logarithm of <paramref name="value"/>; that is, ln <paramref name="value"/>,
    /// or log<sub>e</sub><paramref name="value"/>.
    /// </term>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// </list>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <para>
    /// To calculate the base 10 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log10(HugeNumber)"/> method. To calculate the base 2 logarithm of a <see
    /// cref="HugeNumber"/> value, call the <see cref="Log2(HugeNumber)"/> method. To calculate the
    /// logarithm of a number in another base, call the
    /// <see cref="Log(HugeNumber, HugeNumber)"/> method.
    /// </para>
    /// <para>
    /// This method corresponds to the <see cref="Math.Log(double)"/> method.
    /// </para>
    /// </remarks>
    public static HugeNumber Log(HugeNumber value)
    {
        if (value.IsNaN() || Sign(value) < 0)
        {
            return NaN;
        }
        else if (value.Mantissa == 0 || value.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else if (value.Exponent >= 0
            && long.MaxValue - value.MantissaDigits + 1 < value.Exponent)
        {
            return PositiveInfinity;
        }
        else if (value.Exponent < 0
            && value.Exponent < long.MinValue + value.MantissaDigits - 1)
        {
            return PositiveInfinity;
        }
        else
        {
            var z = (double)value.Mantissa;
            var mantissaDigits = (int)value.MantissaDigits;
            var exponent = (int)value.Exponent;
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
            return (exponent * Ln10) + newResult;
        }
    }

    /// <summary>
    /// Returns the natural (base e) logarithm of this instance.
    /// </summary>
    /// <returns>
    /// The natural (base e) logarithm of this instance, as shown in the table in the Remarks
    /// section.
    /// </returns>
    /// <remarks>
    /// <para>This instance is specified as a base 10 number.</para>
    /// <para>
    /// The precise return value of this method depends on the value of this instance, as the
    /// following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Sign of this instance</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The natural logarithm of this instance; that is, ln value, or log<sub>e</sub>value.
    /// </term>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// </list>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <para>
    /// To calculate the base 10 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log10()"/> method. To calculate the base 2 logarithm of a <see cref="HugeNumber"/>
    /// value, call the <see cref="Log2()"/> method. To calculate the logarithm of a number in
    /// another base, call the
    /// <see cref="Log(HugeNumber)"/> method.
    /// </para>
    /// </remarks>
    public HugeNumber Log() => Log(this);

    /// <summary>
    /// Returns the logarithm of a specified number in a specified base.
    /// </summary>
    /// <param name="value">A number whose logarithm is to be found.</param>
    /// <param name="newBase">The base of the logarithm.</param>
    /// <returns>
    /// The base <paramref name="newBase"/> logarithm of <paramref name="value"/>, as described
    /// in the Remarks section.
    /// </returns>
    /// <remarks>
    /// <para>
    /// The <paramref name="value"/> and <paramref name="newBase"/> parameters are specified as
    /// base 10 numbers.
    /// </para>
    /// <para>
    /// The precise return value of the method depends on the sign of <paramref name="value"/>
    /// and on the sign and value of <paramref name="newBase"/>, as the following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term><paramref name="value"/> parameter</term>
    /// <term><paramref name="newBase"/> parameter</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term><paramref name="value"/> &gt; 0</term>
    /// <term>
    /// (0 &lt; <paramref name="newBase"/> &lt; 1) -or- ( <paramref name="newBase"/> &gt; 1)
    /// </term>
    /// <term>log <sub><paramref name="newBase"/></sub>( <paramref name="value"/>)</term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> = <see cref="NaN"/></term>
    /// <term>(any value)</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> &lt; 0</term>
    /// <term>(any value)</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <term><paramref name="newBase"/> &lt; 0</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> != 1</term>
    /// <term><paramref name="newBase"/> = 0</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> != 1</term>
    /// <term><paramref name="newBase"/> = <see cref="PositiveInfinity"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <term><paramref name="newBase"/> = <see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term>(any value)</term>
    /// <term><paramref name="newBase"/> = 1</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> = <see cref="PositiveInfinity"/></term>
    /// <term>(any value)</term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> = 0</term>
    /// <term>0 &lt; <paramref name="newBase"/> &lt; 1</term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> = 0</term>
    /// <term><paramref name="newBase"/> &gt; 1</term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> = 1</term>
    /// <term><paramref name="newBase"/> = 0</term>
    /// <term>0</term>
    /// </item>
    /// <item>
    /// <term><paramref name="value"/> = 1</term>
    /// <term><paramref name="newBase"/> = <see cref="PositiveInfinity"/></term>
    /// <term>0</term>
    /// </item>
    /// </list>
    /// <para>
    /// To calculate the base 10 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log10(HugeNumber)"/> method. To calculate the base 2 logarithm of a <see
    /// cref="HugeNumber"/> value, call the <see cref="Log2(HugeNumber)"/> method. To calculate the
    /// natural logarithm of a number, call the <see cref="Log(HugeNumber)"/> method.
    /// </para>
    /// <para>
    /// This method corresponds to the <see cref="Math.Log(double, double)"/> method.
    /// </para>
    /// </remarks>
    public static HugeNumber Log(HugeNumber value, HugeNumber newBase)
    {
        if (value.IsNaN()
            || newBase.IsNaN()
            || newBase == 1
            || Sign(value) < 0
            || (value != 1 && (newBase.Mantissa == 0 || newBase.IsInfinity())))
        {
            return NaN;
        }
        else if (value.Mantissa == 0 || value.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(value) / Log(newBase);
        }
    }

    /// <summary>
    /// Returns the base 2 logarithm of a specified number.
    /// </summary>
    /// <param name="value">The number whose logarithm is to be found.</param>
    /// <returns>
    /// The base 2 logarithm of <paramref name="value"/>, as shown in the table in the Remarks
    /// section.
    /// </returns>
    /// <remarks>
    /// <para>The <paramref name="value"/> parameter is specified as a base 10 number.</para>
    /// <para>
    /// The precise return value of this method depends on the value of <paramref
    /// name="value"/>, as the following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Sign of <paramref name="value"/> parameter</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The base 2 logarithm of <paramref name="value"/>; that is, log<sub>2</sub><paramref
    /// name="value"/>.
    /// </term>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="NegativeInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// </list>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <para>
    /// To calculate the base 10 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log10(HugeNumber)"/> method. To calculate the natural logarithm of a <see
    /// cref="HugeNumber"/> value, call the <see cref="Log(HugeNumber)"/> method. To calculate the
    /// logarithm of a number in another base, call the <see cref="Log(HugeNumber, HugeNumber)"/>
    /// method.
    /// </para>
    /// </remarks>
    public static HugeNumber Log2(HugeNumber value)
    {
        if (value.IsNaN() || Sign(value) < 0)
        {
            return NaN;
        }
        else if (value.Mantissa == 0 || value.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(value) / Ln2;
        }
    }

    /// <summary>
    /// Returns the base 2 logarithm of this instance.
    /// </summary>
    /// <returns>
    /// The base 2 logarithm of this instance, as shown in the table in the Remarks section.
    /// </returns>
    /// <remarks>
    /// <para>This instance is specified as a base 10 number.</para>
    /// <para>
    /// The precise return value of this method depends on the value of this instance, as the
    /// following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Sign of this instance</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The base 2 logarithm of this instance; that is, log<sub>2</sub>value.
    /// </term>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="NegativeInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// </list>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <para>
    /// To calculate the base 10 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log10()"/> method. To calculate the natural logarithm of a <see cref="HugeNumber"/>
    /// value, call the <see cref="Log()"/> method. To calculate the logarithm of a number in
    /// another base, call the
    /// <see cref="Log(HugeNumber)"/> method.
    /// </para>
    /// </remarks>
    public HugeNumber Log2() => Log2(this);

    /// <summary>
    /// Returns the base 10 logarithm of a specified number.
    /// </summary>
    /// <param name="value">The number whose logarithm is to be found.</param>
    /// <returns>
    /// The base 10 logarithm of <paramref name="value"/>, as shown in the table in the Remarks
    /// section.
    /// </returns>
    /// <remarks>
    /// <para>The <paramref name="value"/> parameter is specified as a base 10 number.</para>
    /// <para>
    /// The precise return value of this method depends on the value of <paramref
    /// name="value"/>, as the following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Sign of <paramref name="value"/> parameter</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The base 10 logarithm of <paramref name="value"/>; that is, log<sub>10</sub><paramref
    /// name="value"/>.
    /// </term>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="NegativeInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// </list>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <para>
    /// To calculate the base 2 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log2(HugeNumber)"/> method. To calculate the natural logarithm of a <see
    /// cref="HugeNumber"/> value, call the <see cref="Log(HugeNumber)"/> method. To calculate the
    /// logarithm of a number in another base, call the <see cref="Log(HugeNumber, HugeNumber)"/>
    /// method.
    /// </para>
    /// <para>
    /// This method corresponds to the <see cref="Math.Log10(double)"/> method.
    /// </para>
    /// </remarks>
    public static HugeNumber Log10(HugeNumber value)
    {
        if (value.IsNaN() || Sign(value) < 0)
        {
            return NaN;
        }
        else if (value.Mantissa == 0 || value.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        else
        {
            return Log(value) / Ln10;
        }
    }

    /// <summary>
    /// Returns the base 10 logarithm of this instance.
    /// </summary>
    /// <returns>
    /// The base 10 logarithm of this instance, as shown in the table in the Remarks section.
    /// </returns>
    /// <remarks>
    /// <para>This instance is specified as a base 10 number.</para>
    /// <para>
    /// The precise return value of this method depends on the value of this instance, as the
    /// following table shows.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Sign of this instance</term>
    /// <term>Return value</term>
    /// </listheader>
    /// <item>
    /// <term>Positive</term>
    /// <term>
    /// The base 10 logarithm of this instance; that is, log<sub>10</sub>value.
    /// </term>
    /// </item>
    /// <item>
    /// <term><see cref="PositiveInfinity"/></term>
    /// <term><see cref="PositiveInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term><see cref="NegativeInfinity"/></term>
    /// </item>
    /// <item>
    /// <term>Negative</term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// </list>
    /// <item>
    /// <term><see cref="NaN"/></term>
    /// <term><see cref="NaN"/></term>
    /// </item>
    /// <para>
    /// To calculate the base 2 logarithm of a <see cref="HugeNumber"/> value, call the <see
    /// cref="Log2()"/> method. To calculate the natural logarithm of a <see cref="HugeNumber"/>
    /// value, call the <see cref="Log()"/> method. To calculate the logarithm of a number in
    /// another base, call the <see cref="Log(HugeNumber)"/> method.
    /// </para>
    /// </remarks>
    public HugeNumber Log10() => Log10(this);
}
