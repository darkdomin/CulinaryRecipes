using CulinaryRecipes.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace CulinaryRecipes
{
    public partial class Form2 : Form
    {
        public int idDgGridForm2 { get; set; }
        public int numberOfPortionsForm2 { get; set; }
        public int counterForm2 { get; set; }
        private int maxLine = 0;
        public int[] IdMealForm2 = new int[7];
        public int[] ingridientsForm2 = new int[8];
        public string titleForm2 { get; set; }
        public string amountsOfIngredientsForm2 { get; set; }
        public string gramsForm2 { get; set; }
        public string ingredientForm2 { get; set; }
        public string ShortDescriptionForm2 { get; set; }
        public string instructionForm2 { get; set; }
        public string listOfCuisinesForm2 { get; set; }
        public string idRatingForm2 = "-";
        public string difficultLevelForm2 { get; set; }
        public string executionTimeForm2 { get; set; }
        public string linkForm2 = "-";
        public string unlockFieldsForm2 { get; set; }
        public string clear = "0";
        public string dash = "-";
        public string interval = "  ";
        string addRest = "Zmień", add = "add";
        public char stringOfCharacters = ']';
        public char stringOfCharacters1 = '[';
        double converted;
        public bool cancel = false;
        public bool[] checkBoxesCancelForm2Ing = new bool[8];
        public bool[] checkBoxesCancelForm2Meal = new bool[7];
        public bool seekUnsubscribeForm2;
        public bool addRecipe;
        public bool newForm = false;
        private bool EnterOffBoolDescription = false;
        string PunktorOn = "Punktor Wł";
        string PunktorOff = "Punktor Wył";
        bool rtxDescriptionBool = false;
        int numberLine = 0;

        Font letter;
        Graphics graph;

        //sprawdza czy wpisany znak jest cyfrą, pomijając puste pola. Jezeli jest błąd zmienna check przyjmuje wartość true;
        bool check = false;
        bool blockFunc = false;

        string[] sortedUnits = { "dag", "gałązki", "gałązka", "garść", "gram", "kg", "kostka", "kostki", "listki", "listek", "puszka", "łyżka", "łyżeczka", "szt", "szczypty", "szklanka", "szklanki", "ząbki", "ml", "ziarna","płaty" };



        #region Funkcje

        #region Color
        //Przypisuje kolory ( w srodku funkcji porównuje je z funkcja StarAndOtherPicture czy pokryc kolorem)
        private void Color(Control set, Color color)
        {
            foreach (Control element in set.Controls)
            {
                if (element is PictureBox)
                {
                    if (element.Name == StarAndOtherPicture(panelPicture)) continue;
                    else ((PictureBox)element).BackColor = color;
                }
            }
        }

        //Przypisuje nazwe pictureBoxa
        private string StarAndOtherPicture(Control set)
        {
            string pictureName = string.Empty;

            foreach (Control element in set.Controls)
            {
                if (element is PictureBox)
                {
                    pictureName = element.Name;
                }
            }

            return pictureName;
        }

        //Zmiana koloru tekstu w menu na czerwony
        private void ChangeColorToRed(ToolStripMenuItem elementName)
        {
            elementName.ForeColor = System.Drawing.Color.Red;
        }

        //Zmiana koloru tekstu w menu na zielony
        private void ChangeColorToGreen(ToolStripMenuItem elementName)
        {
            elementName.ForeColor = System.Drawing.Color.Green;
        }


        //zmienia color czcionki na biały w polach textbox i richtextbox
        private void ChangeColorToWhite(Control set)
        {
            foreach (Control element in set.Controls)
            {
                if (element is RichTextBox || element is TextBox)
                {
                    element.ForeColor = System.Drawing.Color.White;
                }
            }
        }

        #endregion ColorEnd

        //Czysci Labele i wstawia myślniki
        private void LabelClearTextInsertDash(Label labelName)
        {
            labelName.Text = dash;
        }

        //pokazuje gwiazdki z ratingu
        public void ShowStar(int id, PictureBox _name, string variableName)
        {
            var s = from p in Rating.categoryRating
                    where p.Id == id
                    select p;

            foreach (var c in s)
            {
                if (variableName == c.Id.ToString() && id == 1)
                {
                    _name.Visible = true;
                }
                else if (variableName == c.Id.ToString() && id == 2)
                {
                    _name.Visible = true;
                    variableName = 1.ToString();
                    ShowStar(1, pbStar1, variableName);
                }
                else if (variableName == c.Id.ToString() && id == 3)
                {
                    _name.Visible = true;
                    variableName = 2.ToString();
                    ShowStar(2, pbStar2, variableName);
                }
            }
        }

        //Ukrywa gwiazdki
        private void StarHide(PictureBox pictureName)
        {
            pictureName.Visible = false;
        }

        //sprawdza czy checkbox jest zaznaczony (1) czy nie (0)
        private void IsCheckBoxChecked(CheckBox elementName, int number, int[] snc)
        {
            if (elementName.Checked)
            {
                snc[number - 1] = 1;
            }
            else
            {
                snc[number - 1] = 0;
            }
        }

        //dodaje znak w pustym polu (zapobiegając przesuwaniu się do góry) stringOfCharacters
        private string[] AddStampAndBlockMovingWhenEmptyRows(string[] tableName)
        {
            string[] newTable;

            if (tableName.Length == 0)
            {
                newTable = new string[tableName.Length + 1];
            }
            else
            {
                newTable = new string[tableName.Length];
                newTable = tableName;
            }


            for (int i = 0; i < newTable.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(newTable[i]))
                {
                    newTable[i] = stringOfCharacters + stringOfCharacters1.ToString();
                }
                else
                {
                    continue;
                }
            }

            return tableName = newTable;
        }

        //funkcja w bloku
        private void AddStampBlock()
        {
            rtxtAmountsOfFood.Lines = AddStampAndBlockMovingWhenEmptyRows(rtxtAmountsOfFood.Lines);
            rTxtGrams.Lines = AddStampAndBlockMovingWhenEmptyRows(rTxtGrams.Lines);
            rTxtIngredients.Lines = AddStampAndBlockMovingWhenEmptyRows(rTxtIngredients.Lines);
            txtShortDescription.Lines = AddStampAndBlockMovingWhenEmptyRows(txtShortDescription.Lines);
            rtxtDescription.Lines = AddStampAndBlockMovingWhenEmptyRows(rtxtDescription.Lines);
        }

        //usuwa dodatkowe znaki ][
        private string RemoveExtraCharacters(RichTextBox name)
        {
            string[] table = new string[name.Lines.Length];
            table = name.Lines;

            for (int i = 0; i < name.Lines.Length; i++)
            {
                if (table[i] == stringOfCharacters + stringOfCharacters1.ToString())
                {
                    table[i] = "";
                }
            }

            name.Lines = table;
            return name.Text;
        }

        //funkcja- blok- przypisanie jednej funkcji do kilku textboxów
        private void RemoveExtraCharactersBlock()
        {
            rtxtAmountsOfFood.Text = RemoveExtraCharacters(rtxtAmountsOfFood);
            rTxtGrams.Text = RemoveExtraCharacters(rTxtGrams);
            rTxtIngredients.Text = RemoveExtraCharacters(rTxtIngredients);
            txtShortDescription.Text = RemoveExtraCharacters(txtShortDescription);
            rtxtDescription.Text = RemoveExtraCharacters(rtxtDescription);
        }

        //Funkcja pamięciowa (chceckboxy) będą zaznaczone jak zamknie się formę 2 a otworzy 1
        public void RememberIfCheckBoxChecked(Form1 name)
        {
            for (int i = 0; i < checkBoxesCancelForm2Ing.Length; i++)
            {
                name.ingridients[i] = Convert.ToInt16(checkBoxesCancelForm2Ing[i]);
            }

            for (int i = 0; i < checkBoxesCancelForm2Meal.Length; i++)
            {
                name.idMeal[i] = Convert.ToInt16(checkBoxesCancelForm2Meal[i]);
            }
        }

        //Zmienia Text EnterOn na EnterOff
        private void ChangeEnterNameInMeunuStrip()
        {
            CMAmountsEnter.Text = enterOff;
            CMGramsEnter.Text = enterOff;
            CMIngridientsEnter.Text = enterOff;

        }

        //maksymalna ilość linii w formach
        static int lenght = 38;
        private void NumberOfLines(KeyEventArgs e, RichTextBox _name)//KeyEventArgs
        {
            int start = _name.SelectionStart;

            if (e.KeyCode == Keys.Enter && _name.Lines.Length >= lenght)
            {
                MessageBox.Show("Program nie może mieć więcej już linii");
                string[] tempTable = _name.Lines;
                tempTable[lenght - 1] = null;
                _name.Lines = tempTable;
                e.Handled = true;
                _name.SelectionStart = start;
            }
        }

        int variableToCountNumberOfLines = 0;
        //Sprawdzanie ilości linii po wklejeniu
        private void NumberLinesAfterPasted(RichTextBox elementName)
        {
            string precautionary = elementName.Text;

            elementName.Paste();
            //  variableToCountNumberOfLines = NumberLinesHowManyLines(elementName.Text);

            if (numberLine > lenght)//variableToCountNumberOfLines
            {
                elementName.Text = precautionary;
                MessageBox.Show("Program może posiadać tylko 38 linii.\n Przekroczyłeś je wklejając tekst.");
            }

            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);

        }

        //Jesli pierwsza linia nie istnieje
        private void IfFirstLineIsNull(int selectionStart)
        {
            if (selectionStart == 0 && maxLine == 0) { }
            else
            {
                CheckIfDataIsCorrect();
            }
        }

        static int newLine = 0;
        private static int AddLine()
        {
            newLine++;

            return newLine;
        }

        //seprator gwaizdkowy - poprawić (tak zeby gwiazdki same sie dodawaly w zaleznosci od szerokosci pola)
        private void Separator(RichTextBox name, int quantityStar)
        {
            string starInsert = "           *";
            string prefix = "           ";
            int i = name.SelectionStart;

            for (int j = 0; j < quantityStar; j++)
            {
                name.Text = name.Text.Insert(i, prefix + starInsert + "      ");
            }

            name.Text = name.Text.Insert(i, "\n");
            name.SelectionStart = name.TextLength;

            i = name.SelectionStart;
            name.Text = name.Text.Insert(i, "\n " + " " + interval + " ");
            name.SelectionStart = 4 + i;
        }

        //Punktor
        char sign = '•';
        string window = "     ";
        int additionalNumberOfCharacters = 12;
        private void Punktor(KeyEventArgs e)
        {
            int i = rtxtDescription.SelectionStart;


            RichTextBoxMy.NewLine(rtxtDescription);
            rtxtDescription.AppendText(window + sign + window);

            e.Handled = true;
            rtxtDescription.SelectionStart = i + additionalNumberOfCharacters;
        }

        private void Punktor(KeyEventArgs e, RichTextBox name)
        {
            // EnterOffBoolDescription = true;
            int i = rtxtDescription.SelectionStart;

            RichTextBoxMy.NewLine(rtxtDescription);
            rtxtDescription.Text = rtxtDescription.Text.Insert(i, Environment.NewLine + window + sign + window);

            e.Handled = true;
            rtxtDescription.SelectionStart = i + additionalNumberOfCharacters;
        }

        /// <summary>
        /// Oblicza szerokość linii na podstawie richtextbox.Lines[numberline]
        /// </summary>
        int space = 0;
        string line;
        float lineWidth;
        public float LineWidth(KeyPressEventArgs a)
        {
            if (string.IsNullOrEmpty(rtxtDescription.Text))
            {
                line = a.KeyChar.ToString();
            }
            else
            {
                line = rtxtDescription.Lines[numberLineDescription];
                lineWidth = graph.MeasureString(line, letter).Width;
            }

            if (a.KeyChar == 32)
            {
                space++;
                lineWidth += ((float)4.999999 * space);
            }
            else
            {
                space = 0;
            }

            return lineWidth;
        }

        /// <summary>
        /// Zlicza ilość przerw przed punktorem
        /// </summary>
        /// <param name="NameofStringInWhichEnterWillbeCounted"></param>
        /// <returns></returns>
        private void NumberOfEnter(string NameofStringInWhichEnterWillbeCounted)
        {

            foreach (var item in NameofStringInWhichEnterWillbeCounted)
            {
                if (item == '\n')
                {
                    AddLine();
                }
            }

        }

        /// Dzieli string na linie - ma zastosowanie podczas wklejania tekstu
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string SplitLinesIntoSublines(string name)
        {
            double lineWidth = 0;
            double lineWidthPrefix = 0;
            string Line = string.Empty;
            string wordMovedToNextLine = string.Empty;
            string ShortenedStringbyThLastWord = string.Empty;
            string main = string.Empty;
            string suportingString = string.Empty;
            string prefix = rtxtDescription.Lines[numberLineDescription];

            string lineHide = string.Empty;
            bool prefixExist = false;

            if (prefix.Length > 0)
            {
                foreach (var item in prefix)
                {

                    lineHide += item;


                    lineWidthPrefix = graph.MeasureString(lineHide, letter).Width;
                }
                prefixExist = true;
            }

            foreach (char item in name)
            {
                if (item != '\n' && item != '\r')
                {
                    Line += item;
                }

                if (prefixExist)
                {

                    lineWidth = graph.MeasureString(Line, letter).Width;
                    lineWidth += lineWidthPrefix;
                    prefixExist = false;
                }
                else
                {
                    lineWidth = graph.MeasureString(Line, letter).Width;
                }


                if (lineWidth >= rtxtDescription.Width - 95)
                {
                    lineWidth = 0;

                    wordMovedToNextLine = CharacterCounting(Line);
                    ShortenedStringbyThLastWord = DeletesLastWordInLine(Line);

                    suportingString = ShortenedStringbyThLastWord + '\n' + window + window + signLength + wordMovedToNextLine;//

                    main += suportingString;
                    Line = string.Empty;
                    suportingString = string.Empty;
                    AddLine();
                }
                else
                {
                    if ((item == '\n') && (lineWidth < rtxtDescription.Width - 95))
                    {
                        suportingString += item + window + window + signLength;
                        Line = string.Empty;
                        main += suportingString;

                        suportingString = string.Empty;
                        lineWidth = 0;
                        AddLine();
                    }
                    else if (item != '\r')
                    {
                        suportingString += item;
                    }
                }
            }
            main += suportingString;
            clipboardTextLength = main.Length;
            return main;
        }


        /// Dzieli string na linie - ma zastosowanie podczas wklejania tekstu
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string SplitLinesIntoSublines2(string name)
        {
            double lineWidth = 0;
            double lineWidthPrefix = 0;
            string Line = string.Empty;
            string wordMovedToNextLine = string.Empty;
            string ShortenedStringbyThLastWord = string.Empty;
            string main = string.Empty;
            string suportingString = string.Empty;
            string prefix = string.Empty;

            try
            {
                prefix = rtxtDescription.Lines[numberLineDescription];
            }
            catch (Exception ex)
            {
                //trzeba zlikwidować błąd
            }

            string lineHide = string.Empty;
            bool prefixExist = false;

            if (prefix.Length > 0)
            {
                foreach (var item in prefix)
                {

                    lineHide += item;


                    lineWidthPrefix = graph.MeasureString(lineHide, letter).Width;
                }
                prefixExist = true;
            }

            foreach (char item in name)
            {
                if (item != '\n' && item != '\r')
                {
                    Line += item;
                }

                if (prefixExist)
                {
                    lineWidth += lineWidthPrefix;
                    lineWidth = graph.MeasureString(Line, letter).Width;
                    prefixExist = false;
                }
                else
                {
                    lineWidth = graph.MeasureString(Line, letter).Width;
                }


                if (lineWidth >= rtxtDescription.Width)//- 35
                {
                    lineWidth = 0;

                    wordMovedToNextLine = CharacterCounting(Line);
                    ShortenedStringbyThLastWord = DeletesLastWordInLine(Line);

                    suportingString = ShortenedStringbyThLastWord + '\n' + wordMovedToNextLine;

                    main += suportingString;
                    Line = string.Empty;
                    suportingString = string.Empty;
                    AddLine();
                }
                else
                {
                    if ((item == '\n') && (lineWidth < rtxtDescription.Width))//-35
                    {
                        suportingString += item;
                        Line = string.Empty;
                        main += suportingString;

                        suportingString = string.Empty;
                        lineWidth = 0;
                    }
                    else if (item != '\r')
                    {
                        suportingString += item;
                    }
                }
            }
            main += suportingString;

            clipboardTextLength = main.Length;
            return main;
        }

        /// <summary>
        /// Usuwa ostatni wyraz w linii i zwraca string bez tego wyrazu
        /// </summary>
        /// <param name="textLine"></param>
        /// <returns></returns>
        private string DeletesLastWordInLine(string textLine)
        {
            string text = textLine;

            int lengthOfTheLastString = CharacterCounting(textLine).Length;

            for (int i = 0; i < lengthOfTheLastString; i++)
            {
                text = text.Remove(text.Length - 1);
            }

            return text;
        }

        /// <summary>
        /// Zlicza znaki - czy linia nie jest szersza niż textbox
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string CharacterCounting(string text)
        {
            string textTemp = string.Empty;

            if (!text.EndsWith(" \n"))
            {
                foreach (char item in text)
                {
                    if (item != ' ')
                    {
                        textTemp += item;
                    }
                    else
                    {
                        textTemp = string.Empty;
                    }
                }
            }

            return textTemp;
        }

        public int NumberOfCharactersToFocus(string textboxText)
        {
            int start = rtxtDescription.SelectionStart;

            string text = string.Empty;
            numberLineDescription = 0;
            int temp = 0;

            foreach (char item in textboxText)
            {
                if (start <= temp)
                {
                    break;
                }
                else
                {
                    if (item == '\n')
                    {
                        numberLineDescription++;
                        temp++;
                    }
                    else
                    {
                        text += item;
                        temp++;

                    }
                }
            }

            return numberLineDescription;
        }


        //podswietla pole gdy ktoś w dziwny sposob zaznaczy - spojrzeć na ta funkcje
        private void HighlightingEditFieldInOtherCases(KeyEventArgs e, RichTextBox name, bool nameBool)
        {
            if (nameBool == false && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Escape)
            {
                DisplayForm(name);
                nameBool = true;
            }
        }

        private void HighlightingEditFieldInOtherCases(KeyEventArgs e, TextBox name, bool nameBool)
        {
            if (nameBool == false && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Escape)
            {
                DisplayForm(name);
                nameBool = true;
            }
        }

        //Usuwa puste linie w 3 głownych textbokxach
        private void DeleteEmptyLines()
        {
            rtxtAmountsOfFood.Text = rtxtAmountsOfFood.Text.TrimEnd();
            rTxtGrams.Text = rTxtGrams.Text.TrimEnd();
            rTxtIngredients.Text = rTxtIngredients.Text.TrimEnd();
        }

        //MenuContext blok
        public void ContextMenuBlock()
        {
            if (txtName.ReadOnly == false) txtName.ContextMenuStrip = contextCopy;
            else txtName.ContextMenuStrip = contextName;
            Function.ChangeContextMenu(rtxtAmountsOfFood, contextCopy, contextAmounts);
            Function.ChangeContextMenu(rTxtGrams, contextCopy, ContextMenuGrams);
            Function.ChangeContextMenu(rTxtIngredients, contextCopy, contextIngridients);
            Function.ChangeContextMenu(txtShortDescription, contextCopy, contextShortDesription);
            Function.ChangeContextMenu(rtxtDescription, contextCopy, contextLongDescription);
        }

        //Wyrównanie linii
        private void LineAlignment()
        {
            p.CopyTextToList(rtxtAmountsOfFood.Text);
            p.CompareBuforWithSelection();
            por.CopyTextToList(rtxtPortion.Text);
            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
            rtxtAmountsOfFood.Lines = p.TextOutput();
            rtxtPortion.Lines = por.TextOutput();
        }

        string enterOn = "ENTER ON";
        string enterOff = "ENTER OFF";

        //Przelaczenie między Enterem a przeskakiwaniem miedzy polami
        private void ContextEnter(ToolStripMenuItem first, ToolStripMenuItem second, ToolStripMenuItem third)
        {

            if (p.EnterOn == true)
            {

                addRecipe = false;
                p.EnterOn = false;

                first.Text = enterOff;
                second.Text = enterOff;
                third.Text = enterOff;

                ChangeColorToRed(first);
                ChangeColorToRed(second);
                ChangeColorToRed(third);
            }
            else
            {
                p.EnterOn = true;
                addRecipe = true;

                first.Text = enterOn;
                second.Text = enterOn;
                third.Text = enterOn;

                ChangeColorToGreen(first);
                ChangeColorToRed(second);
                ChangeColorToRed(third);
            }
        }

        //przycina dodatkowe spacje w nazwie i ustawia fokus na koncu 
        private void SetFocusToTheEndOfTheName(TextBox name)
        {
            try
            {
                if (addRecipe == false)
                {
                    if (name.Text.Last() != 32)
                    {
                        name.SelectionStart = name.TextLength;
                    }
                    else
                    {
                        name.Text = name.Text.TrimEnd();
                        name.SelectionStart = name.TextLength;
                    }
                }
            }
            catch (Exception ex)
            {
                // ex.Message;
            }
        }

        private void SetFocusToTheEndOfTheName(RichTextBox name)
        {
            if (addRecipe == false)
            {
                if (name.Text.Last() != 32)
                {
                    name.SelectionStart = name.TextLength;
                }
                else
                {
                    name.Text = name.Text.TrimEnd();
                    name.SelectionStart = name.TextLength;
                }
            }
        }

        //funkcja zarzadzajaca fokusem
        private void ChangeFocusNewProject(RichTextBox first, RichTextBox second, KeyEventArgs e)
        {
            if (p.AddRecipeForm2 == true)
            {
                RichTextBoxMy.FirstLine(first, second, e);
            }
            else if (e.KeyCode == Keys.Enter && p.EnterOn == false)
            {
                int tempNumberLine = numberLine;
                p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                numberLine = tempNumberLine;

                second.Focus();

                e.Handled = true;

                StartFocus(second);
            }
        }

        //przelicza znaki do fokusa w zaleznosci od roznych czynnikow
        private void StartFocus(RichTextBox second)
        {
            try
            {
                if ((string.IsNullOrWhiteSpace(second.Lines[p.NumberLine]) && !string.IsNullOrWhiteSpace(second.Lines[p.NumberLine]) && second != rtxtAmountsOfFood) || (p.NumberLine == 0 && second != rtxtAmountsOfFood))
                {

                    second.SelectionStart = p.NumberOfCharactersToFocus(second.Text);
                }
                else
                {
                    if ((changeLine && (p.NumberLine < maxLine)))
                    {
                        if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Lines[p.NumberLine + 1]))
                        {
                            second.SelectionStart = p.NumberOfCharactersToFocus(second.Text) + rtxtAmountsOfFood.Lines[p.NumberLine + 1].Length + 1;
                        }
                        else
                        {
                            second.SelectionStart = p.NumberOfCharactersToFocus(second.Text) + 1;//+ 1
                        }

                    }
                    else
                    {
                        second.SelectionStart = p.NumberOfCharactersToFocus(second.Text);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //pomocnicza dla wyliczenia numberline
        public int NumberOfLinesInColumn(string name)
        {
            int number = 0;

            foreach (char item in name)
            {
                if (item == '\n') number++;
            }

            return number;
        }

        //funkcja przeliczająca ilości porcji
        double convertNumbers;
        string convertportions = "Przelicz Porcje";
        string convert = "Przelicz";

        private void ConvertFunction()
        {
            if (rtxtAmountsOfFood.ReadOnly == false)
            {
                MessageBox.Show("Przeliczać porcje można dopiero po dodaniu przepisu lub po wykonaniu jego modyfikacji");
            }
            else
            {
                if (btnConvert.Text == convertportions)
                {
                    btnConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
                    btnModify.Enabled = false;
                    btnAdd.Enabled = false;

                    rtxtPortion.ReadOnly = false;
                    rtxtPortion.BackColor = Function.CreateBrightColor();
                    pbConvert.BackColor = Function.CreateBrightColor();
                    convertNumbers = Convert.ToDouble(rtxtPortion.Text);
                    btnConvert.Text = convert;

                }
                else
                {

                    if (rtxtAmountsOfFood.ReadOnly == true && rtxtPortion.ReadOnly == false)
                    {
                        int nrLinii = rtxtAmountsOfFood.Lines.Length;
                        double[] sk = new double[nrLinii];
                        double[] liczba = new double[nrLinii];
                        string[] liczba2 = new string[nrLinii];

                        if (rtxtPortion.Text == "")
                        {
                            rtxtPortion.BackColor = Function.CreateBrightColor();
                            rtxtPortion.ReadOnly = false;
                        }
                        else
                        {
                            p.CopyTextToList(rtxtAmountsOfFood.Text);
                            p.CompareBuforWithSelection();
                            bool copy = false;
                            for (int i = 0; i < rtxtAmountsOfFood.Lines.Length; i++)
                            {
                                foreach (var item in RichTextBoxMy.ulamki)
                                {
                                    if (rtxtAmountsOfFood.Lines[i] == item)
                                    {
                                        if (!copy)
                                        {
                                            p.CopyTextToList(rtxtAmountsOfFood.Text);
                                            p.CompareBuforWithSelection();
                                            copy = true;
                                        }

                                        p.howeverDelete = true;
                                        p.ConvertToDecimalFraction(rtxtAmountsOfFood.Lines[i], i);

                                    }
                                }
                            }
                            rtxtAmountsOfFood.Lines = p.TextOutput();
                            if (double.TryParse(convertNumbers.ToString(), out converted))
                            {
                                for (int i = 0; i < nrLinii; i++)
                                {
                                    if (double.TryParse(rtxtAmountsOfFood.Lines[i], out sk[i]))
                                    {
                                        liczba[i] = sk[i] / convertNumbers;
                                        liczba2[i] = liczba[i].ToString();
                                    }
                                }
                            }
                            rtxtAmountsOfFood.Lines = liczba2;
                        }
                    }
                    if (rtxtPortion.Text != "" && rtxtPortion.Text != "0")
                    {
                        int numberLine = rtxtAmountsOfFood.Lines.Length;
                        double[] sk2 = new double[numberLine];
                        double[] numberOne = new double[numberLine];
                        string[] numberTwo = new string[numberLine];

                        if (double.TryParse(rtxtPortion.Text, out converted))
                        {
                            for (int i = 0; i < numberLine; i++)
                            {
                                if (rtxtAmountsOfFood.Lines[i] == "") continue;
                                if (double.TryParse(rtxtAmountsOfFood.Lines[i], out sk2[i]))
                                {
                                    numberOne[i] = Math.Round(sk2[i] * converted, 2);
                                    numberTwo[i] = numberOne[i].ToString();
                                }
                                else
                                {
                                    MessageBox.Show("W tej rubryce można wpisywać tylko ilości");
                                }
                            }
                            for (int i = 0; i < numberLine; i++)
                            {
                                rtxtAmountsOfFood.Lines = numberTwo;
                            }
                        }
                        rtxtPortion.BackColor = Function.CreateColor();
                        pbConvert.BackColor = Function.CreateColor();
                        rtxtPortion.ReadOnly = true;
                        rtxtAmountsOfFood.Visible = true;
                        btnConvert.Text = convertportions;
                        btnConvert.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(60)))));
                    }
                    btnModify.Enabled = true;
                    btnAdd.Enabled = true;
                }
            }
        }

        //Funkcja Zlozona do KeyDown
        private void AmountsAndGramsKeyDown(KeyEventArgs e, RichTextBox first, RichTextBox second, RichTextBox third)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (p.EnterOn)
                {
                    NumberOfLines(e, first);

                    p.ClassicEnterPlusNewLine(first, second, third, numberLine);
                }
                else
                {
                    NumberOfLines(e, first);

                    ChangeFocusNewProject(first, second, e);

                    DisplayForm(second);
                }
            }
            else if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {
                UndoChanges();
            }
        }

        //numer linii itd
        private void IndexChar(RichTextBox name)
        {
            int index = name.SelectionStart;
            numberLine = name.GetLineFromCharIndex(index);
            p.NumberLine = numberLine;//name.GetLineFromCharIndex(index);


            if (numberLine >= maxLine)
            {
                maxLine = numberLine;
                p.MaxLine = p.NumberLine;

                if ((maxLine > p.bufor.Count))
                {
                    p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                }
            }
            else
            {
                p.AddRecipeForm2 = false;
            }
        }

        int numberLineDescription;
        int maxLineDescription;
        private void IndexCharDescription(RichTextBox name)
        {
            int index = name.SelectionStart;
            numberLineDescription = name.GetLineFromCharIndex(index);

            if (numberLineDescription >= maxLineDescription)
            {
                maxLineDescription = numberLineDescription;
            }

        }

        //sprawdza czy zapis nie jest ulamkowy (1/2) i w razie co zamienia na dziesietny
        int numberLineZero = 99;
        private void CheckIfLineDoesNotContainSlash()
        {
            if (numberLineZero != 99)
            {
                if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Text))
                {
                    if (rtxtAmountsOfFood.Lines[numberLineZero].Contains('/'))
                    {
                        int start = p.GetLengthIfRemoved(numberLineZero);
                        p.ConvertToDecimalFraction(rtxtAmountsOfFood.Lines[numberLineZero], numberLineZero);
                        rtxtAmountsOfFood.Lines = p.TextOutput();

                        rtxtAmountsOfFood.SelectionStart = start;
                    }
                }
            }
        }

        //funkcja wywolywana po kliknieciu lub nacisnieciu enter po zerze
        private void ClearError(RichTextBox name)
        {
            IfFirstLineIsNull(name.SelectionStart);
        }

        //Najwieksza linia
        bool changeLine;
        private void MaxLineIncrease()
        {
            if (maxLine <= numberLine)
            {
                p.AddRecipeForm2 = true;
                changeLine = false;
            }
            else
            {
                p.AddRecipeForm2 = false;
                changeLine = true;
            }
        }

        //Zmienia dodawanie Linii w zaleznosci od pozycji kursora 
        public void ChangeAddLine(KeyEventArgs e)
        {
            if (numberLine < maxLine)
            {
                ChangeFocusNewProject(rTxtIngredients, rtxtAmountsOfFood, e);
            }
            else
            {
                RichTextBoxMy.NewLine(rTxtGrams);
                RichTextBoxMy.NewLine(rTxtIngredients);
                p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                rtxtAmountsOfFood.Lines = p.TextOutput();

                rtxtAmountsOfFood.Focus();
                rtxtAmountsOfFood.ChangeColorOneElement();

                e.Handled = true;
                rtxtAmountsOfFood.SelectionStart = rtxtAmountsOfFood.Text.Length;

                p.AddRecipeForm2 = false;
            }
        }

        //Sprawdza czy wprowadzone dane są prawidłowe
        private bool rtxAmountsClick;
        private void CheckIfDataIsCorrect()
        {
            if (numberLineZero != 99)
            {
                if (p.Deleted || p.ClearTypedIncorrectly(rtxtAmountsOfFood.Lines[numberLineZero]))
                {
                    p.Deleted = true;

                    p.NumberLine = numberLineZero;
                    numberLine = numberLineZero;

                    p.ReplaceLine();

                    int start;
                    if (rtxtAmountsOfFood.Lines.Length == 0)
                    {
                        start = 0;
                        rtxAmountsClick = false;
                    }
                    else if (rtxAmountsClick == true)
                    {
                        start = rtxtAmountsSelectionStartTemp;
                        rtxAmountsClick = false;
                    }
                    else
                    {
                        start = p.GetLengthIfRemoved(rtxtAmountsOfFood.Lines[numberLineZero].Length);
                        rtxAmountsClick = false;
                    }

                    rtxtAmountsOfFood.Lines = p.TextOutput();

                    p.NumberLine = numberLineZero;
                    numberLine = numberLineZero;

                    p.StartSelection = start;

                    rtxtAmountsOfFood.Focus();
                    rtxtAmountsOfFood.SelectionStart = p.StartSelection;

                    DisplayForm(rtxtAmountsOfFood);

                    p.Deleted = false;
                    numberLineZero = 99;
                }
            }
        }

        //Dane są błędne - sprawdza poprawnosc i zmienia kolor po wcisnieciu na checkbox
        private void DataIsFailed()
        {
            CheckIfLineDoesNotContainSlash();
            CheckIfDataIsCorrect();
        }

        //podswietla edytowany element
        public void DisplayForm(RichTextBox rich)
        {
            txtName.Text = txtName.Text.ToUpper();
            rich.ChangeColorOneElement();
            ButtonMy.BorderColor(rich, panelMain, panelPicture);
            rich.ChangeForeColorToBlack();
            rich.Focus();
        }

        public void DisplayForm(TextBox rich)
        {
            txtName.Text = txtName.Text.ToUpper();
            rich.ChangeColorOneElement();
            ButtonMy.BorderColor(rich, panelMain, panelPicture);
            rich.ChangeForeColorToBlack();
        }

        private bool CheckNameAfterClick()
        {
            bool correct = false;

            txtName.Text = txtName.Text.ToUpper();

            if (Function.CheckName(txtName, correctModyficationName))
            {
                correct = true;
                txtName.Text = "";
                DisplayForm(txtName);
                txtName.Focus();
            }

            return correct;
        }

        //funkcja grupująca
        private void CheckChangedGroup(CheckBox nameElement, int locationNumberInTable, int[] tabMealOrIngredients)
        {
            IsCheckBoxChecked(nameElement, locationNumberInTable, tabMealOrIngredients);
            CheckNameAfterClick();
            DataIsFailed();

            Function.ColorAreaAfterUnblocking(panelMain);
            Color(panelMain, Function.CreateBrightColor());
            ChangeColorToWhite(panelMain);
        }

        //cofnij zmiany-anuluj
        private void UndoChanges()
        {
            if (cancel == true)
            {

                linkForm2 = photo;
                pbLittlePhoto.ImageLocation = linkForm2;

                chcFish.Checked = checkBoxesCancel[0];
                chcPasta.Checked = checkBoxesCancel[1];
                chcFruits.Checked = checkBoxesCancel[2];
                chcMuschrooms.Checked = checkBoxesCancel[3];
                chcBird.Checked = checkBoxesCancel[4];
                chcMeat.Checked = checkBoxesCancel[5];
                chcEggs.Checked = checkBoxesCancel[6];
                chcVegetarian.Checked = checkBoxesCancel[7];

                chcSnack.Checked = checkBoxesCancel[8];
                chcDinner.Checked = checkBoxesCancel[9];
                chcSoup.Checked = checkBoxesCancel[10];
                chcDessert.Checked = checkBoxesCancel[11];
                chcDrink.Checked = checkBoxesCancel[12];
                chcPreserves.Checked = checkBoxesCancel[13];
                chcSalad.Checked = checkBoxesCancel[14];

                txtName.Text = title1;
                rtxtAmountsOfFood.Text = amounts;
                rTxtGrams.Text = grams;
                rTxtIngredients.Text = ingrediet;
                txtShortDescription.Text = shortDes;
                rtxtDescription.Text = longDes;

                rtxtPortion.Text = portions.ToString();
                lblCuisine.Text = cuisines;
                lblLevel.Text = level;
                lblTime.Text = time;

                ShowStar(1, pbStar1, rating);
                ShowStar(2, pbStar2, rating);
                ShowStar(3, pbStar3, rating);

                ContextMenuBlock();

                p.EnterOn = false;
            }
            Function.ColorFieldsAfterBlocking(panelMain, rtxtPortion);
            Function.BlockingFields(panelMain);
            Function.BlockCheckbox(panelLeft);
            Function.BlockCheckbox(panelRight);
            btnAddRest.Visible = false;
            Color(panelMain, Function.CreateColorBlockingFields());
            btnCancel.Visible = false;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            btnModify.Visible = true;
            addRecipe = false;
            CMGramsEnter.Text = enterOff;
            CMAmountsEnter.Text = enterOff;
            CMIngridientsEnter.Text = enterOff;
            ButtonMy.ChangeForeColorToWhite(panelMain);

            cancel = false;
        }

        #endregion Function
        public Form2()
        {
            InitializeComponent();
        }


        public string linkForm22;

        public string correctModyficationName;

        private void Form2_Load(object sender, EventArgs e)
        {

            if (titleForm2 == null)
            {
                newForm = true;
                txtNameBool = true;
                ContextMenuBlock();
                ChangeEnterNameInMeunuStrip();

                Function.UnblockingFields(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);

                Color(panelMain, Function.CreateBrightColor());


                btnClose.Text = "Anuluj";

                btnDelete.Visible = false;
                btnModify.Visible = false;
                btnAddRest.Visible = true;
                lblTime.Visible = true;

                clear = add;
                p.AddRecipeForm2 = true;

                chcVegetarian.Enabled = true;
                this.CancelButton = btnClose;



            }
            else if (unlockFieldsForm2 == "1" && txtName.ReadOnly == true)
            {
                correctModyficationName = titleForm2;

                ContextMenuBlock();
                ChangeEnterNameInMeunuStrip();
                txtNameBool = true;

                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockingFields(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);

                Function.DisplaySelectionRightPanel(panelRight, IdMealForm2);
                Function.DisplaySelectionRightPanel(panelLeft, ingridientsForm2);

                Color(panelMain, Function.CreateBrightColor());

                txtName.Text = titleForm2;
                Function.UncheckText(txtName);
                rtxtPortion.Text = numberOfPortionsForm2.ToString();
                rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                rTxtGrams.Text = gramsForm2;
                rTxtIngredients.Text = ingredientForm2;
                txtShortDescription.Text = ShortDescriptionForm2;
                rtxtDescription.Text = instructionForm2;

                lblCuisine.Text = listOfCuisinesForm2;
                lblLevel.Text = difficultLevelForm2;
                lblTime.Text = executionTimeForm2;

                ShowStar(1, pbStar1, idRatingForm2);
                ShowStar(2, pbStar2, idRatingForm2);
                ShowStar(3, pbStar3, idRatingForm2);

                if (linkForm2 == dash) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = linkForm2;

                chcVegetarian.Enabled = true;
                if (ingridientsForm2[7] == 1) chcVegetarian.Checked = true;

                if (clear == add)
                {
                    btnAddRest.Text = addRest;
                    btnAddRest.Visible = true;
                    btnModify.Visible = false;
                    btnDelete.Visible = false;
                    btnCancel.Visible = true;
                }
                else if (clear == "modification")
                {
                    btnAddRest.Text = addRest;
                    btnAdd.Visible = false;
                    btnModify.Visible = true;
                    btnDelete.Visible = false;
                    btnCancel.Visible = true;
                    btnAddRest.Visible = true;
                }
                else
                {
                    btnAddRest.Visible = true;
                    btnModify.Visible = true;
                    btnDelete.Visible = true;
                    btnCancel.Visible = false;
                }

                p.CopyTextToList(rtxtAmountsOfFood.Text);
                p.CompareBuforWithSelection();

            }
            else
            {
                correctModyficationName = titleForm2;
                ChangeEnterNameInMeunuStrip();


                Function.DisplaySelectionRightPanel(panelRight, IdMealForm2);
                Function.DisplaySelectionRightPanel(panelLeft, ingridientsForm2);

                txtName.Text = titleForm2;
          
                rtxtPortion.Text = numberOfPortionsForm2.ToString();

                rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                int i = 0;
                while (rtxtAmountsOfFood.Lines.Length > i)
                {
                    if (rtxtAmountsOfFood.Lines[i].Length >= 5)
                    {
                        rtxtAmountsOfFood.ScrollBars = RichTextBoxScrollBars.Horizontal;
                    }
                    i++;
                }
                rTxtGrams.Text = gramsForm2;
                rTxtIngredients.Text = ingredientForm2;

                txtShortDescription.Text = ShortDescriptionForm2;
                rtxtDescription.Text = instructionForm2;

                if (linkForm2 == dash) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = linkForm2;

                lblCuisine.Text = listOfCuisinesForm2;
                lblLevel.Text = difficultLevelForm2;
                lblTime.Text = executionTimeForm2;
                linkForm22 = linkForm2;

                ShowStar(1, pbStar1, idRatingForm2);
                ShowStar(2, pbStar2, idRatingForm2);
                ShowStar(3, pbStar3, idRatingForm2);

                if (ingridientsForm2[7] == 1) chcVegetarian.Checked = true;
                else chcVegetarian.Checked = false;

                RemoveExtraCharactersBlock();
                pb2.BringToFront();
            }
            if (txtName.ReadOnly == false)
            {
                txtName.ChangeColorOneElement();
                txtName.ForeColor = System.Drawing.Color.Black;
            }

            letter = rtxtDescription.Font;
            graph = this.CreateGraphics();

            correctModyficationName = titleForm2;
            Function.UncheckText(txtName);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        public RecipesBase Model(RecipesBase model)
        {
            if (rtxtPortion.Text == "")
            {
                model.NumberPortions = 1;
            }
            else
            {
                model.NumberPortions = int.Parse(rtxtPortion.Text);
            }

            return model;
        }

        #region Menu
        private void wytnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtName.Cut();
        }

        private void kopiujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtName.Copy();
        }

        private void wklejToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtName.Paste();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtName.SelectedText = "";
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rTxtIngredients.Undo();
        }

        private void wytnijToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rTxtIngredients.Cut();
        }

        private void kopiujToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rTxtIngredients.Copy();
        }

        private void wklejToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NumberLinesAfterPasted(rTxtIngredients);
            DisplayForm(rTxtIngredients);

            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
            rtxtAmountsOfFood.Lines = p.TextOutput();
        }

        private void usuńToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rTxtIngredients.SelectedText = "";
        }

        private void cofnijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rtxtAmountsOfFood.Undo();
        }

        private void kopiujToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtAmountsOfFood.Copy();
        }

        private void wklejToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            NumberLinesAfterPasted(rtxtAmountsOfFood);
        }

        private void usuńToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtAmountsOfFood.SelectedText = "";
        }

        private void separatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Separator(rtxtDescription, 5);
        }

        private void cofnijToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtDescription.Undo();
        }

        private void wytnijToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.Cut();
        }

        bool copyDescriptionBool = false;
        string compareCopy = string.Empty;
        private void kopiujToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            copyDescriptionBool = true;
            rtxtDescription.Copy();
        }

        int clipboardTextLength;
        private void wklejToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string clipboardText = string.Empty;
            string rozbity = string.Empty;
            int start = rtxtDescription.SelectionStart;

            if (Clipboard.ContainsText(TextDataFormat.Text) && EnterOffBoolDescription)
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);

                if (!copyDescriptionBool)
                {
                    rozbity = SplitLinesIntoSublines(clipboardText);
                }
                else
                {
                    rozbity = SplitLinesIntoSublines2(clipboardText);
                }




                rtxtDescription.Text = rtxtDescription.Text.Insert(start, rozbity);
                //NumberOfEnter(clipboardText);
                //int count = AddLine();
                //count = count - 2;


                rtxtDescription.SelectionStart = start + clipboardTextLength;


                EnterOffBoolDescription = true;
                // newLine = 0;

            }
            else
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);

                rozbity = SplitLinesIntoSublines2(clipboardText);
                rtxtDescription.Text = rtxtDescription.Text.Insert(start, rozbity);
                // NumberOfEnter(clipboardText);
                //int count =  AddLine();
                //count--;
                rtxtDescription.SelectionStart = start + clipboardTextLength;
                //  newLine = 0;
            }

        }


        private void usuńToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.SelectedText = "";
        }

        private void włToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmList.Text == PunktorOn)
            {
                cmList.Text = PunktorOff;
                EnterOffBoolDescription = true;
                int i = rtxtDescription.SelectionStart;

                RichTextBoxMy.NewLine(rtxtDescription);
                rtxtDescription.AppendText(window + sign + window);

                rtxtDescription.SelectionStart = i + additionalNumberOfCharacters;
            }
            else
            {
                cmList.Text = PunktorOn;
                EnterOffBoolDescription = false;
            }
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtName.Undo();
        }

        private void cofnijToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtShortDescription.Undo();
        }

        private void wytnijToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            txtShortDescription.Cut();
        }

        private void kopiujToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            txtShortDescription.Copy();
        }

        private void wklejToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            txtShortDescription.Paste();
        }

        private void usuńToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            txtShortDescription.SelectedText = "";
        }

        private void btnConvert_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnConvert, "Przelicza przepis na wybraną ilość osób");
        }

        private void btnAddRest_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAddRest, "Dodaj - Zdjęcie, czas przygotowania,\n stopień trudności i rodzaj kuchni");
        }
        private void ChangeColorButtonEnterWhenShortCut()
        {
            if (btnEnter.Text == "OFF")
            {
                btnEnter.Text = "ON";
                btnEnter.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                btnEnter.Text = "OFF";
                btnEnter.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void eNTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContextEnter(CMAmountsEnter, CMGramsEnter, CMIngridientsEnter);
            ChangeColorButtonEnterWhenShortCut();
        }

        private void CMGramsEnter_Click(object sender, EventArgs e)
        {
            ContextEnter(CMGramsEnter, CMAmountsEnter, CMIngridientsEnter);
            ChangeColorButtonEnterWhenShortCut();
        }

        private void CMIngridientsEnter_Click(object sender, EventArgs e)
        {
            ContextEnter(CMIngridientsEnter, CMAmountsEnter, CMGramsEnter);
            ChangeColorButtonEnterWhenShortCut();
        }

        private void wytnijToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtAmountsOfFood.Cut();
        }

        private void cofnijToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rTxtGrams.Undo();
        }

        private void wytnijToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            rTxtGrams.Cut();
        }

        private void wklejToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            NumberLinesAfterPasted(rTxtGrams);
            DisplayForm(rTxtGrams);
        }

        private void usuńToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            rTxtGrams.SelectedText = "";
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            AddRecipes();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            ModifyRecipes();
        }

        private void toolStripMenuItem13_Click(object sender, EventArgs e)
        {
            DeleteRecipes();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            UndoChanges();
        }

        private void separatorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly)
            {
                separatorToolStripMenuItem1.Enabled = false;
            }
            else
            {
                RichTextBoxMy.NewLine(rtxtAmountsOfFood);
                RichTextBoxMy.NewLine(rtxtAmountsOfFood);
                RichTextBoxMy.NewLine(rTxtGrams);

                Separator(rTxtIngredients, 3);

                RichTextBoxMy.NewLine(rtxtAmountsOfFood);
                RichTextBoxMy.NewLine(rTxtGrams);
                rTxtIngredients.Focus();
            }
        }

        private void kopiujToolStripMenu_Click(object sender, EventArgs e)
        {
            if (txtName.SelectionLength > 0) txtName.Copy();
            else if (rtxtAmountsOfFood.SelectionLength > 0) rtxtAmountsOfFood.Copy();
            else if (rTxtGrams.SelectionLength > 0) rTxtGrams.Copy();
            else if (rTxtIngredients.SelectionLength > 0) rTxtIngredients.Copy();
            else if (txtShortDescription.SelectionLength > 0) txtShortDescription.Copy();
            else if (rtxtDescription.SelectionLength > 0) rtxtDescription.Copy();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            Logo newEmail = new Logo();

            newEmail.titleLogo = txtName.Text;

            newEmail.amountsLogo = rtxtAmountsOfFood.Text;

            newEmail.gramsLogo = rTxtGrams.Text;

            newEmail.ingredientLogo = rTxtIngredients.Text;

            newEmail.descriptionLogo = rtxtDescription.Text;

            this.Hide();

            newEmail.ShowDialog();
        }

        //public String SwapClipboardHtmlText(String replacementHtmlText)
        //{
        //    String returnHtmlText = null;
        //    if (Clipboard.ContainsText(TextDataFormat.Html))
        //    {
        //        returnHtmlText = Clipboard.GetText(TextDataFormat.Html);
        //        Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
        //    }
        //    return returnHtmlText;
        //}

        #endregion Menu

        #region Button

        private void DeleteRecipes()
        {
            if (MessageBox.Show("Czy na pewno usunąć Plik? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    var s = RecipesBase.getById(idDgGridForm2);
                    RecipesBase.del(s.Id);
                    MessageBox.Show("Dokument został usunięty");
                    Form1 m = new Form1();
                    this.Hide();
                    m.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecipes();
        }

        private void ModifyRecipes()
        {

            clear = "modification";
            btnCancel.Text = "Anuluj";

            if (txtName.ReadOnly == true)
            {
                ContextMenuBlock();
                chcVegetarian.Enabled = true;
                AssignmentMainFields();
                linkForm22 = linkForm2;
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockingFields(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);
                btnAddRest.Visible = true;
                btnCancel.Visible = true;
                btnAdd.Visible = false;
                btnDelete.Visible = false;
                btnAddRest.Text = addRest;

                Color(panelMain, Function.CreateBrightColor());

                rTxtIngredients.Focus();
                rTxtIngredients.SelectionStart = rTxtIngredients.TextLength;
                IndexChar(rTxtIngredients);
                maxLine = rTxtIngredients.Lines.Length;


                if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Text) && !string.IsNullOrEmpty(rTxtGrams.Text) && !string.IsNullOrEmpty(rTxtIngredients.Text))
                {
                    RichTextBoxMy.SetFocus(rTxtIngredients, rtxtAmountsOfFood, rTxtGrams);
                    DisplayForm(rTxtIngredients);
                }
            }
            else
            {
                ContextMenuBlock();
                AddStampBlock();

                if (check == false)
                {
                    txtName.Text = txtName.Text.ToUpper();
                    if (txtName.Text == titleForm2) { }
                    else
                    {
                        CheckNameAfterClick();

                        if (newForm)
                        {
                            Function.CheckName(txtName, correctModyficationName);
                        }

                        RemoveExtraCharactersBlock();
                    }

                    if (txtName.Text != "")
                    {
                        AddStampBlock();
                        try
                        {
                            #region PanelMain

                            var up = RecipesBase.getById(idDgGridForm2);
                            up.RecipesName = txtName.Text;
                            up.AmountsMeal = rtxtAmountsOfFood.Text;
                            up.Grams = rTxtGrams.Text;
                            up.Ingredients = rTxtIngredients.Text;
                            up.ShortDescription = txtShortDescription.Text;
                            up.LongDescription = rtxtDescription.Text;
                            if (rtxtPortion.Text == "") rtxtPortion.Text = "1";
                            up.NumberPortions = int.Parse(rtxtPortion.Text);
                            #endregion
                            #region ComponentLeft
                            up.IdFishIngredients = ingridientsForm2[0];
                            up.IdPastaIngredients = ingridientsForm2[1];
                            up.IdFruitsIngredients = ingridientsForm2[2];
                            up.IdMuschroomsIngredients = ingridientsForm2[3];
                            up.IdBirdIngredients = ingridientsForm2[4];
                            up.IdMeatIngredients = ingridientsForm2[5];
                            up.IdEggsIngredients = ingridientsForm2[6];
                            up.Vegetarian = ingridientsForm2[7];

                            #endregion
                            #region MealRight
                            up.SnackMeal = IdMealForm2[0];
                            up.DinnerMeal = IdMealForm2[1];
                            up.SoupMeal = IdMealForm2[2];
                            up.DessertMeal = IdMealForm2[3];
                            up.DrinkMeal = IdMealForm2[4];
                            up.PreservesMeal = IdMealForm2[5];
                            up.SaladMeal = IdMealForm2[6];
                            #endregion

                            up.CategoryPreparationTime = executionTimeForm2;
                            up.CategoryDifficultLevel = lblLevel.Text;
                            up.CategoryPreparationTime = lblTime.Text;
                            if (idRatingForm2 == null) idRatingForm2 = dash;
                            else up.CategoryRating = idRatingForm2;

                            up.CategoryCuisines = lblCuisine.Text;

                            if (linkForm2 == null) linkForm2 = stringOfCharacters.ToString();
                            else up.PhotoLinkLocation = linkForm2;

                            RecipesBase.update(up);
                            chcVegetarian.Enabled = false;
                            RemoveExtraCharactersBlock();
                            MessageBox.Show("Modyfikacja przebiegła pomyślnie!!!");

                            btnCancel.Text = "COFNIJ";
                            Function.BlockingFields(panelMain);
                            Function.BlockCheckbox(panelLeft);
                            Function.BlockCheckbox(panelRight);
                            Function.ColorFieldsAfterBlocking(panelMain, rtxtPortion);
                            btnAddRest.Visible = false;
                            btnAddRest.Text = "Dodaj";
                            clear = "0";
                            btnDelete.Visible = true;
                            btnAdd.Visible = true;
                            btnCancel.Visible = false;
                            Color(panelMain, Function.CreateColorBlockingFields());

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podaj nazwę Przedmiotu");
                    }
                }
                check = false;
                RemoveExtraCharactersBlock();
                ButtonMy.ChangeForeColorToWhite(panelMain);
            }
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == false)
            {
                CheckIfLineDoesNotContainSlash();
                ClearError(rTxtGrams);
            }
            DeleteEmptyLines();
            LineAlignment();

            // kopia rtxgrams do tablicy
            Array.Resize(ref tab, rTxtGrams.Lines.Length);
            CopyTextToTable(rTxtGrams.Text, tab);

            ModifyRecipes();
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            CloseForm();
        }

        public void CloseForm()
        {


            this.Hide();
            Form1 form1 = new Form1();

            RememberIfCheckBoxChecked(form1);
            form1.seekUnsubscribe = seekUnsubscribeForm2;

            form1.counter = counterForm2;
            form1.ShowDialog();


        }

        private void AddRecipes()
        {
            btnCancel.Visible = true;
            AssignmentMainFields();

            if (txtName.ReadOnly == true)
            {
                chcVegetarian.Enabled = true;

                if (pbStar1.Visible == true)
                {
                    StarHide(pbStar1);
                    StarHide(pbStar2);
                    StarHide(pbStar3);
                }

                ContextMenuBlock();
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);
                Function.UnblockingFields(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.ClearFields(panelMain);

                Function.UncheckedCheckBox(panelRight);
                Function.UncheckedCheckBox(panelLeft);

                LabelClearTextInsertDash(lblLevel);
                LabelClearTextInsertDash(lblTime);
                LabelClearTextInsertDash(lblCuisine);

                pbLittlePhoto.Image = Resources.przepisy;
                linkForm2 = stringOfCharacters.ToString();
                clear = add;

                btnAddRest.Visible = true;
                btnModify.Visible = false;
                btnDelete.Visible = false;

                Color(panelMain, Function.CreateBrightColor());

                linkForm22 = linkForm2;
                p.AddRecipeForm2 = false;
                addRecipe = true;

            }
            else
            {
                numberLine = 0;

                DeleteEmptyLines();
                LineAlignment();

                if (!CheckNameAfterClick())
                {
                    if (check == false)
                    {
                        txtName.Text = txtName.Text.ToUpper();

                        if (txtName.Text != "")
                        {
                            AddStampBlock();

                            try
                            {
                                RecipesBase model = new RecipesBase();

                                model.RecipesName = txtName.Text.ToUpper();
                                model.AmountsMeal = rtxtAmountsOfFood.Text;
                                model.Grams = rTxtGrams.Text;
                                model.Ingredients = rTxtIngredients.Text;
                                model.ShortDescription = txtShortDescription.Text;
                                model.LongDescription = rtxtDescription.Text;

                                Model(model);

                                model.CategoryCuisines = lblCuisine.Text;
                                model.CategoryRating = idRatingForm2;
                                model.CategoryDifficultLevel = lblLevel.Text;
                                model.CategoryPreparationTime = lblTime.Text;

                                if (string.IsNullOrWhiteSpace(linkForm2))
                                {
                                    linkForm2 = stringOfCharacters.ToString();
                                }
                                else
                                {
                                    model.PhotoLinkLocation = linkForm2;
                                }

                                #region MealAdd
                                model.SnackMeal = IdMealForm2[0];
                                model.DinnerMeal = IdMealForm2[1];
                                model.SoupMeal = IdMealForm2[2];
                                model.DessertMeal = IdMealForm2[3];
                                model.DrinkMeal = IdMealForm2[4];
                                model.PreservesMeal = IdMealForm2[5];
                                model.SaladMeal = IdMealForm2[6];
                                #endregion
                                #region IngridientsAdd
                                model.IdFishIngredients = ingridientsForm2[0];
                                model.IdPastaIngredients = ingridientsForm2[1];
                                model.IdFruitsIngredients = ingridientsForm2[2];
                                model.IdMuschroomsIngredients = ingridientsForm2[3];
                                model.IdBirdIngredients = ingridientsForm2[4];
                                model.IdMeatIngredients = ingridientsForm2[5];
                                model.IdEggsIngredients = ingridientsForm2[6];
                                model.Vegetarian = ingridientsForm2[7];
                                #endregion

                                RecipesBase.add(model);

                                newForm = true;
                                chcVegetarian.Enabled = false;

                                if (string.IsNullOrEmpty(rtxtPortion.Text))
                                {
                                    rtxtPortion.Text = 1.ToString();
                                }

                                titleForm2 = txtName.Text;
                                idDgGridForm2 = model.Id;
                                RemoveExtraCharactersBlock();

                                MessageBox.Show("Dodano przedmiot");

                                btnAddRest.Visible = false;
                                btnClose.Text = "Zamknij";

                                Function.BlockingFields(panelMain);
                                Function.BlockCheckbox(panelLeft);
                                Function.BlockCheckbox(panelRight);
                                Function.ColorFieldsAfterBlocking(panelMain, rtxtPortion);

                                ShowStar(1, pbStar1, idRatingForm2);
                                ShowStar(2, pbStar2, idRatingForm2);
                                ShowStar(3, pbStar3, idRatingForm2);

                                btnCancel.Visible = false;
                                clear = "0";
                                Color(panelMain, Function.CreateColorBlockingFields());
                                p.AddRecipeForm2 = true;
                                addRecipe = false;
                                ButtonMy.ChangeForeColorToWhite(panelMain);
                            }
                            catch (Exception ex)
                            {
                                {
                                    MessageBox.Show(ex.Message);
                                }
                            }

                            btnModify.Visible = true;
                            btnDelete.Visible = true;
                        }
                    }
                    check = false;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddRecipes();
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (rtxtPortion.Text != "0")
            {
                por.CopyTextToList(rtxtPortion.Text);

                por.CompareBuforWithSelection();

                rtxtPortion.Lines = por.TextOutput();

                ConvertFunction();
            }
            else
            {
                rtxtPortion.Text = "";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UndoChanges();
        }
        public bool hideForm = false;
        private void btnAddRest_Click(object sender, EventArgs e)
        {

            if (!CheckNameAfterClick())
            {
                hideForm = true;
                if (btnAddRest.Text == addRest)
                {
                    clear = "modification";
                }

                Form3 OpenForm = new Form3();

                OpenForm.hideForm3 = hideForm;
                OpenForm.correctModyficationName3 = correctModyficationName;
                OpenForm.clearForm3 = clear;
                OpenForm.idDgGridForm3 = idDgGridForm2;
                OpenForm.titleForm3 = txtName.Text;
                OpenForm.ingredientForm3 = rTxtIngredients.Text;
                OpenForm.gramsForm3 = rTxtGrams.Text;

                if (rtxtAmountsOfFood.Text != "")
                {
                    OpenForm.AmountsOfFoodForm3 = rtxtAmountsOfFood.Text;
                }

                OpenForm.shortDescriptionForm3 = txtShortDescription.Text;
                OpenForm.InstructionForm3 = rtxtDescription.Text;

                if (string.IsNullOrWhiteSpace(rtxtPortion.Text))
                {
                    OpenForm.numberOfPortionsForm3 = 1;
                }
                else
                {
                    OpenForm.numberOfPortionsForm3 = int.Parse(rtxtPortion.Text);
                }

                OpenForm.RatingForm3 = idRatingForm2;
                OpenForm.difficultLevelForm3 = difficultLevelForm2;
                OpenForm.executionTimeForm3 = executionTimeForm2;
                OpenForm.photoForm3 = linkForm2;
                OpenForm.listOfCuisinesForm3 = listOfCuisinesForm2;
                OpenForm.LinkForm23 = linkForm22;
                OpenForm.addRecipeForm3 = p.AddRecipeForm2;
                OpenForm.addRecipe = addRecipe;

                #region MealAdd

                for (int i = 0; i < IdMealForm2.Length; i++)
                {
                    OpenForm.IdMealForm3[i] = IdMealForm2[i];
                }

                #endregion

                #region ComponentAdd

                for (int i = 0; i < ingridientsForm2.Length; i++)
                {
                    OpenForm.idComponentsForm3[i] = ingridientsForm2[i];
                }

                #endregion

                OpenForm.counterForm3 = counterForm2;

                #region Pamięć
                OpenForm.title3 = title1;
                OpenForm.amounts3 = amounts;
                OpenForm.ingrediet3 = ingrediet;
                OpenForm.shortDes3 = shortDes;
                OpenForm.longDes3 = longDes;
                OpenForm.cuisines3 = cuisines;
                OpenForm.level3 = level;
                OpenForm.time3 = time;
                OpenForm.rating3 = rating;
                OpenForm.portions3 = portions;
                OpenForm.cancel3 = cancel;
                #endregion

                OpenForm.newForm3 = newForm;
                this.Visible = false;
                OpenForm.ShowDialog();
                this.Hide();
            }
        }


        #endregion Button

        //zmienne pamięciowe- Anuluj//
        public string title1, amounts, grams, ingrediet, shortDes, longDes, cuisines, level, time, rating;
        public int portions;
        public int[] NewingridientsForm2 = new int[8];
        bool[] checkBoxesCancel = new bool[15];
        string photo;

        //Przypisz zmienne główych pól
        private void AssignmentMainFields()
        {
            cancel = true;
            title1 = txtName.Text;
            amounts = rtxtAmountsOfFood.Text;
            grams = rTxtGrams.Text;
            ingrediet = rTxtIngredients.Text;
            shortDes = txtShortDescription.Text;
            longDes = rtxtDescription.Text;
            if (string.IsNullOrWhiteSpace(rtxtPortion.Text)) portions = 1;
            else portions = int.Parse(rtxtPortion.Text);
            cuisines = lblCuisine.Text;
            level = lblLevel.Text;
            time = lblTime.Text;
            rating = idRatingForm2;

            checkBoxesCancel[0] = chcFish.Checked;
            checkBoxesCancel[1] = chcPasta.Checked;
            checkBoxesCancel[2] = chcFruits.Checked;
            checkBoxesCancel[3] = chcMuschrooms.Checked;
            checkBoxesCancel[4] = chcBird.Checked;
            checkBoxesCancel[5] = chcMeat.Checked;
            checkBoxesCancel[6] = chcEggs.Checked;
            checkBoxesCancel[7] = chcVegetarian.Checked;

            checkBoxesCancel[8] = chcSnack.Checked;
            checkBoxesCancel[9] = chcDinner.Checked;
            checkBoxesCancel[10] = chcSoup.Checked;
            checkBoxesCancel[11] = chcDessert.Checked;
            checkBoxesCancel[12] = chcDrink.Checked;
            checkBoxesCancel[13] = chcPreserves.Checked;
            checkBoxesCancel[14] = chcSalad.Checked;
            photo = linkForm2;

        }

        #region Print
        private StringReader sr = null;
        public string im { get; internal set; }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            Font czcionka2 = new Font("Courier New", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            int wysokoscWiersza2 = (int)czcionka2.GetHeight(e.Graphics);
            int iloscLinii2 = e.MarginBounds.Height / wysokoscWiersza2;

            if (sr == null)
            {
                string textName = "";
                string textAll = "";
                string textDescription = "";

                foreach (string wiersz in table)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;

                    if (szerokosc < e.MarginBounds.Width)
                    {
                        textAll += wiersz + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / wiersz.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = wiersz;
                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            textAll += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);

                        textAll += skracanyWiersz + "\n";
                    }
                }
                foreach (string wiersz in txtName.Lines)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;

                    if (szerokosc < e.MarginBounds.Width)
                    {
                        textName += wiersz + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / wiersz.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = wiersz;

                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            textName += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);

                        textName += skracanyWiersz + "\n";
                    }
                }

                foreach (string wiersz in rtxtDescription.Lines)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;

                    if (szerokosc < e.MarginBounds.Width)
                    {
                        textDescription += wiersz + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / wiersz.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = wiersz;
                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            textDescription += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);

                        textDescription += skracanyWiersz + "\n";
                    }
                }
                sr = new StringReader(textName + "\n" + textAll + "\n" + textDescription);
            }
            e.HasMorePages = true;

            for (int i = 0; i < iloscLinii2; i++)
            {
                string wiersz = sr.ReadLine();

                if (wiersz == null)
                {
                    e.HasMorePages = false;
                    sr = null;
                    break;
                }
                e.Graphics.DrawString(wiersz,
                             czcionka2,
                             Brushes.Black,
                             e.MarginBounds.Left,
                             e.MarginBounds.Top + i * wysokoscWiersza2);
            }
        }

        private void drukujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument1.DocumentName = "Form2";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            printDocument1.Print();
        }

        string[] table;
        private void podglToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int number = rtxtAmountsOfFood.Lines.Length;
            int number2 = rTxtGrams.Lines.Length;
            int number3 = rTxtIngredients.Lines.Length;
            int numberAll = number + number2 + number3;
            table = new string[numberAll];

            int k = 0;

            string space = " ";
            string space2 = " ";

            for (int i = 0; i < rTxtIngredients.Lines.Length * 3; i = i + 3)
            {
                if (rtxtAmountsOfFood.Lines[k] == string.Empty)
                {
                    table[i] = space;
                }
                else
                {
                    table[i] = rtxtAmountsOfFood.Lines[k] + space + rTxtGrams.Lines[k] + space2 + rTxtIngredients.Lines[k];
                }
                k++;
            }

            printPreviewDialog1.ShowDialog();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();

            this.Hide();
            RememberIfCheckBoxChecked(form1);
            form1.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font czcionka2 = new Font("Courier New", 11.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
            int wysokoscWiersza2 = (int)czcionka2.GetHeight(e.Graphics);
            int iloscLinii2 = e.MarginBounds.Height / wysokoscWiersza2;

            if (sr == null)
            {
                string tekstName = "";
                string tekstAll = "";

                foreach (string wiersz in table)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;

                    if (szerokosc < e.MarginBounds.Width)
                    {
                        tekstAll += wiersz + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / wiersz.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = wiersz;
                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            tekstAll += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);

                        tekstAll += skracanyWiersz + "\n";
                    }
                }
                foreach (string wiersz in txtName.Lines)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;

                    if (szerokosc < e.MarginBounds.Width)
                    {
                        tekstName += wiersz + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / wiersz.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = wiersz;
                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            tekstName += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);

                        tekstName += skracanyWiersz + "\n";
                    }
                }

                sr = new StringReader(tekstName + "\n" + tekstAll + "\n");
            }
            e.HasMorePages = true;

            for (int i = 0; i < iloscLinii2; i++)
            {
                string wiersz = sr.ReadLine();

                if (wiersz == null)
                {
                    e.HasMorePages = false;
                    sr = null;
                    break;
                }
                e.Graphics.DrawString(wiersz,
                             czcionka2,
                             Brushes.Black,
                             e.MarginBounds.Left,
                             e.MarginBounds.Top + i * wysokoscWiersza2);
            }
        }

        private void listaZakupowaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                printDocument2.DocumentName = "Form2";
                backgroundWorker1.RunWorkerAsync();
            }
        }

        #endregion Print

        #region TextField
        RichTextBoxMy p = new RichTextBoxMy();
        RichTextBoxMy gram = new RichTextBoxMy(15);
        RichTextBoxMy ingr = new RichTextBoxMy();


        bool txtShortDescriptionBool = false;
        private void txtShortDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtShortDescription.ReadOnly == false)
            {
                HighlightingEditFieldInOtherCases(e, txtShortDescription, txtShortDescriptionBool);

                if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                {
                    UndoChanges();
                }
            }
        }

        private void txtShortDescription_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                DisplayForm(txtShortDescription);

                DataIsFailed();

                CheckNameAfterClick();
            }
        }

        private void txtShortDescription_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtShortDescription, "Streszczenie przepisu");
        }


        private void rtxtDescription_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                DisplayForm(rtxtDescription);

                DataIsFailed();

                CheckNameAfterClick();
            }
        }

        bool rememberEnter = false;
        private void rtxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (rtxtDescription.ReadOnly == false)
            {
                HighlightingEditFieldInOtherCases(e, rtxtDescription, rtxDescriptionBool);

                if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                {
                    UndoChanges();
                }


                if (e.KeyCode == Keys.Enter && EnterOffBoolDescription)
                {

                    if (numberLineDescription >= maxLineDescription)
                    {
                        Punktor(e);
                    }
                    else
                    {
                        Punktor(e, rtxtDescription);
                    }
                }

                if (EnterOffBoolDescription)
                {
                    if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Space)
                    {
                        // miało działac prawidlowe usuwanie ale przy takim kodzie są błędy- jezeli space jest na koncu
                        // EnterOffBoolDescription = false;  
                        rememberEnter = true;
                    }

                }
                if (rememberEnter && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Space)
                {
                    EnterOffBoolDescription = true;
                    rememberEnter = false;
                }
            }
        }

        private void rtxtDescription_SelectionChanged(object sender, EventArgs e)
        {
            IndexCharDescription(rtxtDescription);
        }


        bool txtNameBool = false;
        private void txtName_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                DisplayForm(txtName);

                DataIsFailed();

                txtNameBool = true;

                SetFocusToTheEndOfTheName(txtName);
            }
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                HighlightingEditFieldInOtherCases(e, txtName, txtNameBool);
                if (e.KeyCode == Keys.Enter)
                {
                    txtName.Text = txtName.Text.ToUpper();

                    if (Function.CheckName(txtName, correctModyficationName))
                    {
                        {
                            DisplayForm(txtName);
                            txtName.Focus();
                        }
                    }
                    else
                    {
                        rtxtPortion.Focus();
                        DisplayForm(rtxtPortion);
                    }

                }
                if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                {
                    UndoChanges();
                }
            }
        }


        private int changeSelectionStart;
        bool rtxtAmountsOfFoodBool = false;
        int rtxtAmountsSelectionStartTemp = 0;
        private void rtxtAmountsOfFood_KeyDown(object sender, KeyEventArgs e)
        {

            if (rtxtAmountsOfFood.ReadOnly == false)
            {

                changeLine = false;
                HighlightingEditFieldInOtherCases(e, rtxtAmountsOfFood, rtxtAmountsOfFoodBool);

                numberLineZero = p.NumberLine;
                changeSelectionStart = p.StartSelection;

                if (rtxtAmountsOfFood.SelectionLength > 0)
                {
                    if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                    {
                        int selectionStart = rtxtAmountsOfFood.SelectionStart;

                        rtxtAmountsOfFood.Text = rtxtAmountsOfFood.Text.Remove(selectionStart, rtxtAmountsOfFood.SelectedText.Length);

                        p.CopyTextToList(rtxtAmountsOfFood.Text);

                        rtxtAmountsOfFood.SelectionStart = selectionStart;
                    }
                }

                if (e.KeyCode == Keys.Enter && p.EnterOn == false)
                {

                    int proba = p.StartSelection;
                    rTxtGrams.Focus();
                    int selectionStart = rtxtAmountsOfFood.SelectionStart;

                    rtxtAmountsOfFood.Lines = p.TextOutput();
                    rtxtAmountsOfFood.SelectionStart = selectionStart;
                    AmountsAndGramsKeyDown(e, rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);
                }
            }
        }

        private void rtxtAmountsOfFood_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == false)
            {
                p.CharacterInput(e.KeyChar);

                if (e.KeyChar == 13)
                {
                    if (p.Deleted)
                    {
                        rtxtAmountsSelectionStartTemp = rtxtAmountsOfFood.SelectionStart;
                    }
                    int selectionStart = p.StartSelection;

                    rtxtAmountsOfFood.Lines = p.TextOutput();
                    rtxtAmountsOfFood.SelectionStart = selectionStart;
                }
            }

        }

        string getLine = string.Empty;
        private void rtxtAmountsOfFood_KeyUp(object sender, KeyEventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == false)
            {
                int selectionStart = rtxtAmountsOfFood.SelectionStart;
                int selectionStartMy = p.StartSelection;
                p.TextboxText = rtxtAmountsOfFood.Text;

                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    CheckIfLineDoesNotContainSlash();
                    ClearError(rtxtAmountsOfFood);
                }



                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    p.CopyTextToList(p.TextboxText);

                    p.CompareBuforWithSelection();

                    if (rtxtAmountsOfFood.SelectionStart >= 0)
                    {
                        p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                    }


                    rtxtAmountsOfFood.Lines = p.TextOutput();

                    rtxtAmountsOfFood.Focus();

                    rtxtAmountsOfFood.SelectionStart = selectionStart;

                    getLine = p.TextOutput(p.NumberLine);
                }
                else if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
                {
                    rtxtAmountsOfFood.Lines = p.TextOutput();

                    if (p.EnterOn && e.KeyCode == Keys.Enter)
                    {
                        rtxtAmountsOfFood.SelectionStart = selectionStartMy;
                        p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                    }
                    else
                    {
                        rtxtAmountsOfFood.SelectionStart = selectionStart;
                        if (p.bufor.Count < rTxtGrams.Lines.Length)
                            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);

                    }
                }
            }
        }

        private void rtxtAmountsOfFood_SelectionChanged(object sender, EventArgs e)
        {
            if (p.ChangeStartSelectionIfLineIsFull == false)
            {
                p.StartSelection = rtxtAmountsOfFood.SelectionStart;
                p.TextboxText = rtxtAmountsOfFood.Text;
                IndexChar(rtxtAmountsOfFood);
            }
            else
            {
                rtxtAmountsOfFood.SelectionStart = p.StartSelection;
                p.ChangeStartSelectionIfLineIsFull = false;
            }
        }

        private void rtxtAmountsOfFood_Click(object sender, EventArgs e)
        {
            int selectionStart = rtxtAmountsOfFood.SelectionStart;
            rtxAmountsClick = true;

            if (txtName.ReadOnly == false)
            {
                IndexChar(rtxtAmountsOfFood);


                if (!p.Deleted)
                {
                    rtxtAmountsOfFood.Lines = p.TextOutput();

                    DisplayForm(rtxtAmountsOfFood);

                    CheckIfLineDoesNotContainSlash();
                    ClearError(rTxtGrams);

                    CheckNameAfterClick();
                    p.NumberOfCharactersToFocus(rtxtAmountsOfFood.Text);

                    if (!p.Deleted)
                    {
                        rtxtAmountsOfFood.SelectionStart = selectionStart;
                    }
                    p.Deleted = false;
                }
                counter = 0;
            }
        }

        private void rtxtAmountsOfFood_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(rtxtAmountsOfFood, "Ilości składników - tylko i wyłącznie Liczby można wpisywać");
        }


        RichTextBoxMy por = new RichTextBoxMy(2);
        private void rtxtPortion_KeyDown(object sender, KeyEventArgs e)
        {
            if (rtxtPortion.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                {
                    UndoChanges();
                }

                if (e.KeyCode == Keys.Enter && txtName.ReadOnly == true && rtxtPortion.ReadOnly == false)
                {
                    if (rtxtPortion.Text == "0")
                    {

                        if (!string.IsNullOrWhiteSpace(rtxtPortion.Text))
                        {
                            ConvertFunction();
                        }
                    }

                }
                else if (e.KeyCode == Keys.Enter && txtName.ReadOnly == false)
                {
                    numberLine = 0;

                    if (blockFunc == false)
                    {
                        rtxtAmountsOfFood.Focus();
                        DisplayForm(rtxtAmountsOfFood);
                        SetFocusToTheEndOfTheName(rtxtAmountsOfFood);
                        numberLine = NumberOfLinesInColumn(rtxtAmountsOfFood.Text);
                    }
                    else
                    {
                        blockFunc = false;
                    }
                }
            }
        }

        private void rtxtPortion_KeyPress(object sender, KeyPressEventArgs e)
        {
            por.CharacterInput(e.KeyChar);
        }

        private void rtxtPortion_KeyUp(object sender, KeyEventArgs e)
        {
            if (rtxtPortion.ReadOnly == false)
            {
                int selectionStart = rtxtPortion.SelectionStart;

                if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
                {
                    rtxtPortion.Lines = por.TextOutput();

                    if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                    {
                        por.CopyTextToList(por.TextboxText);

                        por.CompareBuforWithSelection();

                        rtxtPortion.Lines = por.TextOutput();

                        rtxtPortion.SelectionStart = selectionStart;
                    }
                    else
                    {
                        rtxtPortion.SelectionStart = selectionStart;
                    }
                }
            }
        }

        private void rtxtPortion_SelectionChanged(object sender, EventArgs e)
        {
            por.StartSelection = rtxtPortion.SelectionStart;
            por.TextboxText = rtxtPortion.Text;
            IndexChar(rtxtPortion);
        }

        private void rtxtPortion_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                DisplayForm(rtxtPortion);
                DataIsFailed();
                CheckNameAfterClick();
            }
        }

        public void CopyTextToTable(string text, string[] table)
        {

            try
            {
                Array.Clear(table, 0, table.Length);

                string copyText = string.Empty;
                int licznik = 0;

                foreach (var item in text)
                {
                    if (item != '\n')
                    {
                        copyText += item;
                        table[licznik] = copyText;
                    }
                    else
                    {
                        table[licznik] = copyText;
                        copyText = string.Empty;
                        licznik++;
                    }
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        bool rtxGramsBool = false;

        string[] tab = new string[0];
        string newWord = string.Empty;
        int counter = 0;
        bool polishLetter = false;
        private void rTxtGrams_KeyUp(object sender, KeyEventArgs e)
        {

            if (rTxtGrams.Lines.Length > tab.Length)
            {
                Array.Resize(ref tab, maxLine + 1);
            }
            if (numberLine < maxLine && e.KeyCode == Keys.Enter)
            {
                CopyTextToTable(rTxtGrams.Text, tab);
            }


            //zakreślacz w gramach
            try
            {
                if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
                {
                    int start;
                    start = rTxtGrams.SelectionStart;

                    if (e.KeyValue >= 65 && e.KeyValue <= 122 || e.KeyValue == 322 || e.KeyValue == 380 || e.KeyValue == 378 || e.KeyValue == 261 || e.KeyValue == 263 || e.KeyValue == 243 || e.KeyValue == 347 || e.KeyValue == 324 || e.KeyValue == 281)
                    {
                        string temp = string.Empty;
                        temp += e.KeyData;

                        if (temp == "L, Control, Alt")
                        {
                            temp = "ł";
                            polishLetter = true;
                        }
                        else if (temp == "Z, Control, Alt")
                        {
                            temp = "ż";
                            polishLetter = true;
                        }
                        else if (temp == "Z, Control, Alt")
                        {
                            temp = "ź";
                            polishLetter = true;
                        }
                        else if (temp == "A, Control, Alt")
                        {
                            temp = "ą";
                            polishLetter = true;
                        }
                        else if (temp == "C, Control, Alt")
                        {
                            temp = "ć";
                            polishLetter = true;
                        }
                        else if (temp == "O, Control, Alt")
                        {
                            temp = "ó";
                            polishLetter = true;
                        }
                        else if (temp == "S, Control, Alt")
                        {
                            temp = "ś";
                            polishLetter = true;
                        }
                        else if (temp == "N, Control, Alt")
                        {
                            temp = "ń";
                            polishLetter = true;
                        }
                        else if (temp == "E, Control, Alt")
                        {
                            temp = "ę";
                            polishLetter = true;
                        }

                        if (polishLetter)
                        {
                            newWord += temp;
                            polishLetter = false;
                        }
                        else
                        {
                            newWord += e.KeyData;
                        }

                        newWord = newWord.ToLower();
                        temp = string.Empty;

                        var linqQuery = from slowo in sortedUnits
                                        where slowo.StartsWith(newWord)
                                        select slowo;

                        foreach (var word in linqQuery)
                        {
                            tab[numberLine] = string.Empty;

                            for (int i = 0; i < word.Length; i++)
                            {
                                tab[numberLine] += word[i];
                            }

                            int tempNum = numberLine;
                            rTxtGrams.Lines = tab;
                            numberLine = tempNum;

                            if (numberLine == 0)
                            {
                                rTxtGrams.SelectionStart = start;
                                rTxtGrams.SelectionLength = word.Length - start;
                            }
                            else
                            {
                                rTxtGrams.SelectionStart = start;
                                rTxtGrams.SelectionLength = start + word.Length - start - counter;
                                counter++;
                            }

                            break;
                        }
                    }

                }
                else if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    counter = 0;

                    if (newWord.Length > 1)
                    {
                        newWord = rTxtGrams.Lines[numberLine];
                    }
                    else
                    {
                        newWord = string.Empty;
                    }
                    CopyTextToTable(rTxtGrams.Text, tab);



                }

                if (p.Deleted)
                {
                    CheckIfDataIsCorrect();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

           
        }


        private void rTxtGrams_KeyDown(object sender, KeyEventArgs e)
        {
            if (rTxtGrams.ReadOnly == false)
            {
                IndexChar(rTxtGrams);
                changeLine = false;
                HighlightingEditFieldInOtherCases(e, rTxtGrams, rtxGramsBool);

                if (e.KeyCode == Keys.Enter)
                {
                    rTxtIngredients.Focus();

                    AmountsAndGramsKeyDown(e, rTxtGrams, rTxtIngredients, rtxtAmountsOfFood);
                }


                if (rTxtGrams.Lines.Length > 0)
                {
                    if (rTxtGrams.SelectionLength - counter >= rTxtGrams.Lines[numberLine].Length && rTxtGrams.Lines[numberLine].Length != 0)
                    {
                        newWord = string.Empty;
                    }

                }

            }
        }

        private void rTxtGrams_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                IndexChar(rTxtGrams);

                DisplayForm(rTxtGrams);
                CheckIfLineDoesNotContainSlash();
                ClearError(rTxtGrams);

                CheckNameAfterClick();
                counter = 0;

            }
        }

        private void rTxtGrams_SelectionChanged(object sender, EventArgs e)
        {

            IndexChar(rTxtGrams);

        }



        bool rTxtIngredientsBool = false;
        private void rTxtIngredients_KeyDown(object sender, KeyEventArgs e)
        {
            if (rTxtIngredients.ReadOnly == false)
            {
                HighlightingEditFieldInOtherCases(e, rTxtIngredients, rTxtIngredientsBool);

                if (e.KeyCode == Keys.Enter)
                {
                    NumberOfLines(e, rTxtIngredients);

                    MaxLineIncrease();

                    if (p.EnterOn)
                    {
                        p.ClassicEnterPlusNewLine(rTxtIngredients, rtxtAmountsOfFood, rTxtGrams, numberLine);
                    }
                    else
                    {
                        ChangeAddLine(e);

                        DisplayForm(rtxtAmountsOfFood);
                    }
                    //Autouzupełnianie rtxtgrams
                    newWord = string.Empty;
                    counter = 0;
                    rTxtGrams.SelectionLength = 0;
                }
                else if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                {
                    UndoChanges();
                }
            }
        }

        private void rTxtIngredients_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {

                IndexChar(rTxtIngredients);


                DisplayForm(rTxtIngredients);
                CheckIfLineDoesNotContainSlash();
                ClearError(rTxtIngredients);
                CheckNameAfterClick();

                if (numberLine - 1 == numberLineTemp)
                {
                    numberLine = numberLineTemp;
                    p.NumberLine = numberLineTemp;
                }

                counter = 0;
            }
        }

        int numberLineTemp;
        private void rTxtIngredients_SelectionChanged(object sender, EventArgs e)
        {
            ingr.StartSelection = rTxtIngredients.SelectionStart;
            ingr.TextboxText = rTxtIngredients.Text;
            IndexChar(rTxtIngredients);
            numberLineTemp = numberLine;
        }
        #endregion TextField

        #region CheckboxMeal
        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcSnack, 1, IdMealForm2);
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcDinner, 2, IdMealForm2);
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcSoup, 3, IdMealForm2);
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcDessert, 4, IdMealForm2);
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcDrink, 5, IdMealForm2);
        }


        string ShortenedStringbyThLastWord;
        string wordMovedToNextLine;
        bool zero = false;
        string signLength = "  ";
        private void rtxtDescription_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (numberLineDescription <= maxLineDescription)
                {
                    numberLineDescription = NumberOfCharactersToFocus(rtxtDescription.Text);
                }


                if (e.KeyChar != 8)
                {

                    if (LineWidth(e) > rtxtDescription.Width - 35 && e.KeyChar != 13)
                    {

                        AddLine();

                        if (numberLineDescription >= maxLineDescription)
                        {

                            if (!rtxtDescription.Lines[numberLineDescription].EndsWith(" \n"))
                            {
                                if (e.KeyChar != 32)
                                {
                                    ShortenedStringbyThLastWord = DeletesLastWordInLine(rtxtDescription.Lines[numberLineDescription]);
                                    wordMovedToNextLine = CharacterCounting(rtxtDescription.Lines[numberLineDescription]);

                                    RichTextBoxMy.NewLine(rtxtDescription);
                                    rtxtDescription.Text = rtxtDescription.Text.Replace(rtxtDescription.Lines[numberLineDescription], ShortenedStringbyThLastWord);
                                }
                                else
                                {
                                    if (EnterOffBoolDescription)
                                    {
                                        rtxtDescription.Text = rtxtDescription.Text.Insert(rtxtDescription.SelectionStart, "\n" + window + window + signLength);
                                    }
                                    else
                                    {
                                        RichTextBoxMy.NewLine(rtxtDescription);
                                        //trzeba bedzie cofnąć ustawienie focus o 1 pozycje(spacje którą dodaje - nacisniecie)
                                    }


                                    zero = true;
                                }

                                if (!EnterOffBoolDescription)
                                {
                                    rtxtDescription.Text += wordMovedToNextLine;
                                }
                                else
                                {
                                    rtxtDescription.Text += window + window + signLength + wordMovedToNextLine;
                                }
                            }
                            else
                            {
                                rtxtDescription.Text += window;
                            }

                            rtxtDescription.SelectionStart = rtxtDescription.TextLength;
                        }
                        else
                        {
                            wordMovedToNextLine = CharacterCounting(rtxtDescription.Lines[numberLineDescription]);

                            if (!rtxtDescription.Lines[numberLineDescription].EndsWith(" \n"))
                            {
                                ShortenedStringbyThLastWord = DeletesLastWordInLine(rtxtDescription.Lines[numberLineDescription]);


                                int start = rtxtDescription.SelectionStart;

                                rtxtDescription.Text = rtxtDescription.Text.Replace(rtxtDescription.Lines[numberLineDescription], ShortenedStringbyThLastWord);

                                rtxtDescription.SelectionStart = start - wordMovedToNextLine.Length;
                                start = rtxtDescription.SelectionStart;

                                if (!EnterOffBoolDescription)
                                {
                                    rtxtDescription.Text = rtxtDescription.Text.Insert(start, "\n" + wordMovedToNextLine);

                                    rtxtDescription.SelectionStart = start + wordMovedToNextLine.Length + 1;
                                }
                                else
                                {
                                    rtxtDescription.Text = rtxtDescription.Text.Insert(start, "\n" + window + window + signLength + wordMovedToNextLine);
                                    rtxtDescription.SelectionStart = start + additionalNumberOfCharacters + wordMovedToNextLine.Length + 1;
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }



        private void rtxtDescription_KeyUp(object sender, KeyEventArgs e)
        {

            if (zero)
            {
                rtxtDescription.SelectionStart = rtxtDescription.SelectionStart - 1;
                zero = false;
            }

        }

        private void rTxtGrams_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (rTxtGrams.Lines.Length > 0)
                {
                    if (rTxtGrams.SelectionLength - counter >= rTxtGrams.Lines[numberLine].Length)
                    {
                        // fillAutoComplete = true;
                        //   newWord = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContextEnter(CMAmountsEnter, CMGramsEnter, CMIngridientsEnter);
            if (btnEnter.Text == "OFF")
            {
                btnEnter.Text = "ON";
                btnEnter.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                btnEnter.Text = "OFF";
                btnEnter.ForeColor = System.Drawing.Color.Red;
            }
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcPreserves, 6, IdMealForm2);
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcSalad, 7, IdMealForm2);
        }

        private void chcVegetarian_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcVegetarian, 8, ingridientsForm2);
            if (chcVegetarian.Checked)
            {
                chcBird.Checked = false;
                chcMeat.Checked = false;
                chcFish.Checked = false;
                chcVegetarian.Checked = true;
            }
        }
        #endregion CheckboxMeal

        #region CheckBoxComponent

        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcFish, 1, ingridientsForm2);
            chcVegetarian.Checked = false;
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcPasta, 2, ingridientsForm2);
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcFruits, 3, ingridientsForm2);
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcMuschrooms, 4, ingridientsForm2);
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcMeat, 6, ingridientsForm2);
            chcVegetarian.Checked = false;
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcBird, 5, ingridientsForm2);
            chcVegetarian.Checked = false;
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcEggs, 7, ingridientsForm2);
        }
        #endregion CheckBoxComponent
    }
}
