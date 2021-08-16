namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the larger of two <see cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// The <paramref name="left"/> or <paramref name="right"/> parameter, whichever is larger.
    /// </returns>
    /// <remarks>
    /// If either value is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </remarks>
    public static HugeNumber Max(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }
        return left >= right ? left : right;
    }

    /// <summary>
    /// Returns the greater absolute value of two <see cref="HugeNumber"/>
    /// values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// The <paramref name="left"/> or <paramref name="right"/> parameter, whichever has the
    /// larger absolute value (magnitude).
    /// </returns>
    /// <remarks>
    /// <para>
    /// If either value is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If the values have equal absolute values, but one is negative, the positive value is
    /// considered greater.
    /// </para>
    /// </remarks>
    public static HugeNumber MaxMagnitude(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }
        var leftAbs = left.Abs();
        var rightAbs = right.Abs();
        if (leftAbs > rightAbs)
        {
            return left;
        }
        if (leftAbs == rightAbs)
        {
            return left < 0 ? right : left;
        }
        return right;
    }

    /// <summary>
    /// Returns the smaller of two <see cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// The <paramref name="left"/> or <paramref name="right"/> parameter, whichever is smaller.
    /// </returns>
    /// <remarks>
    /// If either value is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </remarks>
    public static HugeNumber Min(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }
        return left <= right ? left : right;
    }

    /// <summary>
    /// Returns the smaller absolute value of two <see cref="HugeNumber"/>
    /// values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// The <paramref name="left"/> or <paramref name="right"/> parameter, whichever has the
    /// smaller absolute value (magnitude).
    /// </returns>
    /// <remarks>
    /// <para>
    /// If either value is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If the values have equal absolute values, but one is negative, the negative value is
    /// considered smaller.
    /// </para>
    /// </remarks>
    public static HugeNumber MinMagnitude(HugeNumber left, HugeNumber right)
    {
        if (left.IsNaN() || right.IsNaN())
        {
            return NaN;
        }
        var leftAbs = left.Abs();
        var rightAbs = right.Abs();
        if (leftAbs < rightAbs)
        {
            return left;
        }
        if (leftAbs == rightAbs)
        {
            return left < 0 ? left : right;
        }
        return right;
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
        if (Exponent < 0)
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

        // When either value is zero, exponents can be disregarded; only the sign of the
        // mantissa matters for the purpose of comparison.
        if (Mantissa == 0 || other.Mantissa == 0)
        {
            return Mantissa.CompareTo(other.Mantissa);
        }

        var adjustedExponent = Exponent + (MantissaDigits - 1);
        var otherAdjustedExponent = other.Exponent + (other.MantissaDigits - 1);

        // simple comparison between equal exponents
        if (adjustedExponent == otherAdjustedExponent)
        {
            var adjustedMantissa = Mantissa * Math.Pow(10, Exponent);
            var otherAdjustedMantissa = other.Mantissa * Math.Pow(10, other.Exponent);
            return adjustedMantissa.CompareTo(otherAdjustedMantissa);
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

        // account for comparisons involving negative exponents
        if (adjustedExponent < 0)
        {
            if (otherAdjustedExponent < 0)
            {
                return adjustedExponent.CompareTo(otherAdjustedExponent);
            }
            return IsPositive() ? -1 : 1;
        }
        if (otherAdjustedExponent < 0)
        {
            return IsPositive() ? 1 : -1;
        }

        // direct comparison of exponents covers all other cases (mantissa comparison is
        // irrelevant when there is a difference in magnitude)
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

        // in non-trivial cases, convert the other value to a Number, then compare
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

    /// <summary>Indicates whether the current object is equal to another object.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(decimal other)
    {
        if (!IsFinite())
        {
            return false;
        }
        if (other == 0)
        {
            return Mantissa == 0;
        }
        return Equals((HugeNumber)other);
    }

    /// <summary>Indicates whether the current object is equal to another object.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(double other)
    {
        if (IsNaN() || double.IsNaN(other))
        {
            return false;
        }
        if (IsNegativeInfinity())
        {
            return double.IsNegativeInfinity(other);
        }
        if (IsPositiveInfinity())
        {
            return double.IsPositiveInfinity(other);
        }
        if (other == 0)
        {
            return Mantissa == 0;
        }
        return Equals((HugeNumber)other);
    }

    /// <summary>Indicates whether the current object is equal to another object.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(float other)
    {
        if (IsNaN() || float.IsNaN(other))
        {
            return false;
        }
        if (IsNegativeInfinity())
        {
            return float.IsNegativeInfinity(other);
        }
        if (IsPositiveInfinity())
        {
            return float.IsPositiveInfinity(other);
        }
        if (other == 0)
        {
            return Mantissa == 0;
        }
        return Equals((HugeNumber)other);
    }

    /// <summary>Indicates whether the current object is equal to another object.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(int other)
    {
        if (!IsFinite())
        {
            return false;
        }
        return Exponent == 0 && Mantissa == other;
    }

    /// <summary>Indicates whether the current object is equal to another object.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(long other)
    {
        if (!IsFinite())
        {
            return false;
        }
        if (other == 0)
        {
            return Mantissa == 0;
        }
        return Equals((HugeNumber)other);
    }

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(HugeNumber other)
    {
        if (IsNaN() || other.IsNaN())
        {
            return false;
        }
        if (IsNegativeInfinity())
        {
            return other.IsNegativeInfinity();
        }
        if (IsPositiveInfinity())
        {
            return other.IsPositiveInfinity();
        }
        return Mantissa == other.Mantissa && Exponent == other.Exponent;
    }

    /// <summary>Indicates whether the current object is equal to another object.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(ulong other)
    {
        if (!IsFinite())
        {
            return false;
        }
        if (other == 0)
        {
            return Mantissa == 0;
        }
        return Equals((HugeNumber)other);
    }

    /// <summary>Indicates whether this instance and a specified object are equal.</summary>
    /// <param name="obj">The object to compare with the current instance.</param>
    /// <returns><see langword="true"/> if <paramref name="obj">obj</paramref> and this instance
    /// are the same type and represent the same value; otherwise, <see
    /// langword="false"/>.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is HugeNumber other)
        {
            return Equals(other);
        }
        if (obj is long lValue)
        {
            return Equals(lValue);
        }
        if (obj is int iValue)
        {
            return Equals(iValue);
        }
        if (obj is double dValue)
        {
            return Equals(dValue);
        }
        if (obj is float fValue)
        {
            return Equals(fValue);
        }
        if (obj is byte bValue)
        {
            return Equals(bValue);
        }
        if (obj is short sValue)
        {
            return Equals(sValue);
        }
        if (obj is nint nValue)
        {
            return Equals(nValue);
        }
        if (obj is sbyte sbValue)
        {
            return Equals(sbValue);
        }
        if (obj is ushort usValue)
        {
            return Equals(usValue);
        }
        if (obj is uint uiValue)
        {
            return Equals(uiValue);
        }
        if (obj is ulong ulValue)
        {
            return Equals(ulValue);
        }
        if (obj is nuint nuValue)
        {
            return Equals(nuValue);
        }
        if (obj is string s)
        {
            return TryParse(s, out var sv)
                && Equals(sv);
        }
        if (obj is IFormattable f)
        {
            return TryParse(f.ToString(null, null), out var fv)
                && Equals(fv);
        }
        return TryParse(obj?.ToString(), out var v)
            && Equals(v);
    }

    /// <summary>Returns the hash code for this instance.</summary>
    /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
    public override int GetHashCode()
        => (Exponent, IsNaN(), IsNegativeInfinity(), IsPositiveInfinity(), Mantissa).GetHashCode();

    /// <summary>
    /// Returns a value that indicates whether a <see cref="decimal"/> value and an <see
    /// cref="HugeNumber"/> value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(decimal left, HugeNumber right) => right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether a <see cref="double"/> value and an <see
    /// cref="HugeNumber"/> value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(double left, HugeNumber right) => right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether a signed <see cref="long"/> integer value and
    /// an <see cref="HugeNumber"/> value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(long left, HugeNumber right) => right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether an unsigned <see cref="long"/> integer value and
    /// an <see cref="HugeNumber"/> value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(ulong left, HugeNumber right) => right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
    /// cref="decimal"/> value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(HugeNumber left, decimal right) => left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
    /// cref="double"/> value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(HugeNumber left, double right) => left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a signed <see
    /// cref="long"/> integer value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(HugeNumber left, long right) => left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and an unsigned
    /// 64-bit integer value are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(HugeNumber left, ulong right) => left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether the values of two <see cref="HugeNumber"/> objects are equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
    /// parameters have the same value; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator ==(HugeNumber left, HugeNumber right) => left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether a <see cref="decimal"/> and an <see
    /// cref="HugeNumber"/> value are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(decimal left, HugeNumber right) => !right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether a <see cref="double"/> and an <see
    /// cref="HugeNumber"/> value are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(double left, HugeNumber right) => !right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether a 64-bit signed integer and an <see
    /// cref="HugeNumber"/> value are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(long left, HugeNumber right) => !right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether a 64-bit unsigned integer and an <see
    /// cref="HugeNumber"/> value are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(ulong left, HugeNumber right) => !right.Equals(left);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
    /// cref="decimal"/> are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(HugeNumber left, decimal right) => !left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
    /// cref="double"/> are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(HugeNumber left, double right) => !left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a 64-bit signed
    /// integer are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(HugeNumber left, long right) => !left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a 64-bit unsigned
    /// integer are not equal.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(HugeNumber left, ulong right) => !left.Equals(right);

    /// <summary>
    /// Returns a value that indicates whether the two <see cref="HugeNumber"/> objects have
    /// different values.
    /// </summary>
    /// <param name="left">The first value to compare.</param>
    /// <param name="right">The second value to compare.</param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
    /// not equal; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool operator !=(HugeNumber left, HugeNumber right) => !left.Equals(right);

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
