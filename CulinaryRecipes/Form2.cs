using Common;
using CulinaryRecipes.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public bool addRecipeForm2;
        public bool addRecipe;


        #region Funkcje

        //Czysci Labele i wstawia myślniki
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


        // Funkcja zbiorcza dla changeColorPbblock, changeColorPbUnblock
        //private void Color(Control set, Color color)
        //{
        //    foreach (Control c in set.Controls)
        //    {
        //        if (c is PictureBox)
        //        {
        //            if (c.Name == pbLittlePhoto.Name || c.Name == pbStar1.Name || c.Name == pbStar2.Name || c.Name == pbStar3.Name || c.Name == pictureBox2.Name) continue;
        //            ((PictureBox)c).BackColor = color;
        //        }
        //    }
        //}

        private void Color(Control set, Color color)
        {
            foreach (Control c in set.Controls)
            {
                int i = 0;
                if (c is PictureBox)
                {
                    if (c.Name == Star(panelPicture)) continue;
                    else ((PictureBox)c).BackColor = color;
                }
            }
        }
        ////funkcja usprawniająca do funkcji Color
        private string Star(Control set)
        {
            //  List<string> cos = new List<string>();
            string cos = string.Empty;
            foreach (Control c in set.Controls)
            {
                if (c is PictureBox)
                    // cos.Add(c.Name);
                    cos = c.Name;
            }
            return cos;
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
        private void HideStars()
        {
            pbStar1.Visible = false;
            pbStar2.Visible = false;
            pbStar3.Visible = false;
        }

        //wstawia znak jezeli textbox jest pusty
        private string IfTextBoxIsEmpty(string namebox)
        {
            if (string.IsNullOrWhiteSpace(namebox)) namebox = stringOfCharacters + stringOfCharacters1.ToString();
            else { }
            return namebox;
        }

        //sprawdza czy checkbox jest zaznaczony (1) czy nie (0)
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
                if (string.IsNullOrWhiteSpace(name.Lines[i])) continue;
                else if (name.Lines[i] == "0")
                {
                    name.Lines[i] = " ";
                    MessageBox.Show("Nie wolno dzielić przez 0 !!!");

                    string[] kopialiczba = new string[name.Lines.Length];
                    kopialiczba = name.Lines;

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

        //dodaje znak w pustym polu (zapobiegając przesuwaniu się do góry) stringOfCharacters
        private string[] AddStamp(string[] name)
        {
            string[] table = new string[name.Length];
            table = name;

            for (int i = 0; i < name.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(name[i])) table[i] = stringOfCharacters + stringOfCharacters1.ToString();
                else { continue; }
            }

            return name = table;
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

        //Funkcja pamięciowa (chceckboxy) będą zaznaczone jak zamknie się formę 2 a otworzy 1
        public void RememberCheckBox(Form1 name)
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
        private void ChangeColorToRed()
        {
            CMAmountsEnter.ForeColor = System.Drawing.Color.Red;
            CMGramsEnter.ForeColor = System.Drawing.Color.Red;
            CMIngridientsEnter.ForeColor = System.Drawing.Color.Red;
        }

        private void ChangeColorToGreen()
        {
            CMAmountsEnter.ForeColor = System.Drawing.Color.Green;
            CMGramsEnter.ForeColor = System.Drawing.Color.Green;
            CMIngridientsEnter.ForeColor = System.Drawing.Color.Green;
        }

        private void ChangeNameEnterInMeunuStrip()
        {
            if (addRecipe == true)
            {
                CMAmountsEnter.Text = enterOff;
                CMGramsEnter.Text = enterOff;
                CMIngridientsEnter.Text = enterOff;

                ChangeColorToRed();
            }
            else
            {
                CMAmountsEnter.Text = enterOn;
                CMGramsEnter.Text = enterOn;
                CMIngridientsEnter.Text = enterOn;

                ChangeColorToGreen();
            }

        }

        private void Form2_Load(object sender, EventArgs e)
        {

            if (titleForm2 == null)
            {
                ContextMenuBlock();
                ChangeNameEnterInMeunuStrip();

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
                addRecipeForm2 = true;

                chcVegetarian.Enabled = true;

            }
            else if (unlockFieldsForm2 == "1" && txtName.ReadOnly == true)
            {
                ContextMenuBlock();
                ChangeNameEnterInMeunuStrip();


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
            }
            else
            {
                ChangeNameEnterInMeunuStrip();


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

                SecuringBlock();
                Interval();
                Function.UncheckText(txtName);
                pb2.BringToFront();

            }
        }


        public RecipesBase Model(RecipesBase model)
        {
            if (rtxtPortion.Text == "") model.NumberPortions = 1;
            else model.NumberPortions = int.Parse(rtxtPortion.Text);
            return model;
        }

        //Dodaj do bazy danych
        private void AddRecipes()
        {
            btnCancel.Visible = true;
            AssignmentMainFields();

            if (txtName.ReadOnly == true)
            {
                chcVegetarian.Enabled = true;

                if (pbStar1.Visible == true)
                {
                    HideStars();
                }

                ContextMenuBlock();
                Function.UnblockCheckbox(panelLeft);
                Function.UnblockCheckbox(panelRight);
                Function.UnblockingFields(panelMain);
                Function.ColorAreaAfterUnblocking(panelMain);
                Function.ClearFields(panelMain);

                WinForm.UncheckedCheckBox(panelRight);
                WinForm.UncheckedCheckBox(panelLeft);

                ClearLabelText();
                pbLittlePhoto.Image = Resources.przepisy;
                linkForm2 = stringOfCharacters.ToString();
                clear = add;

                btnAddRest.Visible = true;
                btnModify.Visible = false;
                btnDelete.Visible = false;

                Color(panelMain, Function.CreateBrightColor());

                linkForm22 = linkForm2;
                addRecipeForm2 = false;
                addRecipe = true;
            }
            else
            {
                numberLine = 0;
                ConvertAmountsOfFood();
                ChangeDotToComma(rtxtAmountsOfFood);
                ChangeDotToComma(rtxtPortion);
                SecuringAmountOffood(rtxtAmountsOfFood);
                SecuringAmountOffood(rtxtPortion);

                if (Function.CheckName(txtName) == false)
                {
                    if (check == false)
                    {
                        txtName.Text = txtName.Text.ToUpper();

                        if (txtName.Text != "")
                        {
                            AddStampBlock();
                            IfTextBoxIsEmptyBlock();

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

                                chcVegetarian.Enabled = false;

                                if (string.IsNullOrEmpty(rtxtPortion.Text))
                                {
                                    rtxtPortion.Text = 1.ToString();
                                }

                                titleForm2 = txtName.Text;
                                idDgGridForm2 = model.Id;
                                SecuringBlock();

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
                                addRecipeForm2 = true;
                                addRecipe = false;
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
                            MessageBox.Show("Nazwa nie może być pusta");
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

        #region Przelicz

        //konwertuje na liczby
        public void ConvertAmountsOfFood()
        {
            string[] tab = new string[rtxtAmountsOfFood.Lines.Length];
            tab = rtxtAmountsOfFood.Lines;

            for (int i = 0; i < tab.Length; i++)
            {
                tab[i] = tab[i].Trim();
            }

            for (int i = 0; i < tab.Length; i++)
            {
                if (tab[i] == "1/1")
                {
                    tab[i] = "1";
                }
                else if (tab[i] == "1/5")
                {
                    tab[i] = "0,2";
                }
                else if (tab[i] == "1/4")
                {
                    tab[i] = "0,25";
                }
                else if (tab[i] == "1/3")
                {
                    tab[i] = "0,35";
                }
                else if (tab[i] == "1/2")
                {
                    tab[i] = "0,5";
                }
                else if (tab[i] == "3/4")
                {
                    tab[i] = "0,75";
                }
                else if (tab[i] == "1/2")
                {
                    tab[i] = "0,5";
                }
                else if (tab[i] == "2/3")
                {
                    tab[i] = "0,65";
                }
                else if (tab[i] == "2/4")
                {
                    tab[i] = "0,5";
                }
                else if (tab[i] == "2/5")
                {
                    tab[i] = "0,6";
                }
                else if (tab[i] == "4/5")
                {
                    tab[i] = "0,8";
                }
            }

            rtxtAmountsOfFood.Lines = tab;
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
                    if (rtxtPortion.Text != "")
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
        private void btnConvert_Click(object sender, EventArgs e)
        {
            ConvertFunction();
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

            OtherEnter.AlignTheNumberOfLines(rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);
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
            OtherEnter.AlignTheNumberOfLines(rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);
        }

        private void usuńToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            rtxtAmountsOfFood.SelectedText = "";
        }

        private void Separator(RichTextBox name, int quantityStar)
        {
            string tekst = "           *";
            string prefix = "           ";
            int i = name.SelectionStart;

            for (int j = 0; j < quantityStar; j++)
            {
                name.Text = name.Text.Insert(i, prefix + tekst + "      ");
            }

            name.Text = name.Text.Insert(i, "\n");
            name.SelectionStart = name.TextLength;

            i = name.SelectionStart;
            name.Text = name.Text.Insert(i, "\n " + " " + interval + " ");
            name.SelectionStart = 4 + i;
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

        private void kopiujToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.Copy();
        }

        private void wklejToolStripMenuItem4_Click(object sender, EventArgs e)
        {

            rtxtDescription.Paste();
            // ta funkcję zostawić 

            //if (Clipboard.ContainsText(TextDataFormat.Text))
            //{
            //    string clipboardText = Clipboard.GetText(TextDataFormat.Text); 
            //    txtName.Text = clipboardText.Length.ToString();
            //}

        }

        private void usuńToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            rtxtDescription.SelectedText = "";
        }

        private void Punktor(KeyEventArgs e)
        {
            char sign = '•';

            int i = rtxtDescription.SelectionStart;
            rtxtDescription.Text = rtxtDescription.Text.Insert(i, Environment.NewLine + " " + sign + "       ");
            e.Handled = true;
            rtxtDescription.SelectionStart = 6 + i;
        }

        private void włToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (cmList.Text == PunktorOn)
            {
                cmList.Text = PunktorOff;
            }
            else cmList.Text = PunktorOn;

        }
        string PunktorOn = "Punktor Wł";
        string PunktorOff = "Punktor Wył";

        private void rtxtDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {
                UndoChanges();
            }

            if (e.KeyCode == Keys.Enter && cmList.Text == PunktorOn)
            {
                int i = rtxtDescription.SelectionStart;
                rtxtDescription.Text = rtxtDescription.Text.Insert(i, "\n " + " " + interval + " ");
                e.Handled = true;
                rtxtDescription.SelectionStart = 4 + i;
            }

            if (e.KeyCode == Keys.Enter && cmList.Text == PunktorOff)
            {
                Punktor(e);
            }
        }

        private void DeleteRecipes()
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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteRecipes();
        }
        //funkcja- blok- przypisanie jednej funkcji do kilku textboxów
        private void SecuringBlock()
        {
            rtxtAmountsOfFood.Text = SecuringAmountOffood3(rtxtAmountsOfFood);
            rTxtGrams.Text = SecuringAmountOffood3(rTxtGrams);
            rTxtIngredients.Text = SecuringAmountOffood3(rTxtIngredients);
            txtShortDescription.Text = SecuringAmountOffood3(txtShortDescription);
            rtxtDescription.Text = SecuringAmountOffood3(rtxtDescription);
        }
        //to samo
        private void IfTextBoxIsEmptyBlock()
        {
            rtxtAmountsOfFood.Text = IfTextBoxIsEmpty(rtxtAmountsOfFood.Text);
            rTxtGrams.Text = IfTextBoxIsEmpty(rTxtGrams.Text);
            rTxtIngredients.Text = IfTextBoxIsEmpty(rTxtIngredients.Text);
            txtShortDescription.Text = IfTextBoxIsEmpty(txtShortDescription.Text);
            rtxtDescription.Text = IfTextBoxIsEmpty(rtxtDescription.Text);
        }
        // to samo
        private void AddStampBlock()
        {
            rtxtAmountsOfFood.Lines = AddStamp(rtxtAmountsOfFood.Lines);
            rTxtGrams.Lines = AddStamp(rTxtGrams.Lines);
            rTxtIngredients.Lines = AddStamp(rTxtIngredients.Lines);
            txtShortDescription.Lines = AddStamp(txtShortDescription.Lines);
            rtxtDescription.Lines = AddStamp(rtxtDescription.Lines);
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

                addRecipeForm2 = true;
                rTxtIngredients.Focus();
                rTxtIngredients.SelectionStart = rTxtIngredients.TextLength;
                IndexChar(rTxtIngredients);
                maxLine = numberLine;

                rtxtAmountsOfFood.Text = rtxtAmountsOfFood.Text.TrimEnd();
                rTxtGrams.Text = rTxtGrams.Text.TrimEnd();
                rTxtIngredients.Text = rTxtIngredients.Text.TrimEnd();

                OtherEnter.AlignTheNumberOfLines(rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);

                if (!string.IsNullOrEmpty(rtxtAmountsOfFood.Text) && !string.IsNullOrEmpty(rTxtGrams.Text) && !string.IsNullOrEmpty(rTxtIngredients.Text))
                {
                    OtherEnter.SetFocus(rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);
                    OtherEnter.SetFocus(rTxtGrams, rtxtAmountsOfFood, rTxtIngredients);
                    OtherEnter.SetFocus(rTxtIngredients, rtxtAmountsOfFood, rTxtGrams);
                }
            }
            else
            {
                ContextMenuBlock();
                ConvertAmountsOfFood();
                ChangeDotToComma(rtxtAmountsOfFood);
                ChangeDotToComma(rtxtPortion);
                SecuringAmountOffood(rtxtAmountsOfFood);
                SecuringAmountOffood(rtxtPortion);

                AddStampBlock();
                IfTextBoxIsEmptyBlock();

                if (check == false)
                {
                    txtName.Text = txtName.Text.ToUpper();
                    if (txtName.Text == titleForm2) { }
                    else if (txtName.Text != titleForm2)
                    {
                        Function.CheckName(txtName);
                        SecuringBlock();
                    }
                    else
                    {
                        SecuringBlock();
                    }

                    if (txtName.Text != "")
                    {
                        AddStampBlock();
                        IfTextBoxIsEmptyBlock();
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
                            SecuringBlock();
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
                SecuringBlock();

            }

        }

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


        private void btnModify_Click(object sender, EventArgs e)
        {

            ModifyRecipes();
        }

        private void btnAddRest_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
            SecuringAmountOffood(rtxtPortion);
            if (btnAddRest.Text == addRest) clear = "modification";

            Form3 OpenForm = new Form3();
            OpenForm.clearForm3 = clear;
            OpenForm.idDgGridForm3 = idDgGridForm2;
            OpenForm.titleForm3 = txtName.Text;
            OpenForm.ingredientForm3 = rTxtIngredients.Text;
            OpenForm.gramsForm3 = rTxtGrams.Text;

            if (rtxtAmountsOfFood.Text == "") { }
            else
            {
                OpenForm.AmountsOfFoodForm3 = rtxtAmountsOfFood.Text;
            }

            OpenForm.shortDescriptionForm3 = txtShortDescription.Text;
            OpenForm.InstructionForm3 = rtxtDescription.Text;

            if (rtxtPortion.Text == "")
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
            OpenForm.addRecipeForm3 = addRecipeForm2;
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
        //string[] tablelka;
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
            this.Hide();
            Form1 form1 = new Form1();
            RememberCheckBox(form1);
            form1.ShowDialog();
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font czcionka2 = new System.Drawing.Font("Courier New", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
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

            RememberCheckBox(form1);
            form1.seekUnsubscribe = seekUnsubscribeForm2;

            form1.counter = counterForm2;
            form1.ShowDialog();
        }

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
            if (rtxtPortion.Text == "") portions = 1;
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

        public string linkForm22;

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
            CMGramsEnter.Text = enterOn;
            CMAmountsEnter.Text = enterOn;
            CMIngridientsEnter.Text = enterOn;

            cancel = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            UndoChanges();
        }

        #region CheckboxMeal
        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcSnack, 1, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcDinner, 2, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcSoup, 3, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcDessert, 4, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcDrink, 5, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }


        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcPreserves, 6, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcSalad, 7, IdMealForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

      





        private void chcVegetarian_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcVegetarian, 8, ingridientsForm2);
            if (chcVegetarian.Checked)
            {
                chcBird.Checked = false;
                chcMeat.Checked = false;
                chcVegetarian.Checked = true;
            }
            txtName.Text = txtName.Text.ToUpper();
        }

        private void btnConvert_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnConvert, "Przelicza przepis na wybraną ilość osób");
        }

        private void txtShortDescription_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(txtShortDescription, "Streszczenie przepisu");
        }

        private void rtxtAmountsOfFood_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(rtxtAmountsOfFood, "Ilości składników - tylko i wyłącznie Liczby można wpisywać");
        }

        private void btnAddRest_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(btnAddRest, "Dodaj - Zdjęcie, czas przygotowania,\n stopień trudności i rodzaj kuchni");
        }

        private void rtxtAmountsOfFood_SelectionChanged(object sender, EventArgs e)
        {
            IndexChar(rtxtAmountsOfFood);
        }

        private void IndexChar(RichTextBox name)
        {
            int index = name.SelectionStart;
            numberLine = name.GetLineFromCharIndex(index);

            if (numberLine >= maxLine) maxLine = numberLine;
            else addRecipeForm2 = false;
        }

        private void rTxtGrams_SelectionChanged(object sender, EventArgs e)
        {
            IndexChar(rTxtGrams);
        }

        private void rTxtIngredients_SelectionChanged(object sender, EventArgs e)
        {
            IndexChar(rTxtIngredients);
        }


        string enterOn = "ENTER ON";
        string enterOff = "ENTER OFF";
        //Przelaczenie między Enterem a przeskakiwaniem miedzy polami
        private void ContextEnter(ToolStripMenuItem first, ToolStripMenuItem second, ToolStripMenuItem third)
        {

            if (first.Text == enterOn)
            {
                addRecipe = true;

                first.Text = enterOff;
                second.Text = enterOff;
                third.Text = enterOff;

                first.ForeColor = System.Drawing.Color.Red;
                second.ForeColor = System.Drawing.Color.Red;
                third.ForeColor = System.Drawing.Color.Red;

            }
            else
            {
                addRecipe = false;

                first.Text = enterOn;
                second.Text = enterOn;
                third.Text = enterOn;

                first.ForeColor = System.Drawing.Color.Green;
                second.ForeColor = System.Drawing.Color.Green;
                third.ForeColor = System.Drawing.Color.Green;
            }
        }

        //private void Enter(KeyEventArgs e, RichTextBox RichName)
        //{
        //    int i = RichName.SelectionStart;
        //    RichName.Text = RichName.Text.Insert(i, "\n" + "");
        //    e.Handled = true;
        //    RichName.SelectionStart = i + 1;
        //}

        //private void BlockRightButton(ToolStripMenuItem name)
        //{
        //    if (txtName.ReadOnly)
        //    {
        //        name.Enabled = false;
        //    }

        //}

        private void eNTERToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ContextEnter(CMAmountsEnter, CMGramsEnter, CMIngridientsEnter);
        }

        private void CMGramsEnter_Click(object sender, EventArgs e)
        {
            ContextEnter(CMGramsEnter, CMAmountsEnter, CMIngridientsEnter);
        }

        private void CMIngridientsEnter_Click(object sender, EventArgs e)
        {
            ContextEnter(CMIngridientsEnter, CMAmountsEnter, CMGramsEnter);
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
            rTxtGrams.Paste();
            OtherEnter.AlignTheNumberOfLines(rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);
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

                OtherEnter.NewLine(rtxtAmountsOfFood);
                OtherEnter.NewLine(rtxtAmountsOfFood);
                OtherEnter.NewLine(rTxtGrams);

                Separator(rTxtIngredients, 3);

                OtherEnter.NewLine(rtxtAmountsOfFood);
                OtherEnter.NewLine(rTxtGrams);
                rTxtIngredients.Focus();
            }

        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                rtxtPortion.Focus();
                txtName.Text = txtName.Text.ToUpper();
            }
            if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {
                UndoChanges();
            }

        }

        private void rtxtPortion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {
                UndoChanges();
            }

            if (e.KeyCode == Keys.Enter && txtName.ReadOnly == true && rtxtPortion.ReadOnly == false)
            {
                ConvertFunction();
            }
            else if (e.KeyCode == Keys.Enter && txtName.ReadOnly == false)
            {
                rtxtAmountsOfFood.Focus();
            }

        }

        private void txtShortDescription_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {
                UndoChanges();
            }
        }


        public String SwapClipboardHtmlText(String replacementHtmlText)
        {
            String returnHtmlText = null;
            if (Clipboard.ContainsText(TextDataFormat.Html))
            {
                returnHtmlText = Clipboard.GetText(TextDataFormat.Html);
                Clipboard.SetText(replacementHtmlText, TextDataFormat.Html);
            }
            return returnHtmlText;
        }

        private void rtxtDescription_SelectionChanged(object sender, EventArgs e)
        {
            IndexChar(rtxtDescription);
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

        private void rtxtAmountsOfFood_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }

        private void rtxtPortion_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }

        private void rTxtGrams_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }

        private void rTxtIngredients_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }

        private void txtShortDescription_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }

        private void rtxtDescription_Click(object sender, EventArgs e)
        {
            txtName.Text = txtName.Text.ToUpper();
        }


        #endregion

        #region rtxtAmountsOfFood_rtxtIngredients


        //zliczanie znaków i "\n" zeby odpowiednio ustawić focus
        OtherEnter Oe = new OtherEnter();
        int numberLine;

        private int IloscZnakow(RichTextBox second)
        {
            int numberLinePomocnicza;
            int iloscZnakow;

            if (0 == numberLine) numberLinePomocnicza = -1;
            else numberLinePomocnicza = 0;
            if (second == rtxtAmountsOfFood)
            {
                numberLine++;
            }

            iloscZnakow = 0;
            string tekst = string.Empty;

            foreach (char item in second.Text)
            {
                if (numberLinePomocnicza == numberLine) break;

                else if (item == '\n')
                {
                    if (numberLinePomocnicza == -1)
                    {
                        numberLinePomocnicza++;
                    }
                    else
                    {
                        iloscZnakow++;
                        numberLinePomocnicza++;
                    }
                }
                else
                {
                    tekst += item;
                    iloscZnakow++;
                }
            }

            return iloscZnakow;
        }

        //podobna funkcja jak popzednia z małą modyfikacją
        private int IloscZnakow2(RichTextBox name)
        {
            int numberLinePomocnicza;
            int iloscZnakow;

            if (0 == numberLine) numberLinePomocnicza = -1;
            else numberLinePomocnicza = 0;


            iloscZnakow = 0;
            string tekst = string.Empty;

            foreach (char item in name.Text)
            {
                if (numberLinePomocnicza == numberLine) break;

                else if (item == '\n')
                {
                    if (numberLinePomocnicza == -1)
                    {
                        numberLinePomocnicza++;
                    }
                    else
                    {
                        iloscZnakow++;
                        numberLinePomocnicza++;
                    }
                }
                else
                {
                    tekst += item;
                    iloscZnakow++;
                }
            }

            return iloscZnakow;
        }

        // 
        private void ChangeFocusNewProject(RichTextBox first, RichTextBox second, KeyEventArgs e)
        {
            if (addRecipeForm2 == true)
            {
                OtherEnter.ForTheFirstLine(first, second, e);
            }
            else if (e.KeyCode == Keys.Enter && addRecipeForm2 == false && addRecipe == true)
            {
                second.Focus();
                e.Handled = true;

                second.SelectionStart = IloscZnakow(second);
            }
        }

        //maksymalna ilość linii w formach
        int lenght = 38;
        private void NumberOfLines(KeyEventArgs e, RichTextBox _name)
        {
            int start = _name.SelectionStart;

            if (e.KeyCode == Keys.Enter && _name.Lines.Length >= lenght)
            {
                MessageBox.Show("Program nie może mieć więcej już linii");
                string[] proba = _name.Lines;
                proba[lenght - 1] = null;
                _name.Lines = proba;
                e.Handled = true;
                _name.SelectionStart = start;
            }
        }

        //Najwieksza linia
        private void MaxLineIncrease()
        {
            if (maxLine <= numberLine)
            {
                addRecipeForm2 = true;
            }
            else
            {
                addRecipeForm2 = false;
            }
        }

        //Zmienia dodawanie Linii w zaleznosci od pozycji kursora 
        public void ChangeAddLine(KeyEventArgs e)
        {
            if (numberLine < maxLine) ChangeFocusNewProject(rTxtIngredients, rtxtAmountsOfFood, e);
            else
            {
                OtherEnter.NewLine(rtxtAmountsOfFood);
                OtherEnter.NewLine(rTxtGrams);
                OtherEnter.NewLine(rTxtIngredients);

                int i = rtxtAmountsOfFood.Text.Length;
                rtxtAmountsOfFood.Focus();
                e.Handled = true;
                rtxtAmountsOfFood.SelectionStart = i - 1;

                addRecipeForm2 = false;
            }
        }

        private void AmountsAndGramsKeyDown(KeyEventArgs e, RichTextBox first, RichTextBox second, RichTextBox third)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (CMIngridientsEnter.Text == enterOn)
                {
                    Oe.ClassicEnterPlusNewLine(e, first, second, third);
                }
                else
                {
                    NumberOfLines(e, first);
                    ChangeFocusNewProject(first, second, e);
                }
            }
            else if(e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {
                UndoChanges();
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pbLittlePhoto_Click(object sender, EventArgs e)
        {

        }

        private void rtxtAmountsOfFood_KeyDown(object sender, KeyEventArgs e)
        {

            AmountsAndGramsKeyDown(e, rtxtAmountsOfFood, rTxtGrams, rTxtIngredients);

        }

        private void rTxtGrams_KeyDown(object sender, KeyEventArgs e)
        {

            AmountsAndGramsKeyDown(e, rTxtGrams, rTxtIngredients, rtxtAmountsOfFood);

        }

        private void rTxtIngredients_KeyDown(object sender, KeyEventArgs e)
        {
           
            if (e.KeyCode == Keys.Enter)
            {
                NumberOfLines(e, rTxtIngredients);

                MaxLineIncrease();

                if (CMIngridientsEnter.Text == enterOn)
                {

                    Oe.ClassicEnterPlusNewLine(e, rTxtIngredients, rtxtAmountsOfFood, rTxtGrams);

                }
                else if (CMIngridientsEnter.Text == enterOff)
                {

                    ChangeAddLine(e);

                }
                else
                {

                    ChangeFocusNewProject(rTxtIngredients, rtxtAmountsOfFood, e);

                }
            }
           
 else if (e.KeyCode == Keys.Escape && txtName.ReadOnly == false)
            {

                UndoChanges();

            }
        }

        #endregion

        #region CheckBoxComponent
        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcFish, 1, ingridientsForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcPasta, 2, ingridientsForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcFruits, 3, ingridientsForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcMuschrooms, 4, ingridientsForm2);
            txtName.Text = txtName.Text.ToUpper();
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcBird, 5, ingridientsForm2);
            chcVegetarian.Checked = false;
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcMeat, 6, ingridientsForm2);
            chcVegetarian.Checked = false;
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            CheckingCheckbox(chcEggs, 7, ingridientsForm2);
            txtName.Text = txtName.Text.ToUpper();
        }
        #endregion
    }
}
