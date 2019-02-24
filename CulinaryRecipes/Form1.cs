using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CulinaryRecipes.Properties;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CulinaryRecipes
{
    public partial class Form1 : Form
    {
        int idDgGrid, numberOfPortionsForm1, searchName;
        string gramsColumnDgGridForm1, ingredientColumnDgGridForm1, descriptionForm1, idRatingForm1, amountsOfIngredientsForm1;
        bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
        public int counter = 0;
        public int[] idMeal = new int[7];
        public int[] ingridients = new int[8];
        public bool seekUnsubscribe = false;
        bool seekUnsubscribeSupport = false;

        public List<CheckBox> CheckBoxList = new List<CheckBox>();

        string[] nameCheckBox = {
            "Fishcheckbox","Pastacheckbox","Fruitscheckbox",
            "Mushroomscheckbox", "Birdcheckbox", "Meatcheckbox",
            "Eggscheckbox", "Vegetarian", "Snackcheckbox",
            "Dinnercheckbox", "Soupcheckbox", "Dessertcheckbox",
            "Drinkscheckbox", "Preservescheckbox", "Saladcheckbox" };

        string[] nameColumnCheckBox = {
            "IdFishIngredients","IdPastaIngredients","IdFruitsIngredients",
            "IdMuschroomsIngredients", "IdBirdIngredients", "IdMeatIngredients",
            "IdEggsIngredients", "Vegetarian", "SnackMeal",
            "DinnerMeal", "SoupMeal", "DessertMeal",
            "DrinkMeal", "PreservesMeal", "SaladMeal" };

        string[] nameColumnsGroup =
        {
            "CategoryPreparationTime","CategoryDifficultLevel","CategoryRating","CategoryCuisines"
        };

        Form2 stringOfCharactersForm2 = new Form2();
        List<CheckBox> checkBoxFilter = new List<CheckBox>();
        XmlSerializer xs;
        List<RecipesBase> ls;

        public void HideLabelClearCheckBox()
        {
            for (int i = 0; i < CheckBoxList.Count; i++)
            {
                if (CheckBoxList[i].Checked)
                {
                    CheckboxLabelShow();
                }
                else
                {
                    CheckboxLabelClear();
                }
            }
        }

        public void CheckboxLabelClear()
        {
            lblClearCheckBox.Visible = false;
            lblRightTwoLine.Visible = false;
            lblRightOneLine.Visible = false;
        }

        public void CheckboxLabelShow()
        {
            lblClearCheckBox.Visible = true;
            lblRightTwoLine.Visible = true;
            lblRightOneLine.Visible = true;
        }

        public Form1()
        {
            InitializeComponent();
            ls = new List<RecipesBase>();

            xs = new XmlSerializer(typeof(List<RecipesBase>));

            CheckBoxList.Add(chcFish);
            CheckBoxList.Add(chcPasta);
            CheckBoxList.Add(chcFruits);
            CheckBoxList.Add(chcMuschrooms);
            CheckBoxList.Add(chcBird);
            CheckBoxList.Add(chcMeat);
            CheckBoxList.Add(chcEggs);
            CheckBoxList.Add(chcVegetarian);

            CheckBoxList.Add(chcSnack);
            CheckBoxList.Add(chcDinner);
            CheckBoxList.Add(chcSoup);
            CheckBoxList.Add(chcDessert);
            CheckBoxList.Add(chcDrink);
            CheckBoxList.Add(chcPreserves);
            CheckBoxList.Add(chcSalad);

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDataGridView();
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            searchName = 1;
            CheckConnection();
            Statistic();

            checkBoxFilter.Add(chcFilterTime30);
            checkBoxFilter.Add(chcFilterTime60);

            #region Przypisanie chackboxów
            if (idMeal[0] == 1) chcSnack.Checked = true;
            if (idMeal[1] == 1) chcDinner.Checked = true;
            if (idMeal[2] == 1) chcSoup.Checked = true;
            if (idMeal[3] == 1) chcDessert.Checked = true;
            if (idMeal[4] == 1) chcDrink.Checked = true;
            if (idMeal[5] == 1) chcPreserves.Checked = true;
            if (idMeal[6] == 1) chcSalad.Checked = true;

            if (ingridients[0] == 1) chcFish.Checked = true;
            if (ingridients[1] == 1) chcPasta.Checked = true;
            if (ingridients[2] == 1) chcFruits.Checked = true;
            if (ingridients[3] == 1) chcMuschrooms.Checked = true;
            if (ingridients[4] == 1) chcBird.Checked = true;
            if (ingridients[5] == 1) chcMeat.Checked = true;
            if (ingridients[6] == 1) chcEggs.Checked = true;
            if (ingridients[7] == 1) chcVegetarian.Checked = true;
            #endregion

            if (seekUnsubscribe == true)
            {
                searchEngine.Search(searchName);
                lblCleanVisibleFalse();
                unsubscribe = true;
                seekUnsubscribe = true;
                seekUnsubscribeSupport = true;
            }
        }

        #region Buttony

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Statistic();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewForm();
        }

        public void OpenClick()
        {
            Form2 OpenForm = new Form2();

            if (txtLittleName.Text == "") { }
            else
            {
                OpenForm.idDgGridForm2 = idDgGrid;
                OpenForm.titleForm2 = txtLittleName.Text;

                if (amountsOfIngredientsForm1 != null)
                {
                    OpenForm.amountsOfIngredientsForm2 = CleanDash(amountsOfIngredientsForm1);
                }
                else
                {
                    OpenForm.amountsOfIngredientsForm2 = amountsOfIngredientsForm1;
                }

                OpenForm.gramsForm2 = gramsColumnDgGridForm1;

                if (ingredientColumnDgGridForm1 != null)
                {
                    OpenForm.ingredientForm2 = CleanDash(ingredientColumnDgGridForm1);
                }
                else
                {
                    OpenForm.ingredientForm2 = ingredientColumnDgGridForm1;
                }

                if (txtShortDescription.Text != null)
                {
                    OpenForm.ShortDescriptionForm2 = CleanDash(txtShortDescription.Text);
                }
                else
                {
                    OpenForm.ShortDescriptionForm2 = txtShortDescription.Text;
                }

                if (descriptionForm1 != null)
                {
                    OpenForm.instructionForm2 = CleanDash(descriptionForm1);
                }
                else
                {
                    OpenForm.instructionForm2 = descriptionForm1;
                }

                OpenForm.numberOfPortionsForm2 = numberOfPortionsForm1;
                OpenForm.linkForm2 = pbLittlePhoto.ImageLocation;
                OpenForm.listOfCuisinesForm2 = lblCuisine.Text;
                OpenForm.idRatingForm2 = idRatingForm1;
                OpenForm.difficultLevelForm2 = lblShortLevel.Text;
                OpenForm.executionTimeForm2 = lblShortTime.Text;

                for (int i = 0; i < idMeal.Length; i++)
                {
                    OpenForm.IdMealForm2[i] = idMeal[i];
                }
                for (int i = 0; i < ingridients.Length; i++)
                {
                    OpenForm.ingridientsForm2[i] = ingridients[i];
                }

                OpenForm.counterForm2 = counter;

                OpenForm.seekUnsubscribeForm2 = seekUnsubscribe;

                this.Visible = false;
                OpenForm.ShowDialog();
                this.Close();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenClick();
        }

        private void lblCleanVisibleFalse()
        {
            if (dgGrid.Rows.Count > 0)
            {
                lblCleanDgGrid.Visible = true;
                lblLeftOneLine.Visible = true;
                lblLeftTwoLine.Visible = true;
            }
            else
            {
                lblCleanDgGrid.Visible = false;
                lblLeftOneLine.Visible = false;
                lblLeftTwoLine.Visible = false;
            }
        }
        bool unsubscribe = false;

        private void btnSeek_Click(object sender, EventArgs e)
        {
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
            if (chcVegetarian.Checked) chcVegetarian.Checked = false;

            searchEngine.Search(searchName);

            lblCleanVisibleFalse();
            unsubscribe = true;
            seekUnsubscribe = true;
            seekUnsubscribeSupport = true;



        }
        #endregion
        public void ClearSeek()
        {
            if (unsubscribe == true)
            {
                dgGrid.Rows.Clear();
            }
        }
        #region Menu

        private void eksportujPojedynczyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";
            if (idDgGrid <= 0) { MessageBox.Show("Wybierz przepis do eksportu"); }
            else
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string sciezka = saveFileDialog1.FileName;
                        FileStream fs = new FileStream(sciezka, System.IO.FileMode.Create, FileAccess.Write);

                        ls.Add(new RecipesBase(eksportId.Id, eksportId.RecipesName, eksportId.Ingredients, eksportId.AmountsMeal, eksportId.ShortDescription, eksportId.LongDescription, eksportId.NumberPortions, eksportId.CategoryCuisines, eksportId.CategoryRating, eksportId.CategoryDifficultLevel, eksportId.CategoryPreparationTime, eksportId.SnackMeal, eksportId.DinnerMeal, eksportId.SoupMeal, eksportId.DessertMeal, eksportId.DrinkMeal, eksportId.PreservesMeal, eksportId.SaladMeal, eksportId.IdFishIngredients, eksportId.IdPastaIngredients, eksportId.IdFruitsIngredients, eksportId.IdMuschroomsIngredients, eksportId.IdBirdIngredients, eksportId.IdMeatIngredients, eksportId.IdEggsIngredients, eksportId.PhotoLinkLocation, eksportId.Vegetarian, eksportId.Grams));

                        xs.Serialize(fs, ls);
                        fs.Close();
                        MessageBox.Show("Eksport pliku zakończył się sukcesem");

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }

                }
            }
        }

        int NewId;
        private void importujPojedynczyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var r in RecipesBase.getAll())
            {
                NewId = r.Id;
            }
            openFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string sciezka = openFileDialog1.FileName;
                    FileStream fs = new FileStream(sciezka, System.IO.FileMode.Open, FileAccess.Read);
                    ls = (List<RecipesBase>)xs.Deserialize(fs);

                    RecipesBase m = new RecipesBase();

                    foreach (var r in ls)
                    {
                        dgGrid.Rows.Add(
                        m.Id = NewId + 1, m.RecipesName = r.RecipesName, m.Ingredients = r.Ingredients, m.AmountsMeal = r.AmountsMeal, m.ShortDescription = r.ShortDescription, m.LongDescription = r.LongDescription, m.NumberPortions = r.NumberPortions, m.CategoryCuisines = r.CategoryCuisines, m.CategoryRating = r.CategoryRating, m.CategoryDifficultLevel = r.CategoryDifficultLevel, m.CategoryPreparationTime = r.CategoryPreparationTime, m.SnackMeal = r.SnackMeal, m.DinnerMeal = r.DinnerMeal, m.SoupMeal = r.SoupMeal, m.DessertMeal = r.DessertMeal, m.DrinkMeal = r.DrinkMeal, m.PreservesMeal = r.PreservesMeal, m.SaladMeal = r.SaladMeal, m.IdFishIngredients = r.IdFishIngredients, m.IdPastaIngredients = r.IdPastaIngredients, m.IdFruitsIngredients = r.IdFruitsIngredients, m.IdMuschroomsIngredients = r.IdMuschroomsIngredients, m.IdBirdIngredients = r.IdBirdIngredients, m.IdMeatIngredients = r.IdMeatIngredients, m.IdEggsIngredients = r.IdEggsIngredients, m.PhotoLinkLocation = r.PhotoLinkLocation, m.Vegetarian = r.Vegetarian, m.Grams = r.Grams);
                        RecipesBase.add(m);
                    }

                    SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
                    searchEngine.FilldgGrid();
                    MessageBox.Show(m.RecipesName + "\n" + "został zaimportowany.", "PLIK ");

                    fs.Close();
                    Statistic();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            lblCleanVisibleFalse();
        }

        private void usuńBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchEngine search = new SearchEngine(txtSeek.Text, dgGrid);
            if (MessageBox.Show("Czy na pewno usunąć Bazę danych? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                RecipesBase.ClearDb();
                MessageBox.Show("Dokument został usunięty");
                search.FilldgGrid();
            }
            lblCleanVisibleFalse();
        }

        private void nowyPrzepisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm();
        }

        #endregion

        #region CheckBoxMeal
        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSnack.Checked == false)
            {

                AllCheckboxAfterIf(chcSnack, "Snackcheckbox", lblSnackVeg, lblSnack);
            }
            else
            {

                AllCheckboxElse("SnackMeal", chcSnack, "Snackcheckbox", lblSnackVeg, lblSnack);
            }
            AllCheckboxAfterElse();
        }

        private void AllCheckboxElse(string nameInDataBase, CheckBox nameThis, string nameColumnDgGrid, Label labelVeg, Label main)
        {
            ClearSeek();

            fillGrid(nameInDataBase, nameThis, nameColumnDgGrid);
            VegeDlaVege(nameThis);
            VegePoprawka(nameThis, nameColumnDgGrid);

            Veg(nameThis, labelVeg, main);

            DeleteDuplicate();
            unsubscribe = false;
            HideWyczyscSiatke();
            seekUnsubscribe = false;
        }

        private void AllCheckboxAfterElse()
        {
            CleanThumbnails();
            CleanFunctionClear();
            CheckCheckBox();
        }

        private void AllCheckboxAfterIf(CheckBox nameThis, string nameColumnDgGrid, Label nameVeg, Label main)
        {
            //  Veg(nameThis, labelVeg, main);
            ClearCheckBox2(nameThis, nameColumnDgGrid);
            fillGridVege("Vegetarian");

            DeleteDuplicate();
            HideLabelClearCheckBox();

            Veg(nameThis, nameVeg, main);
        }


        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {

            if (chcDinner.Checked == false)
            {
                AllCheckboxAfterIf(chcDinner, "Dinnercheckbox", lblDinnerVeg, lblDinner);
            }
            else
            {
                AllCheckboxElse("DinnerMeal", chcDinner, "Dinnercheckbox", lblDinnerVeg, lblDinner);

            }

            AllCheckboxAfterElse();
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {

            if (chcSoup.Checked == false)
            {
                AllCheckboxAfterIf(chcSoup, "Soupcheckbox", lblSoupVeg, lblSoup);
            }
            else
            {
                AllCheckboxElse("SoupMeal", chcSoup, "Soupcheckbox", lblSoupVeg, lblSoup);

            }
            AllCheckboxAfterElse();

        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDessert.Checked == false)
            {
                AllCheckboxAfterIf(chcDessert, "Dessertcheckbox", lblDessertVeg, lblDessert);
            }
            else
            {
                AllCheckboxElse("DessertMeal", chcDessert, "Dessertcheckbox", lblDessertVeg, lblDessert);

            }
            AllCheckboxAfterElse();
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDrink.Checked == false)
            {

                AllCheckboxAfterIf(chcDrink, "Drinkscheckbox", lblDrinksVeg, lblDrinks);

            }
            else
            {
                AllCheckboxElse("DrinkMeal", chcDrink, "Drinkscheckbox", lblDrinksVeg, lblDrinks);

            }
            AllCheckboxAfterElse();
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPreserves.Checked == false)
            {
                AllCheckboxAfterIf(chcPreserves, "Preservescheckbox", lblPrevervesVeg, lblPreserves);
            }
            else
            {

                AllCheckboxElse("PreservesMeal", chcPreserves, "Preservescheckbox", lblPrevervesVeg, lblPreserves);

            }
            AllCheckboxAfterElse();
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSalad.Checked == false)
            {
                AllCheckboxAfterIf(chcSalad, "Saladcheckbox", lblSaladVeg, lblSalad);
            }
            else
            {
                AllCheckboxElse("SaladMeal", chcSalad, "Saladcheckbox", lblSaladVeg, lblSalad);

            }
            AllCheckboxAfterElse();
        }
        #endregion

        #region CheckBoxIngridients
        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFish.Checked == false)
            {
                AllCheckboxAfterIf(chcFish, "Fishcheckbox", lblFishVeg, lblFish);
            }
            else
            {
                AllCheckboxElse("IdFishIngredients", chcFish, "Fishcheckbox", lblFishVeg, lblFish);

            }
            AllCheckboxAfterElse();
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPasta.Checked == false)
            {

                AllCheckboxAfterIf(chcPasta, "Pastacheckbox", lblPastaVeg, lblPasta);
            }
            else
            {
                AllCheckboxElse("IdPastaIngredients", chcPasta, "Pastacheckbox", lblPastaVeg, lblPasta);

            }
            AllCheckboxAfterElse();
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFruits.Checked == false)
            {
                AllCheckboxAfterIf(chcFruits, "Fruitscheckbox", lblFruitsVeg, lblFruits);
            }
            else
            {
                AllCheckboxElse("IdFruitsIngredients", chcFruits, "Fruitscheckbox", lblFruitsVeg, lblFruits);

            }
            AllCheckboxAfterElse();
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMuschrooms.Checked == false)
            {
                AllCheckboxAfterIf(chcMuschrooms, "Mushroomscheckbox", lblMushroomVeg, lblMuschrooms);
            }
            else
            {
                AllCheckboxElse("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox", lblMushroomVeg, lblMuschrooms);

            }
            AllCheckboxAfterElse();
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBird.Checked == false)
            {
                AllCheckboxAfterIf(chcBird, "Birdcheckbox", lblBirdVeg, lblBird);
            }
            else
            {
                AllCheckboxElse("IdBirdIngredients", chcBird, "Birdcheckbox", lblBirdVeg, lblBird);

            }
            AllCheckboxAfterElse();
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMeat.Checked == false)
            {
                AllCheckboxAfterIf(chcMeat, "Meatcheckbox", lblMeatVeg, lblMeat);
            }
            else
            {
                AllCheckboxElse("IdMeatIngredients", chcMeat, "Meatcheckbox", lblMeatVeg, lblMeat);

            }
            AllCheckboxAfterElse();
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            if (chcEggs.Checked == false)
            {
                AllCheckboxAfterIf(chcEggs, "Eggscheckbox", lblEggsVeg, lblEggs);
            }
            else
            {
                AllCheckboxElse("IdEggsIngredients", chcEggs, "Eggscheckbox", lblEggsVeg, lblEggs);

            }
            AllCheckboxAfterElse();
        }
        #endregion

        RecipesBase eksportId = new RecipesBase();
        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK();
        }

        #region Function

        public void Statistic()
        {
            if (chcStstistic.Checked)
            {
                int count = 1;
                foreach (var p in RecipesBase.getAll())
                {
                    NewId = count;
                    count++;
                }

                lblStatistic.Text = NewId.ToString();
                lblYouHave.Visible = true;
                lblStatistic.Visible = true;
                lblCulinary.Visible = true;
            }
            else
            {
                lblYouHave.Visible = false;
                lblStatistic.Visible = false;
                lblCulinary.Visible = false;
            }
        }

        public void CheckConnection()
        {
            if (isAvailable == false && counter == 0)
            {
                MessageBox.Show("Brak Sieci! Zdjęcia zalinkowane z internetu nie będą działały!");
                counter++;
            }
            else if (isAvailable == true && counter == 1)
            {
                MessageBox.Show("Sieć już działa");
                counter = 0;
            }
        }

        private string CleanDash(string nameVariableForm1)
        {
            StringBuilder S = new StringBuilder(nameVariableForm1);

            for (int i = 0; i < nameVariableForm1.Length; i++)
            {
                if (nameVariableForm1[i] == stringOfCharactersForm2.stringOfCharacters && nameVariableForm1[i + 1] == stringOfCharactersForm2.stringOfCharacters1)
                {
                    S[i] = ' ';
                    S[i + 1] = ' ';
                }
                else
                {
                    continue;
                }
            }
            return S.ToString();
        }

        private void HideWyczyscSiatke()
        {
            lblCleanDgGrid.Visible = false;
            lblLeftOneLine.Visible = false;
            lblLeftTwoLine.Visible = false;
        }

        private void CleanThumbnails()
        {
            if (dgGrid.Rows.Count <= 0)
            {
                pbLittlePhoto.Visible = false;
                txtLittleName.Visible = false;
                txtShortDescription.Visible = false;
                lblShortTime.Visible = false;
                lblShortLevel.Visible = false;
                lblCuisine.Visible = false;
                pbStar1.Visible = false;
                pbStar2.Visible = false;
                pbStar3.Visible = false;
                idDgGrid = 0;
            }
        }

        private void BoldAndSlim()
        {
            if (searchName == 1)
            {
                lblNameSeek.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                lblAmountsSeek.Font = new System.Drawing.Font("Corbel", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
            else
            {
                lblNameSeek.Font = new System.Drawing.Font("Corbel", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
                lblAmountsSeek.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            }
        }

        public void CreateDataGridView()
        {
            dgGrid.Rows.ToString().ToUpper();

            dgGrid.Columns.Add("id", "Id");
            dgGrid.Columns.Add("name", "Name");
            dgGrid.Columns.Add("Components", "Components");
            dgGrid.Columns.Add("Amounts", "Amounts");
            dgGrid.Columns.Add("ShortDescription", "ShortDescription");
            dgGrid.Columns.Add("LongDescription", "LongDescription");

            dgGrid.Columns.Add("NumberPortions", "NumberPortions");
            dgGrid.Columns.Add("CategoryCuisines", "CategoryCuisines");
            dgGrid.Columns.Add("IdCategoryRating", "IdCategoryRating");
            dgGrid.Columns.Add("IdcategoryDifficultLevel", "IdcategoryDifficultLevel");

            dgGrid.Columns.Add("IdcategoryPreparationTime", "IdcategoryPreparationTime");

            dgGrid.Columns.Add("Snackcheckbox", "Snackcheckbox");
            dgGrid.Columns.Add("Dinnercheckbox", "Dinnercheckbox");
            dgGrid.Columns.Add("Soupcheckbox", "Soupcheckbox");
            dgGrid.Columns.Add("Dessertcheckbox", "Dessertcheckbox");
            dgGrid.Columns.Add("Drinkscheckbox", "Drinkscheckbox");
            dgGrid.Columns.Add("Preservescheckbox", "Preservescheckbox");
            dgGrid.Columns.Add("Saladcheckbox", "Saladcheckbox");

            dgGrid.Columns.Add("Fishcheckbox", "Fishcheckbox");
            dgGrid.Columns.Add("Pastacheckbox", "Pastacheckbox");
            dgGrid.Columns.Add("Fruitscheckbox", "Fruitscheckbox");
            dgGrid.Columns.Add("Mushroomscheckbox", "Mushroomscheckbox");
            dgGrid.Columns.Add("Birdcheckbox", "Birdcheckbox");
            dgGrid.Columns.Add("Meatcheckbox", "Meatcheckbox");
            dgGrid.Columns.Add("Eggscheckbox", "Eggscheckbox");
            dgGrid.Columns.Add("Photo", "Photo");
            dgGrid.Columns.Add("Vegetarian", "Vegetarian");
            dgGrid.Columns.Add("Grams", "Grams");

            for (int i = 0; i < dgGrid.ColumnCount; i++)
            {
                if (i == 1) continue;
                else dgGrid.Columns[i].Visible = false;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchEngine search = new SearchEngine(txtSeek.Text, dgGrid);
            ClearDataBase(idDgGrid);
            search.FilldgGrid();
            Statistic();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            searchName = 1;
            BoldAndSlim();
        }

        private void label13_Click(object sender, EventArgs e)
        {
            searchName = 2;
            BoldAndSlim();
        }

        private class RowComparer : IComparer
        {
            private static int sortOrderModifier = 1;

            public RowComparer(SortOrder sortOrder)
            {
                if (sortOrder == SortOrder.Descending)
                {
                    sortOrderModifier = -1;
                }
                else if (sortOrder == SortOrder.Ascending)
                {
                    sortOrderModifier = 1;
                }
            }

            public int Compare(object x, object y)
            {
                DataGridViewRow DataGridViewRow1 = (DataGridViewRow)x;
                DataGridViewRow DataGridViewRow2 = (DataGridViewRow)y;

                // Try to sort based on the Last Name column.
                int CompareResult = System.String.Compare(
                    DataGridViewRow1.Cells[1].Value.ToString(),
                    DataGridViewRow2.Cells[1].Value.ToString());

                // If the Last Names are equal, sort based on the First Name.
                if (CompareResult == 0)
                {
                    CompareResult = System.String.Compare(
                        DataGridViewRow1.Cells[0].Value.ToString(),
                        DataGridViewRow2.Cells[0].Value.ToString());
                }
                return CompareResult * sortOrderModifier;
            }
        }

        private void DeleteDuplicate()
        {
            dgGrid.Sort(new RowComparer(SortOrder.Ascending));
            for (int i = 0; i < dgGrid.RowCount - 1; i++)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells[0].Value) == Convert.ToInt32(dgGrid.Rows[i + 1].Cells[0].Value)) dgGrid.Rows.Remove(dgGrid.Rows[i]);
                else continue;
            }
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 nowy = new Form4();
            nowy.ShowDialog();
        }

        private void importujCalaBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchEngine search = new SearchEngine(txtSeek.Text, dgGrid);

            dgGrid.Visible = false;
            search.FilldgGrid();

            if (dgGrid.Rows.Count > 0)
            {
                MessageBox.Show("Zanim Zaimportujesz plik wyczyść bazę danych. - Plik - Usuń bazę danych");
            }
            else
            {
                try
                {
                    openFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string sciezka = openFileDialog1.FileName;

                        FileStream fs = new FileStream(sciezka, System.IO.FileMode.Open, FileAccess.Read);
                        ls = (List<RecipesBase>)xs.Deserialize(fs);

                        RecipesBase m = new RecipesBase();
                        if (dgGrid.Rows.Count <= 0)
                        {
                            foreach (var r in ls)
                            {
                                dgGrid.Rows.Add(
                                m.Id = r.Id, m.RecipesName = r.RecipesName, m.Ingredients = r.Ingredients, m.AmountsMeal = r.AmountsMeal, m.ShortDescription = r.ShortDescription, m.LongDescription = r.LongDescription, m.NumberPortions = r.NumberPortions, m.CategoryCuisines = r.CategoryCuisines, m.CategoryRating = r.CategoryRating, m.CategoryDifficultLevel = r.CategoryDifficultLevel, m.CategoryPreparationTime = r.CategoryPreparationTime, m.SnackMeal = r.SnackMeal, m.DinnerMeal = r.DinnerMeal, m.SoupMeal = r.SoupMeal, m.DessertMeal = r.DessertMeal, m.DrinkMeal = r.DrinkMeal, m.PreservesMeal = r.PreservesMeal, m.SaladMeal = r.SaladMeal, m.IdFishIngredients = r.IdFishIngredients, m.IdPastaIngredients = r.IdPastaIngredients, m.IdFruitsIngredients = r.IdFruitsIngredients, m.IdMuschroomsIngredients = r.IdMuschroomsIngredients, m.IdBirdIngredients = r.IdBirdIngredients, m.IdMeatIngredients = r.IdMeatIngredients, m.IdEggsIngredients = r.IdEggsIngredients, m.PhotoLinkLocation = r.PhotoLinkLocation, m.Vegetarian = r.Vegetarian, m.Grams = r.Grams);
                                RecipesBase.add(m);
                            }
                            MessageBox.Show("Baza danych została zaimportowana");

                            dgGrid.Visible = true;
                            search.FilldgGrid();
                        }
                        else
                        {
                            MessageBox.Show("Baza danych przed importem musi być pusta");
                        }
                        fs.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            lblCleanVisibleFalse();
            dgGrid.Visible = true;
        }

        private void eksportujCalaBazeDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sciezka = saveFileDialog1.FileName;
                FileStream fs = new FileStream(sciezka, System.IO.FileMode.Create, FileAccess.Write);

                foreach (var r in RecipesBase.getAll())
                {
                    ls.Add(new RecipesBase(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams));
                }
                xs.Serialize(fs, ls);
                fs.Close();

                MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            }
        }

        private void MealAndIngredients()
        {
            int row = dgGrid.CurrentCell.RowIndex;

            for (int i = 0; i < idMeal.Length; i++)
            {
                idMeal[i] = Convert.ToInt32(dgGrid.Rows[row].Cells[i + 11].Value);
            }
            for (int i = 0; i < ingridients.Length - 1; i++)
            {
                ingridients[i] = Convert.ToInt32(dgGrid.Rows[row].Cells[i + 18].Value);
            }
        }

        public void OneCliCK()
        {
            deleteCMS.Visible = true;
            int row = dgGrid.CurrentCell.RowIndex;

            if (row >= 0)
            {

                idDgGrid = Convert.ToInt32(dgGrid.Rows[row].Cells[0].Value);
                eksportId = RecipesBase.getById(idDgGrid);
                txtLittleName.Text = dgGrid.Rows[row].Cells[1].Value.ToString();
                ingredientColumnDgGridForm1 = dgGrid.Rows[row].Cells[2].Value.ToString();
                amountsOfIngredientsForm1 = dgGrid.Rows[row].Cells[3].Value.ToString();

                if (dgGrid.Rows[row].Cells[4].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString() +
                    stringOfCharactersForm2.stringOfCharacters1.ToString())
                    txtShortDescription.Text = CleanDash(txtShortDescription.Text);
                else
                {
                    txtShortDescription.Text = dgGrid.Rows[row].Cells[4].Value.ToString();
                }

                descriptionForm1 = dgGrid.Rows[row].Cells[5].Value.ToString();
                numberOfPortionsForm1 = Convert.ToInt32(dgGrid.Rows[row].Cells[6].Value);

                if (dgGrid.Rows[row].Cells[7].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    lblCuisine.Text = dgGrid.Rows[row].Cells[7].Value.ToString();
                }

                if (dgGrid.Rows[row].Cells[8].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    idRatingForm1 = dgGrid.Rows[row].Cells[8].Value.ToString();
                }

                if (idRatingForm1 == "1") pbStar1.Visible = true;
                else if (idRatingForm1 == "2")
                {
                    pbStar1.Visible = true;
                    pbStar2.Visible = true;
                }
                else if (idRatingForm1 == "3")
                {
                    pbStar1.Visible = true;
                    pbStar2.Visible = true;
                    pbStar3.Visible = true;
                }

                lblShortLevel.Text = dgGrid.Rows[row].Cells[9].Value.ToString();
                lblShortTime.Text = dgGrid.Rows[row].Cells[10].Value.ToString();


                MealAndIngredients();

                if (dgGrid.Rows[row].Cells[25].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    pbLittlePhoto.Image = Resources.przepisy;
                }
                else
                {
                    pbLittlePhoto.ImageLocation = dgGrid.Rows[row].Cells[25].Value.ToString();
                }

                ingridients[7] = Convert.ToInt32(dgGrid.Rows[row].Cells[26].Value);

                if (dgGrid.Rows[row].Cells[27].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    gramsColumnDgGridForm1 = dgGrid.Rows[row].Cells[27].Value.ToString();
                }

                pbLittlePhoto.Visible = true;
                txtLittleName.Visible = true;
                txtShortDescription.Visible = true;
                lblShortTime.Visible = true;
                lblShortLevel.Visible = true;
                lblCuisine.Visible = true;

                dgGrid.DefaultCellStyle.SelectionBackColor = Color.SlateGray;
            }
        }

        public void OneCliCK(int id)
        {
            deleteCMS.Visible = true;
            int row = dgGrid.CurrentCell.RowIndex;

            if (row >= 0)
            {
                idDgGrid = Convert.ToInt32(dgGrid.Rows[id].Cells[0].Value);
                eksportId = RecipesBase.getById(idDgGrid);

                txtLittleName.Text = dgGrid.Rows[row].Cells[1].Value.ToString();
                ingredientColumnDgGridForm1 = dgGrid.Rows[row].Cells[2].Value.ToString();
                amountsOfIngredientsForm1 = dgGrid.Rows[row].Cells[3].Value.ToString();

                if (dgGrid.Rows[row].Cells[4].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString() +
                    stringOfCharactersForm2.stringOfCharacters1.ToString())
                {
                    txtShortDescription.Text = CleanDash(txtShortDescription.Text);
                }
                else
                {
                    txtShortDescription.Text = dgGrid.Rows[row].Cells[4].Value.ToString();
                }

                descriptionForm1 = dgGrid.Rows[row].Cells[5].Value.ToString();
                numberOfPortionsForm1 = Convert.ToInt32(dgGrid.Rows[row].Cells[6].Value);

                if (dgGrid.Rows[row].Cells[7].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    lblCuisine.Text = dgGrid.Rows[row].Cells[7].Value.ToString();
                }

                if (dgGrid.Rows[row].Cells[8].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    idRatingForm1 = dgGrid.Rows[row].Cells[8].Value.ToString();
                }

                if (idRatingForm1 == "1") pbStar1.Visible = true;
                else if (idRatingForm1 == "2")
                {
                    pbStar1.Visible = true;
                    pbStar2.Visible = true;
                }
                else if (idRatingForm1 == "3")
                {
                    pbStar1.Visible = true;
                    pbStar2.Visible = true;
                    pbStar3.Visible = true;
                }

                lblShortLevel.Text = dgGrid.Rows[row].Cells[9].Value.ToString();
                lblShortTime.Text = dgGrid.Rows[row].Cells[10].Value.ToString();

                MealAndIngredients();

                if (dgGrid.Rows[row].Cells[25].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString()) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = dgGrid.Rows[row].Cells[25].Value.ToString();

                ingridients[7] = Convert.ToInt32(dgGrid.Rows[row].Cells[26].Value);
                if (dgGrid.Rows[row].Cells[27].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    gramsColumnDgGridForm1 = dgGrid.Rows[row].Cells[27].Value.ToString();
                }

                pbLittlePhoto.Visible = true;
                txtLittleName.Visible = true;
                txtShortDescription.Visible = true;
                lblShortTime.Visible = true;
                lblShortLevel.Visible = true;
                lblCuisine.Visible = true;

                dgGrid.DefaultCellStyle.SelectionBackColor = Color.SlateGray;
            }
        }

        private void dgGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK();
            OpenClick();
        }

        private void btnSeek_KeyDown(object sender, KeyEventArgs e)
        {
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
            if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSeek.Text))
            {
                searchEngine.Search(searchName);

                lblCleanVisibleFalse();
            }
        }

        private void dgGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgGrid.Rows.Count > 0)
            {
                OneCliCK();
                OpenClick();
            }
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                NewForm();
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Application.Exit();
        }

        private void btnOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OpenClick();
        }

        private void Wypelnij()
        {
            if (chcFish.Checked)
            {
                fillGrid("IdFishIngredients", chcFish, "Fishcheckbox");
                Veg(chcFish, lblFishVeg, lblFish);
            }
            if (chcPasta.Checked)
            {
                fillGrid("IdPastaIngredients", chcPasta, "Pastacheckbox");
                Veg(chcPasta, lblPastaVeg, lblPasta);
            }
            if (chcFruits.Checked)
            {
                fillGrid("IdFruitsIngredients", chcFruits, "Fruitscheckbox");
                Veg(chcFruits, lblFruitsVeg, lblFruits);
            }
            if (chcMuschrooms.Checked)
            {
                fillGrid("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox");
                Veg(chcMuschrooms, lblMushroomVeg, lblMuschrooms);
            }
            if (chcBird.Checked)
            {
                fillGrid("IdBirdIngredients", chcBird, "Birdcheckbox");
                Veg(chcBird, lblBirdVeg, lblBird);
            }
            if (chcMeat.Checked)
            {
                fillGrid("IdMeatIngredients", chcMeat, "Meatcheckbox");
                Veg(chcMeat, lblMeatVeg, lblMeat);
            }
            if (chcEggs.Checked)
            {
                fillGrid("IdEggsIngredients", chcEggs, "Eggscheckbox");
                Veg(chcEggs, lblEggsVeg, lblEggs);
            }

            if (chcSnack.Checked)
            {
                fillGrid("SnackMeal", chcSnack, "Snackcheckbox");
                Veg(chcSnack, lblSnackVeg, lblSnack);
            }
            if (chcDinner.Checked)
            {
                fillGrid("DinnerMeal", chcDinner, "Dinnercheckbox");
                Veg(chcDinner, lblDinnerVeg, lblDinner);
            }
            if (chcSoup.Checked)
            {
                fillGrid("SoupMeal", chcSoup, "Soupcheckbox");
                Veg(chcSoup, lblSoupVeg, lblSoup);
            }
            if (chcDessert.Checked)
            {
                fillGrid("DessertMeal", chcDessert, "Dessertcheckbox");
                Veg(chcDessert, lblDessertVeg, lblDessert);
            }
            if (chcDrink.Checked)
            {
                fillGrid("DrinkMeal", chcDrink, "Drinkscheckbox");
                Veg(chcDrink, lblDrinksVeg, lblDrinks);
            }
            if (chcPreserves.Checked)
            {
                fillGrid("PreservesMeal", chcPreserves, "Preservescheckbox");
                Veg(chcPreserves, lblPrevervesVeg, lblPreserves);
            }
            if (chcSalad.Checked)
            {
                fillGrid("SaladMeal", chcSalad, "Saladcheckbox");
                Veg(chcSalad, lblSaladVeg, lblSalad);
            }
            else
            {
                SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
                searchEngine.FilldgGrid();
            }
            //DeleteDuplicate();
        }

        private void VegeDlaVege(CheckBox main)
        {
            for (int i = 0; i < CheckBoxList.Count; i++)
            {
                if (chcVegetarian.Checked)
                {
                    if (CheckBoxList[i] == main) continue;
                    else
                    {
                        CheckBoxList[i].Checked = false;
                        chcVegetarian.Checked = true;
                        main.Checked = true;
                    }
                }
            }

        }
        //zwraca prawde lub fałsz czy checkbox jest zaznaczony
        //public bool[] IsTheCheckboxChecked()
        //{
        //    bool[] zaznaczony = new bool[14];
        //    int i = 0;

        //    foreach (Control p in Controls)
        //    {
        //        if (p is CheckBox && ((CheckBox)p).Checked)
        //        {
        //            zaznaczony[i] = true;
        //            i++;
        //        }
        //    }
        //    return zaznaczony;
        //}

        public void Veg(CheckBox name, Label veg, Label main)
        {
            if (chcVegetarian.Checked && name.Checked)
            {
                veg.Visible = true;
                Vegetarian.GreenLabel(name, main);
            }
            else if (chcVegetarian.Checked == false || name.Checked == false)
            {
                veg.Visible = false;
                Vegetarian.WhiteLabel(name, main);
            }
        }
        private void chcVegetarian_CheckedChanged(object sender, EventArgs e)
        {

            if (chcVegetarian.Checked == false)
            {

                Vegetarian.WhiteLabel(chcVegetarian, lblVegetarian);

                ClearCheckBox2(chcVegetarian, "Vegetarian");

                Wypelnij();


                HideLabelClearCheckBox();
            }
            else
            {

                //foreach (var item in CzyZaznaczonyCheckbox())
                //{

                //    if (item == true)
                //    {

                //        for (int i = 0; i < dgGrid.RowCount; i++)
                //        {

                //            if (Convert.ToInt32(dgGrid.Rows[i].Cells["Vegetarian"].Value) == 1)
                //            {
                //                continue;
                //            }
                //            else
                //            {
                //                dgGrid.Rows[i].Visible = false;
                //            }

                //        }
                //    }
                //}
                //  CleanDgGrid();

                Vegetarian.GreenLabel(chcVegetarian, lblVegetarian);

                int quantity = 0;
                bool delete2 = false;

                for (int i = 0; i < CheckBoxList.Count; i++)
                {
                    if (CheckBoxList[i].Checked) quantity++;
                    if (i == 14 && quantity > 2) //pamietaj w razie zmiany ilosci checkboxow
                    {
                        delete2 = true;
                    }
                }
                for (int j = 0; j < CheckBoxList.Count; j++)
                {
                    if (delete2)
                        if (CheckBoxList[j].Name != "chcVegetarian")
                            CheckBoxList[j].Checked = false;
                    quantity = 0;

                }

                if (chcFish.Checked)
                {
                    VegePoprawka(chcFish, "Fishcheckbox");
                    Veg(chcFish, lblFishVeg, lblFish);
                }
                else if (chcPasta.Checked)
                {
                    VegePoprawka(chcPasta, "Pastacheckbox");
                    Veg(chcPasta, lblPastaVeg, lblPasta);
                }
                else if (chcFruits.Checked)
                {
                    VegePoprawka(chcFruits, "Fruitscheckbox");
                    Veg(chcFruits, lblFruitsVeg, lblFruits);
                }
                else if (chcMuschrooms.Checked)
                {
                    VegePoprawka(chcMuschrooms, "Mushroomscheckbox");
                    Veg(chcMuschrooms, lblMushroomVeg, lblMuschrooms);
                }
                else if (chcBird.Checked)
                {
                    VegePoprawka(chcBird, "Birdcheckbox");
                    Veg(chcBird, lblBirdVeg, lblBird);
                }
                else if (chcMeat.Checked)
                {
                    VegePoprawka(chcMeat, "Meatcheckbox");
                    Veg(chcMeat, lblMeatVeg, lblMeat);
                }
                else if (chcEggs.Checked)
                {
                    VegePoprawka(chcEggs, "Eggscheckbox");
                    Veg(chcEggs, lblEggsVeg, lblEggs);
                }

                else if (chcSnack.Checked)
                {
                    VegePoprawka(chcSnack, "Snackcheckbox");
                    Veg(chcSnack, lblSnackVeg, lblSnack);
                }
                else if (chcDinner.Checked)
                {
                    VegePoprawka(chcDinner, "Dinnercheckbox");
                    Veg(chcDinner, lblDinnerVeg, lblDinner);
                }
                else if (chcSoup.Checked)
                {
                    VegePoprawka(chcSoup, "Soupcheckbox");
                    Veg(chcSoup, lblSoupVeg, lblSoup);
                }
                else if (chcDessert.Checked)
                {
                    VegePoprawka(chcDessert, "Dessertcheckbox");
                    Veg(chcDessert, lblDessertVeg, lblDessert);
                }
                else if (chcDrink.Checked)
                {
                    VegePoprawka(chcDrink, "Drinkscheckbox");
                    Veg(chcDrink, lblDrinksVeg, lblDrinks);
                }

                else if (chcPreserves.Checked)
                {
                    VegePoprawka(chcPreserves, "Preservescheckbox");
                    Veg(chcPreserves, lblPrevervesVeg, lblPreserves);
                }
                else if (chcSalad.Checked)
                {
                    VegePoprawka(chcSalad, "Saladcheckbox");
                    Veg(chcSalad, lblSaladVeg, lblSalad);
                }
                else
                {
                    CleanDgGrid();
                    fillGrid("Vegetarian", chcVegetarian, "Vegetarian");
                }
            }

            DeleteDuplicate();
            CleanThumbnails();
            CleanFunctionClear();
            CheckCheckBox();
        }

        public void CleanDgGrid()
        {
            if (dgGrid.RowCount > 0)
            {
                dgGrid.Rows.Clear();
                lblCleanVisibleFalse();
            }
        }

        private void lblCleanDgGrid_Click(object sender, EventArgs e)
        {
            CleanDgGrid();
        }

        bool delete;
        private void ClearCheckBox2(CheckBox gl, string nameMain)
        {
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                delete = false;

                for (int j = 0; j < CheckBoxList.Count; j++)
                {
                    if (Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 0 && gl.Checked == false) continue;

                    else if (Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && gl.Checked) continue;

                    else if (Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && gl.Checked == false
                        && Convert.ToInt32(dgGrid.Rows[i].Cells[nameCheckBox[j]].Value) == 1
                        && CheckBoxList[j].Checked)
                    {
                        delete = false;
                        break;
                    }
                    else
                    {
                        delete = true;
                    }
                }

                if (delete == true) dgGrid.Rows.Remove(dgGrid.Rows[i]);
            }
        }

        private void CheckCheckBox()
        {
            for (int i = 0; i < CheckBoxList.Count; i++)
            {
                if (CheckBoxList[i].Checked)
                {
                    lblClearCheckBox.Visible = true;
                    lblRightTwoLine.Visible = true;
                    lblRightOneLine.Visible = true;
                }
            }
        }

        private void label13_Click_1(object sender, EventArgs e)
        {
            for (int i = 0; i < CheckBoxList.Count; i++)
            {
                if (CheckBoxList[i].Checked)
                {
                    CheckBoxList[i].Checked = false;
                }
            }

            lblClearCheckBox.Visible = false;
            lblRightTwoLine.Visible = false;
            lblRightOneLine.Visible = false;
        }

        private void txtSeek_KeyDown(object sender, KeyEventArgs e)
        {
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            if (e.KeyCode == Keys.F1)
            {
                searchEngine.Search(searchName);

                lblCleanVisibleFalse();

                unsubscribe = true;
                seekUnsubscribe = true;
                seekUnsubscribeSupport = true;

                OneCliCK(1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panelFiltrCuisine.Visible = true;
            panelFilterLevel.Visible = true;
            panelFilterRating.Visible = true;
            panelFilterTime.Visible = true;
            btnOff.Visible = true;
            btnFilter.Visible = true;
            btnFilter.BackColor = Color.Maroon;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            Function.UncheckedCheckBox(panelFiltrCuisine);
            Function.UncheckedCheckBox(panelFilterLevel);
            Function.UncheckedCheckBox(panelFilterRating);
            Function.UncheckedCheckBox(panelFilterTime);
            //filter = false;

            panelFiltrCuisine.Visible = false;
            panelFilterLevel.Visible = false;
            panelFilterRating.Visible = false;
            panelFilterTime.Visible = false;
            btnFilter.Visible = false;
            btnOff.Visible = false;
        }





        private void chcFiltrTime60_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterTime60);

            AddResultNameToList("60 min", chcFilterTime60, panelFilterTime);

           // UncheckedCheckBoxFilter(chcFilterTime60, panelFilterTime);

        }


        private void VegePoprawka(CheckBox main, string nameMain)
        {
            delete = false;

            if (chcVegetarian.Checked)
            {
                for (int i = dgGrid.RowCount - 1; i >= 0; i--)
                {
                    for (int j = 0; j < CheckBoxList.Count; j++)
                    {
                        if (Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells["Vegetarian"].Value) == 1 && main.Checked)
                        {
                            delete = false;
                            break;
                        }
                        else
                        {
                            delete = true;
                        }

                    }
                    if (delete == true) dgGrid.Rows.Remove(dgGrid.Rows[i]);
                }
            }
        }

    

        private void fillGrid(string _propName, CheckBox _name, string nazwa)
        {

            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll())
                {
                    if ((int)RecipesBase.GetPropValue(r, _propName) == 1)
                    {
                        dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
                    }
                }
            }
            else
            {
                for (int i = dgGrid.RowCount - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dgGrid.Rows[i].Cells[nazwa].Value) == 1 && dgGrid.Rows[i].Cells[nazwa].ToString() == nazwa)
                        dgGrid.Rows.Remove(dgGrid.Rows[i]);
                }
            }
        }

        //ew. columnTime do jednej listy ale wtedy nie porownuje
        private void fillGridFiltr(string _propName, CheckBox _name, string nazwa, List<string> column)
        {
            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll())
                {

                    for (int i = 0; i < column.Count; i++)
                    {
                        if (RecipesBase.GetPropValue(r, _propName) == column[i])
                        {

                            dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);

                        }
                    }
                }
            }
            else
            {
                for (int i = dgGrid.RowCount - 1; i >= 0; i--)
                {
                    if (dgGrid.Rows[i].Cells[nazwa].Value.ToString() == columnTime[i])
                        dgGrid.Rows.Remove(dgGrid.Rows[i]);
                }
            }
        }

        List<string> columnTime = new List<string>();
        List<string> columnLevel = new List<string>();
        List<string> columnRating = new List<string>();

        List<string> columnCuisine = new List<string>();

        public void AddResultNameToList(string resultName, CheckBox checkBoxName, Control set)
        {

            if (checkBoxName.Checked)
            {
                if (set.Name == "panelFilterTime")
                    columnTime.Add(resultName);
                else if (set.Name == "panelFilterLevel")
                    columnLevel.Add(resultName);
                else if (set.Name == "panelFilterRating")
                    columnRating.Add(resultName);
                else if (set.Name == "panelFiltrCuisine")
                    columnCuisine.Add(resultName);
            }
            else
            {
                columnTime.Remove(resultName);
                columnLevel.Remove(resultName);
                columnRating.Remove(resultName);
                columnCuisine.Remove(resultName);
            }
        }


        public void Filter()
        {

            bool cusines = false;
            bool rating = false;
            bool time = false;
            bool level = false;


            delete = false;

            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {

                if (columnCuisine.Count == 0)
                {
                    cusines = true;
                    columnCuisine.Add(dgGrid.Rows[i].Cells["CategoryCuisines"].Value.ToString());
                }
                if (columnRating.Count == 0)
                {
                    rating = true;
                    columnRating.Add(dgGrid.Rows[i].Cells["IdCategoryRating"].Value.ToString());
                }
                if (columnLevel.Count == 0)
                {
                    level = true;
                    columnLevel.Add(dgGrid.Rows[i].Cells["IdcategoryDifficultLevel"].Value.ToString());
                }
                if (columnTime.Count == 0)
                {
                    time = true;
                    columnTime.Add(dgGrid.Rows[i].Cells["IdcategoryPreparationTime"].Value.ToString());
                }

                for (int l = 0; l < columnCuisine.Count; l++)
                {
                    
                    for (int k = 0; k < columnRating.Count; k++)
                    {
                       
                       
                        for (int j = 0; j < columnLevel.Count; j++)
                        {
                          
                            for (int m = 0; m < columnTime.Count; m++)
                            {
                                

                                if (dgGrid.Rows[i].Cells["IdcategoryPreparationTime"].Value.ToString() == columnTime[m]
                        && dgGrid.Rows[i].Cells["IdcategoryDifficultLevel"].Value.ToString() == columnLevel[j]
                        && dgGrid.Rows[i].Cells["IdCategoryRating"].Value.ToString() == columnRating[k]
                        && dgGrid.Rows[i].Cells["CategoryCuisines"].Value.ToString() == columnCuisine[l])
                                {
                                    delete = false;
                                    break;
                                }
                                else
                                {
                                    delete = true;
                                }
                            }
                        }
                    }

                }


                if (delete == true) dgGrid.Rows.Remove(dgGrid.Rows[i]);

             
                if(rating) columnRating.Remove(columnRating[0]);
                if(level) columnLevel.Remove(columnLevel[0]);
                if(time) columnTime.Remove(columnTime[0]);
                if(cusines) columnCuisine.Remove(columnCuisine[0]);

            }

        }

        private void ChangeButtonFilterColor()
        {
            btnFilter.BackColor = Color.Green;
        }

        private void chcFilterTime30_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterTime30);

          //  UncheckedCheckBoxFilter(chcFilterTime30, panelFilterTime);
            AddResultNameToList("30 min", chcFilterTime30, panelFilterTime);

        }

        private void UncheckedCheckBoxFilter(CheckBox check, Control set)
        {
            if (check.Checked==false) check.Checked = false;
            else
            {
                foreach (Control p in set.Controls)
                {
                    if (p is CheckBox && p != check)
                    {
                        ((CheckBox)p).Checked = false;
                        check.Checked = true;
                    }
                    checkBoxFilterName.Remove(p as CheckBox);
                }
              
            }
            checkBoxFilterName.Add(check);


        }

        private void chcFilterTime90_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterTime90);

            AddResultNameToList("90 min", chcFilterTime90, panelFilterTime);
          //  UncheckedCheckBoxFilter(chcFilterTime90, panelFilterTime);
        }

        private void chcFilterTime900_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterTime900);

            AddResultNameToList("pow 90", chcFilterTime900, panelFilterTime);
            //UncheckedCheckBoxFilter(chcFilterTime900, panelFilterTime);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            btnFilter.BackColor = Color.Maroon;
            

                int j = 0;

                for (int i = 0; i < CheckBoxList.Count; i++)
                {
                    if (CheckBoxList[i].Checked == false) j++;

                }
                if (j == CheckBoxList.Count)
                {
                    DisplayFilter(nameColumnsGroup, checkBoxFilterName, nameColumnCheckBox);

                }
                else
                {
                    DisplayFilterWhenCheckboxMealChecked(nameColumnsGroup, nameColumnCheckBox, CheckBoxList, nameCheckBox);
                }

                Filter();


            //     Wypelnij();

        
        }
        List<CheckBox> checkBoxFilterName = new List<CheckBox>();


        public void DisplayFilter(string[] columnNameCategory, List<CheckBox> boxes, string[] columnName)
        {
            int i = 0;

            dgGrid.Rows.Clear();

            foreach (var item in checkBoxFilterName)
            {
                if (item.Checked)
                {
                    for (int j = 0; j < columnNameCategory.Length; j++)
                    {
                        fillGridFiltr(columnNameCategory[j], boxes[i], columnName[i], columnTime);
                        fillGridFiltr(columnNameCategory[j], boxes[i], columnName[i], columnLevel);
                        fillGridFiltr(columnNameCategory[j], boxes[i], columnName[i], columnRating);
                        fillGridFiltr(columnNameCategory[j], boxes[i], columnName[i], columnCuisine);
                        DeleteDuplicate();
                    }

                }

                if (i >= checkBoxFilterName.Count)
                {
                    break;
                }
                else
                {
                    i++;
                }
            }

        }


        public void DisplayFilterWhenCheckboxMealChecked(string[] columnNameCategory, string[] columnName, List<CheckBox> boxes, string[] checkBoxName)
        {
            int i = 0;

            dgGrid.Rows.Clear();

            foreach (var item in CheckBoxList)
            {
                if (item.Checked)
                {
                    for (int j = 0; j < columnNameCategory.Length; j++)
                    {
                        fillGridFilterMeal(columnNameCategory[j], columnName[i], boxes[i], checkBoxName[i], columnTime);
                        fillGridFilterMeal(columnNameCategory[j], columnName[i], boxes[i], checkBoxName[i], columnLevel);
                        fillGridFilterMeal(columnNameCategory[j], columnName[i], boxes[i], checkBoxName[i], columnRating);
                        fillGridFilterMeal(columnNameCategory[j], columnName[i], boxes[i], checkBoxName[i], columnCuisine);
                        DeleteDuplicate();
                    }

                }

                if (i >= CheckBoxList.Count)
                {
                    break;
                }
                else
                {
                    i++;
                }
            }

        }

        private void chcFilterCuisineAmerican_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisineAmerican);
            AddResultNameToList("amerykańska", chcFilterCuisineAmerican, panelFiltrCuisine);
        }

        private void chcFilterCuisinePolish_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            if(chcFilterCuisinePolish.Checked)
            {
                checkBoxFilterName.Add(chcFilterCuisinePolish);
            }
            else
            {
                checkBoxFilterName.Remove(chcFilterCuisinePolish);
            }
           
            AddResultNameToList("polska", chcFilterCuisinePolish, panelFiltrCuisine);
        }

        private void chcFilterCuisineHungarian_CheckedChanged(object sender, EventArgs e)
        {
            checkBoxFilterName.Add(chcFilterCuisineHungarian);
            AddResultNameToList("węgierska", chcFilterCuisineHungarian, panelFiltrCuisine);
        }

        private void chcFilterCuisinePortuguese_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisinePortuguese);
            AddResultNameToList("portugalska", chcFilterCuisinePortuguese, panelFiltrCuisine);
        }

        private void chcFilterCuisineFrench_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisineFrench);
            AddResultNameToList("francuska", chcFilterCuisineFrench, panelFiltrCuisine);
        }

        private void chcFilterCuisineAsian_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisineAsian);
            AddResultNameToList("azjatycka", chcFilterCuisineAsian, panelFiltrCuisine);
        }

        private void chcFilterCuisineItalian_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisineItalian);
            AddResultNameToList("włoska", chcFilterCuisineItalian, panelFiltrCuisine);
        }

        private void chcFilterCuisineGreek_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();

            checkBoxFilterName.Add(chcFilterCuisineGreek);
            AddResultNameToList("grecka", chcFilterCuisineGreek, panelFiltrCuisine);
        }

        private void chcFilterCuisineSpanish_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisineSpanish);
            AddResultNameToList("hiszpańska", chcFilterCuisineSpanish, panelFiltrCuisine);
        }

        private void chcFilterCuisineCzech_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterCuisineSpanish);
            AddResultNameToList("czeska", chcFilterCuisineSpanish, panelFiltrCuisine);
        }

        private void chcFilterLevelHard_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterLevelHard);
            AddResultNameToList("Średni", chcFilterLevelHard, panelFilterLevel);
        }

        private void chcFilterLevelVeryHard_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFilterLevelVeryHard);
            AddResultNameToList("Trudny", chcFilterLevelVeryHard, panelFilterLevel);
        }

        private void chcFilterRatingOne_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
        }

        private void chcFilterRatingTwo_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
        }

        private void chcFilterRatingThree_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
        }

        private void chcFiltrLevelEasy_CheckedChanged(object sender, EventArgs e)
        {
            ChangeButtonFilterColor();
            checkBoxFilterName.Add(chcFiltrLevelEasy);
            AddResultNameToList("Łatwy", chcFiltrLevelEasy, panelFilterLevel);
        }




        private void fillGridFilterMeal(string columnBase, string _propName, CheckBox _name, string columnName, List<string> panelColumn)
        {
            

            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll())
                {
                    for (int i = 0; i < panelColumn.Count; i++)
                    {

                        if ((int)RecipesBase.GetPropValue(r, _propName) == 1
                         && RecipesBase.GetPropValue(r, columnBase) == panelColumn[i])
                        {
                            if (panelColumn[i] == RecipesBase.GetPropValue(r, columnBase))
                            {
                                dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
                            }
                        }


                    }
                }
            }
            else
            {
                for (int i = dgGrid.RowCount - 1; i >= 0; i--)
                {
                    if (Convert.ToInt32(dgGrid.Rows[i].Cells[columnName].Value) == 1)
                        dgGrid.Rows.Remove(dgGrid.Rows[i]);
                }
            }
        }

        private void fillGridVege(string _propName)
        {
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells["Vegetarian"].Value) == 1 && dgGrid.Rows[i].Cells["Vegetarian"].ToString() == "Vegetarian")
                    dgGrid.Rows.Remove(dgGrid.Rows[i]);
            }

            if (chcVegetarian.Checked)
            {
                foreach (var r in RecipesBase.getAll())
                {
                    if ((int)RecipesBase.GetPropValue(r, _propName) == 1 && chcVegetarian.Checked)
                    {
                        dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
                    }
                }
            }
            else { }
        }

        public void ClearDataBase(int numberId)
        {
            try
            {
                if (MessageBox.Show("Czy na pewno usunąć Plik? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    var s = RecipesBase.getById(numberId);
                    RecipesBase.del(s.Id);

                    MessageBox.Show("Dokument został usunięty");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void NewForm()
        {
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
            bool add = true;

            Form2 NewForm = new Form2();

            NewForm.addRecipeForm2 = add;
            NewForm.addRecipe = true;

            searchEngine.FilldgGrid();

            this.Visible = false;
            NewForm.ShowDialog();
        }

        private void CleanFunctionClear()
        {
            if (dgGrid.Rows.Count < 1)
            {
                lblCleanDgGrid.Visible = false;
                lblLeftOneLine.Visible = false;
                lblLeftTwoLine.Visible = false;
            }
        }
        #endregion
    }
}
