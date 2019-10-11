using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CulinaryRecipes
{

    public partial class RichTextBoxMy : RichTextBox
    {
        private int _capacity;
        private StringBuilder _lineBuilder;
        public List<string> bufor;
        private List<int> _selectionList;
        private int _lineSelection;
        private string _line;
        private string _liniaPodzielna;//nad tym trzeba sie zastanowic czy tego nie wywalic i zastapic getLine
        private static int _maxNumberOfLines;
        public static string[] ulamki = { "1/2","1/3","1/4","1/5","2/3","3/4" };

        public bool EnterOn { get; set; }
        public int NumberLine { get; set; }
        public int MaxLine { get; set; }
        public bool AddRecipeForm2 { get; set; }
        public string TextboxText { get; set; }
        public int StartSelection { get; set; }
        public bool Deleted { get; set; }
        public bool ChangeStartSelectionIfLineIsFull { get; set; }
        public bool howeverDelete { get; set; }

        public RichTextBoxMy() : this(capacity: 4)
        {
        }

        public RichTextBoxMy(int capacity)
        {

            _lineSelection = 0;
            _maxNumberOfLines = 37;
            _lineBuilder = new StringBuilder();

            bufor = new List<string>();
            bufor.Insert(0, string.Empty);

            _selectionList = new List<int>();
            _selectionList.Insert(0, 0);

            _line = string.Empty;
            this._capacity = capacity;
        }

        /// <summary>
        /// Jeżeli linia ma większą długość niż pojemność
        /// </summary>
        public bool IsFull
        {
            get
            {
                return _line.Length + 1 > _capacity;
            }
        }

        /// <summary>
        /// Podczas dodawania kolejnych elementów w tekscie - sprawdza czy lista jest pusta
        /// </summary>
        public bool ListIsEmpty
        {
            get
            {
                {
                    return NumberLine > bufor.Count - 1;
                }
            }
        }

        /// <summary>
        /// Zapis prawidłowych znaków do listy
        /// </summary>
        /// <param name="value"></param>
        public void CharacterInput(char value)
        {
            if ((value >= 46 && value <= 57) || value == 44)
            {
              //  _line =
                    GetLine(bufor);

                if (_line.Length == 0 && (value == 46 || value == 44 || value == 47))
                {
                    ChangeStartSelectionIfLineIsFull = true;
                }
                else
                {
                    if (value == 46)
                    {
                        value = ',';
                    }

                    if (value != 13 && value != 8)
                    {
                        _lineBuilder.Append(value);

                      //  _line = 
                            GetLine(bufor);

                        if (MaxLine != NumberLine)
                        {
                            if (IsFull)
                            {
                                _line = _line.Remove(_line.Length - 1);

                                _lineSelection = ZwrotSumyRoznicyWStartSelection(_selectionList);

                                if (_lineSelection == _capacity) _lineSelection--;

                                ChangeStartSelectionIfLineIsFull = true;
                            }
                            else
                            {
                                _lineSelection = ZwrotSumyRoznicyWStartSelection(_selectionList);
                            }

                            _line = _line.Insert(_lineSelection, _lineBuilder.ToString());
                        }
                        else
                        {
                            if (IsFull)
                            {
                                _line = _line.Remove(_line.Length - 1);

                                _lineSelection = ZwrotSumyRoznicyWStartSelection(_selectionList);

                                if (ZwrotSumyRoznicyWStartSelection(_selectionList) == _capacity)
                                {
                                    _lineSelection--;
                                }
                                else if (ZwrotSumyRoznicyWStartSelection(_selectionList) > _capacity)
                                {
                                    _lineSelection = _capacity - 1;
                                }
                            }
                            else
                            {
                                _lineSelection = ZwrotSumyRoznicyWStartSelection(_selectionList);
                            }

                            _line = _line.Insert(_lineSelection, _lineBuilder.ToString());
                        }

                        if (_line == null)
                        {
                            _liniaPodzielna = string.Empty;
                        }
                        else
                        {
                            _liniaPodzielna = _line;
                        }


                        bufor.RemoveAt(NumberLine);
                        bufor.Insert(NumberLine, _line);

                        _selectionList.RemoveAt(NumberLine);
                        _selectionList.Insert(NumberLine, _line.Length);

                        _lineBuilder.Clear();
                    }
                }
            }
            else if (value != 8 && value != 13)
            {
                ChangeStartSelectionIfLineIsFull = true;
            }

            if (value == 13 && EnterOn)
            {
                NumberLine = NumberLine - 1;
               // _line = 
                    GetLine(bufor);
                NumberLine++;
              
                if (ClearTypedIncorrectly(_line) == false)
                {
                    if (NumberOfLines() == false)
                    {
                        if (_liniaPodzielna == null)
                        {
                            _liniaPodzielna = string.Empty;
                        }

                        ConvertToDecimalFraction(_liniaPodzielna, NumberLine - 1);

                        if (ListIsEmpty)
                        {
                            bufor.Add(string.Empty);
                            _selectionList.Add(0);
                        }
                        else
                        {
                            if (!ConvertToDecimalFraction(_liniaPodzielna, NumberLine - 1))
                            {
                                CopyTextToList(TextboxText);
                                CompareBuforWithSelection();
                                NumberLine = NumberLine - 1;
                                //_line = 
                                    GetLine(bufor);
                                ClearTypedIncorrectly(_line);
                                NumberLine++;
                            }
                        }
                    }

                    _line = string.Empty;
                    _liniaPodzielna = string.Empty;
                }
                else
                {

                    Deleted = true;

                    NumberLine = NumberLine - 1;

                    int liniaLength = _line.Length;

                    ReplaceLine();

                    GetLengthIfRemoved(liniaLength);
                }
            }
            else if (value == 13 && EnterOn == false)
            {
                //   _line = GetLine(bufor);
                GetLine(bufor);

                if (ClearTypedIncorrectly(_line))
                {

                    Deleted = true;

                    int liniaLength = _line.Length;

                    ReplaceLine();

                    GetLengthIfRemoved(liniaLength);
                }
                else
                {
                    if (_liniaPodzielna == null)
                    {
                        _liniaPodzielna = string.Empty;
                    }

                    ConvertToDecimalFraction(_liniaPodzielna, NumberLine);

                    _line = string.Empty;

                    _liniaPodzielna = string.Empty;
                }
            }
        }

        /// <summary>
        /// Przelicza ulamek (np.1/3) na ulamek dziesietny
        /// </summary>
        /// <param name="toDownload"></param>
        /// <param name="numerLinii"></param>
        /// <returns></returns>
        public bool ConvertToDecimalFraction(string toDownload, int numerLinii)
        {
            if (toDownload == null) toDownload = string.Empty;
            _line = toDownload;
            string text1 = string.Empty;
            string text2 = string.Empty;
            string slash = "/";
            bool contain = false;
            bool skip = false;

            double txt1;
            double txt2;
            double result=1;
            string save = string.Empty;
            
            if (_line.Contains(slash))
            {
              
                StringBuilder nowa = new StringBuilder(_line);
                int i = 0;

                while (nowa[i] != '/')
                {
                    text1 += nowa[i];
                    i++;
                }

                while (i < nowa.Length)
                {
                    if (nowa[i] != '/')
                    {
                        if (nowa[i] != '\n')
                        {
                            text2 += nowa[i];
                        }
                    }

                    i++;
                }

                if (!string.IsNullOrEmpty(text1) && !string.IsNullOrEmpty(text2))
                {
                        txt1 = double.Parse(text1);
                        txt2 = double.Parse(text2);
                    if (!howeverDelete)
                    {
                        foreach (string item in ulamki)
                        {
                            if (toDownload.Contains(item))
                            {
                                save = item;
                                skip = true;
                            }
                            else
                            {
                                result = txt1 / txt2;

                                result = System.Math.Round(result, 1);

                                contain = true;
                            }

                        }
                    }
                    else
                    {
                        result = txt1 / txt2;

                        result = System.Math.Round(result, 1);

                        contain = true;
                    }
                }
                else
                {
                    Deleted = true;
                    result = 0;
                }


                if (result != 0 && skip==false)
                {
                    save = result.ToString();
                }
              

                bufor.RemoveAt(numerLinii);
                bufor.Insert(numerLinii, save);

                _selectionList.RemoveAt(numerLinii);
                _selectionList.Insert(numerLinii, save.Length);
            }

            text1 = string.Empty;
            text2 = string.Empty;
            _line = string.Empty;

            return contain;
        }

        /// <summary>
        /// Podmienia linie tekstu
        /// </summary>
        public void ReplaceLine()
        {
            _line = string.Empty;

            bufor.RemoveAt(NumberLine);
            bufor.Insert(NumberLine, _line);

            _selectionList.RemoveAt(NumberLine);
            _selectionList.Insert(NumberLine, _line.Length);
        }

        public int GetLengthIfRemoved(int lineLength)
        {
            if (Deleted)
            {
                if (NumberLine != 0)
                {
                    if (!EnterOn)
                    {
                        StartSelection = StartSelection - lineLength;
                    }
                    else
                    {
                        StartSelection = StartSelection - lineLength - 1;
                    }
                }
                else
                {
                    StartSelection = 0;
                }
            }
            return StartSelection;
        }

        /// <summary>
        /// czyści błędnie wpisane dane ( typu 0,  itp )
        /// </summary>
        public bool ClearTypedIncorrectly(string line)
        {
            bool zero = false;

            
                string mainFormula = @"[1-9](d?)";
                string faild = @"(d*)(/)";
                string correct = @"(d*)(/)(d*)";

                var myRegex = new Regex(mainFormula);
                var myRegex2 = new Regex(faild);
                var myRegex3 = new Regex(correct);

                if (!myRegex.IsMatch(line) && !string.IsNullOrEmpty(line) || myRegex2.IsMatch(line) && !myRegex3.IsMatch(line))
                {
                    zero = true;
                }
                else
                {
                    if (line != string.Empty)
                    {
                        if (line[line.Length - 1] == ',')
                        {
                            zero = true;
                        }
                    }
                }
            
            return zero;
        }
        //zmienić nazwe
        private int ZwrotSumyRoznicyWStartSelection(List<int> listName)
        {
            int result;
            int sum = 0;
            int variable = 0;

            foreach (var item in listName)
            {
                if (variable == NumberLine)
                {
                    break;
                }
                else
                {
                    sum += item;
                    variable++;
                }
            }
            if (NumberLine != 0)
            {
                result = StartSelection - (sum + NumberLine);
            }
            else
            {
                result = StartSelection;
            }

            return result;
        }

        /// <summary>
        /// Odczyt Listy
        /// </summary>
        /// <returns></returns>
        public string[] TextOutput()
        {
            return bufor.ToArray();
        }

        /// <summary>
        /// Odczyt pojedynczej określonej przez nas linii Listy
        /// </summary>
        /// <param name="numberLine"></param>
        /// <returns></returns>
        public string TextOutput(int numberLine)
        {
               return bufor[numberLine];
        }

        /// <summary>
        /// - podaje w którym miejscu powinien byc ustawiony focus
        /// </summary>
        /// <param name="textboxText"></param>
        /// <returns></returns>
        public int NumberOfCharactersToFocus(string textboxText)
        {
            int numberLineTemp = 0;
            int numberOfCharacters = 0;

            try
            {
                string text = string.Empty;

                foreach (char item in textboxText)
                {
                    if (item == '\n')
                    {
                        numberOfCharacters++;
                        text += item;
                        numberLineTemp++;
                    }
                    else
                    {
                        text += item;
                        numberOfCharacters++;
                    }

                    if (numberLineTemp > NumberLine)
                    {
                        if (MaxLine != NumberLine)
                        {
                            numberOfCharacters--;
                        }
                        break;
                    }
                }
            }
            catch(NullReferenceException ex)
            { }

            return numberOfCharacters;
        }

        /// <summary>
        /// - wyrównuje ilość linii w 3 textboxach
        /// </summary>
        /// <param name="Grams"></param>
        /// <param name="Ingridient"></param>
        public void AlignTheNumberOfLines(RichTextBox Grams, RichTextBox Ingridient)
        {
            if (Ingridient.Lines.Length > bufor.Count || Ingridient.Lines.Length > Grams.Lines.Length)
            {
                AlignTheNumberOfLinesCenter(Ingridient, bufor);
                AlignTheNumberOfLinesCenter(Ingridient, Grams);
            }
            if (bufor.Count > Ingridient.Lines.Length || bufor.Count > Grams.Lines.Length)
            {
                AlignTheNumberOfLinesCenter(bufor, Ingridient);
                AlignTheNumberOfLinesCenter(bufor, Grams);
            }
            if (Grams.Lines.Length > Ingridient.Lines.Length || Grams.Lines.Length > bufor.Count)
            {
                AlignTheNumberOfLinesCenter(Grams, Ingridient);
                AlignTheNumberOfLinesCenter(Grams, bufor);
            }
        }

        /// <summary>
        /// - dodawanie linii do textboxa ktory ma mniej linii- funkcja uzupelniająca poprzednia
        /// </summary>
        /// <param name="longName"></param>
        /// <param name="shortName"></param>
        public void AlignTheNumberOfLinesCenter(RichTextBox longName, RichTextBox shortName)
        {
            int tempNumberLine = NumberLine;
            while (longName.Lines.Length > shortName.Lines.Length)
            {
                int i = shortName.Text.Length;

                shortName.Text = shortName.Text.Insert(i, "\n");
                shortName.SelectionStart = i;
            }
            NumberLine = tempNumberLine;
        }

        public void AlignTheNumberOfLinesCenter(List<string> longName, RichTextBox shortName)
        {
            int tempNumberLine = NumberLine;

            while (longName.Count > shortName.Lines.Length)
            {
                int i = shortName.Text.Length;

                shortName.Text = shortName.Text.Insert(i, "\n");

                shortName.SelectionStart = i;
            }
            NumberLine = tempNumberLine;
        }

        public void AlignTheNumberOfLinesCenter(RichTextBox longName, List<string> shortName)
        {
            while (longName.Lines.Length > shortName.Count)
            {
                shortName.Add(string.Empty);
                _selectionList.Add(0);
            }
        }

        /// <summary>
        /// Ogranicza ilość wpisywanych linii
        /// </summary>
        /// <returns></returns>
        public bool NumberOfLines()
        {
            bool largerThanNumberOfLines = false;

            if (bufor.Count >= _maxNumberOfLines)
            {
                MessageBox.Show("Program nie może mieć więcej już linii");
                TextOutput();
                largerThanNumberOfLines = true;
            }

            return largerThanNumberOfLines;
        }

        /// <summary>
        /// Nowa linia
        /// </summary>
        /// <param name="name"></param>
        public static void NewLine(RichTextBox name)
        {
            int i = name.Text.Length;

            name.Focus();
            name.Text = name.Text.Insert(i, "\n");
            name.SelectionStart = i;
        }

        /// <summary>
        /// - Nowa Linia dla trzech TextBoxów i ustawienie focusa w pierwszym od lewej
        /// </summary>
        /// <param name="name"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="numberLine"></param>
        public void ClassicEnterPlusNewLine(RichTextBox name, RichTextBox second, RichTextBox third, int numberLine)
        {
            NewLine(second);
            NewLine(third);

            name.Focus();
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

        /// <summary>
        /// - ustaw focus w w najdalszym miejscu (np. gdy wlacza sie modyfikacje)
        /// </summary>
        /// <param name="nameAmount"></param>
        /// <param name="nameGram"></param>
        /// <param name="nameIngredient"></param>
        public static void SetFocus(RichTextBox nameAmount, RichTextBox nameGram, RichTextBox nameIngredient)
        {
            int i = nameAmount.TextLength;
            Form2 dis = new Form2();
            int quantityChar = nameAmount.Lines[nameAmount.Lines.Length - 1].Length;

            if (nameAmount.TextLength > nameGram.TextLength && nameAmount.TextLength > nameIngredient.TextLength)
            {
                
                if (nameAmount.Text.Last() == ' ') i = i - 2;

                nameAmount.Focus();

                nameAmount.SelectionStart = i;
            }
        }

        /// <summary>
        /// Metoda pobiera pojedynczą linie tekstu (numberLine jest ustalony przez Focus) z Listy
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public string GetLine(List<string> listName)
        {
            try
            {
                if (!ListIsEmpty)
                {
                    _line= listName[NumberLine];
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

            return _line;
        }

        /// <summary>
        /// Metoda kopiuje tekst z Textbox(na którym jest zastosowana klasa DifferentEnter) do List
        /// </summary>
        /// <param name="text"></param>
        public void CopyTextToList(string text)
        {
            try
            {
                bufor.Clear();

                string copyText = string.Empty;
                int licznik = 0;

                foreach (var item in text)
                {
                    licznik++;

                    if (item != '\n')
                    {
                        _line = copyText;

                        if (!IsFull)
                        {
                            copyText += item;
                        }

                        _line = string.Empty;
                    }
                    else
                    {
                        bufor.Add(copyText);
                        copyText = string.Empty;
                    }
                }

                if (licznik == text.Length)
                {
                    bufor.Add(copyText);
                    _line = copyText;
                }
            }
            catch(NullReferenceException ex)
            {

            }
        }

        /// <summary>
        /// Porównuje Listy tekstową i selekcyjną 
        /// </summary>
        /// <returns></returns>
        public List<int> CompareBuforWithSelection()
        {
            _selectionList.Clear();

            for (int i = 0; i < bufor.Count; i++)
            {
                _selectionList.Add(0);
                _selectionList[i] = bufor[i].Length;
            }

            return _selectionList;
        }
    }
}
