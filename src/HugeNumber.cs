using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Tavenem.HugeNumbers;

/// <summary>
/// <para>
/// Allows efficient recording of values in the range ±999999999999999999e±32767. Also allows
/// representing positive or negative infinity, and NaN (not-a-number).
/// </para>
/// </summary>
/// <remarks>
/// <para>
/// Values have at most 18 significant digits in the mantissa, and 5 in the exponent. These limits
/// are fixed independently of one another; they do not trade off, as with the standard
/// floating-point types. I.e. you cannot have only one significent digit in the mantissa and
/// thereby gain 22 in the exponent.
/// </para>
/// <para>
/// Despite the ability to record floating-point values, HugeNumber values are internally stored
/// as integral pairs of mantissa and exponent. Therefore, arithmatic operations between
/// HugeNumber values which represent integers are not subject to floating point errors. For
/// example, <c>new HugeNumber(5) * 2 / 2 == 5</c> is always <see langword="true"/>. This also
/// applies to rational floating point values: <code>new HugeNumber(10) / 4 == new Number (100) /
/// 40</code> is also guaranteed to be <see langword="true"/>.
/// </para>
/// <para>
/// Note that imprecision is still possible when performing arithmatic operations or comparisons
/// between <i>irrational</i> floating point values, or when converting to and from non-Number
/// floating point types such as <see cref="float"/> and <see cref="double"/>. For example,
/// <c>new HugeNumber(1) / new HugeNumber(3) == 1.0 / 3.0</c> is <i>not</i> guaranteed to be <see
/// langword="true"/>, nor is <code>new HugeNumber(2) / new HugeNumber (6) / 2 == new HugeNumber(1)
/// / new HugeNumber(3)</code>. These <i>may</i> evaluate to <see langword="true"/>, but this is
/// not guaranteed. The usual caveats and safeguards typically employed when performing floating
/// point math and/or comparisons should be applied to HugeNumber instances which represent
/// irrational values, or have been converted from a non-HugeNumber floating point value.
/// </para>
/// </remarks>
[Serializable]
[DataContract]
[JsonConverter(typeof(HugeNumberConverter))]
readonly public partial struct HugeNumber :
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
    IComparableToZero<HugeNumber>,
    IComparisonOperators<HugeNumber, decimal>,
    IComparisonOperators<HugeNumber, double>,
    IComparisonOperators<HugeNumber, long>,
    IComparisonOperators<HugeNumber, ulong>,
    IConvertible,
    IDivisionOperators<HugeNumber, decimal, HugeNumber>,
    IDivisionOperators<HugeNumber, double, HugeNumber>,
    IDivisionOperators<HugeNumber, long, HugeNumber>,
    IDivisionOperators<HugeNumber, ulong, HugeNumber>,
    IEqualityOperators<HugeNumber, decimal>,
    IEqualityOperators<HugeNumber, double>,
    IEqualityOperators<HugeNumber, long>,
    IEqualityOperators<HugeNumber, ulong>,
    IEquatable<double>,
    IEquatable<float>,
    IEquatable<int>,
    IEquatable<long>,
    IEquatable<ulong>,
    IMinMaxValue<HugeNumber>,
    IModulusOperators<HugeNumber, decimal, HugeNumber>,
    IModulusOperators<HugeNumber, double, HugeNumber>,
    IModulusOperators<HugeNumber, long, HugeNumber>,
    IModulusOperators<HugeNumber, ulong, HugeNumber>,
    IMultiplyOperators<HugeNumber, decimal, HugeNumber>,
    IMultiplyOperators<HugeNumber, double, HugeNumber>,
    IMultiplyOperators<HugeNumber, long, HugeNumber>,
    IMultiplyOperators<HugeNumber, ulong, HugeNumber>,
    ISerializable,
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
    private const long POSITIVE_INFINITE_MANTISSA = 1000000000000000000;
    private const long NEGATIVE_INFINITE_MANTISSA = -1000000000000000000;

    /// <summary>
    /// The power of ten by which the <see cref="Mantissa"/> is multiplied to determine the
    /// value of this <see cref="HugeNumber"/>.
    /// </summary>
    [DataMember(Order = 1)]
    public short Exponent { get; }

    /// <summary>
    /// The value which is multiplied by ten times the <see cref="Exponent"/> to determine the
    /// value of this <see cref="HugeNumber"/>.
    /// </summary>
    [DataMember(Order = 2)]
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
            Mantissa = POSITIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (exponent < MIN_EXPONENT)
        {
            Mantissa = NEGATIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var mantissaDigits = GetMantissaDigits(mantissa);
            Reduce(ref mantissa, ref sExponent, ref mantissaDigits);
            Mantissa = mantissa;
            MantissaDigits = mantissaDigits;
            Exponent = sExponent;
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
            Mantissa = POSITIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (exponent < MIN_EXPONENT)
        {
            Mantissa = NEGATIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var mantissaDigits = GetMantissaDigits(mantissa);
            Mantissa = Reduce(mantissa, ref sExponent, ref mantissaDigits);
            MantissaDigits = mantissaDigits;
            Exponent = sExponent;
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
            Mantissa = val >= 0
                ? POSITIVE_INFINITE_MANTISSA
                : NEGATIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
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
            Mantissa = POSITIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (exponent < MIN_EXPONENT)
        {
            Mantissa = NEGATIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - sExponent < val.Exponent)
            {
                Mantissa = val >= 0
                    ? POSITIVE_INFINITE_MANTISSA
                    : NEGATIVE_INFINITE_MANTISSA;
                MantissaDigits = 0;
                Exponent = 0;
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
            Mantissa = long.MaxValue;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (double.IsPositiveInfinity(mantissa))
        {
            Mantissa = POSITIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (double.IsNegativeInfinity(mantissa))
        {
            Mantissa = NEGATIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else
        {
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - exponent < val.Exponent)
            {
                Mantissa = val >= 0
                    ? POSITIVE_INFINITE_MANTISSA
                    : NEGATIVE_INFINITE_MANTISSA;
                MantissaDigits = 0;
                Exponent = 0;
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
            Mantissa = long.MaxValue;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (double.IsPositiveInfinity(mantissa)
            || exponent > MAX_EXPONENT)
        {
            Mantissa = POSITIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else if (double.IsNegativeInfinity(mantissa)
            || exponent < MIN_EXPONENT)
        {
            Mantissa = NEGATIVE_INFINITE_MANTISSA;
            MantissaDigits = 0;
            Exponent = 0;
        }
        else
        {
            var sExponent = (short)exponent;
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - sExponent < val.Exponent)
            {
                Mantissa = val >= 0
                    ? POSITIVE_INFINITE_MANTISSA
                    : NEGATIVE_INFINITE_MANTISSA;
                MantissaDigits = 0;
                Exponent = 0;
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
            }
        }
    }

    private HugeNumber(bool _, long mantissa = 0, short exponent = 0)
    {
        Mantissa = mantissa;
        MantissaDigits = GetMantissaDigits(mantissa);
        Exponent = exponent;
    }

    private HugeNumber(SerializationInfo info, StreamingContext context) : this(
        true,
        (long?)info.GetValue(nameof(Mantissa), typeof(long)) ?? default,
        (short?)info.GetValue(nameof(Exponent), typeof(short)) ?? default)
    { }

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

    /// <summary>Populates a <see cref="SerializationInfo"></see> with the data needed to
    /// serialize the target object.</summary>
    /// <param name="info">The <see cref="SerializationInfo"></see> to populate with
    /// data.</param>
    /// <param name="context">The destination (see <see cref="StreamingContext"></see>) for this
    /// serialization.</param>
    /// <exception cref="System.Security.SecurityException">The caller does not have the
    /// required permission.</exception>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        info.AddValue(nameof(Exponent), Exponent);
        info.AddValue(nameof(Mantissa), Mantissa);
    }
}
