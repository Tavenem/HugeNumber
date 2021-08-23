using Tavenem.Mathematics;

namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the angle whose cosine is the specified number.
    /// </summary>
    /// <param name="value">A number representing a cosine, where <paramref name="value"/> must
    /// be greater than or equal to -1, but less than or equal to 1.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π/2 ≤θ≤ π/2.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> &lt; -1 or <paramref name="value"/> &gt; 1
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
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster
    /// to perform two boxing conversions and use the native <see cref="Math"/> operation
    /// instead (e.g. <c>(Number)Math.Acos((double)value)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Acos(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }

        return 2 * Atan((1 - value.Square()).Sqrt() / (1 + value));
    }

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
    /// Returns the angle whose sine is the specified number.
    /// </summary>
    /// <param name="value">A number representing a sine, where <paramref name="value"/> must be
    /// greater than or equal to -1, but less than or equal to 1.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π/2 ≤θ≤ π/2.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> &lt; -1 or <paramref name="value"/> &gt; 1
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
    /// (e.g. <c>(Number)Math.Asin((double)value)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Asin(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }

        return Atan(value / (1 - value.Square()).Sqrt());
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
    /// Returns the angle whose tangent is the specified number.
    /// </summary>
    /// <param name="value">A number representing a tangent.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π/2 ≤θ≤ π/2.
    /// </para>
    /// <para>
    /// -or-
    /// </para>
    /// <para>
    /// <see cref="NaN"/> if <paramref name="value"/> equals <see cref="NaN"/>, -π/2 if
    /// <paramref name="value"/> equals <see cref="NegativeInfinity"/>, or π/2 if <paramref
    /// name="value"/> equals <see cref="PositiveInfinity"/>.
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
    /// (e.g. <c>(Number)Math.Atan((double)value)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Atan(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return NaN;
        }
        if (value.Mantissa == 0)
        {
            return Zero;
        }
        if (value.IsNegativeInfinity())
        {
            return -HugeNumberConstants.HalfPi;
        }
        if (value.IsPositiveInfinity())
        {
            return HugeNumberConstants.HalfPi;
        }
        if (value < 0)
        {
            return -Atan(-value);
        }
        if (value > 1)
        {
            return HugeNumberConstants.HalfPi - Atan(1 / value);
        }
        if (value == One)
        {
            return HugeNumberConstants.QuarterPi;
        }
        if (value > _TwoMinusRoot3)
        {
            return HugeNumberConstants.SixthPi + Atan(((_Root3 * value) - 1) / (_Root3 + value));
        }

        var subtract = true;
        var power = value.Cube();
        var square = value.Square();
        var denominator = new HugeNumber(3);
        var result = value - (power / denominator);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            denominator += 2;

            if (subtract)
            {
                result -= power / denominator;
            }
            else
            {
                result += power / denominator;
            }
        }
        while (lastResult != result);

        return result;
    }

    /// <summary>
    /// Returns the angle whose tangent is the quotient of two specified numbers.
    /// </summary>
    /// <param name="y">The y coordinate of a point.</param>
    /// <param name="x">The x coordinate of a point.</param>
    /// <returns>
    /// <para>
    /// An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = <paramref name="y"/> /
    /// <paramref name="x"/>, where (<paramref name="x"/>, <paramref name="y"/>) is a point in
    /// the Cartesian plane. Observe the following:
    /// </para>
    /// <list type="bullet">
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 1, 0 &lt; θ
    /// &lt; π/2.</description>
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 2, π/2 &lt;
    /// θ ≤ π.</description>
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 3, -π &lt; θ
    /// &lt; -π/2.</description>
    /// <description>For (<paramref name="x"/>, <paramref name="y"/>) in quadrant 4, -π/2 &lt; θ
    /// &lt; 0.</description>
    /// </list>
    /// <para>
    /// For points on the boundaries of the quadrants, the return value is the following:
    /// </para>
    /// <list type="bullet">
    /// <description>If y is 0 and x is not negative, θ = 0.</description>
    /// <description>If y is 0 and x is negative, θ = π.</description>
    /// <description>If y is positive and x is 0, θ = π/2.</description>
    /// <description>If y is negative and x is 0, θ = -π/2.</description>
    /// <description>If y is 0 and x is 0, θ = 0.</description>
    /// </list>
    /// <para>
    /// The results when either value is infinity vary.
    /// </para>
    /// <list type="table">
    /// <listheader>
    /// <term>Case</term>
    /// <description>Result</description>
    /// </listheader>
    /// <item>
    /// <term>x is finite, y is <see cref="PositiveInfinity"/></term>
    /// <description>0</description>
    /// </item>
    /// <item>
    /// <term>x is finite, y is <see cref="NegativeInfinity"/></term>
    /// <description>π</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="PositiveInfinity"/>, y is finite and non-zero</term>
    /// <description>π/2</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="NegativeInfinity"/>, y is finite and non-zero</term>
    /// <description>-π/2</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="PositiveInfinity"/>, y is <see cref="PositiveInfinity"/></term>
    /// <description>π/4</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="NegativeInfinity"/>, y is <see cref="PositiveInfinity"/></term>
    /// <description>-π/4</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="PositiveInfinity"/>, y is <see cref="NegativeInfinity"/></term>
    /// <description>3π/4</description>
    /// </item>
    /// <item>
    /// <term>x is <see cref="NegativeInfinity"/>, y is <see cref="NegativeInfinity"/></term>
    /// <description>-3π/4</description>
    /// </item>
    /// </list>
    /// <para>
    /// If <paramref name="x"/> or <paramref name="y"/> is <see cref="NaN"/>, or if <paramref
    /// name="x"/> and <paramref name="y"/> are either <see cref="PositiveInfinity"/> or <see
    /// cref="NegativeInfinity"/>, the method returns <see cref="NaN"/>.
    /// </para>
    /// </returns>
    /// <remarks>
    /// <para>
    /// The return value is the angle in the Cartesian plane formed by the x-axis, and a vector
    /// starting from the origin, (0,0), and terminating at the point, (x,y).
    /// </para>
    /// <para>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Atan2((double)y, (double)x)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Atan2(HugeNumber y, HugeNumber x)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0)
        {
            return y >= 0 ? 0 : Pi;
        }
        if (y.Mantissa == 0)
        {
            return x >= 0 ? HugeNumberConstants.HalfPi : -HugeNumberConstants.HalfPi;
        }
        if (y.IsPositiveInfinity())
        {
            if (x.IsPositiveInfinity())
            {
                return HugeNumberConstants.QuarterPi;
            }
            else if (x.IsNegativeInfinity())
            {
                return -HugeNumberConstants.QuarterPi;
            }
            else
            {
                return Zero;
            }
        }
        if (y.IsNegativeInfinity())
        {
            if (x.IsPositiveInfinity())
            {
                return HugeNumberConstants.ThreeQuartersPi;
            }
            else if (x.IsNegativeInfinity())
            {
                return -HugeNumberConstants.ThreeQuartersPi;
            }
            else if (x >= 0)
            {
                return Pi;
            }
            else
            {
                return -Pi;
            }
        }
        if (x.IsPositiveInfinity())
        {
            return HugeNumberConstants.HalfPi;
        }
        if (x.IsNegativeInfinity())
        {
            return -HugeNumberConstants.HalfPi;
        }
        if (x > 0)
        {
            return Atan(y / x);
        }
        if (y > 0)
        {
            return Atan(y / x) + Pi;
        }
        return Atan(y / x) - Pi;
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
    /// Returns the cosine of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The cosine of <paramref name="value"/>. If <paramref name="value"/> is equal to
    /// <see cref="NaN"/>, <see cref="NegativeInfinity"/>, or <see cref="PositiveInfinity"/>,
    /// this method returns <see cref="NaN"/>.</returns>
    /// <remarks>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Cos((double)value)</c>).
    /// </remarks>
    public static HugeNumber Cos(HugeNumber value)
    {
        if (value.IsNaN()
            || value.IsInfinity())
        {
            return NaN;
        }
        if (value < 0)
        {
            return Cos(-value);
        }
        if (value > HugeNumberConstants.TwoPi)
        {
            return Cos(value - (HugeNumberConstants.TwoPi * (value / HugeNumberConstants.TwoPi).Floor()));
        }
        if (value > HugeNumberConstants.ThreeHalvesPi)
        {
            return Cos(HugeNumberConstants.TwoPi - value);
        }
        if (value > Pi)
        {
            return -Cos(value - Pi);
        }
        if (value > HugeNumberConstants.HalfPi)
        {
            return -Cos(Pi - value);
        }
        if (value > HugeNumberConstants.QuarterPi)
        {
            return Sin(HugeNumberConstants.HalfPi - value);
        }

        var subtract = true;
        var square = value.Square();
        var power = square;
        var factorialDigit = 2;
        var factorial = new HugeNumber(2);
        var result = 1 - (power / factorial);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            factorial *= ++factorialDigit;
            factorial *= ++factorialDigit;

            if (subtract)
            {
                result -= power / factorial;
            }
            else
            {
                result += power / factorial;
            }
        }
        while (lastResult != result);

        return result;
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
    /// Returns the sine of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The sine of <paramref name="value"/>. If <paramref name="value"/> is equal to
    /// <see cref="NaN"/>, <see cref="NegativeInfinity"/>, or <see cref="PositiveInfinity"/>,
    /// this method returns <see cref="NaN"/>.</returns>
    /// <remarks>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Sin((double)value)</c>).
    /// </remarks>
    public static HugeNumber Sin(HugeNumber value)
    {
        if (value.IsNaN()
            || value.IsInfinity())
        {
            return NaN;
        }
        if (value < 0)
        {
            return -Sin(-value);
        }
        if (value > HugeNumberConstants.TwoPi)
        {
            return Sin(value - (HugeNumberConstants.TwoPi * (value / HugeNumberConstants.TwoPi).Floor()));
        }
        if (value > HugeNumberConstants.ThreeHalvesPi)
        {
            return -Sin(HugeNumberConstants.TwoPi - value);
        }
        if (value > Pi)
        {
            return -Sin(value - Pi);
        }
        if (value > HugeNumberConstants.HalfPi)
        {
            return Sin(Pi - value);
        }
        if (value > HugeNumberConstants.QuarterPi)
        {
            return Cos(HugeNumberConstants.HalfPi - value);
        }

        var subtract = true;
        var power = value.Cube();
        var square = value.Square();
        var factorialDigit = 3;
        var factorial = new HugeNumber(6);
        var result = value - (power / factorial);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            subtract = !subtract;
            power *= square;
            factorial *= ++factorialDigit;
            factorial *= ++factorialDigit;

            if (subtract)
            {
                result -= power / factorial;
            }
            else
            {
                result += power / factorial;
            }
        }
        while (lastResult != result);

        return result;
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
    /// Returns the tangent of the specified angle.
    /// </summary>
    /// <param name="value">An angle, measured in radians.</param>
    /// <returns>The tangent of <paramref name="value"/>. If <paramref name="value"/> is equal to
    /// <see cref="NaN"/>, <see cref="NegativeInfinity"/>, or <see cref="PositiveInfinity"/>,
    /// this method returns <see cref="NaN"/>.</returns>
    /// <remarks>
    /// This method directly computes the result to a high degree of precision. It does not take
    /// advantage of hardware implementations or optimizations. If the high precision of <see
    /// cref="HugeNumber"/> is not required or performance is a concern, it will be much faster to
    /// perform two boxing conversions and use the native <see cref="Math"/> operation instead
    /// (e.g. <c>(Number)Math.Tan((double)value)</c>).
    /// </remarks>
    public static HugeNumber Tan(HugeNumber value)
    {
        if (value.IsNaN()
            || value.IsInfinity())
        {
            return NaN;
        }
        if (value < 0)
        {
            return -Tan(-value);
        }
        if (value > Pi)
        {
            return Tan(value - (Pi * (value / Pi).Floor()));
        }
        if (value > HugeNumberConstants.ThreeQuartersPi)
        {
            return 1 / -Tan(Pi - value);
        }
        if (value > HugeNumberConstants.HalfPi)
        {
            return -Tan(Pi - value);
        }
        if (value > HugeNumberConstants.QuarterPi)
        {
            return 1 / Tan(HugeNumberConstants.HalfPi - value);
        }
        if (value > HugeNumberConstants.EighthPi)
        {
            var tanHalf = Tan(value / 2);
            return 2 * tanHalf / (1 - (tanHalf * tanHalf));
        }

        var taylorSeriesIndex = 0;
        var power = value.Cube();
        var square = value.Square();
        var result = value + (_TangentTaylorSeries[taylorSeriesIndex] * power);
        HugeNumber lastResult;
        do
        {
            lastResult = result;

            power *= square;
            result += power * _TangentTaylorSeries[++taylorSeriesIndex];
        }
        while (lastResult != result && taylorSeriesIndex < _TangentTaylorSeries.Length - 1);

        return result;
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
