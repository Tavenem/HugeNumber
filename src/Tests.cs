namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Determines whether the specified value is finite (neither positive or negative
    /// infinity, nor <see cref="NaN"/>).
    /// </summary>
    public static bool IsFinite(HugeNumber x) => x.Mantissa is >= MIN_MANTISSA and <= MAX_MANTISSA;

    /// <summary>
    /// Determines whether the specified value is finite (neither positive or negative
    /// infinity, nor <see cref="NaN"/>).
    /// </summary>
    public bool IsFinite() => IsFinite(this);

    /// <summary>
    /// Returns a value indicating whether the specified number evaluates to negative or positive infinity.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsInfinity(HugeNumber x) => x.IsPositiveInfinity() || x.IsNegativeInfinity();

    /// <summary>
    /// Returns a value indicating whether this instance evaluates to negative or positive infinity.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance evaluates to <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsInfinity() => IsInfinity(this);

    /// <summary>
    /// Returns a value that indicates whether the specified value is not a number (<see cref="NaN"/>).
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to <see cref="NaN"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Floating-point operations return <see cref="NaN"/> to signal that result of the operation is undefined.
    /// For example, dividing <see cref="Zero"/> by <see cref="Zero"/> results in <see cref="NaN"/>.
    /// </para>
    /// </remarks>
    public static bool IsNaN(HugeNumber x) => x.Mantissa == long.MaxValue;

    /// <summary>
    /// Returns a value that indicates whether this instance is not a number (<see cref="NaN"/>).
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance evaluates to <see cref="NaN"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Floating-point operations return <see cref="NaN"/> to signal that result of the operation is undefined.
    /// For example, dividing <see cref="Zero"/> by <see cref="Zero"/> results in <see cref="NaN"/>.
    /// </para>
    /// </remarks>
    public bool IsNaN() => IsNaN(this);

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
    /// Determines if a <see cref="HugeNumber"/> value is nearly zero.
    /// </summary>
    /// <param name="value">A value to test.</param>
    /// <returns>
    /// <see langword="true"/> if the value is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NearlyZero"/> as the threshhold for closeness to zero.
    /// </remarks>
    public static bool IsNearlyZero(HugeNumber value) => value < NearlyZero && value > (-NearlyZero);

    /// <summary>
    /// Determines if this instance is nearly zero.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance is close to 0; otherwise <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Uses <see cref="NearlyZero"/> as the threshhold for closeness to zero.
    /// </remarks>
    public bool IsNearlyZero() => IsNearlyZero(this);

    /// <summary>
    /// Determines whether the specified value is negative.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is negative;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsNegative(HugeNumber x) => x.Mantissa < 0 || (x.Mantissa == 0 && x.Exponent < 0);

    /// <summary>
    /// Determines whether this instance is negative.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance is negative;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsNegative() => IsNegative(this);

    /// <summary>
    /// Returns a value indicating whether the specified number evaluates to negative infinity.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to <see cref="NegativeInfinity"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Floating-point operations return <see cref="NegativeInfinity"/> to signal an overflow condition.
    /// </remarks>
    public static bool IsNegativeInfinity(HugeNumber x) => x.Mantissa < MIN_MANTISSA;

    /// <summary>
    /// Returns a value indicating whether this instance evaluates to negative infinity.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance evaluates to <see cref="NegativeInfinity"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Floating-point operations return <see cref="NegativeInfinity"/> to signal an overflow condition.
    /// </remarks>
    public bool IsNegativeInfinity() => IsNegativeInfinity(this);

    /// <summary>
    /// Determines whether the specified value is normal.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is normal;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Always <see langword="true"/> for <see cref="HugeNumber"/>, which does not require
    /// subnormal numbers to represent small values around zero.
    /// </remarks>
    public static bool IsNormal(HugeNumber x) => true;

    /// <summary>
    /// Determines whether the specified value is positive.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is positive;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsPositive(HugeNumber x) => !x.IsNaN() && !x.IsNegative();

    /// <summary>
    /// Determines whether this instance is positive.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance is positive;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsPositive() => IsPositive(this);

    /// <summary>
    /// Returns a value indicating whether the specified number evaluates to positive infinity.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> evaluates to <see cref="PositiveInfinity"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Floating-point operations return <see cref="PositiveInfinity"/> to signal an overflow condition.
    /// </remarks>
    public static bool IsPositiveInfinity(HugeNumber x) => !x.IsNaN() && x.Mantissa > MAX_MANTISSA;

    /// <summary>
    /// Returns a value indicating whether this instance evaluates to positive infinity.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance evaluates to <see cref="PositiveInfinity"/>;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Floating-point operations return <see cref="PositiveInfinity"/> to signal an overflow condition.
    /// </remarks>
    public bool IsPositiveInfinity() => IsPositiveInfinity(this);

    /// <summary>
    /// Determines whether the specified value is subnormal.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is subnormal;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Always <see langword="false"/> for <see cref="HugeNumber"/>, which does not require
    /// subnormal numbers to represent small values around zero.
    /// </remarks>
    public static bool IsSubnormal(HugeNumber x) => false;

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
    /// The sign of <see cref="NaN"/> is 0.
    /// </remarks>
    public static HugeNumber Sign(HugeNumber value)
    {
        if (value.IsPositiveInfinity())
        {
            return One;
        }
        else if (value.IsNegativeInfinity())
        {
            return NegativeOne;
        }
        else if (value.IsNaN())
        {
            return Zero;
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
    /// The sign of <see cref="NaN"/> is 0.
    /// </remarks>
    public HugeNumber Sign() => Sign(this);
}
