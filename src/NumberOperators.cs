using System;
using System.Globalization;
using System.Linq;

namespace Tavenem.HugeNumbers
{
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
        public static HugeNumber operator ++(HugeNumber value) => !value.IsFinite ? value : value + One;

        /// <summary>
        /// Decrements an <see cref="HugeNumber"/> value by 1.
        /// </summary>
        /// <param name="value">The value to decrement.</param>
        /// <returns>The value of the <paramref name="value"/> parameter decremented by 1.</returns>
        public static HugeNumber operator --(HugeNumber value) => !value.IsFinite ? value : value - One;

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

        /// <summary>
        /// Returns a value that indicates whether a <see cref="decimal"/> value and an <see
        /// cref="HugeNumber"/> value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(decimal left, HugeNumber right) => right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether a <see cref="double"/> value and an <see
        /// cref="HugeNumber"/> value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(double left, HugeNumber right) => right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether a signed <see cref="long"/> integer value and
        /// an <see cref="HugeNumber"/> value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(long left, HugeNumber right) => right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether an unsigned <see cref="long"/> integer value and
        /// an <see cref="HugeNumber"/> value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(ulong left, HugeNumber right) => right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
        /// cref="decimal"/> value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(HugeNumber left, decimal right) => left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
        /// cref="double"/> value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(HugeNumber left, double right) => left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a signed <see
        /// cref="long"/> integer value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(HugeNumber left, long right) => left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and an unsigned
        /// 64-bit integer value are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(HugeNumber left, ulong right) => left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether the values of two <see cref="HugeNumber"/> objects are equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/>
        /// parameters have the same value; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator ==(HugeNumber left, HugeNumber right) => left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether a <see cref="decimal"/> and an <see
        /// cref="HugeNumber"/> value are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(decimal left, HugeNumber right) => !right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether a <see cref="double"/> and an <see
        /// cref="HugeNumber"/> value are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(double left, HugeNumber right) => !right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether a 64-bit signed integer and an <see
        /// cref="HugeNumber"/> value are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(long left, HugeNumber right) => !right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether a 64-bit unsigned integer and an <see
        /// cref="HugeNumber"/> value are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(ulong left, HugeNumber right) => !right.Equals(left);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
        /// cref="decimal"/> are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(HugeNumber left, decimal right) => !left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a <see
        /// cref="double"/> are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(HugeNumber left, double right) => !left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a 64-bit signed
        /// integer are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(HugeNumber left, long right) => !left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> and a 64-bit unsigned
        /// integer are not equal.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(HugeNumber left, ulong right) => !left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether the two <see cref="HugeNumber"/> objects have
        /// different values.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="left"/> and <paramref name="right"/> are
        /// not equal; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator !=(HugeNumber left, HugeNumber right) => !left.Equals(right);

        /// <summary>
        /// Returns a value that indicates whether a <see cref="decimal"/> is less than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(decimal left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="double"/> is less than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(double left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit signed integer is less than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(long left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit unsigned integer is less than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(ulong left, HugeNumber right) => right.CompareTo(left) * -1 < 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(HugeNumber left, decimal right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(HugeNumber left, double right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a 64-bit
        /// signed integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(HugeNumber left, long right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than a 64-bit
        /// unsigned integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(HugeNumber left, ulong right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is less than another
        /// <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <(HugeNumber left, HugeNumber right) => left.CompareTo(right) < 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="decimal"/> is greater than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(decimal left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="double"/> is greater than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(double left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit signed integer is greater than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(long left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit unsigned integer is greater than an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(ulong left, HugeNumber right) => right.CompareTo(left) * -1 > 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
        /// <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(HugeNumber left, decimal right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
        /// <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(HugeNumber left, double right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a 64-bit
        /// signed integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(HugeNumber left, long right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
        /// 64-bit unsigned integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(HugeNumber left, ulong right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is greater than
        /// another <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >(HugeNumber left, HugeNumber right) => left.CompareTo(right) > 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="decimal"/> is less than or equal to an
        /// <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(decimal left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="double"/> is less than or equal to an
        /// <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(double left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit signed integer is less than or equal to an <see
        /// cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(long left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit unsigned integer is less than or equal to
        /// an <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(ulong left, HugeNumber right) => right.CompareTo(left) * -1 <= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
        /// to a <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(HugeNumber left, decimal right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
        /// to a <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(HugeNumber left, double right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
        /// to a 64-bit signed integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(HugeNumber left, long right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is less than or equal
        /// to a 64-bit unsigned integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(HugeNumber left, ulong right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is less than or
        /// equal to another <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is less than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator <=(HugeNumber left, HugeNumber right) => left.CompareTo(right) <= 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="decimal"/> is greater than or equal
        /// to an <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(decimal left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

