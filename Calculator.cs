using System;

//Author: Auer Andreas
namespace Taschenrechner
{
    class Calculator
    {
        public double Calculate(string toCalculate, bool pow, bool sqrt)
        {
            double result = 0;
            bool finished = false;
            bool finishBrackets = false;

            if (pow == true)
            {
                int positionFunktion = 0;
                int start = 0, end = toCalculate.Length;
                for (int i = 0; i < toCalculate.Length; i++)
                {
                    if (toCalculate[i] == '^')
                    {
                        positionFunktion = i;
                    }
                }
                double[] numbers = getNumberBeforeAndBehind(toCalculate, positionFunktion, ref start, ref end);

                result = Math.Pow(numbers[0], numbers[1]);

                return result;
            }
            else if (sqrt == true)
            {
                int positionFunktionBehind = 0, positionFunktionBefore = 0;
                int start = 0, end = toCalculate.Length;
                for (int i = 0; i < toCalculate.Length; i++)
                {
                    if (toCalculate[i] == 'q')
                    {
                        positionFunktionBehind = i + 3;
                        positionFunktionBefore = i - 1;
                        break;
                    }
                }
                double[] numberBehind = getNumberBeforeAndBehind(toCalculate, positionFunktionBehind, ref start, ref end);
                double[] numberBefore = getNumberBeforeAndBehind(toCalculate, positionFunktionBefore, ref start, ref end);

                result = Math.Pow(numberBehind[1], 1 / numberBefore[0]);

                return result;
            }

            while (!finished)
            {
                int sinStart = -1;
                int cosStart = -1;
                int tanStart = -1;
                int bracketsOpen = -1;
                int bracketsClose = -1;
                int multiply = -1;
                int divide = -1;
                int plus = -1;
                int minus = -1;
                int multiplyBrackets = -1;
                int divideBrackets = -1;
                int plusBrackets = -1;
                int minusBrackets = -1;

                //checking for the different operations
                bool trigonometric = checkForTrigonometricFunktion(toCalculate, ref sinStart, ref cosStart, ref tanStart);
                bool plusOrMinus = checkForMinusAndPlus(toCalculate, ref plus, ref minus);
                bool multiplyOrDivide = checkForMultiplyAndDivide(toCalculate, ref multiply, ref divide);
                bool brackets = checkForBrackets(toCalculate, ref bracketsOpen, ref bracketsClose);

                //calculates the trigonometric funktion (sin, cos, tan) if there exists one

                // calculates the sum between brackets
                if (brackets && !finishBrackets)
                {
                    string betweenBrackets = "";
                    for (int i = bracketsOpen + 1; i < bracketsClose; i++)
                    {
                        betweenBrackets += toCalculate[i];
                    }

                    if (checkForMultiplyAndDivide(betweenBrackets, ref multiplyBrackets, ref divideBrackets))
                    {
                        int start = 0;
                        int end = 0;

                        if (multiplyBrackets != -1)
                        {
                            double[] numbersToMultiply = getNumberBeforeAndBehind(betweenBrackets, multiplyBrackets, ref start, ref end);
                            char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                            double interimResult = numbersToMultiply[0] * numbersToMultiply[1];
                            char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                            int j = 0;

                            for (int i = start; i <= end; i++)
                            {
                                betweenBracketsChar[i] = '\0';
                            }

                            for (int i = start; i < interimResultChar.Length + start; i++)
                            {
                                betweenBracketsChar[i] = interimResultChar[j];
                                j++;
                            }

                            betweenBrackets = "";
                            foreach (char charItem in betweenBracketsChar)
                            {
                                if (charItem != '\0')
                                    betweenBrackets += charItem;
                            }
                        }
                        else if (divideBrackets != -1)
                        {
                            double[] numbersToMultiply = getNumberBeforeAndBehind(betweenBrackets, divideBrackets, ref start, ref end);
                            char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                            double interimResult = numbersToMultiply[0] / numbersToMultiply[1];
                            char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                            int j = 0;

                            for (int i = start; i <= end; i++)
                            {
                                betweenBracketsChar[i] = '\0';
                            }

                            for (int i = start; i < interimResultChar.Length + start; i++)
                            {
                                betweenBracketsChar[i] = interimResultChar[j];
                                j++;
                            }

                            betweenBrackets = "";
                            foreach (char charItem in betweenBracketsChar)
                            {
                                if (charItem != '\0')
                                    betweenBrackets += charItem;
                            }
                        }
                    }
                    else if (checkForMinusAndPlus(betweenBrackets, ref plusBrackets, ref minusBrackets))
                    {
                        int start = 0;
                        int end = 0;

                        if (plusBrackets != -1)
                        {
                            double[] numbersToAdd = getNumberBeforeAndBehind(betweenBrackets, plusBrackets, ref start, ref end);
                            char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                            double interimResult = numbersToAdd[0] + numbersToAdd[1];
                            char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                            int j = 0;

                            for (int i = start; i <= end; i++)
                            {
                                betweenBracketsChar[i] = '\0';
                            }

                            for (int i = start; i < interimResultChar.Length + start; i++)
                            {
                                betweenBracketsChar[i] = interimResultChar[j];
                                j++;
                            }

                            betweenBrackets = "";
                            foreach (char charItem in betweenBracketsChar)
                            {
                                if (charItem != '\0')
                                    betweenBrackets += charItem;
                            }
                        }
                        else if (minusBrackets != -1)
                        {
                            double[] numbersToSubtract = getNumberBeforeAndBehind(betweenBrackets, minusBrackets, ref start, ref end);
                            if (numbersToSubtract[0] >= 0)
                            {
                                char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                                double interimResult = numbersToSubtract[0] - numbersToSubtract[1];
                                char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                int j = 0;

                                for (int i = start; i <= end; i++)
                                {
                                    betweenBracketsChar[i] = '\0';
                                }

                                for (int i = start; i < interimResultChar.Length + start; i++)
                                {
                                    betweenBracketsChar[i] = interimResultChar[j];
                                    j++;
                                }

                                betweenBrackets = "";
                                foreach (char charItem in betweenBracketsChar)
                                {
                                    if (charItem != '\0')
                                        betweenBrackets += charItem;
                                }
                            }
                            else
                            {
                                char[] toCalculateChar = betweenBrackets.ToCharArray();
                                double interimResult = numbersToSubtract[0];
                                char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                int j = 0;

                                for (int i = start; i <= end; i++)
                                {
                                    toCalculateChar[i] = '\0';
                                }

                                for (int i = start; i < toCalculateChar.Length + start; i++)
                                {
                                    toCalculateChar[i] = interimResultChar[j];
                                    j++;
                                }

                                betweenBrackets = "";
                                foreach (char charItem in toCalculateChar)
                                {
                                    if (charItem != '\0')
                                        betweenBrackets += charItem;
                                }
                            }
                        }
                    }
                    char[] toChalculateCh = toCalculate.ToCharArray();
                    if(toChalculateCh[bracketsOpen - 1] == 'n' | toChalculateCh[bracketsOpen - 1] == 's' && !betweenBrackets.Contains("+") && !betweenBrackets.Contains("-") && !betweenBrackets.Contains("*") && !betweenBrackets.Contains("/")) 
                    { 
                        finishBrackets = true;
                    }
                    else if (toChalculateCh[bracketsOpen - 1] == 'n' | toChalculateCh[bracketsOpen - 1] == 's' | betweenBrackets.Contains("+") | betweenBrackets.Contains("-") | betweenBrackets.Contains("*") | betweenBrackets.Contains("/"))
                    {
                        toCalculate = toCalculate.Remove(bracketsOpen + 1, bracketsClose - bracketsOpen - 1);
                        toCalculate = toCalculate.Insert(bracketsOpen + 1, betweenBrackets);
                    }
                    else
                    {
                        toCalculate = toCalculate.Remove(bracketsOpen, bracketsClose - bracketsOpen + 1);
                        toCalculate = toCalculate.Insert(bracketsOpen, betweenBrackets);
                    }
                }
                else if (trigonometric)
                {
                    bool finishedTrigonometric = false;
                    string betweenBrackets = "";

                    while (!finishedTrigonometric)
                    {
                        betweenBrackets = "";
                        multiplyBrackets = -1;
                        divideBrackets = -1;
                        plusBrackets = -1;
                        minusBrackets = -1;

                        checkForMinusAndPlus(toCalculate, ref plus, ref minus);
                        checkForMultiplyAndDivide(toCalculate, ref multiply, ref divide);
                        checkForBrackets(toCalculate, ref bracketsOpen, ref bracketsClose);

                        if (sinStart != -1)
                        {
                            bracketsOpen = sinStart + 1;
                        }
                        else if (cosStart != -1)
                        {
                            bracketsOpen = cosStart + 1;
                        }
                        else if (tanStart != -1)
                        {
                            bracketsOpen = tanStart + 1;
                        }

                        for (int i = bracketsOpen + 2; i < bracketsClose; i++)
                        {
                            betweenBrackets += toCalculate[i];
                        }

                        string betweenBracketsOriginal = betweenBrackets;

                        if (checkForMultiplyAndDivide(betweenBrackets, ref multiplyBrackets, ref divideBrackets))
                        {
                            int start = 0;
                            int end = 0;

                            if (multiplyBrackets != -1)
                            {
                                double[] numbersToMultiply = getNumberBeforeAndBehind(betweenBrackets, multiplyBrackets, ref start, ref end);
                                char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                                double interimResult = numbersToMultiply[0] * numbersToMultiply[1];
                                char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                int j = 0;

                                for (int i = start; i <= end; i++)
                                {
                                    betweenBracketsChar[i] = '\0';
                                }

                                for (int i = start; i < interimResultChar.Length + start; i++)
                                {
                                    betweenBracketsChar[i] = interimResultChar[j];
                                    j++;
                                }

                                betweenBrackets = "";
                                foreach (char charItem in betweenBracketsChar)
                                {
                                    if (charItem != '\0')
                                        betweenBrackets += charItem;
                                }
                            }
                            else if (divideBrackets != -1)
                            {
                                double[] numbersToMultiply = getNumberBeforeAndBehind(betweenBrackets, divideBrackets, ref start, ref end);
                                char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                                double interimResult = numbersToMultiply[0] / numbersToMultiply[1];
                                char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                int j = 0;

                                for (int i = start; i <= end; i++)
                                {
                                    betweenBracketsChar[i] = '\0';
                                }

                                for (int i = start; i < interimResultChar.Length + start; i++)
                                {
                                    betweenBracketsChar[i] = interimResultChar[j];
                                    j++;
                                }

                                betweenBrackets = "";
                                foreach (char charItem in betweenBracketsChar)
                                {
                                    if (charItem != '\0')
                                        betweenBrackets += charItem;
                                }

                            }
                            toCalculate = toCalculate.Replace(betweenBracketsOriginal, betweenBrackets);
                        }
                        else if (checkForMinusAndPlus(betweenBrackets, ref plusBrackets, ref minusBrackets))
                        {
                            int start = 0;
                            int end = 0;

                            if (plusBrackets != -1)
                            {
                                double[] numbersToAdd = getNumberBeforeAndBehind(betweenBrackets, plusBrackets, ref start, ref end);
                                char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                                double interimResult = numbersToAdd[0] + numbersToAdd[1];
                                char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                int j = 0;

                                for (int i = start; i <= end; i++)
                                {
                                    betweenBracketsChar[i] = '\0';
                                }

                                for (int i = start; i < interimResultChar.Length + start; i++)
                                {
                                    betweenBracketsChar[i] = interimResultChar[j];
                                    j++;
                                }

                                betweenBrackets = "";
                                foreach (char charItem in betweenBracketsChar)
                                {
                                    if (charItem != '\0')
                                        betweenBrackets += charItem;
                                }
                            }
                            else if (minusBrackets != -1)
                            {
                                double[] numbersToSubtract = getNumberBeforeAndBehind(betweenBrackets, minusBrackets, ref start, ref end);
                                if (numbersToSubtract[0] >= 0)
                                {
                                    char[] betweenBracketsChar = betweenBrackets.ToCharArray();
                                    double interimResult = numbersToSubtract[0] - numbersToSubtract[1];
                                    char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                    int j = 0;

                                    for (int i = start; i <= end; i++)
                                    {
                                        betweenBracketsChar[i] = '\0';
                                    }

                                    for (int i = start; i < interimResultChar.Length + start; i++)
                                    {
                                        betweenBracketsChar[i] = interimResultChar[j];
                                        j++;
                                    }

                                    betweenBrackets = "";
                                    foreach (char charItem in betweenBracketsChar)
                                    {
                                        if (charItem != '\0')
                                            betweenBrackets += charItem;
                                    }
                                }
                                else
                                {
                                    char[] toCalculateChar = betweenBrackets.ToCharArray();
                                    double interimResult = numbersToSubtract[0];
                                    char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                                    int j = 0;

                                    for (int i = start; i <= end; i++)
                                    {
                                        toCalculateChar[i] = '\0';
                                    }

                                    for (int i = start; i < toCalculateChar.Length + start; i++)
                                    {
                                        toCalculateChar[i] = interimResultChar[j];
                                        j++;
                                    }

                                    betweenBrackets = "";
                                    foreach (char charItem in toCalculateChar)
                                    {
                                        if (charItem != '\0')
                                            betweenBrackets += charItem;
                                    }
                                }
                            }
                            toCalculate = toCalculate.Replace(betweenBracketsOriginal, betweenBrackets);
                        }
                        else
                        {
                            finishedTrigonometric = true;
                        }
                    }
                    if (sinStart != -1)
                    {
                        betweenBrackets = Convert.ToString(Math.Sin(Convert.ToDouble(betweenBrackets)));
                    }
                    else if (cosStart != -1)
                    {
                        betweenBrackets = Convert.ToString(Math.Cos(Convert.ToDouble(betweenBrackets)));
                    }
                    else if (tanStart != -1)
                    {
                        betweenBrackets = Convert.ToString(Math.Tan(Convert.ToDouble(betweenBrackets)));
                    }

                    toCalculate = toCalculate.Remove(bracketsOpen - 2, bracketsClose - bracketsOpen + 1 + 2);
                    toCalculate = toCalculate.Insert(bracketsOpen - 2, betweenBrackets);
                }

                //calculates a multiplication or a division
                else if (multiplyOrDivide)
                {
                    int start = 0;
                    int end = 0;

                    if (multiply != -1)
                    {
                        double[] numbersToMultiply = getNumberBeforeAndBehind(toCalculate, multiply, ref start, ref end);
                        char[] toCalculateChar = toCalculate.ToCharArray();
                        double interimResult = numbersToMultiply[0] * numbersToMultiply[1];
                        char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                        int j = 0;

                        for (int i = start; i <= end; i++)
                        {
                            toCalculateChar[i] = '\0';
                        }

                        for (int i = start; i < toCalculate.Length; i++)
                        {
                            if (j < interimResultChar.Length)
                            {
                                toCalculateChar[i] = interimResultChar[j];
                                j++;
                            }
                        }

                        toCalculate = "";
                        foreach (char charItem in toCalculateChar)
                        {
                            if (charItem != '\0')
                                toCalculate += charItem;
                        }
                    }
                    else if (divide != -1)
                    {
                        double[] numbersToMultiply = getNumberBeforeAndBehind(toCalculate, divide, ref start, ref end);
                        char[] toCalculateChar = toCalculate.ToCharArray();
                        double interimResult = numbersToMultiply[0] / numbersToMultiply[1];
                        char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                        int j = 0;

                        for (int i = start; i <= end; i++)
                        {
                            toCalculateChar[i] = '\0';
                        }

                        for (int i = start; i < toCalculateChar.Length; i++)
                        {
                            if (j < interimResultChar.Length)
                            {
                                toCalculateChar[i] = interimResultChar[j];
                                j++;
                            }
                        }

                        toCalculate = "";
                        foreach (char charItem in toCalculateChar)
                        {
                            if (charItem != '\0')
                                toCalculate += charItem;
                        }
                    }
                }

                // calculates a addition or a subtraction
                else if (plusOrMinus)
                {
                    int start = 0;
                    int end = 0;

                    if (plus != -1)
                    {
                        double[] numbersToAdd = getNumberBeforeAndBehind(toCalculate, plus, ref start, ref end);
                        char[] toCalculateChar = toCalculate.ToCharArray();
                        double interimResult = numbersToAdd[0] + numbersToAdd[1];
                        char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                        int j = 0;

                        for (int i = start; i <= end; i++)
                        {
                            toCalculateChar[i] = '\0';
                        }

                        for (int i = start; i < toCalculateChar.Length; i++)
                        {
                            if (j < interimResultChar.Length)
                            {
                                toCalculateChar[i] = interimResultChar[j];
                                j++;
                            }
                        }

                        toCalculate = "";
                        foreach (char charItem in toCalculateChar)
                        {
                            if (charItem != '\0')
                                toCalculate += charItem;
                        }
                    }
                    else if (minus != -1)
                    {
                        double[] numbersToSubtract = getNumberBeforeAndBehind(toCalculate, minus, ref start, ref end);
                        if (numbersToSubtract[0] >= 0)
                        {
                            char[] toCalculateChar = toCalculate.ToCharArray();
                            double interimResult = numbersToSubtract[0] - numbersToSubtract[1];
                            char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                            int j = 0;

                            for (int i = start; i <= end; i++)
                            {
                                toCalculateChar[i] = '\0';
                            }

                            for (int i = start; i < toCalculateChar.Length; i++)
                            {
                                if (j < interimResultChar.Length)
                                {
                                    toCalculateChar[i] = interimResultChar[j];
                                    j++;
                                }
                            }

                            toCalculate = "";
                            foreach (char charItem in toCalculateChar)
                            {
                                if (charItem != '\0')
                                    toCalculate += charItem;
                            }
                        }
                        else
                        {
                            char[] toCalculateChar = toCalculate.ToCharArray();
                            double interimResult = numbersToSubtract[0];
                            char[] interimResultChar = (Convert.ToString(interimResult)).ToCharArray();
                            int j = 0;

                            for (int i = start; i <= end; i++)
                            {
                                toCalculateChar[i] = '\0';
                            }

                            for (int i = start; i < interimResultChar.Length + start; i++)
                            {
                                toCalculateChar[i] = interimResultChar[j];
                                j++;
                            }

                            toCalculate = "";
                            foreach (char charItem in toCalculateChar)
                            {
                                if (charItem != '\0')
                                    toCalculate += charItem;
                            }
                        }
                    }
                }
                else
                {
                    finished = true;
                }
            }
            result = Convert.ToDouble(toCalculate);
            return result;
        }

