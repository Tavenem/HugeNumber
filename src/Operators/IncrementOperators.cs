namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Increments the given <paramref name="value"/> by no less than 1, and at least the
    /// minimum amount required to produce a distinct value.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>The given <paramref name="value"/> incremented by no less than 1, and at least
    /// enough to produce a value distinct from the given <paramref name="value"/>.</returns>
    /// <remarks>
    /// <para>
    /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
    /// the mantissa, a simple <c>x + 1</c> or <c>x++</c> operation may not produce a distinct
    /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
    /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
    /// </para>
    /// <para>
    /// For example, <c>new Number(1, 200) + 1</c> will not result in a different value than
    /// <c>new Number(1, 200)</c>. In this case, the minimum representable value larger than
    /// <c>new Number(1, 200)</c> is <c>new Number(1.00000000000000001, 200)</c> (or
    /// equivalently, <c>new Number(100000000000000001, 183)</c>).
    /// </para>
    /// </remarks>
    public static HugeNumber Increment(HugeNumber value) => value.IsNegativeInfinity()
        ? MinValue
        : value + Max(One, GetEpsilon(value));

    /// <summary>
    /// Increments this instance by no less than 1, and at least the minimum amount required to
    /// produce a distinct value.
    /// </summary>
    /// <returns>This instance incremented by no less than 1, and at least enough to produce a
    /// value distinct from this instance.</returns>
    /// <remarks>
    /// <para>
    /// Because <see cref="HugeNumber"/> structures have a limited number of significant digits in
    /// the mantissa, a simple <c>x + 1</c> or <c>x++</c> operation may not produce a distinct
    /// value from <c>x</c>, for very large values of <c>x</c>. This method guarantees that the
    /// result <i>will</i> be distinct from its input by the minimum amount (but at least by 1).
    /// </para>
    /// <para>
    /// For example, <c>new Number(1, 200) + 1</c> will not result in a different value than
    /// <c>new Number(1, 200)</c>. In this case, the minimum representable value larger than
    /// <c>new Number(1, 200)</c> is <c>new Number(1.00000000000000001, 200)</c> (or
    /// equivalently, <c>new Number(100000000000000001, 183)</c>).
    /// </para>
    /// </remarks>
    public HugeNumber Increment() => Increment(this);

    /// <summary>
    /// Increments an <see cref="HugeNumber"/> value by 1.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>The value of the <paramref name="value"/> parameter incremented by 1.</returns>
    public static HugeNumber operator ++(HugeNumber value) => !value.IsFinite() ? value : value + One;
}
