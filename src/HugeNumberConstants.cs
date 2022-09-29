using Tavenem.HugeNumbers;

namespace Tavenem.Mathematics;

/// <summary>
/// <para>
/// A collection of scientific constants, as well as the results of some precalculated
/// operations involving those constants.
/// </para>
/// <para>
/// The constants in this class are <see cref="HugeNumber"/> values. The <see
/// cref="DoubleConstants"/> class contains the same constants as <see cref="double"/> values,
/// and the <see cref="DecimalConstants"/> class contains the same constants as <see
/// cref="decimal"/> values.
/// </para>
/// </summary>
/// <remarks>
/// <para>
/// Some of the constants available here are provided as <see cref="int"/> or <see cref="long"/>
/// values, where their value is a whole number, and small enough to fit in those data types.
/// </para>
/// <para>
/// The mathematical constants contained in the <see cref="DoubleConstants"/> and <see
/// cref="DecimalConstants"/> classes are available as <see langword="static"/> properties of
/// the <see cref="HugeNumber"/> class itself.
/// </para>
/// </remarks>
public static class HugeNumberConstants
{
    #region Numbers

    /// <summary>
    /// Represents 1e-18 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Atto { get; } = new(1, -18);

    /// <summary>
    /// Represents 1 / 100 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Centi { get; } = new(1, 100, 0);

    /// <summary>
    /// Represents 10 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Deca { get; } = new(10);

    /// <summary>
    /// Represents 1 / 10 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Deci { get; } = new(1, 10, 0);

    /// <summary>
    /// Respresents 1/8 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Eighth { get; } = new(1, 8, 0);

    /// <summary>
    /// Represents 1e18 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Exa { get; } = new(100000000000000000, 0);

    /// <summary>
    /// Represents 1e-15 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Femto { get; } = new(1, -15);

    /// <summary>
    /// Respresents ¼ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Fourth { get; } = new(1, 4, 0);

    /// <summary>
    /// Represents 1000000000 (1e9) as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Giga { get; } = new(1000000000);

    /// <summary>
    /// Respresents ½ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Half { get; } = new(1, 2, 0);

    /// <summary>
    /// Represents 100 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Hecto { get; } = new(100);

    /// <summary>
    /// Represents 1000 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Kilo { get; } = new(1000);

    /// <summary>
    /// Represents 1000000 (1e6) as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Mega { get; } = new(1000000);

    /// <summary>
    /// Represents 1e-6 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Micro { get; } = new(1, -6);

    /// <summary>
    /// Represents 1 / 1000 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Milli { get; } = new(1, 1000, 0);

    /// <summary>
    /// Represents 1e-9 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Nano { get; } = new(1, -9);

    /// <summary>
    /// Represents 1000000000000000 (1e15) as a <see cref="HugeNumber"/>.
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
    /// Represents 1000000000000 (1e12) as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Tera { get; } = new(1000000000000);

    /// <summary>
    /// Respresents ⅓ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Third { get; } = new(1, 3, 0);

    /// <summary>
    /// Respresents ¾ as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber ThreeFourths { get; } = new(3, 4, 0);

    /// <summary>
    /// 1+½
    /// </summary>
    public static HugeNumber ThreeHalves { get; } = Third * Two;

    /// <summary>
    /// Respresents 2 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Two { get; } = new(2);

    /// <summary>
    /// Represents 1e-24 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Yocto { get; } = new(1, -24);

    /// <summary>
    /// Represents 1e24 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Yotta { get; } = new(100000000000000000, 7);

    /// <summary>
    /// Represents 1e-21 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zepto { get; } = new(1, -21);

    /// <summary>
    /// Represents 1e21 as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zetta { get; } = new(100000000000000000, 4);

    #endregion Numbers

    #region Math

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
    /// 1/8π
    /// </summary>
    public static HugeNumber EighthPi { get; } = HugeNumber.Pi / new HugeNumber(8);

    /// <summary>
    /// 4π
    /// </summary>
    public static HugeNumber FourPi { get; } = HugeNumber.Tau * new HugeNumber(2);

    /// <summary>
    /// ½π
    /// </summary>
    public static HugeNumber HalfPi { get; } = HugeNumber.Pi * Half;