        /// <summary>
        /// Returns a value that indicates whether a <see cref="double"/> is greater than or equal
        /// to an <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(double left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit signed integer is greater than or equal
        /// to an <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(long left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

        /// <summary>
        /// Returns a value that indicates whether a 64-bit unsigned integer is greater than or equal
        /// to an <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(ulong left, HugeNumber right) => right.CompareTo(left) * -1 >= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
        /// <see cref="decimal"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(HugeNumber left, decimal right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a
        /// <see cref="double"/>.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(HugeNumber left, double right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than a 64-bit
        /// signed integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than <paramref name="right"/>;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(HugeNumber left, long right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> is greater than or
        /// equal to a 64-bit unsigned integer.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(HugeNumber left, ulong right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Returns a value that indicates whether an <see cref="HugeNumber"/> value is greater than
        /// or equal to another <see cref="HugeNumber"/> value.
        /// </summary>
        /// <param name="left">The first value to compare.</param>
        /// <param name="right">The second value to compare.</param>
        /// <returns>
        /// <see langword="true"/> if <paramref name="left"/> is greater than or equal to <paramref
        /// name="right"/>; otherwise, <see langword="false"/>.
        /// </returns>
        public static bool operator >=(HugeNumber left, HugeNumber right) => left.CompareTo(right) >= 0;

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(bool value) => value ? new HugeNumber(1) : Zero;

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(byte value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(double value)
        {
            if (double.IsNaN(value))
            {
                return NaN;
            }
            if (double.IsNegativeInfinity(value))
            {
                return NegativeInfinity;
            }
            if (double.IsPositiveInfinity(value))
            {
                return PositiveInfinity;
            }

            var exponent = 0;

            var str = value.ToString("G17", CultureInfo.InvariantCulture.NumberFormat).AsSpan();
            var expIndex = str.IndexOf('E');
            if (expIndex != -1)
            {
                exponent = int.Parse(str[(expIndex + 1)..]);

                str = str[0..expIndex];
            }
            var sepIndex = str.IndexOf(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            if (sepIndex != -1)
            {
                exponent -= str.Length - sepIndex - CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Length;
            }
            var neg = str.StartsWith(CultureInfo.InvariantCulture.NumberFormat.NegativeSign);
            var mantissa = long.Parse(string.Concat(str.ToArray().Where(char.IsDigit)));
            if (neg)
            {
                mantissa = -mantissa;
            }

            return new HugeNumber(mantissa, exponent);
        }

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(float value)
        {
            if (float.IsNaN(value))
            {
                return NaN;
            }
            if (float.IsNegativeInfinity(value))
            {
                return NegativeInfinity;
            }
            if (float.IsPositiveInfinity(value))
            {
                return PositiveInfinity;
            }

            var exponent = 0;

            var str = value.ToString("G9", CultureInfo.InvariantCulture.NumberFormat).AsSpan();
            var expIndex = str.IndexOf('E');
            if (expIndex != -1)
            {
                exponent = int.Parse(str[(expIndex + 1)..]);

                str = str[0..expIndex];
            }
            var sepIndex = str.IndexOf(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            if (sepIndex != -1)
            {
                exponent -= str.Length - sepIndex - CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Length;
            }
            var neg = str.StartsWith(CultureInfo.InvariantCulture.NumberFormat.NegativeSign);
            var mantissa = long.Parse(string.Concat(str.ToArray().Where(char.IsDigit)));
            if (neg)
            {
                mantissa = -mantissa;
            }

            return new HugeNumber(mantissa, exponent);
        }

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(int value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(long value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(sbyte value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(short value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(uint value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(ulong value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static implicit operator HugeNumber(ushort value) => new(value);

        /// <summary>
        /// Converts the given value to a <see cref="HugeNumber"/>.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        public static explicit operator HugeNumber(decimal value)
        {
            var exponent = 0;

            var str = value.ToString("G", CultureInfo.InvariantCulture.NumberFormat).AsSpan();
            var sepIndex = str.IndexOf(CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator);
            if (sepIndex != -1)
            {
                exponent -= str.Length - sepIndex - CultureInfo.InvariantCulture.NumberFormat.NumberDecimalSeparator.Length;
            }
            var neg = str.StartsWith(CultureInfo.InvariantCulture.NumberFormat.NegativeSign);
            var mantissaStr = string.Concat(str.ToArray().Where(char.IsDigit));
            var diff = mantissaStr.Length - 19;
            if (diff > 0)
            {
                exponent += diff;
                mantissaStr = mantissaStr.Substring(0, 19);
            }
            var uMantissa = ulong.Parse(mantissaStr);
            if (uMantissa > 999999999999999999) // 18 digits
            {
                var round = uMantissa % 10;
                if (round >= 5)
                {
                    uMantissa += 10 - round;
                }
                exponent++;
                uMantissa /= 10;
            }
            var mantissa = (long)uMantissa;
            if (neg)
            {
                mantissa = -mantissa;
            }

            return new HugeNumber(mantissa, exponent);
        }

        /// <summary>
        /// Converts the given value to a <see cref="bool"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator bool(HugeNumber value) => !value.IsNaN && value > 0;

        /// <summary>
        /// Converts the given value to a <see cref="byte"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator byte(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(byte.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(byte.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (byte)Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="decimal"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator decimal(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(decimal.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(decimal.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                var v = (decimal)value.Mantissa;
                for (var i = 0; i < value.Exponent; i++)
                {
                    v *= 10;
                }
                for (var i = 0; i > value.Exponent; i--)
                {
                    v /= 10;
                }
                return v;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="double"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator double(HugeNumber value)
        {
            if (value.IsNaN)
            {
                return double.NaN;
            }
            else if (value.IsPositiveInfinity || value.CompareTo(double.MaxValue) > 0)
            {
                return double.PositiveInfinity;
            }
            else if (value.IsNegativeInfinity || value.CompareTo(double.MinValue) < 0)
            {
                return double.NegativeInfinity;
            }
            else
            {
                return value.Mantissa * Math.Pow(10, value.Exponent);
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="float"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator float(HugeNumber value)
        {
            if (value.IsNaN)
            {
                return float.NaN;
            }
            else if (value.IsPositiveInfinity || value.CompareTo(float.MaxValue) > 0)
            {
                return float.PositiveInfinity;
            }
            else if (value.IsNegativeInfinity || value.CompareTo(float.MinValue) < 0)
            {
                return float.NegativeInfinity;
            }
            else
            {
                return (float)(value.Mantissa * Math.Pow(10, value.Exponent));
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="int"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator int(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(int.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(int.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (int)Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="long"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator long(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(long.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(long.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="sbyte"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator sbyte(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(sbyte.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(sbyte.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (sbyte)Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="short"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator short(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(short.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(short.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (short)Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="uint"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator uint(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(uint.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(uint.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (uint)Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="ulong"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator ulong(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(ulong.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(ulong.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (ulong)Round(value).Mantissa;
            }
        }

        /// <summary>
        /// Converts the given value to a <see cref="ushort"/>.
        /// </summary>
        /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
        public static explicit operator ushort(HugeNumber value)
        {
            if (value.IsNaN)
            {
                throw new ArgumentOutOfRangeException(nameof(value), HugeNumberErrorMessages.NaNConversion);
            }
            else if (value.CompareTo(ushort.MaxValue) > 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else if (value.CompareTo(ushort.MinValue) < 0)
            {
                throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
            }
            else
            {
                return (ushort)Round(value).Mantissa;
            }
        }
    }
}
