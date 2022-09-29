using System.Globalization;

namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Attempts to creates a new instance of <see cref="HugeNumber"/> by converting an existing value.
    /// </summary>
    /// <typeparam name="TOther">The type from which to convert.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <param name="result">
    /// When the method returns, will contain an instance of <see cref="HugeNumber"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if <paramref name="value"/> could be converted;
    /// otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryCreate<TOther>(TOther value, out HugeNumber result)
        where TOther : INumberBase<TOther>
    {
        if (typeof(TOther) == typeof(byte))
        {
            result = (byte)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(char))
        {
            result = (char)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(decimal))
        {
            result = (HugeNumber)(decimal)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(double))
        {
            result = (double)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(short))
        {
            result = (short)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(int))
        {
            result = (int)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(long))
        {
            result = (long)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(nint))
        {
            result = (nint)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(sbyte))
        {
            result = (sbyte)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(float))
        {
            result = (float)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(ushort))
        {
            result = (ushort)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(uint))
        {
            result = (uint)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(ulong))
        {
            result = (ulong)(object)value;
            return true;
        }
        else if (typeof(TOther) == typeof(nuint))
        {
            result = (nuint)(object)value;
            return true;
        }
        else if (TryParse(value.ToString(null, null), out result))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// Creates a new instance of <see cref="HugeNumber"/> by converting an existing value.
    /// </summary>
    /// <typeparam name="TOther">The type from which to convert.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <returns>
    /// A new <see cref="HugeNumber"/> instance equivalent to <paramref name="value"/>.
    /// </returns>
    /// <exception cref="NotSupportedException" />
    /// <remarks>
    /// This method is intended to perform a checked conversion, but no supported type can
    /// have a value which exceeds the range representable by <see cref="HugeNumber"/>.
    /// </remarks>
    public static HugeNumber CreateChecked<TOther>(TOther value) where TOther : INumberBase<TOther>
        => TryCreate(value, out var result)
        ? result
        : throw new NotSupportedException($"Conversion from type {typeof(TOther).Name} not supported");

    /// <summary>
    /// Creates a new instance of <see cref="HugeNumber"/> by converting an existing value.
    /// </summary>
    /// <typeparam name="TOther">The type from which to convert.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <returns>
    /// A new <see cref="HugeNumber"/> instance equivalent to <paramref name="value"/>.
    /// </returns>
    /// <exception cref="NotSupportedException" />
    /// <remarks>
    /// This method is intended to perform a clamped conversion, but no supported type can
    /// have a value which exceeds the range representable by <see cref="HugeNumber"/>.
    /// </remarks>
    public static HugeNumber CreateSaturating<TOther>(TOther value)
        where TOther : INumberBase<TOther> => CreateChecked(value);

    /// <summary>
    /// Creates a new instance of <see cref="HugeNumber"/> by converting an existing value.
    /// </summary>
    /// <typeparam name="TOther">The type from which to convert.</typeparam>
    /// <param name="value">The value to be converted.</param>
    /// <returns>
    /// A new <see cref="HugeNumber"/> instance equivalent to <paramref name="value"/>.
    /// </returns>
    /// <exception cref="NotSupportedException" />
    /// <remarks>
    /// This method is intended to perform a truncating conversion, but no supported type can
    /// have a value which exceeds the range representable by <see cref="HugeNumber"/>.
    /// </remarks>
    public static HugeNumber CreateTruncating<TOther>(TOther value)
        where TOther : INumberBase<TOther> => CreateChecked(value);

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
    public static implicit operator HugeNumber(nint value) => new(value);

    /// <summary>
    /// Converts the given value to a <see cref="HugeNumber"/>.
    /// </summary>
    /// <param name="value">The value to convert.</param>
    public static implicit operator HugeNumber(nuint value) => new(value);

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
        var mantissaStr = string.Concat(str.ToArray().Where(char.IsDigit)).TrimStart('0');
        if (mantissaStr.Length == 0)
        {
            return Zero;
        }
        var diff = mantissaStr.Length - 19;
        if (diff > 0)
        {
            exponent += diff;
            mantissaStr = mantissaStr[..19];
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
    public static explicit operator bool(HugeNumber value) => !value.IsNaN() && value > 0;

    /// <summary>
    /// Converts the given value to a <see cref="byte"/>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator byte(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
    /// Converts the given value to a <see cref="char"/>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator char(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
            return Convert.ToChar((ushort)Round(value).Mantissa);
        }
    }

    /// <summary>
    /// Converts the given value to a <see cref="decimal"/>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator decimal(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
            var v = (decimal)value.Mantissa / value.Denominator;
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
        if (value.IsNaN())
        {
            return double.NaN;
        }
        else if (value.IsPositiveInfinity() || value.CompareTo(double.MaxValue) > 0)
        {
            return double.PositiveInfinity;
        }
        else if (value.IsNegativeInfinity() || value.CompareTo(double.MinValue) < 0)
        {
            return double.NegativeInfinity;
        }
        else
        {
            return (double)value.Mantissa / value.Denominator * Math.Pow(10, value.Exponent);
        }
    }

    /// <summary>
    /// Converts the given value to a <see cref="float"/>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator float(HugeNumber value)
    {
        if (value.IsNaN())
        {
            return float.NaN;
        }
        else if (value.IsPositiveInfinity() || value.CompareTo(float.MaxValue) > 0)
        {
            return float.PositiveInfinity;
        }
        else if (value.IsNegativeInfinity() || value.CompareTo(float.MinValue) < 0)
        {
            return float.NegativeInfinity;
        }
        else
        {
            return (float)((double)value.Mantissa / value.Denominator * Math.Pow(10, value.Exponent));
        }
    }

    /// <summary>
    /// Converts the given value to a <see cref="int"/>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator int(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
    /// Converts the given value to a <c>nint</c>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator nint(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
        }
        else if (value.CompareTo(nint.MaxValue) > 0)
        {
            throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
        }
        else if (value.CompareTo(nint.MinValue) < 0)
        {
            throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
        }
        else
        {
            return (nint)Round(value).Mantissa;
        }
    }

    /// <summary>
    /// Converts the given value to a <c>nuint</c>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator nuint(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
        }
        else if (value.CompareTo(nuint.MaxValue) > 0)
        {
            throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
        }
        else if (value.CompareTo(nuint.MinValue) < 0)
        {
            throw new OverflowException(HugeNumberErrorMessages.TypeRangeLimit);
        }
        else
        {
            return (nuint)Round(value).Mantissa;
        }
    }

    /// <summary>
    /// Converts the given value to a <see cref="sbyte"/>.
    /// </summary>
    /// <param name="value">The <see cref="HugeNumber"/> to convert.</param>
    public static explicit operator sbyte(HugeNumber value)
    {
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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
        if (value.IsNaN())
        {
            throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
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

    /// <summary>
    /// Returns the <see cref="TypeCode"/> for this instance.
    /// </summary>
    /// <returns>
    /// The enumerated constant that is the <see cref="TypeCode"/> of the class or value type that implements this interface.
    /// </returns>
    public TypeCode GetTypeCode() => TypeCode.Object;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="bool"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="bool"/> value equivalent to the value of this instance.
    /// </returns>
    public bool ToBoolean(IFormatProvider? provider) => (bool)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="byte"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="byte"/> value equivalent to the value of this instance.
    /// </returns>
    public byte ToByte(IFormatProvider? provider) => (byte)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="char"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="char"/> value equivalent to the value of this instance.
    /// </returns>
    public char ToChar(IFormatProvider? provider) => (char)this;

    /// <summary>
    /// Calling this method always throws <see cref="InvalidCastException"/>.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <exception cref="InvalidCastException" />
    public DateTime ToDateTime(IFormatProvider? provider) => throw new InvalidCastException();

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="decimal"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="decimal"/> value equivalent to the value of this instance.
    /// </returns>
    public decimal ToDecimal(IFormatProvider? provider) => (decimal)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="double"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="double"/> value equivalent to the value of this instance.
    /// </returns>
    public double ToDouble(IFormatProvider? provider) => (double)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="short"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="short"/> value equivalent to the value of this instance.
    /// </returns>
    public short ToInt16(IFormatProvider? provider) => (short)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="int"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="int"/> value equivalent to the value of this instance.
    /// </returns>
    public int ToInt32(IFormatProvider? provider) => (int)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="long"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="long"/> value equivalent to the value of this instance.
    /// </returns>
    public long ToInt64(IFormatProvider? provider) => (long)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="sbyte"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="sbyte"/> value equivalent to the value of this instance.
    /// </returns>
    public sbyte ToSByte(IFormatProvider? provider) => (sbyte)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="float"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="float"/> value equivalent to the value of this instance.
    /// </returns>
    public float ToSingle(IFormatProvider? provider) => (float)this;

    /// <summary>
    /// Converts the value of this instance to an <see cref="object"/>
    /// of the specified <see cref="Type"/> that has an equivalent value,
    /// using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="conversionType">
    /// The <see cref="Type"/> to which the value of this instance is converted.
    /// </param>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// An <see cref="object"/> instance of type <paramref name="conversionType"/>
    /// whose value is equivalent to the value of this instance.
    /// </returns>
    /// <exception cref="InvalidCastException" />
    public object ToType(Type conversionType, IFormatProvider? provider)
    {
        if (conversionType == typeof(HugeNumber))
        {
            return this;
        }
        if (conversionType == typeof(bool))
        {
            return (bool)this;
        }
        if (conversionType == typeof(byte))
        {
            return (byte)this;
        }
        if (conversionType == typeof(char))
        {
            return (char)this;
        }
        if (conversionType == typeof(decimal))
        {
            return (decimal)this;
        }
        if (conversionType == typeof(double))
        {
            return (double)this;
        }
        if (conversionType == typeof(short))
        {
            return (short)this;
        }
        if (conversionType == typeof(int))
        {
            return (int)this;
        }
        if (conversionType == typeof(long))
        {
            return (long)this;
        }
        if (conversionType == typeof(sbyte))
        {
            return (sbyte)this;
        }
        if (conversionType == typeof(float))
        {
            return (float)this;
        }
        if (conversionType == typeof(ushort))
        {
            return (ushort)this;
        }
        if (conversionType == typeof(uint))
        {
            return (uint)this;
        }
        if (conversionType == typeof(ulong))
        {
            return (ulong)this;
        }
        throw new InvalidCastException();
    }

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="ushort"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="ushort"/> value equivalent to the value of this instance.
    /// </returns>
    public ushort ToUInt16(IFormatProvider? provider) => (ushort)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="uint"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="uint"/> value equivalent to the value of this instance.
    /// </returns>
    public uint ToUInt32(IFormatProvider? provider) => (uint)this;

    /// <summary>
    /// Converts the value of this instance to an equivalent
    /// <see cref="ulong"/> value using the specified culture-specific formatting information.
    /// </summary>
    /// <param name="provider">
    /// An <see cref="IFormatProvider"/> interface implementation that supplies culture-specific formatting information.
    /// </param>
    /// <returns>
    /// A <see cref="ulong"/> value equivalent to the value of this instance.
    /// </returns>
    public ulong ToUInt64(IFormatProvider? provider) => (ulong)this;

    /// <summary>
    /// Attempts to create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="result">
    /// If this method returns <see langword="true"/>, this will be set to a value of type
    /// <typeparamref name="TTarget"/> with the same value as this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion succeeded; otherwise <see langword="false"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// This value is <see cref="NaN"/> and <typeparamref name="TTarget"/> cannot represent NaN
    /// values.
    /// </exception>
    /// <exception cref="OverflowException">
    /// This value is not within the representable range of <typeparamref name="TTarget"/>.
    /// </exception>
    public bool TryCreateChecked<TTarget>(out TTarget result)
    {
        if (typeof(TTarget) == typeof(byte))
        {
            result = (TTarget)(object)(byte)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(char))
        {
            result = (TTarget)(object)(char)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(decimal))
        {
            result = (TTarget)(object)(decimal)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(double))
        {
            result = (TTarget)(object)(double)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(short))
        {
            result = (TTarget)(object)(short)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(int))
        {
            result = (TTarget)(object)(int)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(long))
        {
            result = (TTarget)(object)(long)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(nint))
        {
            result = (TTarget)(object)(nint)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(sbyte))
        {
            result = (TTarget)(object)(sbyte)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(float))
        {
            result = (TTarget)(object)(float)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(ushort))
        {
            result = (TTarget)(object)(ushort)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(uint))
        {
            result = (TTarget)(object)(uint)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(ulong))
        {
            result = (TTarget)(object)(ulong)this;
            return true;
        }
        else if (typeof(TTarget) == typeof(nuint))
        {
            result = (TTarget)(object)(nuint)this;
            return true;
        }
        else
        {
            result = default!;
            return false;
        }
    }

    /// <summary>
    /// Attempts to create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="result">
    /// If this method returns <see langword="true"/>, this will be set to a value of type
    /// <typeparamref name="TTarget"/> with the same value as this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion succeeded; otherwise <see langword="false"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// This value is <see cref="NaN"/> and <typeparamref name="TTarget"/> cannot represent NaN
    /// values.
    /// </exception>
    public bool TryCreateSaturating<TTarget>(out TTarget result)
    {
        if (typeof(TTarget) == typeof(byte))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(byte.MaxValue) > 0)
            {
                result = (TTarget)(object)byte.MaxValue;
            }
            else if (CompareTo(byte.MinValue) < 0)
            {
                result = (TTarget)(object)byte.MinValue;
            }
            else
            {
                result = (TTarget)(object)(byte)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(char))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(ushort.MaxValue) > 0)
            {
                result = (TTarget)(object)Convert.ToChar(ushort.MaxValue);
            }
            else if (CompareTo(ushort.MinValue) < 0)
            {
                result = (TTarget)(object)Convert.ToChar(ushort.MinValue);
            }
            else
            {
                result = (TTarget)(object)Convert.ToChar((ushort)Round().Mantissa);
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(decimal))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(decimal.MaxValue) > 0)
            {
                result = (TTarget)(object)decimal.MaxValue;
            }
            else if (CompareTo(decimal.MinValue) < 0)
            {
                result = (TTarget)(object)decimal.MinValue;
            }
            else
            {
                var v = (decimal)Mantissa / Denominator;
                for (var i = 0; i < Exponent; i++)
                {
                    v *= 10;
                }
                for (var i = 0; i > Exponent; i--)
                {
                    v /= 10;
                }
                result = (TTarget)(object)v;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(double))
        {
            if (IsNaN())
            {
                result = (TTarget)(object)double.NaN;
            }
            else if (IsPositiveInfinity() || CompareTo(double.MaxValue) > 0)
            {
                result = (TTarget)(object)double.PositiveInfinity;
            }
            else if (IsNegativeInfinity() || CompareTo(double.MinValue) < 0)
            {
                result = (TTarget)(object)double.NegativeInfinity;
            }
            else
            {
                result = (TTarget)(object)((double)Mantissa / Denominator * Math.Pow(10, Exponent));
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(short))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(short.MaxValue) > 0)
            {
                result = (TTarget)(object)short.MaxValue;
            }
            else if (CompareTo(short.MinValue) < 0)
            {
                result = (TTarget)(object)short.MinValue;
            }
            else
            {
                result = (TTarget)(object)(short)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(int))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(int.MaxValue) > 0)
            {
                result = (TTarget)(object)int.MaxValue;
            }
            else if (CompareTo(int.MinValue) < 0)
            {
                result = (TTarget)(object)int.MinValue;
            }
            else
            {
                result = (TTarget)(object)(int)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(long))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(long.MaxValue) > 0)
            {
                result = (TTarget)(object)long.MaxValue;
            }
            else if (CompareTo(long.MinValue) < 0)
            {
                result = (TTarget)(object)long.MinValue;
            }
            else
            {
                result = (TTarget)(object)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(nint))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(nint.MaxValue) > 0)
            {
                result = (TTarget)(object)nint.MaxValue;
            }
            else if (CompareTo(nint.MinValue) < 0)
            {
                result = (TTarget)(object)nint.MinValue;
            }
            else
            {
                result = (TTarget)(object)(nint)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(sbyte))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(sbyte.MaxValue) > 0)
            {
                result = (TTarget)(object)sbyte.MaxValue;
            }
            else if (CompareTo(sbyte.MinValue) < 0)
            {
                result = (TTarget)(object)sbyte.MinValue;
            }
            else
            {
                result = (TTarget)(object)(sbyte)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(float))
        {
            if (IsNaN())
            {
                result = (TTarget)(object)float.NaN;
            }
            else if (IsPositiveInfinity() || CompareTo(float.MaxValue) > 0)
            {
                result = (TTarget)(object)float.PositiveInfinity;
            }
            else if (IsNegativeInfinity() || CompareTo(float.MinValue) < 0)
            {
                result = (TTarget)(object)float.NegativeInfinity;
            }
            else
            {
                result = (TTarget)(object)(float)((double)Mantissa / Denominator * Math.Pow(10, Exponent));
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(ushort))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(ushort.MaxValue) > 0)
            {
                result = (TTarget)(object)ushort.MaxValue;
            }
            else if (CompareTo(ushort.MinValue) < 0)
            {
                result = (TTarget)(object)ushort.MinValue;
            }
            else
            {
                result = (TTarget)(object)(ushort)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(uint))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(uint.MaxValue) > 0)
            {
                result = (TTarget)(object)uint.MaxValue;
            }
            else if (CompareTo(uint.MinValue) < 0)
            {
                result = (TTarget)(object)uint.MinValue;
            }
            else
            {
                result = (TTarget)(object)(uint)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(ulong))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(ulong.MaxValue) > 0)
            {
                result = (TTarget)(object)ulong.MaxValue;
            }
            else if (CompareTo(ulong.MinValue) < 0)
            {
                result = (TTarget)(object)ulong.MinValue;
            }
            else
            {
                result = (TTarget)(object)(ulong)Round().Mantissa;
            }
            return true;
        }
        else if (typeof(TTarget) == typeof(nuint))
        {
            if (IsNaN())
            {
                throw new InvalidOperationException(HugeNumberErrorMessages.NaNConversion);
            }
            else if (CompareTo(nuint.MaxValue) > 0)
            {
                result = (TTarget)(object)nuint.MaxValue;
            }
            else if (CompareTo(nuint.MinValue) < 0)
            {
                result = (TTarget)(object)nuint.MinValue;
            }
            else
            {
                result = (TTarget)(object)(nuint)Round().Mantissa;
            }
            return true;
        }
        else
        {
            result = default!;
            return false;
        }
    }

    /// <summary>
    /// Attempts to create a new instance of <typeparamref name="TTarget"/> from this instance.
    /// </summary>
    /// <typeparam name="TTarget">The type to create.</typeparam>
    /// <param name="result">
    /// If this method returns <see langword="true"/>, this will be set to a value of type
    /// <typeparamref name="TTarget"/> with the same value as this instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the conversion succeeded; otherwise <see langword="false"/>.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// This value is <see cref="NaN"/> and <typeparamref name="TTarget"/> cannot represent NaN
    /// values.
    /// </exception>
    public bool TryCreateTrancating<TTarget>(out TTarget result) => TryCreateSaturating(out result);
}
