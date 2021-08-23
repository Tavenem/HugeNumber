namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to a specified
    /// number of fractional <paramref name="digits"/> using the specified rounding convention.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="value"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="value"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="value"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="value"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// <para>
    /// If <paramref name="value"/> is a rational fraction, it is first converted to a decimal
    /// representation before rounding.
    /// </para>
    /// </remarks>
    public static HugeNumber Round(HugeNumber value, int digits, MidpointRounding mode)
    {
        if (digits is < 0 or > 18)
        {
            throw new ArgumentOutOfRangeException(
                nameof(digits),
                digits,
                $"{nameof(digits)} cannot be less than 0 or greater than 18.");
        }
        if (!Enum.IsDefined(mode))
        {
            throw new ArgumentException(
                $"{nameof(mode)} must be a valid value of {nameof(MidpointRounding)}.",
                nameof(mode));
        }

        if (!value.IsFinite())
        {
            return value;
        }

        if (value.Denominator > 1)
        {
            value = ToDenominator(value, 1);
        }
        if (value.Exponent < -digits)
        {
            var mantissa = value.Mantissa;
            var exponent = value.Exponent;
            while (exponent < -digits - 1)
            {
                mantissa /= 10;
                exponent++;
            }
            var remainder = mode == MidpointRounding.ToZero
                ? 0
                : Math.Abs(mantissa % 10);
            mantissa /= 10;
            exponent++;
            switch (mode)
            {
                case MidpointRounding.ToEven:
                    if (remainder > 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    else if (remainder == 5
                        && mantissa % 2 != 0)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.AwayFromZero:
                    if (remainder >= 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.ToNegativeInfinity:
                    if (remainder > 0
                        && mantissa < 0)
                    {
                        mantissa--;
                    }
                    break;
                case MidpointRounding.ToPositiveInfinity:
                    if (remainder > 0
                        && mantissa > 0)
                    {
                        mantissa++;
                    }
                    break;
            }
            return new HugeNumber(mantissa, exponent);
        }
        return value;
    }

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to a specified
    /// number of fractional <paramref name="digits"/> using the specified rounding convention.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="value"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="value"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="value"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="value"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Round<TInteger>(HugeNumber value, TInteger digits, MidpointRounding mode)
        where TInteger : IBinaryInteger<TInteger>
    {
        var eightteen = TInteger.Create(18);
        if (digits < TInteger.Zero || digits > eightteen)
        {
            throw new ArgumentOutOfRangeException(
                nameof(digits),
                digits,
                $"{nameof(digits)} cannot be less than 0 or greater than 18.");
        }
        if (!Enum.IsDefined(mode))
        {
            throw new ArgumentException(
                $"{nameof(mode)} must be a valid value of {nameof(MidpointRounding)}.",
                nameof(mode));
        }

        if (!value.IsFinite())
        {
            return value;
        }

        var exponent = TInteger.Create(value.Exponent);
        if (exponent < -digits)
        {
            var mantissa = value.Mantissa;
            while (exponent < -digits - TInteger.One)
            {
                mantissa /= 10;
                exponent++;
            }
            var remainder = mode == MidpointRounding.ToZero
                ? 0
                : Math.Abs(mantissa % 10);
            mantissa /= 10;
            exponent++;
            switch (mode)
            {
                case MidpointRounding.ToEven:
                    if (remainder > 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    else if (remainder == 5
                        && mantissa % 2 != 0)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.AwayFromZero:
                    if (remainder >= 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.ToNegativeInfinity:
                    if (remainder > 0
                        && mantissa < 0)
                    {
                        mantissa--;
                    }
                    break;
                case MidpointRounding.ToPositiveInfinity:
                    if (remainder > 0
                        && mantissa > 0)
                    {
                        mantissa++;
                    }
                    break;
            }
            return Create(mantissa, exponent);
        }
        return value;
    }

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to an integer
    /// using the specified rounding convention.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// This method returns a <see cref="HugeNumber"/> instead of an integral type.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    /// <remarks>
    /// If the value of the <paramref name="value"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </remarks>
    public static HugeNumber Round(HugeNumber value, MidpointRounding mode) => Round(value, 0, mode);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to a specified
    /// number of fractional <paramref name="digits"/>, and rounds midpoint values
    /// to the nearest even number.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="value"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="value"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="value"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="value"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Round(HugeNumber value, int digits) => Round(value, digits, MidpointRounding.ToEven);

    /// <summary>
    /// Gets this value rounded to the given number of decimal places.
    /// </summary>
    /// <param name="digits">The number of decimal places to which the value should be
    /// rounded.</param>
    /// <returns>This value rounded to the given number of decimal places.</returns>
    public HugeNumber Round(int digits) => Round(this, digits);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to a specified
    /// number of fractional <paramref name="digits"/>, and rounds midpoint values
    /// to the nearest even number.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="value"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="value"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="value"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="value"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Round<TInteger>(HugeNumber value, TInteger digits)
        where TInteger : IBinaryInteger<TInteger> => Round(value, digits, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest integral value,
    /// and rounds midpoint values to the nearest even number.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// This method returns a <see cref="HugeNumber"/> instead of an integral type.
    /// </returns>
    /// <remarks>
    /// If the value of the <paramref name="value"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="value"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </remarks>
    public static HugeNumber Round(HugeNumber value) => Round(value, 0, MidpointRounding.ToEven);

    /// <summary>
    /// Gets the nearest whole integral value to this instance.
    /// </summary>
    /// <returns>The nearest whole integral value to this instance.</returns>
    public HugeNumber Round() => Round(this);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="int"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static int RoundToInt(HugeNumber value, MidpointRounding mode)
    {
        if (value < int.MinValue)
        {
            return int.MinValue;
        }
        else if (value > int.MaxValue)
        {
            return int.MaxValue;
        }
        else
        {
            return (int)Round(value, mode);
        }
    }

    /// <summary>
    /// Rounds this instance to the nearest <see cref="int"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public int RoundToInt(MidpointRounding mode) => RoundToInt(this, mode);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="int"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static int RoundToInt(HugeNumber value) => RoundToInt(value, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds this instance to the nearest <see cref="int"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="int.MinValue"/> or <see cref="int.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public int RoundToInt() => RoundToInt(this, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="long"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static long RoundToLong(HugeNumber value, MidpointRounding mode)
    {
        if (value < long.MinValue)
        {
            return long.MinValue;
        }
        else if (value > long.MaxValue)
        {
            return long.MaxValue;
        }
        else
        {
            return (long)Round(value, mode);
        }
    }

    /// <summary>
    /// Rounds this instance to the nearest <see cref="long"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public long RoundToLong(MidpointRounding mode) => RoundToLong(this, mode);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="long"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static long RoundToLong(HugeNumber value) => RoundToLong(value, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds this instance to the nearest <see cref="long"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="long.MinValue"/> or <see cref="long.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public long RoundToLong() => RoundToLong(this, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="uint"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="uint.MinValue"/> or <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static uint RoundToUInt(HugeNumber value, MidpointRounding mode)
    {
        if (value < uint.MinValue)
        {
            return uint.MinValue;
        }
        else if (value > uint.MaxValue)
        {
            return uint.MaxValue;
        }
        else
        {
            return (uint)Round(value, mode);
        }
    }

    /// <summary>
    /// Rounds this instance to the nearest <see cref="uint"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="uint.MinValue"/> or <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public uint RoundToUInt(MidpointRounding mode) => RoundToUInt(this, mode);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="uint"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="uint.MinValue"/> or <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static uint RoundToUInt(HugeNumber value) => RoundToUInt(value, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds this instance to the nearest <see cref="uint"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="uint.MinValue"/> or <see cref="uint.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public uint RoundToUInt() => RoundToUInt(this, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="ulong"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="ulong.MinValue"/> or <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static ulong RoundToULong(HugeNumber value, MidpointRounding mode)
    {
        if (value < ulong.MinValue)
        {
            return ulong.MinValue;
        }
        else if (value > ulong.MaxValue)
        {
            return ulong.MaxValue;
        }
        else
        {
            return (ulong)Round(value, mode);
        }
    }

    /// <summary>
    /// Rounds this instance to the nearest <see cref="ulong"/>
    /// using the specified rounding convention.
    /// Truncates to <see cref="ulong.MinValue"/> or <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public ulong RoundToULong(MidpointRounding mode) => RoundToULong(this, mode);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="value"/> to the nearest <see cref="ulong"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="ulong.MinValue"/> or <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <param name="value">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <returns>
    /// The integer that <paramref name="value"/> is rounded to.
    /// </returns>
    public static ulong RoundToULong(HugeNumber value) => RoundToULong(value, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds this instance to the nearest <see cref="ulong"/>,
    /// and rounds midpoint values to the nearest even number.
    /// Truncates to <see cref="ulong.MinValue"/> or <see cref="ulong.MaxValue"/> rather than failing on overflow.
    /// </summary>
    /// <returns>
    /// The integer that this instance is rounded to.
    /// </returns>
    public ulong RoundToULong() => RoundToULong(this, MidpointRounding.ToEven);

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
    /// Returns zero if <paramref name="value"/> is nearly equal to zero (cf.
    /// <see cref="IsNearlyZero(HugeNumber)"/>), or <paramref name="value"/> itself if not.
    /// </summary>
    /// <param name="value">A value.</param>
    /// <returns><see cref="Zero"/>, if <paramref name="value"/> is nearly equal to it;
    /// otherwise <paramref name="value"/>.</returns>
    public static HugeNumber SnapToZero(HugeNumber value) => value.IsNearlyZero() ? Zero : value;

    /// <summary>
    /// Returns zero if this instance is nearly equal to zero (cf.
    /// <see cref="IsNearlyZero(HugeNumber)"/>), or this instance if not.
    /// </summary>
    /// <returns>
    /// <see cref="Zero"/>, if this instance is nearly equal to it; otherwise this instance.
    /// </returns>
    public HugeNumber SnapToZero() => SnapTo(this);

    /// <summary>
    /// Gets the integral component of the given <paramref name="value"/>.
    /// </summary>
    /// <param name="value">A value to round.</param>
    /// <returns>The integral component of the given <paramref name="value"/>.</returns>
    public static HugeNumber Truncate(HugeNumber value)
    {
        if (value.Exponent < -value.MantissaDigits)
        {
            return Zero;
        }
        if (value.Exponent < 0)
        {
            var result = value.Mantissa;
            for (var i = 0; i < -value.Exponent; i++)
            {
                result /= 10;
            }
            return result;
        }
        return value;
    }

    /// <summary>
    /// Gets the integral component of this instance.
    /// </summary>
    /// <returns>The integral component of this instance.</returns>
    public HugeNumber Truncate() => Truncate(this);
}
