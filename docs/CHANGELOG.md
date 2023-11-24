# Changelog

## 2.3
### Changed
- Update to .NET 8

## 2.2
### Fixed
- Multiplication resulted in -0 in some cases when it should not have

## 2.1
### Updated
- Update to .NET 7
- Remove dependency on preview features and experimental NuGet package

## 2.0.0
### Added
- Implement rational fraction support
- Implement `ICreateOther`
### Changed
- Update to .NET 6 preview
- Update to C# 10 preview
- Update to release packages
- Make `HugeNumber` struct readonly
- Move most non-standard constants to `HugeNumberConstants` static class in `Tavenem.Mathematics` namespace
- Implement `IFloatingPoint`
- Rename static `e` to `E` to align with `IFloatingPoint`
- Rename static `PI` and related to `Pi` to align with `IFloatingPoint`
- Rename static `phi` to `Phi` to align with others
- Changed the signature of `DivRem` to align with `INumber`
- Changed the return type of `Sign` to `HugeNumber` to align with `INumber`
- Renamed `CubeRoot` to `Cbrt` to align with `IFloatingPoint`
- Renamed `Ln` to `Log` to align with `IFloatingPoint`
- Removed overload of existing `Log` instance method to avoid conflict
- Changed property `IsInfinite` to method `IsInfinity` to better align with `IFloatingPoint`
- Changed `IsFinite`, `IsNaN`, `IsNegative`, `IsNegativeInfinity`, `IsPositive`, `IsPositiveInfinity` properties to methods to better align with `IFloatingPoint`
- Changed internal structure
    - Eliminated bit fields used to track infinity, NaN. Invalid mantissa and/or exponent values now indicate those states.
      These invalid values cannot be set using any public constructor.
    - Serialization has changed, since the bit fields are no longer used.
      This is a **breaking change**.
      Values serialized using previous versions will not deserialize properly with new versions, and vice versa.
- Removed `IsZero` property, optimized equality to short-circuit on zero instead
- Changed default rounding to nearest even, to better align with `IFloatingPoint`, and added overloads with a `MidpointRounding` parameter
### Fixed
- `Atan` implementation
- decimal conversion
### Removed
- Remove support for non-JSON serialization

## 1.0.2
### Fixed
- Corrected formatting error

## 1.0.1
### Fixed
- Corrected namespace error

## 1.0.0
### Added
- Initial release