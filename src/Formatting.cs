using System.Globalization;

namespace Tavenem.HugeNumbers;

[System.Diagnostics.DebuggerDisplay("{DebugDisplay}")]
public partial struct HugeNumber
{
    private const string DefaultFormat = "g";

    private string DebugDisplay
    {
        get
        {
            if (IsNaN())
            {
                return "NaN";
            }
            else if (IsInfinity())
            {
                if (IsNegativeInfinity())
                {
                    return "-∞";
                }
                else
                {
                    return "∞";
                }
            }
            else if (Mantissa == 0)
            {
                return "0";
            }
            else if (Exponent == 0)
            {
                return Mantissa.ToString(CultureInfo.InvariantCulture);
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
    public string ToString(string? format, IFormatProvider? formatProvider)
        => HugeNumberFormatProvider.Instance.Format(format, this, formatProvider);

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
    public string ToString(IFormatProvider? formatProvider)
        => HugeNumberFormatProvider.Instance.Format(DefaultFormat, this, formatProvider);

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
    public string ToString(string? format)
        => HugeNumberFormatProvider.Instance.Format(format, this, NumberFormatInfo.CurrentInfo);

    /// <summary>
    /// Converts the value of the current <see cref="HugeNumber"/> object to its equivalent string
    /// representation.
    /// </summary>
    /// <returns>
    /// The string representation of the current <see cref="HugeNumber"/> value.
    /// </returns>
    public override string ToString()
        => HugeNumberFormatProvider.Instance.Format(DefaultFormat, this, NumberFormatInfo.CurrentInfo);

    /// <summary>
    /// Attempts to write this instance to the given <see cref="Span{T}"/>.
    /// </summary>
    /// <param name="destination">The <see cref="Span{T}"/> to write to.</param>
    /// <param name="charsWritten">
    /// When this method returns, this will contains the number of characters written to <paramref name="destination"/>.
    /// </param>
    /// <param name="format">A format string containing formatting specifications.</param>
    /// <param name="provider">
    /// An object that supplies format information about the current instance.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if this instance was successfully written to the
    /// <paramref name="destination"/>; otherwise <see langword="false"/>.
    /// </returns>
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider? provider)
        => HugeNumberFormatProvider.TryFormat(this, destination, out charsWritten, format, provider);
}
