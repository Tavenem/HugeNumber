namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between the two given values.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between <paramref
    /// name="first"/> and <paramref name="second"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between <paramref name="first"/> and <paramref name="second"/>; or <see
    /// cref="NaN"/> if the weight cannot be computed for the given
    /// parameters.</returns>
    /// <remarks>
    /// <see cref="NaN"/> will be returned if the given values are nearly
    /// equal, but the given result is not also nearly equal to them, since the calculation in
    /// that case would require a division by zero.
    /// </remarks>
    public static HugeNumber InverseLerp(HugeNumber first, HugeNumber second, HugeNumber result)
    {
        var difference = second - first;
        if (difference.Mantissa == 0)
        {
            if (result == first)
            {
                return HugeNumberConstants.Half;
            }
            else
            {
                return NaN;
            }
        }
        return (result - first) / difference;
    }

    /// <summary>
    /// Finds the weight which would produce the given <paramref name="result"/> when linearly
    /// interpolating between this value and the <paramref name="other"/> value.
    /// </summary>
    /// <param name="other">The second value.</param>
    /// <param name="result">The desired result of a linear interpolation between this instance
    /// and <paramref name="other"/>.</param>
    /// <returns>The weight which would produce <paramref name="result"/> when linearly
    /// interpolating between this instance and <paramref name="other"/>; or <see cref="NaN"/>
    /// if the weight cannot be computed for the given parameters.</returns>
    /// <remarks>
    /// <see cref="NaN"/> will be returned if the given values are nearly equal, but the given
    /// result is not also nearly equal to them, since the calculation in that case would
    /// require a division by zero.
    /// </remarks>
    public HugeNumber InverseLerp(HugeNumber other, HugeNumber result) => InverseLerp(this, other, result);

    /// <summary>
    /// Linearly interpolates between two values based on the given weighting.
    /// </summary>
    /// <param name="first">The first value.</param>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from <paramref name="first"/> as <paramref
    /// name="second"/>, rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as <paramref
    /// name="first"/>, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public static HugeNumber Lerp(HugeNumber first, HugeNumber second, HugeNumber amount)
        => first + ((second - first) * amount);

    /// <summary>
    /// Linearly interpolates between this value and another based on the given weighting.
    /// </summary>
    /// <param name="second">The second value.</param>
    /// <param name="amount">Value between 0 and 1 indicating the weight of the second source
    /// vector.</param>
    /// <returns>The interpolated value.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="amount"/> is negative, a value will be obtained that is in the
    /// opposite direction on a number line from this instance as <paramref name="second"/>,
    /// rather than between them. E.g. <c>Lerp(2, 3, -0.5)</c> would return
    /// <c>1.5</c>.
    /// </para>
    /// <para>If <paramref name="amount"/> is greater than one, a value will be obtained that is
    /// in the opposite direction on a number line from <paramref name="second"/> as this
    /// instance, rather than between them. E.g. <c>Lerp(2, 3, 1.5)</c> would return
    /// <c>3.5</c>.</para>
    /// </remarks>
    public HugeNumber Lerp(HugeNumber second, HugeNumber amount) => Lerp(this, second, amount);
}
