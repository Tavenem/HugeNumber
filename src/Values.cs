namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    private static readonly HugeNumber _Atto = new(1, -18);
    private static readonly HugeNumber _Centi = new(1, -2);
    private static readonly HugeNumber _Deca = new(10);
    private static readonly HugeNumber _Deci = new(1, -1);
    private static readonly HugeNumber _Exa = new(100000000000000000, 1);
    private static readonly HugeNumber _Femto = new(1, -15);
    private static readonly HugeNumber _Kilo = new(1000);
    private static readonly HugeNumber _Giga = new(1000000000);
    private static readonly HugeNumber _Half = new(5, -1);
    private static readonly HugeNumber _Hecto = new(100);
    private static readonly HugeNumber _Mega = new(1000000);
    private static readonly HugeNumber _Micro = new(1, -6);
    private static readonly HugeNumber _Milli = new(1, -3);
    private static readonly HugeNumber _Nano = new(1, -9);
    private static readonly HugeNumber _NegativeOne = new(-1, 0);
    private static readonly HugeNumber _One = new(1, 0);
    private static readonly HugeNumber _Peta = new(1000000000000000);
    private static readonly HugeNumber _Pico = new(1, -12);
    private static readonly HugeNumber _Ten = new(10);
    private static readonly HugeNumber _Tera = new(1000000000000);
    private static readonly HugeNumber _Yocto = new(1, -24);
    private static readonly HugeNumber _Yotta = new(100000000000000000, 7);
    private static readonly HugeNumber _Zepto = new(1, -21);
    private static readonly HugeNumber _Zetta = new(100000000000000000, 4);

    private static readonly HugeNumber _Epsilon = new(1, MIN_EXPONENT);
    private static readonly HugeNumber _MaxValue = new(MAX_MANTISSA, MAX_EXPONENT);
    private static readonly HugeNumber _MinValue = new(MIN_MANTISSA, MAX_EXPONENT);
    private static readonly HugeNumber _NaN = new(true, long.MaxValue);
    private static readonly HugeNumber _NearlyZero = new(1, -15);
    private static readonly HugeNumber _NegativeInfinity = new(true, NEGATIVE_INFINITE_MANTISSA);
    private static readonly HugeNumber _NegativeZero = new(true, 0, -1);
    private static readonly HugeNumber _PositiveInfinity = new(true, POSITIVE_INFINITE_MANTISSA);

    private static readonly HugeNumber _E = new(271828182845904524, -17);
    private static readonly HugeNumber _Pi = new(314159265358979324, -17);
    private static readonly HugeNumber _Tau = new(628318530717958647, -17);
    private static readonly HugeNumber _Ln2 = new(693147180559945309, -18);
    private static readonly HugeNumber _Ln10 = new(230258509299404568, -17);
    private static readonly HugeNumber _Phi = new(161803398874989485, -17);
    private static readonly HugeNumber _Root2 = new(141421356237309505, -17);

    private static readonly HugeNumber _Third = _One / 3;

    private static readonly HugeNumber _EighthPi = _Pi / new HugeNumber(8);
    private static readonly HugeNumber _FourPi = _Tau * new HugeNumber(2);
    private static readonly HugeNumber _HalfPi = _Pi * _Half;
    private static readonly HugeNumber _InverseE = _One / _E;
    private static readonly HugeNumber _InversePi = _One / _Pi;
    private static readonly HugeNumber _OneEightyOverPi = new HugeNumber(180) / _Pi;
    private static readonly HugeNumber _PiOver180 = _Pi / new HugeNumber(180);
    private static readonly HugeNumber _PiSquared = _Pi * _Pi;
    private static readonly HugeNumber _QuarterPi = _Pi / new HugeNumber(4);
    private static readonly HugeNumber _SixthPi = _Pi / new HugeNumber(6);
    private static readonly HugeNumber _ThreePi = _Tau + _Pi;

    private static readonly HugeNumber _FourThirdsPi = _FourPi * _Third;
    private static readonly HugeNumber _ThirdPi = _Pi * _Third;
    private static readonly HugeNumber _ThreeHalvesPi = _ThreePi * _Half;
    private static readonly HugeNumber _ThreeQuartersPi = _ThreePi / new HugeNumber(4);
    private static readonly HugeNumber _TwoPiSquared = new HugeNumber(2) * _PiSquared;

    private static readonly HugeNumber[] _TangentTaylorSeries = new HugeNumber[]
    {
        _Third,
        new HugeNumber(2) / new HugeNumber(15),
        new HugeNumber(17) / new HugeNumber(315),
        new HugeNumber(62) / new HugeNumber(2835),
    };
    private static readonly HugeNumber _Root3 = new HugeNumber(3).Sqrt();
    private static readonly HugeNumber _TwoMinusRoot3 = new HugeNumber(2) - _Root3;

    /// <summary>
    /// Represents 1e-18 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Atto => _Atto;

    /// <summary>
    /// Represents 1e-2 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Centi => _Centi;

    /// <summary>
    /// Represents 10 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Deca => _Deca;

    /// <summary>
    /// Represents 1e-1 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Deci => _Deci;

    /// <summary>
    /// Respresents the smallest positive <see cref="HugeNumber"/> value that is greater than
    /// zero.
    /// </summary>
    public static HugeNumber Epsilon => _Epsilon;

    /// <summary>
    /// Represents 1e18 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Exa => _Exa;

    /// <summary>
    /// Represents 1e-15 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Femto => _Femto;

    /// <summary>
    /// Represents 1e9 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Giga => _Giga;

    /// <summary>
    /// Respresents ½ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Half => _Half;

    /// <summary>
    /// Represents 100 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Hecto => _Hecto;

    /// <summary>
    /// Represents 1e3 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Kilo => _Kilo;

    /// <summary>
    /// Respresents the largest possible value of a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber MaxValue => _MaxValue;

    /// <summary>
    /// Represents 1e6 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Mega => _Mega;

    /// <summary>
    /// Represents 1e-6 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Micro => _Micro;

    /// <summary>
    /// Represents 1e-3 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Milli => _Milli;

    /// <summary>
    /// Respresents the smallest possible value of a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber MinValue => _MinValue;

    /// <summary>
    /// Respresents a value that is not a number (NaN).
    /// </summary>
    public static HugeNumber NaN => _NaN;

    /// <summary>
    /// Represents 1e-9 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Nano => _Nano;

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
    public static HugeNumber NearlyZero => _NearlyZero;

    /// <summary>
    /// Respresents negative infinity as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeInfinity => _NegativeInfinity;

    /// <summary>
    /// Respresents negative one as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeOne => _NegativeOne;

    /// <summary>
    /// Respresents negative zero as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeZero => _NegativeZero;

    /// <summary>
    /// Respresents one as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber One => _One;
    /// <summary>
    /// <para>
    /// The value which, when multiplied by a <see cref="HugeNumber"/>, will return that <see cref="HugeNumber"/>.
    /// </para>
    /// <para>
    /// For <see cref="HugeNumber"/>, this is one.
    /// </para>
    /// </summary>
    public static HugeNumber MultiplicativeIdentity => _One;

    /// <summary>
    /// Represents 1e15 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Peta => _Peta;

    /// <summary>
    /// Represents 1e-12 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Pico => _Pico;

    /// <summary>
    /// Respresents positive infinity as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber PositiveInfinity => _PositiveInfinity;

    /// <summary>
    /// Represents 10 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Ten => _Ten;

    /// <summary>
    /// Represents 1e12 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Tera => _Tera;

    /// <summary>
    /// Respresents ⅓ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Third => _Third;

    /// <summary>
    /// Represents 1e-24 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Yocto => _Yocto;

    /// <summary>
    /// Represents 1e24 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Yotta => _Yotta;

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
    public static HugeNumber Zepto => _Zepto;

    /// <summary>
    /// Represents 1e21 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zetta => _Zetta;

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant, e, rounded to 18
    /// places of precision, which is the limit of the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber E => _E;

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the
    /// constant, π, rounded to 18 places of precision, which is the limit of the <see
    /// cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Pi => _Pi;

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its radius, specified by the
    /// constant, τ, rounded to 18 places of precision, which is the limit of the <see
    /// cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Tau => _Tau;

    /// <summary>
    /// 1/8π
    /// </summary>
    public static HugeNumber EighthPi => _EighthPi;

    /// <summary>
    /// 1/e
    /// </summary>
    public static HugeNumber InverseE => _InverseE;

    /// <summary>
    /// 4π
    /// </summary>
    public static HugeNumber FourPi => _FourPi;

    /// <summary>
    /// π+⅓π
    /// </summary>
    public static HugeNumber FourThirdsPi => _FourThirdsPi;

    /// <summary>
    /// ½π
    /// </summary>
    public static HugeNumber HalfPi => _HalfPi;

    /// <summary>
    /// 1/π
    /// </summary>
    public static HugeNumber InversePi => _InversePi;

    /// <summary>
    /// The natural logarithm of 2, rounded to 18 places of precision, which is the limit of
    /// the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Ln2 => _Ln2;

    /// <summary>
    /// The natural logarithm of 10, rounded to 18 places of precision, which is the limit of
    /// the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Ln10 => _Ln10;

    /// <summary>
    /// 180/π
    /// </summary>
    public static HugeNumber OneEightyOverPi => _OneEightyOverPi;

    /// <summary>
    /// Represents the golden ratio, specified by the constant, φ, rounded to 18 places of
    /// precision, which is the limit of the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Phi => _Phi;

    /// <summary>
    /// π/180
    /// </summary>
    public static HugeNumber PiOver180 => _PiOver180;

    /// <summary>
    /// π²
    /// </summary>
    public static HugeNumber PiSquared => _PiSquared;

    /// <summary>
    /// √2 rounded to 18 places of precision, which is the limit of the <see cref="HugeNumber"/>
    /// structure.
    /// </summary>
    public static HugeNumber Root2 => _Root2;

    /// <summary>
    /// ¼π
    /// </summary>
    public static HugeNumber QuarterPi => _QuarterPi;

    /// <summary>
    /// 1/6π
    /// </summary>
    public static HugeNumber SixthPi => _SixthPi;

    /// <summary>
    /// ⅓π
    /// </summary>
    public static HugeNumber ThirdPi => _ThirdPi;

    /// <summary>
    /// 3π
    /// </summary>
    public static HugeNumber ThreePi => _ThreePi;

    /// <summary>
    /// 3/2π
    /// </summary>
    public static HugeNumber ThreeHalvesPi => _ThreeHalvesPi;

    /// <summary>
    /// 3/4π
    /// </summary>
    public static HugeNumber ThreeQuartersPi => _ThreeQuartersPi;

    /// <summary>
    /// 2π
    /// </summary>
    public static HugeNumber TwoPi => Tau;

    /// <summary>
    /// 2π²
    /// </summary>
    public static HugeNumber TwoPiSquared => _TwoPiSquared;
}
