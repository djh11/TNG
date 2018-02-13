using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorProgram
{
    public class CalcInputParser
    {
        private List<String> delimiters;

        public List<String> Delimiters
        {
            get { return delimiters; }
            set { delimiters = value; }
        }

        private int[] addends;

        public int[] Addends
        {
            get { return addends; }
            set { addends = value; }
        }

        public int[] ParseStringInputToAddends(string input = "")
        {
            //business rule: if empty string, return 0
            if(CheckForEmptyInput(input))
            {
                addends = new int[1];
                addends[0] = 0;
            }

            //business rule: allow user to input custom delimiters to separate inputs
            else if (CheckForCustomDelimiter(input))
            {
                delimiters = new List<string>();
                delimiters = ParseForCustomDelimiters(input);
                if (!CheckDelimiterValidity(delimiters))
                {
                    throw new ArgumentException("Delimiter cannot be a number or '-'.");
                }
                input = TrimDelimiterHeaderFromInput(input, delimiters);
                string[] tempStrings = SplitOnDelimiters(input, delimiters);
                addends = new int[tempStrings.Length];
                addends = ParseStringArrayAndCheckForTrash(tempStrings);

            }

            //but don't require it, set default delimiter to ";"
            else
            {
                delimiters = new List<string>();
                delimiters.Add(",");
                input = ChangeLineBreakToDefaultDelimiter(input, delimiters[0]);
                string[] tempStrings = SplitOnDelimiters(input, delimiters);
                addends = new int[tempStrings.Length];
                addends = ParseStringArrayAndCheckForTrash(tempStrings);
            }

            addends = ThrowOutNumbersGreaterThan1000(addends);

            return addends;
        }

        private string TrimDelimiterHeaderFromInput(string input, List<String> delimiters)
        {
            string[] temp = input.Split("\n");
            if(temp.Length > 2)
            {
                throw new ArgumentException("Cannot use line break with a custom delimiter");
            }
            else
            {
                return temp[1];
            }

        }

        private List<string> ParseForCustomDelimiters(string input)
        {
            List<string> customDelimiters = new List<string>();
            string[] temp = input.Split("\n");
            string delimHeader = temp[0];
            string[] temp2 = delimHeader.Split("][");

            foreach(string delim in temp2)
            {
                bool isSubstringOrDupe = customDelimiters.Any(s => s.Contains(delim));
                if (!isSubstringOrDupe)
                {
                    customDelimiters.Add(delim);
                }
            }
            for(int i = 0; i < customDelimiters.Count; i++)
            {
                customDelimiters[i] = customDelimiters[i].Replace("//", "");
                customDelimiters[i] = customDelimiters[i].Replace("[", "");
                customDelimiters[i] = customDelimiters[i].Replace("]", "");
            }

            return customDelimiters;
        }

        private bool CheckForEmptyInput(string input)
        {
            if(input == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        
        private bool CheckForCustomDelimiter(string input)
        {
            if(input[0] == '/')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckDelimiterValidity(List<String> delimiter)
        {
            bool delimiterValidity = true;
            for(int i = 0; i < delimiter.Count; i++)
            {
                char[] temp = delimiter[i].ToCharArray();
                for (int j = 0; j < temp.Length; j++)
                {
                    if (!Char.IsDigit(temp[j]))
                    {
                        continue;
                    }
                    else
                    {
                        delimiterValidity = false;
                    }
                }
            }

            return delimiterValidity;
        }
        
        private string ChangeLineBreakToDefaultDelimiter(string input, string delimiter)
        {
            if (delimiter == ",")
            {
                input = input.Replace("\n", delimiter);
            }

            return input;
        }

        private string[] SplitOnDelimiters(string input, List<String> delimiter)
        {
            if (delimiter[0] == ",")
            {
                input = input.Replace("\n", delimiter[0]);
            }
            else
            {
                foreach (string delim in delimiter)
                {
                    if (delim == "-")
                    {
                        input = input.Replace("--", "-negative");
                        input = input.Replace("-", ",");
                        input = input.Replace("negative", "-");
                    }
                    else
                    {
                        input = input.Replace(delim, ",");
                    }
                }
            }
            return input.Split(",");
        }

        private bool CheckForHyphenAsDelimiter(List<string> delimiter)
        {
            bool hyphenAsDelimiter = false;
            foreach (string delim in delimiter)
            {
                if(delim == "-")
                {
                    hyphenAsDelimiter = true;
                }
                else
                {
                    continue;
                }
            }

            return hyphenAsDelimiter;
        }

        private int[] ParseStringArrayAndCheckForTrash(string[] input)
        {
            int[] results = new int[input.Length];

            for(int i = 0; i < input.Length; i++)
            {
                if(input[i] == "")
                {
                    input[i] = "0";
                }

                bool success = Int32.TryParse(input[i], out results[i]);
                if (!success)
                {
                    throw new ArgumentException("trash input");
                }
            }

            return results;
        }
        
        private int[] ThrowOutNumbersGreaterThan1000(int[] oldAddends)
        {
            int[] newAddends = new int[oldAddends.Length];
            for(int i = 0; i < oldAddends.Length; i++)
            {
                if(oldAddends[i] > 1000)
                {
                    newAddends[i] = 0;
                }
                else
                {
                    newAddends[i] = oldAddends[i];
                }
            }
            
            return newAddends;
        }

    }
}
