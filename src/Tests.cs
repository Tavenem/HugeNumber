namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Determines whether the specified value is finite (neither positive or negative
    /// infinity, nor <see cref="NaN"/>).
    /// </summary>
    public static bool IsFinite(HugeNumber x) => x.Denominator > 0;

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
    public static bool IsInfinity(HugeNumber x) => x.Mantissa != 0 && x.Denominator == 0;

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
    public static bool IsNaN(HugeNumber x) => x.Mantissa == 0 && x.Denominator == 0;

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

        return Abs(value - other) < epsilon;
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
    public static bool IsNegativeInfinity(HugeNumber x) => x.Mantissa < 0 && x.Denominator == 0;

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
    /// Returns a value indicating whether the specified number represents a non-integral value
    /// which is not expressed as a rational fraction.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is <see cref="NaN"/>,
    /// <see cref="PositiveInfinity"/>, or <see cref="NegativeInfinity"/>, or has a denominator of
    /// one and a non-zero exponent; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Note that this method does not determine whether the value is an irrational number
    /// according to the mathematical definition (which is why the method is not called
    /// IsIrrational, to avoid confusion). It only determines if the value is not either integral
    /// or a rational fraction.
    /// </para>
    /// <para>
    /// For instance: an integral value or a rational fraction (by the mathematical definition)
    /// which is too large or small to be represented by a <see cref="HugeNumber"/> would return
    /// <see langword="true"/>. So would the result of a mathematical operation which has too many
    /// significant digits to be represented fully.
    /// </para>
    /// <para>
    /// This method's intended purpose is to determine when appropriate measures should be taken to
    /// guard against mathematical and/or rounding errors which might occur due to imprecision,
    /// not to determine mathematical irrationality.
    /// </para>
    /// </remarks>
    public static bool IsNotRational(HugeNumber x) => IsNaN(x)
        || IsInfinity(x)
        || (x.Denominator == 1 && x.Exponent != 0);

    /// <summary>
    /// Returns a value indicating whether the specified number represents a non-integral value
    /// which is not expressed as a rational fraction.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance is <see cref="NaN"/>,
    /// <see cref="PositiveInfinity"/>, or <see cref="NegativeInfinity"/>, or has a denominator of
    /// one and a non-zero exponent; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// Note that this method does not determine whether the value is an irrational number
    /// according to the mathematical definition (which is why the method is not called
    /// IsIrrational, to avoid confusion). It only determines if the value is not either integral
    /// or a rational fraction.
    /// </para>
    /// <para>
    /// For instance: an integral value or a rational fraction (by the mathematical definition)
    /// which is too large or small to be represented by a <see cref="HugeNumber"/> would return
    /// <see langword="true"/>. So would the result of a mathematical operation which has too many
    /// significant digits to be represented fully.
    /// </para>
    /// <para>
    /// This method's intended purpose is to determine when appropriate measures should be taken to
    /// guard against mathematical and/or rounding errors which might occur due to imprecision,
    /// not to determine mathematical irrationality.
    /// </para>
    /// </remarks>
    public bool IsNotRational() => IsNotRational(this);

    /// <summary>
    /// Determines whether the specified value is positive.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is positive;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsPositive(HugeNumber x) => x.Mantissa > 0;

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
    public static bool IsPositiveInfinity(HugeNumber x) => x.Mantissa > 0 && x.Denominator == 0;

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
    /// Returns a value indicating whether the specified number represents either an integral value
    /// or a rational fraction.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/>.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="x"/> is not <see cref="NaN"/>,
    /// <see cref="PositiveInfinity"/>, or <see cref="NegativeInfinity"/>, and has either an
    /// exponent of zero (indicating an integegral value), or a non-zero denominator (indicating a
    /// rational fraction); otherwise, <see langword="false"/>.
    /// </returns>
    public static bool IsRational(HugeNumber x) => !IsNaN(x)
        && !IsInfinity(x)
        && (x.Exponent == 0 || x.Denominator != 0);

    /// <summary>
    /// Returns a value indicating whether the specified number represents either an integral value
    /// or a rational fraction.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this instance is not <see cref="NaN"/>,
    /// <see cref="PositiveInfinity"/>, or <see cref="NegativeInfinity"/>, and has either an
    /// exponent of zero (indicating an integegral value), or a non-zero denominator (indicating a
    /// rational fraction); otherwise, <see langword="false"/>.
    /// </returns>
    public bool IsRational() => IsRational(this);

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
    public static HugeNumber Sign(HugeNumber value) => Math.Sign(value.Mantissa);

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
