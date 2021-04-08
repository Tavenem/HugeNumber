using System;

namespace Tavenem.HugeNumber
{
    public partial struct HugeNumber :
        IComparable,
        IComparable<double>,
        IComparable<float>,
        IComparable<int>,
        IComparable<long>,
        IComparable<HugeNumber>,
        IComparable<ulong>,
        IEquatable<double>,
        IEquatable<float>,
        IEquatable<int>,
        IEquatable<long>,
        IEquatable<HugeNumber>,
        IEquatable<ulong>
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
            if (IsNaN)
            {
                return -1;
            }
            if (IsPositiveInfinity)
            {
                return 1;
            }
            if (IsNegativeInfinity)
            {
                return -1;
            }

            // When the value is zero, exponent can be disregarded; only the sign of the mantissa
            // matters for the purpose of comparison.
            if (IsZero)
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
            if (IsNaN)
            {
                return double.IsNaN(other) ? 0 : -1;
            }
            if (double.IsNaN(other))
            {
                return 1;
            }
            if (IsPositiveInfinity)
            {
                return double.IsPositiveInfinity(other) ? 0 : 1;
            }
            if (double.IsPositiveInfinity(other))
            {
                return -1;
            }
            if (IsNegativeInfinity)
            {
                return double.IsNegativeInfinity(other) ? 0 : -1;
            }
            if (double.IsNegativeInfinity(other))
            {
                return 1;
            }

            // When the value is zero, exponent can be disregarded; only the sign of the mantissa
            // matters for the purpose of comparison.
            if (IsZero)
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
            if (IsNaN)
            {
                return float.IsNaN(other) ? 0 : -1;
            }
            if (float.IsNaN(other))
            {
                return 1;
            }
            if (IsPositiveInfinity)
            {
                return float.IsPositiveInfinity(other) ? 0 : 1;
            }
            if (float.IsPositiveInfinity(other))
            {
                return -1;
            }
            if (IsNegativeInfinity)
            {
                return float.IsNegativeInfinity(other) ? 0 : -1;
            }
            if (float.IsNegativeInfinity(other))
            {
                return 1;
            }

            // When the value is zero, exponent can be disregarded; only the sign of the mantissa
            // matters for the purpose of comparison.
            if (IsZero)
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
            if (IsNaN)
            {
                return -1;
            }
            if (IsPositiveInfinity)
            {
                return 1;
            }
            if (IsNegativeInfinity)
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
            if (IsNaN)
            {
                return -1;
            }
            if (IsPositiveInfinity)
            {
                return 1;
            }
            if (IsNegativeInfinity)
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
            if (IsNaN)
            {
                return other.IsNaN ? 0 : -1;
            }
            if (other.IsNaN)
            {
                return 1;
            }
            if (IsPositiveInfinity)
            {
                return other.IsPositiveInfinity ? 0 : 1;
            }
            if (other.IsPositiveInfinity)
            {
                return -1;
            }
            if (IsNegativeInfinity)
            {
                return other.IsNegativeInfinity ? 0 : -1;
            }
            if (other.IsNegativeInfinity)
            {
                return 1;
            }

            // When either value is zero, exponents can be disregarded; only the sign of the
            // mantissa matters for the purpose of comparison.
            if (IsZero || other.IsZero)
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
                return IsPositive ? -1 : 1;
            }
            if (otherAdjustedExponent < 0)
            {
                return IsPositive ? 1 : -1;
            }

            // direct comparison of exponents covers all other cases (mantissa comparison is
            // irrelevant when there is a difference in magnitude)
            return adjustedExponent.CompareTo(otherAdjustedExponent) * (IsNegative ? -1 : 1);
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
            if (IsNaN)
            {
                return -1;
            }
            if (IsPositiveInfinity)
            {
                return 1;
            }
            if (IsNegativeInfinity)
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
            if (!IsFinite)
            {
                return false;
            }
            return Equals((HugeNumber)other);
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref
        /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(double other)
        {
            if (IsNaN || double.IsNaN(other))
            {
                return false;
            }
            if (IsNegativeInfinity)
            {
                return double.IsNegativeInfinity(other);
            }
            if (IsPositiveInfinity)
            {
                return double.IsPositiveInfinity(other);
            }
            return Equals((HugeNumber)other);
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref
        /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(float other)
        {
            if (IsNaN || float.IsNaN(other))
            {
                return false;
            }
            if (IsNegativeInfinity)
            {
                return float.IsNegativeInfinity(other);
            }
            if (IsPositiveInfinity)
            {
                return float.IsPositiveInfinity(other);
            }
            return Equals((HugeNumber)other);
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref
        /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(int other)
        {
            if (!IsFinite)
            {
                return false;
            }
            return Mantissa == other;
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref
        /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(long other)
        {
            if (!IsFinite)
            {
                return false;
            }
            return Equals((HugeNumber)other);
        }

        /// <summary>Indicates whether the current object is equal to another object of the same type.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref
        /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(HugeNumber other)
        {
            if (IsNaN || other.IsNaN)
            {
                return false;
            }
            if (IsNegativeInfinity)
            {
                return other.IsNegativeInfinity;
            }
            if (IsPositiveInfinity)
            {
                return other.IsPositiveInfinity;
            }
            return Mantissa == other.Mantissa && Exponent == other.Exponent;
        }

        /// <summary>Indicates whether the current object is equal to another object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns><see langword="true"/> if the current object is equal to the <paramref
        /// name="other">other</paramref> parameter; otherwise, <see langword="false"/>.</returns>
        public bool Equals(ulong other)
        {
            if (!IsFinite)
            {
                return false;
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
            if (obj is string s)
            {
                return TryParse(s, out var v) && Equals(v);
            }
            return false;
        }

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A 32-bit signed integer that is the hash code for this instance.</returns>
        public override int GetHashCode()
            => (Exponent, IsNaN, IsNegativeInfinity, IsPositiveInfinity, Mantissa).GetHashCode();
    }
}
