using System.Globalization;
using System.Text;

namespace Tavenem.HugeNumbers;

/// <summary>
/// A custom formatter and format provider for <see cref="HugeNumber"/>.
/// </summary>
public class HugeNumberFormatProvider : IFormatProvider, ICustomFormatter
{
    /// <summary>
    /// A static instance of <see cref="HugeNumberFormatProvider"/>.
    /// </summary>
    public static readonly HugeNumberFormatProvider Instance = new();

    private static readonly char[] _AllowedFormatChars = ['c', 'C', 'd', 'D', 'e', 'E', 'f', 'F', 'g', 'G', 'n', 'N', 'p', 'P', 'r', 'R'];

    /// <summary>
    /// Attempts to write the given <paramref name="number"/> to the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="number">The <see cref="HugeNumber"/> to write.</param>
    /// <param name="destination">The <see cref="Span{T}"/> to write to.</param>
    /// <param name="charsWritten">
    /// When this method returns, this will contains the number of characters written to <paramref name="destination"/>.
    /// </param>
    /// <param name="format">A format string containing formatting specifications.</param>
    /// <param name="provider">
    /// An object that supplies format information about the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the <paramref name="number"/> was successfully written to the
    /// <paramref name="destination"/>; otherwise <see langword="false"/>.
    /// </returns>
    public static bool TryFormat(
        HugeNumber number,
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format,
        IFormatProvider? provider)
    {
        var numberFormatInfo = NumberFormatInfo.GetInstance(provider ?? NumberFormatInfo.CurrentInfo);

        charsWritten = 0;
        if (number.IsNaN())
        {
            if (destination.Length < numberFormatInfo.NaNSymbol.Length)
            {
                return false;
            }
            numberFormatInfo.NaNSymbol.CopyTo(destination);
            charsWritten += numberFormatInfo.NaNSymbol.Length;
            return true;
        }
        else if (number.IsPositiveInfinity())
        {
            if (destination.Length < numberFormatInfo.PositiveInfinitySymbol.Length)
            {
                return false;
            }
            numberFormatInfo.PositiveInfinitySymbol.CopyTo(destination);
            charsWritten += numberFormatInfo.PositiveInfinitySymbol.Length;
            return true;
        }
        else if (number.IsNegativeInfinity())
        {
            if (destination.Length < numberFormatInfo.NegativeInfinitySymbol.Length)
            {
                return false;
            }
            numberFormatInfo.NegativeInfinitySymbol.CopyTo(destination);
            charsWritten += numberFormatInfo.NegativeInfinitySymbol.Length;
            return true;
        }

        var sb = FormatInternal(number, format, numberFormatInfo);
        if (destination.Length < sb.Length)
        {
            return false;
        }
        sb.CopyTo(0, destination, sb.Length);
        charsWritten += sb.Length;
        return true;
    }

