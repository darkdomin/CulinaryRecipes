using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CulinaryRecipes.Properties;
using LiteDB;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Collections;
using System.Xml.XPath;
using System.Net.NetworkInformation;

namespace CulinaryRecipes
{
    public partial class Form1 : Form
    {
        int idDgGrid, NumberOfPortionsForm1;
        string ingredientForm1, instructionForm1, idRatingForm1, amountsOfIngredientsForm1;
        bool isAvailable = NetworkInterface.GetIsNetworkAvailable();

        public int counter = 0;
        Form2 stringOfCharactersForm2 = new Form2();

        int seekName;
        int[] idMeal = new int[7];
        int[] ingridients = new int[7];
        XmlSerializer xs;
        List<RecipesBase> ls;
        bool d = false;
        public Form1()
        {
            InitializeComponent();
            ls = new List<RecipesBase>();
            xs = new XmlSerializer(typeof(List<RecipesBase>));
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDataGridView();
            seekName = 1;
            CheckConnection();
        }

        #region Buttony
        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewForm();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Form2 OpenForm = new Form2();

            if (txtLittleName.Text == "") { }
            else
            {
                OpenForm.idDgGridForm2 = idDgGrid;
                OpenForm.titleForm2 = txtLittleName.Text;
                if (amountsOfIngredientsForm1 != null) OpenForm.amountsOfIngredientsForm2 = CleanDash(amountsOfIngredientsForm1);
                else OpenForm.amountsOfIngredientsForm2 = amountsOfIngredientsForm1;

                if (ingredientForm1 != null) OpenForm.ingredientForm2 = CleanDash(ingredientForm1);
                else OpenForm.ingredientForm2 = ingredientForm1;

                if (txtShortDescription.Text != null) OpenForm.ShortDescriptionForm2 = CleanDash(txtShortDescription.Text);
                else OpenForm.ShortDescriptionForm2 = txtShortDescription.Text;

                if (instructionForm1 != null) OpenForm.instructionForm2 = CleanDash(instructionForm1);
                else OpenForm.instructionForm2 = instructionForm1;

                OpenForm.NumberOfPortionsForm2 = NumberOfPortionsForm1;
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
                    else if (seek.Length > dgGrid.Rows[i].Cells[number].Value.ToString().Length)
                    {
                        dgGrid.Rows[i].Visible = false;
                    }
                    else if (grid.IndexOf(txtSeek.Text) >= 0)
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

        private void btnSeek_Click(object sender, EventArgs e)
        {
            Search(seekName);
        }
        #endregion

        #region Menu
        private void importujBazęToolStripMenuItem_Click(object sender, EventArgs e)
        {
            filldgGrid();
            dgGrid.Visible = false;
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
                            m.Id = r.Id, m.RecipesName = r.RecipesName, m.Ingredients = r.Ingredients, m.AmountsMeal = r.AmountsMeal, m.ShortDescription = r.ShortDescription, m.LongDescription = r.LongDescription, m.NumberPortions = r.NumberPortions, m.CategoryCuisines = r.CategoryCuisines, m.CategoryRating = r.CategoryRating, m.CategoryDifficultLevel = r.CategoryDifficultLevel, m.CategoryPreparationTime = r.CategoryPreparationTime, m.SnackMeal = r.SnackMeal, m.DinnerMeal = r.DinnerMeal, m.SoupMeal = r.SoupMeal, m.DessertMeal = r.DessertMeal, m.DrinkMeal = r.DrinkMeal, m.PreservesMeal = r.PreservesMeal, m.SaladMeal = r.SaladMeal, m.IdFishIngredients = r.IdFishIngredients, m.IdPastaIngredients = r.IdPastaIngredients, m.IdFruitsIngredients = r.IdFruitsIngredients, m.IdMuschroomsIngredients = r.IdMuschroomsIngredients, m.IdBirdIngredients = r.IdBirdIngredients, m.IdMeatIngredients = r.IdMeatIngredients, m.IdEggsIngredients = r.IdEggsIngredients, m.PhotoLinkLocation = r.PhotoLinkLocation, m.Vegetarian = r.Vegetarian);
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
            
        }

        private void exportujBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sciezka = saveFileDialog1.FileName;
                FileStream fs = new FileStream(sciezka, System.IO.FileMode.Create, FileAccess.Write);

