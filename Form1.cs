using System;
using System.Windows.Forms;

//Author: Auer Andreas
namespace Taschenrechner
{
    public partial class Form1 : Form
    {
        string input = "";
        double lastResult = 0;
        bool pow = false;
        bool sqrt = false;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button0_Click(object sender, EventArgs e)
        {
            input += "0";
            resultLabel.Text = input;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            input += "1";
            resultLabel.Text = input;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            input += "2";
            resultLabel.Text = input;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            input += "3";
            resultLabel.Text = input;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            input += "4";
            resultLabel.Text = input;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            input += "5";
            resultLabel.Text = input;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            input += "6";
            resultLabel.Text = input;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            input += "7";
            resultLabel.Text = input;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            input += "8";
            resultLabel.Text = input;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            input += "9";
            resultLabel.Text = input;
        }

        private void button23_Click(object sender, EventArgs e)
        {
            input += ",";
            resultLabel.Text = input;
        }

        private void buttonCos_Click(object sender, EventArgs e)
        {
            input += "cos(";
            resultLabel.Text = input;
        }

        private void buttonTan_Click(object sender, EventArgs e)
        {
            input += "tan(";
            resultLabel.Text = input;
        }

        private void buttonXPowMinOne_Click(object sender, EventArgs e)
        {
            //Ergebnis direkt ausgeben
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(Math.Pow(result, -1));
            resultLabel.Text = input;
        }

        private void buttonSin_Click(object sender, EventArgs e)
        {
            input += "sin(";
            resultLabel.Text = input;
        }

        private void buttonXPowY_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            pow = true;
            input = Convert.ToString(result) + "^";
            resultLabel.Text = input;
        }