    /// <summary>
    /// 1/e
    /// </summary>
    public static HugeNumber InverseE { get; } = HugeNumber.One / HugeNumber.E;

    /// <summary>
    /// 1/π
    /// </summary>
    public static HugeNumber InversePi { get; } = HugeNumber.One / HugeNumber.Pi;

    /// <summary>
    /// 180/π
    /// </summary>
    public static HugeNumber OneEightyOverPi { get; } = new HugeNumber(180) / HugeNumber.Pi;

    /// <summary>
    /// π/180
    /// </summary>
    public static HugeNumber PiOver180 { get; } = HugeNumber.Pi / new HugeNumber(180);

    /// <summary>
    /// π²
    /// </summary>
    public static HugeNumber PiSquared { get; } = HugeNumber.Pi * HugeNumber.Pi;

    /// <summary>
    /// ¼π
    /// </summary>
    public static HugeNumber QuarterPi { get; } = HugeNumber.Pi / new HugeNumber(4);

    /// <summary>
    /// 1/6π
    /// </summary>
    public static HugeNumber SixthPi { get; } = HugeNumber.Pi / new HugeNumber(6);

    /// <summary>
    /// 2π
    /// </summary>
    public static HugeNumber TwoPi => HugeNumber.Tau;

    /// <summary>
    /// 3π
    /// </summary>
    public static HugeNumber ThreePi { get; } = HugeNumber.Tau + HugeNumber.Pi;

    /// <summary>
    /// π+⅓π
    /// </summary>
    public static HugeNumber FourThirdsPi { get; } = FourPi * Third;

    /// <summary>
    /// ⅓π
    /// </summary>
    public static HugeNumber ThirdPi { get; } = HugeNumber.Pi * Third;

    /// <summary>
    /// π+½π
    /// </summary>
    public static HugeNumber ThreeHalvesPi { get; } = ThreePi * Half;

    /// <summary>
    /// ¾π
    /// </summary>
    public static HugeNumber ThreeQuartersPi { get; } = ThreePi / new HugeNumber(4);

    /// <summary>
    /// 2π²
    /// </summary>
    public static HugeNumber TwoPiSquared { get; } = new HugeNumber(2) * PiSquared;

    #endregion Math

    #region Science

    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public static readonly HugeNumber AvogadroConstant = new(602214076, 15);
    /// <summary>
    /// The Avogadro constant (<i>N</i><sub>A</sub>, <i>L</i>), in SI base units.
    /// </summary>
    public static readonly HugeNumber L = AvogadroConstant;

    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public static readonly HugeNumber BoltzmannConstant = new(1380649, -29);
    /// <summary>
    /// The Boltzmann constant (<i>k</i><sub>B</sub>, <i>k</i>), in SI base units.
    /// </summary>
    public static readonly HugeNumber k = BoltzmannConstant;

    /// <summary>
    /// The mass of a electron, in kg.
    /// </summary>
    public static readonly HugeNumber ElectronMass = new(910938356, -39);

    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public static readonly HugeNumber ElementaryCharge = new(1602176634, -28);
    /// <summary>
    /// The elementary charge (<i>e</i>, <i>q</i><sub>e</sub>), in coulombs.
    /// </summary>
    public static readonly HugeNumber qe = ElementaryCharge;

    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber GravitationalConstant = new(667408, -16);
    /// <summary>
    /// The gravitational constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber G = GravitationalConstant;
    /// <summary>
    /// Twice the gravitational constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber TwoG = 2 * G;

    /// <summary>
    /// The heat of vaporization of water, in SI base units.
    /// </summary>
    public const int HeatOfVaporizationOfWater = 2501000;
    /// <summary>
    /// The heat of vaporization of water, in SI base units.
    /// </summary>
    public const int DeltaHvapWater = HeatOfVaporizationOfWater;

    /// <summary>
    /// The heat of vaporization of water, squared, in SI base units.
    /// </summary>
    public static readonly HugeNumber DeltaHvapWaterSquared = ((HugeNumber)DeltaHvapWater).Square();

