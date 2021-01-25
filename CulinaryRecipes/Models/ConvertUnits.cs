using System;
using System.Text;

namespace CulinaryRecipes.Models
{
    static class ConvertUnits
    {
        // zamienia tekst na tablice
        private static string[] NumberLine(string textAmounts)
        {
            return textAmounts.Split('\n');
        }

        /// <summary>
        /// Converts a fraction to a decimal
        /// </summary>
        /// <returns></returns>
        public static double ConverterToDecimal(string text)
        {
            double result = 0;

            if (IsFraction(text))
            {
                string leftSide = string.Empty;
                string rightSide = string.Empty;

                DetermineLeftAndRightSides(ref leftSide, ref rightSide, text);

                if (!string.IsNullOrEmpty(leftSide) && !string.IsNullOrEmpty(rightSide))
                {
                    result = Division(leftSide, rightSide);
                    result = Math.Round(result, 2);
                }
            }
            else
            {
                double.TryParse(text, out result);
            }
            return result;
        }

        //wew dzielenie wew.
        private static double Division(string leftSide, string rightSide)
        {
            double result;
            double leftResult;
            double rightResult;

            double.TryParse(leftSide, out leftResult);
            double.TryParse(rightSide, out rightResult);

            if (rightResult != 0)
                result = leftResult / rightResult;
            else
                result = 0;
            return result;
        }

        //wew   ustala double dla licznika i mianownika wew
        private static void DetermineLeftAndRightSides(ref string leftSide, ref string rightSide, string text)
        {
            StringBuilder textBuilder = new StringBuilder(text);
            int i = 0;

            while (textBuilder[i] != '/')
            {
                if (textBuilder[i] == '\n') leftSide = "";
                else leftSide += textBuilder[i];
                i++;
            }

            while (i < textBuilder.Length)
            {
                if (textBuilder[i] != '/' && textBuilder[i] != '\n')
                {
                    rightSide += textBuilder[i];
                }
                i++;
            }
        }

        /// <summary>
        /// // funkcja przeliczająca ilość w stosunku do ilosci osob
        /// </summary>
        /// <param name="textAmounts"></param>
        /// <param name="numberOfPeople"></param>
        /// <param name="numberOfPeopleCurrently"></param>
        /// <returns></returns>
        public static string[] ConvertFunction(string textAmounts, int numberOfPeopleCurrently, int numberOfPeople)
        {
            string[] textLines = NumberLine(textAmounts);
            double[] convertedTextLine = new double[textLines.Length];

            //zamienia z tekstu na double i jezeli jest ulamek zwykly to zmienia na 10
            ConvertToDecimalFractionalAndDouble(textLines, convertedTextLine);

            // dzielenie przez obecna ilość osób w celu uzyskania ilosci dla pojedynczej osoby
            ResultForOnePerson(numberOfPeopleCurrently, convertedTextLine);

            //mnozenie przez podaną ilość osób i zaokrąglenie
            IncreaseToCertainNumber(numberOfPeople, convertedTextLine);

            string text = ConvertDoubleArrayToString(convertedTextLine);

            return ReplaceZeroWithAnEmptyString(text);
        }

        private static string ConvertDoubleArrayToString(double[] convertedTextLine)
        {
            string text = string.Empty;
            for (int i = 0; i < convertedTextLine.Length; i++)
            {
                text += convertedTextLine[i].ToString() + '\n';
            }

            return text;
        }

        private static void ConvertToDecimalFractionalAndDouble(string[] textLines, double[] convertedTextLine)
        {
            if (textLines != null)
            {
                for (int i = 0; i < textLines.Length; i++)
                {
                    convertedTextLine[i] = ConverterToDecimal(textLines[i]);
                }
            }
        }

        private static void ResultForOnePerson(int numberOfPeopleCurrently, double[] convertedTextLine)
        {
            for (int i = 0; i < convertedTextLine.Length; i++)
            {
                convertedTextLine[i] = convertedTextLine[i] / numberOfPeopleCurrently;
            }
        }

        private static void IncreaseToCertainNumber(int numberOfPeople, double[] convertedTextLine)
        {
            for (int i = 0; i < convertedTextLine.Length; i++)
            {
                convertedTextLine[i] = convertedTextLine[i] * numberOfPeople;
                convertedTextLine[i] = Math.Round(convertedTextLine[i], 2);
            }
        }

        private static bool IsFraction(string text)
        {
            if (text.Contains("/")) return true;
            else return false;
        }

        /// <summary>
        /// zamien zero na pusty string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string[] ReplaceZeroWithAnEmptyString(string text)
        {
            string[] textLines = NumberLine(text);

            for (int i = 0; i < textLines.Length; i++)
            {
                if (IsZero(text, i)) textLines[i] = textLines[i].Replace("0", "");
            }
            return textLines;
        }

        // sprawdza czy jest tylko zero wpisane w linii
        public static bool IsZero(string text, int i = 0)
        {
            string[] textLines = NumberLine(text);

            if (textLines[i].Equals("0")) return true;
            else return false;
        }
    }
}
