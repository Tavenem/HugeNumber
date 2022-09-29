namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
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
}
