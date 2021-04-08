using System;

namespace Tavenem.HugeNumber
{
#pragma warning disable CS0419
    public partial struct HugeNumber
    {
        /// <summary>
        /// Represents the natural logarithmic base, specified by the constant, e, rounded to 18
        /// places of precision, which is the limit of the <see cref="HugeNumber"/> structure.
        /// </summary>
        public static readonly HugeNumber e = new(271828182845904524, -17);

        /// <summary>
        /// Represents the ratio of the circumference of a circle to its diameter, specified by the
        /// constant, π, rounded to 18 places of precision, which is the limit of the <see
        /// cref="HugeNumber"/> structure.
        /// </summary>
        public static readonly HugeNumber PI = new(314159265358979324, -17);

        /// <summary>
        /// 1/8π
        /// </summary>
        public static readonly HugeNumber EighthPI = PI / new HugeNumber(8);

        /// <summary>
        /// 1/e
        /// </summary>
        public static readonly HugeNumber InverseE = One / e;

        /// <summary>
        /// 4π
        /// </summary>
        public static readonly HugeNumber FourPI = PI * new HugeNumber(4);

        /// <summary>
        /// π+⅓π
        /// </summary>
        public static readonly HugeNumber FourThirdsPI = FourPI * Third;

        /// <summary>
        /// ½π
        /// </summary>
        public static readonly HugeNumber HalfPI = PI * Half;

        /// <summary>
        /// 1/π
        /// </summary>
        public static readonly HugeNumber InversePI = One / PI;

        /// <summary>
        /// The natural logarithm of 2, rounded to 18 places of precision, which is the limit of
        /// the <see cref="HugeNumber"/> structure.
        /// </summary>
        public static readonly HugeNumber Ln2 = new(693147180559945309, -18);

        /// <summary>
        /// The natural logarithm of 10, rounded to 18 places of precision, which is the limit of
        /// the <see cref="HugeNumber"/> structure.
        /// </summary>
        public static readonly HugeNumber Ln10 = new(230258509299404568, -17);

        /// <summary>
        /// 180/π
        /// </summary>
        public static readonly HugeNumber OneEightyOverPI = new HugeNumber(180) / PI;

        /// <summary>
        /// Represents the golden ratio, specified by the constant, φ, rounded to 18 places of
        /// precision, which is the limit of the <see cref="HugeNumber"/> structure.
        /// </summary>
        public static readonly HugeNumber phi = new(161803398874989485, -17);

        /// <summary>
        /// π/180
        /// </summary>
        public static readonly HugeNumber PIOver180 = PI / new HugeNumber(180);

        /// <summary>
        /// π²
        /// </summary>
        public static readonly HugeNumber PISquared = PI * PI;

        /// <summary>
        /// √2 rounded to 18 places of precision, which is the limit of the <see cref="HugeNumber"/>
        /// structure.
        /// </summary>
        public static readonly HugeNumber Root2 = new(141421356237309505, -17);

        /// <summary>
        /// ¼π
        /// </summary>
        public static readonly HugeNumber QuarterPI = PI / new HugeNumber(4);

        /// <summary>
        /// 1/6π
        /// </summary>
        public static readonly HugeNumber SixthPI = PI / new HugeNumber(6);

        /// <summary>
        /// ⅓π
        /// </summary>
        public static readonly HugeNumber ThirdPI = PI * Third;

        /// <summary>
        /// 3π
        /// </summary>
        public static readonly HugeNumber ThreePI = PI * new HugeNumber(3);

        /// <summary>
        /// 3/2π
        /// </summary>
        public static readonly HugeNumber ThreeHalvesPI = ThreePI * Half;

        /// <summary>
        /// 3/4π
        /// </summary>
        public static readonly HugeNumber ThreeQuartersPI = ThreePI / new HugeNumber(4);

        /// <summary>
        /// 2π
        /// </summary>
        public static readonly HugeNumber TwoPI = PI * new HugeNumber(2);

        /// <summary>
        /// 2π²
        /// </summary>
        public static readonly HugeNumber TwoPISquared = new HugeNumber(2) * PISquared;

        private static readonly HugeNumber[] _TangentTaylorSeries = new HugeNumber[]
        {
            Third,
            new HugeNumber(2) / new HugeNumber(15),
            new HugeNumber(17) / new HugeNumber(315),
            new HugeNumber(62) / new HugeNumber(2835),
        };
        private static readonly HugeNumber _Root3 = new HugeNumber(3).Sqrt();
        private static readonly HugeNumber _TwoMinusRoot3 = 2 - _Root3;

        /// <summary>
        /// Gets the absolute value of a <see cref="HugeNumber"/> object.
        /// </summary>
        /// <param name="value">A number.</param>
        /// <returns>The absolute value of <paramref name="value"/>.</returns>
        public static HugeNumber Abs(HugeNumber value)
            => new(Math.Abs(value.Mantissa), value.Exponent);

        /// <summary>
        /// Gets the absolute value of this instance.
        /// </summary>
        /// <returns>The absolute value of this instance.</returns>
        public HugeNumber Abs() => Abs(this);

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
        /// cref="PI"/> to convert from radians to degrees.
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
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsZero)
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
        /// cref="PI"/> to convert from radians to degrees.
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
            if (value.IsNaN
                || value < 1)
            {
                return NaN;
            }

