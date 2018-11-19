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
        string gramsForm1,ingredientForm1, instructionForm1, idRatingForm1, amountsOfIngredientsForm1;
        bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
        public int counter = 0;
        int[] idMeal = new int[7];
        int[] ingridients = new int[7];
        Form2 stringOfCharactersForm2 = new Form2();

        CheckBox[] proba = new CheckBox[2];
        

        XmlSerializer xs;
        List<RecipesBase> ls;

       
        public Form1()
        {
            InitializeComponent();
            ls = new List<RecipesBase>();
            xs = new XmlSerializer(typeof(List<RecipesBase>));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            proba[0] = chcDinner;
            proba[1] = chcMeat;
            CreateDataGridView();
            seekName =1;
            CheckConnection();
            Statistic();
          //  CleanThumbnails();
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
                //  if (gramsForm1 != null) OpenForm.gramsForm2 = CleanDash(gramsForm1);
                // else
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
        private void btnSeek_Click(object sender, EventArgs e)
        {
            Search(seekName);
        
            lblCleanVisibleFalse();

        }
        #endregion

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

                        ls.Add(new RecipesBase(eksportId.Id, eksportId.RecipesName, eksportId.Ingredients, eksportId.AmountsMeal, eksportId.ShortDescription, eksportId.LongDescription, eksportId.NumberPortions, eksportId.CategoryCuisines, eksportId.CategoryRating, eksportId.CategoryDifficultLevel, eksportId.CategoryPreparationTime, eksportId.SnackMeal, eksportId.DinnerMeal, eksportId.SoupMeal, eksportId.DessertMeal, eksportId.DrinkMeal, eksportId.PreservesMeal, eksportId.SaladMeal, eksportId.IdFishIngredients, eksportId.IdPastaIngredients, eksportId.IdFruitsIngredients, eksportId.IdMuschroomsIngredients, eksportId.IdBirdIngredients, eksportId.IdMeatIngredients, eksportId.IdEggsIngredients, eksportId.PhotoLinkLocation, eksportId.Vegetarian,eksportId.Grams));

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
                        m.Id = NewId + 1, m.RecipesName = r.RecipesName, m.Ingredients = r.Ingredients, m.AmountsMeal = r.AmountsMeal, m.ShortDescription = r.ShortDescription, m.LongDescription = r.LongDescription, m.NumberPortions = r.NumberPortions, m.CategoryCuisines = r.CategoryCuisines, m.CategoryRating = r.CategoryRating, m.CategoryDifficultLevel = r.CategoryDifficultLevel, m.CategoryPreparationTime = r.CategoryPreparationTime, m.SnackMeal = r.SnackMeal, m.DinnerMeal = r.DinnerMeal, m.SoupMeal = r.SoupMeal, m.DessertMeal = r.DessertMeal, m.DrinkMeal = r.DrinkMeal, m.PreservesMeal = r.PreservesMeal, m.SaladMeal = r.SaladMeal, m.IdFishIngredients = r.IdFishIngredients, m.IdPastaIngredients = r.IdPastaIngredients, m.IdFruitsIngredients = r.IdFruitsIngredients, m.IdMuschroomsIngredients = r.IdMuschroomsIngredients, m.IdBirdIngredients = r.IdBirdIngredients, m.IdMeatIngredients = r.IdMeatIngredients, m.IdEggsIngredients = r.IdEggsIngredients, m.PhotoLinkLocation = r.PhotoLinkLocation, m.Vegetarian = r.Vegetarian,m.Grams=r.Grams);
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
                ClearCheckBox(chcSnack, "Snackcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("SnackMeal", chcSnack, "Snackcheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            //foreach(CheckBox p in proba)
            //{
            //    if (p is CheckBox&&p.Checked)
            //    {

            //        // dgGrid.Rows.Add(p);
            //        dgGrid.Rows.Add(p).ToString();

            //    }
            //}

            //if (chcDinner.Checked == false)
            //{
             //  ClearCheckBox(chcDinner, "Dinnercheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            //}
            //else
            //{
            //    fillGrid("DinnerMeal", chcDinner, "Dinnercheckbox");
            //    DeleteDuplicate();
            //}
            //CleanThumbnails();
            //CleanFunctionClear();

           // cos(chcDinner, chcDinner, "Dinnercheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox", "DinnerMeal", chcDinner, "Dinnercheckbox");
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSoup.Checked == false)
            {
                ClearCheckBox(chcSoup, "Soupcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("SoupMeal", chcSoup, "Soupcheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDessert.Checked == false)
            {
                ClearCheckBox(chcDessert, "Dessertcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("DessertMeal", chcDessert, "Dessertcheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDrink.Checked == false)
            {
                ClearCheckBox(chcDrink, "Drinkscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("DrinkMeal", chcDrink, "Drinkscheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPreserves.Checked == false)
            {
                ClearCheckBox(chcPreserves, "Preservescheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("PreservesMeal", chcPreserves, "Preservescheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSalad.Checked == false)
            {
                ClearCheckBox(chcSalad, "Saladcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox");
            }
            else
            {
                fillGrid("SaladMeal", chcSalad, "Saladcheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }
        #endregion

        private void cos(CheckBox name, CheckBox gl, string nameMain, CheckBox one, string name1, CheckBox two, string name2, CheckBox three, string name3, CheckBox four, string name4, CheckBox five, string name5, CheckBox six, string name6, CheckBox seven, string name7, CheckBox eight, string name8, CheckBox nine, string name9, CheckBox ten, string name10, CheckBox eleven, string name11, CheckBox twelve, string name12, CheckBox thirteen, string name13, string _propName, CheckBox _name, string nazwa)
        {
            if (name.Checked == false)
            {
                ClearCheckBox(gl, nameMain, one, name1, two, name2, three, name3, four, name4, five, name5, six, name6, seven, name7, eight, name8, nine, name9, ten, name10, eleven, name11, twelve, name12, thirteen, name13);
            }
            else
            {
                fillGrid(_propName, _name, nazwa);
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }
        #region CheckBoxIngridients
        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            //if (chcFish.Checked == false)
            //{
            //    ClearCheckBox(chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            //}
            //else
            //{
            // fillGrid("IdFishIngredients", chcFish, "Fishcheckbox");
            //    DeleteDuplicate();
            //}
            //CleanThumbnails();
            //CleanFunctionClear();
            cos(chcFish, chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox", "IdFishIngredients", chcFish, "Fishcheckbox");
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPasta.Checked == false)
            {
                ClearCheckBox(chcPasta, "Pastacheckbox", chcFish, "Fishcheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdPastaIngredients", chcPasta, "Pastacheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFruits.Checked == false)
            {
                ClearCheckBox(chcFruits, "Fruitscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdFruitsIngredients", chcFruits, "Fruitscheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMuschrooms.Checked == false)
            {
                ClearCheckBox(chcMuschrooms, "Mushroomscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBird.Checked == false)
            {
                ClearCheckBox(chcBird, "Birdcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdBirdIngredients", chcBird, "Birdcheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMeat.Checked == false)
            {
                ClearCheckBox(chcMeat, "Meatcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdMeatIngredients", chcMeat, "Meatcheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            if (chcEggs.Checked == false)
            {
                ClearCheckBox(chcEggs, "Eggscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdEggsIngredients", chcEggs, "Eggscheckbox");
                DeleteDuplicate();
            }
            CleanThumbnails();
            CleanFunctionClear();
        }
        #endregion

        RecipesBase eksportId = new RecipesBase();
        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK(e);
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
                //lblCleanDgGrid.Visible = false;
                //lblLineOne.Visible = false;
                //lblLineTwo.Visible = false;
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

            for (int i = 0; i < dgGrid.ColumnCount; i++)//dgGrid.ColumnCount
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
                dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian,r.Grams);
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

        public void OneCliCK(DataGridViewCellEventArgs e)
        {
            deleteCMS.Visible = true;

            if (e.RowIndex >= 0)
            {
                idDgGrid = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[0].Value);
                eksportId = RecipesBase.getById(idDgGrid);
                txtLittleName.Text = dgGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                ingredientForm1 = dgGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                amountsOfIngredientsForm1 = dgGrid.Rows[e.RowIndex].Cells[3].Value.ToString();

                if (dgGrid.Rows[e.RowIndex].Cells[4].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString() +
                    stringOfCharactersForm2.stringOfCharacters1.ToString())
                    txtShortDescription.Text = CleanDash(txtShortDescription.Text);
                else
                {
                    txtShortDescription.Text = dgGrid.Rows[e.RowIndex].Cells[4].Value.ToString();
                }

                instructionForm1 = dgGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                numberOfPortionsForm1 = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[6].Value);

                if (dgGrid.Rows[e.RowIndex].Cells[7].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    lblCuisine.Text = dgGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
                }

                if (dgGrid.Rows[e.RowIndex].Cells[8].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    idRatingForm1 = dgGrid.Rows[e.RowIndex].Cells[8].Value.ToString();
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

                lblShortLevel.Text = dgGrid.Rows[e.RowIndex].Cells[9].Value.ToString();
                lblShortTime.Text = dgGrid.Rows[e.RowIndex].Cells[10].Value.ToString();

                idMeal[0] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[11].Value);
                idMeal[1] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[12].Value);
                idMeal[2] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[13].Value);
                idMeal[3] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[14].Value);
                idMeal[4] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[15].Value);
                idMeal[5] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[16].Value);
                idMeal[6] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[17].Value);

                ingridients[0] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[18].Value);
                ingridients[1] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[19].Value);
                ingridients[2] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[20].Value);
                ingridients[3] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[21].Value);
                ingridients[4] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[22].Value);
                ingridients[5] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[23].Value);
                ingridients[6] = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[24].Value);
                if (dgGrid.Rows[e.RowIndex].Cells[25].Value.ToString() ==
                    stringOfCharactersForm2.stringOfCharacters.ToString()) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = dgGrid.Rows[e.RowIndex].Cells[25].Value.ToString();

                //wegetarianskie.checkbox=Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[26].Value);
                if (dgGrid.Rows[e.RowIndex].Cells[27].Value.ToString() !=
                    stringOfCharactersForm2.stringOfCharacters.ToString())
                {
                    gramsForm1 = dgGrid.Rows[e.RowIndex].Cells[27].Value.ToString();
                }


                pbLittlePhoto.Visible = true;
                txtLittleName.Visible = true;
                txtShortDescription.Visible = true;
                lblShortTime.Visible = true;
                lblShortLevel.Visible = true;
                lblCuisine.Visible = true;

                dgGrid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SlateGray;
            }
        }

        private void dgGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK(e);
            OpenClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void btnSeek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter&&string.IsNullOrEmpty(txtSeek.Text))
            {
                Search(seekName);

                lblCleanVisibleFalse();
            }
        }

        private void dgGrid_KeyDown(object sender, KeyEventArgs e)
        {
           
            //if (e.KeyCode == Keys.Enter)
            //{
            //    OneCliCK(e);
            //    OpenClick();
            //}
            
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                NewForm();
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            Application.Exit();
        }

        private void btnOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OpenClick();
        }

      

        private void lblCleanDgGrid_Click(object sender, EventArgs e)
        {

            if (dgGrid.RowCount > 0 && chcFish.Checked == false && chcPasta.Checked == false && chcFruits.Checked == false && chcMuschrooms.Checked == false && chcBird.Checked == false && chcMeat.Checked == false && chcEggs.Checked == false && chcSnack.Checked == false && chcDinner.Checked == false && chcSoup.Checked == false && chcDessert.Checked == false && chcDrink.Checked == false && chcPreserves.Checked == false && chcFish.Checked == false)
            {
                dgGrid.Rows.Clear();
                lblCleanVisibleFalse();

                //lblCleanDgGrid.Visible = false;
                //lblLineOne.Visible = false;
                //lblLineTwo.Visible = false;
            }
            else if (chcFish.Checked)
            {
                ClearCheckBox(chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcPasta.Checked)
            {
                ClearCheckBox(chcPasta, "Pastacheckbox", chcFish, "Fishcheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcFruits.Checked)
            {
                ClearCheckBox(chcFruits, "Fruitscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcMuschrooms.Checked)
            {
                ClearCheckBox(chcMuschrooms, "Mushroomscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcBird.Checked)
            {
                ClearCheckBox(chcBird, "Birdcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcMeat.Checked)
            {
                ClearCheckBox(chcMeat, "Meatcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcEggs.Checked)
            {
                ClearCheckBox(chcEggs, "Eggscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcSnack.Checked)
            {
                ClearCheckBox(chcSnack, "Snackcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcDinner.Checked)
            {
                ClearCheckBox(chcDinner, "Dinnercheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcSoup.Checked)
            {
                ClearCheckBox(chcSoup, "Soupcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcDessert.Checked)
            {
                ClearCheckBox(chcDessert, "Dessertcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcDrink.Checked)
            {
                ClearCheckBox(chcDrink, "Drinkscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcPreserves.Checked)
            {
                ClearCheckBox(chcPreserves, "Preservescheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcSalad, "Saladcheckbox");
            }
            else if (chcSalad.Checked)
            {
                ClearCheckBox(chcSalad, "Saladcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox");
            }
            CleanThumbnails();
        }


        private void ClearCheckBox(CheckBox gl, string nameMain, CheckBox one, string name1, CheckBox two, string name2, CheckBox three, string name3, CheckBox four, string name4, CheckBox five, string name5, CheckBox six, string name6, CheckBox seven, string name7, CheckBox eight, string name8, CheckBox nine, string name9, CheckBox ten, string name10, CheckBox eleven, string name11, CheckBox twelve, string name12, CheckBox thirteen, string name13)
        {
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 0 && gl.Checked == false
                    || Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && gl.Checked ||
                    Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name1].Value) == 1 && one.Checked ||
                    Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name2].Value) == 1 && two.Checked ||
                    Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && gl.Checked ||
                    Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name1].Value) == 1 && one.Checked ||
                    Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name2].Value) == 1 && two.Checked ||

                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name3].Value) == 1 && three.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name4].Value) == 1 && four.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name5].Value) == 1 && five.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name6].Value) == 1 && six.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name7].Value) == 1 && seven.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name8].Value) == 1 && eight.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name9].Value) == 1 && nine.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name10].Value) == 1 && ten.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name11].Value) == 1 && eleven.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name12].Value) == 1 && twelve.Checked ||
                     Convert.ToInt32(dgGrid.Rows[i].Cells[nameMain].Value) == 1 && Convert.ToInt32(dgGrid.Rows[i].Cells[name13].Value) == 1 && thirteen.Checked)
                { continue; }
                else dgGrid.Rows.Remove(dgGrid.Rows[i]);
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
                        dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian,r.Grams);
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
