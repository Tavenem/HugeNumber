using System.Text.Json.Serialization;

namespace Tavenem.HugeNumbers;

/// <summary>
/// Allows efficient recording of values in the range ±999999999999999999e±32767, as well as
/// rational fractions such as ⅓. Also allows representing positive or negative infinity, and
/// <c>NaN</c> (not a number).
/// </summary>
[JsonConverter(typeof(HugeNumberConverter))]
public readonly partial struct HugeNumber :
    IAdditionOperators<HugeNumber, decimal, HugeNumber>,
    IAdditionOperators<HugeNumber, double, HugeNumber>,
    IAdditionOperators<HugeNumber, long, HugeNumber>,
    IAdditionOperators<HugeNumber, ulong, HugeNumber>,
    IComparable,
    IComparable<double>,
    IComparable<float>,
    IComparable<int>,
    IComparable<long>,
    IComparable<ulong>,
    IComparisonOperators<HugeNumber, decimal, bool>,
    IComparisonOperators<HugeNumber, double, bool>,
    IComparisonOperators<HugeNumber, long, bool>,
    IComparisonOperators<HugeNumber, ulong, bool>,
    IConvertible,
    IDivisionOperators<HugeNumber, decimal, HugeNumber>,
    IDivisionOperators<HugeNumber, double, HugeNumber>,
    IDivisionOperators<HugeNumber, long, HugeNumber>,
    IDivisionOperators<HugeNumber, ulong, HugeNumber>,
    IEquatable<double>,
    IEquatable<float>,
    IEquatable<int>,
    IEquatable<long>,
    IEquatable<ulong>,
    IFloatingPointIeee754<HugeNumber>,
    IMinMaxValue<HugeNumber>,
    IModulusOperators<HugeNumber, decimal, HugeNumber>,
    IModulusOperators<HugeNumber, double, HugeNumber>,
    IModulusOperators<HugeNumber, long, HugeNumber>,
    IModulusOperators<HugeNumber, ulong, HugeNumber>,
    IMultiplyOperators<HugeNumber, decimal, HugeNumber>,
    IMultiplyOperators<HugeNumber, double, HugeNumber>,
    IMultiplyOperators<HugeNumber, long, HugeNumber>,
    IMultiplyOperators<HugeNumber, ulong, HugeNumber>,
    ISubtractionOperators<HugeNumber, decimal, HugeNumber>,
    ISubtractionOperators<HugeNumber, double, HugeNumber>,
    ISubtractionOperators<HugeNumber, long, HugeNumber>,
    ISubtractionOperators<HugeNumber, ulong, HugeNumber>
{
    private const byte MANTISSA_SIGNIFICANT_DIGITS = 18;
    private const short MAX_EXPONENT = short.MaxValue;
    private const short MIN_EXPONENT = short.MinValue;
    private const long MAX_MANTISSA = 999999999999999999;
    private const long MIN_MANTISSA = -999999999999999999;

    /// <summary>
    /// Gets the radix, or base, for the type.
    /// </summary>
    /// <remarks>
    /// Returns 10 for <see cref="HugeNumber"/>.
    /// </remarks>
    public static int Radix => 10;

    /// <summary>
    /// <para>
    /// When this <see cref="HugeNumber"/> represents a rational fraction, this value indicates the
    /// denominator of that fraction.
    /// </para>
    /// <para>
    /// When this <see cref="HugeNumber"/> represents a whole or irrational number, this value will
    /// be one.
    /// </para>
    /// <para>
    /// When this <see cref="HugeNumber"/> represents positive or negative infinity, or
    /// <see cref="NaN"/> (not a number), this value will be zero.
    /// </para>
    /// </summary>
    public ushort Denominator { get; }

    /// <summary>
    /// The power of ten by which the <see cref="Mantissa"/> is multiplied to determine the
    /// value of this <see cref="HugeNumber"/>.
    /// </summary>
    public short Exponent { get; }

    /// <summary>
    /// <para>
    /// When this <see cref="HugeNumber"/> represents a rational fraction, this is the numerator of
    /// that fraction.
    /// </para>
    /// <para>
    /// When this <see cref="HugeNumber"/> represents a whole or irrational number, this is the
    /// value which is multiplied by ten times the <see cref="Exponent"/> to give the complete
    /// value.
    /// </para>
    /// </summary>
    public long Mantissa { get; }

    /// <summary>
    /// <para>
    /// The number of digits in the <see cref="Mantissa"/>.
    /// </para>
    /// <para>
    /// Not necessarily the same as the number of significant digits, which is undefined.
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// <see cref="HugeNumber"/> values are always recorded with the exponent closest to zero
    /// possible which does not unnecessarily eliminate nonzero significant digits in the
    /// mantissa. For example, the value 1e50 is represented by a mantissa of 100000000000000000
    /// (with the maximum of eighteen digits) and an exponent of 33. The value 1.23e-4 would be
    /// represented by a mantissa of 123 and an exponent of -6, since -6 is the closest exponent
    /// to 0 which does not eliminate any of the three significant digits.
    /// </para>
    /// <para>
    /// <see cref="HugeNumber"/> values do not preserve trailing zero significant digits. A value of
    /// 1.2300e-4 would be represented identically to 1.23e-4, for instance. Additionally,
    /// trailing zeros may be introduced to the mantissa in order to reduce the exponent as much
    /// as possible, as was shown above with 1e50. This method ensures that <see cref="HugeNumber"/>
    /// instances with equal values are always represented in the same way, which greatly
    /// simplifies mathematical operations and comparisons.
    /// </para>
    /// <para>
    /// This method has the downside, however, of not preserving significant trailing zeros, and
    /// of introducing them when that precision was not implied by the original value or
    /// operation. If your use case requires direct control over, or accurate preservation of,
    /// the number of significant trailing zeros, the <see cref="HugeNumber"/> class will not be
    /// appropriate to your needs.
    /// </para>
    /// </remarks>
    public byte MantissaDigits { get; }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/>, and an <see cref="Exponent"/> of 0.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    public HugeNumber(long mantissa)
    {
        short exponent = 0;
        var mantissaDigits = GetMantissaDigits(mantissa);
        Reduce(ref mantissa, ref exponent, ref mantissaDigits);
        Mantissa = mantissa;
        MantissaDigits = mantissaDigits;
        Exponent = exponent;
        Denominator = 1;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/>, and an <see cref="Exponent"/> of 0.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    public HugeNumber(ulong mantissa)
    {
        short exponent = 0;
        var mantissaDigits = GetMantissaDigits(mantissa);
        Mantissa = Reduce(mantissa, ref exponent, ref mantissaDigits);
        MantissaDigits = mantissaDigits;
        Exponent = exponent;
        Denominator = 1;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(long mantissa, short exponent)
    {
        var mantissaDigits = GetMantissaDigits(mantissa);
        Reduce(ref mantissa, ref exponent, ref mantissaDigits);
        Mantissa = mantissa;
        MantissaDigits = mantissaDigits;
        Exponent = exponent;
        Denominator = 1;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(long mantissa, int exponent)
    {
        if (exponent > MAX_EXPONENT)
        {
            Mantissa = 1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else if (exponent < MIN_EXPONENT)
        {
            Mantissa = -1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var mantissaDigits = GetMantissaDigits(mantissa);
            Reduce(ref mantissa, ref sExponent, ref mantissaDigits);
            Mantissa = mantissa;
            MantissaDigits = mantissaDigits;
            Exponent = sExponent;
            Denominator = 1;
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(ulong mantissa, short exponent)
    {
        var mantissaDigits = GetMantissaDigits(mantissa);
        Mantissa = Reduce(mantissa, ref exponent, ref mantissaDigits);
        MantissaDigits = mantissaDigits;
        Exponent = exponent;
        Denominator = 1;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(ulong mantissa, int exponent)
    {
        if (exponent > MAX_EXPONENT)
        {
            Mantissa = 1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else if (exponent < MIN_EXPONENT)
        {
            Mantissa = -1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var mantissaDigits = GetMantissaDigits(mantissa);
            Mantissa = Reduce(mantissa, ref sExponent, ref mantissaDigits);
            MantissaDigits = mantissaDigits;
            Exponent = sExponent;
            Denominator = 1;
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(decimal mantissa, short exponent)
    {
        var val = (HugeNumber)mantissa;
        if (MAX_EXPONENT - exponent < val.Exponent)
        {
            Mantissa = val >= 0 ? 1 : -1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else
        {
            var vMantissa = val.Mantissa;
            var vMantissaDigits = val.MantissaDigits;
            var totalExponent = (short)(val.Exponent + exponent);
            Reduce(ref vMantissa, ref totalExponent, ref vMantissaDigits);
            Mantissa = vMantissa;
            MantissaDigits = vMantissaDigits;
            Exponent = totalExponent;
            Denominator = 1;
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(decimal mantissa, int exponent)
    {
        if (exponent > MAX_EXPONENT)
        {
            Mantissa = 1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else if (exponent < MIN_EXPONENT)
        {
            Mantissa = -1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - sExponent < val.Exponent)
            {
                Mantissa = val >= 0 ? 1 : -1;
                MantissaDigits = 0;
                Exponent = 0;
                Denominator = 0;
            }
            else
            {
                var vMantissa = val.Mantissa;
                var vMantissaDigits = val.MantissaDigits;
                var totalExponent = (short)(val.Exponent + sExponent);
                Reduce(ref vMantissa, ref totalExponent, ref vMantissaDigits);
                Mantissa = vMantissa;
                MantissaDigits = vMantissaDigits;
                Exponent = totalExponent;
                Denominator = 1;
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(double mantissa, short exponent)
    {
        if (double.IsNaN(mantissa))
        {
            Mantissa = 0;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else if (double.IsPositiveInfinity(mantissa))
        {
            Mantissa = 1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 1;
        }
        else if (double.IsNegativeInfinity(mantissa))
        {
            Mantissa = -1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else
        {
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - exponent < val.Exponent)
            {
                Mantissa = val >= 0 ? 1 : -1;
                MantissaDigits = 0;
                Exponent = 0;
                Denominator = 0;
            }
            else
            {
                var vMantissa = val.Mantissa;
                var vMantissaDigits = val.MantissaDigits;
                var totalExponent = (short)(val.Exponent + exponent);
                Reduce(ref vMantissa, ref totalExponent, ref vMantissaDigits);
                Mantissa = vMantissa;
                MantissaDigits = vMantissaDigits;
                Exponent = totalExponent;
                Denominator = 1;
            }
        }
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public HugeNumber(double mantissa, int exponent)
    {
        if (double.IsNaN(mantissa))
        {
            Mantissa = 0;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else if (double.IsPositiveInfinity(mantissa)
            || exponent > MAX_EXPONENT)
        {
            Mantissa = 1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 0;
        }
        else if (double.IsNegativeInfinity(mantissa)
            || exponent < MIN_EXPONENT)
        {
            Mantissa = -1;
            MantissaDigits = 0;
            Exponent = 0;
            Denominator = 1;
        }
        else
        {
            var sExponent = (short)exponent;
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - sExponent < val.Exponent)
            {
                Mantissa = val >= 0 ? 1 : -1;
                MantissaDigits = 0;
                Exponent = 0;
                Denominator = 0;
            }
            else
            {
                var vMantissa = val.Mantissa;
                var vMantissaDigits = val.MantissaDigits;
                var totalExponent = (short)(val.Exponent + sExponent);
                Reduce(ref vMantissa, ref totalExponent, ref vMantissaDigits);
                Mantissa = vMantissa;
                MantissaDigits = vMantissaDigits;
                Exponent = totalExponent;
                Denominator = 1;
            }
        }
    }

    internal HugeNumber(long mantissa, ushort denominator, short exponent = 1, bool withReduce = false)
    {
        var mantissaDigits = GetMantissaDigits(mantissa);
        if (withReduce && denominator == 1)
        {
            Reduce(ref mantissa, ref exponent, ref mantissaDigits);
        }
        Mantissa = mantissa;
        MantissaDigits = mantissaDigits;
        Denominator = denominator;
        Exponent = exponent;
    }

    /// <summary>
    /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
    /// name="mantissa"/> and <paramref name="exponent"/>.
    /// </summary>
    /// <param name="mantissa">The mantissa.</param>
    /// <param name="exponent">The exponent.</param>
    public static HugeNumber Create<TMantissa, TExponent>(TMantissa mantissa, TExponent exponent)
        where TMantissa : IBinaryInteger<TMantissa>
        where TExponent : IBinaryInteger<TExponent>
    {
        if (exponent > TExponent.CreateSaturating(short.MaxValue))
        {
            return PositiveInfinity;
        }
        else if (exponent < TExponent.CreateSaturating(short.MinValue))
        {
            return NegativeInfinity;
        }
        var e = (short)(object)exponent;
        if (mantissa > TMantissa.CreateSaturating(long.MaxValue))
        {
            return new HugeNumber((ulong)(object)mantissa, e);
        }
        else
        {
            return new HugeNumber((long)(object)mantissa, e);
        }
    }

    private static byte GetMantissaDigits(long value)
    {
        if (value == 0)
        {
            return 0;
        }
        var absValue = Math.Abs(value);
        if (absValue < 1)
        {
            return 0;
        }
        if (absValue < 10)
        {
            return 1;
        }
        return (byte)Math.Floor(Math.Log10(absValue) + 1);
    }

    private static byte GetMantissaDigits(ulong value)
    {
        if (value < 1)
        {
            return 0;
        }
        if (value < 10)
        {
            return 1;
        }
        return (byte)Math.Floor(Math.Log10(value) + 1);
    }

    private static void Reduce(ref long mantissa, ref short exponent, ref byte mantissaDigits)
    {
        while (mantissaDigits < MANTISSA_SIGNIFICANT_DIGITS
            && exponent > 0)
        {
            mantissa *= 10;
            exponent--;
            mantissaDigits++;
        }

        while (mantissaDigits < MANTISSA_SIGNIFICANT_DIGITS
            && exponent < 0
            && mantissa % 10 == 0)
        {
            mantissa /= 10;
            exponent++;
            mantissaDigits--;
        }

        while (mantissa > MAX_MANTISSA
            && exponent < MAX_EXPONENT)
        {
            mantissa /= 10;
            exponent++;
            mantissaDigits--;
        }

        while (exponent < 0 && mantissa % 10 == 0)
        {
            mantissa /= 10;
            exponent++;
        }

        if (mantissa == 0)
        {
            exponent = 0;
        }
    }

    private static long Reduce(ulong mantissa, ref short exponent, ref byte mantissaDigits)
    {
        while (mantissaDigits < MANTISSA_SIGNIFICANT_DIGITS
            && exponent > 0)
        {
            mantissa *= 10;
            exponent--;
            mantissaDigits++;
        }

        while (mantissaDigits < MANTISSA_SIGNIFICANT_DIGITS
            && exponent < 0
            && mantissa % 10 == 0)
        {
            mantissa /= 10;
            exponent++;
            mantissaDigits--;
        }

        while (mantissa > MAX_MANTISSA
            && exponent < MAX_EXPONENT)
        {
            mantissa /= 10;
            exponent++;
            mantissaDigits--;
        }

        if (mantissa == 0)
        {
            exponent = 0;
        }

        return (long)mantissa;
    }
}
