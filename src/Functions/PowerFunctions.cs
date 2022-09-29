namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Computes a value raised to a given power.
    /// </summary>
    /// <param name="x">The value which is raised to the power of <paramref name="y"/>.</param>
    /// <param name="y">The power to which <paramref name="x"/> is raised.</param>
    /// <returns>
    /// <paramref name="x"/> raised to the power of <paramref name="y"/>.
    /// </returns>
    public static HugeNumber Pow(HugeNumber x, HugeNumber y)
    {
        if (x.IsNaN()
            || y.IsNaN())
        {
            return NaN;
        }
        if (y.Mantissa == 0)
        {
            return One;
        }
        if (x.Mantissa == 0)
        {
            return y.IsNegative()
                ? PositiveInfinity
                : Zero;
        }
        if (x == One
            || y == One)
        {
            return x;
        }
        if (x.IsPositiveInfinity())
        {
            return y.IsNegative()
                ? Zero
                : PositiveInfinity;
        }
        if (x.IsNegative()
            && y.Exponent < 0
            && (One / y).Exponent < 0)
        {
            return NaN;
        }
        if (x.IsNegativeInfinity())
        {
            if (y.IsNegative())
            {
                return Zero;
            }
            else if (y.Exponent < 0 || y % 2 != 0)
            {
                return NegativeInfinity;
            }
            else
            {
                return PositiveInfinity;
            }
        }
        if (y.IsPositiveInfinity())
        {
            if (x == NegativeOne)
            {
                return NaN;
            }
            else if (x.Abs() > One)
            {
                return PositiveInfinity;
            }
            else
            {
                return Zero;
            }
        }
        if (y.IsNegativeInfinity())
        {
            if (x == NegativeOne)
            {
                return NaN;
            }
            else if (x.Abs() > One)
            {
                return Zero;
            }
            else
            {
                return NaN;
            }
        }
        if (x == NegativeOne)
        {
            return y.Exponent < 0 || y % 2 != 0
                ? NegativeOne
                : One;
        }

        if (x.IsNegative())
        {
            return -Pow(-x, y);
        }

        if (y.IsNegative())
        {
            return One / Pow(x, -y);
        }

        return Exp(y * Log(x));
    }

    /// <summary>
    /// Computes this value raised to a given <paramref name="power"/>.
    /// </summary>
    /// <param name="power">The power to which this value is raised.</param>
    /// <returns>
    /// This value raised to the given <paramref name="power"/>.
    /// </returns>
    public HugeNumber Pow(HugeNumber power) => Pow(this, power);
}
