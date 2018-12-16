using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToIntOrNotToInt
{
    public class MyDigitMethod
    {
        private Dictionary<char, int> digitSymbols = new Dictionary<char, int>
            {
                { '.', 0 },
                { '+', 0 },
                { '-', 0 },
                { 'e', 0 },
                { '0', 0 },
                { '1', 0 },
                { '2', 0 },
                { '3', 0 },
                { '4', 0 },
                { '5', 0 },
                { '6', 0 },
                { '7', 0 },
                { '8', 0 },
                { '9', 0 },
            };

        public bool IsDigit(string inputStr)
        {
            this.ZeroDigitCollection();
            if (inputStr == null)
            {
                return false;
            }

            string str = this.ChangeCommaAndE(inputStr);
            if (!this.InputControl(str, this.digitSymbols))
            {
                return false;
            }

            if (!this.LightAnalysis(str))
            {
                return false;
            }

            string transformStr = this.TransformStr(str, this.digitSymbols);
            if (this.LastStepAnalysis(transformStr))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void ZeroDigitCollection()
        {
            var keys = this.digitSymbols.Keys.ToArray();
            foreach (var item in keys)
            {
                this.digitSymbols[item] = 0;
            }
        }

        private bool LastStepAnalysis(string inputStr)
        {
            char[] inputArray = inputStr.ToCharArray();
            int index = Array.IndexOf(inputArray, 'e');
            if (this.digitSymbols['e'] == 1)
            {
                if (Array.IndexOf(inputArray, 'e') == inputArray.Length)
                {
                    return false;
                }

                int degree;
                if (this.digitSymbols['-'] == 1)
                {
                    if (Array.IndexOf(inputArray, '-') == inputArray.Length)
                    {
                        return false;
                    }

                    degree = ~this.MyParse(inputStr.Substring(Array.IndexOf(inputArray, '-') + 1)) + 1;
                }
                else
                {
                    degree = this.MyParse(inputStr.Substring(Array.IndexOf(inputArray, 'e') + 1));
                }

                if (degree < 0)
                {
                    if (this.digitSymbols['.'] == 1)
                    {
                        if (Array.IndexOf(inputArray, 'e') - Array.IndexOf(inputArray, '.') != 1)
                        {
                            return false;
                        }

                        index = Array.IndexOf(inputArray, '.');
                    }

                    if (index < degree * (-1))
                    {
                        return false;
                    }

                    for (int i = 0; i < degree * (-1); i++)
                    {
                        if (inputArray[index - i - 1] != '0')
                        {
                            return false;
                        }
                    }

                    return true;
                }

                if (degree == 0)
                {
                    return true;
                }

                if (degree > 0)
                {
                    if (this.digitSymbols['.'] == 1)
                    {
                        if (Array.IndexOf(inputArray, 'e') - Array.IndexOf(inputArray, '.') - 1 > degree)
                        {
                            return false;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }
            }

            if (this.digitSymbols['.'] == 1)
            {
                if (Array.IndexOf(inputArray, '.') != inputArray.Length - 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return true;
        }

        private int MyParse(string input)
        {
            int tenDegree = 10;
            int result = 0;
            int tempStep = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                switch (input[i])
                {
                    case '0':
                        tempStep = 0;
                        break;
                    case '1':
                        tempStep = 1;
                        break;
                    case '2':
                        tempStep = 2;
                        break;
                    case '3':
                        tempStep = 3;
                        break;
                    case '4':
                        tempStep = 4;
                        break;
                    case '5':
                        tempStep = 5;
                        break;
                    case '6':
                        tempStep = 6;
                        break;
                    case '7':
                        tempStep = 7;
                        break;
                    case '8':
                        tempStep = 8;
                        break;
                    case '9':
                        tempStep = 9;
                        break;
                }

                if (i == input.Length - 1)
                {
                    result = tempStep;
                }
                else
                {
                    result += tenDegree * tempStep;
                    tenDegree *= 10;
                }
            }

            return result;
        }

        private string ChangeCommaAndE(string inputStr)
        {
            var str = new StringBuilder(inputStr.Length);
            int i = -1;
            while (i < inputStr.Length - 1)
            {
                i++;
                if (inputStr[i] == ',')
                {
                    str.Append(".");
                    i++;
                }

                if (inputStr[i] == 'E')
                {
                    str.Append("e");
                    i++;
                }

                str.Append(inputStr[i]);
            }

            return str.ToString();
        }

        private bool LightAnalysis(string inputStr)
        {
            bool notDigit = this.digitSymbols['.'] > 1 ||
                this.digitSymbols['-'] > 1 ||
                this.digitSymbols['+'] > 2 ||
                this.digitSymbols['e'] > 1;
            bool numberExist = this.digitSymbols['0'] +
                this.digitSymbols['1'] +
                this.digitSymbols['2'] +
                this.digitSymbols['3'] +
                this.digitSymbols['4'] +
                this.digitSymbols['5'] +
                this.digitSymbols['6'] +
                this.digitSymbols['7'] +
                this.digitSymbols['8'] +
                this.digitSymbols['9'] > 0;
            bool numberExistWithoutZero = this.digitSymbols['1'] +
                this.digitSymbols['2'] +
                this.digitSymbols['3'] +
                this.digitSymbols['4'] +
                this.digitSymbols['5'] +
                this.digitSymbols['6'] +
                this.digitSymbols['7'] +
                this.digitSymbols['8'] +
                this.digitSymbols['9'] == 0;

            if (notDigit || !numberExist)
            {
                return false;
            }

            char[] str = inputStr.ToCharArray();
            if (this.digitSymbols['+'] == 2)
            {
                if (this.digitSymbols['e'] == 0)
                {
                    return false;
                }
                else
                {
                    if (str[0] != '+')
                    {
                        return false;
                    }
                    else
                    {
                        if (Array.LastIndexOf(str, '+') - Array.IndexOf(str, 'e') != 1)
                        {
                            return false;
                        }
                    }
                }
            }

            if (this.digitSymbols['+'] == 1)
            {
                if (str[0] != '+')
                {
                    if (this.digitSymbols['e'] == 0)
                    {
                        return false;
                    }
                    else
                    {
                        if (Array.LastIndexOf(str, '+') - Array.IndexOf(str, 'e') != 1)
                        {
                            return false;
                        }
                    }
                }
            }

            if (this.digitSymbols['-'] == 1)
            {
                if (this.digitSymbols['e'] == 0)
                {
                    return false;
                }
                else
                {
                    if (Array.IndexOf(str, '-') - Array.IndexOf(str, 'e') != 1)
                    {
                        return false;
                    }
                }
            }

            if (this.digitSymbols['.'] == 1)
            {
                if (this.digitSymbols['e'] == 1 &&
                    Array.IndexOf(str, 'e') - Array.IndexOf(str, '.') < 0)
                {
                    return false;
                }
            }

            if (this.digitSymbols['e'] == 1)
            {
                if (str[0] == 'e' || str[str.Length - 1] == 'e')
                {
                    return false;
                }
            }

            if (numberExistWithoutZero)
            {
                return false;
            }

            return true;
        }

        private bool AnalysisE(string transformStr)
        {
            string[] str = transformStr.Split('e');
            if (str[0].Contains('-'))
            {
                return false;
            }

            if (str[1].Contains('.'))
            {
                return false;
            }

            if (str[0].Contains('.'))
            {
                int positionAfterComma = 0;
                for (int i = Array.IndexOf(str, '.'); i < str[0].Length; i++)
                {
                    if (str[0][i] != 0)
                    {
                        positionAfterComma = i - Array.IndexOf(str, '.');
                    }
                }
            }

            return false;
        }

        private string TransformStr(string inputStr, Dictionary<char, int> digitSymbols)
        {
            int numbersAfterComma = 0;
            var str = new StringBuilder(inputStr.Length);
            int i = 0;
            if (inputStr[0] == '+')
            {
                i++;
                digitSymbols['+']--;
            }

            if (digitSymbols['.'] == 1)
            {
                int commaIndex = Array.IndexOf(inputStr.ToCharArray(), '.');
                while (i <= commaIndex)
                {
                    str.Append(inputStr[i]);
                    i++;
                }

                for (int k = inputStr.Length - 1; k >= i; k--)
                {
                    if (numbersAfterComma == 0 && inputStr[k] != '0')
                    {
                        numbersAfterComma = k;
                    }

                    if (inputStr[k] == 'e')
                    {
                        numbersAfterComma = 0;
                    }
                }

                while (i <= numbersAfterComma)
                {
                    str.Append(inputStr[i]);
                    i++;
                }

                if (digitSymbols['e'] == 1)
                {
                    int indexE = Array.IndexOf(inputStr.ToCharArray(), 'e');
                    str.Append(inputStr[indexE]);
                    i = indexE + 1;
                    if (inputStr[i] == '+')
                    {
                        i++;
                        digitSymbols['+']--;
                    }

                    for (; i < inputStr.Length; i++)
                    {
                        str.Append(inputStr[i]);
                    }
                }
            }
            else
            {
                while (i < inputStr.Length)
                {
                    if (inputStr[i] == '+')
                    {
                        i++;
                        digitSymbols['+']--;
                    }
                    else
                    {
                        str.Append(inputStr[i]);
                        i++;
                    }
                }
            }

            return str.ToString();
        }

        private bool InputControl(string str, Dictionary<char, int> digitSymbols)
        {
            foreach (var symbol in str)
            {
                if (digitSymbols.ContainsKey(symbol))
                {
                    digitSymbols[symbol]++;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
    }
}
