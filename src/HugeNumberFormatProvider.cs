using System;
using System.Globalization;
using System.Text;

namespace Tavenem.HugeNumbers
{
    /// <summary>
    /// A custom formatter and format provider for <see cref="HugeNumber"/>.
    /// </summary>
    public class HugeNumberFormatProvider : IFormatProvider, ICustomFormatter
    {
        /// <summary>
        /// A static instance of <see cref="HugeNumberFormatProvider"/>.
        /// </summary>
        public static readonly HugeNumberFormatProvider Instance = new();

        private static readonly char[] _AllowedFormatChars = new char[] { 'c', 'C', 'd', 'D', 'e', 'E', 'f', 'F', 'g', 'G', 'n', 'N', 'p', 'P', 'r', 'R' };

        /// <summary>Converts the value of a specified object to an equivalent string representation
        /// using specified format and culture-specific formatting information.</summary>
        /// <param name="format">A format string containing formatting specifications.</param>
        /// <param name="arg">An object to format.</param>
        /// <param name="formatProvider">An object that supplies format information about the
        /// current instance.</param>
        /// <returns>The string representation of the value of <paramref name="arg">arg</paramref>,
        /// formatted as specified by <paramref name="format">format</paramref> and <paramref
        /// name="formatProvider">formatProvider</paramref>.</returns>
        public string Format(string? format, object? arg, IFormatProvider? formatProvider)
        {
            var provider = NumberFormatInfo.GetInstance(formatProvider ?? NumberFormatInfo.CurrentInfo);

            if (arg?.GetType() != typeof(HugeNumber))
            {
                try
                {
                    return HandleOtherFormats(format, arg);
                }
                catch (FormatException e)
                {
                    throw new FormatException($"The format of {format} is invalid.", e);
                }
            }

            var number = (HugeNumber)arg;

            if (number.IsNaN)
            {
                return provider.NaNSymbol;
            }
            else if (number.IsPositiveInfinity)
            {
                return provider.PositiveInfinitySymbol;
            }
            else if (number.IsNegativeInfinity)
            {
                return provider.NegativeInfinitySymbol;
            }

            char formatChar;
            int? precision = null;
            if (string.IsNullOrWhiteSpace(format))
            {
                formatChar = 'g';
            }
            else
            {
                formatChar = format[0];
                if (Array.IndexOf(_AllowedFormatChars, formatChar) == -1)
                {
                    try
                    {
                        return HandleOtherFormats(format, arg);
                    }
                    catch (FormatException e)
                    {
                        throw new FormatException($"The format of {format} is invalid.", e);
                    }
                }
                if (format.Length > 1)
                {
                    if (!int.TryParse(format[1..], out var specificPrecision))
                    {
                        try
                        {
                            return HandleOtherFormats(format, arg);
                        }
                        catch (FormatException e)
                        {
                            throw new FormatException($"The format of {format} is invalid.", e);
                        }
                    }
                    else
                    {
                        precision = specificPrecision;
                    }
                }
            }
            var formatCharLower = formatChar.ToString().ToLower();

            if (formatCharLower == "p")
            {
                number *= 100;
            }
            // Re-check infinities, in case the multiplication caused an overflow.
            if (number.IsPositiveInfinity)
            {
                return provider.PositiveInfinitySymbol;
            }
            else if (number.IsNegativeInfinity)
            {
                return provider.NegativeInfinitySymbol;
            }

            if (!precision.HasValue)
            {
                precision = formatCharLower switch
                {
                    "c" => provider.CurrencyDecimalDigits,
                    "d" => 0,
                    "e" => 6,
                    "f" => provider.NumberDecimalDigits,
                    "n" => provider.NumberDecimalDigits,
                    "p" => provider.PercentDecimalDigits,
                    _ => (int?)null,
                };
            }

            return Format(formatCharLower, !formatChar.ToString().Equals(formatCharLower), precision, provider, number);
        }

