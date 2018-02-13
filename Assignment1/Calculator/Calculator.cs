using System;
using System.Collections.Generic;
using System.Text;

namespace CalculatorProgram
{
    public class Calculator
    {
        private int sum;
        private int[] addends;

        public int[] Addends
        {
            get { return addends; }
            set { addends = value; }
        }


        public int Sum
        {
            get { return sum; }
            set { sum = value; }
        }

        public int Add(string stuffToAdd = "")
        {
            try
            {
                CalcInputParser myParser = new CalcInputParser();
                addends = myParser.ParseStringInputToAddends(stuffToAdd);
            }
            catch (ArgumentException exception)
            {
                throw exception;
            }

            for (int i = 0; i < addends.Length; i++)
            {
                sum += addends[i];
            }
                
            return sum;
        }
    }
}
