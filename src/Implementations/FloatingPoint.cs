namespace Tavenem.HugeNumbers;

public partial struct HugeNumber
{
    /// <summary>
    /// Computes the ceiling of a value.
    /// </summary>
    /// <param name="x">The value whose ceiling is to be computed.</param>
    /// <returns>The ceiling of <paramref name="x"/>.</returns>
    public static HugeNumber Ceiling(HugeNumber x)
    {
        if (x.Exponent < 0)
        {
            var mantissa = x.Mantissa;
            for (var exponent = x.Exponent; exponent < -1; exponent++)
            {
                mantissa /= 10;
            }
            var roundUp = mantissa > 0 && mantissa % 10 != 0;
            mantissa /= 10;
            if (roundUp)
            {
                mantissa++;
            }
            return mantissa;
        }
        return x;
    }

    /// <summary>
    /// Computes the ceiling of this value.
    /// </summary>
    /// <returns>The ceiling of this value.</returns>
    public HugeNumber Ceiling() => Ceiling(this);

    /// <summary>
    /// Computes the floor of a value.
    /// </summary>
    /// <param name="x">The value whose floor is to be computed.</param>
    /// <returns>The floor of <paramref name="x"/>.</returns>
    public static HugeNumber Floor(HugeNumber x)
    {
        if (x.Exponent < 0)
        {
            var mantissa = x.Mantissa;
            for (var exponent = x.Exponent; exponent < -1; exponent++)
            {
                mantissa /= 10;
            }
            var roundDown = mantissa < 0 && mantissa % 10 != 0;
            mantissa /= 10;
            if (roundDown)
            {
                mantissa--;
            }
            return mantissa;
        }
        return x;
    }

    /// <summary>
    /// Computes the floor of this value.
    /// </summary>
    /// <returns>The floor of this value.</returns>
    public HugeNumber Floor() => Floor(this);

    /// <summary>
    /// Gets the number of bytes that will be written as part of <see
    /// cref="TryWriteExponentLittleEndian(Span{byte}, out int)"/>.
    /// </summary>
    /// <returns>
    /// The number of bytes that will be written as part of <see
    /// cref="TryWriteExponentLittleEndian(Span{byte}, out int)"/>.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public int GetExponentByteCount()
        => ((IFloatingPoint<double>)0.0).GetExponentByteCount();

    /// <summary>
    /// Gets the length, in bits, of the shortest two's complement representation of the current exponent.
    /// </summary>
    /// <returns>
    /// The length, in bits, of the shortest two's complement representation of the current exponent.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public int GetExponentShortestBitLength()
        => ((IFloatingPoint<double>)(double)this).GetExponentShortestBitLength();

    /// <summary>
    /// Gets the length, in bits, of the current significand.
    /// </summary>
    /// <returns>
    /// The length, in bits, of the current significand.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public int GetSignificandBitLength()
        => ((IFloatingPoint<double>)(double)this).GetSignificandBitLength();

