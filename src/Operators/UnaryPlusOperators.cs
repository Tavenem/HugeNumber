namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the value of the <see cref="HugeNumber"/> operand. (The sign of the operand is unchanged.)
    /// </summary>
    /// <param name="value">An integer value.</param>
    /// <returns>The value of the <paramref name="value"/> operand.</returns>
    public static HugeNumber operator +(HugeNumber value) => value;
}
