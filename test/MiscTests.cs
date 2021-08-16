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
        Assert.IsTrue(numberResult.Equals(new HugeNumber(1, -8)));

        numberResult = (HugeNumber)1E-08m;
        Assert.IsTrue(numberResult.Equals(new HugeNumber(1, -8)));
    }
}
