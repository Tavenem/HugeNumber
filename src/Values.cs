namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Gets the value <c>-1</c> for <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber NegativeOne { get; } = new(-1, 0);

    /// <summary>
    /// Gets a value that represents negative <c>zero</c>.
    /// </summary>
    public static HugeNumber NegativeZero { get; } = new(0, 1, -1);

    /// <summary>
    /// Gets the value <c>1</c> for <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber One { get; } = new(1, 0);
    /// <summary>
    /// <para>
    /// Gets the multiplicative identity of <see cref="HugeNumber"/>.
    /// </para>
    /// <para>
    /// For <see cref="HugeNumber"/>, this is one.
    /// </para>
    /// </summary>
    public static HugeNumber MultiplicativeIdentity => One;

    /// <summary>
    /// Gets the value <c>0</c> for <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber Zero => new(0, 0);
    /// <summary>
    /// <para>
    /// Gets the additive identity of the current type.
    /// </para>
    /// <para>
    /// For <see cref="HugeNumber"/>, this is zero.
    /// </para>
    /// </summary>
    public static HugeNumber AdditiveIdentity => Zero;

    /// <summary>
    /// <para>
    /// Gets the smallest value such that can be added to <c>0</c> that does not result in <c>0</c>.
    /// </para>
    /// <para>
    /// This is (1/65535)e-32767.
    /// </para>
    /// </summary>
    public static HugeNumber Epsilon { get; } = new(1, ushort.MaxValue, MIN_EXPONENT);

    /// <summary>
    /// Gets the maximum value of a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber MaxValue { get; } = new(MAX_MANTISSA, MAX_EXPONENT);

    /// <summary>
    /// Gets the minimum value of a <see cref="HugeNumber"/>.
    /// </summary>
    public static HugeNumber MinValue { get; } = new(MIN_MANTISSA, MAX_EXPONENT);

    /// <summary>
    /// Gets a value that represents <c>NaN</c>.
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
    /// Gets a value that represents negative <c>infinity</c>.
    /// </summary>
    public static HugeNumber NegativeInfinity { get; } = new(-1, 0, 0);

    /// <summary>
    /// Gets a value that represents positive <c>infinity</c>.
    /// </summary>
    public static HugeNumber PositiveInfinity { get; } = new(1, 0, 0);

    /// <summary>
    /// Represents the natural logarithmic base, specified by the constant <c>e</c>, rounded to 18
    /// places of precision, which is the limit of the <see cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber E { get; } = new(271828182845904524, -17);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its diameter, specified by the
    /// constant <c>π</c>, rounded to 18 places of precision, which is the limit of the <see
    /// cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Pi { get; } = new(314159265358979324, -17);

    /// <summary>
    /// Represents the ratio of the circumference of a circle to its radius, specified by the
    /// constant <c>τ</c>, rounded to 18 places of precision, which is the limit of the <see
    /// cref="HugeNumber"/> structure.
    /// </summary>
    public static HugeNumber Tau { get; } = new(628318530717958647, -17);

    private static readonly HugeNumber[] _TangentTaylorSeries =
    [
        new(1, 3, 1),
        new(2, 15, 1),
        new(17, 315, 1),
        new(62, 2835, 1),
    ];
    private static readonly HugeNumber _Root3 = new(173205080756887729, -17);
    private static readonly HugeNumber _TwoMinusRoot3 = new(267949192431122706, -18);
}
