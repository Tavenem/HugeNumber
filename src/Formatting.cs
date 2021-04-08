using System;
using System.Globalization;

namespace Tavenem.HugeNumbers
{
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay}")]
    public partial struct HugeNumber : IFormattable
    {
        private const string DefaultFormat = "g";

        private string DebugDisplay
        {
            get
            {
                if (IsNaN)
                {
                    return "NaN";
                }
                else if (IsInfinite)
                {
                    if (IsNegativeInfinity)
                    {
                        return "-∞";
                    }
                    else
                    {
                        return "∞";
                    }
                }
                else if (IsZero)
                {
                    return "0";
                }
                else if (Exponent == 0)
                {
                    return Mantissa.ToString();
                }
                else if (MantissaDigits == 1)
                {
                    return $"{Mantissa}e{Exponent}";
                }
                else
                {
                    return $"{Mantissa / Math.Pow(10, MantissaDigits - 1)}e{Exponent + (MantissaDigits - 1)}";
                }
            }
        }

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
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>.
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
        public static HugeNumber Parse(string? value, NumberStyles style, IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value))
            {
                return NaN;
            }
            else if (TryParse(value, style, provider, out var result))
            {
                return result;
            }
            else
            {
                return NaN;
            }
        }

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
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>.
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
        public static HugeNumber Parse(in ReadOnlySpan<char> value, NumberStyles style, IFormatProvider provider)
        {
            if (TryParse(value, style, provider, out var result))
            {
                return result;
            }
            else
            {
                return NaN;
            }
        }

        /// <summary>
        /// Converts the string representation of a number in a specified culture-specific format to
        /// its <see cref="HugeNumber"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="provider">
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>.
        /// </param>
        /// <returns>A value that is equivalent to the number specified in the value
        /// parameter.</returns>
        /// <remarks>
        /// Null strings, or those which are not in the correct format, result in a return value of
        /// <see cref="NaN"/>.
        /// </remarks>
        public static HugeNumber Parse(string? value, IFormatProvider provider) => Parse(value, NumberStyles.Float | NumberStyles.Any, provider);

        /// <summary>
        /// Converts the string representation of a number in a specified culture-specific format to
        /// its <see cref="HugeNumber"/> equivalent.
        /// </summary>
        /// <param name="value">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> that contains
        /// a number to convert.</param>
        /// <param name="provider">
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>.
        /// </param>
        /// <returns>A value that is equivalent to the number specified in the value
        /// parameter.</returns>
        /// <remarks>
        /// Null strings, or those which are not in the correct format, result in a return value of
        /// <see cref="NaN"/>.
        /// </remarks>
        public static HugeNumber Parse(in ReadOnlySpan<char> value, IFormatProvider provider) => Parse(value, NumberStyles.Float | NumberStyles.Any, provider);

        /// <summary>
        /// Converts the string representation of a number in a specified style to its <see
        /// cref="HugeNumber"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <param name="style">
        /// A bitwise combination of the enumeration values that specify the permitted format of
        /// <paramref name="value"/>.
        /// </param>
        /// <returns>A value that is equivalent to the number specified in the value
        /// parameter.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value. -or- <paramref
        /// name="style"/> includes the <see cref="NumberStyles.AllowHexSpecifier"/> or <see
        /// cref="NumberStyles.HexNumber"/> flag along with another value.
        /// </exception>
        /// <remarks>
        /// Null strings, or those which do not comply with the input pattern specified by <see
        /// cref="NumberStyles"/>, result in a return value of <see cref="NaN"/>.
        /// </remarks>
        public static HugeNumber Parse(string? value, NumberStyles style) => Parse(value, style, HugeNumberFormatProvider.Instance);

        /// <summary>
        /// Converts the string representation of a number in a specified style to its <see
        /// cref="HugeNumber"/> equivalent.
        /// </summary>
        /// <param name="value">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> that contains
        /// a number to convert.</param>
        /// <param name="style">
        /// A bitwise combination of the enumeration values that specify the permitted format of
        /// <paramref name="value"/>.
        /// </param>
        /// <returns>A value that is equivalent to the number specified in the value
        /// parameter.</returns>
        /// <exception cref="ArgumentException">
        /// <paramref name="style"/> is not a <see cref="NumberStyles"/> value. -or- <paramref
        /// name="style"/> includes the <see cref="NumberStyles.AllowHexSpecifier"/> or <see
        /// cref="NumberStyles.HexNumber"/> flag along with another value.
        /// </exception>
        /// <remarks>
        /// Null strings, or those which do not comply with the input pattern specified by <see
        /// cref="NumberStyles"/>, result in a return value of <see cref="NaN"/>.
        /// </remarks>
        public static HugeNumber Parse(in ReadOnlySpan<char> value, NumberStyles style) => Parse(value, style, HugeNumberFormatProvider.Instance);

        /// <summary>
        /// Converts the string representation of a number in a specified style to its <see
        /// cref="HugeNumber"/> equivalent.
        /// </summary>
        /// <param name="value">A string that contains a number to convert.</param>
        /// <returns>A value that is equivalent to the number specified in the value
        /// parameter.</returns>
        /// <remarks>
        /// Null strings, or those which are not in the correct format, result in a return value of
        /// <see cref="NaN"/>.
        /// </remarks>
        public static HugeNumber Parse(string? value) => Parse(value, NumberStyles.Float | NumberStyles.Any, HugeNumberFormatProvider.Instance);

        /// <summary>
        /// Converts the string representation of a number in a specified style to its <see
        /// cref="HugeNumber"/> equivalent.
        /// </summary>
        /// <param name="value">A <see cref="ReadOnlySpan{T}"/> of <see cref="char"/> that contains
        /// a number to convert.</param>
        /// <returns>A value that is equivalent to the number specified in the value
        /// parameter.</returns>
        /// <remarks>
        /// Null strings, or those which are not in the correct format, result in a return value of
        /// <see cref="NaN"/>.
        /// </remarks>
        public static HugeNumber Parse(in ReadOnlySpan<char> value) => Parse(value, NumberStyles.Float | NumberStyles.Any, HugeNumberFormatProvider.Instance);

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
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>. A <see cref="NumberFormatInfo"/> instance for the desired culture is
        /// expected.
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
        public static bool TryParse(in ReadOnlySpan<char> value, NumberStyles style, IFormatProvider provider, out HugeNumber result)
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
                && val.StartsWith(new ReadOnlySpan<char>(new char[] { '(' })) && val.EndsWith(new ReadOnlySpan<char>(new char[] { ')' })))
            {
                isNegative = true;
                val = val[1..];
                val = val[0..^1];
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

            long mantissa;
            if ((style & NumberStyles.AllowExponent) != 0)
            {
                var eIndex = val.IndexOfAny('e', 'E');
                if (eIndex == -1)
                {
                    var exponent = 0;

                    if (!long.TryParse(val.ToString(), PermittedStyles, provider, out mantissa))
                    {
                        var decimalIndex = val.IndexOf(isCurrency ? numberFormatInfo.CurrencyDecimalSeparator : numberFormatInfo.NumberDecimalSeparator);
                        if (decimalIndex == -1)
                        {
                            return false;
                        }
                        var firstPart = val.Slice(0, decimalIndex);
                        var secondpart = val[(decimalIndex + 1)..];
                        if (!long.TryParse(firstPart.ToString() + secondpart.ToString(), PermittedStyles, provider, out mantissa))
                        {
                            return false;
                        }
                        exponent -= secondpart.Length;
                    }

                    result = new HugeNumber(mantissa, exponent);
                    if (isNegative && result.IsPositive)
                    {
                        result *= -1;
                    }
                    return true;
                }
                else
                {
                    var exponent = 0;

                    var rest = val[(eIndex + 1)..];
                    if (rest.Length > 0 && !int.TryParse(rest.ToString(), PermittedStyles, provider, out exponent))
                    {
                        return false;
                    }

                    var mantissaSlice = val.Slice(0, eIndex);
                    if (!long.TryParse(mantissaSlice.ToString(), PermittedStyles, provider, out mantissa))
                    {
                        var decimalIndex = mantissaSlice.IndexOf(isCurrency ? numberFormatInfo.CurrencyDecimalSeparator : numberFormatInfo.NumberDecimalSeparator);
                        if (decimalIndex == -1)
                        {
                            return false;
                        }
                        var firstPart = mantissaSlice.Slice(0, decimalIndex);
                        var secondpart = mantissaSlice[(decimalIndex + 1)..];
                        if (!long.TryParse(firstPart.ToString() + secondpart.ToString(), PermittedStyles, provider, out mantissa))
                        {
                            return false;
                        }
                        exponent -= secondpart.Length;
                    }

                    result = new HugeNumber(mantissa, exponent);
                    if (isNegative && result.IsPositive)
                    {
                        result *= -1;
                    }
                    return true;
                }
            }
            else
            {
                if (!long.TryParse(val.ToString(), PermittedStyles, provider, out mantissa))
                {
                    return false;
                }
                result = new HugeNumber(mantissa);
                if (isNegative && result > 0)
                {
                    result *= -1;
                }
                return true;
            }
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
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>. A <see cref="NumberFormatInfo"/> instance for the desired culture is
        /// expected.
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
        public static bool TryParse(in string? value, NumberStyles style, IFormatProvider provider, out HugeNumber result)
            => TryParse(value.AsSpan(), style, provider, out result);

        /// <summary>
        /// Tries to convert the string representation of a number in a specified style and
        /// culture-specific format to its <see cref="HugeNumber"/> equivalent, and returns a value that
        /// indicates whether the conversion succeeded.
        /// </summary>
        /// <param name="value">
        /// The string representation of a number.
        /// </param>
        /// <param name="provider">
        /// An object that provides culture-specific formatting information about <paramref
        /// name="value"/>. A <see cref="NumberFormatInfo"/> instance for the desired culture is
        /// expected.
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
        public static bool TryParse(in string? value, IFormatProvider provider, out HugeNumber result)
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
        public static bool TryParse(in ReadOnlySpan<char> value, out HugeNumber result)
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
        public static bool TryParse(in string? value, out HugeNumber result)
            => TryParse(value.AsSpan(), NumberStyles.Float | NumberStyles.Any, HugeNumberFormatProvider.Instance, out result);

        /// <summary>
        /// Converts the value of the current <see cref="HugeNumber"/> object to its equivalent string
        /// representation by using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <param name="formatProvider">
        /// An object that supplies culture-specific formatting information.
        /// </param>
        /// <returns>
        /// The string representation of the current <see cref="HugeNumber"/> value in the format
        /// specified by the <paramref name="format"/> and <paramref name="formatProvider"/>
        /// parameters.
        /// </returns>
        /// <exception cref="FormatException">
        /// <paramref name="format"/> is not a valid format string, or <paramref
        /// name="formatProvider"/> is not a valid provider for <see cref="HugeNumber"/>.
        /// </exception>
        public string ToString(string? format, IFormatProvider? formatProvider) => HugeNumberFormatProvider.Instance.Format(format, this, formatProvider);

        /// <summary>
        /// Converts the value of the current <see cref="HugeNumber"/> object to its equivalent string
        /// representation by using the specified culture-specific format information.
        /// </summary>
        /// <param name="formatProvider">An object that supplies culture-specific formatting
        /// information.</param>
        /// <returns>
        /// The string representation of the current <see cref="HugeNumber"/> value in the format
        /// specified by the <paramref name="formatProvider"/>
        /// parameters.
        /// </returns>
        /// <exception cref="FormatException">
        /// <paramref name="formatProvider"/> is not a valid provider for <see cref="HugeNumber"/>.
        /// </exception>
        public string ToString(IFormatProvider? formatProvider) => HugeNumberFormatProvider.Instance.Format(DefaultFormat, this, formatProvider);

        /// <summary>
        /// Converts the value of the current <see cref="HugeNumber"/> object to its equivalent string
        /// representation by using the specified format.
        /// </summary>
        /// <param name="format">A standard or custom numeric format string.</param>
        /// <returns>
        /// The string representation of the current <see cref="HugeNumber"/> value in the format
        /// specified by the <paramref name="format"/> parameter.
        /// </returns>
        /// <exception cref="FormatException">
        /// <paramref name="format"/> is not a valid format string.
        /// </exception>
        public string ToString(string? format) => HugeNumberFormatProvider.Instance.Format(format, this, NumberFormatInfo.CurrentInfo);

        /// <summary>
        /// Converts the value of the current <see cref="HugeNumber"/> object to its equivalent string
        /// representation.
        /// </summary>
        /// <returns>
        /// The string representation of the current <see cref="HugeNumber"/> value.
        /// </returns>
        public override string ToString() => HugeNumberFormatProvider.Instance.Format(DefaultFormat, this, NumberFormatInfo.CurrentInfo);
    }
}
