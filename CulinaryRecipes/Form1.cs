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
        int idDgGrid, numberOfPortionsForm1, searchName;
        string gramsColumnDgGridForm1, ingredientColumnDgGridForm1, descriptionForm1, idRatingForm1, amountsOfIngredientsForm1;
        bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
        public int counter = 0;
        public int[] idMeal = new int[7];
        public int[] ingridients = new int[8];
        public bool seekUnsubscribe = false;

        List<string> autoComp = new List<string>();
        public List<CheckBox> CheckBoxList = new List<CheckBox>();
        SearchEngine searchEngine;


        string[] nameCheckBox =
        {
            "Fishcheckbox","Pastacheckbox","Fruitscheckbox",
            "Mushroomscheckbox", "Birdcheckbox", "Meatcheckbox",
            "Eggscheckbox", "Vegetarian", "Snackcheckbox",
            "Dinnercheckbox", "Soupcheckbox", "Dessertcheckbox",
            "Drinkscheckbox", "Preservescheckbox", "Saladcheckbox"
        };

        string[] nameColumnCheckBox =
        {
            "IdFishIngredients","IdPastaIngredients","IdFruitsIngredients",
            "IdMuschroomsIngredients", "IdBirdIngredients", "IdMeatIngredients",
            "IdEggsIngredients", "Vegetarian", "SnackMeal",
            "DinnerMeal", "SoupMeal", "DessertMeal",
            "DrinkMeal", "PreservesMeal", "SaladMeal"
        };

        string[] nameColumnsGroup =
        {
            "CategoryPreparationTime","CategoryDifficultLevel","CategoryRating","CategoryCuisines"
        };

        Form2 stringOfCharactersForm2 = new Form2();

        public enum CreateDg
        {
            id,
            Name,
            Components,
            Amounts,
            ShortDescription,
            LongDescription,

            NumberPortions,
            CategoryCuisines,
            IdCategoryRating,
            IdcategoryDifficultLevel,

            IdcategoryPreparationTime,

            Snackcheckbox,
            Dinnercheckbox,
            Soupcheckbox,
            Dessertcheckbox,
            Drinkscheckbox,
            Preservescheckbox,
            Saladcheckbox,

            Fishcheckbox,
            Pastacheckbox,
            Fruitscheckbox,
            Mushroomscheckbox,
            Birdcheckbox,
            Meatcheckbox,
            Eggscheckbox,
            Photo,
            Vegetarian,
            Grams
        };
        public enum RecipesData
        {
            SnackMeal,
            DinnerMeal,
            SoupMeal,
            DessertMeal,
            DrinkMeal,
            PreservesMeal,
            SaladMeal,
            IdFishIngredients,
            IdPastaIngredients,
            IdFruitsIngredients,
            IdMuschroomsIngredients,
            IdBirdIngredients,
            IdMeatIngredients,
            IdEggsIngredients,
            PhotoLinkLocation,
            Vegetarian,
            Grams
        };

        XmlSerializer xs;
        List<RecipesBase> ls;

        public Form1()
        {
            InitializeComponent();
            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

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

            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            foreach (var r in RecipesBase.getAll("RecipesBase"))
            {
                txtSeek.AutoCompleteCustomSource.Add(r.RecipesName);
            }


            searchName = 1;
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
                searchEngine.Search(searchName);
                lblCleanVisibleFalse();
                unsubscribe = true;
                seekUnsubscribe = true;
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

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Application.Exit();
        }

        private void NewForm()
        {
            RichTextBoxMy p = new RichTextBoxMy();

            bool add = true;

            Form2 NewForm = new Form2();

            p.AddRecipeForm2 = add;
            NewForm.addRecipe = true;

            this.Visible = false;
            NewForm.ShowDialog();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewForm();
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) NewForm();
        }

        public void OpenClick()
        {
            Form2 OpenForm = new Form2();

            if (txtLittleName.Text != "")
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

        private void btnOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OpenClick();
        }

        private void btnSeek_Click(object sender, EventArgs e)
        {
            SearchGeneral();
            dgGrid.Focus();
            OneCliCK();
        }

        private void btnSeek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSeek.Text))
            {
                searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
                searchEngine.Search(searchName);
                lblCleanVisibleFalse();
            }
        }

        private void txtSeek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
                searchEngine.Search(searchName);

                lblCleanVisibleFalse();

                unsubscribe = true;
                seekUnsubscribe = true;

                OneCliCK(1);
            }
        }

        private void lblNameSeek_Click(object sender, EventArgs e)
        {
            searchName = 1;
            txtSeek.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            BoldAndSlim();
        }

        private void lblAmountsSeek_Click(object sender, EventArgs e)
        {
            searchName = 2;
            txtSeek.AutoCompleteMode = AutoCompleteMode.None;
            BoldAndSlim();
        }

        private void lblCleanDgGrid_Click(object sender, EventArgs e)
        {
            ClearDgGrid();
        }

        private void lblClearCheckBox_Click_1(object sender, EventArgs e)
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

            CMSdelete.Visible = false;
            CMSexportOne.Visible = false;
            CMSSend.Visible = false;
        }

        #endregion Buttony 

        #region Menu

        private void eksportujPojedynczyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportOneFile();
        }

        int NewId;
        private void importujPojedynczyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (var r in RecipesBase.getAll("RecipesBase"))
            {
                NewId = r.Id;
            }
            openFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string path = openFileDialog1.FileName;
                    FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                    ls = (List<RecipesBase>)xs.Deserialize(fs);


                    RecipesBase m = new RecipesBase();

                    foreach (var r in ls)
                    {
                        dgGrid.Rows.Add(
                        m.Id = NewId + 1, m.RecipesName = r.RecipesName, m.Ingredients = r.Ingredients, m.AmountsMeal = r.AmountsMeal, m.ShortDescription = r.ShortDescription, m.LongDescription = r.LongDescription, m.NumberPortions = r.NumberPortions, m.CategoryCuisines = r.CategoryCuisines, m.CategoryRating = r.CategoryRating, m.CategoryDifficultLevel = r.CategoryDifficultLevel, m.CategoryPreparationTime = r.CategoryPreparationTime, m.SnackMeal = r.SnackMeal, m.DinnerMeal = r.DinnerMeal, m.SoupMeal = r.SoupMeal, m.DessertMeal = r.DessertMeal, m.DrinkMeal = r.DrinkMeal, m.PreservesMeal = r.PreservesMeal, m.SaladMeal = r.SaladMeal, m.IdFishIngredients = r.IdFishIngredients, m.IdPastaIngredients = r.IdPastaIngredients, m.IdFruitsIngredients = r.IdFruitsIngredients, m.IdMuschroomsIngredients = r.IdMuschroomsIngredients, m.IdBirdIngredients = r.IdBirdIngredients, m.IdMeatIngredients = r.IdMeatIngredients, m.IdEggsIngredients = r.IdEggsIngredients, m.PhotoLinkLocation = r.PhotoLinkLocation, m.Vegetarian = r.Vegetarian, m.Grams = r.Grams);
                        RecipesBase.add(m);
                    }

                    searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
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
            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
            if (MessageBox.Show("Czy na pewno usunąć Bazę danych? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                RecipesBase.ClearDb();
                MessageBox.Show("Dokument został usunięty");
                searchEngine.FilldgGrid();
            }
            lblCleanVisibleFalse();
        }

        private void nowyPrzepisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewForm();
        }

        private void eksportujCalaBazeDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                FileStream fs = new FileStream(path, System.IO.FileMode.Create, FileAccess.Write);

                foreach (var r in RecipesBase.getAll("RecipesBase"))
                {
                    ls.Add(new RecipesBase(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams));
                }
                xs.Serialize(fs, ls);
                fs.Close();

                MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            }
        }

        private void importujCalaBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
                searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            dgGrid.Visible = false;
            searchEngine.FilldgGrid();

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
                        string path = openFileDialog1.FileName;

                        FileStream fs = new FileStream(path, System.IO.FileMode.Open, FileAccess.Read);
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
                            searchEngine.FilldgGrid();
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

                #region LinqJeszczeZly
            }
            #endregion

            lblCleanVisibleFalse();
            dgGrid.Visible = true;
        }

        private void oProgramieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 nowy = new Form4();
            nowy.ShowDialog();
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
            ClearDataBase(idDgGrid);
            searchEngine.FilldgGrid();
            Statistic();
        }

        private void CMSSend_Click(object sender, EventArgs e)
        {
            Logo show = new Logo();
            show.titleLogo = txtLittleName.Text;
            show.ingredientLogo = ingredientColumnDgGridForm1;
            show.amountsLogo = amountsOfIngredientsForm1;
            show.gramsLogo = gramsColumnDgGridForm1;
            show.descriptionLogo = descriptionForm1;
            show.Show();
            this.Hide();
        }

        #endregion Menu

        #region CheckBox

        private void AllCheckboxElse(string nameInDataBase, CheckBox nameThis, string nameColumnDgGrid, Label labelVeg, Label main)
        {
            txtSeek.Text = String.Empty;
            ClearSeek();

            fillGrid(nameInDataBase, nameThis, nameColumnDgGrid);
            UncheckCheckboxAfterPressingAnother(nameThis);
            FillDataGrdViewVegetarianRecipes(nameThis, nameColumnDgGrid);

            DisplayGreenVegetarianLabel(nameThis, labelVeg, main);

            DeleteDuplicate();
            unsubscribe = false;
            HideHeadres();
            seekUnsubscribe = false;

            ChangeButtonFilterColor();

            dgGrid.Focus();
            OneCliCK();


        }

        private void AllCheckboxAfterElse()
        {
            CleanThumbnails();
            CleanFunctionClear();
            CheckCheckBox();
        }

        private void AllCheckboxAfterIf(CheckBox nameThis, string nameColumnDgGrid, Label nameVeg, Label main)
        {
            DeleteRowsAfterUncheckCheckBox(nameThis, nameColumnDgGrid);
            RemoveVegetarianRecipesWithDataGridView(RecipesData.Vegetarian.ToString());

            DeleteDuplicate();
            HideLabelClearCheckBox();

            DisplayGreenVegetarianLabel(nameThis, nameVeg, main);
        }

        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcSnack, lblSnackVeg, lblSnack, CreateDg.Snackcheckbox.ToString(), RecipesData.SnackMeal.ToString());
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcDinner, lblDinnerVeg, lblDinner, CreateDg.Dinnercheckbox.ToString(), RecipesData.DinnerMeal.ToString());
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcSoup, lblSoupVeg, lblSoup, CreateDg.Soupcheckbox.ToString(), RecipesData.SoupMeal.ToString());
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcDessert, lblDessertVeg, lblDessert, CreateDg.Dessertcheckbox.ToString(), RecipesData.DessertMeal.ToString());
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcDrink, lblDrinksVeg, lblDrinks, CreateDg.Drinkscheckbox.ToString(), RecipesData.DrinkMeal.ToString());
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcPreserves, lblPrevervesVeg, lblPreserves, CreateDg.Preservescheckbox.ToString(), RecipesData.PreservesMeal.ToString());
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcSalad, lblSaladVeg, lblSalad, CreateDg.Saladcheckbox.ToString(), RecipesData.SaladMeal.ToString());
        }

        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcFish, lblFishVeg, lblFish, CreateDg.Fishcheckbox.ToString(), RecipesData.IdFishIngredients.ToString());
        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcPasta, lblPastaVeg, lblPasta, CreateDg.Pastacheckbox.ToString(), RecipesData.IdPastaIngredients.ToString());
        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcFruits, lblFruitsVeg, lblFruits, CreateDg.Fruitscheckbox.ToString(), RecipesData.IdFruitsIngredients.ToString());
        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcMuschrooms, lblMushroomVeg, lblMuschrooms, CreateDg.Mushroomscheckbox.ToString(), RecipesData.IdMuschroomsIngredients.ToString());
        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcBird, lblBirdVeg, lblBird, CreateDg.Birdcheckbox.ToString(), RecipesData.IdBirdIngredients.ToString());
        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcMeat, lblMeatVeg, lblMeat, CreateDg.Meatcheckbox.ToString(), RecipesData.IdMeatIngredients.ToString());
        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcEggs, lblEggsVeg, lblEggs, CreateDg.Eggscheckbox.ToString(), RecipesData.IdEggsIngredients.ToString());
        }

        private void fillGrid(string _propName, CheckBox _name, string nazwa)
        {
            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll("RecipesBase"))
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

        bool delete;
        private void DeleteRowsAfterUncheckCheckBox(CheckBox gl, string nameMain)
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

        #endregion Checkbox

        //całe do przerobienia
        #region Filtrowanie

        List<CheckBox> checkBoxFilterName = new List<CheckBox>();
        List<string> columnTime = new List<string>();
        List<string> columnLevel = new List<string>();
        List<string> columnRating = new List<string>();
        List<string> columnCuisine = new List<string>();

        //do zmiany
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

        private void fillGridFiltr(string _propName, CheckBox _name, string nazwa, List<string> column)
        {
            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll("RecipesBase"))
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
                    columnCuisine.Add(dgGrid.Rows[i].Cells[CreateDg.CategoryCuisines.ToString()].Value.ToString());
                }
                if (columnRating.Count == 0)
                {
                    rating = true;
                    columnRating.Add(dgGrid.Rows[i].Cells[CreateDg.IdCategoryRating.ToString()].Value.ToString());
                }
                if (columnLevel.Count == 0)
                {
                    level = true;
                    columnLevel.Add(dgGrid.Rows[i].Cells[CreateDg.IdcategoryDifficultLevel.ToString()].Value.ToString());
                }
                if (columnTime.Count == 0)
                {
                    time = true;
                    columnTime.Add(dgGrid.Rows[i].Cells[CreateDg.IdcategoryPreparationTime.ToString()].Value.ToString());
                }

                for (int l = 0; l < columnCuisine.Count; l++)
                {
                    for (int k = 0; k < columnRating.Count; k++)
                    {
                        for (int j = 0; j < columnLevel.Count; j++)
                        {
                            for (int m = 0; m < columnTime.Count; m++)
                            {
                                if (dgGrid.Rows[i].Cells[CreateDg.IdcategoryPreparationTime.ToString()].Value.ToString() == columnTime[m]
                                 && dgGrid.Rows[i].Cells[CreateDg.IdcategoryDifficultLevel.ToString()].Value.ToString() == columnLevel[j]
                                 && dgGrid.Rows[i].Cells[CreateDg.IdCategoryRating.ToString()].Value.ToString() == columnRating[k]
                                 && dgGrid.Rows[i].Cells[CreateDg.CategoryCuisines.ToString()].Value.ToString() == columnCuisine[l])
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

                if (rating) columnRating.Remove(columnRating[0]);
                if (level) columnLevel.Remove(columnLevel[0]);
                if (time) columnTime.Remove(columnTime[0]);
                if (cusines) columnCuisine.Remove(columnCuisine[0]);
            }
        }

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

        private void fillGridFilterMeal(string columnBase, string _propName, CheckBox _name, string columnName, List<string> panelColumn)
        {
            if (_name.Checked)
            {
                foreach (var r in RecipesBase.getAll("RecipesBase"))
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

        private void ChangeButtonFilterColor()
        {
            if (panelFilterTime.Visible == true)
            {
                btnFilter.BackColor = Color.Green;
            }
        }

        private void UncheckCheckBoxFilter(CheckBox check, Control set)
        {
            if (check.Checked == false)
            {
                check.Checked = false;
                checkBoxFilterName.Remove(check);
            }
            else
            {
                foreach (Control p in set.Controls)
                {
                    if (p is CheckBox && p != check)
                    {
                        ((CheckBox)p).Checked = false;
                        check.Checked = true;
                    }
                    else
                    {
                        checkBoxFilterName.Add(check);
                    }
                }
            }
        }

        private void FilterInPanel(CheckBox filterName, Panel panelName, string textInColumn)
        {
            ChangeButtonFilterColor();

            UncheckCheckBoxFilter(filterName, panelName);
            AddResultNameToList(textInColumn, filterName, panelName);
        }

        private void chcFilterCuisineAmerican_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineAmerican, panelFiltrCuisine, "amerykańska");
        }

        private void chcFilterCuisinePolish_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisinePolish, panelFiltrCuisine, "polska");
        }

        private void chcFilterCuisineHungarian_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineHungarian, panelFiltrCuisine, "węgierska");
        }

        private void chcFilterCuisinePortuguese_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisinePortuguese, panelFiltrCuisine, "portugalska");
        }

        private void chcFilterCuisineFrench_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineFrench, panelFiltrCuisine, "francuska");
        }

        private void chcFilterCuisineAsian_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineAsian, panelFiltrCuisine, "azjatycka");
        }

        private void chcFilterCuisineItalian_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineItalian, panelFiltrCuisine, "włoska");
        }

        private void chcFilterCuisineGreek_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineGreek, panelFiltrCuisine, "grecka");
        }

        private void chcFilterCuisineSpanish_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineSpanish, panelFiltrCuisine, "hiszpańska");
        }

        private void chcFilterCuisineCzech_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterCuisineCzech, panelFiltrCuisine, "czeska");
        }

        private void chcFiltrLevelEasy_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFiltrLevelEasy, panelFilterLevel, "Łatwy");
        }

        private void chcFilterLevelHard_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterLevelHard, panelFilterLevel, "Średni");
        }

        private void chcFilterLevelVeryHard_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterLevelVeryHard, panelFilterLevel, "Trudny");
        }

        private void chcFilterRatingOne_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterRatingOne, panelFilterRating, "1");
        }

        private void chcFilterRatingTwo_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterRatingTwo, panelFilterRating, "2");
        }

        private void chcFilterRatingThree_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterRatingThree, panelFilterRating, "3");
        }

        private void chcFilterTime30_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterTime30, panelFilterTime, "30 min");
        }

        private void chcFiltrTime60_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterTime60, panelFilterTime, "60 min");
        }

        private void chcFilterTime90_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterTime90, panelFilterTime, "90 min");
        }

        private void chcFilterTime900_CheckedChanged(object sender, EventArgs e)
        {
            FilterInPanel(chcFilterTime900, panelFilterTime, "pow 90");
        }

        private void btnFilterOpen_Click(object sender, EventArgs e)
        {
            if (panelFilterLevel.Visible == false)
            {
                panelFiltrCuisine.Visible = true;
                panelFilterLevel.Visible = true;
                panelFilterRating.Visible = true;
                panelFilterTime.Visible = true;

                btnFilter.Visible = true;
                btnFilter.BackColor = Color.Maroon;
                btnFilterOpen.Visible = false;
                btnFilterClose.Visible = true;
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
            btnFilter.BackColor = Color.Maroon;

            if (checkBoxFilterName.Count != 0)
            {

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
            }
            else
            {
                FillInAfterUncheckedVegetarian();
            }

            dgGrid.Focus();
            OneCliCK();
        }

        private void btnFilterClose_Click(object sender, EventArgs e)
        {
            Function.UncheckedCheckBox(panelFiltrCuisine);
            Function.UncheckedCheckBox(panelFilterLevel);
            Function.UncheckedCheckBox(panelFilterRating);
            Function.UncheckedCheckBox(panelFilterTime);

            panelFiltrCuisine.Visible = false;
            panelFilterLevel.Visible = false;
            panelFilterRating.Visible = false;
            panelFilterTime.Visible = false;
            btnFilter.Visible = false;
            btnFilterClose.Visible = false;
            btnFilterOpen.Visible = true;
        }

        #endregion Filtrowanie

        #region DatagridView

        RecipesBase eksportId = new RecipesBase();
        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK();
        }

        public void OneCliCK()
        {
            try
            {
                int row = dgGrid.CurrentCell.RowIndex;

                if (row >= 0)
                {
                    CMSdelete.Visible = true;
                    CMSexportOne.Visible = true;
                    CMSSend.Visible = true;

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

                    if (idRatingForm1 == "1")
                    {
                        pbStar1.Visible = true;
                        pbStar2.Visible = false;
                        pbStar3.Visible = false;
                    }
                    else if (idRatingForm1 == "2")
                    {
                        pbStar1.Visible = true;
                        pbStar2.Visible = true;
                        pbStar3.Visible = false;
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
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void OneCliCK(int id)
        {
            try
            {
                CMSdelete.Visible = true;
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
            catch (NullReferenceException ex)
            { }
        }

        private void dgGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK();
            OpenClick();
        }

        private void dgGrid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgGrid.Rows.Count > 0)
            {
                OneCliCK();
                OpenClick();
            }

        }

        #endregion DatagridView

        #region Function

        bool unsubscribe = false;
        private void SearchGeneral()
        {
            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            if (chcVegetarian.Checked) chcVegetarian.Checked = false;

            searchEngine.Search(searchName);

            lblCleanVisibleFalse();
            unsubscribe = true;
            seekUnsubscribe = true;

        }

        public void CheckboxLabelShow()
        {
            lblClearCheckBox.Visible = true;
            lblRightTwoLine.Visible = true;
            lblRightOneLine.Visible = true;
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

        private string CleanDash(string nameVariableForm1)
        {
            StringBuilder S = new StringBuilder(nameVariableForm1);
            string cos = nameVariableForm1;
            for (int i = 0; i < nameVariableForm1.Length; i++)
            {
                if (nameVariableForm1[i] == stringOfCharactersForm2.stringOfCharacters && nameVariableForm1[i + 1] == stringOfCharactersForm2.stringOfCharacters1)
                {
                    S.Remove(i, 0);
                    S.Remove(i + 1, 0);
                }
                else
                {
                    continue;
                }
            }
            return S.ToString();
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

        private void GetAndHideRecipes(CheckBox checkboxName, Label labelCheckboxNameVeg, Label labelCheckboxName, string elementInDataGrid, string elementInDataBase)
        {
            if (checkboxName.Checked == false)
            {
                AllCheckboxAfterIf(checkboxName, elementInDataGrid, labelCheckboxNameVeg, labelCheckboxName);
            }
            else
            {
                AllCheckboxElse(elementInDataBase, checkboxName, elementInDataGrid, labelCheckboxNameVeg, labelCheckboxName);
            }
            AllCheckboxAfterElse();
        }

        private void HideHeadres()
        {
            lblCleanDgGrid.Visible = false;
            lblLeftOneLine.Visible = false;
            lblLeftTwoLine.Visible = false;
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

        public void ClearSeek()
        {
            if (unsubscribe == true)
            {
                dgGrid.Rows.Clear();
            }
        }

        public void ClearDgGrid()
        {
            if (dgGrid.RowCount > 0)
            {
                dgGrid.Rows.Clear();
                lblCleanVisibleFalse();
            }

            CMSdelete.Visible = false;
            CMSexportOne.Visible = false;
        }

        //wyczysc obrazki i napisy
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

        //czcionka pogrubiona i na odwrót
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

        //funkcja do statystyk
        public void Statistic()
        {
            if (chcStstistic.Checked)
            {
                int count = 1;

                foreach (var p in RecipesBase.getAll("RecipesBase"))
                {
                    NewId = count;
                    count++;
                }

                lblStatistic.Text = NewId.ToString();
                lblStatistic.Visible = true;
                lblCulinary.Visible = true;
            }
            else
            {
                lblStatistic.Visible = false;
                lblCulinary.Visible = false;
            }
        }

        public void ExportOneFile()
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

        public void CreateDataGridView()
        {
            dgGrid.Rows.ToString().ToUpper();

            dgGrid.Columns.Add("id", "Id");
            dgGrid.Columns.Add("Name", "Name");
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

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void eksportujPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportOneFile();
        }

        private void exportujBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                FileStream fs = new FileStream(path, System.IO.FileMode.Create, FileAccess.Write);

                foreach (var r in RecipesBase.getAll("RecipesBase"))
                {
                    ls.Add(new RecipesBase(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams));
                }
                xs.Serialize(fs, ls);
                fs.Close();

                MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            }

            #region Linq

            //saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string path = saveFileDialog1.FileName;

            //    var document = new XDocument();
            //    var recipes = new XElement("CulinaryRecipes");

            //    var elements = from rekord in RecipesBase.getAll("RecipesBase")
            //                   select new XElement("RecipesBase",
            //                                              new XElement("Id", rekord.Id),
            //                                              new XAttribute("Name", rekord.RecipesName),
            //                                              new XAttribute("Ingredients", rekord.Ingredients),
            //                                              new XAttribute("Amounts", rekord.AmountsMeal),
            //                                              new XAttribute("Grams", rekord.Grams),
            //                                              new XAttribute("ShortDescription", rekord.ShortDescription),
            //                                              new XAttribute("LongDescription", rekord.LongDescription),

            //                                              new XAttribute("NumberPortions", rekord.NumberPortions),
            //                                              new XAttribute("CategoryCuisines", rekord.CategoryCuisines),
            //                                              new XAttribute("CategoryRating", rekord.CategoryRating),
            //                                              new XAttribute("CategoryDifficultLevel", rekord.CategoryDifficultLevel),
            //                                              new XAttribute("CategoryPreparationTime", rekord.CategoryPreparationTime),

            //                                              new XAttribute("SnackMeal", rekord.SnackMeal),
            //                                              new XAttribute("DinnerMeal", rekord.DinnerMeal),
            //                                              new XAttribute("SoupMeal", rekord.SoupMeal),                                       
            //                                              new XAttribute("DessertMeal", rekord.DessertMeal),
            //                                              new XAttribute("DrinkMeal", rekord.DrinkMeal),
            //                                              new XAttribute("PreservesMeal", rekord.PreservesMeal),
            //                                              new XAttribute("SaladMeal", rekord.SaladMeal),

            //                                              new XAttribute("FishIngredients", rekord.IdFishIngredients),
            //                                              new XAttribute("PastaIngredients", rekord.IdPastaIngredients),
            //                                              new XAttribute("FruitsIngredientspMeal", rekord.IdFruitsIngredients),
            //                                              new XAttribute("MuschroomsIngredients", rekord.IdMuschroomsIngredients),
            //                                              new XAttribute("BirdIngredients", rekord.IdBirdIngredients),
            //                                              new XAttribute("MeatIngredients", rekord.IdMeatIngredients),
            //                                              new XAttribute("EggsIngredients", rekord.IdEggsIngredients),

            //                                              new XAttribute("PhotoLinkLocation", rekord.PhotoLinkLocation),
            //                                              new XAttribute("Vegetarian", rekord.Vegetarian)
            //                                              );
            //    recipes.Add(elements);

            //    document.Add(recipes);
            //    document.Save(path);


            //MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            //}
            #endregion
        }

        #endregion Function

        #region vegetarian
        private void FillDataGrdViewVegetarianRecipes(CheckBox main, string nameMain)
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

        private void dgGrid_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down || e.KeyCode == Keys.Up)
            {
                OneCliCK();
            }
        }
        DataGridViewCellEventArgs a;
        private void txtSeek_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && txtSeek.Text != string.Empty)
            {
                SearchAndSelectTheGrid(a);
            }
            else if (txtSeek.Text == "" && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
            {
                SearchGeneral();
                dgGrid.Focus();
                OneCliCK();
                txtSeek.Focus();
            }

        }

        private void SearchAndSelectTheGrid(DataGridViewCellEventArgs a)
        {
            SearchGeneral();

            if(searchEngine.FilldgGrid(txtSeek.Text))
            {
                dgGrid.Focus();
                OneCliCK();

                txtSeek.Focus();
            }
        }

        private void RemoveVegetarianRecipesWithDataGridView(string _propName)
        {
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells[RecipesData.Vegetarian.ToString()].Value) == 1 && dgGrid.Rows[i].Cells[RecipesData.Vegetarian.ToString()].ToString() == RecipesData.Vegetarian.ToString())
                    dgGrid.Rows.Remove(dgGrid.Rows[i]);
            }

            if (chcVegetarian.Checked)
            {
                foreach (var r in RecipesBase.getAll("RecipesBase"))
                {
                    if ((int)RecipesBase.GetPropValue(r, _propName) == 1 && chcVegetarian.Checked)
                    {
                        dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
                    }
                }
            }
        }

        private void FillInAfterUncheckedVegetarianInternalMethod(CheckBox checkboxName, Label labelCheckboxNameVeg, Label labelCheckboxName, string elementInDataGrid, string elementInDataBase)
        {

            if (checkboxName.Checked)
            {
                fillGrid(elementInDataBase, checkboxName, elementInDataGrid);
                DisplayGreenVegetarianLabel(checkboxName, labelCheckboxNameVeg, labelCheckboxName);
            }
        }


        //do poprawy
        private void FillInAfterUncheckedVegetarian()
        {
            FillInAfterUncheckedVegetarianInternalMethod(chcFish, lblFishVeg, lblFish, CreateDg.Fishcheckbox.ToString(), RecipesData.IdFishIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcPasta, lblPastaVeg, lblPasta, CreateDg.Pastacheckbox.ToString(), RecipesData.IdPastaIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcFruits, lblFruitsVeg, lblFruits, CreateDg.Fruitscheckbox.ToString(), RecipesData.IdFruitsIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcMuschrooms, lblMushroomVeg, lblMuschrooms, CreateDg.Mushroomscheckbox.ToString(), RecipesData.IdMuschroomsIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcBird, lblBirdVeg, lblBird, CreateDg.Birdcheckbox.ToString(), RecipesData.IdBirdIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcMeat, lblMeatVeg, lblMeat, CreateDg.Meatcheckbox.ToString(), RecipesData.IdMeatIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcEggs, lblEggsVeg, lblEggs, CreateDg.Eggscheckbox.ToString(), RecipesData.IdEggsIngredients.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcSnack, lblSnackVeg, lblSnack, CreateDg.Snackcheckbox.ToString(), RecipesData.SnackMeal.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcDinner, lblDinnerVeg, lblDinner, CreateDg.Dinnercheckbox.ToString(), RecipesData.DinnerMeal.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcSoup, lblSoupVeg, lblSoup, CreateDg.Soupcheckbox.ToString(), RecipesData.SoupMeal.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcDessert, lblDessertVeg, lblDessert, CreateDg.Dessertcheckbox.ToString(), RecipesData.DessertMeal.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcDrink, lblDrinksVeg, lblDrinks, CreateDg.Drinkscheckbox.ToString(), RecipesData.DrinkMeal.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcPreserves, lblPrevervesVeg, lblPreserves, CreateDg.Preservescheckbox.ToString(), RecipesData.PreservesMeal.ToString());

            FillInAfterUncheckedVegetarianInternalMethod(chcSalad, lblSaladVeg, lblSalad, CreateDg.Saladcheckbox.ToString(), RecipesData.SaladMeal.ToString());

            if (unsubscribe == true)
            {
                //SearchEngine
                searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
                searchEngine.FilldgGrid();
                lblCleanVisibleFalse();
            }

            DeleteDuplicate();
        }

        private void UncheckCheckboxAfterPressingAnother(CheckBox main)
        {
            if (chcVegetarian.Checked)
            {
                for (int i = 0; i < CheckBoxList.Count; i++)
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

        public void DisplayGreenVegetarianLabel(CheckBox name, Label veg, Label main)
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

                DeleteRowsAfterUncheckCheckBox(chcVegetarian, RecipesData.Vegetarian.ToString());

                FillInAfterUncheckedVegetarian();

                HideLabelClearCheckBox();
            }
            else
            {

                Vegetarian.GreenLabel(chcVegetarian, lblVegetarian);

                int quantity = 0;
                bool delete2 = false;

                for (int i = 0; i < CheckBoxList.Count; i++)
                {
                    if (CheckBoxList[i].Checked) quantity++;
                    if (i == 14 && quantity > 2) //i = liczba checkboxów bez vegetarian - do poprawy bo napisane na sztywno
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
                    FillDataGrdViewVegetarianRecipes(chcFish, CreateDg.Fishcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcFish, lblFishVeg, lblFish);
                }
                else if (chcPasta.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcPasta, CreateDg.Pastacheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcPasta, lblPastaVeg, lblPasta);
                }
                else if (chcFruits.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcFruits, CreateDg.Fruitscheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcFruits, lblFruitsVeg, lblFruits);
                }
                else if (chcMuschrooms.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcMuschrooms, CreateDg.Mushroomscheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcMuschrooms, lblMushroomVeg, lblMuschrooms);
                }
                else if (chcBird.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcBird, CreateDg.Birdcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcBird, lblBirdVeg, lblBird);
                }
                else if (chcMeat.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcMeat, CreateDg.Meatcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcMeat, lblMeatVeg, lblMeat);
                }
                else if (chcEggs.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcEggs, CreateDg.Eggscheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcEggs, lblEggsVeg, lblEggs);
                }
                else if (chcSnack.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcSnack, CreateDg.Snackcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcSnack, lblSnackVeg, lblSnack);
                }
                else if (chcDinner.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcDinner, CreateDg.Dinnercheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcDinner, lblDinnerVeg, lblDinner);
                }
                else if (chcSoup.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcSoup, CreateDg.Soupcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcSoup, lblSoupVeg, lblSoup);
                }
                else if (chcDessert.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcDessert, CreateDg.Dessertcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcDessert, lblDessertVeg, lblDessert);
                }
                else if (chcDrink.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcDrink, CreateDg.Drinkscheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcDrink, lblDrinksVeg, lblDrinks);
                }
                else if (chcPreserves.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcPreserves, CreateDg.Preservescheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcPreserves, lblPrevervesVeg, lblPreserves);
                }
                else if (chcSalad.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcSalad, CreateDg.Saladcheckbox.ToString());
                    DisplayGreenVegetarianLabel(chcSalad, lblSaladVeg, lblSalad);
                }
                else
                {
                    ClearDgGrid();
                    fillGrid(RecipesData.Vegetarian.ToString(), chcVegetarian, RecipesData.Vegetarian.ToString());
                }
            }

            DeleteDuplicate();
            CleanThumbnails();
            CleanFunctionClear();
            CheckCheckBox();
        }

        #endregion Vegetarian
    }
}
