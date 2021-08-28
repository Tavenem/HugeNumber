using Tavenem.Mathematics;

namespace Tavenem.HugeNumbers.Test;

[TestClass]
public class MiscTests
{
    [TestMethod]
    public void ComparisonTest()
    {
        var value = new HugeNumber(10000000000000000, -10);
        Assert.IsTrue(value > 2500);
        Assert.IsFalse(value <= 75000);
        Assert.IsTrue(value <= 10000000);
        Assert.IsTrue(value <= 30000000);
    }

    [TestMethod]
    public void ConversionTest()
    {
        var result = (double)new HugeNumber(-625766431676160906, -24);
        Assert.IsTrue(result.IsNearlyEqualTo(-625766431676160906e-24));

        result = (double)new HugeNumber(-0.7280716627805054, 7);
        Assert.IsTrue(result.IsNearlyEqualTo(-0.7280716627805054e7, 1e-8));

        var numberResult = (HugeNumber)1E-08;
        Assert.AreEqual(new HugeNumber(1, -8), numberResult);

        numberResult = (HugeNumber)0m;
        Assert.AreEqual(HugeNumber.Zero, numberResult);

        numberResult = (HugeNumber)1.2m;
        Assert.AreEqual(new HugeNumber(12, -1), numberResult);

        numberResult = (HugeNumber)(-1.2m);
        Assert.AreEqual(new HugeNumber(-12, -1), numberResult);

        numberResult = (HugeNumber)1E-08m;
        Assert.AreEqual(new HugeNumber(1, -8), numberResult);

        numberResult = (HugeNumber)(-1E-08m);
        Assert.AreEqual(new HugeNumber(-1, -8), numberResult);

        Assert.IsTrue(HugeNumber.TryCreate(1.0, out var converted));
        Assert.AreEqual(HugeNumber.One, converted);

        Assert.IsTrue(HugeNumber.TryCreate(double.PositiveInfinity, out converted));
        Assert.AreEqual(HugeNumber.PositiveInfinity, converted);

        Assert.IsTrue(HugeNumber.TryCreate(double.NegativeInfinity, out converted));
        Assert.AreEqual(HugeNumber.NegativeInfinity, converted);

        Assert.IsTrue(HugeNumber.TryCreate(double.NaN, out converted));
        Assert.IsTrue(converted.IsNaN());

        converted = HugeNumber.Create(1.0);
        Assert.AreEqual(HugeNumber.One, converted);

        converted = HugeNumber.Create(double.PositiveInfinity);
        Assert.AreEqual(HugeNumber.PositiveInfinity, converted);

        converted = HugeNumber.Create(double.NegativeInfinity);
        Assert.AreEqual(HugeNumber.NegativeInfinity, converted);

        converted = HugeNumber.Create(double.NaN);
        Assert.IsTrue(converted.IsNaN());

        converted = 1.0.Create<double, HugeNumber>();
        Assert.AreEqual(HugeNumber.One, converted);

        converted = double.PositiveInfinity.Create<double, HugeNumber>();
        Assert.AreEqual(HugeNumber.PositiveInfinity, converted);

        converted = double.NegativeInfinity.Create<double, HugeNumber>();
        Assert.AreEqual(HugeNumber.NegativeInfinity, converted);

        converted = double.NaN.Create<double, HugeNumber>();
        Assert.IsTrue(converted.IsNaN());

        result = HugeNumber.One.Create<HugeNumber, double>();
        Assert.AreEqual(1.0, result);

        result = HugeNumber.PositiveInfinity.Create<HugeNumber, double>();
        Assert.AreEqual(double.PositiveInfinity, result);

        result = HugeNumber.NegativeInfinity.Create<HugeNumber, double>();
        Assert.AreEqual(double.NegativeInfinity, result);

        result = HugeNumber.NaN.Create<HugeNumber, double>();
        Assert.IsTrue(result.IsNaN());
    }
}