    /// <summary>Converts the value of a specified object to an equivalent string representation
    /// using specified format and culture-specific formatting information.</summary>
    /// <param name="format">A format string containing formatting specifications.</param>
    /// <param name="arg">An object to format.</param>
    /// <param name="formatProvider">
    /// An object that supplies format information about the current instance.
    /// </param>
    /// <returns>The string representation of the value of <paramref name="arg">arg</paramref>,
    /// formatted as specified by <paramref name="format">format</paramref> and <paramref
    /// name="formatProvider">formatProvider</paramref>.</returns>
    public string Format(string? format, object? arg, IFormatProvider? formatProvider)
    {
        var provider = NumberFormatInfo.GetInstance(formatProvider ?? NumberFormatInfo.CurrentInfo);

        if (arg is not HugeNumber number)
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

        if (number.IsNaN())
        {
            return provider.NaNSymbol;
        }
        else if (number.IsPositiveInfinity())
        {
            return provider.PositiveInfinitySymbol;
        }
        else if (number.IsNegativeInfinity())
        {
            return provider.NegativeInfinitySymbol;
        }

        return FormatInternal(number, format ?? string.Empty, provider)
            .ToString();
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

    private static void Format(
        StringBuilder builder,
        NumberFormatInfo provider,
        bool capitalize,
        bool condense,
        bool optionalCondense,
        int? precision,
        bool groupSeparators,
        string groupSeparator,
        int[] groupSizes,
        string decimalSeparator,
        bool alwaysPositiveExponent,
        bool isCurrency,
        HugeNumber number)
    {
        // Condensed and currency formats do not display rational fractions, and are always
        // converted to a decimal representation.
        if ((condense || isCurrency)
            && number.Denominator > 1)
        {
            number = HugeNumber.ToDenominator(number, 1);
        }

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
            mantissaSpan = new StringBuilder(rawMantissaSpan.Length + decimalSeparator.Length)
                .Append(rawMantissaSpan[0])
                .Append(decimalSeparator)
                .Append(rawMantissaSpan[1..])
                .TrimEnd('0')
                .TrimEnd(decimalSeparator, out var hadDecimalSeparator)
                .ToSpan();
            if (!hadDecimalSeparator && precision.HasValue)
            {
                var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                if (mantissaSpan.Length - sepIndex - decimalSeparator.Length > precision.Value)
                {
                    var nextDigit = precision.Value <= 0
                        ? int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length, 1))
                        : int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length + precision.Value, 1));
                    mantissaSpan = precision.Value <= 0
                        ? mantissaSpan[..sepIndex]
                        : mantissaSpan[..(sepIndex + decimalSeparator.Length + precision.Value)];
                    if (nextDigit >= 5)
                    {
                        mantissaSpan = new StringBuilder(mantissaSpan.Length)
                            .Append(mantissaSpan[..^1])
                            .Append(int.Parse(mantissaSpan[^1..]) + 1)
                            .ToSpan();
                    }
                }
            }
        }
        else if (optionalCondense && exponent < 0)
        {
            var insertPoint = rawMantissaSpan.Length + exponent;
            var sb = new StringBuilder();
            if (insertPoint <= 0)
            {
                sb.Append('0')
                    .Append(decimalSeparator);
                for (var i = 0; i < -exponent - rawMantissaSpan.Length; i++)
                {
                    sb.Append('0');
                }
                sb.Append(rawMantissaSpan);
            }
            else
            {
                sb.Append(rawMantissaSpan[..(rawMantissaSpan.Length + exponent)])
                    .Append(decimalSeparator)
                    .Append(rawMantissaSpan[(rawMantissaSpan.Length + exponent)..]);
            }
            mantissaSpan = sb
                .TrimEnd('0')
                .TrimEnd(decimalSeparator, out var hadDecimalSeparator)
                .ToSpan();
            if (!hadDecimalSeparator && precision.HasValue)
            {
                var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                if (mantissaSpan.Length - sepIndex - decimalSeparator.Length > precision.Value)
                {
                    var nextDigit = precision.Value <= 0
                        ? int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length, 1))
                        : int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length + precision.Value, 1));
                    mantissaSpan = precision.Value <= 0
                        ? mantissaSpan[..sepIndex]
                        : mantissaSpan[..(sepIndex + decimalSeparator.Length + precision.Value)];
                    if (nextDigit >= 5)
                    {
                        sb.Clear();
                        mantissaSpan = sb.Append(mantissaSpan[..^1])
                            .Append(int.Parse(mantissaSpan[^1..]) + 1)
                            .ToSpan();
                    }
                }
            }
            exponent = 0;
        }
        else if (exponent < 0)
        {
            if (Math.Abs(exponent) >= rawMantissaSpan.Length)
            {
                var sb = new StringBuilder("0")
                    .Append(decimalSeparator)
                    .Append('0', Math.Abs(exponent) - rawMantissaSpan.Length)
                    .Append(rawMantissaSpan);
                if (precision < sb.Length - 2)
                {
                    var nextDigit = precision.Value <= 0
                        ? int.Parse(sb[decimalSeparator.Length + 1].ToString())
                        : int.Parse(sb[precision.Value + decimalSeparator.Length + 1].ToString());
                    if (precision.Value <= 0)
                    {
                        sb.Remove(1, sb.Length - 1);
                    }
                    else
                    {
                        sb.Remove(precision.Value + 2, sb.Length - precision.Value - 2);
                    }
                    if (nextDigit >= 5)
                    {
                        var lastDigit = int.Parse(new ReadOnlySpan<char>([sb[^1]]));
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(lastDigit + 1);
                    }
                }
                mantissaSpan = sb.ToSpan();
            }
            else
            {
                mantissaSpan = new StringBuilder()
                    .Append(rawMantissaSpan[..(rawMantissaSpan.Length + exponent)])
                    .Append(decimalSeparator)
                    .Append(rawMantissaSpan[(rawMantissaSpan.Length + exponent)..])
                    .TrimEnd('0')
                    .TrimEnd(decimalSeparator, out var hadDecimalSeparator)
                    .ToSpan();
                if (!hadDecimalSeparator && precision.HasValue)
                {
                    var sepIndex = mantissaSpan.IndexOf(decimalSeparator);
                    if (mantissaSpan.Length - sepIndex - decimalSeparator.Length > precision.Value)
                    {
                        var nextDigit = precision.Value <= 0
                            ? int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length, 1))
                            : int.Parse(mantissaSpan.Slice(sepIndex + decimalSeparator.Length + precision.Value, 1));
                        mantissaSpan = precision.Value <= 0
                            ? mantissaSpan[..sepIndex]
                            : mantissaSpan[..(sepIndex + decimalSeparator.Length + precision.Value)];
                        if (nextDigit >= 5)
                        {
                            mantissaSpan = new StringBuilder(mantissaSpan.Length)
                                .Append(mantissaSpan[..^1])
                                .Append(int.Parse(mantissaSpan[^1..]) + 1)
                                .ToSpan();
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
                    mantissaSpan = digits.ToSpan();
                }
            }
            exponent = 0;
        }
        else if (exponent > 0)
        {
            var sb = new StringBuilder(rawMantissaSpan.Length + exponent)
                .Append(rawMantissaSpan)
                .Append('0', exponent);
            if (groupSeparators)
            {
                var digits = new StringBuilder();
                var groupIndex = 0;
                var groupCount = 0;
                for (var i = sb.Length - 1; i >= 0; i--)
                {
                    digits.Insert(0, sb[i]);

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
                mantissaSpan = digits.ToSpan();
            }
            else
            {
                mantissaSpan = sb.ToSpan();
            }
            exponent = 0;
        }
        else if (number.Denominator == 1
            && precision.HasValue)
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

        builder.Append(mantissaSpan);

        var exponentString = exponent.ToString("d");

        if (precision.HasValue && number.Denominator == 1)
        {
            if (mantissaSpan.IndexOf(decimalSeparator) != -1)
            {
                var index = mantissaSpan.IndexOf(decimalSeparator);
                var precisionLength = mantissaSpan.Length - index - decimalSeparator.Length;
                if (precisionLength < precision.Value)
                {
                    builder.Append('0', precision.Value - precisionLength);
                }
            }
            else
            {
                var exponentStringLength = exponent == 0
                    ? 0
                    : exponentString.Length;
                if (precision.Value - exponentStringLength - mantissaSpan.Length > 0)
                {
                    builder.Append(decimalSeparator);
                    builder.Append('0', precision.Value - exponentStringLength - mantissaSpan.Length);
                }
            }
        }

        if (exponent != 0)
        {
            builder.Append(capitalize ? "E" : "e");

            if (exponent >= 0 && alwaysPositiveExponent)
            {
                builder.Append(provider.PositiveSign);
            }

            builder.Append(exponentString);
        }

        if (number.Denominator > 1)
        {
            builder.Append('/')
                .Append(number.Denominator);
        }
    }

    private static StringBuilder FormatInternal(HugeNumber number, ReadOnlySpan<char> format, NumberFormatInfo provider)
    {
        char formatChar;
        int? precision = null;
        if (format.IsWhiteSpace())
        {
            formatChar = 'g';
        }
        else
        {
            formatChar = format[0];
            if (Array.IndexOf(_AllowedFormatChars, formatChar) == -1)
            {
                throw new FormatException($"The format of {format} is invalid.");
            }
            if (format.Length > 1)
            {
                if (!int.TryParse(format[1..], out var specificPrecision))
                {
                    throw new FormatException($"The format of {format} is invalid.");
                }
                else
                {
                    precision = specificPrecision;
                }
            }
        }
        var formatCharLower = formatChar.ToString().ToLower();

        var sb = new StringBuilder();

        if (formatCharLower == "p")
        {
            number *= 100;
        }
        // Re-check infinities, in case the multiplication caused an overflow.
        if (number.IsPositiveInfinity())
        {
            return sb.Append(provider.PositiveInfinitySymbol);
        }
        else if (number.IsNegativeInfinity())
        {
            return sb.Append(provider.NegativeInfinitySymbol);
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

        var capitalize = !formatChar.ToString().Equals(formatCharLower);
        switch (formatCharLower)
        {
            case "c":
                if (number.IsNegative())
                {
                    number = -number;
                    switch (provider.CurrencyNegativePattern)
                    {
                        case 0:
                            sb.Append('(').Append(provider.CurrencySymbol);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(')');
                            break;
                        case 1:
                            sb.Append(provider.NegativeSign).Append(provider.CurrencySymbol);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            break;
                        case 2:
                            sb.Append(provider.CurrencySymbol).Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            break;
                        case 3:
                            sb.Append(provider.CurrencySymbol);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.NegativeSign);
                            break;
                        case 4:
                            sb.Append('(');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.CurrencySymbol).Append(')');
                            break;
                        case 5:
                            sb.Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.CurrencySymbol);
                            break;
                        case 6:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.NegativeSign).Append(provider.CurrencySymbol);
                            break;
                        case 7:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.CurrencySymbol).Append(provider.NegativeSign);
                            break;
                        case 8:
                            sb.Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(' ').Append(provider.CurrencySymbol);
                            break;
                        case 9:
                            sb.Append(provider.NegativeSign).Append(provider.CurrencySymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            break;
                        case 10:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(' ').Append(provider.CurrencySymbol).Append(provider.NegativeSign);
                            break;
                        case 11:
                            sb.Append(provider.CurrencySymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.NegativeSign);
                            break;
                        case 12:
                            sb.Append(provider.CurrencySymbol).Append(' ').Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            break;
                        case 13:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.NegativeSign).Append(' ').Append(provider.CurrencySymbol);
                            break;
                        case 14:
                            sb.Append('(').Append(provider.CurrencySymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(')');
                            break;
                        case 15:
                            sb.Append('(');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
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
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            break;
                        case 1:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(provider.CurrencySymbol);
                            break;
                        case 2:
                            sb.Append(provider.CurrencySymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            break;
                        case 3:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.CurrencyGroupSeparator, provider.CurrencyGroupSizes, provider.CurrencyDecimalSeparator, false, true, number);
                            sb.Append(' ').Append(provider.CurrencySymbol);
                            break;
                    }
                }
                break;
            case "d":
            case "f":
            case "r":
                if (number.IsNegative())
                {
                    number = -number;
                    sb.Append(provider.NegativeSign);
                }
                Format(sb, provider, capitalize, false, false, precision, false, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                break;
            case "e":
                if (number.IsNegative())
                {
                    number = -number;
                    sb.Append(provider.NegativeSign);
                }
                Format(sb, provider, capitalize, true, false, precision, false, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, true, false, number);
                break;
            case "n":
                if (number.IsNegative())
                {
                    number = -number;
                    switch (provider.NumberNegativePattern)
                    {
                        case 0:
                            sb.Append('(');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                            sb.Append(')');
                            break;
                        case 1:
                            sb.Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                            break;
                        case 2:
                            sb.Append(provider.NegativeSign).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                            break;
                        case 3:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                            sb.Append(provider.NegativeSign);
                            break;
                        case 4:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                            sb.Append(' ').Append(provider.NegativeSign);
                            break;
                    }
                }
                else
                {
                    Format(sb, provider, capitalize, false, false, precision, true, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                }
                break;
            case "p":
                if (number.IsNegative())
                {
                    number = -number;
                    switch (provider.PercentNegativePattern)
                    {
                        case 0:
                            sb.Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(' ').Append(provider.PercentSymbol);
                            break;
                        case 1:
                            sb.Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.PercentSymbol);
                            break;
                        case 2:
                            sb.Append(provider.NegativeSign).Append(provider.PercentSymbol);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            break;
                        case 3:
                            sb.Append(provider.PercentSymbol).Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            break;
                        case 4:
                            sb.Append(provider.PercentSymbol);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.NegativeSign);
                            break;
                        case 5:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.NegativeSign).Append(provider.PercentSymbol);
                            break;
                        case 6:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.PercentSymbol).Append(provider.NegativeSign);
                            break;
                        case 7:
                            sb.Append(provider.NegativeSign).Append(provider.PercentSymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            break;
                        case 8:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(' ').Append(provider.PercentSymbol).Append(provider.NegativeSign);
                            break;
                        case 9:
                            sb.Append(provider.PercentSymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.NegativeSign);
                            break;
                        case 10:
                            sb.Append(provider.PercentSymbol).Append(' ').Append(provider.NegativeSign);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            break;
                        case 11:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.NegativeSign).Append(' ').Append(provider.PercentSymbol);
                            break;
                    }
                }
                else
                {
                    switch (provider.PercentPositivePattern)
                    {
                        case 0:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(' ').Append(provider.PercentSymbol);
                            break;
                        case 1:
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            sb.Append(provider.PercentSymbol);
                            break;
                        case 2:
                            sb.Append(provider.PercentSymbol);
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            break;
                        case 3:
                            sb.Append(provider.PercentSymbol).Append(' ');
                            Format(sb, provider, capitalize, false, false, precision, true, provider.PercentGroupSeparator, provider.PercentGroupSizes, provider.PercentDecimalSeparator, false, false, number);
                            break;
                    }
                }
                break;
            default:
                if (number.IsNegative())
                {
                    number = -number;
                    sb.Append(provider.NegativeSign);
                }
                Format(sb, provider, capitalize, false, true, precision, false, provider.NumberGroupSeparator, provider.NumberGroupSizes, provider.NumberDecimalSeparator, false, false, number);
                break;
        }

        return sb;
    }

    private static string HandleOtherFormats(string? format, object? arg) => arg is IFormattable formattable
        ? formattable.ToString(format, null)
        : arg?.ToString() ?? string.Empty;
}
