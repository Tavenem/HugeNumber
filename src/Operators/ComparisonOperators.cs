namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(decimal other)
    {
        if (IsNaN())
        {
            return -1;
        }
        if (IsPositiveInfinity())
        {
            return 1;
        }
        if (IsNegativeInfinity())
        {
            return -1;
        }

        // When the value is zero, exponent can be disregarded; only the sign of the mantissa
        // matters for the purpose of comparison.
        if (Mantissa == 0)
        {
            return Mantissa.CompareTo(Math.Sign(other));
        }

        // simple cases with opposite signs
        if (Mantissa < 0 && other > 0)
        {
            return -1;
        }
        if (other < 0 && Mantissa > 0)
        {
            return 1;
        }

        // in non-trivial cases, convert to a Number to compare
        return CompareTo((HugeNumber)other);
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(double other)
    {
        if (IsNaN())
        {
            return double.IsNaN(other) ? 0 : -1;
        }
        if (double.IsNaN(other))
        {
            return 1;
        }
        if (IsPositiveInfinity())
        {
            return double.IsPositiveInfinity(other) ? 0 : 1;
        }
        if (double.IsPositiveInfinity(other))
        {
            return -1;
        }
        if (IsNegativeInfinity())
        {
            return double.IsNegativeInfinity(other) ? 0 : -1;
        }
        if (double.IsNegativeInfinity(other))
        {
            return 1;
        }

        // When the value is zero, exponent can be disregarded; only the sign of the mantissa
        // matters for the purpose of comparison.
        if (Mantissa == 0)
        {
            return Mantissa.CompareTo(Math.Sign(other));
        }

        // simple cases with opposite signs
        if (Mantissa < 0 && other > 0)
        {
            return -1;
        }
        if (other < 0 && Mantissa > 0)
        {
            return 1;
        }

        // in non-trivial cases, convert to a Number to compare
        return CompareTo((HugeNumber)other);
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(float other)
    {
        if (IsNaN())
        {
            return float.IsNaN(other) ? 0 : -1;
        }
        if (float.IsNaN(other))
        {
            return 1;
        }
        if (IsPositiveInfinity())
        {
            return float.IsPositiveInfinity(other) ? 0 : 1;
        }
        if (float.IsPositiveInfinity(other))
        {
            return -1;
        }
        if (IsNegativeInfinity())
        {
            return float.IsNegativeInfinity(other) ? 0 : -1;
        }
        if (float.IsNegativeInfinity(other))
        {
            return 1;
        }

        // When the value is zero, exponent can be disregarded; only the sign of the mantissa
        // matters for the purpose of comparison.
        if (Mantissa == 0)
        {
            return Mantissa.CompareTo(Math.Sign(other));
        }

        // simple cases with opposite signs
        if (Mantissa < 0 && other > 0)
        {
            return -1;
        }
        if (other < 0 && Mantissa > 0)
        {
            return 1;
        }

        // in non-trivial cases, convert to a Number to compare
        return CompareTo((HugeNumber)other);
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(int other)
    {
        if (IsNaN())
        {
            return -1;
        }
        if (IsPositiveInfinity())
        {
            return 1;
        }
        if (IsNegativeInfinity())
        {
            return -1;
        }

        // When the other value is zero, or this instance has no exponent, the exponent can be
        // disregarded.
        if (other == 0 || Exponent == 0)
        {
            return Mantissa.CompareTo(other);
        }

        // simple cases with opposite signs
        if (Mantissa < 0 && other > 0)
        {
            return -1;
        }
        if (other < 0 && Mantissa > 0)
        {
            return 1;
        }

        // account for comparisons involving negative exponents
        if (Exponent < -MantissaDigits)
        {
            return -1;
        }

        // in non-trivial cases, convert the other value to a Number, then compare
        return CompareTo(new HugeNumber(other));
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(long other)
    {
        if (IsNaN())
        {
            return -1;
        }
        if (IsPositiveInfinity())
        {
            return 1;
        }
        if (IsNegativeInfinity())
        {
            return -1;
        }

        // When the other value is zero, or this instance has no exponent, the exponent can be
        // disregarded.
        if (other == 0 || Exponent == 0)
        {
            return Mantissa.CompareTo(other);
        }

        // simple cases with opposite signs
        if (Mantissa < 0 && other > 0)
        {
            return -1;
        }
        if (other < 0 && Mantissa > 0)
        {
            return 1;
        }

        // account for comparisons involving negative exponents
        if (Exponent < -MantissaDigits)
        {
            return -1;
        }

        // in non-trivial cases, convert the other value to a Number, then compare
        return CompareTo((HugeNumber)other);
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(HugeNumber other)
    {
        if (IsNaN())
        {
            return other.IsNaN() ? 0 : -1;
        }
        if (other.IsNaN())
        {
            return 1;
        }

        // simple cases with opposite signs
        if (Mantissa < 0 && other.Mantissa > 0)
        {
            return -1;
        }
        if (other.Mantissa < 0 && Mantissa > 0)
        {
            return 1;
        }

        // When either value is zero, exponents can be disregarded; only the sign of the
        // mantissa matters for the purpose of comparison.
        if (Mantissa == 0 || other.Mantissa == 0)
        {
            var r = Mantissa.CompareTo(other.Mantissa);
            return r == 0
                ? Exponent.CompareTo(other.Exponent) // permit comparing 0 to -0
                : r;
        }

        if (IsPositiveInfinity())
        {
            return other.IsPositiveInfinity() ? 0 : 1;
        }
        if (other.IsPositiveInfinity())
        {
            return -1;
        }
        if (IsNegativeInfinity())
        {
            return other.IsNegativeInfinity() ? 0 : -1;
        }
        if (other.IsNegativeInfinity())
        {
            return 1;
        }

        // make the denominators agree (or remove them)
        // afterward they can be ignored
        var value = this;
        if ((Denominator > 1 || other.Denominator > 1)
            && Denominator != other.Denominator)
        {
            other = ToDenominator(other, Denominator);
            if (Denominator != other.Denominator)
            {
                value = ToDenominator(value, 1);
            }
        }

        var adjustedExponent = value.Exponent + (value.MantissaDigits - 1);
        var otherAdjustedExponent = other.Exponent + (other.MantissaDigits - 1);

        // comparison between equal exponents
        if (adjustedExponent == otherAdjustedExponent)
        {
            return value.Mantissa.CompareTo(other.Mantissa);
        }

        // comparison of exponents covers all other cases (mantissa comparison is
        // irrelevant when there is a difference in magnitude)
        if (adjustedExponent < 0)
        {
            if (otherAdjustedExponent < 0)
            {
                return adjustedExponent.CompareTo(otherAdjustedExponent);
            }
            return value.Mantissa > 0 ? -1 : 1;
        }
        if (otherAdjustedExponent < 0)
        {
            return value.Mantissa > 0 ? 1 : -1;
        }
        return adjustedExponent.CompareTo(otherAdjustedExponent) * (IsNegative() ? -1 : 1);
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="other">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="other"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="other"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="other"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="other"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(ulong other)
    {
        if (IsNaN())
        {
            return -1;
        }
        if (IsPositiveInfinity())
        {
            return 1;
        }
        if (IsNegativeInfinity())
        {
            return -1;
        }

        // When the other value is zero, or this instance has no exponent, the exponent can be
        // disregarded.
        if (other == 0 || Exponent == 0)
        {
            return Mantissa < 0
                ? -1
                : ((ulong)Mantissa).CompareTo(other);
        }

        // simple cases with opposite signs or negative exponents
        if (Mantissa < 0 || Exponent < -MantissaDigits)
        {
            return -1;
        }

        // in non-trivial cases convert the other value, then compare
        return CompareTo((HugeNumber)other);
    }

    /// <summary>
    /// Compares this instance to a specified object and returns an indication of their relative values.
    /// </summary>
    /// <param name="obj">An object to compare, or <see langword="null"/>.</param>
    /// <returns>
    /// A signed integer value that indicates the relationship of this instance to <paramref
    /// name="obj"/>, as shown in the following table.
    /// <list type="table">
    /// <listheader>
    /// <term>Return value</term>
    /// <term>Description</term>
    /// </listheader>
    /// <item>
    /// <term>Less than zero</term>
    /// <term>The current instance is less than <paramref name="obj"/>.</term>
    /// </item>
    /// <item>
    /// <term>Zero</term>
    /// <term>The current instance equals <paramref name="obj"/>.</term>
    /// </item>
    /// <item>
    /// <term>Greater than zero</term>
    /// <term>The current instance is greater than <paramref name="obj"/>.</term>
    /// </item>
    /// </list>
    /// </returns>
    /// <remarks>
    /// <para>If the current instance is <see cref="NaN"/>, -1 is returned.</para>
    /// <para>If <paramref name="obj"/> is <see langword="null"/>, 1 is returned.</para>
    /// </remarks>
    public int CompareTo(object? obj)
    {
        if (obj is HugeNumber other)
        {
            return CompareTo(other);
        }
        if (obj is long lValue)
        {
            return CompareTo(lValue);
        }
        if (obj is int iValue)
        {
            return CompareTo(iValue);
        }
        if (obj is double dValue)
        {
            return CompareTo(dValue);
        }
        if (obj is float fValue)
        {
            return CompareTo(fValue);
        }
        if (obj is string s)
        {
            return TryParse(s, out var v) ? CompareTo(v) : 1;
        }
        return 1;
    }

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode() => HashCode.Combine(Mantissa, Denominator, Exponent);

    /// <summary>
    /// Returns a value that indicates whether a <see cref="decimal"/> is less than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(decimal left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="double"/> is less than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(double left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit signed integer is less than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(long left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit unsigned integer is less than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(ulong left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(HugeNumber left, decimal right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(HugeNumber left, double right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a 64-bit
    /// signed integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(HugeNumber left, long right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a 64-bit
    /// unsigned integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(HugeNumber left, ulong right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is less than another
    /// <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <(HugeNumber left, HugeNumber right) => left.CompareTo(right) < 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="decimal"/> is greater than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(decimal left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="double"/> is greater than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(double left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit signed integer is greater than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(long left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit unsigned integer is greater than an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(ulong left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
    /// <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(HugeNumber left, decimal right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
    /// <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(HugeNumber left, double right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a 64-bit
    /// signed integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(HugeNumber left, long right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
    /// 64-bit unsigned integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(HugeNumber left, ulong right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is greater than
    /// another <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >(HugeNumber left, HugeNumber right) => left.CompareTo(right) > 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="decimal"/> is less than or equal to an
    /// <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(decimal left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="double"/> is less than or equal to an
    /// <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(double left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit signed integer is less than or equal to an <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(long left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit unsigned integer is less than or equal to
    /// an <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(ulong left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
    /// to a <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(HugeNumber left, decimal right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
    /// to a <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(HugeNumber left, double right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
    /// to a 64-bit signed integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(HugeNumber left, long right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
    /// to a 64-bit unsigned integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(HugeNumber left, ulong right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is less than or
    /// equal to another <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator <=(HugeNumber left, HugeNumber right) => left.CompareTo(right) <= 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="decimal"/> is greater than or equal
    /// to an <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(decimal left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

    /// <summary>
    /// Returns a value that indicates whether a <see cref="double"/> is greater than or equal
    /// to an <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(double left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit signed integer is greater than or equal
    /// to an <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(long left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

    /// <summary>
    /// Returns a value that indicates whether a 64-bit unsigned integer is greater than or equal
    /// to an <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(ulong left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
    /// <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(HugeNumber left, decimal right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
    /// <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(HugeNumber left, double right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a 64-bit
    /// signed integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(HugeNumber left, long right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than or
    /// equal to a 64-bit unsigned integer.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(HugeNumber left, ulong right) => left.CompareTo(right) >= 0;

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is greater than
    /// or equal to another <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
    /// name="right"/>; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator >=(HugeNumber left, HugeNumber right) => left.CompareTo(right) >= 0;
}
