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
using LiteDB;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.Linq;
using System.Xml;

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
        List<string> autoComp = new List<string>();
        public List<CheckBox> CheckBoxList = new List<CheckBox>();

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

        XmlSerializer xs;
        List<RecipesBase> ls;

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

        public void cos(string text)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");

          //  var proba = from p in RecipesBase.ParsujCSV()
                        //where p.RecipesName == text
                        //select p;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDataGridView();
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

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
                seekUnsubscribeSupport = true;
            }
        }

        #region Buttony i cały srodek

        private void txtSeek_TextChanged(object sender, EventArgs e)
        {




        }

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Statistic();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            //Application.Exit();
       
        }

        private void btnClose_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                Application.Exit();
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

        private void btnNew_Click(object sender, EventArgs e)
        {
            NewForm();
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
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

        private void btnOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                OpenClick();
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

        private void btnSeek_KeyDown(object sender, KeyEventArgs e)
        {
            SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
            if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSeek.Text))
            {
                searchEngine.Search(searchName);

                lblCleanVisibleFalse();
            }
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

        private void lblNameSeek_Click(object sender, EventArgs e)
        {
            searchName = 1;
            BoldAndSlim();
        }

        private void lblAmountsSeek_Click(object sender, EventArgs e)
        {
            searchName = 2;
            BoldAndSlim();
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

        public void CleanDgGrid()
        {
            if (dgGrid.RowCount > 0)
            {
                dgGrid.Rows.Clear();
                lblCleanVisibleFalse();
            }

            CMSdelete.Visible = false;
            CMSexportOne.Visible = false;
        }

        private void lblCleanDgGrid_Click(object sender, EventArgs e)
        {
            CleanDgGrid();
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

        #endregion Buttony i cały srodek

        #region Menu
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

        private void eksportujCalaBazeDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string sciezka = saveFileDialog1.FileName;
                FileStream fs = new FileStream(sciezka, System.IO.FileMode.Create, FileAccess.Write);

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

                #region LinqJeszczeZly
                //try
                //{
                //    openFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

                //    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                //    {
                //        string path = openFileDialog1.FileName;
                //        var document = XDocument.Load(path);

                //        var zapytanie = from element in document.Element("CulinaryRecipes").Elements("RecipesBase")
                //                        select new
                //                        {
                //                          // Id = element.Attribute("Id").Value,
                //                            RecipesName = element.Attribute("Name").Value,
                //                            Ingredients = element.Attribute("Ingredients").Value,
                //                            AmountsMeal = element.Attribute("Amounts").Value,
                //                            Grams = element.Attribute("Grams").Value,
                //                            ShortDescription = element.Attribute("ShortDescription").Value,
                //                            LongDescription = element.Attribute("LongDescription").Value,

                //                            NumberPortions = element.Attribute("NumberPortions").Value,
                //                            CategoryCuisines = element.Attribute("CategoryCuisines").Value,
                //                            CategoryRating = element.Attribute("CategoryRating").Value,
                //                            CategoryDifficultLevel = element.Attribute("CategoryDifficultLevel").Value,
                //                            CategoryPreparationTime = element.Attribute("CategoryPreparationTime").Value,

                //                            SnackMeal = element.Attribute("SnackMeal").Value,
                //                            DinnerMeal = element.Attribute("DinnerMeal").Value,
                //                            SoupMeal = element.Attribute("SoupMeal").Value,
                //                            DessertMeal = element.Attribute("DessertMeal").Value,
                //                            DrinkMeal = element.Attribute("DrinkMeal").Value,
                //                            PreservesMeal = element.Attribute("PreservesMeal").Value,
                //                            SaladMeal = element.Attribute("SaladMeal").Value,

                //                            IdFishIngredients = element.Attribute("FishIngredients").Value,
                //                            IdPastaIngredients = element.Attribute("PastaIngredients").Value,
                //                            IdFruitsIngredients = element.Attribute("FruitsIngredientspMeal").Value,
                //                            IdMuschroomsIngredients = element.Attribute("MuschroomsIngredients").Value,
                //                            IdBirdIngredients = element.Attribute("BirdIngredients").Value,
                //                            IdMeatIngredients = element.Attribute("MeatIngredients").Value,
                //                            IdEggsIngredients = element.Attribute("EggsIngredients").Value,

                //                            PhotoLinkLocation = element.Attribute("PhotoLinkLocation").Value,
                //                            Vegetarian = element.Attribute("Vegetarian").Value
                //                        };


                //        RecipesBase m = new RecipesBase();
                //        if (dgGrid.Rows.Count <= 0)
                //        {

                //            foreach (var r in zapytanie)
                //            {   

                //                dgGrid.Rows.Add(

                //                  m.RecipesName = r.RecipesName,
                //                  m.Ingredients = r.Ingredients,
                //                  m.AmountsMeal = r.AmountsMeal,
                //                  m.Grams = r.Grams,
                //                  m.ShortDescription = r.ShortDescription,
                //                  m.LongDescription = r.LongDescription,
                //                  m.NumberPortions = int.Parse(r.NumberPortions),
                //                  m.CategoryCuisines = r.CategoryCuisines,
                //                  m.CategoryRating = r.CategoryRating,
                //                  m.CategoryDifficultLevel = r.CategoryDifficultLevel,
                //                  m.CategoryPreparationTime = r.CategoryPreparationTime,
                //                  m.SnackMeal = int.Parse(r.SnackMeal),
                //                  m.DinnerMeal = int.Parse(r.DinnerMeal),
                //                  m.SoupMeal = int.Parse(r.SoupMeal),
                //                  m.DessertMeal = int.Parse(r.DessertMeal),
                //                  m.DrinkMeal = int.Parse(r.DrinkMeal),
                //                  m.PreservesMeal = int.Parse(r.PreservesMeal),
                //                  m.SaladMeal = int.Parse(r.SaladMeal),
                //                  m.IdFishIngredients = int.Parse(r.IdFishIngredients),
                //                  m.IdPastaIngredients = int.Parse(r.IdPastaIngredients),
                //                  m.IdFruitsIngredients = int.Parse(r.IdFruitsIngredients),
                //                  m.IdMuschroomsIngredients = int.Parse(r.IdMuschroomsIngredients),
                //                  m.IdBirdIngredients = int.Parse(r.IdBirdIngredients),
                //                  m.IdMeatIngredients = int.Parse(r.IdMeatIngredients),
                //                  m.IdEggsIngredients = int.Parse(r.IdEggsIngredients),
                //                  m.PhotoLinkLocation = r.PhotoLinkLocation,
                //                  m.Vegetarian = int.Parse(r.Vegetarian));

                //                RecipesBase.add(m);
                //                m.Id ++;
                //            }
                //            MessageBox.Show("Baza danych została zaimportowana");

                //            dgGrid.Visible = true;
                //            search.FilldgGrid();

                //        }
                //        else
                //        {
                //            MessageBox.Show("Baza danych przed importem musi być pusta");
                //        }

                //    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message);
                //}
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

        private void usuńToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SearchEngine search = new SearchEngine(txtSeek.Text, dgGrid);
            ClearDataBase(idDgGrid);
            search.FilldgGrid();
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

        #endregion

        #region CheckBox

        private void AllCheckboxElse(string nameInDataBase, CheckBox nameThis, string nameColumnDgGrid, Label labelVeg, Label main)
        {
            ClearSeek();

            fillGrid(nameInDataBase, nameThis, nameColumnDgGrid);
            UncheckCheckboxAfterPressingAnother(nameThis);
            FillDataGrdViewVegetarianRecipes(nameThis, nameColumnDgGrid);

            DisplayGreenVegetarianLabel(nameThis, labelVeg, main);

            DeleteDuplicate();
            unsubscribe = false;
            HideWyczyscSiatke();
            seekUnsubscribe = false;

            ChangeButtonFilterColor();
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
            RemoveVegetarianRecipesWithDataGridView("Vegetarian");

            DeleteDuplicate();
            HideLabelClearCheckBox();

            DisplayGreenVegetarianLabel(nameThis, nameVeg, main);
        }

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

        #endregion

        #region Filtrowanie

        List<CheckBox> checkBoxFilterName = new List<CheckBox>();
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
                Wypelnij();

            }
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

        #endregion

        #region DatagridView

        RecipesBase eksportId = new RecipesBase();
        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK();
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

        public void OneCliCK()
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

        public void OneCliCK(int id)
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

        #endregion DatagridView

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
                string sciezka = saveFileDialog1.FileName;
                FileStream fs = new FileStream(sciezka, System.IO.FileMode.Create, FileAccess.Write);

                foreach (var r in RecipesBase.getAll("RecipesBase"))
                {
                    ls.Add(new RecipesBase(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams));
                }
                xs.Serialize(fs, ls);
                fs.Close();

                MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            }

            /// <summary>
            /// ////////////////////
            /// </summary>
            /// 

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

        private void RemoveVegetarianRecipesWithDataGridView(string _propName)
        {
            for (int i = dgGrid.RowCount - 1; i >= 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells["Vegetarian"].Value) == 1 && dgGrid.Rows[i].Cells["Vegetarian"].ToString() == "Vegetarian")
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
            else { }
        }

        private void Wypelnij()
        {
            if (chcFish.Checked)
            {
                fillGrid("IdFishIngredients", chcFish, "Fishcheckbox");
                DisplayGreenVegetarianLabel(chcFish, lblFishVeg, lblFish);
            }
            if (chcPasta.Checked)
            {
                fillGrid("IdPastaIngredients", chcPasta, "Pastacheckbox");
                DisplayGreenVegetarianLabel(chcPasta, lblPastaVeg, lblPasta);
            }
            if (chcFruits.Checked)
            {
                fillGrid("IdFruitsIngredients", chcFruits, "Fruitscheckbox");
                DisplayGreenVegetarianLabel(chcFruits, lblFruitsVeg, lblFruits);
            }
            if (chcMuschrooms.Checked)
            {
                fillGrid("IdMuschroomsIngredients", chcMuschrooms, "Mushroomscheckbox");
                DisplayGreenVegetarianLabel(chcMuschrooms, lblMushroomVeg, lblMuschrooms);
            }
            if (chcBird.Checked)
            {
                fillGrid("IdBirdIngredients", chcBird, "Birdcheckbox");
                if (chcVegetarian.Checked)
                    DisplayGreenVegetarianLabel(chcBird, lblBirdVeg, lblBird);
            }
            if (chcMeat.Checked)
            {
                fillGrid("IdMeatIngredients", chcMeat, "Meatcheckbox");
                DisplayGreenVegetarianLabel(chcMeat, lblMeatVeg, lblMeat);
            }
            if (chcEggs.Checked)
            {
                fillGrid("IdEggsIngredients", chcEggs, "Eggscheckbox");
                DisplayGreenVegetarianLabel(chcEggs, lblEggsVeg, lblEggs);
            }

            if (chcSnack.Checked)
            {
                fillGrid("SnackMeal", chcSnack, "Snackcheckbox");
                DisplayGreenVegetarianLabel(chcSnack, lblSnackVeg, lblSnack);
            }
            if (chcDinner.Checked)
            {
                fillGrid("DinnerMeal", chcDinner, "Dinnercheckbox");
                DisplayGreenVegetarianLabel(chcDinner, lblDinnerVeg, lblDinner);
            }
            if (chcSoup.Checked)
            {
                fillGrid("SoupMeal", chcSoup, "Soupcheckbox");
                DisplayGreenVegetarianLabel(chcSoup, lblSoupVeg, lblSoup);
            }
            if (chcDessert.Checked)
            {
                fillGrid("DessertMeal", chcDessert, "Dessertcheckbox");
                DisplayGreenVegetarianLabel(chcDessert, lblDessertVeg, lblDessert);
            }
            if (chcDrink.Checked)
            {
                fillGrid("DrinkMeal", chcDrink, "Drinkscheckbox");
                DisplayGreenVegetarianLabel(chcDrink, lblDrinksVeg, lblDrinks);
            }
            if (chcPreserves.Checked)
            {
                fillGrid("PreservesMeal", chcPreserves, "Preservescheckbox");
                DisplayGreenVegetarianLabel(chcPreserves, lblPrevervesVeg, lblPreserves);
            }
            if (chcSalad.Checked)
            {
                fillGrid("SaladMeal", chcSalad, "Saladcheckbox");
                DisplayGreenVegetarianLabel(chcSalad, lblSaladVeg, lblSalad);
            }
            else if (unsubscribe == true)
            {
                SearchEngine searchEngine = new SearchEngine(txtSeek.Text, dgGrid);
                searchEngine.FilldgGrid();
                lblCleanVisibleFalse();
            }
            DeleteDuplicate();
        }

        private void UncheckCheckboxAfterPressingAnother(CheckBox main)
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

                DeleteRowsAfterUncheckCheckBox(chcVegetarian, "Vegetarian");

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
                    FillDataGrdViewVegetarianRecipes(chcFish, "Fishcheckbox");
                    DisplayGreenVegetarianLabel(chcFish, lblFishVeg, lblFish);
                }
                else if (chcPasta.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcPasta, "Pastacheckbox");
                    DisplayGreenVegetarianLabel(chcPasta, lblPastaVeg, lblPasta);
                }
                else if (chcFruits.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcFruits, "Fruitscheckbox");
                    DisplayGreenVegetarianLabel(chcFruits, lblFruitsVeg, lblFruits);
                }
                else if (chcMuschrooms.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcMuschrooms, "Mushroomscheckbox");
                    DisplayGreenVegetarianLabel(chcMuschrooms, lblMushroomVeg, lblMuschrooms);
                }
                else if (chcBird.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcBird, "Birdcheckbox");
                    DisplayGreenVegetarianLabel(chcBird, lblBirdVeg, lblBird);
                }
                else if (chcMeat.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcMeat, "Meatcheckbox");
                    DisplayGreenVegetarianLabel(chcMeat, lblMeatVeg, lblMeat);
                }
                else if (chcEggs.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcEggs, "Eggscheckbox");
                    DisplayGreenVegetarianLabel(chcEggs, lblEggsVeg, lblEggs);
                }
                else if (chcSnack.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcSnack, "Snackcheckbox");
                    DisplayGreenVegetarianLabel(chcSnack, lblSnackVeg, lblSnack);
                }
                else if (chcDinner.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcDinner, "Dinnercheckbox");
                    DisplayGreenVegetarianLabel(chcDinner, lblDinnerVeg, lblDinner);
                }
                else if (chcSoup.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcSoup, "Soupcheckbox");
                    DisplayGreenVegetarianLabel(chcSoup, lblSoupVeg, lblSoup);
                }
                else if (chcDessert.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcDessert, "Dessertcheckbox");
                    DisplayGreenVegetarianLabel(chcDessert, lblDessertVeg, lblDessert);
                }
                else if (chcDrink.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcDrink, "Drinkscheckbox");
                    DisplayGreenVegetarianLabel(chcDrink, lblDrinksVeg, lblDrinks);
                }
                else if (chcPreserves.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcPreserves, "Preservescheckbox");
                    DisplayGreenVegetarianLabel(chcPreserves, lblPrevervesVeg, lblPreserves);
                }
                else if (chcSalad.Checked)
                {
                    FillDataGrdViewVegetarianRecipes(chcSalad, "Saladcheckbox");
                    DisplayGreenVegetarianLabel(chcSalad, lblSaladVeg, lblSalad);
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

        #endregion Vegetarian

    }
}
