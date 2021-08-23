namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Respresents negative one as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeOne { get; } = new(-1, 0);

    /// <summary>
    /// Respresents negative zero as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeZero { get; } = new(0, 1, -1);

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
    /// Respresents zero as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zero => new(0, 0);
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
    /// <para>
    /// Respresents the smallest positive <see cref="HugeNumber"/> value that is greater than
    /// zero.
    /// </para>
    /// <para>
    /// This is (1/65535)e-32767.
    /// </para>
    /// </summary>
    public static HugeNumber Epsilon { get; } = new(1, ushort.MaxValue, MIN_EXPONENT);

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
    public static HugeNumber NaN { get; } = new();

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
    public static HugeNumber NegativeInfinity { get; } = new(-1, 0, 0);

    /// <summary>
    /// Respresents positive infinity as a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber PositiveInfinity { get; } = new(1, 0, 0);

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

    private static readonly HugeNumber[] _TangentTaylorSeries = new HugeNumber[]
    {
        new(1, 3, 0),
        new(2, 15, 0),
        new(17, 315, 0),
        new(62, 2835, 0),
    };
    private static readonly HugeNumber _Root3 = new HugeNumber(3).Sqrt();
    private static readonly HugeNumber _TwoMinusRoot3 = new HugeNumber(2) - _Root3;
}
