namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Returns the value of the <see cref="HugeNumber"/> operand. (The sign of the operand is unchanged.)
    /// </summary>
    /// <param name="value">An integer value.</param>
    /// <returns>The value of the <paramref name="value"/> operand.</returns>
    public static HugeNumber operator +(HugeNumber value) => value;

    /// <summary>
    /// Adds the values of two specified <see cref="HugeNumber"/> objects.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, decimal right) => Add(left, (HugeNumber)right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="decimal"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(decimal left, HugeNumber right) => Add((HugeNumber)left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, double right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="double"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(double left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="long"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, long right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="long"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(long left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="ulong"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(HugeNumber left, ulong right) => Add(left, right);

    /// <summary>
    /// Adds the values of a <see cref="HugeNumber"/> and a <see cref="ulong"/>.
    /// </summary>
    /// <param name="left">The first value to add.</param>
    /// <param name="right">The second value to add.</param>
    /// <returns>The sum of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator +(ulong left, HugeNumber right) => Add(left, right);

    /// <summary>
    /// Negates a specified <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="value">The value to negate.</param>
    /// <returns>The result of the <paramref name="value"/> parameter multiplied by negative one (-1).</returns>
    public static HugeNumber operator -(HugeNumber value) => Negate(value);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from another <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="decimal"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, decimal right) => Subtract(left, (HugeNumber)right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(decimal left, HugeNumber right) => Subtract((HugeNumber)left, right);

    /// <summary>
    /// Subtracts a <see cref="double"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, double right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="double"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(double left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="long"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, long right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="long"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(long left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="ulong"/> value from a <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(HugeNumber left, ulong right) => Subtract(left, right);

    /// <summary>
    /// Subtracts a <see cref="HugeNumber"/> value from a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="left">The value to subtract from (the minuend).</param>
    /// <param name="right">The value to subtract (the subtrahend).</param>
    /// <returns>The result of subtracting <paramref name="right"/> from <paramref name="left"/>.</returns>
    public static HugeNumber operator -(ulong left, HugeNumber right) => Subtract(left, right);

    /// <summary>
    /// Increments an <see cref="HugeNumber"/> value by 1.
    /// </summary>
    /// <param name="value">The value to increment.</param>
    /// <returns>The value of the <paramref name="value"/> parameter incremented by 1.</returns>
    public static HugeNumber operator ++(HugeNumber value) => !value.IsFinite() ? value : value + One;

    /// <summary>
    /// Decrements an <see cref="HugeNumber"/> value by 1.
    /// </summary>
    /// <param name="value">The value to decrement.</param>
    /// <returns>The value of the <paramref name="value"/> parameter decremented by 1.</returns>
    public static HugeNumber operator --(HugeNumber value) => !value.IsFinite() ? value : value - One;

    /// <summary>
    /// Multiplies two specified <see cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, decimal right) => Multiply(left, (HugeNumber)right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="decimal"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(decimal left, HugeNumber right) => Multiply((HugeNumber)left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="double"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, double right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="double"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(double left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="long"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, long right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="long"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(long left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(HugeNumber left, ulong right) => Multiply(left, right);

    /// <summary>
    /// Multiplies a <see cref="HugeNumber"/> and a <see cref="ulong"/> value.
    /// </summary>
    /// <param name="left">The first value to multiply.</param>
    /// <param name="right">The second value to multiply.</param>
    /// <returns>The product of <paramref name="left"/> and <paramref name="right"/>.</returns>
    public static HugeNumber operator *(ulong left, HugeNumber right) => Multiply(left, right);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by another specified <see cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see cref="NaN"/>,
    /// the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result is
    /// infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="decimal"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="dividend"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, decimal divisor) => Divide(dividend, (HugeNumber)divisor);

    /// <summary>
    /// Divides a specified <see cref="decimal"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="divisor"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(decimal dividend, HugeNumber divisor) => Divide((HugeNumber)dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="double"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see cref="NaN"/>,
    /// the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result is
    /// infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, double divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="double"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If either <paramref name="dividend"/> or <paramref name="divisor"/> is <see cref="NaN"/>,
    /// the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result is
    /// infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(double dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="long"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="dividend"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, long divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="long"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="divisor"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(long dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="HugeNumber"/> value by a specified <see cref="ulong"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="dividend"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="dividend"/> is infinite, the result is infinite; <see
    /// cref="PositiveInfinity"/> if its sign matches <paramref name="divisor"/>, and <see
    /// cref="NegativeInfinity"/> if they have opposite signs.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(HugeNumber dividend, ulong divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Divides a specified <see cref="ulong"/> value by a specified <see cref="HugeNumber"/>
    /// value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The result of the division.</returns>
    /// <remarks>
    /// <para>
    /// If <paramref name="divisor"/> is <see cref="NaN"/>, the result is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If both <paramref name="dividend"/> and <paramref name="divisor"/> are zero, the result
    /// is <see cref="NaN"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is zero, but not <paramref name="dividend"/>, the result
    /// is infinite, with a sign that matches <paramref name="dividend"/>.
    /// </para>
    /// <para>
    /// If <paramref name="divisor"/> is infinite and <paramref name="dividend"/> is a non-zero
    /// finite value, the result is 0.
    /// </para>
    /// </remarks>
    public static HugeNumber operator /(ulong dividend, HugeNumber divisor) => Divide(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with two specified <see
    /// cref="HugeNumber"/> values.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, HugeNumber divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="decimal"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see
    /// cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, decimal divisor) => Mod(dividend, (HugeNumber)divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="decimal"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(decimal dividend, HugeNumber divisor) => Mod((HugeNumber)dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="double"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, double divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="double"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(double dividend, HugeNumber divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="long"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see
    /// cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, long divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="long"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(long dividend, HugeNumber divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="HugeNumber"/> and a <see
    /// cref="ulong"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see
    /// cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// </remarks>
    public static HugeNumber operator %(HugeNumber dividend, ulong divisor) => Mod(dividend, divisor);

    /// <summary>
    /// Returns the remainder that results from division with a <see cref="ulong"/> and a <see
    /// cref="HugeNumber"/> value.
    /// </summary>
    /// <param name="dividend">The value to be divided.</param>
    /// <param name="divisor">The value to divide by.</param>
    /// <returns>The remainder that results from the division.</returns>
    /// <remarks>
    /// <para>
    /// If the result of the division is <see cref="NaN"/>, the result will also be <see cref="NaN"/>.
    /// </para>
    /// <para>If the result of the division is infinite, the result will be 0.</para>
    /// <para>
    /// If a non-zero value is divided by an infinite value, the result will have the same value
    /// as <paramref name="dividend"/>, just as in any case when a smaller number is divided by a
    /// larger number.
    /// </para>
    /// </remarks>
    public static HugeNumber operator %(ulong dividend, HugeNumber divisor) => Mod(dividend, divisor);
}
