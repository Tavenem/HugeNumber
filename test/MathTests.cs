namespace Tavenem.HugeNumbers.Test;

[TestClass]
public class MathTests
{
    [TestMethod]
    public void AdditionTest()
    {
        var first = HugeNumber.One;
        Assert.AreEqual(1, first);

        var second = new HugeNumber(2);
        Assert.AreEqual(2, second);

        Assert.AreEqual(3, first + second);

        first = new HugeNumber(300551835350938372, -15);
        second = new HugeNumber(-529536463668508561, -14);

        Assert.AreEqual(new HugeNumber(-4994812801334147238, -15), first + second);
    }

    [TestMethod]
    public void Atan2Test()
    {
        var first = HugeNumber.One;
        var second = HugeNumber.One;

        Assert.IsTrue(HugeNumber.QuarterPi.IsNearlyEqualTo(HugeNumber.Atan2(first, second), new HugeNumber(1, -14)));

        first = new HugeNumber(-734621347385107912, -18);
        second = new HugeNumber(659844505805127677, -18);

        _ = HugeNumber.Atan2(first, second);
    }

    [TestMethod]
    public void CubeRootTest()
    {
        var first = new HugeNumber(3.23143878972139169, 34);
        var second = new HugeNumber(3.1851653160978082074434669072492m, 11);

        Assert.IsTrue(first.Cbrt().IsNearlyEqualTo(second, HugeNumber.Milli));
    }

    [TestMethod]
    public void DivisionTest()
    {
        var first = new HugeNumber(6);
        Assert.AreEqual(6, first);

        var second = new HugeNumber(2);
        Assert.AreEqual(2, second);

        Assert.AreEqual(3, first / second);

        first = new HugeNumber(600000000000000000, -10);
        second = new HugeNumber(200000000000000000, -12);
        Assert.AreEqual(300, first / second);
    }

    [TestMethod]
    public void LogarithmTest()
    {
        var first = new HugeNumber(6);
        Assert.AreEqual(6, first);

        Assert.IsTrue(first.Log().IsNearlyEqualTo(new HugeNumber(1791759469228, -12), new HugeNumber(1, -12)));
    }

    [TestMethod]
    public void MultiplicationTest()
    {
        var first = new HugeNumber(3);
        Assert.AreEqual(3, first);

        var second = new HugeNumber(2);
        Assert.AreEqual(2, second);

        Assert.AreEqual(6, first * second);

        first = new HugeNumber(300000000000000000, -10);
        second = new HugeNumber(200000000000000000, -12);
        Assert.AreEqual(new HugeNumber(6, 12), first * second);
    }

    [TestMethod]
    public void SubtractionTest()
    {
        var first = new HugeNumber(3);
        Assert.AreEqual(3, first);

        var second = new HugeNumber(2);
        Assert.AreEqual(2, second);

        Assert.AreEqual(1, first - second);
    }
}
