using CulinaryRecipes.Models;
using CulinaryRecipes.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CulinaryRecipes
{
    public partial class Form2 : Form
    {
        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public int idDgGridForm2;
        public int numberOfPortionsForm2;
        public int counterForm2;
        private int maxLine = 0;
        public int[] checkBoxDish = new int[7];
        public int[] checkBoxIngredients = new int[8];
        public string titleForm2;
        public string amountsOfIngredientsForm2;
        public string gramsForm2;
        public string ingredientForm2;
        public string ShortDescriptionForm2;
        public string instructionForm2;
        public string listOfCuisinesForm2;
        public const string dash = "-";
        public string idRatingForm2 = "-";
        public string difficultLevelForm2;
        public string executionTimeForm2;
        public string linkForm2 = "-";
        public string unlockFieldsForm2;
        public string clear = "0";
        string addRest = "Zmień", add = "add";
        public bool cancel = false;
        public bool[] checkBoxesCancelForm2Ing = new bool[8];
        public bool[] checkBoxesCancelForm2Meal = new bool[7];
        public bool seekUnsubscribeForm2;
        public bool addRecipe;
        public bool newForm = false;
        int numberLine = 0;
        public List<CheckBox> SavedCheckBoxForm2 = new List<CheckBox>();
        BulletCharacter point;
        RichTextBoxMy p;
        AutoCompleter complete;
        string enterOn = "ENTER ON";
        string enterOff = "ENTER OFF";
        public string linkForm22;
        public string correctModyficationName;

        //sprawdza czy wpisany znak jest cyfrą, pomijając puste pola. Jezeli jest błąd zmienna check przyjmuje wartość true;
        bool isItNumber = false;

        public Form2()
        {
            InitializeComponent();
            point = new BulletCharacter(rtxtDescription);
            complete = new AutoCompleter(rTxtGrams, rtxtAmountsOfFood);
            p = new RichTextBoxMy(rtxtAmountsOfFood);
            rtxtDescription.SetInnerMargins(27, 15, 24, 0);
            rtxtAmountsOfFood.SetInnerMargins(3, 3, 0, 0);
            rTxtGrams.SetInnerMargins(3, 3, 0, 0);
            rTxtIngredients.SetInnerMargins(3, 3, 0, 0);
            rtxtShortDescription.SetInnerMargins(5, 3, 3, 0);
            rtxtPortion.SetInnerMargins(4, 2, 0, 0);
        }
        private const string anuluj = "Anuluj";
        private void Form2_Load(object sender, EventArgs e)
        {
            //SetWindowPos(this.Handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);  // program jest zawrze na wierzchu

            if (titleForm2 == null)
            {
                btnAdd.Visible = true;
                InTheProcessOfAdding = true;
                newForm = true;
                ContextMenuBlock();
                ChangeEnterNameInMeunuStrip();

                Function.UnblockingFields(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);

                btnClose.Text = anuluj;

                btnDelete.Visible = false;
                btnModify.Visible = false;
                btnAddRest.Visible = true;
                lblTime.Visible = true;

                clear = add;
                p.AddRecipeForm2 = true;

                chcVegetarian.Enabled = true;
                this.CancelButton = btnClose;
                HighlightEditField(txtName, brightColorForComparison);
            }
            else if (unlockFieldsForm2 == "1" && txtName.ReadOnly == true)
            {
                correctModyficationName = titleForm2;

                ContextMenuBlock();
                ChangeEnterNameInMeunuStrip();

                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockingFields(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);

                Function.IsCheckBoxChecked(panelRight, checkBoxDish);
                Function.IsCheckBoxChecked(panelLeft, checkBoxIngredients);

                txtName.Text = titleForm2;
                Function.UncheckText(txtName);
                rtxtPortion.Text = numberOfPortionsForm2.ToString();
                rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                rTxtGrams.Text = gramsForm2;
                rTxtIngredients.Rtf = ingredientForm2;
                rtxtShortDescription.Rtf = ShortDescriptionForm2;
                rtxtDescription.Rtf = instructionForm2;

                lblCuisine.Text = listOfCuisinesForm2;
                lblLevel.Text = difficultLevelForm2;
                lblTime.Text = executionTimeForm2;

                Function.ShowStar(1, pbStar1, idRatingForm2);
                Function.ShowStar(2, pbStar2, idRatingForm2);
                Function.ShowStar(3, pbStar3, idRatingForm2);

                if (string.IsNullOrWhiteSpace(linkForm2)) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = linkForm2;

                chcVegetarian.Enabled = true;
                if (checkBoxIngredients[7] == 1) chcVegetarian.Checked = true;

                if (clear == add)
                {
                    btnAddRest.Text = addRest;
                    btnAddRest.Visible = true;
                    btnAdd.Visible = true;
                    btnClose.Text = anuluj;
                    btnModify.Visible = false;
                    btnDelete.Visible = false;
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
            }
            else
            {
                correctModyficationName = titleForm2;
                ChangeEnterNameInMeunuStrip();

                Function.IsCheckBoxChecked(panelRight, checkBoxDish);
                Function.IsCheckBoxChecked(panelLeft, checkBoxIngredients);

                txtName.Text = titleForm2;
                rtxtPortion.Text = numberOfPortionsForm2.ToString();

                rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                rTxtGrams.Text = gramsForm2;
                rTxtIngredients.Rtf = ingredientForm2;

                rtxtShortDescription.Text = ShortDescriptionForm2;
                rtxtDescription.Rtf = instructionForm2;

                if (string.IsNullOrWhiteSpace(linkForm2)) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = linkForm2;

                lblCuisine.Text = listOfCuisinesForm2;
                lblLevel.Text = difficultLevelForm2;
                lblTime.Text = executionTimeForm2;
                linkForm22 = linkForm2;

                Function.ShowStar(1, pbStar1, idRatingForm2);
                Function.ShowStar(2, pbStar2, idRatingForm2);
                Function.ShowStar(3, pbStar3, idRatingForm2);

                if (checkBoxIngredients[7] == 1) chcVegetarian.Checked = true;
                else chcVegetarian.Checked = false;
            }

            Function.UncheckText(txtName);
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isClosing && !InTheProcessOfAdding)
            {
                this.Visible = false;
                Function.PokazForm("Form1");
            }
        }

        //Funkcja pamięciowa (chceckboxy) będą zaznaczone jak zamknie się formę 2 a otworzy 1
        public void RememberIfCheckBoxChecked(Form1 name)
        {
            name.SavedCheckBox = SavedCheckBoxForm2;
        }

        //Zmienia Text EnterOn na EnterOff
        private void ChangeEnterNameInMeunuStrip()
        {
            CMAmountsEnter.Text = enterOff;
            CMGramsEnter.Text = enterOff;
            CMIngridientsEnter.Text = enterOff;
        }

        /// <summary>
        /// Maximum number of lines
        /// </summary>
        /// <param name="e"></param>
        /// <param name="nameItem"></param>
        private bool NumberOfLines(KeyEventArgs e, RichTextBox nameItem)
        {
            int start = nameItem.SelectionStart;
            bool limit = false;
            if (e.KeyCode == Keys.Enter && nameItem.Lines.Length > RichTextBoxMy.MaxNumberOfLines)
            {
                MessageBox.Show("Program nie może mieć więcej już linii");
                string[] tempTable = nameItem.Lines;
                tempTable[RichTextBoxMy.MaxNumberOfLines] = null;
                nameItem.Lines = tempTable;
                e.Handled = true;
                nameItem.SelectionStart = start;
                maxLine--;
                limit = true;
            }
            return limit;
        }

        /// <summary>
        /// Checks the number of lines after pasting
        /// </summary>
        /// <param name="elementName"></param>
        private void NumberLinesAfterPasted(RichTextBox elementName)
        {
            string precautionary = elementName.Text;
            elementName.Paste();

            if (numberLine > RichTextBoxMy.MaxNumberOfLines)
            {
                elementName.Text = precautionary;
                MessageBox.Show("Program może posiadać tylko " + RichTextBoxMy.MaxNumberOfLines + " linii.\n Przekroczyłąś/eś rozmiar wklejając tak długi tekst.");
            }

            int tempNum = numberLine;
            numberLine = 0;
            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
            numberLine = tempNum;
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
            Function.ChangeContextMenu(rtxtShortDescription, contextCopy, contextShortDesription);
            Function.ChangeContextMenu(rtxtDescription, contextCopy, contextLongDescription);
        }

        //Wyrównanie linii
        private void LineAlignment()
        {
            p.CopyTextToList(rtxtAmountsOfFood.Text);
            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
            rtxtAmountsOfFood.Lines = p.TextOutput();
        }

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
                ColorMy.RedForeToolStrip(first);
                ColorMy.RedForeToolStrip(second);
                ColorMy.RedForeToolStrip(third);
                btnEnter.ForeColor = Color.Red;
            }
            else
            {
                p.EnterOn = true;
                addRecipe = true;

                first.Text = enterOn;
                second.Text = enterOn;
                third.Text = enterOn;
                btnEnter.ForeColor = Color.Green;
                ColorMy.GreenForeColorToolStrip(first);
                ColorMy.GreenForeColorToolStrip(second);
                ColorMy.GreenForeColorToolStrip(third);
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
                second.SelectionLength = 0;
            }
        }

        //przelicza znaki do fokusa w zaleznosci od roznych czynnikow
        private void StartFocus(RichTextBox second)
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
                        second.SelectionStart = p.NumberOfCharactersToFocus(second.Text) + 1;
                    }

                }
                else
                {
                    second.SelectionStart = p.NumberOfCharactersToFocus(second.Text);
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
                    p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                    int temp = numberLine;
                    RichTextBoxMy.ClassicEnterPlusNewLine(first, second, third);
                    p.CopyTextToList(rtxtAmountsOfFood.Text);
                    numberLine = temp;
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
            p.NumberLine = numberLine;
            complete.NumberLine = numberLine;

            maxLine = name.Lines.Length - 1;
            p.MaxLine = maxLine;

            if (numberLine >= maxLine)
            {
                maxLine = numberLine;
                p.MaxLine = p.NumberLine;
            }
            else
            {
                p.AddRecipeForm2 = false;
            }
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
                rtxtAmountsOfFood.HighlightItem();

                e.Handled = true;
                rtxtAmountsOfFood.SelectionStart = rtxtAmountsOfFood.Text.Length;

                p.AddRecipeForm2 = false;
            }
        }

        //podswietla pole gdy ktoś tabulatorem przesuwa i 
        public void HighlightEditField(RichTextBox name, Color color)
        {
            if (name.BackColor == color)
            {
                DisplayForm(name);
            }
        }

        public void HighlightEditField(TextBox name, Color color)
        {
            if (name.BackColor == color)
            {
                DisplayForm(name);
            }
        }

        //podswietla edytowany element
        public bool DisplayForm(RichTextBox rich)
        {
            rich.HighlightItem();
            Function.BorderColor(rich, panelMain, panelPicture);
            rich.ForeColor = Color.Black;
            rich.Focus();
            return true;
        }

        public void DisplayForm(TextBox rich)
        {
            rich.HighlightItem();
            Function.BorderColor(rich, panelMain, panelPicture);
            rich.ForeColor = Color.Black;
        }

        private bool CheckNameAfterClick()
        {
            bool correct = false;

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
            Function.IsCheckBoxChecked(nameElement, locationNumberInTable, tabMealOrIngredients);
            CheckNameAfterClick();
            ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);

            Function.ColorAreaAfterUnblocking(panelMain);
            Function.ChangeForeColorToWhiteAllItems(panelMain);
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
                rTxtIngredients.Rtf = ingrediet;
                rtxtShortDescription.Rtf = shortDes;
                rtxtDescription.Rtf = longDes;

                rtxtPortion.Text = portions.ToString();
                lblCuisine.Text = cuisines;
                lblLevel.Text = level;
                lblTime.Text = time;

                Function.ShowStar(1, pbStar1, rating);
                Function.ShowStar(2, pbStar2, rating);
                Function.ShowStar(3, pbStar3, rating);

                ContextMenuBlock();

                p.EnterOn = false;
            }
            Function.ColorFieldsAfterBlocking(panelMain, rtxtPortion);
            Function.BlockingFields(panelMain);
            Function.BlockCheckbox(panelLeft);
            Function.BlockCheckbox(panelRight);
            btnAddRest.Visible = false;
            btnCancel.Visible = false;
            btnClose.Visible = true;
            btnDelete.Visible = true;
            btnModify.Visible = true;
            addRecipe = false;
            CMGramsEnter.Text = enterOff;
            CMAmountsEnter.Text = enterOff;
            CMIngridientsEnter.Text = enterOff;
            Function.ChangeForeColorToWhiteAllItems(panelMain);
            InTheProcessOfAdding = false;

            cancel = false;
        }

        public RecipesBase Model(RecipesBase model)
        {
            if (rtxtPortion.Text == "")
            {
                rtxtPortion.Text = "1";
                model.NumberPortions = 1;
            }
            else
            {
                try
                {
                    model.NumberPortions = int.Parse(rtxtPortion.Text);
                }
                catch (Exception e)
                {
                    MessageBox.Show("W rubrykę - Porcje, wpisuje się tylko liczby!");
                }
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
            HighlightEditField(txtName, brightColorForComparison);
            txtName.Paste();
            Function.SetFocusToTheEndOfTheName(txtName);
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
            HighlightEditField(rTxtIngredients, brightColorForComparison);
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
            //FUNKCJA DO POPRAWIENIA
            Function.Separator(rtxtDescription, 5);
        }

        private void cofnijToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtDescription.Undo();
        }

        private void wytnijToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.Cut();
        }

        private void kopiujToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.Copy();
        }

        private void wklejToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            HighlightEditField(rtxtDescription, brightColorForComparison);

            string clipboardText = string.Empty;
            int start = rtxtDescription.SelectionStart;

            try
            {
                clipboardText = Clipboard.GetText(TextDataFormat.Text);
                if (rtxtDescription.SelectedText.Length > 1)
                {
                    rtxtDescription.SelectedText = "";
                }
                if (string.IsNullOrWhiteSpace(rtxtDescription.Text))
                {
                    rtxtDescription.Text = clipboardText;
                    rtxtDescription.SelectionStart = rtxtDescription.TextLength;
                }
                else
                {
                    rtxtDescription.Text = rtxtDescription.Text.Insert(start, clipboardText);
                    rtxtDescription.SelectionStart = start + clipboardText.Length;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + " Błąd po wklejeniu - do poprawienia");
            }
        }

        private void usuńToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.SelectedText = "";
        }

        private void undoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            txtName.Undo();
        }

        private void cofnijToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            rtxtShortDescription.Undo();
        }

        private void wytnijToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rtxtShortDescription.Cut();
        }

        private void kopiujToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rtxtShortDescription.Copy();
        }

        private void wklejToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rtxtShortDescription.Paste();
            HighlightEditField(rtxtShortDescription, brightColorForComparison);
        }

        private void usuńToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            rtxtShortDescription.SelectedText = "";
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
            if (btnEnter.Text == enterOff)
            {
                btnEnter.Text = enterOn;
                btnEnter.ForeColor = Color.Green;
            }
            else
            {
                btnEnter.Text = enterOff;
                btnEnter.ForeColor = Color.Red;
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
            Array.Resize(ref tab, rTxtGrams.Lines.Length);
            tab = rTxtGrams.Lines;
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
            Function.DeleteRecipes(idDgGridForm2);
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

                Function.Separator(rTxtIngredients, 3);

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
            else if (rtxtShortDescription.SelectionLength > 0) rtxtShortDescription.Copy();
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

        #endregion Menu

        #region Button

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Function.DeleteRecipes(idDgGridForm2);
            this.Hide();
            Form1 m = new Form1();
            m.ShowDialog();
        }


        private bool ModifyRecipes()
        {
            bool modify = false;
            clear = "modification";
            btnCancel.Text = "Anuluj";

            if (txtName.ReadOnly == true)
            {
                btnClose.Visible = false;
                ContextMenuBlock();
                chcVegetarian.Enabled = true;
                TemporaryFields();
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

                rTxtIngredients.Focus();
                rTxtIngredients.SelectionStart = rTxtIngredients.TextLength;
                IndexChar(rTxtIngredients);
                maxLine = rTxtIngredients.Lines.Length - 1;


                if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Text) && !string.IsNullOrEmpty(rTxtGrams.Text) && !string.IsNullOrEmpty(rTxtIngredients.Rtf))
                {
                    RichTextBoxMy.SetFocus(rTxtIngredients, rtxtAmountsOfFood, rTxtGrams);
                    DisplayForm(rTxtIngredients);
                }
            }
            else
            {
                ContextMenuBlock();
                rtxtAmountsOfFood.Lines = Stamp.AddStamp(rtxtAmountsOfFood.Lines);
                rTxtGrams.Lines = Stamp.AddStamp(rTxtGrams.Lines);

                if (isItNumber == false)
                {
                    if (txtName.Text == titleForm2) { }
                    else
                    {
                        CheckNameAfterClick();

                        if (newForm)
                        {
                            Function.CheckName(txtName, correctModyficationName);
                        }

                        rtxtAmountsOfFood.Text = Stamp.RemoveCharacters(rtxtAmountsOfFood.Text);
                        rTxtGrams.Text = Stamp.RemoveCharacters(rTxtGrams.Text);
                    }

                    if (txtName.Text != "")
                    {
                        rtxtAmountsOfFood.Lines = Stamp.AddStamp(rtxtAmountsOfFood.Lines);
                        rTxtGrams.Lines = Stamp.AddStamp(rTxtGrams.Lines);
                        try
                        {
                            #region PanelMain

                            var up = RecipesBase.GetById(idDgGridForm2);
                            up.RecipesName = txtName.Text;
                            up.AmountsMeal = rtxtAmountsOfFood.Text;
                            up.Grams = rTxtGrams.Text;
                            up.Ingredients = rTxtIngredients.Rtf;
                            up.ShortDescription = rtxtShortDescription.Rtf;
                            up.LongDescription = rtxtDescription.Rtf;
                            if (rtxtPortion.Text == "") rtxtPortion.Text = "1";
                            up.NumberPortions = int.Parse(rtxtPortion.Text);
                            #endregion
                            #region ComponentLeft
                            up.IdFishIngredients = checkBoxIngredients[0];
                            up.IdPastaIngredients = checkBoxIngredients[1];
                            up.IdFruitsIngredients = checkBoxIngredients[2];
                            up.IdMuschroomsIngredients = checkBoxIngredients[3];
                            up.IdBirdIngredients = checkBoxIngredients[4];
                            up.IdMeatIngredients = checkBoxIngredients[5];
                            up.IdEggsIngredients = checkBoxIngredients[6];
                            up.Vegetarian = checkBoxIngredients[7];

                            #endregion
                            #region MealRight
                            up.SnackMeal = checkBoxDish[0];
                            up.DinnerMeal = checkBoxDish[1];
                            up.SoupMeal = checkBoxDish[2];
                            up.DessertMeal = checkBoxDish[3];
                            up.DrinkMeal = checkBoxDish[4];
                            up.PreservesMeal = checkBoxDish[5];
                            up.SaladMeal = checkBoxDish[6];
                            #endregion

                            up.CategoryPreparationTime = executionTimeForm2;
                            up.CategoryDifficultLevel = lblLevel.Text;
                            up.CategoryPreparationTime = lblTime.Text;
                            if (idRatingForm2 == null) idRatingForm2 = dash;
                            else up.CategoryRating = idRatingForm2;

                            up.CategoryCuisines = lblCuisine.Text;

                            if (linkForm2 == null) linkForm2 = Stamp.StampsCharacters();
                            else up.PhotoLinkLocation = linkForm2;

                            RecipesBase.Update(up);
                            modify = true;
                            btnClose.Visible = true;
                            chcVegetarian.Enabled = false;
                            rtxtAmountsOfFood.Text = Stamp.RemoveCharacters(rtxtAmountsOfFood.Text);
                            rTxtGrams.Text = Stamp.RemoveCharacters(rTxtGrams.Text);
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
                            btnCancel.Visible = false;

                            Function.ChangeForeColorToWhiteAllItems(panelMain);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Błąd W AddSTampBlock -" + ex.Message);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Podaj nazwę Przedmiotu");
                    }
                }
                isItNumber = false;
                rtxtAmountsOfFood.Text = Stamp.RemoveCharacters(rtxtAmountsOfFood.Text);
                rTxtGrams.Text = Stamp.RemoveCharacters(rTxtGrams.Text);
                Function.ChangeForeColorToWhiteAllItems(panelMain);
            }
            return modify;
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == false)
            {
                ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
            }
            DeleteEmptyLines();
            LineAlignment();

            // kopia rtxgrams do tablicy
            Array.Resize(ref tab, rTxtGrams.Lines.Length);
            CopyTextToTable(rTxtGrams.Text, tab);

            Function.ChangeForeColorToWhiteAllItems(panelMain);
            InTheProcessOfAdding = true;
            bool modify = ModifyRecipes();
            if (modify)
            {
                InTheProcessOfAdding = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (WantToDisable())
            {
                this.Visible = false;
                Function.PokazForm("Form1");
            }
        }

        private bool WantToDisable()
        {
            if (InTheProcessOfAdding)
            {
                var result = MessageBox.Show("Jesteś w trakcie dodawania przepisu\n       Na pewno przerwać proces?", "Zamykanie programu", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    InTheProcessOfAdding = false;
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        bool isClosing = false;

        internal bool InTheProcessOfAdding = false;
        private bool AddRecipes()
        {
            bool added = false;
            TemporaryFields();
            numberLine = 0;

            DeleteEmptyLines();
            LineAlignment();

            if (!CheckNameAfterClick())
            {
                if (isItNumber == false)
                {
                    if (txtName.Text == "")
                    {
                        MessageBox.Show("Tytuł nie może byc pusty");
                    }
                    else
                    {
                        rtxtAmountsOfFood.Lines = Stamp.AddStamp(rtxtAmountsOfFood.Lines);
                        rTxtGrams.Lines = Stamp.AddStamp(rTxtGrams.Lines);

                        RecipesBase model = new RecipesBase();

                        model.RecipesName = txtName.Text.ToUpper();
                        model.AmountsMeal = rtxtAmountsOfFood.Text;
                        model.Grams = rTxtGrams.Text;

                        model.Ingredients = rTxtIngredients.Rtf;
                        model.ShortDescription = rtxtShortDescription.Rtf;
                        model.LongDescription = rtxtDescription.Rtf;

                        Model(model);

                        model.CategoryCuisines = lblCuisine.Text;
                        model.CategoryRating = idRatingForm2;
                        model.CategoryDifficultLevel = lblLevel.Text;
                        model.CategoryPreparationTime = lblTime.Text;

                        if (string.IsNullOrWhiteSpace(linkForm2))
                        {
                            linkForm2 = Stamp.StampsCharacters();
                        }
                        else
                        {
                            model.PhotoLinkLocation = linkForm2;
                        }

                        #region MealAdd
                        model.SnackMeal = checkBoxDish[0];
                        model.DinnerMeal = checkBoxDish[1];
                        model.SoupMeal = checkBoxDish[2];
                        model.DessertMeal = checkBoxDish[3];
                        model.DrinkMeal = checkBoxDish[4];
                        model.PreservesMeal = checkBoxDish[5];
                        model.SaladMeal = checkBoxDish[6];
                        #endregion
                        #region IngridientsAdd
                        model.IdFishIngredients = checkBoxIngredients[0];
                        model.IdPastaIngredients = checkBoxIngredients[1];
                        model.IdFruitsIngredients = checkBoxIngredients[2];
                        model.IdMuschroomsIngredients = checkBoxIngredients[3];
                        model.IdBirdIngredients = checkBoxIngredients[4];
                        model.IdMeatIngredients = checkBoxIngredients[5];
                        model.IdEggsIngredients = checkBoxIngredients[6];
                        model.Vegetarian = checkBoxIngredients[7];
                        #endregion

                        RecipesBase.Add(model);



                        added = true;
                        newForm = true;
                        chcVegetarian.Enabled = false;



                        titleForm2 = txtName.Text;
                        idDgGridForm2 = model.Id;
                        rtxtAmountsOfFood.Text = Stamp.RemoveCharacters(rtxtAmountsOfFood.Text);
                        rTxtGrams.Text = Stamp.RemoveCharacters(rTxtGrams.Text);

                        MessageBox.Show("Dodano przedmiot");

                        btnAddRest.Visible = false;
                        btnClose.Text = "Zamknij";

                        Function.BlockingFields(panelMain);
                        Function.BlockCheckbox(panelLeft);
                        Function.BlockCheckbox(panelRight);
                        Function.ColorFieldsAfterBlocking(panelMain, rtxtPortion);

                        Function.ShowStar(1, pbStar1, idRatingForm2);
                        Function.ShowStar(2, pbStar2, idRatingForm2);
                        Function.ShowStar(3, pbStar3, idRatingForm2);

                        clear = "0";

                        p.AddRecipeForm2 = true;
                        addRecipe = false;
                        Function.ChangeForeColorToWhiteAllItems(panelMain);


                        btnModify.Visible = true;
                        btnDelete.Visible = true;
                    }
                }
                isItNumber = false;
            }
            return added;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!rtxtAmountsOfFood.ReadOnly)
            {
                Function.ChangeForeColorToWhiteAllItems(panelMain);
                InTheProcessOfAdding = true;
                bool added = AddRecipes();
                if (added)
                {
                    InTheProcessOfAdding = false;
                    btnAdd.Visible = false;
                }
            }
        }

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (rtxtPortion.ReadOnly)
            {
                rtxtPortion.ReadOnly = false;
                btnConvert.BackColor = ColorMy.Red();
                DisplayForm(rtxtPortion);
                rtxtPortion.SelectionStart = rtxtPortion.TextLength;
            }
            else
            {
                if (rtxtAmountsOfFood.ReadOnly == false)
                {
                    MessageBox.Show("Przeliczać porcje można dopiero po dodaniu przepisu lub po wykonaniu jego modyfikacji");
                }
                else
                {
                    Convert();

                }
                rtxtPortion.ReadOnly = true;
            }
        }

        private void Convert()
        {
            rtxtAmountsOfFood.Lines = ConvertUnits.ConvertFunction(rtxtAmountsOfFood.Text, numberOfPortionsForm2, int.Parse(rtxtPortion.Text));

            rtxtPortion.BackColor = ColorMy.CreateDeepBlue();
            rtxtPortion.ReadOnly = true;
            rtxtAmountsOfFood.Visible = true;
            btnConvert.BackColor = ColorMy.CreateBlueAtlantic();
            rtxtPortion.ForeColor = Color.White;

            btnModify.Enabled = true;
            btnAdd.Enabled = true;
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
                Function.ChangeForeColorToWhiteAllItems(panelMain);
                Form3 OpenForm = new Form3();

                OpenForm.hideForm3 = hideForm;
                OpenForm.correctModyficationName3 = correctModyficationName;
                OpenForm.clearForm3 = clear;
                OpenForm.idDgGridForm3 = idDgGridForm2;
                OpenForm.titleForm3 = txtName.Text;
                OpenForm.ingredientForm3 = rTxtIngredients.Rtf;
                OpenForm.gramsForm3 = rTxtGrams.Text;

                if (rtxtAmountsOfFood.Text != "")
                {
                    OpenForm.AmountsOfFoodForm3 = rtxtAmountsOfFood.Text;
                }

                OpenForm.shortDescriptionForm3 = rtxtShortDescription.Rtf;
                OpenForm.InstructionForm3 = rtxtDescription.Rtf;

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

                for (int i = 0; i < checkBoxDish.Length; i++)
                {
                    OpenForm.IdMealForm3[i] = checkBoxDish[i];
                }

                #endregion

                #region ComponentAdd

                for (int i = 0; i < checkBoxIngredients.Length; i++)
                {
                    OpenForm.idComponentsForm3[i] = checkBoxIngredients[i];
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

                OpenForm.SavedCheckBoxForm3 = SavedCheckBoxForm2;
                OpenForm.InTheProcessOfAdding = InTheProcessOfAdding;
                InTheProcessOfAdding = false;
                OpenForm.newForm3 = newForm;
                this.Hide();
                OpenForm.ShowDialog(); 
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
        private void TemporaryFields()
        {
            cancel = true;
            title1 = txtName.Text;
            amounts = rtxtAmountsOfFood.Text;
            grams = rTxtGrams.Text;
            ingrediet = rTxtIngredients.Rtf;
            shortDes = rtxtShortDescription.Rtf;
            longDes = rtxtDescription.Rtf;
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

        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
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

        private void txtShortDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (rtxtShortDescription.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    tabBool = true;
                }

                HighlightEditField(rtxtShortDescription, brightColorForComparison);

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
                HighlightEditField(rtxtShortDescription, brightColorForComparison);
                ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                CheckNameAfterClick();
            }
        }

        private void txtShortDescription_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(rtxtShortDescription, "Streszczenie przepisu");
        }

        private void rtxtDescription_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtName.ReadOnly == false)
                {
                    HighlightEditField(rtxtDescription, brightColorForComparison);
                    ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                    CheckNameAfterClick();
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Błąd podczas kliknięcia w RtxtDescription -NULL -" + ex.Message);
            }
            catch (Exception exc)
            {
                MessageBox.Show("Błąd podczas kliknięcia w RtxtDescription -" + exc.Message);
            }
        }

        private void txtName_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                HighlightEditField(txtName, brightColorForComparison);
                ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                Function.SetFocusToTheEndOfTheName(txtName);
            }
        }

        bool tabBool = false;
        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                HighlightEditField(txtName, brightColorForComparison);

                if (e.KeyCode == Keys.Tab)
                {
                    tabBool = true;
                }
            }
        }

        int start;
        Color brightColorForComparison = ColorMy.CreateBright();
        private void rtxtAmountsOfFood_KeyDown(object sender, KeyEventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    tabBool = true;
                }

                changeLine = false;

                HighlightEditField(rtxtAmountsOfFood, brightColorForComparison);

                if (rtxtAmountsOfFood.SelectionLength > 0)
                {
                    if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                    {
                        int selectionStart = rtxtAmountsOfFood.SelectionStart;

                        rtxtAmountsOfFood.Text = rtxtAmountsOfFood.Text.Remove
                                                (
                                                    selectionStart,
                                                    rtxtAmountsOfFood.SelectedText.Length
                                                );

                        p.CopyTextToList(rtxtAmountsOfFood.Text);
                        rtxtAmountsOfFood.SelectionStart = selectionStart;
                    }
                }

                if (e.KeyCode == Keys.Enter && !p.EnterOn)
                {
                    rTxtGrams.Focus();
                    int selectionStart = rtxtAmountsOfFood.SelectionStart;
                    rtxtAmountsOfFood.Lines = p.TextOutput();
                    rtxtAmountsOfFood.SelectionStart = selectionStart;
                    AmountsAndGramsKeyDown(e, rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);
                }
                else if (e.KeyCode == Keys.Enter && p.EnterOn)
                {
                    NumberOfLines(e, rtxtAmountsOfFood);
                    p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                }


                start = rtxtAmountsOfFood.SelectionStart;
                return;
            }
        }


        private void rtxtAmountsOfFood_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!rtxtAmountsOfFood.ReadOnly)
            {
                p.CharacterInput(e.KeyChar, start);
            }
        }

        private void rtxtAmountsOfFood_KeyUp(object sender, KeyEventArgs e)
        {
            if (!rtxtAmountsOfFood.ReadOnly)
            {
                int selectionStart = rtxtAmountsOfFood.SelectionStart;

                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                }

                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    p.CopyTextToList(rtxtAmountsOfFood.Text);

                    if (rtxtAmountsOfFood.SelectionStart >= 0)
                    {
                        p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                    }

                    rtxtAmountsOfFood.Lines = p.TextOutput();
                    rtxtAmountsOfFood.Focus();
                    rtxtAmountsOfFood.SelectionStart = selectionStart;
                }
                else if (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Up && e.KeyCode != Keys.Down)
                {

                    rtxtAmountsOfFood.Lines = p.TextOutput();

                    if (p.EnterOn && e.KeyCode == Keys.Enter)
                    {
                        p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                        rtxtAmountsOfFood.SelectionStart = selectionStart;
                    }
                    else
                    {
                        if (p.ValidateTheLastCharacter)
                        {
                            rtxtAmountsOfFood.SelectionStart = selectionStart - 1;
                            p.ValidateTheLastCharacter = false;
                        }
                        else rtxtAmountsOfFood.SelectionStart = selectionStart;

                        if (p.Count < rTxtGrams.Lines.Length)
                        {
                            p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                        }
                    }
                }

                Alignvertically(e, rtxtAmountsOfFood);
            }
        }

        private bool AlignLines(bool skip)
        {
            if (selected)
            {
                if (!skip)
                {
                    DeleteEmptyLines();
                    p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                    rtxtAmountsOfFood.Lines = p.TextOutput();
                    skip = false;
                    selected = false;
                }
            }

            return skip;
        }

        private void rtxtAmountsOfFood_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Text))
                {
                    if (IsTextSelected(rtxtAmountsOfFood))
                    {
                        selected = true;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            { }
            IndexChar(rtxtAmountsOfFood);
        }

        private void rtxtAmountsOfFood_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                IndexChar(rtxtAmountsOfFood);

                HighlightEditField(rtxtAmountsOfFood, brightColorForComparison);
            }
        }

        private void rtxtAmountsOfFood_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(rtxtAmountsOfFood, "Ilości składników - tylko i wyłącznie Liczby można wpisywać");
        }

        private void rtxtPortion_KeyDown(object sender, KeyEventArgs e)
        {
            HighlightEditField(rtxtPortion, brightColorForComparison);

            if (rtxtPortion.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    tabBool = true;
                }

                if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                {
                    UndoChanges();
                }
            }
        }

        private void rtxtPortion_Click(object sender, EventArgs e)
        {
            if (rtxtPortion.ReadOnly == false)
            {
                DisplayForm(rtxtPortion);
                ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                CheckNameAfterClick();
            }
        }

        public void CopyTextToTable(string text, string[] table)
        {
            try
            {
                Array.Clear(table, 0, table.Length);
                tab = text.Split('\n');
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Błąd w CopyTextToTable - NULL" + ex.Message);
            }
        }

        string[] tab = new string[1];

        private void rTxtGrams_KeyUp(object sender, KeyEventArgs e)
        {
            if (!rTxtGrams.ReadOnly)
            {
                if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Left || e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)
                {
                    return;
                }
                else if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Tab)
                {
                    complete.CreateAutoComplete(e);
                }
                if (e.KeyCode == Keys.Enter)
                {
                    rtxtAmountsOfFood.Lines = p.TextOutput();
                }
                if (e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
                {
                    p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                }

                Alignvertically(e, rTxtGrams);
            }
        }

        private void rTxtGrams_KeyDown(object sender, KeyEventArgs e)
        {
            if (rTxtGrams.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    tabBool = true;
                }

                IndexChar(rTxtGrams);
                changeLine = false;

                HighlightEditField(rTxtGrams, brightColorForComparison);

                if (e.KeyCode == Keys.Enter)
                {
                    AmountsAndGramsKeyDown(e, rTxtGrams, rTxtIngredients, rtxtAmountsOfFood);
                }
            }
        }

        private void rTxtGrams_Click(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == false)
            {
                IndexChar(rTxtGrams);

                HighlightEditField(rTxtGrams, brightColorForComparison);
                ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                CheckNameAfterClick();
            }
        }

        private void rTxtGrams_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(rTxtGrams.Text))
                {
                    if (IsTextSelected(rTxtGrams))
                    {
                        selected = true;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            { }

            IndexChar(rTxtGrams);
        }

        private void rTxtIngredients_KeyDown(object sender, KeyEventArgs e)
        {
            if (rTxtIngredients.ReadOnly == false)
            {
                if (e.KeyCode == Keys.Tab)
                {
                    tabBool = true;
                }

                HighlightEditField(rTxtIngredients, brightColorForComparison);

                if (e.KeyCode == Keys.Enter)
                {
                    if (NumberOfLines(e, rTxtIngredients)) return;

                    MaxLineIncrease();

                    if (p.EnterOn)
                    {
                        RichTextBoxMy.ClassicEnterPlusNewLine(rTxtIngredients, rtxtAmountsOfFood, rTxtGrams);
                    }
                    else
                    {
                        ChangeAddLine(e);
                        DisplayForm(rtxtAmountsOfFood);
                    }

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
                ConvertUnits.ConverterToDecimal(rtxtAmountsOfFood.Text);
                CheckNameAfterClick();
            }
        }

        bool selected = false;


        private void rTxtIngredients_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(rTxtIngredients.Text))
                {
                    if (IsTextSelected(rTxtIngredients))
                    {
                        selected = true;
                    }
                }
            }
            catch (IndexOutOfRangeException ex)
            { }
            IndexChar(rTxtIngredients);
        }

        private bool IsTextSelected(RichTextBox richName)
        {
            if (richName.SelectedText.Length > richName.Lines[numberLine].Length) return true;
            else return false;
        }
        #endregion TextField

        private void rtxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (rtxtDescription.ReadOnly == false)
                {
                    if (e.KeyCode == Keys.Tab)
                    {
                        tabBool = true;
                    }

                    HighlightEditField(rtxtDescription, brightColorForComparison);

                    if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
                    {
                        UndoChanges();
                    }

                    point.CreateBullet(e);
                }
            }
            catch (IndexOutOfRangeException exc)
            {
                MessageBox.Show(exc.Message);
            }
            catch (NullReferenceException exx)
            {
                MessageBox.Show(exx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ContextEnter(CMAmountsEnter, CMGramsEnter, CMIngridientsEnter);
        }

        private void txtName_TextChanged(object sender, EventArgs e)
        {
            int start = txtName.SelectionStart;
            Function.TitleTextToUpper(txtName);
            txtName.SelectionStart = start;
        }

        #region CheckboxMeal
        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcSnack, 1, checkBoxDish);
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcDinner, 2, checkBoxDish);
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcSoup, 3, checkBoxDish);
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcDessert, 4, checkBoxDish);
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcDrink, 5, checkBoxDish);
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcPreserves, 6, checkBoxDish);
        }

        private void rTxtIngredients_KeyUp(object sender, KeyEventArgs e)
        {
            Alignvertically(e, rTxtIngredients);
        }

        private void Alignvertically(KeyEventArgs e, RichTextBox richName)
        {
            int start = richName.SelectionStart;
            if (e.KeyCode != Keys.Enter && selected || e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete)
            {
                if (Alignment(richName))
                {
                    richName.SelectionStart = start;
                    selected = false;
                }
            }
        }

        private static bool CzyPierwszyWLinii(RichTextBox nameRich, int selectionStart, int lineToDelete)
        {
            string[] podz = nameRich.Text.Split('\n');
            int lenght = 0;
            for (int i = 0; i < podz.Length; i++)
            {
                lenght += podz[i].Length;
                if (lenght > selectionStart)
                {
                    return false;
                }
                else if (lenght == selectionStart && lineToDelete == 1)
                {
                    return true;
                }
            }
            return false;
        }

        private bool Alignment(RichTextBox name)
        {
            bool align = false;
            if (!string.IsNullOrWhiteSpace(name.Text))
            {
                string[] arrayAmount, arrayGrams, arrayIngre;
                PrzypisanieDoTablic(out arrayAmount, out arrayGrams, out arrayIngre);

                int longest = arrayIngre.Length;
                if (DifferentLength(arrayAmount, arrayGrams, arrayIngre, ref longest))
                {
                    ZmianaRozmiaru(ref arrayAmount, ref arrayGrams, ref arrayIngre, longest);
                }

                int lineToDelete = 0;
                for (int i = longest - 1; i > 0; i--)
                {
                    if (string.IsNullOrWhiteSpace(arrayAmount[i]) && string.IsNullOrWhiteSpace(arrayGrams[i]) && string.IsNullOrWhiteSpace(arrayIngre[i]))
                    {
                        lineToDelete++;
                        // CzyPierwszyWLinii(name, name.SelectionStart, lineToDelete);

                        align = true;

                    }
                    else
                    {
                        break;
                    }
                }

                if (CzyPierwszyWLinii(name, name.SelectionStart, lineToDelete)) //lineToDelete  > 0
                {
                    p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients, longest - lineToDelete);
                }
            }
            return align;
        }

        private static void ZmianaRozmiaru(ref string[] arrayAmount, ref string[] arrayGrams, ref string[] arrayIngre, int longest)
        {
            Array.Resize(ref arrayAmount, longest);
            Array.Resize(ref arrayGrams, longest);
            Array.Resize(ref arrayIngre, longest);
        }

        private void PrzypisanieDoTablic(out string[] arrayAmount, out string[] arrayGrams, out string[] arrayIngre)
        {
            arrayAmount = rtxtAmountsOfFood.Text.Split('\n');
            arrayGrams = rTxtGrams.Text.Split('\n');
            arrayIngre = rTxtIngredients.Text.Split('\n');
        }

        /// <summary>
        /// Sprawdza czy dlugości są różne 
        /// </summary>
        /// <param name="arrayAmount"></param>
        /// <param name="arrayGrams"></param>
        /// <param name="arrayIngre"></param>
        /// <param name="longest"></param>
        /// <returns></returns>
        private bool DifferentLength(string[] arrayAmount, string[] arrayGrams, string[] arrayIngre, ref int longest)
        {
            bool different = false;
            if (arrayAmount.Length != arrayGrams.Length || arrayAmount.Length != arrayIngre.Length)
            {
                longest = p.AlignTheNumberOfLines(rTxtGrams, rTxtIngredients);
                different = true;
            }

            return different;
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!WantToDisable())
            {
                e.Cancel = true;
            }
        }

        private void txtName_Validated(object sender, EventArgs e)
        {
            if (!txtName.ReadOnly && tabBool)
            {
                if (Function.CheckName(txtName, correctModyficationName))
                {
                    txtName.Focus();
                }
                else
                {
                    rtxtPortion.Focus();
                    HighlightEditField(rtxtPortion, brightColorForComparison);
                }
                tabBool = false;
            }
        }

        private void rtxtPortion_Validated(object sender, EventArgs e)
        {
            if (txtName.ReadOnly == true && rtxtPortion.ReadOnly == false)
            {
                if (!string.IsNullOrWhiteSpace(rtxtPortion.Text) && rtxtPortion.Text != "0")
                {
                    Convert();
                }
            }
            else if (!txtName.ReadOnly && tabBool)
            {
                numberLine = 0;

                MoveFocusAndChangeColor(rtxtAmountsOfFood);

                if (addRecipe == false)
                {
                    Function.SetFocusToTheEndOfTheName(rtxtAmountsOfFood);
                }

                numberLine = Function.NumberOfLinesInColumn(rtxtAmountsOfFood.Text);
            }
            tabBool = false;
        }

        private void rtxtAmountsOfFood_Validated(object sender, EventArgs e)
        {
            if (!rtxtAmountsOfFood.ReadOnly && tabBool)
            {
                MoveFocusAndChangeColor(rTxtGrams);

                if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Text) && !p.CorrectDate)
                {
                    rtxtAmountsOfFood.Lines = p.TextOutput();
                }
                tabBool = false;
            }
        }

        private void rTxtGrams_Validated(object sender, EventArgs e)
        {
            if (!rTxtGrams.ReadOnly && tabBool)
            {
                MoveFocusAndChangeColor(rTxtIngredients);
                tabBool = false;
            }
        }

        private void MoveFocusAndChangeColor(RichTextBox currentName)
        {
            currentName.Focus();
            DisplayForm(currentName);
        }

        private void rTxtIngredients_Validated(object sender, EventArgs e)
        {
            if (!rTxtIngredients.ReadOnly && tabBool)
            {
                MoveFocusAndChangeColor(rtxtShortDescription);
                tabBool = false;
            }
        }

        private void rtxtDescription_Validated(object sender, EventArgs e)
        {
            if (!rtxtDescription.ReadOnly && tabBool)
            {
                rtxtDescription.BackColor = ColorMy.CreateBright();
                tabBool = false;
            }
        }

        private void rtxtShortDescription_Validated(object sender, EventArgs e)
        {
            if (!rtxtShortDescription.ReadOnly && tabBool)
            {
                MoveFocusAndChangeColor(rtxtDescription);
                tabBool = false;
            }
        }

        private void btnClose_Validated(object sender, EventArgs e)
        {
            if (!txtName.ReadOnly)
            {
                txtName.Focus();
                DisplayForm(txtName);
            }
            else
            {
                txtName.Focus();
            }
        }

        private void btnCancel_Validated(object sender, EventArgs e)
        {
            if (!txtName.ReadOnly)
            {
                txtName.Focus();
                DisplayForm(txtName);
            }
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcSalad, 7, checkBoxDish);
        }

        private void chcVegetarian_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcVegetarian, 8, checkBoxIngredients);

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
            CheckChangedGroup(chcFish, 1, checkBoxIngredients);
            chcVegetarian.Checked = false;
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcPasta, 2, checkBoxIngredients);
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcFruits, 3, checkBoxIngredients);
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcMuschrooms, 4, checkBoxIngredients);
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcMeat, 6, checkBoxIngredients);
            chcVegetarian.Checked = false;
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcBird, 5, checkBoxIngredients);
            chcVegetarian.Checked = false;
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            CheckChangedGroup(chcEggs, 7, checkBoxIngredients);
        }
        #endregion CheckBoxComponent

        private void pbBullPoint(object sender, EventArgs e)
        {
            if (rtxtDescription.ReadOnly == false)
            {
                HighlightEditField(rtxtDescription, brightColorForComparison);
                point.ChangeParagraph();
            }
        }
    }
}
