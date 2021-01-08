using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    public partial class RichTextBoxMy
    {
        private int _capacity;
        private List<string> _bufor;
        private const int _maxNumberOfLines = 33;
        private RichTextBox rtxtAmount;
        private int _maxLine = 0;

        public RichTextBoxMy(RichTextBox amount) : this(capacity: 4)
        {
            rtxtAmount = amount;
            _bufor = new List<string>();
        }

        public RichTextBoxMy(int capacity)
        {
            _bufor = new List<string>();
            this._capacity = capacity;
        }

        public bool EnterOn { get; set; }
        public int NumberLine { get; set; }
        public int MaxLine { get; set; } //poprawic MaxLine
        public bool AddRecipeForm2 { get; set; }
        public int MaxLines
        {
            get
            {
                return _maxLine;
            }
            private set
            {
                _maxLine = value;
            }
        }

        /// <summary>
        /// Gets total number of elements in the List
        /// </summary>
        public int Count
        {
            get
            {
                return _bufor.Count;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public static int MaxNumberOfLines
        {
            get
            {
                return _maxNumberOfLines;
            }
        }

        /// <summary>
        /// Checks the correctness of entered data
        /// </summary>
        public bool CorrectDate
        {
            get
            {
                return RemoveTypedIncorrectlyData(_bufor[NumberLine]);
            }
        }

        /// <summary>
        /// The line is longer than the capacity.
        /// </summary>
        public bool IsFull
        {
            get
            {
                return _bufor[NumberLine].Length > _capacity;
            }
        }

        /// <summary>
        /// Checks if the given line number is not greater than the length of the list
        /// </summary>
        private bool ListIsEmpty
        {
            get
            {
                {
                    return NumberLine > _bufor.Count - 1;
                }
            }
        }

        public bool ValidateTheLastCharacter { get; set; }

        /// <summary>
        /// The method corrects and introduces only numbers
        /// </summary>
        /// <param name="value"></param>
        public void CharacterInput(char value, int select)
        {
            if (value == 13)
            {
                if (EnterOn)
                {
                    if (NumberLine < MaxLine)
                    {
                        _bufor.Insert(NumberLine, "");
                    }
                    else
                    {
                        if (ListIsEmpty)
                        {
                            NewEmptyLine();
                            NewEmptyLine();
                        }
                        else
                        {
                            NewEmptyLine();
                        }
                    }

                    MaxLines++;

                    NumberLine--;

                    if (!string.IsNullOrEmpty(rtxtAmount.Text))
                    {
                        if (!CorrectDate)
                        {
                            RemoveIncorrectTextLine(NumberLine);
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(rtxtAmount.Text))
                    {
                        if (!CorrectDate)
                        {
                            RemoveIncorrectTextLine(NumberLine);
                        }
                    }

                }
                if (!string.IsNullOrEmpty(rtxtAmount.Text))
                {
                    if (_bufor[NumberLine] == "0") _bufor[NumberLine] = "";
                }
                NumberLine++;
                return;
            }
            if (value >= 48 && value <= 57)
            {
                if (!string.IsNullOrEmpty(rtxtAmount.Text))
                {
                    if (DoubleZeroAtTheStart(value))
                    {
                        RemoveIncorrectTextLine(NumberLine);
                        return;
                    }
                }

                if (NumberLine < MaxLine)
                {

                    int dlugoscJakas = GetTextToNumberLine().Length + NumberLine;
                    int dlugoscKoncowa = select - dlugoscJakas;

                    string textLine = GetLine(_bufor, NumberLine);

                    if (dlugoscKoncowa < _bufor[NumberLine].Length)
                    {
                        textLine = textLine.Insert(dlugoscKoncowa, value.ToString());
                    }
                    else
                    {
                        textLine = textLine + value.ToString();
                    }

                    _bufor.RemoveAt(NumberLine);
                    _bufor.Insert(NumberLine, textLine);
                }
                else
                {
                    string textLine = GetLine(_bufor, NumberLine);

                    if (!string.IsNullOrEmpty(rtxtAmount.Text))
                    {
                        _bufor.RemoveAt(NumberLine);
                    }

                    _bufor.Insert(NumberLine, textLine + value.ToString());
                }
            }
            else
            {
                ValidateTheLastCharacter = true;
            }
            if (!string.IsNullOrEmpty(rtxtAmount.Text))
            {
                if (IsFull)
                {
                    string textLine = GetLine(_bufor, NumberLine);
                    _bufor.RemoveAt(NumberLine);
                    textLine = textLine.Remove(textLine.Length - 2);
                    _bufor.Insert(NumberLine, textLine + value);
                    ValidateTheLastCharacter = true;
                }
                else
                {
                    if ((_bufor[NumberLine].Length > 0 && _bufor[NumberLine].Length < 4) && (value == 46 || value == 44 || value == 47))
                    {
                        if (value == 46)
                        {
                            value = ConvertPointToComma();
                        }

                        int selection = GetLine(_bufor, NumberLine).Length;

                        if (1 <= selection && selection < 3)
                        {
                            if (!ProtectionAgainstTooManyRepetitions())
                            {
                                string line = GetLine(_bufor, NumberLine);
                                _bufor.RemoveAt(NumberLine);
                                _bufor.Insert(NumberLine, line + value.ToString());
                            }
                        }
                        else
                        {
                            ValidateTheLastCharacter = true;
                        }
                    }
                }
            }
        }

        public void NewEmptyLine()
        {
            _bufor.Add(string.Empty);
        }

        /// <summary>
        /// Reads a single element from an list
        /// </summary>
        /// <param name="numberLine"></param>
        /// <returns></returns>
        public string TextLine(int numberLine)
        {
            return _bufor[numberLine];
        }

        /// <summary>
        /// Iterates over the list elements adding their lengths together
        /// </summary>
        /// <param name="numberOfCharacters"></param>
        /// <returns></returns>
        private int TextLinesLengthAdding(int numberOfCharacters, int numberLine)
        {

            for (int i = 0; i < numberLine; i++)
            {
                numberOfCharacters += TextLine(i).Length;
                numberOfCharacters++;
            }

            return numberOfCharacters;
        }

        /// <summary>
        /// Gets the text length of the given number of lines
        /// </summary>
        /// <returns></returns>
        public int GetTextToNumberLineLength(int numberLine)
        {
            int numberOfCharacters = 0;

            numberOfCharacters = TextLinesLengthAdding(numberOfCharacters, numberLine);

            return numberOfCharacters;
        }

        /// <summary>
        /// Gets the text length of the given number of lines
        /// </summary>
        /// <returns></returns>
        public int GetTextToNumberLineLength(int numberLine, RichTextBox name)
        {
            int numberOfCharacters = 0;

            for (int i = 0; i < numberLine; i++)
            {
                numberOfCharacters += name.Lines[i].Length;
                numberOfCharacters++;
            }

            return numberOfCharacters;
        }

        /// <summary>
        /// Gets the text length of the given number of lines
        /// </summary>
        /// <returns></returns>
        public string GetTextToNumberLine()
        {
            string numberOfCharacters = string.Empty;

            for (int i = 0; i < NumberLine; i++)
            {
                numberOfCharacters += TextLine(i);
            }

            return numberOfCharacters;
        }

        /// <summary>
        /// Protection against too many repetitions of the same characters - ('/ , .')
        /// </summary>
        /// <returns></returns>
        private bool ProtectionAgainstTooManyRepetitions()
        {
            return _bufor[NumberLine].Contains("/") || _bufor[NumberLine].Contains(",") || _bufor[NumberLine].Contains(".") ? true : false;
        }

        /// <summary>
        /// Converts a point to a comma
        /// </summary>
        /// <returns></returns>
        private static char ConvertPointToComma()
        {
            return ',';
        }

        /// <summary>
        /// Checks that the user has not specified two zeros at the beginning of the line
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool DoubleZeroAtTheStart(char value)
        {
            if ((_bufor[NumberLine] == "0") && (value != 44 || value != 46) && _bufor[NumberLine].Length == 1) return true;
            else return false;
        }

        /// <summary>
        /// Removes incorrectly entered data
        /// </summary>
        private bool RemoveTypedIncorrectlyData(string line)
        {
            string mainFormula = @"^[0-9]{1,4}$";
            string fractionCorrect = @"[1-9]/[2-9]";
            string fractionCorrectComma = @"[0-9],[1-9]";

            if (!string.IsNullOrEmpty(line))
            {
                if (MyCorrectRegex(mainFormula).IsMatch(line) ||
                    MyCorrectRegex(fractionCorrect).IsMatch(line) ||
                    MyCorrectRegex(fractionCorrectComma).IsMatch(line)
                   )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Returns the regex variable
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        private static Regex MyCorrectRegex(string pattern)
        {
            return new Regex(pattern);
        }

        /// <summary>
        /// The method takes a single line of text (numberLine is fixed by Focus) from the List
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public string GetLine(List<string> listName, int nrLine)
        {
            if (!ListIsEmpty)
            {
                return listName[nrLine];
            }
            return string.Empty;
        }

        /// <summary>
        /// Removes incorrect text line
        /// </summary>
        public void RemoveIncorrectTextLine(int numberLine)
        {
            string line = string.Empty;

            _bufor.RemoveAt(numberLine);
            _bufor.Insert(numberLine, line);
        }

        /// <summary>
        /// Reading the list
        /// </summary>
        /// <returns></returns>
        public string[] TextOutput()
        {
            return _bufor.ToArray();
        }

        /// <summary>
        /// Reading a single line of text
        /// </summary>
        /// <param name="numberLine"></param>
        /// <returns></returns>
        public string TextOutput(int numberLine)
        {
            return _bufor[numberLine];
        }

        /// <summary>
        /// The method copies the text into the list broken down into lines
        /// </summary>
        /// <param name="text"></param>
        public void CopyTextToList(string text)
        {
            _bufor.Clear();
            string[] pod = text.Split('\n');
            _bufor = pod.ToList();
        }


        /// <summary>
        /// Alignment the number of lines in three text boxes
        /// </summary>
        /// <param name="Grams"></param>
        /// <param name="Ingridient"></param>
        public int AlignTheNumberOfLines(RichTextBox Grams, RichTextBox Ingridient, int longest)
        {
            int tempNumberLine = NumberLine;
            int tempMaxLine = MaxLine;

            if (Grams.Lines.Length < longest) AddNewLines(Grams, longest);
            else NewMethod(Grams, longest);

            if (Ingridient.Lines.Length < longest) AddNewLines(Ingridient, longest);
            else NewMethod(Ingridient, longest);

            if (Count < longest) AddNewLines(longest);
            else
            {
                int i = Count - 1;
                while (Count > longest)
                {
                    _bufor.RemoveAt(i);
                    i--;
                }
            }
            rtxtAmount.Lines = TextOutput();

            MaxLines = Grams.Lines.Length - 1;
            NumberLine = tempNumberLine;
            MaxLine = tempMaxLine;
            return longest;
        }

        private static void NewMethod(RichTextBox nameRich, int longest)
        {
            int select = nameRich.Lines.Length - 1;
            string[] tempArray = nameRich.Lines;

            Array.Resize(ref tempArray, longest);
            nameRich.Lines = tempArray;
        }

        /// <summary>
        /// Alignment the number of lines in three text boxes
        /// </summary>
        /// <param name="Grams"></param>
        /// <param name="Ingridient"></param>
        public int AlignTheNumberOfLines(RichTextBox Grams, RichTextBox Ingridient)
        {
            int tempNumberLine = NumberLine;
            int tempMaxLine = MaxLine;
            int longest = Compare(Grams, Ingridient);

            AddNewLines(Grams, longest);
            AddNewLines(Ingridient, longest);
            AddNewLines(longest);

            MaxLines = Grams.Lines.Length - 1;
            NumberLine = tempNumberLine;
            MaxLine = tempMaxLine;
            return longest;
        }

        public int Compare(RichTextBox Grams, RichTextBox Ingridient)
        {
            int longest;
            int comp = Grams.Lines.Length.CompareTo(Ingridient.Lines.Length);

            if (comp == 1) longest = Grams.Lines.Length;
            else longest = Ingridient.Lines.Length;

            comp = longest.CompareTo(Count);
            if (comp < 1) longest = Count;
            return longest;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longest"></param>
        private void AddNewLines(int longest)
        {
            while (longest > Count)
            {
                NewEmptyLine();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="longest"></param>
        private static void AddNewLines(RichTextBox name, int longest)
        {
            while (longest > name.Lines.Length)
            {
                NewLine(name);
            }
        }

        /// <summary>
        /// New Line
        /// </summary>
        /// <param name="name"></param>
        public static void NewLine(RichTextBox name)
        {
            int selectionStart = name.Text.Length;
            name.Text = name.Text.Insert(selectionStart, "\n");
            name.SelectionStart = selectionStart;
        }

        /// <summary>
        /// New line for three text boxes and cursor shift
        /// </summary>
        /// <param name="name"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="numberLine"></param>
        public static void ClassicEnterPlusNewLine(RichTextBox name, RichTextBox second, RichTextBox third)
        {
            NewLine(second);
            NewLine(third);
        }

        /// <summary>
        /// kod dla pierwszej linii 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="e"></param>
        public static void FirstLine(RichTextBox first, RichTextBox second, KeyEventArgs e)
        {
            int i = 0;

            if (first.Lines.Length <= 1 && e.KeyCode == Keys.Enter)
            {
                second.Focus();
                e.Handled = true;
                second.SelectionStart = i;
            }
        }

        // do poprawy - trezeba sie zasastanowic jak to ma dzialac
        /// <summary>
        /// Set the focus to the farthest point 
        /// </summary>
        /// <param name="nameAmount"></param>
        /// <param name="nameGram"></param>
        /// <param name="nameIngredient"></param> 
        public static void SetFocus(RichTextBox nameIngredient, RichTextBox nameGram, RichTextBox nameAmount)
        {
            int i = nameIngredient.TextLength;
            int quantityChar = nameIngredient.Lines[nameIngredient.Lines.Length - 1].Length;

            if (nameIngredient.TextLength > nameGram.TextLength && nameIngredient.TextLength > nameAmount.TextLength)
            {
                if (nameIngredient.Text.Last() == ' ')
                {
                    i = i - 2;
                }

                nameIngredient.Focus();
                nameIngredient.SelectionStart = i;
            }
        }

        /// <summary>
        /// Set focus to the correct location
        /// </summary>
        /// <param name="textboxText"></param>
        /// <returns></returns>
        public int NumberOfCharactersToFocus(string textboxText)
        {
            int numberOfCharacters = 0;
            string[] toFocus = textboxText.Split('\n');

            for (int i = 0; i <= NumberLine; i++)
            {
                numberOfCharacters += toFocus[i].Length;
            }

            return numberOfCharacters + NumberLine;
        }

    }
}