    /// <summary>
    /// Gets the number of bytes that will be written as part of <see
    /// cref="TryWriteSignificandLittleEndian(Span{byte}, out int)"/>.
    /// </summary>
    /// <returns>
    /// The number of bytes that will be written as part of <see
    /// cref="TryWriteSignificandLittleEndian(Span{byte}, out int)"/>.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public int GetSignificandByteCount()
        => ((IFloatingPoint<double>)0.0).GetSignificandByteCount();

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="x"/> to a specified
    /// number of fractional <paramref name="digits"/> using the specified rounding convention.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="x"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="x"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="x"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="x"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="x"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// <para>
    /// If <paramref name="x"/> is a rational fraction, it is first converted to a decimal
    /// representation before rounding.
    /// </para>
    /// </remarks>
    public static HugeNumber Round(HugeNumber x, int digits, MidpointRounding mode)
    {
        if (digits is < 0 or > 18)
        {
            throw new ArgumentOutOfRangeException(
                nameof(digits),
                digits,
                $"{nameof(digits)} cannot be less than 0 or greater than 18.");
        }
        if (!Enum.IsDefined(mode))
        {
            throw new ArgumentException(
                $"{nameof(mode)} must be a valid value of {nameof(MidpointRounding)}.",
                nameof(mode));
        }

        if (!x.IsFinite())
        {
            return x;
        }

        if (x.Denominator > 1)
        {
            x = ToDenominator(x, 1);
        }
        if (x.Exponent < -digits)
        {
            var mantissa = x.Mantissa;
            var exponent = x.Exponent;
            while (exponent < -digits - 1)
            {
                mantissa /= 10;
                exponent++;
            }
            var remainder = mode == MidpointRounding.ToZero
                ? 0
                : Math.Abs(mantissa % 10);
            mantissa /= 10;
            exponent++;
            switch (mode)
            {
                case MidpointRounding.ToEven:
                    if (remainder > 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    else if (remainder == 5
                        && mantissa % 2 != 0)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.AwayFromZero:
                    if (remainder >= 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.ToNegativeInfinity:
                    if (remainder > 0
                        && mantissa < 0)
                    {
                        mantissa--;
                    }
                    break;
                case MidpointRounding.ToPositiveInfinity:
                    if (remainder > 0
                        && mantissa > 0)
                    {
                        mantissa++;
                    }
                    break;
            }
            return new HugeNumber(mantissa, exponent);
        }
        return x;
    }

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="x"/> to a specified
    /// number of fractional <paramref name="digits"/> using the specified rounding convention.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="x"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="x"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="x"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="x"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="x"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Round<TInteger>(HugeNumber x, TInteger digits, MidpointRounding mode)
        where TInteger : IBinaryInteger<TInteger>
    {
        var eightteen = TInteger.CreateChecked(18);
        if (digits < TInteger.Zero || digits > eightteen)
        {
            throw new ArgumentOutOfRangeException(
                nameof(digits),
                digits,
                $"{nameof(digits)} cannot be less than 0 or greater than 18.");
        }
        if (!Enum.IsDefined(mode))
        {
            throw new ArgumentException(
                $"{nameof(mode)} must be a valid value of {nameof(MidpointRounding)}.",
                nameof(mode));
        }

        if (!x.IsFinite())
        {
            return x;
        }

        var exponent = TInteger.CreateChecked(x.Exponent);
        if (exponent < -digits)
        {
            var mantissa = x.Mantissa;
            while (exponent < -digits - TInteger.One)
            {
                mantissa /= 10;
                exponent++;
            }
            var remainder = mode == MidpointRounding.ToZero
                ? 0
                : Math.Abs(mantissa % 10);
            mantissa /= 10;
            exponent++;
            switch (mode)
            {
                case MidpointRounding.ToEven:
                    if (remainder > 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    else if (remainder == 5
                        && mantissa % 2 != 0)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.AwayFromZero:
                    if (remainder >= 5)
                    {
                        mantissa = mantissa < 0
                            ? mantissa--
                            : mantissa++;
                    }
                    break;
                case MidpointRounding.ToNegativeInfinity:
                    if (remainder > 0
                        && mantissa < 0)
                    {
                        mantissa--;
                    }
                    break;
                case MidpointRounding.ToPositiveInfinity:
                    if (remainder > 0
                        && mantissa > 0)
                    {
                        mantissa++;
                    }
                    break;
            }
            return Create(mantissa, exponent);
        }
        return x;
    }

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="x"/> to an integer
    /// using the specified rounding convention.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="mode">
    /// One of the enumeration values that specifies which rounding strategy to use.
    /// </param>
    /// <returns>
    /// The integer that <paramref name="x"/> is rounded to.
    /// This method returns a <see cref="HugeNumber"/> instead of an integral type.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// <paramref name="mode"/> is not a valid value of <see cref="MidpointRounding"/>.
    /// </exception>
    /// <remarks>
    /// If the value of the <paramref name="x"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="x"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </remarks>
    public static HugeNumber Round(HugeNumber x, MidpointRounding mode) => Round(x, 0, mode);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="x"/> to a specified
    /// number of fractional <paramref name="digits"/>, and rounds midpoint values
    /// to the nearest even number.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="x"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="x"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="x"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="x"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="x"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Round(HugeNumber x, int digits) => Round(x, digits, MidpointRounding.ToEven);

    /// <summary>
    /// Gets this value rounded to the given number of decimal places.
    /// </summary>
    /// <param name="digits">The number of decimal places to which the value should be
    /// rounded.</param>
    /// <returns>This value rounded to the given number of decimal places.</returns>
    public HugeNumber Round(int digits) => Round(this, digits);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="x"/> to a specified
    /// number of fractional <paramref name="digits"/>, and rounds midpoint values
    /// to the nearest even number.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <param name="digits">
    /// The number of fractional digits in the return value.
    /// </param>
    /// <returns>
    /// The number nearest to <paramref name="x"/> that contains a number of
    /// fractional digits equal to <paramref name="digits"/>.
    /// If <paramref name="x"/> has fewer fractional digits than <paramref name="digits"/>,
    /// <paramref name="x"/> is returned unchanged.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="digits"/> is less than 0 or greater than 18.
    /// </exception>
    /// <remarks>
    /// <para>
    /// The value of the <paramref name="digits"/> argument can range from 0 to 18.
    /// The maximum number of integral and fractional digits supported by the
    /// <see cref="HugeNumber"/> type is 18.
    /// </para>
    /// <para>
    /// If the value of the <paramref name="x"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="x"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </para>
    /// </remarks>
    public static HugeNumber Round<TInteger>(HugeNumber x, TInteger digits)
        where TInteger : IBinaryInteger<TInteger> => Round(x, digits, MidpointRounding.ToEven);

    /// <summary>
    /// Rounds a <see cref="HugeNumber"/> <paramref name="x"/> to the nearest integral value,
    /// and rounds midpoint values to the nearest even number.
    /// </summary>
    /// <param name="x">A <see cref="HugeNumber"/> value to be rounded.</param>
    /// <returns>
    /// The integer that <paramref name="x"/> is rounded to.
    /// This method returns a <see cref="HugeNumber"/> instead of an integral type.
    /// </returns>
    /// <remarks>
    /// If the value of the <paramref name="x"/> argument is <see cref="NaN"/>, the method returns <see cref="NaN"/>.
    /// If <paramref name="x"/> is <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>,
    /// the method returns <see cref="PositiveInfinity"/> or <see cref="NegativeInfinity"/>, respectively.
    /// </remarks>
    public static HugeNumber Round(HugeNumber x) => Round(x, 0, MidpointRounding.ToEven);

    /// <summary>
    /// Gets the nearest whole integral value to this instance.
    /// </summary>
    /// <returns>The nearest whole integral value to this instance.</returns>
    public HugeNumber Round() => Round(this);

    /// <summary>
    /// Truncates a value.
    /// </summary>
    /// <param name="x">The value to truncate.</param>
    /// <returns>The truncation of <paramref name="x"/>.</returns>
    public static HugeNumber Truncate(HugeNumber x)
    {
        if (x.Exponent < -x.MantissaDigits)
        {
            return Zero;
        }
        if (x.Exponent < 0)
        {
            var result = x.Mantissa;
            for (var i = 0; i < -x.Exponent; i++)
            {
                result /= 10;
            }
            return result;
        }
        return x;
    }

    /// <summary>
    /// Truncates this value.
    /// </summary>
    /// <returns>The truncation of this value.</returns>
    public HugeNumber Truncate() => Truncate(this);

    /// <summary>
    /// Tries to write the current exponent, in big-endian format, to a given span.
    /// </summary>
    /// <param name="destination">The span to which the current exponent should be written.</param>
    /// <param name="bytesWritten">
    /// When this method returns, contains the number of bytes written to <paramref
    /// name="destination"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the exponent was succesfully written to <paramref
    /// name="destination"/>; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public bool TryWriteExponentBigEndian(Span<byte> destination, out int bytesWritten)
        => ((IFloatingPoint<double>)(double)this).TryWriteExponentBigEndian(destination, out bytesWritten);

    /// <summary>
    /// Tries to write the current exponent, in little-endian format, to a given span.
    /// </summary>
    /// <param name="destination">The span to which the current exponent should be written.</param>
    /// <param name="bytesWritten">
    /// When this method returns, contains the number of bytes written to <paramref
    /// name="destination"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the exponent was succesfully written to <paramref
    /// name="destination"/>; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public bool TryWriteExponentLittleEndian(Span<byte> destination, out int bytesWritten)
        => ((IFloatingPoint<double>)(double)this).TryWriteExponentLittleEndian(destination, out bytesWritten);

    /// <summary>
    /// Tries to write the current significand, in big-endian format, to a given span.
    /// </summary>
    /// <param name="destination">The span to which the current significand should be written.</param>
    /// <param name="bytesWritten">
    /// When this method returns, contains the number of bytes written to <paramref
    /// name="destination"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the significand was succesfully written to <paramref
    /// name="destination"/>; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public bool TryWriteSignificandBigEndian(Span<byte> destination, out int bytesWritten)
        => ((IFloatingPoint<double>)(double)this).TryWriteSignificandBigEndian(destination, out bytesWritten);

    /// <summary>
    /// Tries to write the current significand, in little-endian format, to a given span.
    /// </summary>
    /// <param name="destination">The span to which the current significand should be written.</param>
    /// <param name="bytesWritten">
    /// When this method returns, contains the number of bytes written to <paramref
    /// name="destination"/>.
    /// </param>
    /// <returns>
    /// <see langword="true"/> if the significand was succesfully written to <paramref
    /// name="destination"/>; otherwise, <see langword="false"/>.
    /// </returns>
    /// <remarks>
    /// Note that <see cref="HugeNumber"/> does not store its significand or exponent in this way.
    /// It implements the required significand and exponent writing methods of <see
    /// cref="IFloatingPoint{TSelf}"/> by converting the value to a <see cref="double"/>.
    /// </remarks>
    public bool TryWriteSignificandLittleEndian(Span<byte> destination, out int bytesWritten)
        => ((IFloatingPoint<double>)(double)this).TryWriteSignificandLittleEndian(destination, out bytesWritten);
}
