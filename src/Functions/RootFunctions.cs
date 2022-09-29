using System.Diagnostics;

namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Computes the cube-root of a value.
    /// </summary>
    /// <param name="x">The value whose cube-root is to be computed.</param>
    /// <returns>
    /// The cube root of <paramref name="x"/>.
    /// </returns>
    public static HugeNumber Cbrt(HugeNumber x)
    {
        if (x.IsNaN())
        {
            return NaN;
        }
        if (x.Mantissa == 0
            || x.IsPositiveInfinity())
        {
            return PositiveInfinity;
        }
        if (x == One)
        {
            return x;
        }
        if (x.IsNegativeInfinity())
        {
            return NegativeInfinity;
        }
        if (x == NegativeOne)
        {
            return NegativeOne;
        }

        if (x.IsNegative())
        {
            return -Cbrt(-x);
        }

        return Exp(HugeNumberConstants.Third * Log(x));
    }

    /// <summary>
    /// Computes the cube-root of this value.
    /// </summary>
    /// <returns>
    /// The cube root of this value.
    /// </returns>
    public HugeNumber Cbrt() => Cbrt(this);

    /// <summary>
    /// Computes the hypotenuse given two values representing the lengths of the shorter sides in a
    /// right-angled triangle.
    /// </summary>
    /// <param name="x">The value to square and add to <paramref name="y"/>.</param>
    /// <param name="y">The value to square and add to <paramref name="x"/>.</param>
    /// <returns>
    /// The square root of <paramref name="x"/>-squared plus <paramref name="y"/>-squared.
    /// </returns>
    public static HugeNumber Hypot(HugeNumber x, HugeNumber y)
        => Sqrt((x * x) + (y * y));

    /// <summary>
    /// Computes the hypotenuse given two values representing the lengths of the shorter sides in a
    /// right-angled triangle, where one of those values is this instance.
    /// </summary>
    /// <param name="other">The value to square and add to this value.</param>
    /// <returns>
    /// The square root of this value squared plus <paramref name="other"/>-squared.
    /// </returns>
    public HugeNumber Hypot(HugeNumber other) => Hypot(this, other);

    /// <summary>
    /// Computes the n-th root of a value.
    /// </summary>
    /// <param name="x">The value whose <c>n</c>-th root is to be computed.</param>
    /// <param name="n">The degree of the root to be computed.</param>
    /// <returns>
    /// The <c>n</c>-th root of <paramref name="x"/>.
    /// </returns>
    public static HugeNumber RootN(HugeNumber x, int n)
    {
        HugeNumber result;

        if (n > 0)
        {
            if (n == 2)
            {
                result = !x.IsZero() ? Sqrt(x) : Zero;
            }
            else if (n == 3)
            {
                result = Cbrt(x);
            }
            else
            {
                result = PositiveN(x, n);
            }
        }
        else if (n < 0)
        {
            result = NegativeN(x, n);
        }
        else
        {
            Debug.Assert(n == 0);
            result = NaN;
        }

        return result;

        static HugeNumber PositiveN(HugeNumber x, int n)
        {
            HugeNumber result;

            if (IsFinite(x))
            {
                if (!IsZero(x))
                {
                    if (IsPositive(x) || int.IsOddInteger(n))
                    {
                        result = Pow(Abs(x), One / n);
                        result = CopySign(result, x);
                    }
                    else
                    {
                        result = NaN;
                    }
                }
                else if (int.IsEvenInteger(n))
                {
                    result = Zero;
                }
                else
                {
                    result = CopySign(Zero, x);
                }
            }
            else if (IsNaN(x))
            {
                result = NaN;
            }
            else if (IsPositive(x))
            {
                Debug.Assert(IsPositiveInfinity(x));
                result = PositiveInfinity;
            }
            else
            {
                Debug.Assert(IsNegativeInfinity(x));
                result = int.IsOddInteger(n) ? NegativeInfinity : NaN;
            }

            return result;
        }

        static HugeNumber NegativeN(HugeNumber x, int n)
        {
            HugeNumber result;

            if (IsFinite(x))
            {
                if (!IsZero(x))
                {
                    if (IsPositive(x) || int.IsOddInteger(n))
                    {
                        result = Pow(Abs(x), One / n);
                        result = CopySign(result, x);
                    }
                    else
                    {
                        result = NaN;
                    }
                }
                else if (int.IsEvenInteger(n))
                {
                    result = PositiveInfinity;
                }
                else
                {
                    result = CopySign(PositiveInfinity, x);
                }
            }
            else if (IsNaN(x))
            {
                result = NaN;
            }
            else if (x > 0)
            {
                Debug.Assert(IsPositiveInfinity(x));
                result = Zero;
            }
            else
            {
                Debug.Assert(IsNegativeInfinity(x));
                result = int.IsOddInteger(n) ? NegativeZero : NaN;
            }

            return result;
        }
    }

    /// <summary>
    /// Computes the n-th root of this value.
    /// </summary>
    /// <param name="n">The degree of the root to be computed.</param>
    /// <returns>
    /// The <c>n</c>-th root of this value.
    /// </returns>
    public HugeNumber RootN(int n) => RootN(this, n);

    /// <summary>
    /// Computes the square-root of a value.
    /// </summary>
    /// <param name="x">The value whose square-root is to be computed.</param>
    /// <returns>
    /// The square-root of <paramref name="x"/>.
    /// </returns>
    public static HugeNumber Sqrt(HugeNumber x)
    {
        if (x.IsNaN()
            || x.IsNegative())
        {
            return NaN;
        }
        if (x.Mantissa == 0
            || x == One
            || x.IsPositiveInfinity())
        {
            return x;
        }

        return Exp(HugeNumberConstants.Half * Log(x));
    }

    /// <summary>
    /// Computes the square-root of this value.
    /// </summary>
    /// <returns>
    /// The square-root of this value.
    /// </returns>
    public HugeNumber Sqrt() => Sqrt(this);
}