                foreach (var r in RecipesBase.getAll())
                {
                    ls.Add(new RecipesBase(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation,r.Vegetarian));
                }
                xs.Serialize(fs, ls);
                fs.Close();
                MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            }
        }

        private void usuńBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno usunąć Bazę danych? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                RecipesBase.ClearDb();
                MessageBox.Show("Dokument został usunięty");
                filldgGrid();
            }
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
                odznaczanieCheckBox(chcSnack, "Snackcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("SnackMeal", chcSnack, "Snackcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDinner.Checked == false)
            {
                odznaczanieCheckBox(chcDinner, "Dinnercheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("DinnerMeal", chcDinner, "Dinnercheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();

        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSoup.Checked == false)
            {
                odznaczanieCheckBox(chcSoup, "Soupcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("SoupMeal", chcSoup, "Soupcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDessert.Checked == false)
            {
                odznaczanieCheckBox(chcDessert, "Dessertcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("DessertMeal", chcDessert, "Dessertcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            if (chcDrink.Checked == false)
            {
                odznaczanieCheckBox(chcDrink, "Drinkscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("DrinkMeal", chcDrink, "Drinkscheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPreserves.Checked == false)
            {
                odznaczanieCheckBox(chcPreserves, "Preservescheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("PreservesMeal", chcPreserves, "Preservescheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            if (chcSalad.Checked == false)
            {
                odznaczanieCheckBox(chcSalad, "Saladcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox");
            }
            else
            {
                fillGrid("SaladMeal", chcSalad, "Saladcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }
        #endregion

        #region CheckBoxIngridients
        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFish.Checked == false)
            {
                odznaczanieCheckBox(chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdFishIngredients", chcFish, "Fishcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            if (chcPasta.Checked == false)
            {
                odznaczanieCheckBox(chcPasta, "Pastacheckbox", chcFish, "Fishcheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdPastaIngredients", chcPasta, "Pastacheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            if (chcFruits.Checked == false)
            {
                odznaczanieCheckBox(chcFruits, "Fruitscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdFruitsIngredients", chcFruits, "Fruitscheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMuschrooms.Checked == false)
            {
                odznaczanieCheckBox(chcMuschrooms, "Mushroomscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            if (chcBird.Checked == false)
            {
                odznaczanieCheckBox(chcBird, "Birdcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcMeat, "Meatcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdBirdIngredients", chcBird, "Birdcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            if (chcMeat.Checked == false)
            {
                odznaczanieCheckBox(chcMeat, "Meatcheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcEggs, "Eggscheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdMeatIngredients", chcMeat, "Meatcheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            if (chcEggs.Checked == false)
            {
                odznaczanieCheckBox(chcEggs, "Eggscheckbox", chcFish, "Fishcheckbox", chcPasta, "Pastacheckbox", chcFruits, "Fruitscheckbox", chcMuschrooms, "Mushroomscheckbox", chcBird, "Birdcheckbox", chcMeat, "Meatcheckbox", chcSnack, "Snackcheckbox", chcDinner, "Dinnercheckbox", chcSoup, "Soupcheckbox", chcDessert, "Dessertcheckbox", chcDrink, "Drinkscheckbox", chcPreserves, "Preservescheckbox", chcSalad, "Saladcheckbox");
            }
            else
            {
                fillGrid("IdEggsIngredients", chcEggs, "Eggscheckbox");
                DeleteDuplikat();
            }
            CleanThumbnails();
        }
        #endregion

        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            deleteCMS.Visible = true;

            if (e.RowIndex >= 0)
            {
                idDgGrid = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[0].Value);
                txtLittleName.Text = dgGrid.Rows[e.RowIndex].Cells[1].Value.ToString();
                ingredientForm1 = dgGrid.Rows[e.RowIndex].Cells[2].Value.ToString();
                amountsOfIngredientsForm1 = dgGrid.Rows[e.RowIndex].Cells[3].Value.ToString();

                if (dgGrid.Rows[e.RowIndex].Cells[4].Value.ToString() == stringOfCharactersForm2.stringOfCharacters.ToString()) txtShortDescription.Text = CleanDash(txtShortDescription.Text);
                else txtShortDescription.Text = dgGrid.Rows[e.RowIndex].Cells[4].Value.ToString();

                instructionForm1 = dgGrid.Rows[e.RowIndex].Cells[5].Value.ToString();
                NumberOfPortionsForm1 = Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[6].Value);

                if (dgGrid.Rows[e.RowIndex].Cells[7].Value.ToString() == stringOfCharactersForm2.stringOfCharacters.ToString()) { }
                else lblCuisine.Text = dgGrid.Rows[e.RowIndex].Cells[7].Value.ToString();
                if (dgGrid.Rows[e.RowIndex].Cells[8].Value.ToString() == stringOfCharactersForm2.stringOfCharacters.ToString()) { }
                else idRatingForm1 = dgGrid.Rows[e.RowIndex].Cells[8].Value.ToString();

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
                if (dgGrid.Rows[e.RowIndex].Cells[25].Value.ToString() == stringOfCharactersForm2.stringOfCharacters.ToString()) pbLittlePhoto.Image = Resources.przepisy;
                else pbLittlePhoto.ImageLocation = dgGrid.Rows[e.RowIndex].Cells[25].Value.ToString();

                //wegetarianskie.checkbox=Convert.ToInt32(dgGrid.Rows[e.RowIndex].Cells[26].Value);
                pbLittlePhoto.Visible = true;
                txtLittleName.Visible = true;
                txtShortDescription.Visible = true;
                lblShortTime.Visible = true;
                lblShortLevel.Visible = true;
                lblCuisine.Visible = true;

                dgGrid.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.SlateGray;
            }
        }

        #region Function

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
                if (nameVariableForm1[i] == stringOfCharactersForm2.stringOfCharacters&& nameVariableForm1[i+1] == stringOfCharactersForm2.stringOfCharacters1)
                {
                    S[i] = ' ';
                    S[i+1] = ' ';
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
            }
            else
            {
                ////pbLittlePhoto.Visible = true;
                //txtLittleName.Visible = true;
                //txtShortDescription.Visible = true;
                //lblShortTime.Visible = true;
                //lblShortLevel.Visible = true;
                //lblCuisine.Visible = true;
                //pbStar1.Visible = true;
                //pbStar2.Visible = true;
                //pbStar3.Visible = true;
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
            // dgGrid.Columns.Add("Vegetarian","Vegetarian");

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
                dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian);
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

        private void DeleteDuplikat()
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

        private void odznaczanieCheckBox(CheckBox gl, string nameMain, CheckBox one, string name1, CheckBox two, string name2, CheckBox three, string name3, CheckBox four, string name4, CheckBox five, string name5, CheckBox six, string name6, CheckBox seven, string name7, CheckBox eight, string name8, CheckBox nine, string name9, CheckBox ten, string name10, CheckBox eleven, string name11, CheckBox twelve, string name12, CheckBox thirteen, string name13)
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
                        dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation,r.Vegetarian);
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
        #endregion
    }
}
