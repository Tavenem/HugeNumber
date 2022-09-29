namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Clamps a value to an inclusive minimum and maximum value.
    /// </summary>
    /// <param name="value">The value to clamp.</param>
    /// <param name="min">
    /// The inclusive minimum to which <paramref name="value"/> should clamp.
    /// </param>
    /// <param name="max">
    /// The inclusive maximum to which <paramref name="value"/> should clamp.
    /// </param>
    /// <returns>
    /// The result of clamping value to the inclusive range of min and max.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="min"/> is greater than <paramref name="max"/>.
    /// </exception>
    public static HugeNumber Clamp(HugeNumber value, HugeNumber min, HugeNumber max)
    {
        if (min > max)
        {
            throw new ArgumentException($"{nameof(min)} is greater than {nameof(max)}", nameof(min));
        }
        if (value < min)
        {
            return min;
        }
        else if (value > max)
        {
            return max;
        }
        else
        {
            return value;
        }
    }

    /// <summary>
    /// Clamps this value to an inclusive minimum and maximum value.
    /// </summary>
    /// <param name="min">
    /// The inclusive minimum to which this value should clamp.
    /// </param>
    /// <param name="max">
    /// The inclusive maximum to which this value should clamp.
    /// </param>
    /// <returns>
    /// The result of clamping value to the inclusive range of min and max.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="min"/> is greater than <paramref name="max"/>.
    /// </exception>
    public HugeNumber Clamp(HugeNumber min, HugeNumber max) => Clamp(this, min, max);

    /// <summary>
    /// Copies the sign of a value to the sign of another value.
    /// </summary>
    /// <param name="value">The value whose magnitude is used in the result.</param>
    /// <param name="sign">The value whose sign is used in the result.</param>
    /// <returns>
    /// A value with the magnitude of <paramref name="value"/> and the sign of <paramref name="sign"/>.
    /// </returns>
    public static HugeNumber CopySign(HugeNumber value, HugeNumber sign)
    {
        if (sign.IsNegative())
        {
            if (value.IsNegative())
            {
                return value;
            }
            else
            {
                return value.Negate();
            }
        }
        else if (value.IsNegative())
        {
            return value.Negate();
        }
        else
        {
            return value;
        }
    }

    /// <summary>
    /// Copies the sign of a value to the sign of this value.
    /// </summary>
    /// <param name="sign">The value whose sign is used in the result.</param>
    /// <returns>
    /// A value with the magnitude of this value and the sign of <paramref name="sign"/>.
    /// </returns>
    public HugeNumber CopySign(HugeNumber sign) => CopySign(this, sign);

    /// <summary>
    /// Compares two values to compute which is greater.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is greater than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// This method matches the IEEE 754:2019 <c>maximum</c> function. This requires NaN inputs to
    /// be propagated back to the caller and for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static HugeNumber Max(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        if (x > y
            || (x == y
            && (y < 0
            || (y.IsZero() && y.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    /// <summary>
    /// Compares two values to compute which is greater and returning the other value if an input is
    /// <c>NaN</c>.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is greater than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// This method matches the IEEE 754:2019 <c>maximumNumber</c> function. This requires NaN
    /// inputs to not be propagated back to the caller and for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static HugeNumber MaxNumber(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN())
        {
            return y;
        }
        if (y.IsNaN())
        {
            return x;
        }
        if (x > y
            || (x == y
            && (y < 0
            || (y.IsZero() && y.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    /// <summary>
    /// Compares two values to compute which is lesser.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is less than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// This method matches the IEEE 754:2019 <c>minimum</c> function. This requires NaN inputs to
    /// be propagated back to the caller and for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static HugeNumber Min(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        if (x < y
            || (x == y
            && (x < 0
            || (x.IsZero() && x.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    /// <summary>
    /// Compares two values to compute which is lesser and returning the other value if an input is
    /// <c>NaN</c>.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is less than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// This method matches the IEEE 754:2019 <c>minimumNumber</c> function. This requires NaN
    /// inputs to not be propagated back to the caller and for -0.0 to be treated as less than +0.0.
    /// </remarks>
    public static HugeNumber MinNumber(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN())
        {
            return y;
        }
        if (y.IsNaN())
        {
            return x;
        }
        if (x < y
            || (x == y
            && (x < 0
            || (x.IsZero() && x.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    /// <summary>
    /// Computes the sign of a value.
    /// </summary>
    /// <param name="value">The value whose sign is to be computed.</param>
    /// <returns>
    /// <para>
    /// <see cref="One"/> if <paramref name="value"/> is positive, <see cref="Zero"/> if <paramref
    /// name="value"/> is zero, and <see cref="NegativeOne"/> if <paramref name="value"/> is
    /// negative.
    /// </para>
    /// <para>
    /// Returns <see cref="NaN"/> if <paramref name="value"/> is <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public static HugeNumber Sign(HugeNumber value)
    {
        if (value.IsZero())
        {
            return Zero;
        }
        if (value.IsPositive())
        {
            return One;
        }
        if (value.IsNegative())
        {
            return NegativeOne;
        }
        return NaN;
    }

    /// <summary>
    /// Computes the sign of this value.
    /// </summary>
    /// <returns>
    /// <para>
    /// <see cref="One"/> if this value is positive, <see cref="Zero"/> if this value is zero, and
    /// <see cref="NegativeOne"/> if this value is negative.
    /// </para>
    /// <para>
    /// Returns <see cref="NaN"/> if this value is <see cref="NaN"/>.
    /// </para>
    /// </returns>
    public HugeNumber Sign() => Sign(this);
}