        /// <summary>Returns an object that provides formatting services for the specified
        /// type.</summary>
        /// <param name="formatType">An object that specifies the type of format object to
        /// return.</param>
        /// <returns>An instance of the object specified by <paramref
        /// name="formatType">formatType</paramref>, if the <see cref="IFormatProvider"></see>
        /// implementation can supply that type of object; otherwise, <see
        /// langword="null"/>.</returns>
        public object? GetFormat(Type? formatType)
            => typeof(ICustomFormatter).IsAssignableFrom(formatType) ? this : null;

        private static string Format(string formatCharLower, bool capitalize, int? precision, NumberFormatInfo provider, HugeNumber number)
        {
            var sb = new StringBuilder();

            switch (formatCharLower)
            {
                case "c":
                    if (number.IsNegative)
                    {
                        number = -number;
                        switch (provider.CurrencyNegativePattern)
                        {
                            case 0:
                                sb.Append('(').Append(provider.CurrencySymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(')');
                                break;
                            case 1:
                                sb.Append(provider.NegativeSign).Append(provider.CurrencySymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                break;
                            case 2:
                                sb.Append(provider.CurrencySymbol).Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                break;
                            case 3:
                                sb.Append(provider.CurrencySymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign);
                                break;
                            case 4:
                                sb.Append('(');
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.CurrencySymbol).Append(')');
                                break;
                            case 5:
                                sb.Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.CurrencySymbol);
                                break;
                            case 6:
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign).Append(provider.CurrencySymbol);
                                break;
                            case 7:
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.CurrencySymbol).Append(provider.NegativeSign);
                                break;
                            case 8:
                                sb.Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.CurrencySymbol);
                                break;
                            case 9:
                                sb.Append(provider.NegativeSign).Append(provider.CurrencySymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                break;
                            case 10:
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.CurrencySymbol).Append(provider.NegativeSign);
                                break;
                            case 11:
                                sb.Append(provider.CurrencySymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign);
                                break;
                            case 12:
                                sb.Append(provider.CurrencySymbol).Append(' ').Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                break;
                            case 13:
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign).Append(' ').Append(provider.CurrencySymbol);
                                break;
                            case 14:
                                sb.Append('(').Append(provider.CurrencySymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(')');
                                break;
                            case 15:
                                sb.Append('(');
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.CurrencySymbol).Append(')');
                                break;
                        }
                    }
                    else
                    {
                        switch (provider.CurrencyPositivePattern)
                        {
                            case 0:
                                sb.Append(provider.CurrencySymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                break;
                            case 1:
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(provider.CurrencySymbol);
                                break;
                            case 2:
                                sb.Append(provider.CurrencySymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                break;
                            case 3:
                                Format(sb, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.CurrencySymbol);
                                break;
                        }
                    }
                    break;
                case "d":
                case "f":
                case "r":
                    if (number.IsNegative)
                    {
                        number = -number;
                        sb.Append(provider.NegativeSign);
                    }
                    Format(sb, capitalize, false, false, precision, false, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                    break;
                case "e":
                    if (number.IsNegative)
                    {
                        number = -number;
                        sb.Append(provider.NegativeSign);
                    }
                    Format(sb, capitalize, true, false, precision, false, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, true, number);
                    break;
                case "n":
                    if (number.IsNegative)
                    {
                        number = -number;
                        switch (provider.NumberNegativePattern)
                        {
                            case 0:
                                sb.Append('(');
                                Format(sb, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                                sb.Append(')');
                                break;
                            case 1:
                                sb.Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                                break;
                            case 2:
                                sb.Append(provider.NegativeSign).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                                break;
                            case 3:
                                Format(sb, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign);
                                break;
                            case 4:
                                Format(sb, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.NegativeSign);
                                break;
                        }
                    }
                    else
                    {
                        Format(sb, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                    }
                    break;
                case "p":
                    if (number.IsNegative)
                    {
                        number = -number;
                        switch (provider.PercentNegativePattern)
                        {
                            case 0:
                                sb.Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.PercentSymbol);
                                break;
                            case 1:
                                sb.Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.PercentSymbol);
                                break;
                            case 2:
                                sb.Append(provider.NegativeSign).Append(provider.PercentSymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                break;
                            case 3:
                                sb.Append(provider.PercentSymbol).Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                break;
                            case 4:
                                sb.Append(provider.PercentSymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign);
                                break;
                            case 5:
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign).Append(provider.PercentSymbol);
                                break;
                            case 6:
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.PercentSymbol).Append(provider.NegativeSign);
                                break;
                            case 7:
                                sb.Append(provider.NegativeSign).Append(provider.PercentSymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                break;
                            case 8:
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.PercentSymbol).Append(provider.NegativeSign);
                                break;
                            case 9:
                                sb.Append(provider.PercentSymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign);
                                break;
                            case 10:
                                sb.Append(provider.PercentSymbol).Append(' ').Append(provider.NegativeSign);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                break;
                            case 11:
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.NegativeSign).Append(' ').Append(provider.PercentSymbol);
                                break;
                        }
                    }
                    else
                    {
                        switch (provider.PercentPositivePattern)
                        {
                            case 0:
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(' ').Append(provider.PercentSymbol);
                                break;
                            case 1:
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                sb.Append(provider.PercentSymbol);
                                break;
                            case 2:
                                sb.Append(provider.PercentSymbol);
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                break;
                            case 3:
                                sb.Append(provider.PercentSymbol).Append(' ');
                                Format(sb, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, number);
                                break;
                        }
                    }
                    break;
                default:
                    if (number.IsNegative)
                    {
                        number = -number;
                        sb.Append(provider.NegativeSign);
                    }
                    Format(sb, capitalize, false, true, precision, false, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, number);
                    break;
            }

            return sb.ToString();
        }

        private static void Format(
            StringBuilder sb,
            bool capitalize,
            bool condense,
            bool optionalCondense,
            int? precision,
            bool groupSeparators,
            string groupSeparator,
            int[] groupSizes,
            string decimalSeparator,
            bool alwaysPositiveExponent,
            HugeNumber number)
        {
            var mantissa = number.Mantissa;
            int exponent = number.Exponent;

            var rawMantissaSpan = mantissa.ToString("d").AsSpan();

            // All formats use exponential notation (even those which do not ordinarily do so) when
            // the unmodified exponent is greater than zero, or less than -18. These values cannot
            // be reasonably expressed without exponents (doing so would involve printing an
            // arbitrarily long sequence of leading or trailing zeros).
            //
            // For example: 123000000000000000e1 becomes 1.23e18, and 123e-19 becomes 1.23e-17.
            //
            // Optional condensing ("g") formats use exponential notation if there are at least
            // three leading zeros, or 4 trailing zeroes.
            //
            // For example: 123e-5 becomes 1.23e-3, and 10000e0 becomes 1e4.
            //
            // Optional condensing uses fixed-point notation when there is a negative unmodified
            // exponent which becomes zero after adjusting the mantissa.
            //
            // For example: 123e-1 becomes 12.3.
            //
            // Other values show the mantissa with no exponent, adjusting it if necessary.
            //
            // For example: 12345e0 becomes 12345.
            ReadOnlySpan<char> mantissaSpan;
            if ((condense && rawMantissaSpan.Length > 1)
                || exponent > 0
                || exponent < -18
                || (optionalCondense
                && (exponent + rawMantissaSpan.Length <= -2
                || rawMantissaSpan.Length - rawMantissaSpan.TrimEnd('0').Length >= 4)))
            {
                exponent += rawMantissaSpan.Length - 1;
                mantissaSpan = new StringBuilder()
                    .Append(rawMantissaSpan[0])
                    .Append(decimalSeparator)
                    .Append(rawMantissaSpan[1..])
                    .ToString()
                    .AsSpan()
                    .TrimEnd('0');
                if (mantissaSpan.EndsWith(decimalSeparator))
                {
                    mantissaSpan = mantissaSpan.Slice(0, mantissaSpan.Length - decimalSeparator.Length);
                }
                else if (precision.HasValue)
                {
                    var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                    if (mantissaSpan.Length - sepIndex - decimalSeparator.Length > precision.Value)
                    {
                        var nextDigit = precision.Value <= 0
                            ? int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length, 1))
                            : int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length + precision.Value, 1));
                        mantissaSpan = precision.Value <= 0
                            ? mantissaSpan.Slice(0, sepIndex)
                            : mantissaSpan.Slice(0, sepIndex + decimalSeparator.Length + precision.Value);
                        if (nextDigit >= 5)
                        {
                            mantissaSpan = new StringBuilder()
                                .Append(mantissaSpan[0..^1])
                                .Append(int.Parse(mantissaSpan[^1..]) + 1)
                                .ToString();
                        }
                    }
                }
            }
            else if (optionalCondense && exponent < 0)
            {
                var insertPoint = rawMantissaSpan.Length + exponent;
                mantissaSpan = insertPoint == 0
                    ? new StringBuilder("0")
                        .Append(decimalSeparator)
                        .Append(rawMantissaSpan)
                        .ToString()
                        .AsSpan()
                        .TrimEnd('0')
                    : new StringBuilder()
                        .Append(rawMantissaSpan.Slice(0, rawMantissaSpan.Length + exponent))
                        .Append(decimalSeparator)
                        .Append(rawMantissaSpan[(rawMantissaSpan.Length + exponent)..])
                        .ToString()
                        .AsSpan()
                        .TrimEnd('0');
                if (mantissaSpan.EndsWith(decimalSeparator))
                {
                    mantissaSpan = mantissaSpan.Slice(0, mantissaSpan.Length - decimalSeparator.Length);
                }
                else if (precision.HasValue)
                {
                    var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                    if (mantissaSpan.Length - sepIndex - decimalSeparator.Length > precision.Value)
                    {
                        var nextDigit = precision.Value <= 0
                            ? int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length, 1))
                            : int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length + precision.Value, 1));
                        mantissaSpan = precision.Value <= 0
                            ? mantissaSpan.Slice(0, sepIndex)
                            : mantissaSpan.Slice(0, sepIndex + decimalSeparator.Length + precision.Value);
                        if (nextDigit >= 5)
                        {
                            mantissaSpan = new StringBuilder()
                                .Append(mantissaSpan[0..^1])
                                .Append(int.Parse(mantissaSpan[^1..]) + 1)
                                .ToString();
                        }
                    }
                }
                exponent = 0;
            }
            else if (exponent < 0)
            {
                if (Math.Abs(exponent) >= rawMantissaSpan.Length)
                {
                    var mantissaStr = new StringBuilder("0")
                        .Append(decimalSeparator)
                        .Append('0', Math.Abs(exponent) - rawMantissaSpan.Length)
                        .Append(rawMantissaSpan);
                    if (precision < mantissaStr.Length - 2)
                    {
                        var nextDigit = precision.Value <= 0
                            ? int.Parse(mantissaStr[decimalSeparator.Length + 1].ToString())
                            : int.Parse(mantissaStr[precision.Value + decimalSeparator.Length + 1].ToString());
                        if (precision.Value <= 0)
                        {
                            mantissaStr.Remove(1, mantissaStr.Length - 1);
                        }
                        else
                        {
                            mantissaStr.Remove(precision.Value + 2, mantissaStr.Length - precision.Value - 2);
                        }
                        if (nextDigit >= 5)
                        {
                            var lastDigit = int.Parse(new ReadOnlySpan<char>(new[] { mantissaStr[^1] }));
                            mantissaStr.Remove(mantissaStr.Length - 1, 1);
                            mantissaStr.Append(lastDigit + 1);
                        }
                    }
                    mantissaSpan = mantissaStr.ToString();
                }
                else
                {
                    mantissaSpan = new StringBuilder()
                        .Append(rawMantissaSpan.Slice(0, rawMantissaSpan.Length + exponent))
                        .Append(decimalSeparator)
                        .Append(rawMantissaSpan[(rawMantissaSpan.Length + exponent)..])
                        .ToString()
                        .AsSpan()
                        .TrimEnd('0');
                    if (mantissaSpan.EndsWith(decimalSeparator))
                    {
                        mantissaSpan = mantissaSpan.Slice(0, mantissaSpan.Length - decimalSeparator.Length);
                    }
                    else if (precision.HasValue)
                    {
                        var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                        if (mantissaSpan.Length - sepIndex - decimalSeparator.Length > precision.Value)
                        {
                            var nextDigit = precision.Value <= 0
                                ? int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length, 1))
                                : int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length + precision.Value, 1));
                            mantissaSpan = precision.Value <= 0
                                ? mantissaSpan.Slice(0, sepIndex)
                                : mantissaSpan.Slice(0, sepIndex + decimalSeparator.Length + precision.Value);
                            if (nextDigit >= 5)
                            {
                                mantissaSpan = new StringBuilder()
                                    .Append(mantissaSpan[0..^1])
                                    .Append(int.Parse(mantissaSpan[^1..]) + 1)
                                    .ToString();
                            }
                        }
                    }
                    if (groupSeparators)
                    {
                        var digits = new StringBuilder();
                        var groupIndex = 0;
                        var groupCount = 0;
                        var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                        for (var i = mantissaSpan.Length - 1; i >= 0; i--)
                        {
                            digits.Insert(0, mantissaSpan[i]);

                            if (sepIndex != -1 && i >= sepIndex)
                            {
                                continue;
                            }

                            groupCount++;
                            if (groupCount == groupSizes[groupIndex]
                                && i > 0)
                            {
                                digits.Insert(0, groupSeparator);
                                groupCount = 0;
                                if (groupIndex < groupSizes.Length - 1)
                                {
                                    groupIndex++;
                                }
                            }
                        }
                        mantissaSpan = digits.ToString();
                    }
                }
                exponent = 0;
            }
            else if (exponent > 0)
            {
                var mantissaStr = new StringBuilder()
                    .Append(rawMantissaSpan)
                    .Append('0', exponent);
                if (groupSeparators)
                {
                    var digits = new StringBuilder();
                    var groupIndex = 0;
                    var groupCount = 0;
                    for (var i = mantissaStr.Length - 1; i >= 0; i--)
                    {
                        digits.Insert(0, mantissaStr[i]);

                        groupCount++;
                        if (groupCount == groupSizes[groupIndex]
                            && i > 0)
                        {
                            digits.Insert(0, groupSeparator);
                            groupCount = 0;
                            if (groupIndex < groupSizes.Length - 1)
                            {
                                groupIndex++;
                            }
                        }
                    }
                    mantissaSpan = digits.ToString();
                }
                else
                {
                    mantissaSpan = mantissaStr.ToString();
                }
                exponent = 0;
            }
            else if (precision.HasValue)
            {
                mantissaSpan = condense
                    ? rawMantissaSpan
                    : mantissa.ToString(groupSeparators ? $"n{precision.Value}" : $"f{precision.Value}");
            }
            else if (condense || !groupSeparators)
            {
                mantissaSpan = rawMantissaSpan;
            }
            else
            {
                mantissaSpan = mantissa.ToString("n");
            }

            var exponentString = exponent.ToString("d");

            sb.Append(mantissaSpan);

            if (precision.HasValue)
            {
                if (mantissaSpan.IndexOf(decimalSeparator) != -1)
                {
                    var index = mantissaSpan.IndexOf(decimalSeparator);
                    var precisionLength = mantissaSpan.Length - index - decimalSeparator.Length;
                    if (precisionLength < precision.Value)
                    {
                        sb.Append('0', precision.Value - precisionLength);
                    }
                }
                else if (precision.Value - exponentString.Length - mantissaSpan.Length > 0)
                {
                    sb.Append(decimalSeparator);
                    sb.Append('0', precision.Value - exponentString.Length - mantissaSpan.Length);
                }
            }

            if (exponent != 0)
            {
                sb.Append(capitalize ? "E" : "e");

                if (exponent >= 0 && alwaysPositiveExponent)
                {
                    sb.Append(NumberFormatInfo.CurrentInfo.PositiveSign);
                }

                sb.Append(exponentString);
            }
        }

        private static string HandleOtherFormats(string? format, object? arg)
        {
            if (arg is IFormattable formattable)
            {
                return formattable.ToString(format, CultureInfo.CurrentCulture);
            }
            else if (arg != null)
            {
                return arg.ToString() ?? string.Empty;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
