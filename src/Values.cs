namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    private static readonly HugeNumber[] _TangentTaylorSeries = new HugeNumber[]
    {
        Third,
        new HugeNumber(2) / new HugeNumber(15),
        new HugeNumber(17) / new HugeNumber(315),
        new HugeNumber(62) / new HugeNumber(2835),
    };
    private static readonly HugeNumber _Root3 = new HugeNumber(3).Sqrt();
    private static readonly HugeNumber _TwoMinusRoot3 = new HugeNumber(2) - _Root3;

    /// <summary>
    /// Represents 1e-18 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Atto { get; } = new(1, -18);

    /// <summary>
    /// Represents 1e-2 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Centi { get; } = new(1, -2);

    /// <summary>
    /// Represents 10 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Deca { get; } = new(10);

    /// <summary>
    /// Represents 1e-1 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Deci { get; } = new(1, -1);

    /// <summary>
    /// Represents 1e18 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Exa { get; } = new(100000000000000000, 1);

    /// <summary>
    /// Represents 1e-15 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Femto { get; } = new(1, -15);

    /// <summary>
    /// Represents 1e9 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Giga { get; } = new(1000000000);

    /// <summary>
    /// Respresents ½ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Half { get; } = new(5, -1);

    /// <summary>
    /// Represents 100 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Hecto { get; } = new(100);

    /// <summary>
    /// Represents 1e3 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Kilo { get; } = new(1000);

    /// <summary>
    /// Represents 1e6 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Mega { get; } = new(1000000);

    /// <summary>
    /// Represents 1e-6 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Micro { get; } = new(1, -6);

    /// <summary>
    /// Represents 1e-3 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Milli { get; } = new(1, -3);

    /// <summary>
    /// Represents 1e-9 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Nano { get; } = new(1, -9);

    /// <summary>
    /// Respresents negative one as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeOne { get; } = new(-1, 0);

    /// <summary>
    /// Respresents negative zero as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeZero { get; } = new(true, 0, -1);

    /// <summary>
    /// Respresents one as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber One { get; } = new(1, 0);
    /// <summary>
    /// <para>
    /// The value which, when multiplied by a <see cref="HugeNumber"/>, will return that <see cref="HugeNumber"/>.
    /// </para>
    /// <para>
    /// For <see cref="HugeNumber"/>, this is one.
    /// </para>
    /// </summary>
    public static HugeNumber MultiplicativeIdentity => One;

    /// <summary>
    /// Represents 1e15 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Peta { get; } = new(1000000000000000);

    /// <summary>
    /// Represents 1e-12 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Pico { get; } = new(1, -12);

    /// <summary>
    /// Represents 10 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Ten { get; } = new(10);

    /// <summary>
    /// Represents 1e12 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Tera { get; } = new(1000000000000);

    /// <summary>
    /// Represents 1e-24 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Yocto { get; } = new(1, -24);

    /// <summary>
    /// Represents 1e24 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Yotta { get; } = new(100000000000000000, 7);

    /// <summary>
    /// Respresents zero as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zero => new();
    /// <summary>
    /// <para>
    /// The value which, when added to a <see cref="HugeNumber"/>, will return that <see cref="HugeNumber"/>.
    /// </para>
    /// <para>
    /// For <see cref="HugeNumber"/>, this is zero.
    /// </para>
    /// </summary>
    public static HugeNumber AdditiveIdentity => Zero;

    /// <summary>
    /// Represents 1e-21 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zepto { get; } = new(1, -21);

    /// <summary>
    /// Represents 1e21 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zetta { get; } = new(100000000000000000, 4);

    /// <summary>
    /// Respresents the smallest positive <see cref="HugeNumber"/> value that is greater than
    /// zero.
    /// </summary>
    public static HugeNumber Epsilon { get; } = new(1, MIN_EXPONENT);

    /// <summary>
    /// Respresents the largest possible value of a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber MaxValue { get; } = new(MAX_MANTISSA, MAX_EXPONENT);

    /// <summary>
    /// Respresents the smallest possible value of a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber MinValue { get; } = new(MIN_MANTISSA, MAX_EXPONENT);

    /// <summary>
    /// Respresents a value that is not a number (NaN).
    /// </summary>
    public static HugeNumber NaN { get; } = new(true, long.MaxValue);

    /// <summary>
    /// A value which can be used to determine near-equivalence to zero,
    /// or to other <see cref="HugeNumber"/> values.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This is not the same as <see cref="Epsilon"/>, which is the smallest possible value that is distinguishable from zero.
    /// </para>
    /// <para>
    /// The <see cref="NearlyZero"/> value is instead used when making comparisons
    /// to determine close equivalence, and thereby avoid false negative comparison
    /// results due to floating-point errors.
    /// </para>
    /// </remarks>
    public static HugeNumber NearlyZero { get; } = new(1, -15);

    /// <summary>
    /// Respresents negative infinity as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeInfinity { get; } = new(true, NEGATIVE_INFINITE_MANTISSA);

    /// <summary>
    /// Respresents positive infinity as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber PositiveInfinity { get; } = new(true, POSITIVE_INFINITE_MANTISSA);

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e, rounded to 18
    /// places of precision, which is the limit of the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber E { get; } = new(271828182845904524, -17);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the
    /// constant, π, rounded to 18 places of precision, which is the limit of the <see
    /// cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Pi { get; } = new(314159265358979324, -17);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its radius, specified by the
    /// constant, τ, rounded to 18 places of precision, which is the limit of the <see
    /// cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Tau { get; } = new(628318530717958647, -17);

    /// <summary>
    /// The natural logarithm of 2, rounded to 18 places of precision, which is the limit of
    /// the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Ln2 { get; } = new(693147180559945309, -18);

    /// <summary>
    /// The natural logarithm of 10, rounded to 18 places of precision, which is the limit of
    /// the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Ln10 { get; } = new(230258509299404568, -17);

    /// <summary>
    /// Represents the golden ratio, specified by the constant, φ, rounded to 18 places of
    /// precision, which is the limit of the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Phi { get; } = new(161803398874989485, -17);

    /// <summary>
    /// √2 rounded to 18 places of precision, which is the limit of the <see cref="HugeNumber"/>
    /// structure.
    /// </summary>
    public static HugeNumber Root2 { get; } = new(141421356237309505, -17);

    /// <summary>
    /// Respresents ⅓ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Third { get; } = One / 3;

    /// <summary>
    /// 1/8π
    /// </summary>
    public static HugeNumber EighthPi { get; } = Pi / new HugeNumber(8);

    /// <summary>
    /// 4π
    /// </summary>
    public static HugeNumber FourPi { get; } = Tau * new HugeNumber(2);

    /// <summary>
    /// ½π
    /// </summary>
    public static HugeNumber HalfPi { get; } = Pi * Half;

    /// <summary>
    /// 1/e
    /// </summary>
    public static HugeNumber InverseE { get; } = One / E;

    /// <summary>
    /// 1/π
    /// </summary>
    public static HugeNumber InversePi { get; } = One / Pi;

    /// <summary>
    /// 180/π
    /// </summary>
    public static HugeNumber OneEightyOverPi { get; } = new HugeNumber(180) / Pi;

    /// <summary>
    /// π/180
    /// </summary>
    public static HugeNumber PiOver180 { get; } = Pi / new HugeNumber(180);

    /// <summary>
    /// π²
    /// </summary>
    public static HugeNumber PiSquared { get; } = Pi * Pi;

    /// <summary>
    /// ¼π
    /// </summary>
    public static HugeNumber QuarterPi { get; } = Pi / new HugeNumber(4);

    /// <summary>
    /// 1/6π
    /// </summary>
    public static HugeNumber SixthPi { get; } = Pi / new HugeNumber(6);

    /// <summary>
    /// 2π
    /// </summary>
    public static HugeNumber TwoPi => Tau;

    /// <summary>
    /// 3π
    /// </summary>
    public static HugeNumber ThreePi { get; } = Tau + Pi;

    /// <summary>
    /// π+⅓π
    /// </summary>
    public static HugeNumber FourThirdsPi { get; } = FourPi * Third;

    /// <summary>
    /// ⅓π
    /// </summary>
    public static HugeNumber ThirdPi { get; } = Pi * Third;

    /// <summary>
    /// 3/2π
    /// </summary>
    public static HugeNumber ThreeHalvesPi { get; } = ThreePi * Half;

    /// <summary>
    /// 3/4π
    /// </summary>
    public static HugeNumber ThreeQuartersPi { get; } = ThreePi / new HugeNumber(4);

    /// <summary>
    /// 2π²
    /// </summary>
    public static HugeNumber TwoPiSquared { get; } = new HugeNumber(2) * PiSquared;
}
