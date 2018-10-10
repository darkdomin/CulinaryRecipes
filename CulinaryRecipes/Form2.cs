using CulinaryRecipes.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace CulinaryRecipes
{
    public partial class Form2 : Form
    {
        public int idDgGridForm2 { get; set; }
        public int NumberOfPortionsForm2 { get; set; }
        public int counterForm2 { get; set; }
        public int[] IdMealForm2 = new int[7];
        public int[] ingridientsForm2 = new int[7];
        public string titleForm2 { get; set; }
        public string amountsOfIngredientsForm2{ get; set; }
        public string ingredientForm2{ get; set; }
        public string ShortDescriptionForm2{ get; set; }
        public string instructionForm2{ get; set; }
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

        #region Funkcje

        private void ClearLabelText()
        {
            lblLevel.Text = dash;
            lblTime.Text = dash;
            lblCuisine.Text = dash;
        }

        //reguluje spacje w opisie
        private string Interval()
        {
            foreach (char znak in rtxtDescription.Text)
            {
                if (znak == '•')
                {
                    rtxtDescription.Text = interval + instructionForm2;
                    break;
                }
                else
                {
                    rtxtDescription.Text = instructionForm2;
                };
            }
            return rtxtDescription.Text;
        }

        private void changeColorPbUnblock(Control set)
        {
            foreach (Control c in set.Controls)
            {
                if (c is PictureBox)
                {
                    if (c.Name == pbLittlePhoto.Name || c.Name == pbStar1.Name || c.Name == pbStar2.Name || c.Name == pbStar3.Name || c.Name == pictureBox2.Name) continue;
                    ((PictureBox)c).BackColor = Function.CreateBrightColor();
                }
            }
        }

        private void changeColorPbblock(Control set)
        {
            foreach (Control c in set.Controls)
            {
                if (c is PictureBox)
                {
                    if (c.Name == pbLittlePhoto.Name || c.Name == pbStar1.Name || c.Name == pbStar2.Name || c.Name == pbStar3.Name || c.Name == pictureBox2.Name) continue;
                    ((PictureBox)c).BackColor = Function.CreateColorBlockingFields();
                }
            }
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

        private void HideStars()
        {
            pbStar1.Visible = false;
            pbStar2.Visible = false;
            pbStar3.Visible = false;
        }

        //dodaje znak w pustym polu (zapobiegając przesuwaniu się do góry) stringOfCharacters
        private string[] AddStamp(string[] name)
        {
            string[] table = new string[name.Length];
            table = name;
            for (int i = 0; i < name.Length; i++)
            {
                if (name[i] == "" || name[i] == " ") table[i] = stringOfCharacters + stringOfCharacters1.ToString();
                else { continue; }
            }
            return name = table;
        }

        //wstawia znak jezeli textbox jest pusty
        private string IfTextBoxIsEmpty(string namebox)
        {
            if (namebox == string.Empty || namebox == "" || namebox == "  ") namebox = stringOfCharacters + stringOfCharacters1.ToString();
            else { }
            return namebox;
        }

        private void CheckingCheckbox(CheckBox name, int number, int[] snc)
        {
            if (name.Checked) snc[number - 1] = 1;
            else snc[number - 1] = 0;
        }

        //sprawdza czy wpisany znak jest cyfrą, pomijając puste pola. Jezeli jest błąd zmienna check przyjmuje wartość true;
        bool check = false;
        private void SecuringAmountOffood(RichTextBox name)
        {
            double[] liczba = new double[name.Lines.Length];
            string[] table = new string[name.Lines.Length];

            for (int i = 0; i < name.Lines.Length; i++)
            {
                if (name.Lines[i] == "" || name.Lines[i] == " " || name.Lines[i] == "  " || name.Lines[i] == "   " || name.Lines[i] == "    " || name.Lines[i] == null) continue;
                else if (name.Lines[i] == "0")
                {
                    name.Lines[i] = " ";
                    MessageBox.Show("Nie wolno dzielić przez 0 !!!");

                    string[] kopialiczba = new string[name.Lines.Length];
                    kopialiczba = name.Lines;
                    //check = true;
                    for (int j = 0; j < kopialiczba.Length; j++)
                    {
                        if (kopialiczba[j] == "0")
                            kopialiczba[j] = "";
                    }
                    name.Lines = kopialiczba;
                }
                else if (double.TryParse(name.Lines[i], out liczba[i])) { }
                else
                {
                    MessageBox.Show("W Rubryce składniki i Porcje można wpisywać tylko liczby! ", "Uwaga!!!");
                    string[] kopialiczba = new string[name.Lines.Length];
                    kopialiczba = name.Lines;
                    check = true;
                    for (int j = 0; j < kopialiczba.Length; j++)
                    {
                        if (!(double.TryParse(name.Lines[j], out liczba[j]))) kopialiczba[j] = "";
                        name.Clear();
                        name.Lines = kopialiczba;
                    }
                }

                //    string wzorzec = @"(\d+)";
                //    string wzorzec2 = @"";
                //var MyRegex = new Regex(wzorzec);
                //    var MyRegex2 = new Regex(wzorzec2);
                //if (MyRegex.IsMatch(txtAmountsOfFood.Lines[i])||MyRegex2.IsMatch( txtAmountsOfFood.Lines[i]))
                //{ }
                //else MessageBox.Show("W Rubryce składniki i Porcje można wpisywać tylko liczby! ", "Uwaga!!!");
                //}
            }
        }

        private void zabez()
        {
            if (Convert.ToDouble(rtxtPortion.Text) < 1)
            {
                rtxtPortion.Text = "";
            }
        }

        //zamienia kropkę na przecinek 
        private string ChangeDotToComma(RichTextBox name)
        {
            string[] table = new string[name.Lines.Length];

            for (int j = 0; j < name.Lines.Length; j++)
            {
                table[j] = name.Lines[j].Replace(".", ",");
            }
            name.Lines = table;
            return name.Text;
        }

        //usuwa dodatkowe znaki (stringOfCharacters) po dodaniu czy modyfikacji
        private string SecuringAmountOffood3(RichTextBox name)
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

        #endregion
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            if (titleForm2 == null)
            {
                btnClose.Text = "Anuluj";
                changeColorPbUnblock(panelMain);
                btnDelete.Visible = false;
                btnModify.Visible = false;
                Function.UnblockingFields(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);
                clear = add;
                btnAddRest.Visible = true;
                lblTime.Visible = true;
            }
            else if (unlockFieldsForm2 == "1" && txtName.ReadOnly == true)
            {
                changeColorPbUnblock(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.UnblockingFields(panelMain);
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);
                txtName.Text = titleForm2;
                Function.StartPositionFromZero(txtName);
                rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                rTxtIngredients.Text = ingredientForm2;
                txtShortDescription.Text = ShortDescriptionForm2;
                rtxtDescription.Text = instructionForm2;
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

                if (linkForm2 == dash) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = linkForm2;

                rtxtPortion.Text = NumberOfPortionsForm2.ToString();
                lblCuisine.Text = listOfCuisinesForm2;

                ShowStar(1, pbStar1, idRatingForm2);
                ShowStar(2, pbStar2, idRatingForm2);
                ShowStar(3, pbStar3, idRatingForm2);

                lblLevel.Text = difficultLevelForm2;
                lblTime.Text = executionTimeForm2;

                Function.DisplaySelectionRightPanel(panelRight, IdMealForm2);
                Function.DisplaySelectionRightPanel(panelLeft, ingridientsForm2);
            }
            else
            {
                linkForm22 = linkForm2;
                txtName.Text = titleForm2;

                Function.StartPositionFromZero(txtName);
                if (txtName.SelectedText.Length >= 0) txtName.SelectionStart = 0;

                rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                int i = 0;
                while (rtxtAmountsOfFood.Lines.Length > i)
                {
                    if (rtxtAmountsOfFood.Lines[i].Length >= 5)
                    {
                        rtxtAmountsOfFood.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Horizontal;
                    }
                    i++;
                }

                rTxtIngredients.Text = ingredientForm2;
                txtShortDescription.Text = ShortDescriptionForm2;
                rtxtDescription.Text = instructionForm2;
                Interval();
                if (linkForm2 == dash) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = linkForm2;
                rtxtPortion.Text = NumberOfPortionsForm2.ToString();
                lblCuisine.Text = listOfCuisinesForm2;

                lblLevel.Text = difficultLevelForm2;
                lblTime.Text = executionTimeForm2;

                Function.DisplaySelectionRightPanel(panelRight, IdMealForm2);
                Function.DisplaySelectionRightPanel(panelLeft, ingridientsForm2);
           
                ShowStar(1, pbStar1, idRatingForm2);
                ShowStar(2, pbStar2, idRatingForm2);
                ShowStar(3, pbStar3, idRatingForm2);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            btnCancel.Visible = true;

            if (txtName.ReadOnly == true)
            {
                AssignmentMainFields();
                if (pbStar1.Visible == true)
                {
                    HideStars();
                }
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);
                Function.UnblockingFields(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.ClearFields(panelMain);
                Function.UncheckedCheckBox(panelRight);
                Function.UncheckedCheckBox(panelLeft);
                ClearLabelText();
                pbLittlePhoto.Image = Resources.przepisy;
                clear = add;

                btnAddRest.Visible = true;
                btnModify.Visible = false;
                btnDelete.Visible = false;
                changeColorPbUnblock(panelMain);
                linkForm22 = linkForm2;
            }
            else
            {
                ConvertAmountsOfFood();
                ChangeDotToComma(rtxtAmountsOfFood);
                ChangeDotToComma(rtxtPortion);
                SecuringAmountOffood(rtxtAmountsOfFood);
                SecuringAmountOffood(rtxtPortion);

                if (check == false)
                {
                    txtName.Text = txtName.Text.ToUpper();
                    Function.CheckName(txtName);

                    if (txtName.Text != "")
                    {
                        rtxtAmountsOfFood.Lines = AddStamp(rtxtAmountsOfFood.Lines);
                        rTxtIngredients.Lines = AddStamp(rTxtIngredients.Lines);
                        txtShortDescription.Lines = AddStamp(txtShortDescription.Lines);
                        rtxtDescription.Lines = AddStamp(rtxtDescription.Lines);

                        rtxtAmountsOfFood.Text = IfTextBoxIsEmpty(rtxtAmountsOfFood.Text);
                        rTxtIngredients.Text = IfTextBoxIsEmpty(rTxtIngredients.Text);
                        txtShortDescription.Text = IfTextBoxIsEmpty(txtShortDescription.Text);
                        rtxtDescription.Text = IfTextBoxIsEmpty(rtxtDescription.Text);
                        try
                        {
                            RecipesBase model = new RecipesBase();
                            model.RecipesName = txtName.Text.ToUpper();
                            model.AmountsMeal = rtxtAmountsOfFood.Text;
                            model.Ingredients = rTxtIngredients.Text;
                            model.ShortDescription = txtShortDescription.Text;
                            model.LongDescription = rtxtDescription.Text;

                            if (rtxtPortion.Text == "") model.NumberPortions = 1;
                            else model.NumberPortions = int.Parse(rtxtPortion.Text);

                            model.CategoryCuisines = lblCuisine.Text;
                            model.CategoryRating = idRatingForm2;
                            model.CategoryDifficultLevel = lblLevel.Text;
                            model.CategoryPreparationTime = lblTime.Text;
                            if (linkForm2 == null || linkForm2 == "") linkForm2 = stringOfCharacters.ToString();
                            else model.PhotoLinkLocation = linkForm2;
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
                            #endregion

                            RecipesBase.add(model);
                            idDgGridForm2 = model.Id;

                            rtxtAmountsOfFood.Text = SecuringAmountOffood3(rtxtAmountsOfFood);
                            rTxtIngredients.Text = SecuringAmountOffood3(rTxtIngredients);
                            txtShortDescription.Text = SecuringAmountOffood3(txtShortDescription);
                            rtxtDescription.Text = SecuringAmountOffood3(rtxtDescription);
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
                            changeColorPbblock(panelMain);
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
                    else
                    {
                        MessageBox.Show("Pola nie mogą być puste");
                    }
                }
                check = false;
            }
        }

        #region Przelicz

        public void ConvertAmountsOfFood()
        {
            string[] tab = new string[rtxtAmountsOfFood.Lines.Length];
            tab = rtxtAmountsOfFood.Lines;

            for (int i = 0; i < rtxtAmountsOfFood.Lines.Length; i++)
            {
                if (tab[i] == "1/5") tab[i] = "0,2";
                if (tab[i] == "1/4") tab[i] = "0,25";
                if (tab[i] == "1/3") tab[i] = "0,33";
                if (tab[i] == "1/2") tab[i] = "0,5";
            }
            rtxtAmountsOfFood.Lines = tab;
        }

        private void przeliczPorcjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == false) { MessageBox.Show("Przeliczać porcje można dopiero po dodaniu przepisu lub po wykonaniu jego modyfikacji"); }
            else
            {
                rtxtPortion.ReadOnly = false;
                MessageBox.Show("Wpisz na ile porcji chcesz przeliczyć przepis");
                rtxtPortion.BackColor = Function.CreateBrightColor();
                btnConvert.Visible = true;
                convertNumbers = Convert.ToDouble(rtxtPortion.Text);
            }
        }
        double convertNumbers;

        private void btnConvert_Click(object sender, EventArgs e)
        {
            if (rtxtAmountsOfFood.ReadOnly == true && rtxtPortion.ReadOnly == false)
            {
                int nrLinii = rtxtAmountsOfFood.Lines.Length;
                double[] sk = new double[nrLinii];
                double[] liczba = new double[nrLinii];
                string[] liczba2 = new string[nrLinii];

                SecuringAmountOffood(rtxtPortion);
                if (rtxtPortion.Text == "")
                {
                    rtxtPortion.BackColor = Function.CreateBrightColor();
                    rtxtPortion.ReadOnly = false;
                }
                else
                {
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
            if (rtxtPortion.Text == "") { }
            else
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
                            //if (converted == 0)
                            //{
                            //    MessageBox.Show("Nie moża dzielić przez 0");
                            //    rtxtPortion.Text = NumberOfPortionsForm2.ToString();
                            //    break;
                            //}
                            //else
                            //{
                            numberOne[i] = Math.Round(sk2[i] * converted, 1);
                            numberTwo[i] = numberOne[i].ToString();
                            //  }
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
                //else
                //{
                //    MessageBox.Show("Możesz wpisywać tylko cyfry", "Uwaga!", MessageBoxButtons.OKCancel);
                //    rtxtAmountsOfFood.Text = amountsOfIngredientsForm2;
                //    rtxtPortion.Text = NumberOfPortionsForm2.ToString();
                //}
                btnConvert.Visible = false;
                rtxtPortion.BackColor = Function.CreateColor();
                rtxtPortion.ReadOnly = true;
                rtxtAmountsOfFood.Visible = true;
            }
        }
        #endregion

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
            rTxtIngredients.Paste();
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
            rtxtAmountsOfFood.Paste();
        }

        private void usuńToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtAmountsOfFood.SelectedText = "";
        }

        string[] tekst = new string[9];
        int j = 1;
        string przedrostek = "            ";
        private void separatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string tekst = "             *";

            int i = rtxtDescription.SelectionStart;

            for (int j = 0; j < 6; j++)
            {
                rtxtDescription.Text = rtxtDescription.Text.Insert(i, "     " + tekst + "      ");
            }
            rtxtDescription.Text = rtxtDescription.Text.Insert(i, Environment.NewLine);
            rtxtDescription.SelectionStart = 150 + i;
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
            rtxtDescription.Paste();
        }

        private void usuńToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.SelectedText = "";
        }

        private void włToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmList.Text == "Włącz Listę") cmList.Text = "Wyłącz Listę";
            else cmList.Text = "Włącz Listę";
        }

        private void rtxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            char sign = '•';
            if (cmList.Text == "Wyłącz Listę")
            {
                if (e.KeyCode == Keys.Enter)
                {
                    int i = rtxtDescription.SelectionStart;
                    rtxtDescription.Text = rtxtDescription.Text.Insert(i, "\n " + " " + sign + "     ");
                    e.Handled = true;
                    rtxtDescription.SelectionStart = 6 + i;
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                int i = rtxtDescription.SelectionStart;
                rtxtDescription.Text = rtxtDescription.Text.Insert(i, "\n " + " " + interval + " ");
                e.Handled = true;
                rtxtDescription.SelectionStart = 4 + i;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno usunąć Plik? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
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

        private void btnModify_Click(object sender, EventArgs e)
        {
            clear = "modification";
            btnCancel.Text = "Anuluj";
            if (txtName.ReadOnly == true)
            {
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
                changeColorPbUnblock(panelMain);
            }
            else
            {
                ConvertAmountsOfFood();
                ChangeDotToComma(rtxtAmountsOfFood);
                ChangeDotToComma(rtxtPortion);
                SecuringAmountOffood(rtxtAmountsOfFood);
                SecuringAmountOffood(rtxtPortion);

                rtxtAmountsOfFood.Lines = AddStamp(rtxtAmountsOfFood.Lines);
                rTxtIngredients.Lines = AddStamp(rTxtIngredients.Lines);
                txtShortDescription.Lines = AddStamp(txtShortDescription.Lines);
                rtxtDescription.Lines = AddStamp(rtxtDescription.Lines);

                rtxtAmountsOfFood.Text = IfTextBoxIsEmpty(rtxtAmountsOfFood.Text);
                rTxtIngredients.Text = IfTextBoxIsEmpty(rTxtIngredients.Text);
                txtShortDescription.Text = IfTextBoxIsEmpty(txtShortDescription.Text);
                rtxtDescription.Text = IfTextBoxIsEmpty(rtxtDescription.Text);

                if (check == false)
                {
                    if (txtName.Text == titleForm2) { }
                    else
                    {
                        rtxtAmountsOfFood.Text = SecuringAmountOffood3(rtxtAmountsOfFood);
                        rTxtIngredients.Text = SecuringAmountOffood3(rTxtIngredients);
                        txtShortDescription.Text = SecuringAmountOffood3(txtShortDescription);
                        rtxtDescription.Text = SecuringAmountOffood3(rtxtDescription);
                        Function.CheckName(txtName);

                    }

                    if (txtName.Text != "")
                    {
                        rtxtAmountsOfFood.Lines = AddStamp(rtxtAmountsOfFood.Lines);
                        rTxtIngredients.Lines = AddStamp(rTxtIngredients.Lines);
                        txtShortDescription.Lines = AddStamp(txtShortDescription.Lines);
                        rtxtDescription.Lines = AddStamp(rtxtDescription.Lines);

                        rtxtAmountsOfFood.Text = IfTextBoxIsEmpty(rtxtAmountsOfFood.Text);
                        rTxtIngredients.Text = IfTextBoxIsEmpty(rTxtIngredients.Text);
                        txtShortDescription.Text = IfTextBoxIsEmpty(txtShortDescription.Text);
                        rtxtDescription.Text = IfTextBoxIsEmpty(rtxtDescription.Text);
                        try
                        {
                            #region PanelMain

                            var up = RecipesBase.getById(idDgGridForm2);
                            up.RecipesName = txtName.Text;
                            up.AmountsMeal = rtxtAmountsOfFood.Text;
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
                            up.CategoryDifficultLevel = difficultLevelForm2;
                            if (idRatingForm2 == null) idRatingForm2 = dash;
                            else up.CategoryRating = idRatingForm2;
                            up.CategoryCuisines = lblCuisine.Text;
                            if (linkForm2 == null) linkForm2 = stringOfCharacters.ToString();
                            else up.PhotoLinkLocation = linkForm2;

                            RecipesBase.update(up);

                            rtxtAmountsOfFood.Text = SecuringAmountOffood3(rtxtAmountsOfFood);
                            rTxtIngredients.Text = SecuringAmountOffood3(rTxtIngredients);
                            txtShortDescription.Text = SecuringAmountOffood3(txtShortDescription);
                            rtxtDescription.Text = SecuringAmountOffood3(rtxtDescription);
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
                            changeColorPbblock(panelMain);

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
            }
        }

        private void btnAddRest_Click(object sender, EventArgs e)
        {
            SecuringAmountOffood(rtxtPortion);
            Form3 OpenForm = new Form3();
            if (btnAddRest.Text == addRest) clear = "modification";
            OpenForm.clearForm3 = clear;
            OpenForm.idDgGridForm3 = idDgGridForm2;
            OpenForm.titleForm3 = txtName.Text;
            OpenForm.ingredientForm3 = rTxtIngredients.Text;
            if (rtxtAmountsOfFood.Text == "") { }
            else OpenForm.AmountsOfFoodForm3 = rtxtAmountsOfFood.Text;
            OpenForm.shortDescriptionForm3 = txtShortDescription.Text;
            OpenForm.InstructionForm3 = rtxtDescription.Text;

            if (rtxtPortion.Text == "") { OpenForm.NumberOfPortionsForm3 = 1; }
            else OpenForm.NumberOfPortionsForm3 = int.Parse(rtxtPortion.Text);
            OpenForm.RatingForm3 = idRatingForm2;
            OpenForm.difficultLevelForm3 = difficultLevelForm2;
            OpenForm.executionTimeForm3 = executionTimeForm2;
            OpenForm.photoForm3 = linkForm2;
            OpenForm.listOfCuisinesForm3 = listOfCuisinesForm2;
            OpenForm.LinkForm23 = linkForm22;

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
            this.Visible = false;
            OpenForm.ShowDialog();
            this.Close();
        }

        private StringReader sr = null;
        public string im { get; internal set; }
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font czcionka2 = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

            int wysokoscWiersza2 = (int)czcionka2.GetHeight(e.Graphics);

            int iloscLinii2 = e.MarginBounds.Height / wysokoscWiersza2;

            if (sr == null)
            {
                string tekst = "";
                string tekst1 = "";
                string tekst2 = "";

                //for (int i = 0; i < rtxtAmountsOfFood.Lines.Length; i++)
                //{
                //    float szerokosc = e.Graphics.MeasureString(170 + rTxtIngredients.Lines[i], czcionka2).Width;
                //    if (szerokosc < e.MarginBounds.Width)
                //    {

                //        if (rtxtAmountsOfFood.Lines[i].Length >= 4)
                //            tekst1 += rtxtAmountsOfFood.Lines[i] + "  " + rTxtIngredients.Lines[i] + "\n";
                //        else if (rtxtAmountsOfFood.Lines[i].Length == 3)
                //            tekst1 += rtxtAmountsOfFood.Lines[i] + "   " + rTxtIngredients.Lines[i] + "\n";
                //        else if (rtxtAmountsOfFood.Lines[i].Length == 2) tekst1 += rtxtAmountsOfFood.Lines[i] + "      " + rTxtIngredients.Lines[i] + "\n";
                //        else if (rtxtAmountsOfFood.Lines[i].Length == 1) tekst1 += rtxtAmountsOfFood.Lines[i] + "         " + rTxtIngredients.Lines[i] + "\n";
                //        else tekst1 += rtxtAmountsOfFood.Lines[i] + "         " + rTxtIngredients.Lines[i] + "\n";
                //    }
                //    else
                //    {
                //        float sredniaSerokoscLitery = szerokosc / rTxtIngredients.Lines.Length;
                //        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                //        string skracanyWiersz = rtxtAmountsOfFood.Lines[i] + rTxtIngredients.Lines[i];
                //        do
                //        {
                //            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                //            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                //            tekst1 += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                //            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                //        } while (skracanyWiersz.Length > ileLiterWWierszu);
                //        tekst1 += skracanyWiersz + "\n";
                //    }
                //}

                for (int i = 0; i < tablica.Length; i++)
                {
                    float szerokosc = e.Graphics.MeasureString(tablica[i], czcionka2).Width;
                    if (szerokosc < e.MarginBounds.Width)
                    {
                        if (i % 2 == 0) tekst1 += tablica[i];
                        else tekst1 += tablica[i] + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / rTxtIngredients.Lines.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = tablica[i];
                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            tekst1 += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);
                        tekst1 += skracanyWiersz + "\n";
                    }
                }

                foreach (string wiersz in txtName.Lines)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;
                    if (szerokosc < e.MarginBounds.Width)
                    {
                        tekst += wiersz + "\n";
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
                            tekst += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);
                        tekst += skracanyWiersz + "\n";
                    }
                }

                foreach (string wiersz in rtxtDescription.Lines)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;
                    if (szerokosc < e.MarginBounds.Width)
                    {
                        tekst2 += wiersz + "\n";
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
                            tekst2 += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);
                        tekst2 += skracanyWiersz + "\n";
                    }
                }
                sr = new StringReader(tekst + "\n\n" + tekst1 + "\n\n" + tekst2);
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
        string[] tablica;
        private void podglToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tablica = new string[rtxtAmountsOfFood.Lines.Length + rTxtIngredients.Lines.Length + 2];

            int k = 0;
            int p = 0;
            for (int i = 0; i < rtxtAmountsOfFood.Lines.Length * 2; i = i + 2)
            {
                if (rtxtAmountsOfFood.Lines[k] == string.Empty) { tablica[i] = ""; }
                else tablica[i] = rtxtAmountsOfFood.Lines[k];

                
                if (tablica[i].Length == 1) tablica[i] = tablica[i] + "     ";
                else if (tablica[i].Length == 2) tablica[i] = tablica[i] + "    ";
                else if (tablica[i].Length == 3) tablica[i] = tablica[i] + "   ";
                else if (tablica[i].Length == 4) tablica[i] = tablica[i] + "  ";
                k++;
            }
            for (int j = 1; j < rTxtIngredients.Lines.Length * 2; j = j + 2)
            {
                if (rTxtIngredients.Lines[p] == "") { tablica[j] = ""; }
                else tablica[j] = rTxtIngredients.Lines[p];
                p++;
            }
            printPreviewDialog1.ShowDialog();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font czcionka2 = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));

            int wysokoscWiersza2 = (int)czcionka2.GetHeight(e.Graphics);
            int iloscLinii2 = e.MarginBounds.Height / wysokoscWiersza2;

            if (sr == null)
            {
                string tekst = "";
                string tekst1 = "";

                for (int i = 0; i < tablica.Length; i++)
                {
                    float szerokosc = e.Graphics.MeasureString(tablica[i], czcionka2).Width;
                    if (szerokosc < e.MarginBounds.Width)
                    {
                        if (i % 2 == 0) tekst1 += tablica[i];
                        else tekst1 +=  tablica[i] + "\n";
                    }
                    else
                    {
                        float sredniaSerokoscLitery = szerokosc / rTxtIngredients.Lines.Length;
                        int ileLiterWWierszu = (int)(e.MarginBounds.Width / sredniaSerokoscLitery);
                        string skracanyWiersz = tablica[i];
                        do
                        {
                            int ostatniaSpacja = skracanyWiersz.Substring(0, ileLiterWWierszu).LastIndexOf(' ');
                            int iloscLiter = ostatniaSpacja != -1 ? Math.Min(ostatniaSpacja, ileLiterWWierszu) : ileLiterWWierszu;
                            tekst1 += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);
                        tekst1 += skracanyWiersz + "\n";
                    }
                }
                foreach (string wiersz in txtName.Lines)
                {
                    float szerokosc = e.Graphics.MeasureString(wiersz, czcionka2).Width;
                    if (szerokosc < e.MarginBounds.Width)
                    {
                        tekst += wiersz + "\n";
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
                            tekst += skracanyWiersz.Substring(0, iloscLiter) + "\n";
                            skracanyWiersz = skracanyWiersz.Substring(iloscLiter).TrimStart(' ');
                        } while (skracanyWiersz.Length > ileLiterWWierszu);
                        tekst += skracanyWiersz + "\n";
                    }
                }

                sr = new StringReader(tekst + "\n\n" + tekst1);
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

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
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

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.counter = counterForm2;
            form1.ShowDialog();
        }

        //zmienne pamięciowe- Anuluj//
        public string title1, amounts, ingrediet, shortDes, longDes, cuisines, level, time, rating;
        public int portions;
        private void AssignmentMainFields()
        {
            cancel = true;
            title1 = txtName.Text;
            amounts = rtxtAmountsOfFood.Text;
            ingrediet = rTxtIngredients.Text;
            shortDes = txtShortDescription.Text;
            longDes = rtxtDescription.Text;
            if (rtxtPortion.Text == "") portions = 1;
            else portions = int.Parse(rtxtPortion.Text);
            cuisines = lblCuisine.Text;
            level = lblLevel.Text;
            time = lblTime.Text;
            rating = idRatingForm2;
        }
        public string linkForm22;
        private void btnCancel_Click(object sender, EventArgs e)
        {

            if (cancel == true)
            {
                txtName.Text = title1;
                rtxtAmountsOfFood.Text = amounts;
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
            }
            Function.ColorFieldsAfterBlocking(panelMain, rtxtPortion);
            Function.BlockingFields(panelMain);
            Function.BlockCheckbox(panelLeft);
            Function.BlockCheckbox(panelRight);
            btnAddRest.Visible = false;
            changeColorPbblock(panelMain);
            btnCancel.Visible = false;
            btnAdd.Visible = true;
            btnDelete.Visible = true;
            btnModify.Visible = true;
            pbLittlePhoto.ImageLocation = linkForm22;

            cancel = false;
        }

        #region CheckboxMeal
        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcSnack, 1, IdMealForm2);
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcDinner, 2, IdMealForm2);
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcSoup, 3, IdMealForm2);
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcDessert, 4, IdMealForm2);
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcDrink, 5, IdMealForm2);
        }

        private void rTxtIngredients_KeyDown(object sender, KeyEventArgs e)
        {
            int start = rTxtIngredients.SelectionStart;
            if (e.KeyCode == Keys.Enter && rTxtIngredients.Lines.Length >= lenght)
            {
                MessageBox.Show("Program nie może mieć więcej niż linii");
                string[] proba = rTxtIngredients.Lines;
                proba[lenght-1] = null;
                rTxtIngredients.Lines = proba;
                e.Handled = true;
                rTxtIngredients.SelectionStart = start ;
            }
            else { }
        }
        int lenght = 38;
        private void rtxtAmountsOfFood_KeyDown(object sender, KeyEventArgs e)
        {
            
            int start = rtxtAmountsOfFood.SelectionStart;
            if (e.KeyCode == Keys.Enter && rtxtAmountsOfFood.Lines.Length >= lenght)
            {
                MessageBox.Show("Program nie może mieć więcej niż linii");
                string[] copyLines = rtxtAmountsOfFood.Lines;
                copyLines[lenght-1] = null;
                rtxtAmountsOfFood.Lines = copyLines;
                e.Handled = true;
                rtxtAmountsOfFood.SelectionStart = start;
            }
            else { }
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcPreserves, 6, IdMealForm2);
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcSalad, 7, IdMealForm2);
        }
        #endregion

        #region CheckBoxComponent
        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcFish, 1, ingridientsForm2);
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcPasta, 2, ingridientsForm2);
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcFruits, 3, ingridientsForm2);
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcMuschrooms, 4, ingridientsForm2);
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcBird, 5, ingridientsForm2);
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcMeat, 6, ingridientsForm2);
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcEggs, 7, ingridientsForm2);
        }
        #endregion
    }
}
