using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    class AutoCompleter : RichTextBoxMy
    {
        public List<string> buforGram;
        private StringBuilder _lineBuilder;
        private string _line;
        private int _lineSelection;
        private List<int> _selectionList;
        private string _liniaPodzielna;
        public int NumberLineGram { get; set; }
        public string TextboxTextGram { get; set; }
        public int StartSelectionGram { get; set; }
        public bool EnterOnGram { get; set; }

        public AutoCompleter()
        {

            _lineSelection = 0;
            //_maxNumberOfLines = 37;
            _lineBuilder = new StringBuilder();

            buforGram = new List<string>();
            buforGram.Insert(0, string.Empty);

            _selectionList = new List<int>();
            _selectionList.Insert(0, 0);

            _line = string.Empty;
         
        }
        private bool ListIsEmpty
        {
            get
            {
                {
                    return NumberLineGram > buforGram.Count - 1;
                }
            }
        }
        public void ReplaceLineGram()
        {
            _line = string.Empty;

            buforGram.RemoveAt(NumberLineGram);
            buforGram.Insert(NumberLineGram, _line);

            _selectionList.RemoveAt(NumberLineGram);
            _selectionList.Insert(NumberLineGram, _line.Length);
        }

        public int GetLengthIfRemovedGram(int lineLength)
        {
            if (Deleted)
            {
                if (NumberLineGram != 0)
                {
                    if (!EnterOnGram)
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
        /// Metoda kopiuje tekst z Textbox(na którym jest zastosowana klasa DifferentEnter) do List
        /// </summary>
        /// <param name="text"></param>
        public void CopyTextToListGram(string text)
        {
            try
            {
                buforGram.Clear();

                string copyText = string.Empty;
                int licznik = 0;

                foreach (var item in text)
                {
                    licznik++;

                    if (item != '\n')
                    {
                        _line = copyText;

                        //if (!IsFull)
                        //{
                            copyText += item;
                       // }

                        _line = string.Empty;
                    }
                    else
                    {
                        buforGram.Add(copyText);
                        copyText = string.Empty;
                    }
                }

                if (licznik == text.Length)
                {
                    buforGram.Add(copyText);
                    _line = copyText;
                }
            }
            catch (NullReferenceException ex)
            {

            }
        }

        /// <summary>
        /// Metoda pobiera pojedynczą linie tekstu (numberLine jest ustalony przez Focus) z Listy
        /// </summary>
        /// <param name="listName"></param>
        /// <returns></returns>
        public string GetLineGram(List<string> listName, int nrLinii)
        {
            try
            {
                if (!ListIsEmpty)
                {
                    _line = listName[nrLinii];
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
        /// Porównuje Listy tekstową i selekcyjną 
        /// </summary>
        /// <returns></returns>
        public List<int> CompareBuforWithSelectionGram()
        {
            _selectionList.Clear();

            for (int i = 0; i < buforGram.Count; i++)
            {
                _selectionList.Add(0);
                _selectionList[i] = buforGram[i].Length;
            }

            return _selectionList;
        }

        /// <summary>
        /// Zapis prawidłowych znaków do listy
        /// </summary>
        /// <param name="value"></param>
        public void Input(char value)
        {
            GetLineGram(buforGram, NumberLineGram);

            if (value != 13 && value != 8)
            {
                _lineBuilder.Append(value);

                GetLineGram(buforGram, NumberLineGram);

                if (MaxLine != NumberLineGram)
                {

                    _line = _line.Remove(_line.Length - 1);

                    _lineSelection = ZwrotSumyRoznicyWStartSelectionGram(_selectionList, NumberLineGram);

                    ChangeStartSelectionIfLineIsFull = true;

                    _line = _line.Insert(_lineSelection, _lineBuilder.ToString());
                }
                else
                {
                    _lineSelection = ZwrotSumyRoznicyWStartSelectionGram(_selectionList, NumberLineGram);

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


                buforGram.RemoveAt(NumberLineGram);
                buforGram.Insert(NumberLineGram, _line);

                _selectionList.RemoveAt(NumberLineGram);
                _selectionList.Insert(NumberLineGram, _line.Length);

                _lineBuilder.Clear();
            }
            // if (value != 8 && value != 13)
            //{
            //    ChangeStartSelectionIfLineIsFull = true;
            //}

            if (value == 13 && EnterOnGram)
            {
                NumberLineGram = NumberLineGram - 1;
                GetLineGram(buforGram, NumberLineGram);
                NumberLineGram++;

                if (ClearTypedIncorrectly(_line) == false)
                {
                    if (NumberOfLines() == false)
                    {
                        if (_liniaPodzielna == null)
                        {
                            _liniaPodzielna = string.Empty;
                        }

                        // ConvertToDecimalFraction(_liniaPodzielna, NumberLine - 1);

                        if (ListIsEmpty)
                        {
                            buforGram.Add(string.Empty);
                            _selectionList.Add(0);
                        }
                        else
                        {
                            //if (!ConvertToDecimalFraction(_liniaPodzielna, NumberLine - 1))
                            //{
                            CopyTextToListGram(TextboxText);
                            CompareBuforWithSelectionGram();
                            NumberLineGram = NumberLineGram - 1;
                            GetLineGram(buforGram, NumberLineGram);
                            NumberLineGram++;
                            // }
                        }
                    }

                    _line = string.Empty;
                    _liniaPodzielna = string.Empty;
                }
                else
                {

                    Deleted = true;

                    NumberLineGram = NumberLineGram - 1;

                    int liniaLength = _line.Length;

                    ReplaceLineGram();

                    GetLengthIfRemovedGram(liniaLength);
                }
            }
            else if (value == 13 && EnterOnGram == false)
            {
                //   _line = GetLine(bufor);
                GetLineGram(buforGram, NumberLineGram);

                //if (ClearTypedIncorrectly(_line))
                //{

                //    Deleted = true;

                //    int liniaLength = _line.Length;

                //    ReplaceLineGram();

                //    GetLengthIfRemovedGram(liniaLength);
                //}
                //else
                //{
                    if (_liniaPodzielna == null)
                    {
                        _liniaPodzielna = string.Empty;
                    }

                    // ConvertToDecimalFraction(_liniaPodzielna, NumberLine);

                    _line = string.Empty;

                    _liniaPodzielna = string.Empty;
              //  }
            }
        }
        /// <summary>
        /// Odczyt Listy
        /// </summary>
        /// <returns></returns>
        public string[] TextOutputGram()
        {
            return buforGram.ToArray();
        }

        /// <summary>
        /// - wyrównuje ilość linii w 3 textboxach
        /// </summary>
        /// <param name="Grams"></param>
        /// <param name="Ingridient"></param>
        public void AlignTheNumberOfLinesGram(RichTextBox Amounts, RichTextBox Ingridient)
        {
            int tempNumberLine = NumberLineGram;
            int tempMaxLine = MaxLine;


            if (Ingridient.Lines.Length > buforGram.Count || Ingridient.Lines.Length > Amounts.Lines.Length)
            {
                AlignTheNumberOfLinesCenterGram(Ingridient, buforGram);
                AlignTheNumberOfLinesCenterGram(Ingridient, Amounts);
            }
            if (buforGram.Count > Ingridient.Lines.Length || buforGram.Count > Amounts.Lines.Length)
            {
                AlignTheNumberOfLinesCenterGram(buforGram, Ingridient);
                AlignTheNumberOfLinesCenterGram(buforGram, Amounts);
            }
            if (Amounts.Lines.Length > Ingridient.Lines.Length || Amounts.Lines.Length > buforGram.Count)
            {
                AlignTheNumberOfLinesCenterGram(Amounts, Ingridient);
                AlignTheNumberOfLinesCenterGram(Amounts, buforGram);
            }

            NumberLineGram = tempNumberLine;
            MaxLine = tempMaxLine;

        }

        /// <summary>
        /// - dodawanie linii do textboxa ktory ma mniej linii- funkcja uzupelniająca poprzednia
        /// </summary>
        /// <param name="longName"></param>
        /// <param name="shortName"></param>
        public void AlignTheNumberOfLinesCenterGram(RichTextBox longName, RichTextBox shortName)
        {
            // int tempNumberLine = NumberLine;
            while (longName.Lines.Length > shortName.Lines.Length)
            {
                int i = shortName.Text.Length;

                shortName.Text = shortName.Text.Insert(i, "\n");
                shortName.SelectionStart = i;
            }
            //   NumberLine = tempNumberLine;
        }

        public void AlignTheNumberOfLinesCenterGram(List<string> longName, RichTextBox shortName)
        {
            int tempNumberLine = NumberLineGram;

            while (longName.Count > shortName.Lines.Length)
            {
                int i = shortName.Text.Length;

                shortName.Text = shortName.Text.Insert(i, "\n");

                shortName.SelectionStart = i;
            }
            NumberLine = tempNumberLine;
        }

        public void AlignTheNumberOfLinesCenterGram(RichTextBox longName, List<string> shortName)
        {
            while (longName.Lines.Length > shortName.Count)
            {
                shortName.Add(string.Empty);
                _selectionList.Add(0);
            }
        }

        /// <summary>
        /// Odczyt pojedynczej określonej przez nas linii Listy
        /// </summary>
        /// <param name="numberLine"></param>
        /// <returns></returns>
        public string TextOutputGram(int numberLine)
        {
            return buforGram[numberLine];
        }

        //zmienić nazwe
        private int ZwrotSumyRoznicyWStartSelectionGram(List<int> listName, int nrLinii)
        {
            int result;
            int sum = 0;
            int variable = 0;

            foreach (var item in listName)
            {
                if (variable == nrLinii)
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
                result = StartSelectionGram - (sum + NumberLineGram);
            }
            else
            {
                result = StartSelectionGram;
            }

            return result;
        }
    }
}