        private void buttonYSqrtX_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            sqrt = true;
            input = Convert.ToString(result) + "sqrt(";
            resultLabel.Text = input;
        }

        private void buttonSqrt_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(Math.Sqrt(result));
            resultLabel.Text = input;
        }

        private void buttonXPowTwo_Click(object sender, EventArgs e)
        {
            //ergebnis direkt ausrechnen
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(Math.Pow(result, 2));
            resultLabel.Text = input;
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            if (getLastChar(input) == '-' | getLastChar(input) == '+' | getLastChar(input) == '*' | getLastChar(input) == '/')
            {
                input = changeLastChar(input, '*');
                resultLabel.Text = input;
            }
            else
            {
                input += "*";
                resultLabel.Text = input;
            }
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            if (getLastChar(input) == '-' | getLastChar(input) == '+' | getLastChar(input) == '*' | getLastChar(input) == '/')
            {
                input = changeLastChar(input, '/');
                resultLabel.Text = input;
            }
            else
            {
                input += "/";
                resultLabel.Text = input;
            }
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (getLastChar(input) == '-' | getLastChar(input) == '+' | getLastChar(input) == '*' | getLastChar(input) == '/')
            {
                input = changeLastChar(input, '+');
                resultLabel.Text = input;
            }
            else
            {
                input += "+";
                resultLabel.Text = input;
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (getLastChar(input) == '-' | getLastChar(input) == '+' | getLastChar(input) == '*' | getLastChar(input) == '/')
            {
                input = changeLastChar(input, '-');
                resultLabel.Text = input;
            }
            else
            {
                input += "-";
                resultLabel.Text = input;
            }
        }

        private void buttonBracketOpen_Click(object sender, EventArgs e)
        {
            input += "(";
            resultLabel.Text = input;
        }

        private void buttonBracketClose_Click(object sender, EventArgs e)
        {
            input += ")";
            resultLabel.Text = input;
        }

        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (input.Length > 0)
            {
                input = removeLastChar(input);
            }
            resultLabel.Text = input;
        }

        private void buttonEqual_Click(object sender, EventArgs e)
        {
            if (checkUserInput())
            {
                Calculator cal = new Calculator();
                double result = cal.Calculate(input, pow, sqrt);
                lastResult = result;
                resultLabel.Text = Convert.ToString(result);
                input = Convert.ToString(lastResult);
            }
            else
            {
                resultLabel.Text = "ERROR";
            }

            pow = false;
            sqrt = false;
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            input = "";
            resultLabel.Text = input;
        }

        private void button22_Click(object sender, EventArgs e)
        {
            input = negateLastNumber(input);
            resultLabel.Text = input;
        }

        public static string negateLastNumber(string input)
        {
            string numbers = "";
            char[] inputChar = input.ToCharArray();
            bool negative = false;
            _ = new char[0];

            for (int i = inputChar.Length - 1; i >= 0; i--)
            {
                if (inputChar[i] == '1' | inputChar[i] == '2' | inputChar[i] == '3' | inputChar[i] == '4' | inputChar[i] == '5' | inputChar[i] == '6' | inputChar[i] == '7' | inputChar[i] == '8' | inputChar[i] == '9' | inputChar[i] == '0' | inputChar[i] == ',')
                {
                    numbers += inputChar[i];
                }
                else if (inputChar[i] == '-')
                {
                    numbers += inputChar[i];
                    negative = true;
                    break;
                }
                else
                {
                    break;
                }
            }

            char[] numbersChar = numbers.ToCharArray();
            Array.Reverse(numbersChar);
            numbers = "";

            foreach (char item in numbersChar)
            {
                numbers += item;
            }

            double numbersDouble = Convert.ToDouble(numbers);
            numbersDouble *= -1;
            numbers = Convert.ToString(numbersDouble);
            char[] retchar;
            if (!negative)
            {
                retchar = new char[inputChar.Length - (numbers.Length - 1)];

                for (int i = 0; i < inputChar.Length - (numbers.Length - 1); i++)
                {
                    retchar[i] = inputChar[i];
                }
            }
            else
            {
                retchar = new char[inputChar.Length - numbers.Length];

                for (int i = 0; i < inputChar.Length - numbers.Length - 1; i++)
                {
                    retchar[i] = inputChar[i];
                }
            }

            string ret = "";

            foreach (char item in retchar)
            {
                if (item == '1' | item == '2' | item == '3' | item == '4' | item == '5' | item == '6' | item == '7' | item == '8' | item == '9' | item == '0' | item == ',' | item == '*' | item == '/' | item == '+' | item == '-')
                    ret += item;
            }

            ret += numbers;

            return ret;
        }

        public static string removeLastChar(string toRemove)
        {
            char[] remove = toRemove.ToCharArray();
            toRemove = "";
            char[] removed = new char[remove.Length - 1];
            for (int i = 0; i < remove.Length - 1; i++)
            {
                removed[i] = remove[i];
            }

            foreach (char item in removed)
            {
                toRemove += item;
            }
            return toRemove;
        }

        public static char getLastChar(string input)
        {
            char[] inputChar = input.ToCharArray();
            return inputChar[inputChar.Length - 1];
        }

        public static string changeLastChar(string input, char lastChar)
        {
            char[] inputChar = input.ToCharArray();
            inputChar[inputChar.Length - 1] = lastChar;
            input = "";
            foreach (char item in inputChar)
            {
                input += item;
            }
            return input;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(result - lastResult);
            resultLabel.Text = input;
        }

        private void buttonAns_Click(object sender, EventArgs e)
        {
            input += lastResult;
            resultLabel.Text = input;
        }

        private void buttonPi_Click(object sender, EventArgs e)
        {
            input += Math.PI;
            resultLabel.Text = input;
        }

        private void buttonLn_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(Math.Log(result));
            resultLabel.Text = input;
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(Math.Log10(result));
            resultLabel.Text = input;
        }

        private void buttonMPlus_Click(object sender, EventArgs e)
        {
            Calculator cal = new Calculator();
            double result = cal.Calculate(input, pow, sqrt);
            input = Convert.ToString(result + lastResult);
            resultLabel.Text = input;
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            resultLabel.Text = Convert.ToString(lastResult);
        }

        private bool checkUserInput()
        {
            int bracketsOpen = 0;
            int bracketsClose = 0;
            bool userInputCorrect = true;
            foreach (char item in input)
            {
                if (item == '(')
                {
                    bracketsOpen++;
                }
                else if (item == ')')
                {
                    bracketsClose++;
                }
            }

            if (bracketsClose > bracketsOpen)
            {
                userInputCorrect = false;
            }
            else if (bracketsOpen > bracketsClose)
            {
                for (int i = 0; i < bracketsOpen - bracketsClose; i++)
                {
                    input += ")";
                }
            }

            int start = 0;
            int end = 0;
            char[] inputChar = input.ToCharArray();

            for (int i = 0; i < inputChar.Length; i++)
            {
                inputChar = input.ToCharArray();
                int temp = i - 1;
                if(i == 0)
                {
                    temp = 0;
                }
                if (inputChar[i] == '(' && inputChar[i - 1] != 'n' && inputChar[i - 1] != 's' && i != 0)
                {
                    Calculator cal = new Calculator();
                    double[] numbers = cal.getNumberBeforeAndBehind(input, i, ref start, ref end);

                    if (numbers[0] > 0 | numbers[0] <= 0)
                    {
                        input = input.Insert(i, "*");
                        i++;
                    }
                }                
                else if (inputChar[i] == 'c' | inputChar[i] == 's' | inputChar[i] == 't' && inputChar[temp] != 'o' && i != 0)
                {
                    Calculator cal = new Calculator();
                    double[] numbers = cal.getNumberBeforeAndBehind(input, i, ref start, ref end);

                    if (numbers[0] > 0 | numbers[0] <= 0)
                    {
                        input = input.Insert(i, "*");
                        i++;
                    }
                }
            }

            inputChar = input.ToCharArray();

            //check for two points
            bool comma = false;

            foreach (char item in inputChar)
            {
                if (item == '+' | item == '-' | item == '*' | item == '/' | item == '(' | item == 's' | item == 'c' | item == 't')
                {
                    comma = false;
                }
                else if (item == ',')
                {
                    if (comma == true)
                    {
                        userInputCorrect = false;
                    }
                    else
                    {
                        comma = true;
                    }
                }
            }

            int j = 0;
            foreach (char item in inputChar)
            {
                Calculator cal = new Calculator();

                if (item == '/')
                {
                    double[] numbers = cal.getNumberBeforeAndBehind(input, j, ref start, ref end);

                    if (numbers[1] == 0)
                    {
                        userInputCorrect = false;
                    }
                }
                j++;
            }

            return userInputCorrect;
        }
    }
}
