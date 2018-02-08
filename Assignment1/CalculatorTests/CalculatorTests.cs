using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorProgram;

namespace CalculatorTests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void ParamContainsJunk()
        {
            var myCalc = new CalculatorProgram.Calculator();
            myCalc.Add("1,test");
        }

        [TestMethod]
        public void ReturnZeroForNullInput()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add();
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ReturnZeroForEmptyString()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ReturnPositiveSingleParam()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("1");
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ReturnZeroSingleParam()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("0");
            Assert.AreEqual(0, result);
        }

        [TestMethod]
        public void ReturnNegativeSingleParam()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-1");
            Assert.AreEqual(-1, result);
        }

        [TestMethod]
        public void AddTwoPositiveSingleDigitParams()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("1,2");
            Assert.AreEqual(3, result);
        }

        [TestMethod]
        public void AddTwoPositiveMultiDigitParams()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("123,22");
            Assert.AreEqual(145, result);
        }

        [TestMethod]
        public void AddOnePositiveParamToZero()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("0,22");
            Assert.AreEqual(22, result);
        }

        [TestMethod]
        public void AddPositiveAndNegativeReturnNegative()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-123,22");
            Assert.AreEqual(-101, result);
        }

        [TestMethod]
        public void AddThreeNegativeNumbers()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-123,-1,-54");
            Assert.AreEqual(-178, result);
        }

        [TestMethod]
        public void AddFourNumbersWithNewLineAndCommas()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-123,22\n44,5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void AddMultipleNumbersIncludingEmptyValue()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-123,22,\n44,5");
            Assert.AreEqual(-52, result);
        }
    }
}