    /// <summary>
    /// The distance light travels in a Julian year, in m.
    /// </summary>
    public const long LightYear = 9460730472580800;
    /// <summary>
    /// The distance light travels in a Julian year, in m.
    /// </summary>
    public const long ly = LightYear;

    /// <summary>
    /// The molar mass of air, in SI base units.
    /// </summary>
    public static readonly HugeNumber MolarMassOfAir = new(289644, -7);
    /// <summary>
    /// The molar mass of air, in SI base units.
    /// </summary>
    public static readonly HugeNumber MAir = MolarMassOfAir;

    /// <summary>
    /// The mass of a neutron, in kg.
    /// </summary>
    public static readonly HugeNumber NeutronMass = new(1674927471, -36);

    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public static readonly HugeNumber PlanckConstant = new(662607015, -42);
    /// <summary>
    /// The Planck constant (<i>h</i>) in SI base units.
    /// </summary>
    public static readonly HugeNumber h = PlanckConstant;

    /// <summary>
    /// The mass of a proton, in kg.
    /// </summary>
    public static readonly HugeNumber ProtonMass = new(1672621898, -36);

    /// <summary>
    /// The specific gas constant of dry air, in SI base units.
    /// </summary>
    public const int SpecificGasConstantOfDryAir = 287;
    /// <summary>
    /// The specific gas constant of dry air, in SI base units.
    /// </summary>
    public const int RSpecificDryAir = SpecificGasConstantOfDryAir;

    /// <summary>
    /// The specific gas constant of water, in SI base units.
    /// </summary>
    public static readonly HugeNumber SpecificGasConstantOfWater = new(4615, -1);
    /// <summary>
    /// The specific gas constant of water, in SI base units.
    /// </summary>
    public static readonly HugeNumber RSpecificWater = SpecificGasConstantOfWater;

    /// <summary>
    /// The ratio of the specific gas constants of dry air to water, in SI base units.
    /// </summary>
    public static readonly HugeNumber RSpecificRatioOfDryAirToWater = RSpecificDryAir / RSpecificWater;

    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public static readonly HugeNumber SpecificHeatOfDryAir = new(10035, -1);
    /// <summary>
    /// The specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public static readonly HugeNumber CpDryAir = SpecificHeatOfDryAir;

    /// <summary>
    /// The specific heat multiplied by the specific gas constant of dry air at constant pressure, in SI base units.
    /// </summary>
    public static readonly HugeNumber CpTimesRSpecificDryAir = CpDryAir * RSpecificDryAir;

    /// <summary>
    /// The specific gas constant divided by the specific heat of dry air at constant pressure, in SI base units.
    /// </summary>
    public static readonly HugeNumber RSpecificOverCpDryAir = RSpecificDryAir / CpDryAir;

    /// <summary>
    /// The speed of light in a vacuum, in m/s.
    /// </summary>
    public const int SpeedOfLight = 299792458;
    /// <summary>
    /// The speed of light in a vacuum, in m/s.
    /// </summary>
    public const int c = SpeedOfLight;

    /// <summary>
    /// The speed of light in a vacuum, squared, in m/s.
    /// </summary>
    public static readonly HugeNumber SpeedOfLightSquared = ((HugeNumber)SpeedOfLight).Square();
    /// <summary>
    /// The speed of light in a vacuum, squared, in m/s.
    /// </summary>
    public static readonly HugeNumber cSquared = SpeedOfLightSquared;

    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public static readonly HugeNumber StandardAtmosphericPressure = new(101325, -3);
    /// <summary>
    /// The standard atmospheric pressure, in SI base units.
    /// </summary>
    public static readonly HugeNumber atm = StandardAtmosphericPressure;

    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber StefanBoltzmannConstant = new(5670367, -14);
    /// <summary>
    /// The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber sigma = StefanBoltzmannConstant;

    /// <summary>
    /// Four times The Stefan–Boltzmann constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber FourSigma = 4 * sigma;

    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber UniversalGasConstant = new(83144598, -7);
    /// <summary>
    /// The universal gas constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber R = UniversalGasConstant;

    /// <summary>
    /// The molar mass of air divided by the universal gas constant, in SI base units.
    /// </summary>
    public static readonly HugeNumber MAirOverR = MAir / R;

    #endregion Science
}