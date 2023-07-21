using Tavenem.Mathematics;

namespace Tavenem.HugeNumbers.Test;

[TestClass]
public class MathTests
{
    [TestMethod]
    public void AdditionTest()
    {
        Assert.IsTrue((HugeNumber.NaN + HugeNumber.One).IsNaN());
        Assert.IsTrue((HugeNumber.PositiveInfinity + HugeNumber.One).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity + HugeNumber.One).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity + HugeNumber.PositiveInfinity).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity + HugeNumber.NegativeInfinity).IsNegativeInfinity());
        Assert.AreEqual(HugeNumber.Zero, HugeNumber.PositiveInfinity + HugeNumber.NegativeInfinity);

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

        Assert.IsTrue(HugeNumberConstants.QuarterPi.IsNearlyEqualTo(
            HugeNumber.Atan2(first, second),
            new HugeNumber(1, -14)));

        first = new HugeNumber(-734621347385107912, -18);
        second = new HugeNumber(659844505805127677, -18);

        _ = HugeNumber.Atan2(first, second);
    }

    [TestMethod]
    public void CubeRootTest()
    {
        var first = new HugeNumber(3.23143878972139169, 34);
        var second = new HugeNumber(3.1851653160978082074434669072492m, 11);

        Assert.IsTrue(first.Cbrt().IsNearlyEqualTo(second, HugeNumberConstants.Milli));
    }

    [TestMethod]
    public void DivisionTest()
    {
        Assert.IsTrue((HugeNumber.NaN / HugeNumber.One).IsNaN());
        Assert.IsTrue((HugeNumber.Zero / HugeNumber.Zero).IsNaN());
        Assert.IsTrue((HugeNumber.One / HugeNumber.Zero).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeOne / HugeNumber.Zero).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity / HugeNumber.One).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity / HugeNumber.One).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity / HugeNumber.PositiveInfinity).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity / HugeNumber.NegativeInfinity).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity / HugeNumber.NegativeInfinity).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity / HugeNumber.PositiveInfinity).IsNegativeInfinity());

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
        Assert.IsTrue((HugeNumber.NaN * HugeNumber.One).IsNaN());
        Assert.IsTrue((HugeNumber.PositiveInfinity * HugeNumber.One).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity * HugeNumber.One).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity * HugeNumber.PositiveInfinity).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity * HugeNumber.NegativeInfinity).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity * HugeNumber.NegativeInfinity).IsNegativeInfinity());

        var first = new HugeNumber(3);
        Assert.AreEqual(3, first);

        var second = new HugeNumber(2);
        Assert.AreEqual(2, second);

        Assert.AreEqual(6, first * second);

        first = new HugeNumber(300000000000000000, -10);
        second = new HugeNumber(200000000000000000, -12);
        Assert.AreEqual(new HugeNumber(6, 12), first * second);

        first = HugeNumber.Zero;
        second = new HugeNumber(127485571494234550, -6);
        var result = first * second;
        Assert.IsTrue(result.IsZero());
        Assert.IsFalse(result.IsNegative());
    }

    [TestMethod]
    public void RationalAdditionTest()
    {
        var first = new HugeNumber(2) / new HugeNumber(3);
        Assert.AreEqual(2, first.Mantissa);
        Assert.AreEqual(3, first.Denominator);
        Assert.AreEqual(2.0m / 3.0m, (decimal)first);

        var result = first + HugeNumber.One;
        Assert.AreEqual(5, result.Mantissa);
        Assert.AreEqual(3, result.Denominator);
        Assert.AreEqual(5.0m / 3.0m, (decimal)result);

        result += first;
        Assert.AreEqual(7, result.Mantissa);
        Assert.AreEqual(3, result.Denominator);
        Assert.AreEqual(7.0m / 3.0m, (decimal)result);

        var third = new HugeNumber(1) / new HugeNumber(3);
        Assert.AreEqual(1, third.Mantissa);
        Assert.AreEqual(3, third.Denominator);
        Assert.AreEqual(1.0m / 3.0m, (decimal)third);

        result = first + third;
        Assert.AreEqual(HugeNumber.One, result);

        var fourth = new HugeNumber(1, -1);
        Assert.AreEqual(new HugeNumber(766666666666666667, -18), first + fourth);
    }

    [TestMethod]
    public void RationalDivisionTest()
    {
        var first = new HugeNumber(2) / new HugeNumber(3);
        Assert.AreEqual(2, first.Mantissa);
        Assert.AreEqual(3, first.Denominator);
        Assert.AreEqual(2.0m / 3.0m, (decimal)first);

        var second = new HugeNumber(5) / new HugeNumber(3);
        Assert.AreEqual(5, second.Mantissa);
        Assert.AreEqual(3, second.Denominator);
        Assert.AreEqual(5.0m / 3.0m, (decimal)second);

        var result = first / second;
        Assert.AreEqual(2, result.Mantissa);
        Assert.AreEqual(5, result.Denominator);
        Assert.AreEqual(2.0m / 5.0m, (decimal)result);

        Assert.AreEqual(HugeNumber.One, first / first);

        var fourth = new HugeNumber(1, -1);
        Assert.AreEqual(new HugeNumber(666666666666666667, -17), first / fourth);
    }

    [TestMethod]
    public void RationalInversionTest()
    {
        var first = new HugeNumber(2) / new HugeNumber(3);
        Assert.AreEqual(2, first.Mantissa);
        Assert.AreEqual(3, first.Denominator);
        Assert.AreEqual(2.0m / 3.0m, (decimal)first);

        var result = first.Reciprocal();
        Assert.AreEqual(3, result.Mantissa);
        Assert.AreEqual(2, result.Denominator);
        Assert.AreEqual(3.0m / 2.0m, (decimal)result);

        result = HugeNumberConstants.Half.Reciprocal();
        Assert.AreEqual(2, (int)result);
    }

    [TestMethod]
    public void RationalMultiplicaionTest()
    {
        var first = new HugeNumber(2) / new HugeNumber(3);
        Assert.AreEqual(2, first.Mantissa);
        Assert.AreEqual(3, first.Denominator);
        Assert.AreEqual(2.0m / 3.0m, (decimal)first);

        var result = first * new HugeNumber(2);
        Assert.AreEqual(4, result.Mantissa);
        Assert.AreEqual(3, result.Denominator);
        Assert.AreEqual(4.0m / 3.0m, (decimal)result);

        result *= first;
        Assert.AreEqual(8, result.Mantissa);
        Assert.AreEqual(9, result.Denominator);
        Assert.AreEqual(8.0m / 9.0m, (decimal)result);

        var second = new HugeNumber(3) / new HugeNumber(2);
        Assert.AreEqual(3, second.Mantissa);
        Assert.AreEqual(2, second.Denominator);
        Assert.AreEqual(3.0m / 2.0m, (decimal)second);

        Assert.AreEqual(HugeNumber.One, first * second);

        var fourth = new HugeNumber(1, -1);
        Assert.AreEqual(new HugeNumber(666666666666666667, -19), first * fourth);
    }

    [TestMethod]
    public void RationalSubtractionTest()
    {
        var first = new HugeNumber(7) / new HugeNumber(3);
        Assert.AreEqual(7, first.Mantissa);
        Assert.AreEqual(3, first.Denominator);
        Assert.AreEqual(7.0m / 3.0m, (decimal)first);

        var second = new HugeNumber(2) / new HugeNumber(3);
        Assert.AreEqual(2, second.Mantissa);
        Assert.AreEqual(3, second.Denominator);
        Assert.AreEqual(2.0m / 3.0m, (decimal)second);

        var result = first - second;
        Assert.AreEqual(5, result.Mantissa);
        Assert.AreEqual(3, result.Denominator);
        Assert.AreEqual(5.0m / 3.0m, (decimal)result);

        result = second - 1;
        Assert.AreEqual(-1, result.Mantissa);
        Assert.AreEqual(3, result.Denominator);
        Assert.AreEqual(-1.0m / 3.0m, (decimal)result);

        result += second;
        Assert.AreEqual(1, result.Mantissa);
        Assert.AreEqual(3, result.Denominator);
        Assert.AreEqual(1.0m / 3.0m, (decimal)result);

        result = first - result;
        Assert.AreEqual(2, (int)result);

        var fourth = new HugeNumber(1, -1);
        Assert.AreEqual(new HugeNumber(566666666666666667, -18), second - fourth);
    }

    [TestMethod]
    public void SubtractionTest()
    {
        Assert.IsTrue((HugeNumber.NaN - HugeNumber.One).IsNaN());
        Assert.IsTrue((HugeNumber.PositiveInfinity - HugeNumber.One).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity - HugeNumber.One).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.PositiveInfinity - HugeNumber.NegativeInfinity).IsPositiveInfinity());
        Assert.IsTrue((HugeNumber.NegativeInfinity - HugeNumber.PositiveInfinity).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.One - HugeNumber.PositiveInfinity).IsNegativeInfinity());
        Assert.IsTrue((HugeNumber.One - HugeNumber.NegativeInfinity).IsPositiveInfinity());
        Assert.AreEqual(HugeNumber.Zero, HugeNumber.NegativeInfinity - HugeNumber.NegativeInfinity);
        Assert.AreEqual(HugeNumber.Zero, HugeNumber.PositiveInfinity - HugeNumber.PositiveInfinity);

        var first = new HugeNumber(3);
        Assert.AreEqual(3, first);

        var second = new HugeNumber(2);
        Assert.AreEqual(2, second);

        Assert.AreEqual(1, first - second);
    }
}
