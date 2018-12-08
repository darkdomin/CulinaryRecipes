using CulinaryRecipes.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CulinaryRecipes
{
    public partial class Form1 : Form
    {
        int idDgGrid, numberOfPortionsForm1, seekName;
        string gramsForm1, ingredientForm1, instructionForm1, idRatingForm1, amountsOfIngredientsForm1;
        bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
        public int counter = 0;
        public int[] idMeal = new int[7];
        public int[] ingridients = new int[8];
        public bool seekUnsubscribe = false;
        bool seekUnsubscribeSupport = false;
        List<CheckBox> CheckBoxList = new List<CheckBox>();
        string[] nameCheckBox = { "Fishcheckbox", "Pastacheckbox", "Fruitscheckbox", "Mushroomscheckbox", "Birdcheckbox", "Meatcheckbox", "Eggscheckbox", "Vegetarian", "Snackcheckbox", "Dinnercheckbox", "Soupcheckbox", "Dessertcheckbox", "Drinkscheckbox", "Preservescheckbox", "Saladcheckbox" };

        Form2 stringOfCharactersForm2 = new Form2();

        CheckBox[] proba = new CheckBox[2];


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
            label16.Visible = false;
            label17.Visible = false;
        }

        public void CheckboxLabelShow()
        {
            lblClearCheckBox.Visible = true;
            label16.Visible = true;
            label17.Visible = true;
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
            proba[0] = chcDinner;
            proba[1] = chcMeat;
            CreateDataGridView();
            seekName = 1;
            CheckConnection();
            Statistic();
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
                Search(seekName);
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
                if (amountsOfIngredientsForm1 != null) OpenForm.amountsOfIngredientsForm2 = CleanDash(amountsOfIngredientsForm1);
                else OpenForm.amountsOfIngredientsForm2 = amountsOfIngredientsForm1;

                OpenForm.gramsForm2 = gramsForm1;
                if (ingredientForm1 != null) OpenForm.ingredientForm2 = CleanDash(ingredientForm1);
                else OpenForm.ingredientForm2 = ingredientForm1;

                if (txtShortDescription.Text != null) OpenForm.ShortDescriptionForm2 = CleanDash(txtShortDescription.Text);
                else OpenForm.ShortDescriptionForm2 = txtShortDescription.Text;

                if (instructionForm1 != null) OpenForm.instructionForm2 = CleanDash(instructionForm1);
                else OpenForm.instructionForm2 = instructionForm1;

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


                OpenForm.checkBoxesCancelForm2Ing[0] = chcFish.Checked;
                OpenForm.checkBoxesCancelForm2Ing[1] = chcPasta.Checked;
                OpenForm.checkBoxesCancelForm2Ing[2] = chcFruits.Checked;
                OpenForm.checkBoxesCancelForm2Ing[3] = chcMuschrooms.Checked;
                OpenForm.checkBoxesCancelForm2Ing[4] = chcBird.Checked;
                OpenForm.checkBoxesCancelForm2Ing[5] = chcMeat.Checked;
                OpenForm.checkBoxesCancelForm2Ing[6] = chcEggs.Checked;
                OpenForm.checkBoxesCancelForm2Ing[7] = chcVegetarian.Checked;

                OpenForm.checkBoxesCancelForm2Meal[0] = chcSnack.Checked;
                OpenForm.checkBoxesCancelForm2Meal[1] = chcDinner.Checked;
                OpenForm.checkBoxesCancelForm2Meal[2] = chcSoup.Checked;
                OpenForm.checkBoxesCancelForm2Meal[3] = chcDessert.Checked;
                OpenForm.checkBoxesCancelForm2Meal[4] = chcDrink.Checked;
                OpenForm.checkBoxesCancelForm2Meal[5] = chcPreserves.Checked;
                OpenForm.checkBoxesCancelForm2Meal[6] = chcSalad.Checked;
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

        private void Search(int number)
        {
            if (seekName == 1) txtSeek.Text = txtSeek.Text.ToUpper();

            StringBuilder seek = new StringBuilder(txtSeek.Text);
            filldgGrid();
            for (int i = 0; i < dgGrid.RowCount; i++)
            {
                if (seekName == 2)
                {
                    txtSeek.Text = txtSeek.Text.ToLower();
                    dgGrid.Rows[i].Cells[number].Value.ToString().ToLower();
                }
                String grid = dgGrid.Rows[i].Cells[number].Value.ToString();

                for (int j = 0; j < seek.Length; j++)
                {
                    if (txtSeek.Text == "")
                    {
                        filldgGrid();
                    }
                    else if (!dgGrid.Rows[i].Cells[number].Value.ToString().Contains(txtSeek.Text))
                    {
                        dgGrid.Rows[i].Visible = false;
                    }
                    else if (dgGrid.Rows[i].Cells[number].Value.ToString().Contains(txtSeek.Text))
                    {
                        continue;
                    }
                    else
                    {
                        dgGrid.Rows[i].Visible = false;
                    }
                }
            }
        }
        private void lblCleanVisibleFalse()
        {
            if (dgGrid.Rows.Count > 0)
            {
                lblCleanDgGrid.Visible = true;
                lblLineOne.Visible = true;
                lblLineTwo.Visible = true;
            }
            else
            {
                lblCleanDgGrid.Visible = false;
                lblLineOne.Visible = false;
                lblLineTwo.Visible = false;
            }
        }
        bool unsubscribe = false;

        private void btnSeek_Click(object sender, EventArgs e)
        {
            Search(seekName);

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
                    filldgGrid();
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
            if (MessageBox.Show("Czy na pewno usunąć Bazę danych? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                RecipesBase.ClearDb();
                MessageBox.Show("Dokument został usunięty");
                filldgGrid();
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
                AllCheckboxAfterIf(chcSnack, "Snackcheckbox");
            }
            else
            {
                AllCheckboxElse("SnackMeal", chcSnack, "Snackcheckbox");
            }
            AllCheckboxAfterElse();

        }

        private void AllCheckboxElse(string nameInDataBase,CheckBox nameThis, string nameColumnDgGrid)
        {
            ClearSeek();
            fillGrid(nameInDataBase, nameThis, nameColumnDgGrid);
            VegeDlaVege(nameThis);
            VegePoprawka(nameThis, nameColumnDgGrid);

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

        private void AllCheckboxAfterIf(CheckBox nameThis,string nameColumnDgGrid)
        {
            ClearCheckBox2(nameThis, nameColumnDgGrid);
            fillGridVege("Vegetarian");
            DeleteDuplicate();
            HideLabelClearCheckBox();
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {

            if (chcDinner.Checked == false)
            {
                AllCheckboxAfterIf(chcDinner, "Dinnercheckbox");
            }
            else
            {
                AllCheckboxElse("DinnerMeal", chcDinner, "Dinnercheckbox");
               
            }

            AllCheckboxAfterElse();
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSoup.Checked == false)
            {
                AllCheckboxAfterIf(chcSoup, "Soupcheckbox");
            }
            else
            {
                AllCheckboxElse("SoupMeal", chcSoup, "Soupcheckbox");

            }
            AllCheckboxAfterElse();
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDessert.Checked == false)
            {
                AllCheckboxAfterIf(chcDessert, "Dessertcheckbox");
            }
            else
            {
                AllCheckboxElse("DessertMeal", chcDessert, "Dessertcheckbox");
              
            }
            AllCheckboxAfterElse();
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDrink.Checked == false)
            {

                AllCheckboxAfterIf(chcDrink, "Drinkscheckbox");

            }
            else
            {
                AllCheckboxElse("DrinkMeal", chcDrink, "Drinkscheckbox");
              
            }
            AllCheckboxAfterElse();
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPreserves.Checked == false)
            {
                AllCheckboxAfterIf(chcPreserves, "Preservescheckbox");
            }
            else
            {
               
                AllCheckboxElse("PreservesMeal", chcPreserves, "Preservescheckbox");
               
            }
            AllCheckboxAfterElse();
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSalad.Checked == false)
            {
                AllCheckboxAfterIf(chcSalad, "Saladcheckbox");
            }
            else
            {
                AllCheckboxElse("SaladMeal", chcSalad, "Saladcheckbox");
                
            }
            AllCheckboxAfterElse();
        }
        #endregion


        #region CheckBoxIngridients
        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFish.Checked == false)
            {
                AllCheckboxAfterIf(chcFish, "Fishcheckbox");
            }
            else
            {
                AllCheckboxElse("IdFishIngredients", chcFish, "Fishcheckbox");

            }
            AllCheckboxAfterElse();
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPasta.Checked == false)
            {

                AllCheckboxAfterIf(chcPasta, "Pastacheckbox");
            }
            else
            {
                AllCheckboxElse("IdPastaIngredients", chcPasta, "Pastacheckbox");
              
            }
            AllCheckboxAfterElse();
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFruits.Checked == false)
            {
                AllCheckboxAfterIf(chcFruits, "Fruitscheckbox");
            }
            else
            {
                AllCheckboxElse("IdFruitsIngredients", chcFruits, "Fruitscheckbox"); 
             
            }
            AllCheckboxAfterElse();
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMuschrooms.Checked == false)
            {
                AllCheckboxAfterIf(chcMuschrooms, "Mushroomscheckbox");
            }
            else
            {
                AllCheckboxElse("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox");
              
            }
            AllCheckboxAfterElse();
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBird.Checked == false)
            {
                AllCheckboxAfterIf(chcBird, "Birdcheckbox");
            }
            else
            {
                AllCheckboxElse("IdBirdIngredients", chcBird, "Birdcheckbox");      
               
            }
            AllCheckboxAfterElse();
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMeat.Checked == false)
            {
                AllCheckboxAfterIf(chcMeat, "Meatcheckbox");
            }
            else
            {
                AllCheckboxElse("IdMeatIngredients", chcMeat, "Meatcheckbox");
              
            }
            AllCheckboxAfterElse();
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            if (chcEggs.Checked == false)
            {
                AllCheckboxAfterIf(chcEggs, "Eggscheckbox");
            }
            else
            {
                AllCheckboxElse("IdEggsIngredients", chcEggs, "Eggscheckbox");
               
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
            lblLineOne.Visible = false;
            lblLineTwo.Visible = false;
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
            if (seekName == 1)
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

        private void CreateDataGridView()
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

        private void filldgGrid()
        {
            dgGrid.Rows.Clear();
            foreach (var r in RecipesBase.getAll())
            {
                dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
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
            ClearDataBase(idDgGrid);
            filldgGrid();
            Statistic();
        }

        private void label12_Click(object sender, EventArgs e)
        {
            seekName = 1;
            BoldAndSlim();
        }

        int seekAmounts;
        private void label13_Click(object sender, EventArgs e)
        {
            seekName = 2;
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
            dgGrid.Visible = false;
            filldgGrid();

            if (dgGrid.Rows.Count > 0) { MessageBox.Show("Zanim Zaimportujesz plik wyczyść bazę danych. - Plik - Usuń bazę danych"); }
            else
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
                    }
                    else
                    {
                        MessageBox.Show("Baza danych przed importem musi być pusta");
                    }
                    fs.Close();
                }
            }
            lblCleanVisibleFalse();
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

        public void OneCliCK()
        {

            deleteCMS.Visible = true;
            int row = dgGrid.CurrentCell.RowIndex;
            if (row >= 0)
            {

                idDgGrid = Convert.ToInt32(dgGrid.Rows[row].Cells[0].Value);
                eksportId = RecipesBase.getById(idDgGrid);
                txtLittleName.Text = dgGrid.Rows[row].Cells[1].Value.ToString();
                ingredientForm1 = dgGrid.Rows[row].Cells[2].Value.ToString();
                amountsOfIngredientsForm1 = dgGrid.Rows[row].Cells[3].Value.ToString();

                if (dgGrid.Rows[row].Cells[4].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString() +
                    stringOfCharactersForm2.stringOfCharacters1.ToString())
                    txtShortDescription.Text = CleanDash(txtShortDescription.Text);
                else
                {
                    txtShortDescription.Text = dgGrid.Rows[row].Cells[4].Value.ToString();
                }

                instructionForm1 = dgGrid.Rows[row].Cells[5].Value.ToString();
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

                idMeal[0] = Convert.ToInt32(dgGrid.Rows[row].Cells[11].Value);
                idMeal[1] = Convert.ToInt32(dgGrid.Rows[row].Cells[12].Value);
                idMeal[2] = Convert.ToInt32(dgGrid.Rows[row].Cells[13].Value);
                idMeal[3] = Convert.ToInt32(dgGrid.Rows[row].Cells[14].Value);
                idMeal[4] = Convert.ToInt32(dgGrid.Rows[row].Cells[15].Value);
                idMeal[5] = Convert.ToInt32(dgGrid.Rows[row].Cells[16].Value);
                idMeal[6] = Convert.ToInt32(dgGrid.Rows[row].Cells[17].Value);

                ingridients[0] = Convert.ToInt32(dgGrid.Rows[row].Cells[18].Value);
                ingridients[1] = Convert.ToInt32(dgGrid.Rows[row].Cells[19].Value);
                ingridients[2] = Convert.ToInt32(dgGrid.Rows[row].Cells[20].Value);
                ingridients[3] = Convert.ToInt32(dgGrid.Rows[row].Cells[21].Value);
                ingridients[4] = Convert.ToInt32(dgGrid.Rows[row].Cells[22].Value);
                ingridients[5] = Convert.ToInt32(dgGrid.Rows[row].Cells[23].Value);
                ingridients[6] = Convert.ToInt32(dgGrid.Rows[row].Cells[24].Value);
                if (dgGrid.Rows[row].Cells[25].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString()) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = dgGrid.Rows[row].Cells[25].Value.ToString();

                ingridients[7] = Convert.ToInt32(dgGrid.Rows[row].Cells[26].Value);
                if (dgGrid.Rows[row].Cells[27].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    gramsForm1 = dgGrid.Rows[row].Cells[27].Value.ToString();
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
                ingredientForm1 = dgGrid.Rows[row].Cells[2].Value.ToString();
                amountsOfIngredientsForm1 = dgGrid.Rows[row].Cells[3].Value.ToString();

                if (dgGrid.Rows[row].Cells[4].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString() +
                    stringOfCharactersForm2.stringOfCharacters1.ToString())
                    txtShortDescription.Text = CleanDash(txtShortDescription.Text);
                else
                {
                    txtShortDescription.Text = dgGrid.Rows[row].Cells[4].Value.ToString();
                }

                instructionForm1 = dgGrid.Rows[row].Cells[5].Value.ToString();
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

                idMeal[0] = Convert.ToInt32(dgGrid.Rows[row].Cells[11].Value);
                idMeal[1] = Convert.ToInt32(dgGrid.Rows[row].Cells[12].Value);
                idMeal[2] = Convert.ToInt32(dgGrid.Rows[row].Cells[13].Value);
                idMeal[3] = Convert.ToInt32(dgGrid.Rows[row].Cells[14].Value);
                idMeal[4] = Convert.ToInt32(dgGrid.Rows[row].Cells[15].Value);
                idMeal[5] = Convert.ToInt32(dgGrid.Rows[row].Cells[16].Value);
                idMeal[6] = Convert.ToInt32(dgGrid.Rows[row].Cells[17].Value);

                ingridients[0] = Convert.ToInt32(dgGrid.Rows[row].Cells[18].Value);
                ingridients[1] = Convert.ToInt32(dgGrid.Rows[row].Cells[19].Value);
                ingridients[2] = Convert.ToInt32(dgGrid.Rows[row].Cells[20].Value);
                ingridients[3] = Convert.ToInt32(dgGrid.Rows[row].Cells[21].Value);
                ingridients[4] = Convert.ToInt32(dgGrid.Rows[row].Cells[22].Value);
                ingridients[5] = Convert.ToInt32(dgGrid.Rows[row].Cells[23].Value);
                ingridients[6] = Convert.ToInt32(dgGrid.Rows[row].Cells[24].Value);
                if (dgGrid.Rows[row].Cells[25].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString()) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = dgGrid.Rows[row].Cells[25].Value.ToString();

                ingridients[7] = Convert.ToInt32(dgGrid.Rows[row].Cells[26].Value);
                if (dgGrid.Rows[row].Cells[27].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    gramsForm1 = dgGrid.Rows[row].Cells[27].Value.ToString();
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
            if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSeek.Text))
            {
                Search(seekName);

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
            if (chcFish.Checked) fillGrid("IdFishIngredients", chcFish, "Fishcheckbox");
            if (chcPasta.Checked) fillGrid("IdPastaIngredients", chcPasta, "Pastacheckbox");
            if (chcFruits.Checked) fillGrid("IdFruitsIngredients", chcFruits, "Fruitscheckbox");
            if (chcMuschrooms.Checked) fillGrid("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox");
            if (chcBird.Checked) fillGrid("IdBirdIngredients", chcBird, "Birdcheckbox");
            if (chcMeat.Checked) fillGrid("IdMeatIngredients", chcMeat, "Meatcheckbox");
            if (chcEggs.Checked) fillGrid("IdEggsIngredients", chcEggs, "Eggscheckbox");

            if (chcSnack.Checked) fillGrid("SnackMeal", chcSnack, "Snackcheckbox");
            if (chcDinner.Checked) fillGrid("DinnerMeal", chcDinner, "Dinnercheckbox");
            if (chcSoup.Checked) fillGrid("SoupMeal", chcSoup, "Soupcheckbox");
            if (chcDessert.Checked) fillGrid("DessertMeal", chcDessert, "Dessertcheckbox");
            if (chcDrink.Checked) fillGrid("DrinkMeal", chcDrink, "Drinkscheckbox");

            if (chcPreserves.Checked) fillGrid("PreservesMeal", chcPreserves, "Preservescheckbox");
            if (chcSalad.Checked) fillGrid("SaladMeal", chcSalad, "Saladcheckbox");
        }
        private void VegeDlaVege(CheckBox main)
        {
            for (int i = 0; i < CheckBoxList.Count; i++)
            {
                if (chcVegetarian.Checked)
                {
                    if (CheckBoxList[i] == main) continue;
                    else {
                        CheckBoxList[i].Checked = false;
                        chcVegetarian.Checked = true;
                        main.Checked = true;
                    }  
                }
            }
           
        }

        private void chcVegetarian_CheckedChanged(object sender, EventArgs e)
        {
         
            if (chcVegetarian.Checked == false)
            {
               
                ClearCheckBox2(chcVegetarian, "Vegetarian");

                Wypelnij();

                DeleteDuplicate();
                HideLabelClearCheckBox();
            }
            else
            {
                int quantity=0;
                bool delete2 = false;
               
                for (int i = 0; i < CheckBoxList.Count; i++)
                {

                    if (CheckBoxList[i].Checked) quantity++;
                    if (i==14 && quantity>2) //pamietaj w razie zmiany ilosci checkboxow
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
                

                if (chcFish.Checked) { VegePoprawka(chcFish, "Fishcheckbox");   }
                else if (chcPasta.Checked) VegePoprawka(chcPasta, "Pastacheckbox");
                else if (chcFruits.Checked) VegePoprawka(chcFruits, "Fruitscheckbox");
                else if (chcMuschrooms.Checked) VegePoprawka(chcMuschrooms, "Mushroomscheckbox");
                else if (chcBird.Checked) VegePoprawka(chcBird, "Birdcheckbox");
                else if (chcMeat.Checked) VegePoprawka(chcMeat, "Meatcheckbox");
                else if (chcEggs.Checked) VegePoprawka(chcEggs, "Eggscheckbox");

                else if (chcSnack.Checked) VegePoprawka(chcSnack, "Snackcheckbox");
                else if (chcDinner.Checked) { VegePoprawka(chcDinner, "Dinnercheckbox"); }
                else if (chcSoup.Checked) VegePoprawka(chcSoup, "Soupcheckbox");
                else if (chcDessert.Checked) VegePoprawka(chcPreserves, "Preservescheckbox");
                else if (chcDrink.Checked) VegePoprawka(chcSalad, "Saladcheckbox");

                else if (chcPreserves.Checked) fillGrid("PreservesMeal", chcPreserves, "Preservescheckbox");
                else if (chcSalad.Checked) fillGrid("SaladMeal", chcSalad, "Saladcheckbox");

                else fillGrid("Vegetarian", chcVegetarian, "Vegetarian");
                    DeleteDuplicate();
                }

            CleanThumbnails();
            CleanFunctionClear();
            CheckCheckBox();
        }

        private void lblCleanDgGrid_Click(object sender, EventArgs e)
        {
            if (dgGrid.RowCount > 0)
            {
                dgGrid.Rows.Clear();
                lblCleanVisibleFalse();
            }
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
                    label16.Visible = true;
                    label17.Visible = true;
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
            label16.Visible = false;
            label17.Visible = false;
        }

        private void txtSeek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                Search(seekName);

                lblCleanVisibleFalse();
                unsubscribe = true;
                seekUnsubscribe = true;
                seekUnsubscribeSupport = true;
                OneCliCK(1);
            }
        }

        bool proba2;
        private void VegePoprawka(CheckBox main,string nameMain)
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
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells[nazwa].Value) == 1 && dgGrid.Rows[i].Cells[nazwa].ToString() == nazwa)
                    dgGrid.Rows.Remove(dgGrid.Rows[i]);
            }
            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll())
                {
                    if ((int)RecipesBase.GetPropValue(r, _propName) == 1 && _name.Checked)
                    {
                        dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
                    }
                }
            }
            else { }
        }

        private void fillGridVege(string _propName)
        {
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells["Vegetarian"].Value) == 1 && dgGrid.Rows[i].Cells["Vegetarian"].ToString() == "Vegetarian")//
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
            Form2 NewForm = new Form2();
            filldgGrid();
            this.Visible = false;

            NewForm.ShowDialog();
        }

        private void CleanFunctionClear()
        {
            if (dgGrid.Rows.Count < 1)
            {
                lblCleanDgGrid.Visible = false;
                lblLineOne.Visible = false;
                lblLineTwo.Visible = false;
            }
        }
        #endregion
    }
}
