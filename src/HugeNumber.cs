using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Tavenem.HugeNumber
{
    /// <summary>
    /// <para>
    /// Allows efficient recording of values in the range ±999999999999999999e±32767. Also allows
    /// representing positive or negative infinity, and NaN (not-a-number).
    /// </para>
    /// </summary>
    /// <remarks>
    /// <para>
    /// The extreme potential range trades off with precision: values have at most 18 significant
    /// digits in the mantissa, and 5 in the exponent. These limits are fixed independently of one
    /// another; they do not trade off, as with the standard floating-point types. I.e. you cannot
    /// have only one significent digit in the mantissa and thereby gain 22 in the exponent.
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
    public partial struct HugeNumber : ISerializable
    {
        private const byte MANTISSA_SIGNIFICANT_DIGITS = 18;
        private const short MAX_EXPONENT = short.MaxValue;
        private const short MIN_EXPONENT = short.MinValue;
        private const long MAX_MANTISSA = 999999999999999999;
        private const long MIN_MANTISSA = -999999999999999999;

        /// <summary>
        /// Represents 1e-18 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Atto = new(1, -18);

        /// <summary>
        /// Represents 1e-2 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Centi = new(1, -2);

        /// <summary>
        /// Represents 10 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Deca = new(10);

        /// <summary>
        /// Represents 1e-1 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Deci = new(1, -1);

        /// <summary>
        /// Respresents the smallest positive <see cref="HugeNumber"/> value that is greater than
        /// zero.
        /// </summary>
        public static readonly HugeNumber Epsilon = new(1, MIN_EXPONENT);

        /// <summary>
        /// Represents 1e18 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Exa = new(100000000000000000, 1);

        /// <summary>
        /// Represents 1e-15 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Femto = new(1, -15);

        /// <summary>
        /// Represents 1e9 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Giga = new(1000000000);

        /// <summary>
        /// Respresents ½ as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Half = new(5, -1);

        /// <summary>
        /// Represents 100 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Hecto = new(100);

        /// <summary>
        /// Represents 1e3 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Kilo = new(1000);

        /// <summary>
        /// Respresents the largest possible value of a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber MaxValue = new(MAX_MANTISSA, MAX_EXPONENT);

        /// <summary>
        /// Represents 1e6 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Mega = new(1000000);

        /// <summary>
        /// Represents 1e-6 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Micro = new(1, -6);

        /// <summary>
        /// Represents 1e-3 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Milli = new(1, -3);

        /// <summary>
        /// Respresents the smallest possible value of a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber MinValue = new(MIN_MANTISSA, MAX_EXPONENT);

        /// <summary>
        /// Respresents a value that is not a number (NaN).
        /// </summary>
        public static readonly HugeNumber NaN = new(false, false, true);

        /// <summary>
        /// Represents 1e-9 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Nano = new(1, -9);

        /// <summary>
        /// Respresents negative infinity as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber NegativeInfinity = new(false, true, false);

        /// <summary>
        /// Respresents negative one as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber NegativeOne = new(-1, 0);

        /// <summary>
        /// Respresents one as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber One = new(1, 0);

        /// <summary>
        /// Represents 1e15 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Peta = new(1000000000000000);

        /// <summary>
        /// Represents 1e-12 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Pico = new(1, -12);

        /// <summary>
        /// Respresents positive infinity as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber PositiveInfinity = new(true, false, false);

        /// <summary>
        /// Represents 10 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Ten = new(10);

        /// <summary>
        /// Represents 1e12 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Tera = new(1000000000000);

        /// <summary>
        /// Respresents ⅓ as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Third = One / 3;

        /// <summary>
        /// Represents 1e-24 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Yocto = new(1, -24);

        /// <summary>
        /// Represents 1e24 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Yotta = new(100000000000000000, 7);

        /// <summary>
        /// Respresents zero as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Zero;

        /// <summary>
        /// Represents 1e-21 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Zepto = new(1, -21);

        /// <summary>
        /// Represents 1e21 as a <see cref="HugeNumber"/>.
        /// </summary>
        public static readonly HugeNumber Zetta = new(100000000000000000, 4);

        /// <summary>
        /// The power of ten by which the <see cref="Mantissa"/> is multiplied to determine the
        /// value of this <see cref="HugeNumber"/>.
        /// </summary>
        [DataMember(Order = 1)]
        public short Exponent { get; }

        /// <summary>
        /// Indicates whether this instance represents a finite value (neither positive or negative
        /// infinity, or NaN).
        /// </summary>
        public bool IsFinite => !IsNaN && !IsInfinite;

        /// <summary>
        /// Indicates whether this instance represents an infinite value (positive or negative).
        /// </summary>
        public bool IsInfinite => IsPositiveInfinity || IsNegativeInfinity;

        /// <summary>
        /// Indicates whether this instance does not represent a number.
        /// </summary>
        [DataMember(Order = 2)]
        public bool IsNaN { get; }

        /// <summary>
        /// Indicates whether this instance is less than zero.
        /// </summary>
        /// <remarks>
        /// Not <see langword="true"/> for <see cref="NaN"/>.
        /// </remarks>
        public bool IsNegative => IsNegativeInfinity || Mantissa < 0;

        /// <summary>
        /// Indicates whether this instance represents negative infinity.
        /// </summary>
        [DataMember(Order = 3)]
        public bool IsNegativeInfinity { get; }

        /// <summary>
        /// Indicates whether this instance is greater than or equal to zero.
        /// </summary>
        /// <remarks>
        /// Not <see langword="true"/> for <see cref="NaN"/>.
        /// </remarks>
        public bool IsPositive => IsPositiveInfinity || Mantissa >= 0;

        /// <summary>
        /// Indicates whether this instance represents positive infinity.
        /// </summary>
        [DataMember(Order = 4)]
        public bool IsPositiveInfinity { get; }

        /// <summary>
        /// Indicates whether this instance is equal to zero.
        /// </summary>
        public bool IsZero => Mantissa == 0;

        /// <summary>
        /// The value which is multiplied by ten times the <see cref="Exponent"/> to determine the
        /// value of this <see cref="HugeNumber"/>.
        /// </summary>
        [DataMember(Order = 5)]
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
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsNaN = false;
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
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(long mantissa, short exponent)
        {
            var mantissaDigits = GetMantissaDigits(mantissa);
            Reduce(ref mantissa, ref exponent, ref mantissaDigits);
            Mantissa = mantissa;
            MantissaDigits = mantissaDigits;
            Exponent = mantissa == 0 ? (short)0 : exponent;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(long mantissa, int exponent)
        {
            if (exponent > MAX_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = true;
                IsNegativeInfinity = false;
            }
            else if (exponent < MIN_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = true;
            }
            else
            {
                var sExponent = (short)exponent;
                var mantissaDigits = GetMantissaDigits(mantissa);
                Reduce(ref mantissa, ref sExponent, ref mantissaDigits);
                Mantissa = mantissa;
                MantissaDigits = mantissaDigits;
                Exponent = mantissa == 0 ? (short)0 : sExponent;
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
            }
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(ulong mantissa, short exponent)
        {
            var mantissaDigits = GetMantissaDigits(mantissa);
            Mantissa = Reduce(mantissa, ref exponent, ref mantissaDigits);
            MantissaDigits = mantissaDigits;
            Exponent = mantissa == 0 ? (short)0 : exponent;
            IsPositiveInfinity = false;
            IsNegativeInfinity = false;
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(ulong mantissa, int exponent)
        {
            if (exponent > MAX_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = true;
                IsNegativeInfinity = false;
            }
            else if (exponent < MIN_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = true;
            }
            else
            {
                var sExponent = (short)exponent;
                var mantissaDigits = GetMantissaDigits(mantissa);
                Mantissa = Reduce(mantissa, ref sExponent, ref mantissaDigits);
                MantissaDigits = mantissaDigits;
                Exponent = mantissa == 0 ? (short)0 : sExponent;
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
            }
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(decimal mantissa, short exponent)
        {
            var val = (HugeNumber)mantissa;
            if (MAX_EXPONENT - exponent < val.Exponent)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = val >= 0;
                IsNegativeInfinity = !IsPositiveInfinity;
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
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
            }
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(decimal mantissa, int exponent)
        {
            if (exponent > MAX_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = true;
                IsNegativeInfinity = false;
                IsNaN = false;
            }
            else if (exponent < MIN_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = true;
                IsNaN = false;
            }
            else
            {
                var sExponent = (short)exponent;
                var val = (HugeNumber)mantissa;
                if (MAX_EXPONENT - sExponent < val.Exponent)
                {
                    Mantissa = 0;
                    MantissaDigits = 0;
                    Exponent = 0;
                    IsPositiveInfinity = val >= 0;
                    IsNegativeInfinity = !IsPositiveInfinity;
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
                    IsPositiveInfinity = false;
                    IsNegativeInfinity = false;
                }
            }
            IsNaN = false;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(double mantissa, short exponent)
        {
            if (double.IsNaN(mantissa))
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
                IsNaN = true;
            }
            else if (double.IsPositiveInfinity(mantissa))
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = true;
                IsNegativeInfinity = false;
                IsNaN = false;
            }
            else if (double.IsNegativeInfinity(mantissa))
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = true;
                IsNaN = false;
            }
            else
            {
                var val = (HugeNumber)mantissa;
                if (MAX_EXPONENT - exponent < val.Exponent)
                {
                    Mantissa = 0;
                    MantissaDigits = 0;
                    Exponent = 0;
                    IsPositiveInfinity = val >= 0;
                    IsNegativeInfinity = !IsPositiveInfinity;
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
                    IsPositiveInfinity = false;
                    IsNegativeInfinity = false;
                }
                IsNaN = false;
            }
        }

        /// <summary>
        /// Initializes a new instance of <see cref="HugeNumber"/> with the given <paramref
        /// name="mantissa"/> and <paramref name="exponent"/>.
        /// </summary>
        /// <param name="mantissa">The mantissa.</param>
        /// <param name="exponent"></param>
        public HugeNumber(double mantissa, int exponent)
        {
            if (double.IsNaN(mantissa))
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = false;
                IsNaN = true;
            }
            else if (double.IsPositiveInfinity(mantissa)
                || exponent > MAX_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = true;
                IsNegativeInfinity = false;
                IsNaN = false;
            }
            else if (double.IsNegativeInfinity(mantissa)
                || exponent < MIN_EXPONENT)
            {
                Mantissa = 0;
                MantissaDigits = 0;
                Exponent = 0;
                IsPositiveInfinity = false;
                IsNegativeInfinity = true;
                IsNaN = false;
            }
            else
            {
                var sExponent = (short)exponent;
                var val = (HugeNumber)mantissa;
                if (MAX_EXPONENT - sExponent < val.Exponent)
                {
                    Mantissa = 0;
                    MantissaDigits = 0;
                    Exponent = 0;
                    IsPositiveInfinity = val >= 0;
                    IsNegativeInfinity = !IsPositiveInfinity;
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
                    IsPositiveInfinity = false;
                    IsNegativeInfinity = false;
                }
                IsNaN = false;
            }
        }

        private HugeNumber(
            bool isPositiveInfinity,
            bool isNegativeInfinity,
            bool isNaN,
            long mantissa = 0,
            short exponent = 0)
        {
            Mantissa = mantissa;
            MantissaDigits = GetMantissaDigits(mantissa);
            Exponent = exponent;
            IsPositiveInfinity = isPositiveInfinity;
            IsNegativeInfinity = isNegativeInfinity;
            IsNaN = isNaN;
        }

        private HugeNumber(SerializationInfo info, StreamingContext context) : this(
            (bool?)info.GetValue(nameof(IsPositiveInfinity), typeof(bool)) ?? default,
            (bool?)info.GetValue(nameof(IsNegativeInfinity), typeof(bool)) ?? default,
            (bool?)info.GetValue(nameof(IsNaN), typeof(bool)) ?? default,
            (long?)info.GetValue(nameof(Mantissa), typeof(long)) ?? default,
            (short?)info.GetValue(nameof(Exponent), typeof(short)) ?? default)
        { }

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
            info.AddValue(nameof(IsNaN), IsNaN);
            info.AddValue(nameof(IsNegativeInfinity), IsNegativeInfinity);
            info.AddValue(nameof(IsPositiveInfinity), IsPositiveInfinity);
            info.AddValue(nameof(Mantissa), Mantissa);
        }
    }
}
