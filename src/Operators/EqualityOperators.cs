namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
        return Mantissa == other
            && Denominator == 1
            && Exponent == 0;
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
        return Mantissa == other
            && Denominator == 1
            && Exponent == 0;
    }

    /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
    /// <param name="other">An object to compare with this object.</param>
    /// <returns><see langword="true"/> if the current object is equal to the <paramref
    /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
    public bool Equals(HugeNumber other) => !IsNaN()
        && !other.IsNaN()
        && Mantissa == other.Mantissa && Denominator == other.Denominator && Exponent == other.Exponent;

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
}
