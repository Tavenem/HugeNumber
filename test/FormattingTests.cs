using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace Tavenem.HugeNumber.Test
{
    [TestClass]
    public class FormattingTests
    {
        [TestMethod]
        public void FormatTest()
        {
            var num = HugeNumber.One;
            Console.WriteLine($"HugeNumber: {num}");
        }

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("1", HugeNumber.One.ToString());

            Assert.AreEqual("2e42", new HugeNumber(2, 42).ToString());

            Assert.AreEqual("1.23e18", new HugeNumber(123000000000000000, 1).ToString(NumberFormatInfo.InvariantInfo));

            Assert.AreEqual("1.23e-17", new HugeNumber(123, -19).ToString(NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1.23e-17", new HugeNumber(123, -19).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1e-17", new HugeNumber(123, -19).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("2e-17", new HugeNumber(153, -19).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("2e-17", new HugeNumber(163, -19).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}1.23e-17", new HugeNumber(123, -19).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1.23e-17 %", new HugeNumber(123, -21).ToString("p", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual("0.123", new HugeNumber(123, -3).ToString(NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.12", new HugeNumber(123, -3).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.13", new HugeNumber(125, -3).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.13", new HugeNumber(126, -3).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0", new HugeNumber(123, -3).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1", new HugeNumber(523, -3).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1", new HugeNumber(623, -3).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.12", new HugeNumber(123, -3).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.13", new HugeNumber(125, -3).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.13", new HugeNumber(126, -3).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("12.30 %", new HugeNumber(123, -3).ToString("p", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual("1.23e-3", new HugeNumber(123, -5).ToString(NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.00", new HugeNumber(123, -5).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.01", new HugeNumber(523, -5).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.01", new HugeNumber(623, -5).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0", new HugeNumber(123, -5).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.00", new HugeNumber(123, -5).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.01", new HugeNumber(523, -5).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.01", new HugeNumber(623, -5).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.12 %", new HugeNumber(123, -5).ToString("p", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.13 %", new HugeNumber(125, -5).ToString("p", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("0.13 %", new HugeNumber(126, -5).ToString("p", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual("1e4", new HugeNumber(10000).ToString());
            Assert.AreEqual("10000", new HugeNumber(10000).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("10,000.00", new HugeNumber(10000).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("123,456,789", new HugeNumber(1234567891234567, -7).ToString("n0", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual("1234", new HugeNumber(1234).ToString());
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}1,234.00", new HugeNumber(1234).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1234.000", new HugeNumber(1234).ToString("f3", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1,234.00", new HugeNumber(1234).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1,234.00 %", new HugeNumber(1234, -2).ToString("p", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual("12.3", new HugeNumber(123, -1).ToString());
            Assert.AreEqual("12", new HugeNumber(123, -1).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("13", new HugeNumber(125, -1).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("13", new HugeNumber(126, -1).ToString("d", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("12.30", new HugeNumber(123, -1).ToString("n", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}12.30", new HugeNumber(123, -1).ToString("c", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual("1,230.00 %", new HugeNumber(123, -1).ToString("p", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(NumberFormatInfo.CurrentInfo.NaNSymbol, HugeNumber.NaN.ToString());

            Assert.AreEqual(NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol, HugeNumber.PositiveInfinity.ToString());

            Assert.AreEqual(NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol, HugeNumber.NegativeInfinity.ToString());
        }

        [TestMethod]
        public void ParseTest()
        {
            Assert.AreEqual(HugeNumber.One, HugeNumber.Parse("1", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(2, 42), HugeNumber.Parse("2e42"));

            Assert.AreEqual(new HugeNumber(123000000000000000, 1), HugeNumber.Parse("1.23e18", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(123, -19), HugeNumber.Parse("1.23e-17", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual(new HugeNumber(123, -19), HugeNumber.Parse($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}1.23e-17", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(123, -3), HugeNumber.Parse("0.123", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual(new HugeNumber(12, -2), HugeNumber.Parse($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}0.12", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(123, -5), HugeNumber.Parse("1.23e-3", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(10000), HugeNumber.Parse("1e4"));
            Assert.AreEqual(new HugeNumber(10000), HugeNumber.Parse("10000", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual(new HugeNumber(10000), HugeNumber.Parse("10,000.00", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(1234), HugeNumber.Parse("1234"));
            Assert.AreEqual(new HugeNumber(1234), HugeNumber.Parse($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}1,234.00", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual(new HugeNumber(1234), HugeNumber.Parse("1234.000", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual(new HugeNumber(1234), HugeNumber.Parse("1,234.00", NumberFormatInfo.InvariantInfo));

            Assert.AreEqual(new HugeNumber(123, -1), HugeNumber.Parse("12.3"));
            Assert.AreEqual(new HugeNumber(123, -1), HugeNumber.Parse("12.30", NumberFormatInfo.InvariantInfo));
            Assert.AreEqual(new HugeNumber(123, -1), HugeNumber.Parse($"{NumberFormatInfo.InvariantInfo.CurrencySymbol}12.30", NumberFormatInfo.InvariantInfo));

            Assert.IsTrue(HugeNumber.Parse(NumberFormatInfo.CurrentInfo.NaNSymbol).IsNaN);

            Assert.IsTrue(HugeNumber.Parse(NumberFormatInfo.CurrentInfo.PositiveInfinitySymbol).IsPositiveInfinity);

            Assert.IsTrue(HugeNumber.Parse(NumberFormatInfo.CurrentInfo.NegativeInfinitySymbol).IsNegativeInfinity);
        }

        [TestMethod]
        public void SerializationTest()
        {
            var json = System.Text.Json.JsonSerializer.Serialize(HugeNumber.NaN);
            Assert.AreEqual($"\"{NumberFormatInfo.InvariantInfo.NaNSymbol}\"", json);
            Assert.IsTrue(System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json).IsNaN);

            json = System.Text.Json.JsonSerializer.Serialize(HugeNumber.PositiveInfinity);
            Assert.AreEqual($"\"{NumberFormatInfo.InvariantInfo.PositiveInfinitySymbol}\"", json);
            Assert.IsTrue(System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json).IsPositiveInfinity);

            json = System.Text.Json.JsonSerializer.Serialize(HugeNumber.NegativeInfinity);
            Assert.AreEqual($"\"{NumberFormatInfo.InvariantInfo.NegativeInfinitySymbol}\"", json);
            Assert.IsTrue(System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json).IsNegativeInfinity);

            json = System.Text.Json.JsonSerializer.Serialize(HugeNumber.One);
            Assert.AreEqual("\"1\"", json);
            Assert.AreEqual(HugeNumber.One, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            var value = new HugeNumber(12);
            json = System.Text.Json.JsonSerializer.Serialize(value);
            Assert.AreEqual("\"12\"", json);
            Assert.AreEqual(value, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            value = new HugeNumber(2, 42);
            json = System.Text.Json.JsonSerializer.Serialize(value);
            Assert.AreEqual("\"2e42\"", json);
            Assert.AreEqual(value, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            value = new HugeNumber(24, 41);
            json = System.Text.Json.JsonSerializer.Serialize(value);
            Assert.AreEqual("\"2.4e42\"", json);
            Assert.AreEqual(value, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            value = new HugeNumber(-2, 42);
            json = System.Text.Json.JsonSerializer.Serialize(value);
            Assert.AreEqual("\"-2e42\"", json);
            Assert.AreEqual(value, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            value = new HugeNumber(2, -42);
            json = System.Text.Json.JsonSerializer.Serialize(value);
            Assert.AreEqual("\"2e-42\"", json);
            Assert.AreEqual(value, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            value = new HugeNumber(-2, -42);
            json = System.Text.Json.JsonSerializer.Serialize(value);
            Assert.AreEqual("\"-2e-42\"", json);
            Assert.AreEqual(value, System.Text.Json.JsonSerializer.Deserialize<HugeNumber>(json));

            json = System.Text.Json.JsonSerializer.Serialize<HugeNumber?>(null);
            Assert.AreEqual("null", json);
            Assert.IsFalse(System.Text.Json.JsonSerializer.Deserialize<HugeNumber?>(json).HasValue);
        }
    }
}
