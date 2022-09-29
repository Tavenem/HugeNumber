namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the angle whose hyberbolic cosine is the specified number.
    /// </summary>
    /// <param name="value">A number representing a hyberbolic cosine, where <paramref
    /// name="value"/> must be greater than or equal to 1.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> &lt; 1 or <paramref name="value"/> equals
    /// <see cref="NaN"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// A positive return value represents a counterclockwise angle from the x-axis; a negative
    /// return value represents a clockwise angle. Multiply the return value by 180/<see
    /// cref="Pi"/> to convert from radians to degrees.
    /// </para>
    /// <para>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Acosh((double)value)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Acosh(HugeNumber value)
    {
        if (value.IsNaN()
            || value < 1)
        {
            return NaN;
        }

        return Log(value + (value.Square() - 1).Sqrt());
    }

    /// <summary>
    /// Returns the angle whose hyberbolic sine is the specified number.
    /// </summary>
    /// <param name="value">A number representing a hyberbolic sine.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> equals <see cref="NaN"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// A positive return value represents a counterclockwise angle from the x-axis; a negative
    /// return value represents a clockwise angle. Multiply the return value by 180/<see
    /// cref="Pi"/> to convert from radians to degrees.
    /// </para>
    /// <para>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Asinh((double)value)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Asinh(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.IsInfinity())
        {
            return value;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }

        return value.Sign() * Log(value.Abs() + (value.Square() + 1).Sqrt());
    }

    /// <summary>
    /// Returns the angle whose hyberbolic tangent is the specified number.
    /// </summary>
    /// <param name="value">A number representing a hyberbolic tangent, where <paramref
    /// name="value"/> must be greater than or equal to -1, but less than or equal to 1.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> &lt; 1 or <paramref name="value"/> &gt; 1
    /// or <paramref name="value"/> equals <see cref="NaN"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// A positive return value represents a counterclockwise angle from the x-axis; a negative
    /// return value represents a clockwise angle. Multiply the return value by 180/<see
    /// cref="Pi"/> to convert from radians to degrees.
    /// </para>
    /// <para>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Atanh((double)value)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Atanh(HugeNumber value)
    {
        if (value.IsNaN()
            || value < -1
            || value > 1)
        {
            return NaN;
        }
        if (value == 1)
        {
            return PositiveInfinity;
        }
        if (value == -1)
        {
            return NegativeInfinity;
        }
        if (value < 0)
        {
            return -Atanh(-value);
        }

        return Log((1 + value) / (1 - value)) / 2;
    }

    /// <summary>
    /// Returns the hyperbolic cosine of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The hyperbolic cosine of <paramref name="value"/>. If <paramref name="value"/>
    /// is equal to <see cref="NegativeInfinity"/>, or <see cref="PositiveInfinity"/>, <see
    /// cref="PositiveInfinity"/> is returned. If <paramref name="value"/> is equal to <see
    /// cref="NaN"/>, <see cref="NaN"/> is returned.</returns>
    /// <remarks>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Cosh((double)value)</c>).
    /// </remarks>
    public static HugeNumber Cosh(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.IsInfinity())
        {
            return PositiveInfinity;
        }
        if (value.Mantissa == 0)
        {
            return One;
        }
        if (value < 0)
        {
            return Cosh(-value);
        }

        return (Exp(value) + Exp(-value)) / 2;
    }

    /// <summary>
    /// Returns the hyperbolic sine of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The hyperbolic sine of <paramref name="value"/>. If <paramref name="value"/>
    /// is equal to <see cref="NegativeInfinity"/>, <see cref="PositiveInfinity"/>, or <see
    /// cref="NaN"/>, the <paramref name="value"/> is returned.
    /// </returns>
    /// <remarks>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Sinh((double)value)</c>).
    /// </remarks>
    public static HugeNumber Sinh(HugeNumber value)
    {
        if (value.IsNaN()
            || value.IsInfinity())
        {
            return value;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }
        if (value < 0)
        {
            return -Sinh(-value);
        }

        return (Exp(value) - Exp(-value)) / 2;
    }

    /// <summary>
    /// Returns the hyperbolic tangent of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The hyperbolic sine of <paramref name="value"/>. If <paramref name="value"/>
    /// is equal to <see cref="NegativeInfinity"/>, this method returns -1. If <paramref
    /// name="value"/> is equal to <see cref="PositiveInfinity"/>, this method returns 1. If
    /// <paramref name="value"/> is equal to <see cref="NaN"/>, this method returns <see
    /// cref="NaN"/>.
    /// </returns>
    /// <remarks>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Tanh((double)value)</c>).
    /// </remarks>
    public static HugeNumber Tanh(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.IsNegativeInfinity())
        {
            return -1;
        }
        if (value.IsPositiveInfinity())
        {
            return 1;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }
        if (value < 0)
        {
            return -Tanh(-value);
        }

        var exp = Exp(value);
        var negativeExp = Exp(-value);
        return (exp - negativeExp) / (exp + negativeExp);
    }
}
