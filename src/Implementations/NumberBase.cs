namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Computes the absolute of a value.
    /// </summary>
    /// <param name="value">The value for which to get its absolute.</param>
    /// <returns>
    /// The absolute of <paramref name="value"/>.
    /// </returns>
    public static HugeNumber Abs(HugeNumber value)
        => new(Math.Abs(value.Mantissa), value.Denominator, value.Exponent);

    /// <summary>
    /// Computes the absolute of this value.
    /// </summary>
    /// <returns>
    /// The absolute of this value.
    /// </returns>
    public HugeNumber Abs() => Abs(this);

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <returns>
    /// A value of type <typeparamref name="TTarget"/> with the same value this instance.
    /// </returns>
    /// <remarks>
    /// This method performs a checked conversion.
    /// </remarks>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="TTarget"/> is not supported.
    /// </exception>
    public TTarget CreateChecked<TTarget>() => TryCreateChecked<TTarget>(out var result)
        ? result
        : throw new NotSupportedException($"Conversion from type {typeof(TTarget).Name} not supported");

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <returns>
    /// <para>
    /// A value of type <typeparamref name="TTarget"/> with the same value as this instance.
    /// </para>
    /// <para>
    /// -or- if this instance is less than the minimum allowed value of
    /// <typeparamref name="TTarget"/>, the minimum allowed value.
    /// </para>
    /// <para>
    /// -or- if this instance is greater than the maximum allowed value of
    /// <typeparamref name="TTarget"/>, the maximum allowed value.
    /// </para>
    /// </returns>
    /// <remarks>
    /// This method performs a saturating (clamped) conversion.
    /// </remarks>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="TTarget"/> is not supported.
    /// </exception>
    public TTarget CreateSaturating<TTarget>() => CreateChecked<TTarget>();

    /// <summary>
    /// Create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <returns>
    /// A value of type <typeparamref name="TTarget"/> with the same value as this instance.
    /// </returns>
    /// <remarks>
    /// This method performs a truncating conversion.
    /// </remarks>
    /// <exception cref="NotSupportedException">
    /// <typeparamref name="TTarget"/> is not supported.
    /// </exception>
    public TTarget CreateTruncating<TTarget>() => CreateChecked<TTarget>();

    /// <summary>
    /// Determines if a value is in its canonical representation.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is in its canonical representation;
    /// otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Always returns <see langword="true"/> for <see cref="HugeNumber"/>.
    /// </remarks>
    public static bool IsCanonical(HugeNumber value) => true;

    /// <summary>
    /// Determines if a value represents a complex number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is a complex number; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Always returns <see langword="false"/> for <see cref="HugeNumber"/>.
    /// </remarks>
    public static bool IsComplexNumber(HugeNumber value) => false;

    /// <summary>
    /// Determines if a value represents an even integral number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an even integer; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </para>
    /// <para>
    /// A return value of <see langword="false"/> does not imply that <see
    /// cref="IsOddInteger(HugeNumber)"/> will return <see langword="true"/>. A number with a
    /// fractional portion, for example, <c>3.3</c>, is not even or odd.
    /// </para>
    /// </remarks>
    public static bool IsEvenInteger(HugeNumber value)
    {
        if (value.Denominator != 1
            || value.Exponent != 0)
        {
            return false;
        }
        return long.IsEvenInteger(value.Mantissa);
    }

    /// <summary>
    /// Determines if a value represents an even integral number.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this value is an even integer; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </para>
    /// <para>
    /// A return value of <see langword="false"/> does not imply that <see
    /// cref="IsOddInteger()"/> will return <see langword="true"/>. A number with a
    /// fractional portion, for example, <c>3.3</c>, is not even or odd.
    /// </para>
    /// </remarks>
    public bool IsEvenInteger() => IsEvenInteger(this);

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
    /// Determines if a value represents an imaginary number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an imaginary number; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Always returns <see langword="false"/> for <see cref="HugeNumber"/>.
    /// </remarks>
    public static bool IsImaginaryNumber(HugeNumber value) => false;

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
    /// Determines if a value represents an integral number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an integer; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </remarks>
    public static bool IsInteger(HugeNumber value) => value.Denominator == 1 && value.Exponent == 0;

    /// <summary>
    /// Determines if a value represents an integral number.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this value is an integer; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </remarks>
    public bool IsInteger() => IsInteger(this);

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
    /// Determines if a value represents an odd integral number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is an odd integer; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </para>
    /// <para>
    /// A return value of <see langword="false"/> does not imply that <see
    /// cref="IsEvenInteger(HugeNumber)"/> will return <see langword="true"/>. A number with a
    /// fractional portion, for example, <c>3.3</c>, is not even or odd.
    /// </para>
    /// </remarks>
    public static bool IsOddInteger(HugeNumber value)
    {
        if (value.Denominator != 1
            || value.Exponent != 0)
        {
            return false;
        }
        return long.IsOddInteger(value.Mantissa);
    }

    /// <summary>
    /// Determines if a value represents an odd integral number.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this value is an odd integer; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// <para>
    /// This method correctly handles floating-point values and so <c>2.0</c> will return <see
    /// langword="true"/> while <c>2.2</c> will return <see langword="false"/>.
    /// </para>
    /// <para>
    /// A return value of <see langword="false"/> does not imply that <see
    /// cref="IsEvenInteger()"/> will return <see langword="true"/>. A number with a
    /// fractional portion, for example, <c>3.3</c>, is not even or odd.
    /// </para>
    /// </remarks>
    public bool IsOddInteger() => IsOddInteger(this);

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
    /// Determines if a value represents a real number.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is a real number; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Always returns <see langword="true"/> for <see cref="HugeNumber"/>.
    /// </remarks>
    public static bool IsRealNumber(HugeNumber value) => true;

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
    /// Determines if a value is zero.
    /// </summary>
    /// <param name="value">The value to be checked.</param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> is zero; otherwise, <see
    /// langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function treats both positive and negative zero as zero and so will return <see
    /// langword="true"/> for <c>+0.0</c> and <c>-0.0</c>.
    /// </remarks>
    public static bool IsZero(HugeNumber value) => value.Mantissa == 0 && value.Denominator != 0;

    /// <summary>
    /// Determines if a value is zero.
    /// </summary>
    /// <returns>
    /// <see langword="true"/> if this value is zero; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// This function treats both positive and negative zero as zero and so will return <see
    /// langword="true"/> for <c>+0.0</c> and <c>-0.0</c>.
    /// </remarks>
    public bool IsZero() => IsZero(this);

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
    /// This method matches the IEEE 754:2019 <c>maximumMagnitude</c> function. This requires NaN
    /// inputs to be propagated back to the caller and for <c>-0.0</c> to be treated as less than
    /// <c>+0.0</c>.
    /// </remarks>
    public static HugeNumber MaxMagnitude(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        var leftAbs = x.Abs();
        var rightAbs = y.Abs();
        if (leftAbs > rightAbs
            || (leftAbs == rightAbs
            && (y < 0
            || (y.IsZero() && y.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    /// <summary>
    /// Compares two values to compute which has the greater magnitude and returning the other value
    /// if an input is <c>NaN</c>.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is greater than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// This method matches the IEEE 754:2019 <c>maximumMagnitudeNumber</c> function. This requires
    /// NaN inputs to not be propagated back to the caller and for <c>-0.0</c> to be treated as less
    /// than <c>+0.0</c>.
    /// </remarks>
    public static HugeNumber MaxMagnitudeNumber(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN())
        {
            return y;
        }
        if (y.IsNaN())
        {
            return x;
        }
        var leftAbs = x.Abs();
        var rightAbs = y.Abs();
        if (leftAbs > rightAbs
            || (leftAbs == rightAbs
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
    /// This method matches the IEEE 754:2019 <c>minimumMagnitude</c> function. This requires NaN
    /// inputs to be propagated back to the caller and for <c>-0.0</c> to be treated as less than
    /// <c>+0.0</c>.
    /// </remarks>
    public static HugeNumber MinMagnitude(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN() || y.IsNaN())
        {
            return NaN;
        }
        var leftAbs = x.Abs();
        var rightAbs = y.Abs();
        if (leftAbs < rightAbs
            || (leftAbs == rightAbs
            && (x < 0
            || (x.IsZero() && x.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    /// <summary>
    /// Compares two values to compute which has the lesser magnitude and returning the other value
    /// if an input is <c>NaN</c>.
    /// </summary>
    /// <param name="x">The value to compare with <paramref name="y"/>.</param>
    /// <param name="y">The value to compare with <paramref name="x"/>.</param>
    /// <returns>
    /// <paramref name="x"/> if it is less than <paramref name="y"/>; otherwise <paramref
    /// name="y"/>.
    /// </returns>
    /// <remarks>
    /// This method matches the IEEE 754:2019 <c>minimumMagnitudeNumber</c> function. This requires
    /// NaN inputs to not be propagated back to the caller and for <c>-0.0</c> to be treated as less
    /// than <c>+0.0</c>.
    /// </remarks>
    public static HugeNumber MinMagnitudeNumber(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN())
        {
            return y;
        }
        if (y.IsNaN())
        {
            return x;
        }
        var leftAbs = x.Abs();
        var rightAbs = y.Abs();
        if (leftAbs < rightAbs
            || (leftAbs == rightAbs
            && (x < 0
            || (x.IsZero() && x.Exponent < 0))))
        {
            return x;
        }
        return y;
    }

    static bool INumberBase<HugeNumber>.TryConvertFromChecked<TOther>(TOther value, out HugeNumber result)
        => TryCreate(value, out result);

    static bool INumberBase<HugeNumber>.TryConvertFromSaturating<TOther>(TOther value, out HugeNumber result)
        => TryCreate(value, out result);

    static bool INumberBase<HugeNumber>.TryConvertFromTruncating<TOther>(TOther value, out HugeNumber result)
        => TryCreate(value, out result);

    static bool INumberBase<HugeNumber>.TryConvertToChecked<TOther>(HugeNumber value, out TOther result) where TOther : default
        => value.TryCreateChecked(out result);

    static bool INumberBase<HugeNumber>.TryConvertToSaturating<TOther>(HugeNumber value, out TOther result) where TOther : default
        => value.TryCreateSaturating(out result);

    static bool INumberBase<HugeNumber>.TryConvertToTruncating<TOther>(HugeNumber value, out TOther result) where TOther : default
        => value.TryCreateTrancating(out result);
}
