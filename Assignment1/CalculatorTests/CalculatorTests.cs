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

        [TestMethod]
        public void TestParserReturn()
        {
            int[] expected = new int[] { -123, 22, 0, 44, 5 };
            var myParser = new CalculatorProgram.CalcInputParser();
            var results = myParser.ParseStringInputToAddends("-123,22,\n44,5");
            for(int i = 0; i < results.Length; i++)
            {

                Assert.AreEqual(expected[i], results[i]);
            }
        }

        [TestMethod]
        public void DefaultDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-123,22,\n44,5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void LineBreaksAsDefaultDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("-123\n22\n44\n5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void CustomDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//y\n-123y22y44y5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void CustomDelimiterAndNewLine()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//y\n-123y22y\n44y5");
        }

        [TestMethod]
        public void AddNumberGreaterThanOneThousand()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("1123,22,1000,5");
            Assert.AreEqual(1027, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void CommasAfterCustomDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//y\n-123,22,\n44y5");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void NumberAsCustomDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//5\n-1235225\n4455");
        }

        [TestMethod]
        public void MultipleCustomSingleCharDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[y][z]\n-123y22z44y5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void MultipleCharDelimiterWithBrackets()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[yz]\n-123yz22yz44yz5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void MultipleCharDelimiterWithoutBrackets()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//yz\n-123yz22yz44yz5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void HyphenAsDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//-\n--123-22-44-5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        public void HyphenAsOneOfMultipleDelimiters()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[abc][-]\n--123-22abc44-5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void BracketsAsDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[]\n-123[]22[]44[]5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void QuotesAsDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add(@"//[""]\n-123[]22[]44[]5");
            Assert.AreEqual(-52, result);
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void MultiDigitNumberAsOneOfMultipleDelimiters()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[abc][456]\n-123abc22456444565");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void DelimiterAsSubsetOfAnotherDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[a][abc]\n-123a22abc44a5");
        }

        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void NewLineAsOneOfMultipleCustomDelimiter()
        {
            var myCalc = new CalculatorProgram.Calculator();
            var result = myCalc.Add("//[!][\n]\n-123\n22!44!5");
        }
    }
}