        private bool checkForTrigonometricFunktion(string toCheck, ref int sinStart, ref int cosStart, ref int tanStart)
        {
            char[] toCheckChar = toCheck.ToCharArray();
            bool exist = false;

            for (int i = toCheckChar.Length - 1; i > 0; i--)
            {
                if (toCheckChar[i] == 'i')
                {
                    sinStart = i;
                    exist = true;
                    break;
                }
                else if (toCheckChar[i] == 'o')
                {
                    cosStart = i;
                    exist = true;
                    break;
                }
                else if (toCheckChar[i] == 'a')
                {
                    tanStart = i;
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private bool checkForBrackets(string toCheck, ref int bracketsOpen, ref int bracketsClose, int start = 0)
        {
            char[] toCheckChar = toCheck.ToCharArray();
            bool exist = false;

            for (int i = start; i < toCheckChar.Length; i++)
            {
                if (toCheckChar[i] == '(')
                {
                    bracketsOpen = i;
                    exist = true;
                }
                else if (toCheckChar[i] == ')')
                {
                    bracketsClose = i;
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private bool checkForMultiplyAndDivide(string toCheck, ref int multiply, ref int divide)
        {
            char[] toCheckChar = toCheck.ToCharArray();
            bool exist = false;
            for (int i = 0; i < toCheckChar.Length; i++)
            {
                if (toCheckChar[i] == '*')
                {
                    multiply = i;
                    exist = true;
                    break;
                }
                else if (toCheckChar[i] == '/')
                {
                    divide = i;
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        private bool checkForMinusAndPlus(string toCheck, ref int plus, ref int minus)
        {
            char[] toCheckChar = toCheck.ToCharArray();
            bool exist = false;
            for (int i = 0; i < toCheckChar.Length; i++)
            {
                if (toCheckChar[i] == '+' && i != 0)
                {
                    plus = i;
                    exist = true;
                    break;
                }
                else if (toCheckChar[i] == '-' && i != 0)
                {
                    minus = i;
                    exist = true;
                    break;
                }
            }
            return exist;
        }

        public double[] getNumberBeforeAndBehind(string input, int positionFunktion, ref int start, ref int end)
        {
            char[] inputChar = input.ToCharArray();
            string before = "";
            string behind = "";
            double[] retArray = new double[2];

            for (int i = positionFunktion - 1; i >= 0; i--)
            {
                if (inputChar[i] == '1' | inputChar[i] == '2' | inputChar[i] == '3' | inputChar[i] == '4' | inputChar[i] == '5' | inputChar[i] == '6' | inputChar[i] == '7' | inputChar[i] == '8' | inputChar[i] == '9' | inputChar[i] == '0' | inputChar[i] == ',' | inputChar[i] == '-')
                {
                    before += inputChar[i];
                }
                else
                {
                    break;
                }
                start = i;
            }

            for (int i = positionFunktion + 1; i < inputChar.Length; i++)
            {
                if (inputChar[i] == '1' | inputChar[i] == '2' | inputChar[i] == '3' | inputChar[i] == '4' | inputChar[i] == '5' | inputChar[i] == '6' | inputChar[i] == '7' | inputChar[i] == '8' | inputChar[i] == '9' | inputChar[i] == '0' | inputChar[i] == ',' | inputChar[i] == '-')
                {
                    behind += inputChar[i];
                }
                else
                {
                    break;
                }
                end = i;
            }

            char[] beforeChar = before.ToCharArray();
            Array.Reverse(beforeChar);
            before = "";

            foreach (char item in beforeChar)
            {
                before += item;
            }

            if (before == "")
            {
                before = "-";
                before += behind;
            }
            try
            {
                retArray[0] = Convert.ToDouble(before);
                retArray[1] = Convert.ToDouble(behind);
            }
            catch
            {
            }

            return retArray;
        }
    }
}
