using System;
using NUnit.Framework;
using Assignment3;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTests
    {
        private const double Tolerance = 1e-12;

        [TestCase(0, 0, 0)]
        [TestCase(1, 2, 3)]
        [TestCase(-1, -2, -3)]
        [TestCase(-5, 10, 5)]
        [TestCase(1.5, 2.25, 3.75)]
        [TestCase(double.MaxValue, 0, double.MaxValue)]
        public void Add_ReturnsExpected(double a, double b, double expected)
        {
            var calc = new Calculator(a, b, '+');
            Assert.That(calc.Calculate(), Is.EqualTo(expected).Within(Tolerance));
        }

        [TestCase(3.2, -7.4)]
        [TestCase(-10, -0.5)]
        [TestCase(1e100, 1e-100)]
        public void Add_IsCommutative(double a, double b)
        {
            var calc1 = new Calculator(a, b, '+').Calculate();
            var calc2 = new Calculator(b, a, '+').Calculate();
            Assert.That(calc1, Is.EqualTo(calc2).Within(Tolerance));
        }

        [TestCase(5, 3, 2)]
        [TestCase(3, 5, -2)]
        [TestCase(-2, -3, 1)]
        [TestCase(1.5, 0.25, 1.25)]
        public void Subtract_ReturnsExpected(double a, double b, double expected)
        {
            var calc = new Calculator(a, b, '-');
            Assert.That(calc.Calculate(), Is.EqualTo(expected).Within(Tolerance));
        }

        [TestCase(0, 100, 0)]
        [TestCase(2, 3, 6)]
        [TestCase(-2, 3, -6)]
        [TestCase(-2, -3, 6)]
        [TestCase(1.5, 2, 3)]
        public void Multiply_ReturnsExpected(double a, double b, double expected)
        {
            var calc = new Calculator(a, b, '*');
            Assert.That(calc.Calculate(), Is.EqualTo(expected).Within(Tolerance));
        }

        [TestCase(3.2, -7.4)]
        [TestCase(-10, -0.5)]
        [TestCase(1e50, 1e-20)]
        public void Multiply_IsCommutative(double a, double b)
        {
            var calc1 = new Calculator(a, b, '*').Calculate();
            var calc2 = new Calculator(b, a, '*').Calculate();
            Assert.That(calc1, Is.EqualTo(calc2).Within(Tolerance));
        }

        [Test]
        public void Multiply_Overflow_ProducesInfinity()
        {
            var calc = new Calculator(double.MaxValue, 2, '*');
            var result = calc.Calculate();
            Assert.That(double.IsInfinity(result), Is.True);
        }

        [Test]
        public void Multiply_Underflow_TowardZero()
        {
            var tiny = double.Epsilon; // the smallest positive subnormal step
            var calc = new Calculator(tiny, tiny, '*');
            var result = calc.Calculate();
            Assert.That(result, Is.EqualTo(0.0).Within(Tolerance)); // likely underflows to 0
        }

        [TestCase(6, 3, 2)]
        [TestCase(-6, 3, -2)]
        [TestCase(6, -3, -2)]
        [TestCase(-6, -3, 2)]
        [TestCase(1.0, 3.0, 0.3333333333333)]
        [TestCase(0, 5, 0)]
        public void Divide_ReturnsExpected(double a, double b, double expected)
        {
            var calc = new Calculator(a, b, '/');
            Assert.That(calc.Calculate(), Is.EqualTo(expected).Within(1e-9));
        }

        [Test]
        public void Divide_ByZero_Throws()
        {
            var calc = new Calculator(1, 0, '/');
            Assert.Throws<DivideByZeroException>(() => calc.Calculate());
        }

        [TestCase('%')]
        [TestCase('x')]
        [TestCase(' ')]
        public void InvalidOperation_Throws(char op)
        {
            var calc = new Calculator(1, 2, op);
            Assert.Throws<InvalidOperationException>(() => calc.Calculate());
        }

        [Test]
        public void WithNaN_PropagatesNaN_Add()
        {
            var calc = new Calculator(double.NaN, 5, '+');
            var result = calc.Calculate();
            Assert.That(double.IsNaN(result), Is.True);
        }

        [Test]
        public void WithNaN_PropagatesNaN_Multiply()
        {
            var calc = new Calculator(3, double.NaN, '*');
            var result = calc.Calculate();
            Assert.That(double.IsNaN(result), Is.True);
        }

        [Test]
        public void Divide_InvolvingNaN_PropagatesNaN()
        {
            var calc = new Calculator(double.NaN, 2, '/');
            var result = calc.Calculate(); // B != 0, so division runs and returns NaN
            Assert.That(double.IsNaN(result), Is.True);
        }

        [Test]
        public void Add_NegativeZero_BehavesLikeZero()
        {
            double negZero = -0.0;
            var calc = new Calculator(negZero, 0.0, '+');
            var result = calc.Calculate();
            Assert.That(result, Is.EqualTo(0.0).Within(Tolerance));
            Assert.That(BitConverter.DoubleToInt64Bits(result), Is.EqualTo(BitConverter.DoubleToInt64Bits(0.0)));
        }

        [TestCase(5.0, 2.0)]
        [TestCase(-10.0, 7.25)]
        [TestCase(1e100, 1e90)]
        public void Subtract_ThenAdd_RoundTrip(double a, double b)
        {
            var minus = new Calculator(a, b, '-').Calculate();
            var back = new Calculator(minus, b, '+').Calculate();
            Assert.That(back, Is.EqualTo(a).Within(1e-9));
        }

        [Test]
        public void Division_Precision_Sanity()
        {
            var calc = new Calculator(1, 3, '/');
            var result = calc.Calculate();
            Assert.That(result, Is.EqualTo(1.0 / 3.0).Within(1e-12));
        }
    }
}