            return Ln(value + (value.Square() - 1).Sqrt());
        }

        /// <summary>
        /// Adds two <see cref="HugeNumber"/> values and returns the result.
        /// </summary>
        /// <param name="left">The first value to add.</param>
        /// <param name="right">The second value to add.</param>
        /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static HugeNumber Add(HugeNumber left, HugeNumber right)
        {
            if (left.IsNaN || right.IsNaN)
            {
                return NaN;
            }

            HugeNumber larger, smaller;
            if (left >= right)
            {
                larger = left;
                smaller = right;
            }
            else
            {
                larger = right;
                smaller = left;
            }

            if (larger.IsPositiveInfinity)
            {
                return smaller.IsNegativeInfinity
                    ? Zero
                    : PositiveInfinity;
            }
            if (larger.IsNegativeInfinity)
            {
                return smaller.IsPositiveInfinity
                    ? Zero
                    : NegativeInfinity;
            }
            if (smaller.IsPositiveInfinity)
            {
                return PositiveInfinity;
            }
            if (smaller.IsNegativeInfinity)
            {
                return NegativeInfinity;
            }

            var smallerMantissa = smaller.Mantissa;
            var smallerMantissaDigits = smaller.MantissaDigits;
            var smallerExponent = smaller.Exponent;
            var largerMantissa = larger.Mantissa;
            var largerMantissaDigits = larger.MantissaDigits;
            var largerExponent = larger.Exponent;
            // Shift the smaller value into the exponent base of the larger, even if that
            // extinguishes all precision.
            while (smallerMantissa != 0 && smallerExponent < largerExponent)
            {
                if (largerMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
                {
                    largerMantissa *= 10;
                    largerMantissaDigits++;
                    largerExponent--;
                }
                else
                {
                    smallerMantissa /= 10;
                    smallerMantissaDigits--;
                    smallerExponent++;
                }
            }
            if (smallerExponent < largerExponent)
            {
                return larger;
            }
            while (smallerExponent > largerExponent)
            {
                if (largerMantissa % 10 == 0)
                {
                    largerMantissa /= 10;
                    largerMantissaDigits--;
                    largerExponent++;
                }
                else if (smallerMantissaDigits > 0)
                {
                    smallerMantissa *= 10;
                    smallerMantissaDigits++;
                    smallerExponent--;
                }
                else
                {
                    break;
                }
            }
            if (smallerExponent > largerExponent)
            {
                return larger;
            }

            if (MAX_MANTISSA - smallerMantissa < largerMantissa)
            {
                if (largerExponent == MAX_EXPONENT)
                {
                    return PositiveInfinity;
                }
                largerMantissa /= 10;
                largerExponent++;
                smallerMantissa /= 10;
            }

            return new HugeNumber(largerMantissa + smallerMantissa, largerExponent);
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
        /// cref="PI"/> to convert from radians to degrees.
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
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsZero)
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
        /// cref="PI"/> to convert from radians to degrees.
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
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsInfinite)
            {
                return value;
            }
            if (value.IsZero)
            {
                return Zero;
            }

            return value.Sign() * Ln(value.Abs() + (value.Square() + 1).Sqrt());
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
        /// cref="PI"/> to convert from radians to degrees.
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
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsZero)
            {
                return Zero;
            }
            if (value.IsNegativeInfinity)
            {
                return -HalfPI;
            }
            if (value.IsPositiveInfinity)
            {
                return HalfPI;
            }
            if (value < 0)
            {
                return -Atan(-value);
            }
            if (value > 1)
            {
                return HalfPI - Atan(1 / value);
            }
            if (value > _TwoMinusRoot3)
            {
                return SixthPI + Atan(((_Root3 * value) - 1) / (_Root3 + value));
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
            if (x.IsNaN || y.IsNaN)
            {
                return NaN;
            }
            if (x.IsZero)
            {
                return y >= 0 ? 0 : PI;
            }
            if (y.IsZero)
            {
                return x >= 0 ? HalfPI : -HalfPI;
            }
            if (y.IsPositiveInfinity)
            {
                if (x.IsPositiveInfinity)
                {
                    return QuarterPI;
                }
                else if (x.IsNegativeInfinity)
                {
                    return -QuarterPI;
                }
                else
                {
                    return Zero;
                }
            }
            if (y.IsNegativeInfinity)
            {
                if (x.IsPositiveInfinity)
                {
                    return ThreeQuartersPI;
                }
                else if (x.IsNegativeInfinity)
                {
                    return -ThreeQuartersPI;
                }
                else if (x >= 0)
                {
                    return PI;
                }
                else
                {
                    return -PI;
                }
            }
            if (x.IsPositiveInfinity)
            {
                return HalfPI;
            }
            if (x.IsNegativeInfinity)
            {
                return -HalfPI;
            }
            if (x > 0)
            {
                return Atan(y / x);
            }
            if (y > 0)
            {
                return Atan(y / x) + PI;
            }
            return Atan(y / x) - PI;
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
        /// cref="PI"/> to convert from radians to degrees.
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
            if (value.IsNaN
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

            return Ln((1 + value) / (1 - value)) / 2;
        }

        /// <summary>
        /// Gets the nearest whole integral value greater than or equal to the given <paramref
        /// name="value"/>.
        /// </summary>
        /// <param name="value">A value to round.</param>
        /// <returns>The nearest whole integral value greater than or equal to the given <paramref
        /// name="value"/>.</returns>
        public static HugeNumber Ceiling(HugeNumber value)
        {
            if (value.Exponent < 0)
            {
                var mantissa = value.Mantissa;
                for (var exponent = value.Exponent; exponent < -1; exponent++)
                {
                    mantissa /= 10;
                }
                var roundUp = mantissa > 0 && mantissa % 10 != 0;
                mantissa /= 10;
                if (roundUp)
                {
                    mantissa++;
                }
                return mantissa;
            }
            return value;
        }

        /// <summary>
        /// Gets the nearest whole integral value greater than or equal to this instance.
        /// </summary>
        /// <returns>The nearest whole integral value greater than or equal to this
        /// instance.</returns>
        public HugeNumber Ceiling() => Ceiling(this);

        /// <summary>
        /// Gets a value which has been truncated between lower and upper bounds.
        /// </summary>
        /// <param name="value">This value.</param>
        /// <param name="min">The inclusive lower bound of the result.</param>
        /// <param name="max">The inclusive upper bound of the result.</param>
        /// <returns><paramref name="value"/>; or <paramref name="min"/> if it is greater than
        /// <paramref name="value"/>; or <paramref name="max"/> if it is less than <paramref
        /// name="value"/>.</returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as
        /// <c>6.Clamp(3, 5)</c> would.
        /// </para>
        /// </remarks>
        public static HugeNumber Clamp(HugeNumber value, HugeNumber min, HugeNumber max)
        {
            if (min > max)
            {
                var t = max;
                max = min;
                min = t;
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
        /// Gets a value which has been truncated between lower and upper bounds.
        /// </summary>
        /// <param name="min">The inclusive lower bound of the result.</param>
        /// <param name="max">The inclusive upper bound of the result.</param>
        /// <returns>This instance; or <paramref name="min"/> if it is greater than this instance;
        /// or <paramref name="max"/> if it is less than this instance.</returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="min"/> is greater than <paramref name="max"/>, their values are
        /// swapped.
        /// </para>
        /// <para>
        /// For instance, <c>6.Clamp(5, 3)</c> will result in <c>5</c>, just as <c>6.Clamp(3, 5)</c>
        /// would.
        /// </para>
        /// </remarks>
        public HugeNumber Clamp(HugeNumber min, HugeNumber max) => Clamp(this, min, max);

        /// <summary>
        /// Gets a value whose absolute value equals that of the <paramref name="first"/> given
        /// input, but whose sign is the same as the <paramref name="second"/> given input.
        /// </summary>
        /// <param name="first">The value whose absolute value will be copied.</param>
        /// <param name="second">The value whose sign will be copied.</param>
        /// <returns>A value whose absolute value equals that of the <paramref name="first"/> given
        /// input, but whose sign is the same as the <paramref name="second"/> given
        /// input.</returns>
        public static HugeNumber CopySign(HugeNumber first, HugeNumber second)
        {
            if (second.IsNegative)
            {
                if (first.IsNegative)
                {
                    return first;
                }
                else
                {
                    return first.Negate();
                }
            }
            else if (first.IsNegative)
            {
                return first.Negate();
            }
            else
            {
                return first;
            }
        }

        /// <summary>
        /// Gets a value whose absolute value equals that of this instance, but whose sign is the
        /// same as the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value whose sign will be copied.</param>
        /// <returns>A value whose absolute value equals that of this instance, but whose sign is the
        /// same as the given <paramref name="value"/>.</returns>
        public HugeNumber CopySign(HugeNumber value) => CopySign(this, value);

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
            if (value.IsNaN
                || value.IsInfinite)
            {
                return NaN;
            }
            if (value < 0)
            {
                return Cos(-value);
            }
            if (value > TwoPI)
            {
                return Cos(value - (TwoPI * (value / TwoPI).Floor()));
            }
            if (value > ThreeHalvesPI)
            {
                return Cos(TwoPI - value);
            }
            if (value > PI)
            {
                return -Cos(value - PI);
            }
            if (value > HalfPI)
            {
                return -Cos(PI - value);
            }
            if (value > QuarterPI)
            {
                return Sin(HalfPI - value);
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
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsInfinite)
            {
                return PositiveInfinity;
            }
            if (value.IsZero)
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
        /// A fast implementation of cubing (a number raised to the power of 3).
        /// </summary>
        /// <param name="value">The value to cube.</param>
        /// <returns><paramref name="value"/> cubed (raised to the power of 3).</returns>
        public static HugeNumber Cube(HugeNumber value)
        {
            if (value.IsNaN
                || value.IsInfinite
                || value.IsZero)
            {
                return value;
            }
            return value * value * value;
        }

        /// <summary>
        /// A fast implementation of cubing (a number raised to the power of 3).
        /// </summary>
        /// <returns>This instance cubed (raised to the power of 3).</returns>
        public HugeNumber Cube() => Cube(this);

        /// <summary>
        /// Returns the cube root of a specified number.
        /// </summary>
        /// <param name="value">The number whose cube root is to be found.</param>
        /// <returns>
        /// The cube root of <paramref name="value"/>, or <see cref="NaN"/> if <paramref
        /// name="value"/> is <see cref="NaN"/>.
        /// </returns>
        public static HugeNumber CubeRoot(HugeNumber value)
        {
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsZero
                || value.IsPositiveInfinity)
            {
                return PositiveInfinity;
            }
            if (value == One)
            {
                return value;
            }
            if (value.IsNegativeInfinity)
            {
                return NegativeInfinity;
            }
            if (value == NegativeOne)
            {
                return NegativeOne;
            }

            if (value.IsNegative)
            {
                return -CubeRoot(-value);
            }

            return Exp(Third * Ln(value));
        }

        /// <summary>
        /// Returns the cube root of this instance.
        /// </summary>
        /// <returns>
        /// The cube root of this instance, or <see cref="NaN"/> if this instance is <see
        /// cref="NaN"/>.
        /// </returns>
        public HugeNumber CubeRoot() => CubeRoot(this);

        /// <summary>
        /// Decrements the given <paramref name="value"/> by no less than 1, and at least the
        /// minimum amount required to produce a distinct value.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>The given <paramref name="value"/> decremented by no less than 1, and at least
        /// enough to produce a value distinct from the given <paramref name="value"/>.</returns>
        /// <remarks>
        /// <para>
        /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
        /// the mantissa, a simple <c>x - 1</c> or <c>x--</c> operation may not produce a distinct
        /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
        /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
        /// </para>
        /// <para>
        /// For example, <c>new Number(1, 200) - 1</c> will not result in a different value than
        /// <c>new Number(1, 200)</c>. In this case, the minimum representable value smaller than
        /// <c>new Number(1, 200)</c> is <c>new Number(9.99999999999999999, 199)</c> (or
        /// equivalently, <c>new Number(999999999999999999, 182)</c>).
        /// </para>
        /// </remarks>
        public static HugeNumber Decrement(HugeNumber value)
            => value - Max(One, GetEpsilon(value));

        /// <summary>
        /// Decrements this instance by no less than 1, and at least the minimum amount required to
        /// produce a distinct value.
        /// </summary>
        /// <returns>This instance decremented by no less than 1, and at least enough to produce a
        /// value distinct from this instance.</returns>
        /// <remarks>
        /// <para>
        /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
        /// the mantissa, a simple <c>x - 1</c> or <c>x--</c> operation may not produce a distinct
        /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
        /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
        /// </para>
        /// <para>
        /// For example, <c>new Number(1, 200) - 1</c> will not result in a different value than
        /// <c>new Number(1, 200)</c>. In this case, the minimum representable value smaller than
        /// <c>new Number(1, 200)</c> is <c>new Number(9.99999999999999999, 199)</c> (or
        /// equivalently, <c>new Number(999999999999999999, 182)</c>).
        /// </para>
        /// </remarks>
        public HugeNumber Decrement() => Decrement(this);

        /// <summary>
        /// Divides one <see cref="HugeNumber"/> value by another and returns the result.
        /// </summary>
        /// <param name="dividend">The value to be divided.</param>
        /// <param name="divisor">The value to divide by.</param>
        /// <returns>The quotient of the division.</returns>
        /// <remarks>
        /// <para>
        /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see
        /// cref="NaN"/>, the result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
        /// is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
        /// is infinite, with a sign that matches <paramref name="dividend"/>.
        /// </para>
        /// <para>
        /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
        /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
        /// cref="NegativeInfinity"/> if they have opposite signs.
        /// </para>
        /// <para>
        /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
        /// finite value, the result is 0.
        /// </para>
        /// </remarks>
        public static HugeNumber Divide(HugeNumber dividend, HugeNumber divisor)
        {
            if (dividend.IsNaN || divisor.IsNaN)
            {
                return NaN;
            }
            if (divisor.IsZero)
            {
                if (dividend.IsZero)
                {
                    return NaN;
                }
                else if (dividend.IsPositive)
                {
                    return PositiveInfinity;
                }
                else
                {
                    return NegativeInfinity;
                }
            }
            if (dividend.IsZero)
            {
                return Zero;
            }
            if (dividend.IsInfinite)
            {
                return Sign(dividend) == Sign(divisor)
                    ? PositiveInfinity
                    : NegativeInfinity;
            }
            if (divisor.IsInfinite)
            {
                return Zero;
            }

            var dividendMantissa = (decimal)dividend.Mantissa;
            var dividendExponent = dividend.Exponent;
            var divisorMantissa = (decimal)divisor.Mantissa;
            var divisorExponent = divisor.Exponent;

            while (Math.Abs(divisorMantissa) < Math.Abs(dividendMantissa) / decimal.MaxValue)
            {
                if (dividendMantissa % 10 == 0)
                {
                    dividendMantissa /= 10;
                    dividendExponent++;
                }
                else
                {
                    divisorMantissa *= 10;
                    divisorExponent--;
                }
            }

            return new HugeNumber(dividendMantissa / divisorMantissa, dividendExponent - divisorExponent);
        }

        /// <summary>
        /// Divides one <see cref="HugeNumber"/> value by another and returns the result.
        /// </summary>
        /// <param name="dividend">The value to be divided.</param>
        /// <param name="divisor">The value to divide by.</param>
        /// <param name="remainder">
        /// When the method returns, will be set to the remainder of the division.
        /// </param>
        /// <returns>The quotient of the division.</returns>
        /// <remarks>
        /// <para>
        /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see
        /// cref="NaN"/>, the result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
        /// is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
        /// is infinite, with a sign that matches <paramref name="dividend"/>.
        /// </para>
        /// <para>
        /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
        /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
        /// cref="NegativeInfinity"/> if they have opposite signs.
        /// </para>
        /// <para>
        /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
        /// finite value, the result is 0.
        /// </para>
        /// </remarks>
        public static HugeNumber DivRem(HugeNumber dividend, HugeNumber divisor, out HugeNumber remainder)
        {
            var div = dividend / divisor;
            var result = div.Floor();
            remainder = div - result;
            return result;
        }

        /// <summary>
        /// Returns e raised to the specified power.
        /// </summary>
        /// <param name="value">A number specifying a power.</param>
        /// <returns>
        /// The number e raised to the power <paramref name="value"/>. If <paramref name="value"/>
        /// equals <see cref="NaN"/> or <see cref="PositiveInfinity"/>, that value is
        /// returned. If <paramref name="value"/> equals <see cref="NegativeInfinity"/>, 0 is
        /// returned.
        /// </returns>
        /// <remarks>
        /// <para>
        /// e is a mathematical constant whose value is approximately 2.71828182845904524.
        /// </para>
        /// <para>
        /// Use the <see cref="Pow(HugeNumber, HugeNumber)"/> method to calculate powers of other bases.
        /// </para>
        /// <para>
        /// <see cref="Exp(HugeNumber)"/> is the inverse of <see cref="Ln(HugeNumber)"/>.
        /// </para>
        /// </remarks>
        public static HugeNumber Exp(HugeNumber value)
        {
            if (value.IsNaN || value.IsPositiveInfinity)
            {
                return value;
            }
            else if (value.IsNegativeInfinity)
            {
                return Zero;
            }
            else if (value.IsZero)
            {
                return 1;
            }
            else if (value == 1)
            {
                return e;
            }
            else
            {
                var result = 1 + value;
                if (result.IsPositiveInfinity)
                {
                    return PositiveInfinity;
                }
                var numerator = value * value;
                if (numerator.IsPositiveInfinity)
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
                    numerator *= value;
                    if (numerator.IsPositiveInfinity)
                    {
                        return PositiveInfinity;
                    }
                    denominator *= nextDenominatorDigit;
                    if (denominator.IsPositiveInfinity)
                    {
                        return result;
                    }
                    nextDenominatorDigit++;
                    result = newResult;
                } while (nextDenominatorDigit <= int.MaxValue);
                return result;
            }
        }

        /// <summary>
        /// Returns e raised to the power of this instance.
        /// </summary>
        /// <returns>
        /// The number e raised to the power of this instance. If this instance equals <see
        /// cref="NaN"/> or <see cref="PositiveInfinity"/>, that value is returned. If
        /// this instance equals <see cref="NegativeInfinity"/>, 0 is returned.
        /// </returns>
        /// <remarks>
        /// <para>
        /// e is a mathematical constant whose value is approximately 2.71828182845904524.
        /// </para>
        /// <para>
        /// Use the <see cref="Pow(HugeNumber)"/> method to calculate powers of other bases.
        /// </para>
        /// <para>
        /// <see cref="Exp(HugeNumber)"/> is the inverse of <see cref="Ln(HugeNumber)"/>.
        /// </para>
        /// </remarks>
        public HugeNumber Exp() => Exp(this);

        /// <summary>
        /// Gets the nearest whole integral value less than or equal to the given <paramref
        /// name="value"/>.
        /// </summary>
        /// <param name="value">A value to round.</param>
        /// <returns>The nearest whole integral value less than or equal to the given <paramref
        /// name="value"/>.</returns>
        public static HugeNumber Floor(HugeNumber value)
        {
            if (value.Exponent < 0)
            {
                var mantissa = value.Mantissa;
                for (var exponent = value.Exponent; exponent < -1; exponent++)
                {
                    mantissa /= 10;
                }
                var roundDown = mantissa < 0 && mantissa % 10 != 0;
                mantissa /= 10;
                if (roundDown)
                {
                    mantissa--;
                }
                return mantissa;
            }
            return value;
        }

        /// <summary>
        /// Gets the nearest whole integral value less than or equal to this instance.
        /// </summary>
        /// <returns>The nearest whole integral value less than or equal to this instance.</returns>
        public HugeNumber Floor() => Floor(this);

        /// <summary>
        /// Gets the smallest value which will produce a distinct value from the given <paramref
        /// name="value"/> when added to it or subtracted from it.
        /// </summary>
        /// <returns>The smallest value which will produce a distinct value from the given <paramref
        /// name="value"/> when added to it or subtracted from it.</returns>
        public static HugeNumber GetEpsilon(HugeNumber value)
        {
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsZero)
            {
                return Epsilon;
            }
            if (value.IsInfinite)
            {
                return PositiveInfinity;
            }
            var mantissaDigits = value.MantissaDigits;
            var exp = value.Exponent;
            if (mantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
            {
                exp -= (short)(MANTISSA_SIGNIFICANT_DIGITS - mantissaDigits);
            }
            return new HugeNumber(1, exp);
        }

        /// <summary>
        /// Gets the smallest value which will produce a distinct value from this instance when
        /// added to it or subtracted from it.
        /// </summary>
        /// <returns>The smallest value which will produce a distinct value from this instance when
        /// added to it or subtracted from it.</returns>
        public HugeNumber GetEpsilon() => GetEpsilon(this);

        /// <summary>
        /// Increments the given <paramref name="value"/> by no less than 1, and at least the
        /// minimum amount required to produce a distinct value.
        /// </summary>
        /// <param name="value">The value to increment.</param>
        /// <returns>The given <paramref name="value"/> incremented by no less than 1, and at least
        /// enough to produce a value distinct from the given <paramref name="value"/>.</returns>
        /// <remarks>
        /// <para>
        /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
        /// the mantissa, a simple <c>x + 1</c> or <c>x++</c> operation may not produce a distinct
        /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
        /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
        /// </para>
        /// <para>
        /// For example, <c>new Number(1, 200) + 1</c> will not result in a different value than
        /// <c>new Number(1, 200)</c>. In this case, the minimum representable value larger than
        /// <c>new Number(1, 200)</c> is <c>new Number(1.00000000000000001, 200)</c> (or
        /// equivalently, <c>new Number(100000000000000001, 183)</c>).
        /// </para>
        /// </remarks>
        public static HugeNumber Increment(HugeNumber value)
            => value + Max(One, GetEpsilon(value));

        /// <summary>
        /// Increments this instance by no less than 1, and at least the minimum amount required to
        /// produce a distinct value.
        /// </summary>
        /// <returns>This instance incremented by no less than 1, and at least enough to produce a
        /// value distinct from this instance.</returns>
        /// <remarks>
        /// <para>
        /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
        /// the mantissa, a simple <c>x + 1</c> or <c>x++</c> operation may not produce a distinct
        /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
        /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
        /// </para>
        /// <para>
        /// For example, <c>new Number(1, 200) + 1</c> will not result in a different value than
        /// <c>new Number(1, 200)</c>. In this case, the minimum representable value larger than
        /// <c>new Number(1, 200)</c> is <c>new Number(1.00000000000000001, 200)</c> (or
        /// equivalently, <c>new Number(100000000000000001, 183)</c>).
        /// </para>
        /// </remarks>
        public HugeNumber Increment() => Increment(this);

        /// <summary>
        /// Finds the weight which would produce the given <paramref name="result"/> when linearly
        /// interpolating between the two given values.
        /// </summary>
        /// <param name="first">The first value.</param>
        /// <param name="second">The second value.</param>
        /// <param name="result">The desired result of a linear interpolation between <paramref
        /// name="first"/> and <paramref name="second"/>.</param>
        /// <returns>The weight which would produce <paramref name="result"/> when linearly
        /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
        /// cref="NaN"/> if the weight cannot be computed for the given
        /// parameters.</returns>
        /// <remarks>
        /// <see cref="NaN"/> will be returned if the given values are nearly
        /// equal, but the given result is not also nearly equal to them, since the calculation in
        /// that case would require a division by zero.
        /// </remarks>
        public static HugeNumber InverseLerp(HugeNumber first, HugeNumber second, HugeNumber result)
        {
            var difference = second - first;
            if (difference.IsZero)
            {
                if (result == first)
                {
                    return new HugeNumber(5, -1);
                }
                else
                {
                    return NaN;
                }
            }
            return (result - first) / difference;
        }

        /// <summary>
        /// Finds the weight which would produce the given <paramref name="result"/> when linearly
        /// interpolating between this value and the <paramref name="other"/> value.
        /// </summary>
        /// <param name="other">The second value.</param>
        /// <param name="result">The desired result of a linear interpolation between this instance
        /// and <paramref name="other"/>.</param>
        /// <returns>The weight which would produce <paramref name="result"/> when linearly
        /// interpolating between this instance and <paramref name="other"/>; or <see cref="NaN"/>
        /// if the weight cannot be computed for the given parameters.</returns>
        /// <remarks>
        /// <see cref="NaN"/> will be returned if the given values are nearly equal, but the given
        /// result is not also nearly equal to them, since the calculation in that case would
        /// require a division by zero.
        /// </remarks>
        public HugeNumber InverseLerp(HugeNumber other, HugeNumber result) => InverseLerp(this, other, result);

        /// <summary>
        /// Determines if <see cref="HugeNumber"/> values are nearly equal, within a tolerance
        /// determined by the given epsilon value.
        /// </summary>
        /// <param name="value">This value.</param>
        /// <param name="other">The other value.</param>
        /// <param name="epsilon">
        /// An epsilon value which determines the tolerance for near-equality between the values.
        /// </param>
        /// <returns><see langword="true"/> if this value and the other are nearly equal; otherwise
        /// <see langword="false"/>.</returns>
        public static bool IsNearlyEqualTo(HugeNumber value, HugeNumber other, HugeNumber epsilon)
        {
            if (value == other)
            {
                return true;
            }

            return HugeNumber.Abs(value - other) < epsilon;
        }

        /// <summary>
        /// Determines if this value is nearly equal to another, within a tolerance determined by
        /// the given epsilon value.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <param name="epsilon">
        /// An epsilon value which determines the tolerance for near-equality between the values.
        /// </param>
        /// <returns><see langword="true"/> if this value and the other are nearly equal; otherwise
        /// <see langword="false"/>.</returns>
        public bool IsNearlyEqualTo(HugeNumber other, HugeNumber epsilon) => IsNearlyEqualTo(this, other, epsilon);

        /// <summary>
        /// Determines if this value is nearly equal to another, within a tolerance determined by an
        /// epsilon value based on their magnitudes.
        /// </summary>
        /// <param name="other">The other value.</param>
        /// <returns><see langword="true"/> if the values are nearly equal; otherwise <see
        /// langword="false"/>.</returns>
        public bool IsNearlyEqualTo(HugeNumber other) => IsNearlyEqualTo(this, other, Max(this, other).GetEpsilon());

        /// <summary>
        /// Linearly interpolates between two values based on the given weighting.
        /// </summary>
        /// <param name="first">The first value.</param>
        /// <param name="second">The second value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
        /// vector.</param>
        /// <returns>The interpolated value.</returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
        /// opposite direction on a number line from <paramref name="first"/> as <paramref
        /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
        /// <c>1.5</c>.
        /// </para>
        /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
        /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
        /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
        /// <c>3.5</c>.</para>
        /// </remarks>
        public static HugeNumber Lerp(HugeNumber first, HugeNumber second, HugeNumber amount)
            => first + ((second - first) * amount);

        /// <summary>
        /// Linearly interpolates between this value and another based on the given weighting.
        /// </summary>
        /// <param name="second">The second value.</param>
        /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
        /// vector.</param>
        /// <returns>The interpolated value.</returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
        /// opposite direction on a number line from this instance as <paramref name="second"/>,
        /// rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
        /// <c>1.5</c>.
        /// </para>
        /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
        /// in the opposite direction on a number line from <paramref name="second"/> as this
        /// instance, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
        /// <c>3.5</c>.</para>
        /// </remarks>
        public HugeNumber Lerp(HugeNumber second, HugeNumber amount) => Lerp(this, second, amount);

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
        public static HugeNumber Ln(HugeNumber value)
        {
            if (value.IsNaN || Sign(value) < 0)
            {
                return NaN;
            }
            else if (value.IsZero || value.IsPositiveInfinity)
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
        public HugeNumber Ln() => Ln(this);

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
        /// natural logarithm of a number, call the <see cref="Ln(HugeNumber)"/> method.
        /// </para>
        /// <para>
        /// This method corresponds to the <see cref="Math.Log(double, double)"/> method.
        /// </para>
        /// </remarks>
        public static HugeNumber Log(HugeNumber value, HugeNumber newBase)
        {
            if (value.IsNaN
                || newBase.IsNaN
                || newBase == 1
                || Sign(value) < 0
                || (value != 1 && (newBase.IsZero || newBase.IsInfinite)))
            {
                return NaN;
            }
            else if (value.IsZero || value.IsPositiveInfinity)
            {
                return PositiveInfinity;
            }
            else
            {
                return Ln(value) / Ln(newBase);
            }
        }

        /// <summary>
        /// Returns the logarithm of this instance in a specified base.
        /// </summary>
        /// <param name="newBase">The base of the logarithm.</param>
        /// <returns>
        /// The base <paramref name="newBase"/> logarithm of this instance, as described in the
        /// Remarks section.
        /// </returns>
        /// <remarks>
        /// <para>
        /// This instance and <paramref name="newBase"/> parameters are specified as base 10
        /// numbers.
        /// </para>
        /// <para>
        /// The precise return value of the method depends on the sign of this instance and on the
        /// sign and value of <paramref name="newBase"/>, as the following table shows.
        /// </para>
        /// <list type="table">
        /// <listheader>
        /// <term>This instance</term>
        /// <term><paramref name="newBase"/> parameter</term>
        /// <term>Return value</term>
        /// </listheader>
        /// <item>
        /// <term>value &gt; 0</term>
        /// <term>
        /// (0 &lt; <paramref name="newBase"/> &lt; 1) -or- ( <paramref name="newBase"/> &gt; 1)
        /// </term>
        /// <term>log <sub><paramref name="newBase"/></sub>(value)</term>
        /// </item>
        /// <item>
        /// <term><see cref="NaN"/></term>
        /// <term>(any value)</term>
        /// <term><see cref="NaN"/></term>
        /// </item>
        /// <item>
        /// <term>value &lt; 0</term>
        /// <term>(any value)</term>
        /// <term><see cref="NaN"/></term>
        /// </item>
        /// <item>
        /// <term>(any value)</term>
        /// <term><paramref name="newBase"/> &lt; 0</term>
        /// <term><see cref="NaN"/></term>
        /// </item>
        /// <item>
        /// <term>value != 1</term>
        /// <term><paramref name="newBase"/> = 0</term>
        /// <term><see cref="NaN"/></term>
        /// </item>
        /// <item>
        /// <term>value != 1</term>
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
        /// <term><see cref="PositiveInfinity"/></term>
        /// <term>(any value)</term>
        /// <term><see cref="PositiveInfinity"/></term>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <term>0 &lt; <paramref name="newBase"/> &lt; 1</term>
        /// <term><see cref="PositiveInfinity"/></term>
        /// </item>
        /// <item>
        /// <term>0</term>
        /// <term><paramref name="newBase"/> &gt; 1</term>
        /// <term><see cref="PositiveInfinity"/></term>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <term><paramref name="newBase"/> = 0</term>
        /// <term>0</term>
        /// </item>
        /// <item>
        /// <term>1</term>
        /// <term><paramref name="newBase"/> = <see cref="PositiveInfinity"/></term>
        /// <term>0</term>
        /// </item>
        /// </list>
        /// <para>
        /// To calculate the base 10 logarithm of a <see cref="HugeNumber"/> value, call the <see
        /// cref="Log10()"/> method. To calculate the base 2 logarithm of a <see cref="HugeNumber"/>
        /// value, call the <see cref="Log2()"/> method. To calculate the natural logarithm of a
        /// number, call the <see cref="Ln()"/> method.
        /// </para>
        /// </remarks>
        public HugeNumber Log(HugeNumber newBase) => Log(this, newBase);

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
        /// cref="HugeNumber"/> value, call the <see cref="Ln(HugeNumber)"/> method. To calculate the
        /// logarithm of a number in another base, call the <see cref="Log(HugeNumber, HugeNumber)"/>
        /// method.
        /// </para>
        /// </remarks>
        public static HugeNumber Log2(HugeNumber value)
        {
            if (value.IsNaN || Sign(value) < 0)
            {
                return NaN;
            }
            else if (value.IsZero || value.IsPositiveInfinity)
            {
                return PositiveInfinity;
            }
            else
            {
                return Ln(value) / Ln2;
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
        /// value, call the <see cref="Ln()"/> method. To calculate the logarithm of a number in
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
        /// cref="HugeNumber"/> value, call the <see cref="Ln(HugeNumber)"/> method. To calculate the
        /// logarithm of a number in another base, call the <see cref="Log(HugeNumber, HugeNumber)"/>
        /// method.
        /// </para>
        /// <para>
        /// This method corresponds to the <see cref="Math.Log10(double)"/> method.
        /// </para>
        /// </remarks>
        public static HugeNumber Log10(HugeNumber value)
        {
            if (value.IsNaN || Sign(value) < 0)
            {
                return NaN;
            }
            else if (value.IsZero || value.IsPositiveInfinity)
            {
                return PositiveInfinity;
            }
            else
            {
                return Ln(value) / Ln10;
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
        /// value, call the <see cref="Ln()"/> method. To calculate the logarithm of a number in
        /// another base, call the <see cref="Log(HugeNumber)"/> method.
        /// </para>
        /// </remarks>
        public HugeNumber Log10() => Log10(this);

        /// <summary>
        /// Returns the larger of two <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// The <paramref name="left"/> or <paramref name="right"/> parameter, whichever is larger.
        /// </returns>
        /// <remarks>
        /// <para>
        /// If either value is <see cref="NaN"/>, the result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// This method corresponds to the <see cref="Math.Max"/> method for primitive numeric types.
        /// </para>
        /// </remarks>
        public static HugeNumber Max(HugeNumber left, HugeNumber right)
        {
            if (left.IsNaN || right.IsNaN)
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
            if (left.IsNaN || right.IsNaN)
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
        /// <para>
        /// If either value is <see cref="NaN"/>, the result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// This method corresponds to the <see cref="Math.Min"/> method for primitive numeric types.
        /// </para>
        /// </remarks>
        public static HugeNumber Min(HugeNumber left, HugeNumber right)
        {
            if (left.IsNaN || right.IsNaN)
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
            if (left.IsNaN || right.IsNaN)
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
        /// Divides one <see cref="HugeNumber"/> value by another and returns the remainder.
        /// </summary>
        /// <param name="dividend">The value to be divided.</param>
        /// <param name="divisor">The value to divide by.</param>
        /// <returns>The remainder of the division.</returns>
        /// <remarks>
        /// <para>
        /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see
        /// cref="NaN"/>, the result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
        /// is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
        /// is zero.
        /// </para>
        /// <para>
        /// If <paramref name="dividend"/> is infinite, the result is zero.
        /// </para>
        /// <para>
        /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
        /// finite value, the result is 0.
        /// </para>
        /// </remarks>
        public static HugeNumber Mod(HugeNumber dividend, HugeNumber divisor)
        {
            if (dividend.IsNaN || divisor.IsNaN)
            {
                return NaN;
            }
            if (divisor.IsZero)
            {
                return dividend.IsZero ? NaN : Zero;
            }
            if (dividend.IsZero || dividend.IsInfinite || divisor.IsInfinite)
            {
                return Zero;
            }

            var dividendMantissa = dividend.Mantissa;
            var dividendExponent = dividend.Exponent;
            var significantDigits = dividend.MantissaDigits;
            while (significantDigits < MANTISSA_SIGNIFICANT_DIGITS
                && dividendExponent > MIN_EXPONENT
                && dividendMantissa % divisor.Mantissa != 0)
            {
                dividendMantissa *= 10;
                dividendExponent--;
                significantDigits++;
            }

            var quotient = dividendMantissa / divisor.Mantissa;
            var remainder = dividendMantissa % divisor.Mantissa;
            if (remainder >= dividendMantissa / 2)
            {
                quotient++;
            }

            if (dividendExponent - divisor.Exponent < 0)
            {
                return new HugeNumber(quotient, dividendExponent - divisor.Exponent);
            }

            return new HugeNumber(remainder, dividendExponent - divisor.Exponent);
        }

        /// <summary>
        /// Returns the product of two <see cref="HugeNumber"/> values.
        /// </summary>
        /// <param name="left">The first number to multiply.</param>
        /// <param name="right">The second number to multiply.</param>
        /// <returns>
        /// The product of the <paramref name="left"/> and <paramref name="right"/> parameters.
        /// </returns>
        public static HugeNumber Multiply(HugeNumber left, HugeNumber right)
        {
            if (left.IsNaN || right.IsNaN)
            {
                return NaN;
            }
            if (left.IsZero || right.IsZero)
            {
                return Zero;
            }
            if (left.IsInfinite || right.IsInfinite)
            {
                return Sign(left) == Sign(right)
                    ? PositiveInfinity
                    : NegativeInfinity;
            }

            var leftMantissa = (decimal)left.Mantissa;
            var leftExponent = left.Exponent;
            var rightMantissa = (decimal)right.Mantissa;
            var rightExponent = right.Exponent;
            while (MAX_MANTISSA / Math.Abs(leftMantissa) < Math.Abs(rightMantissa))
            {
                if (leftExponent + rightExponent > MAX_EXPONENT - 2)
                {
                    return Sign(left) == Sign(right)
                        ? PositiveInfinity
                        : NegativeInfinity;
                }
                if (leftMantissa % 10 == 0)
                {
                    leftMantissa /= 10;
                    leftExponent++;
                }
                else if (rightMantissa % 10 == 0)
                {
                    rightMantissa /= 10;
                    rightExponent++;
                }
                else
                {
                    leftMantissa /= 10;
                    leftExponent++;
                    rightMantissa /= 10;
                    rightExponent++;
                }
            }

            return new HugeNumber(leftMantissa * rightMantissa, leftExponent + rightExponent);
        }

        /// <summary>
        /// Negates a specified <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="value">The value to negate.</param>
        /// <returns>
        /// The result of the <paramref name="value"/> parameter multiplied by negative one (-1).
        /// </returns>
        public static HugeNumber Negate(HugeNumber value)
        {
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsPositiveInfinity)
            {
                return NegativeInfinity;
            }
            if (value.IsNegativeInfinity)
            {
                return PositiveInfinity;
            }
            if (value.IsZero)
            {
                return Zero;
            }
            return new HugeNumber(-value.Mantissa, value.Exponent);
        }

        /// <summary>
        /// Negates this instance.
        /// </summary>
        /// <returns>
        /// The result of this instance multiplied by negative one (-1).
        /// </returns>
        public HugeNumber Negate() => Negate(this);

        /// <summary>
        /// Raises an <see cref="HugeNumber"/> value to the power of a specified value.
        /// </summary>
        /// <param name="value">The number to raise to the <paramref name="exponent"/>
        /// power.</param>
        /// <param name="exponent">The exponent to raise <paramref name="value"/> by.</param>
        /// <returns>
        /// The result of raising <paramref name="value"/> to the <paramref name="exponent"/> power.
        /// </returns>
        /// <remarks>
        /// <para>
        /// If <paramref name="value"/> is <see cref="NaN"/> or <paramref name="exponent"/> is
        /// negative, the result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If <paramref name="value"/> is <see cref="PositiveInfinity"/>, the result is 1 if
        /// <paramref name="exponent"/> is 0, otherwise <see cref="PositiveInfinity"/>.
        /// </para>
        /// <para>
        /// If <paramref name="value"/> is <see cref="NegativeInfinity"/>, the result is 1 if
        /// <paramref name="exponent"/> is 0, <see cref="PositiveInfinity"/> if <paramref
        /// name="exponent"/> is even, or <see cref="NegativeInfinity"/> if exponent is odd.
        /// </para>
        /// </remarks>
        public static HugeNumber Pow(HugeNumber value, HugeNumber exponent)
        {
            if (value.IsNaN
                || exponent.IsNaN)
            {
                return NaN;
            }
            if (exponent.IsZero)
            {
                return One;
            }
            if (value.IsZero)
            {
                return exponent.IsNegative
                    ? PositiveInfinity
                    : Zero;
            }
            if (value == One
                || exponent == One)
            {
                return value;
            }
            if (value.IsPositiveInfinity)
            {
                return exponent.IsNegative
                    ? Zero
                    : PositiveInfinity;
            }
            if (value.IsNegative
                && exponent.Exponent < 0
                && (One / exponent).Exponent < 0)
            {
                return NaN;
            }
            if (value.IsNegativeInfinity)
            {
                if (exponent.IsNegative)
                {
                    return Zero;
                }
                else if (exponent.Exponent < 0 || exponent % 2 != 0)
                {
                    return NegativeInfinity;
                }
                else
                {
                    return PositiveInfinity;
                }
            }
            if (exponent.IsPositiveInfinity)
            {
                if (value == NegativeOne)
                {
                    return NaN;
                }
                else if (value.Abs() > One)
                {
                    return PositiveInfinity;
                }
                else
                {
                    return Zero;
                }
            }
            if (exponent.IsNegativeInfinity)
            {
                if (value == NegativeOne)
                {
                    return NaN;
                }
                else if (value.Abs() > One)
                {
                    return Zero;
                }
                else
                {
                    return NaN;
                }
            }
            if (value == NegativeOne)
            {
                return exponent.Exponent < 0 || exponent % 2 != 0
                    ? NegativeOne
                    : One;
            }

            if (value.IsNegative)
            {
                return -Pow(-value, exponent);
            }

            if (exponent.IsNegative)
            {
                return One / Pow(value, -exponent);
            }

            return Exp(exponent * Ln(value));
        }

        /// <summary>
        /// Raises this instance to the power of a specified value.
        /// </summary>
        /// <param name="exponent">The exponent by which to raise this instance.</param>
        /// <returns>
        /// The result of raising this instance to the <paramref name="exponent"/> power.
        /// </returns>
        /// <remarks>
        /// <para>
        /// If this instance is <see cref="NaN"/> or <paramref name="exponent"/> is negative, the
        /// result is <see cref="NaN"/>.
        /// </para>
        /// <para>
        /// If this instance is <see cref="PositiveInfinity"/>, the result is 1 if
        /// <paramref name="exponent"/> is 0, otherwise <see cref="PositiveInfinity"/>.
        /// </para>
        /// <para>
        /// If this instance is <see cref="NegativeInfinity"/>, the result is 1 if
        /// <paramref name="exponent"/> is 0, <see cref="PositiveInfinity"/> if <paramref
        /// name="exponent"/> is even, or <see cref="NegativeInfinity"/> if exponent is odd.
        /// </para>
        /// </remarks>
        public HugeNumber Pow(HugeNumber exponent) => Pow(this, exponent);

        /// <summary>
        /// Gets the given <paramref name="value"/> rounded to the given number of decimal places.
        /// </summary>
        /// <param name="value">A value to round.</param>
        /// <param name="decimals">The number of decimal places to which the value should be
        /// rounded.</param>
        /// <returns>The <paramref name="value"/> rounded to the given number of decimal
        /// places.</returns>
        public static HugeNumber Round(HugeNumber value, int decimals)
        {
            if (value.Exponent < -decimals)
            {
                var mantissa = value.Mantissa;
                var exponent = value.Exponent;
                while (exponent < -decimals - 1)
                {
                    mantissa /= 10;
                    exponent++;
                }
                var roundAwayFromZero = Math.Abs(mantissa % 10) >= 5;
                mantissa /= 10;
                exponent++;
                if (roundAwayFromZero)
                {
                    mantissa = mantissa < 0
                        ? mantissa--
                        : mantissa++;
                }
                return new HugeNumber(mantissa, exponent);
            }
            return value;
        }

        /// <summary>
        /// Gets the nearest whole integral value to the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value to round.</param>
        /// <returns>The nearest whole integral value to the given <paramref
        /// name="value"/>.</returns>
        public static HugeNumber Round(HugeNumber value) => Round(value, 0);

        /// <summary>
        /// Gets this value rounded to the given number of decimal places.
        /// </summary>
        /// <param name="decimals">The number of decimal places to which the value should be
        /// rounded.</param>
        /// <returns>This value rounded to the given number of decimal places.</returns>
        public HugeNumber Round(int decimals) => Round(this, decimals);

        /// <summary>
        /// Gets the nearest whole integral value to this instance.
        /// </summary>
        /// <returns>The nearest whole integral value to this instance.</returns>
        public HugeNumber Round() => Round(this);

        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of <paramref
        /// name="value"/>.
        /// </summary>
        /// <param name="value">A number.</param>
        /// <returns>
        /// A number that indicates the sign (negative, positive, or zero) of <paramref
        /// name="value"/>.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Sign property is equivalent to the <see cref="Math.Sign"/> method for the primitive
        /// numeric types.
        /// </para>
        /// <para>
        /// The sign of <see cref="NaN"/> is 0.
        /// </para>
        /// </remarks>
        public static int Sign(HugeNumber value)
        {
            if (value.IsPositiveInfinity)
            {
                return 1;
            }
            else if (value.IsNegativeInfinity)
            {
                return -1;
            }
            else if (value.IsNaN)
            {
                return 0;
            }
            else
            {
                return Math.Sign(value.Mantissa);
            }
        }

        /// <summary>
        /// Gets a number that indicates the sign (negative, positive, or zero) of this instance.
        /// </summary>
        /// <returns>
        /// A number that indicates the sign (negative, positive, or zero) of this instance.
        /// </returns>
        /// <remarks>
        /// <para>
        /// The Sign property is equivalent to the <see cref="Math.Sign"/> method for the primitive
        /// numeric types.
        /// </para>
        /// <para>
        /// The sign of <see cref="NaN"/> is 0.
        /// </para>
        /// </remarks>
        public int Sign() => Sign(this);

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
            if (value.IsNaN
                || value.IsInfinite)
            {
                return NaN;
            }
            if (value < 0)
            {
                return -Sin(-value);
            }
            if (value > TwoPI)
            {
                return Sin(value - (TwoPI * (value / TwoPI).Floor()));
            }
            if (value > ThreeHalvesPI)
            {
                return -Sin(TwoPI - value);
            }
            if (value > PI)
            {
                return -Sin(value - PI);
            }
            if (value > HalfPI)
            {
                return Sin(PI - value);
            }
            if (value > QuarterPI)
            {
                return Cos(HalfPI - value);
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
            if (value.IsNaN
                || value.IsInfinite)
            {
                return value;
            }
            if (value.IsZero)
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
        /// Returns <paramref name="target"/> if <paramref name="value"/> is nearly equal to it (cf.
        /// <see cref="IsNearlyEqualTo(HugeNumber, HugeNumber)"/>), or
        /// <paramref name="value"/> itself if not.
        /// </summary>
        /// <param name="value">A value.</param>
        /// <param name="target">The value to snap to.</param>
        /// <returns><paramref name="target"/>, if <paramref name="value"/> is nearly equal to it;
        /// otherwise <paramref name="value"/>.</returns>
        public static HugeNumber SnapTo(HugeNumber value, HugeNumber target)
            => value.IsNearlyEqualTo(target) ? target : value;

        /// <summary>
        /// Returns <paramref name="target"/> if this value is nearly equal to it (cf.
        /// <see cref="IsNearlyEqualTo(HugeNumber, HugeNumber)"/>), or this value if not.
        /// </summary>
        /// <param name="target">The value to snap to.</param>
        /// <returns><paramref name="target"/>, if this value is nearly equal to it;
        /// otherwise this value.</returns>
        public HugeNumber SnapTo(HugeNumber target) => SnapTo(this, target);

        /// <summary>
        /// A fast implementation of squaring (a number raised to the power of 2).
        /// </summary>
        /// <param name="value">The value to square.</param>
        /// <returns><paramref name="value"/> squared (raised to the power of 2).</returns>
        public static HugeNumber Square(HugeNumber value)
        {
            if (value.IsNaN
                || value.IsZero)
            {
                return value;
            }
            if (value.IsPositiveInfinity)
            {
                return value;
            }
            if (value.IsNegativeInfinity)
            {
                return PositiveInfinity;
            }
            return value * value;
        }

        /// <summary>
        /// A fast implementation of squaring (a number raised to the power of 2).
        /// </summary>
        /// <returns>This instance squared (raised to the power of 2).</returns>
        public HugeNumber Square() => Square(this);

        /// <summary>
        /// Returns the square root of a specified number.
        /// </summary>
        /// <param name="value">The number whose square root is to be found.</param>
        /// <returns>
        /// One of the values in the following table.
        /// <list type="table">
        /// <listheader>
        /// <term><paramref name="value"/> parameter</term>
        /// <description>Return value</description>
        /// </listheader>
        /// <item>
        /// <term>Zero or positive</term>
        /// <description>The positive square root of <paramref name="value"/></description>
        /// </item>
        /// <item>
        /// <term>Negative</term>
        /// <description><see cref="NaN"/></description>
        /// </item>
        /// <item>
        /// <term><see cref="NaN"/></term>
        /// <description><see cref="NaN"/></description>
        /// </item>
        /// <item>
        /// <term><see cref="PositiveInfinity"/></term>
        /// <description><see cref="PositiveInfinity"/></description>
        /// </item>
        /// </list>
        /// </returns>
        public static HugeNumber Sqrt(HugeNumber value)
        {
            if (value.IsNaN
                || value.IsNegative)
            {
                return NaN;
            }
            if (value.IsZero
                || value == One
                || value.IsPositiveInfinity)
            {
                return value;
            }

            return Exp(Half * Ln(value));
        }

        /// <summary>
        /// Returns the square root of this instance.
        /// </summary>
        /// <returns>
        /// One of the values in the following table.
        /// <list type="table">
        /// <listheader>
        /// <term>This instance</term>
        /// <description>Return value</description>
        /// </listheader>
        /// <item>
        /// <term>Zero or positive</term>
        /// <description>The positive square root of this instance</description>
        /// </item>
        /// <item>
        /// <term>Negative</term>
        /// <description><see cref="NaN"/></description>
        /// </item>
        /// <item>
        /// <term><see cref="NaN"/></term>
        /// <description><see cref="NaN"/></description>
        /// </item>
        /// <item>
        /// <term><see cref="PositiveInfinity"/></term>
        /// <description><see cref="PositiveInfinity"/></description>
        /// </item>
        /// </list>
        /// </returns>
        public HugeNumber Sqrt() => Sqrt(this);

        /// <summary>
        /// Subtracts one <see cref="HugeNumber"/> value from another and returns the result.
        /// </summary>
        /// <param name="left">The value to subtract from (the minuend).</param>
        /// <param name="right">The value to subtract (the subtrahend).</param>
        /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
        public static HugeNumber Subtract(HugeNumber left, HugeNumber right)
        {
            if (left.IsNaN || right.IsNaN)
            {
                return NaN;
            }
            if (left.IsPositiveInfinity)
            {
                return right.IsPositiveInfinity
                    ? Zero
                    : PositiveInfinity;
            }
            if (left.IsNegativeInfinity)
            {
                return right.IsNegativeInfinity
                    ? Zero
                    : NegativeInfinity;
            }
            if (right.IsPositiveInfinity)
            {
                return NegativeInfinity;
            }
            if (right.IsNegativeInfinity)
            {
                return PositiveInfinity;
            }

            // Shift the left value into the exponent base of the right, even if that
            // extinguishes all precision.
            var leftMantissa = left.Mantissa;
            var leftMantissaDigits = left.MantissaDigits;
            var leftExponent = left.Exponent;
            var rightMantissa = right.Mantissa;
            var rightMantissaDigits = right.MantissaDigits;
            var rightExponent = right.Exponent;
            while (leftMantissa != 0 && leftExponent < rightExponent)
            {
                if (rightMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS
                    && rightExponent > MIN_EXPONENT)
                {
                    rightMantissa *= 10;
                    rightMantissaDigits++;
                    rightExponent--;
                }
                else
                {
                    leftMantissa /= 10;
                    leftMantissaDigits--;
                    leftExponent++;
                }
            }
            if (leftExponent < rightExponent)
            {
                return -right;
            }
            while (leftExponent > rightExponent && leftMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
            {
                if (rightMantissa % 10 == 0)
                {
                    rightMantissa /= 10;
                    rightMantissaDigits--;
                    rightExponent++;
                }
                else
                {
                    leftMantissa *= 10;
                    leftMantissaDigits++;
                    leftExponent--;
                }
            }
            // If the left value could not be shifted to the base of right, shift the right value to
            // the base of left.
            if (leftExponent > rightExponent)
            {
                while (rightMantissa != 0 && rightExponent < leftExponent)
                {
                    if (leftMantissaDigits < MANTISSA_SIGNIFICANT_DIGITS)
                    {
                        leftMantissa *= 10;
                        leftMantissaDigits++;
                        leftExponent--;
                    }
                    else
                    {
                        rightMantissa /= 10;
                        rightMantissaDigits--;
                        rightExponent++;
                    }
                }
                if (rightExponent < leftExponent)
                {
                    return left;
                }
            }

            return new HugeNumber(leftMantissa - rightMantissa, leftExponent);
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
            if (value.IsNaN
                || value.IsInfinite)
            {
                return NaN;
            }
            if (value < 0)
            {
                return -Tan(-value);
            }
            if (value > PI)
            {
                return Tan(value - (PI * (value / PI).Floor()));
            }
            if (value > ThreeQuartersPI)
            {
                return 1 / -Tan(PI - value);
            }
            if (value > HalfPI)
            {
                return -Tan(PI - value);
            }
            if (value > QuarterPI)
            {
                return 1 / Tan(HalfPI - value);
            }
            if (value > EighthPI)
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
            if (value.IsNaN)
            {
                return NaN;
            }
            if (value.IsNegativeInfinity)
            {
                return -1;
            }
            if (value.IsPositiveInfinity)
            {
                return 1;
            }
            if (value.IsZero)
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

        /// <summary>
        /// Gets the integral component of the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">A value to round.</param>
        /// <returns>The integral component of the given <paramref name="value"/>.</returns>
        public static HugeNumber Truncate(HugeNumber value) => value.Exponent < 0 ? value.Mantissa : value;

        /// <summary>
        /// Gets the integral component of this instance.
        /// </summary>
        /// <returns>The integral component of this instance.</returns>
        public HugeNumber Truncate() => Truncate(this);
    }
#pragma warning restore CS0419
}
