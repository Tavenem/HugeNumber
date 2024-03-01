using System.Globalization;

namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Converts the string representation of a number in a specified style and culture-specific
    /// format to its <see cref="HugeNumber"/> equivalent.
    /// </summary>
    /// <param name="value">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> that contains
    /// a number to convert.</param>
    /// <param name="style">
    /// A bitwise combination of the enumeration values that specify the permitted format of
    /// <paramref name="value"/>.
    /// </param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <returns>A value that is equivalent to the number specified in the value
    /// parameter.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value. -or- <paramref
    /// name="style"/> includes the <see cref="NumberStyles.AllowHexSpecifier"/> or <see
    /// cref="NumberStyles.HexNumber"/> flag along with another value.
    /// </exception>
    /// <remarks>
    /// Null strings, or those which do not comply with the input pattern specified by <paramref
    /// name="style"/>, result in a return value of <see cref="NaN"/>.
    /// </remarks>
    public static HugeNumber Parse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider = null)
        => TryParse(value, style, provider, out var result) ? result : NaN;

    /// <summary>
    /// Converts the string representation of a number in a specified style and culture-specific
    /// format to its <see cref="HugeNumber"/> equivalent.
    /// </summary>
    /// <param name="value">A string that contains a number to convert.</param>
    /// <param name="style">
    /// A bitwise combination of the enumeration values that specify the permitted format of
    /// <paramref name="value"/>.
    /// </param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <returns>A value that is equivalent to the number specified in the value
    /// parameter.</returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value. -or- <paramref
    /// name="style"/> includes the <see cref="NumberStyles.AllowHexSpecifier"/> or <see
    /// cref="NumberStyles.HexNumber"/> flag along with another value.
    /// </exception>
    /// <remarks>
    /// Null strings, or those which do not comply with the input pattern specified by <paramref
    /// name="style"/>, result in a return value of <see cref="NaN"/>.
    /// </remarks>
    public static HugeNumber Parse(string? value, NumberStyles style, IFormatProvider? provider = null)
    {
        if (string.IsNullOrEmpty(value))
        {
            return NaN;
        }
        return TryParse(value, style, provider, out var result)
            ? result
            : NaN;
    }

    /// <summary>
    /// Converts the string representation of a number in a specified culture-specific format to
    /// its <see cref="HugeNumber"/> equivalent.
    /// </summary>
    /// <param name="value">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> that contains
    /// a number to convert.</param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <returns>A value that is equivalent to the number specified in the value
    /// parameter.</returns>
    /// <remarks>
    /// Null strings, or those which are not in the correct format, result in a return value of
    /// <see cref="NaN"/>.
    /// </remarks>
    public static HugeNumber Parse(ReadOnlySpan<char> value, IFormatProvider? provider = null)
        => Parse(value, NumberStyles.Float | NumberStyles.Any, provider);

    /// <summary>
    /// Converts the string representation of a number in a specified culture-specific format to
    /// its <see cref="HugeNumber"/> equivalent.
    /// </summary>
    /// <param name="value">A string that contains a number to convert.</param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <returns>A value that is equivalent to the number specified in the value
    /// parameter.</returns>
    /// <remarks>
    /// Null strings, or those which are not in the correct format, result in a return value of
    /// <see cref="NaN"/>.
    /// </remarks>
    public static HugeNumber Parse(string? value, IFormatProvider? provider = null)
        => Parse(value, NumberStyles.Float | NumberStyles.Any, provider);

    /// <summary>
    /// Tries to convert the string representation of a number in a specified style and
    /// culture-specific format to its <see cref="HugeNumber"/> equivalent, and returns a value that
    /// indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number. The string is interpreted using the style
    /// specified by <paramref name="style"/>.
    /// </param>
    /// <param name="style">
    /// A bitwise combination of the enumeration values that indicates the style elements that
    /// can be present in <paramref name="value"/>. Not all style flags are valid for <see
    /// cref="HugeNumber"/>.
    /// </param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// in a format that is compliant with <paramref name="style"/>. This parameter is passed
    /// uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value.
    /// </exception>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, IFormatProvider? provider, out HugeNumber result)
    {
        result = NaN;
        if (value.Length == 0)
        {
            return false;
        }

        var numberFormatInfo = NumberFormatInfo.GetInstance(provider);

        var val = value;
        if ((style & NumberStyles.AllowLeadingWhite) != 0)
        {
            val = val.TrimStart();
        }
        if ((style & NumberStyles.AllowTrailingWhite) != 0)
        {
            val = val.TrimEnd();
        }

        if (val.Equals(numberFormatInfo.NegativeInfinitySymbol, StringComparison.Ordinal))
        {
            result = NegativeInfinity;
            return true;
        }

        var isNegative = false;
        if ((style & NumberStyles.AllowParentheses) != 0
            && val.StartsWith(new ReadOnlySpan<char>(['('])) && val.EndsWith(new ReadOnlySpan<char>([')'])))
        {
            isNegative = true;
            val = val[1..^1];
        }

        if ((style & NumberStyles.AllowLeadingSign) != 0 && val.StartsWith(numberFormatInfo.NegativeSign))
        {
            isNegative = true;
            val = val[numberFormatInfo.NegativeSign.Length..].TrimStart();
        }
        else if ((style & NumberStyles.AllowTrailingSign) != 0 && val.EndsWith(numberFormatInfo.NegativeSign))
        {
            isNegative = true;
            val = val[..^numberFormatInfo.NegativeSign.Length].TrimEnd();
        }

        var isCurrency = false;
        if ((style & NumberStyles.AllowCurrencySymbol) != 0)
        {
            if (val.StartsWith(numberFormatInfo.CurrencySymbol))
            {
                isCurrency = true;
                val = val[numberFormatInfo.CurrencySymbol.Length..].TrimStart();
            }
            else if (val.EndsWith(numberFormatInfo.CurrencySymbol))
            {
                isCurrency = true;
                val = val[..^numberFormatInfo.CurrencySymbol.Length].TrimEnd();
            }
        }

        if (val.Equals(numberFormatInfo.NaNSymbol, StringComparison.Ordinal))
        {
            return true;
        }

        if (val.Equals(numberFormatInfo.PositiveInfinitySymbol, StringComparison.Ordinal))
        {
            result = isNegative ? NegativeInfinity : PositiveInfinity;
            return true;
        }

        const NumberStyles PermittedStyles = NumberStyles.AllowLeadingSign | NumberStyles.AllowTrailingSign | NumberStyles.AllowThousands;

        if (!TryParseValue(
            val,
            PermittedStyles,
            provider,
            numberFormatInfo,
            isCurrency,
            out var mantissa,
            out var denominator,
            out var exponent)
            || (exponent != 0
            && (style & NumberStyles.AllowExponent) == 0))
        {
            return false;
        }
        if (isNegative && mantissa > 0)
        {
            mantissa *= -1;
        }
        result = new HugeNumber(mantissa, denominator, exponent, true);
        return true;
    }

    /// <summary>
    /// Tries to convert the string representation of a number in a specified style and
    /// culture-specific format to its <see cref="HugeNumber"/> equivalent, and returns a value that
    /// indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number. The string is interpreted using the style
    /// specified by <paramref name="style"/>.
    /// </param>
    /// <param name="style">
    /// A bitwise combination of the enumeration values that indicates the style elements that
    /// can be present in <paramref name="value"/>. Not all style flags are valid for <see
    /// cref="HugeNumber"/>.
    /// </param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// in a format that is compliant with <paramref name="style"/>. This parameter is passed
    /// uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value.
    /// </exception>
    public static bool TryParse(string? value, NumberStyles style, IFormatProvider? provider, out HugeNumber result)
        => TryParse(value.AsSpan(), style, provider, out result);

    /// <summary>
    /// Tries to convert the string representation of a number in a specified style
    /// to its <see cref="HugeNumber"/> equivalent, and returns a value that
    /// indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number. The string is interpreted using the style
    /// specified by <paramref name="style"/>.
    /// </param>
    /// <param name="style">
    /// A bitwise combination of the enumeration values that indicates the style elements that
    /// can be present in <paramref name="value"/>. Not all style flags are valid for <see
    /// cref="HugeNumber"/>.
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// in a format that is compliant with <paramref name="style"/>. This parameter is passed
    /// uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value.
    /// </exception>
    public static bool TryParse(ReadOnlySpan<char> value, NumberStyles style, out HugeNumber result)
        => TryParse(value, style, null, out result);

    /// <summary>
    /// Tries to convert the string representation of a number in a specified style
    /// to its <see cref="HugeNumber"/> equivalent, and returns a value that
    /// indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number. The string is interpreted using the style
    /// specified by <paramref name="style"/>.
    /// </param>
    /// <param name="style">
    /// A bitwise combination of the enumeration values that indicates the style elements that
    /// can be present in <paramref name="value"/>. Not all style flags are valid for <see
    /// cref="HugeNumber"/>.
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// in a format that is compliant with <paramref name="style"/>. This parameter is passed
    /// uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value.
    /// </exception>
    public static bool TryParse(string? value, NumberStyles style, out HugeNumber result)
        => TryParse(value.AsSpan(), style, null, out result);

    /// <summary>
    /// Tries to convert the string representation of a number in a specified style and
    /// culture-specific format to its <see cref="HugeNumber"/> equivalent, and returns a value that
    /// indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number.
    /// </param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// in a recognized format. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryParse(ReadOnlySpan<char> value, IFormatProvider? provider, out HugeNumber result)
        => TryParse(value, NumberStyles.Float | NumberStyles.Any, provider, out result);

    /// <summary>
    /// Tries to convert the string representation of a number in a specified style and
    /// culture-specific format to its <see cref="HugeNumber"/> equivalent, and returns a value that
    /// indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">
    /// The string representation of a number.
    /// </param>
    /// <param name="provider">
    /// <para>
    /// An object that provides culture-specific formatting information about <paramref
    /// name="value"/>.
    /// </para>
    /// <para>
    /// If omitted, the current culture info will be used.
    /// </para>
    /// </param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// in a recognized format. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryParse(string? value, IFormatProvider? provider, out HugeNumber result)
        => TryParse(value.AsSpan(), NumberStyles.Float | NumberStyles.Any, provider, out result);

    /// <summary>
    /// Tries to convert the string representation of a number to its <see cref="HugeNumber"/>
    /// equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">The string representation of a number.</param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// of the correct format. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryParse(ReadOnlySpan<char> value, out HugeNumber result)
        => TryParse(value, NumberStyles.Float | NumberStyles.Any, HugeNumberFormatProvider.Instance, out result);

    /// <summary>
    /// Tries to convert the string representation of a number to its <see cref="HugeNumber"/>
    /// equivalent, and returns a value that indicates whether the conversion succeeded.
    /// </summary>
    /// <param name="value">The string representation of a number.</param>
    /// <param name="result">
    /// When this method returns, contains the <see cref="HugeNumber"/> equivalent to the number
    /// that is contained in <paramref name="value"/>, or <see cref="NaN"/> if the conversion
    /// failed. The conversion fails if the <paramref name="value"/> parameter is null or is not
    /// of the correct format. This parameter is passed uninitialized.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="value"/> parameter was converted
    /// successfully; otherwise, <see langword="false"/>.
    /// </returns>
    public static bool TryParse(string? value, out HugeNumber result)
        => TryParse(value.AsSpan(), NumberStyles.Float | NumberStyles.Any, HugeNumberFormatProvider.Instance, out result);

    private static bool TryParseValue(
        ReadOnlySpan<char> value,
        NumberStyles permittedStyles,
        IFormatProvider? provider,
        NumberFormatInfo numberFormatInfo,
        bool isCurrency,
        out long mantissa,
        out ushort denominator,
        out short exponent)
    {
        mantissa = 0;
        denominator = 1;
        exponent = 0;

        var vinculumIndex = value.IndexOf('/');
        if (vinculumIndex != -1)
        {
            return TryParseValue(
                value[..vinculumIndex],
                permittedStyles,
                provider,
                numberFormatInfo,
                isCurrency,
                out mantissa,
                out var d,
                out exponent)
                && d == 1
                && ushort.TryParse(value[(vinculumIndex + 1)..], permittedStyles, provider, out denominator);
        }

        var eIndex = value.IndexOfAny('e', 'E');
        if (eIndex != -1)
        {
            var exponentPart = value[(eIndex + 1)..];
            if (exponentPart.Length == 0
                || !short.TryParse(exponentPart, permittedStyles, provider, out exponent)
                || !TryParseValue(
                    value[..eIndex],
                    permittedStyles,
                    provider,
                    numberFormatInfo,
                    isCurrency,
                    out mantissa,
                    out var d,
                    out var additionalExponent)
                || d != 1)
            {
                return false;
            }
            var newExponent = exponent + additionalExponent;
            if (newExponent is < short.MinValue or > short.MaxValue)
            {
                return false;
            }
            exponent = (short)newExponent;
            return true;
        }

        if (long.TryParse(value, permittedStyles, provider, out mantissa))
        {
            return true;
        }

        var decimalIndex = value.IndexOf(isCurrency
            ? numberFormatInfo.CurrencyDecimalSeparator
            : numberFormatInfo.NumberDecimalSeparator);
        if (decimalIndex == -1)
        {
            return false;
        }

        var firstPart = value[..decimalIndex];
        var secondpart = value[(decimalIndex + 1)..];
        var exp = -secondpart.Length;
        if (exp < short.MinValue
            || !long.TryParse(firstPart.ToString() + secondpart.ToString(), permittedStyles, provider, out mantissa))
        {
            return false;
        }

        exponent = (short)exp;
        return true;
    }
}
