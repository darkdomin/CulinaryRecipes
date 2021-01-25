using CulinaryRecipes.Models;
using CulinaryRecipes.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using System.Xml.Serialization;


namespace CulinaryRecipes
{
    public partial class Form1 : Form
    {
        int idDgGrid;
        int numberOfPortionsForm1;
        string gramsColumnDgGridForm1;
        string ingredientColumnDgGridForm1;
        string descriptionForm1;
        string idRatingForm1;
        string amountsOfIngredientsForm1;
        bool isAvailable = NetworkInterface.GetIsNetworkAvailable();
        public int counter = 0;
        public int[] idMeal = new int[7];
        public int[] ingridients = new int[8];
        public bool seekUnsubscribe = false;
        public string emailIngredient;
        public string emailDescription;
        IEnumerable<RecipesBase>[] zap;

        public List<CheckBox> CheckBoxListFilter = new List<CheckBox>();
        public List<CheckBox> SavedCheckBox = new List<CheckBox>();
        RecipesBase getExportId = new RecipesBase();
        Serialization export;
        Deserialization import;

        SearchEngine searchEngine;

        string[] nameColumnsGroup =
        {
            "CategoryPreparationTime","CategoryDifficultLevel","CategoryRating","CategoryCuisines"
        };

        XmlSerializer xs;
        List<RecipesBase> ls;

        public Form1()
        {
            InitializeComponent();

            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            ls = new List<RecipesBase>();
            xs = new XmlSerializer(typeof(List<RecipesBase>));
        }

        bool DataBaseIsEmpty = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDataGridView();
            export = new Serialization(ls, xs);
            import = new Deserialization(ls, xs);
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";
            zap = new IEnumerable<RecipesBase>[panelFilterTime.Controls.Count];

            foreach (var r in DbFunc<RecipesBase>.GetAll())  
            {
                txtSeek.AutoCompleteCustomSource.Add(r.RecipesName);
            }

            if (DbFunc<RecipesBase>.GetCount() == 0)
            {
                DataBaseIsEmpty = true;
            }

            CheckConnection();
            Statistic();

            if (seekUnsubscribe == true)
            {
                searchEngine.Search(searchEngine.SearchName);
                seekUnsubscribe = true;
            }

            PrintSavedCheckBox(panelRighCentre);
            PrintSavedCheckBox(panelLeftCenter);
        }
        //Do poprawy - bo coś zwaliłem   //funkcja pamięciowa
        /// <summary>
        /// Unsubscribe checked and saved checkboxes
        /// </summary>
        /// <param name="set"></param>
        private void PrintSavedCheckBox(Control set)
        {
            foreach (var saved in SavedCheckBox)
            {
                foreach (Control elementCheck in set.Controls)
                {
                    if (elementCheck is CheckBox)
                    {
                        if (saved.Name.Equals(elementCheck.Name))
                        {
                            ((CheckBox)elementCheck).Checked = true;
                            break;
                        }
                    }
                }
            }
        }

        #region Buttons 

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
            {
                Application.Exit();
            }
        }


        private void btnNew_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            Function.NewFormTwo();
            // NewForm.addRecipe = true;
        }

        private void btnNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Function.NewFormTwo();
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
                    OpenForm.amountsOfIngredientsForm2 = Stamp.RemoveCharacters(amountsOfIngredientsForm1);
                }
                else
                {
                    OpenForm.amountsOfIngredientsForm2 = amountsOfIngredientsForm1;
                }

                if (amountsOfIngredientsForm1 != null)
                {
                    OpenForm.gramsForm2 = Stamp.RemoveCharacters(gramsColumnDgGridForm1);
                }
                else
                {
                    OpenForm.gramsForm2 = gramsColumnDgGridForm1;
                }

                if (ingredientColumnDgGridForm1 != null)
                {
                    OpenForm.ingredientForm2 = Stamp.RemoveCharacters(ingredientColumnDgGridForm1);
                }
                else
                {
                    OpenForm.ingredientForm2 = ingredientColumnDgGridForm1;
                }

                if (txtShortDescription.Text != null)
                {
                    OpenForm.ShortDescriptionForm2 = Stamp.RemoveCharacters(txtShortDescription.Text);
                }
                else
                {
                    OpenForm.ShortDescriptionForm2 = txtShortDescription.Text;
                }

                if (descriptionForm1 != null)
                {
                    OpenForm.instructionForm2 = Stamp.RemoveCharacters(descriptionForm1);
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
                    OpenForm.checkBoxDish[i] = idMeal[i];
                }
                for (int i = 0; i < ingridients.Length; i++)
                {
                    OpenForm.checkBoxIngredients[i] = ingridients[i];
                }

                OpenForm.counterForm2 = counter;
                OpenForm.seekUnsubscribeForm2 = seekUnsubscribe;

                OpenForm.SavedCheckBoxForm2 = SavedCheckBox;

                this.Hide();
                OpenForm.ShowDialog();
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenClick();
        }

        private void btnOpen_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                OpenClick();
            }
        }

        bool firstIsChecked = false;

        /// <summary>
        ///  Marks the first row and transfers the data from the DatagridView to thumbnails
        /// </summary>
        public bool SelectFirstRow()
        {
            if (dgGrid.Rows.Count > 0)
            {
                dgGrid.CurrentCell = dgGrid.Rows[0].Cells[1];
                dgGrid.DefaultCellStyle.SelectionBackColor = Color.SlateGray;
                return true;
            }

            return false;
        }

        private void btnSeek_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && string.IsNullOrEmpty(txtSeek.Text))
            {
                searchEngine.Search(searchEngine.SearchName);
            }
        }

        private void lblNameSeek_Click(object sender, EventArgs e)
        {
            searchEngine.SearchName = 1;
            rememberSearchNameIfNewObject = false;
            if (txtSeek.Text != string.Empty)
            {
                searchEngine.CompletedgGrid(txtSeek.Text);
                searchEngine.Search(searchEngine.SearchName);
            }
            txtSeek.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            LabelBold(lblNameSeek);
            LabelSlim(lblAmountsSeek);
            txtSeek.Text = string.Empty;
            if (dgGrid.Rows.Count > 0)
            {
                DeleteDuplicate();
            }
        }
        bool rememberSearchNameIfNewObject = false;
        private void lblAmountsSeek_Click(object sender, EventArgs e)
        {
            searchEngine.SearchName = 2;
            rememberSearchNameIfNewObject = true;
            if (txtSeek.Text != string.Empty)
            {
                searchEngine.CompletedgGrid(txtSeek.Text);
                searchEngine.Search(searchEngine.SearchName);
            }
            txtSeek.AutoCompleteMode = AutoCompleteMode.None;

            LabelBold(lblAmountsSeek);
            LabelSlim(lblNameSeek);
            txtSeek.Text = string.Empty;
        }

        private void lblCleanDgGrid_Click(object sender, EventArgs e)
        {
            ClearDgGrid();
            ClearThumbnails();
            txtSeek.Text = string.Empty;
        }

        private void lblClearCheckBox_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (dgGrid.Rows.Count > 0)
                {
                    dgGrid.Rows.Clear();
                }
                int count = searchEngine.CheckBoxList.Count;

                for (int i = count - 1; i >= 0; i--)
                {
                    if (searchEngine.CheckBoxList[i].Checked)
                    {
                        searchEngine.CheckBoxList[i].Checked = false;
                    }
                }

                chcVegetarian.Checked = false;

                HideLabelAboveDatagridView(panelMiddle, lblClearCheckBox);

                CMSdelete.Visible = false;
                CMSexportOne.Visible = false;
                CMSSend.Visible = false;

                txtSeek.Text = string.Empty;
                ClearThumbnails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion Buttony 

        #region Menu

        private void eksportujPojedynczyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSingleFile();
        }

        private void importujPojedynczyPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                import.ImportSingleFIleFromXML(openFileDialog1.FileName);

                searchEngine.CompletedgGrid();
                Statistic();
            }
        }

        private void usuńBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Czy na pewno usunąć Bazę danych? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                DbFunc<RecipesBase>.ClearDb("RecipesBase");

                MessageBox.Show("Dokument został usunięty");
                searchEngine.CompletedgGrid();
            }

            lblStatistic.Text = 0.ToString();
        }

        private void nowyPrzepisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Function.NewFormTwo();
        }

        private void eksportujCalaBazeDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Pliki tekstowe (*.xml)|*.xml";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = saveFileDialog1.FileName;
                FileStream fs = new FileStream(path, System.IO.FileMode.Create, FileAccess.Write);

                foreach (var r in DbFunc<RecipesBase>.GetAll()) 
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
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                import.ImportFileFromXML(openFileDialog1.FileName);
                searchEngine.Search(searchEngine.SearchName);
            }
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
            RemoveSingeFileFromDataBase(idDgGrid);
            ClearDgGrid();
            ClearThumbnails();
            searchEngine.CompletedgGrid();
            Statistic();
        }

        private void CMSSend_Click(object sender, EventArgs e)
        {
            Logo show = new Logo();

            show.titleLogo = txtLittleName.Text;
            show.ingredientLogo = emailIngredient;
            show.amountsLogo = amountsOfIngredientsForm1;
            show.gramsLogo = gramsColumnDgGridForm1;
            show.descriptionLogo = emailDescription;
            this.Hide();
            show.ShowDialog();
        }

        #endregion Menu

        #region CheckBox

        private void AllCheckboxElse(CheckBox nameThis, string nameColumnDgGridOrDB, Label labelVeg, Label main)
        {
            AddCheckBoxCheckedToList(nameThis);
            AddColumnToList(nameColumnDgGridOrDB);

            if (!vege)
            {
                if (!string.IsNullOrWhiteSpace(txtSeek.Text))
                {
                    dgGrid.Rows.Clear();
                    BezNazwyNaRazie();
                }
                else
                {
                    if (searchEngine.CheckBoxList.Count < 1)
                    {
                        dgGrid.Rows.Clear();
                        searchEngine.CompleteGrid(nameColumnDgGridOrDB, nameThis);
                    }
                    else
                    {
                        if (searchEngine.CheckBoxList.Count == 1)
                        {
                            if (dgGrid.Rows.Count > 0)
                            {
                                dgGrid.Rows.Clear();
                            }
                        }
                        searchEngine.CompleteGrid(nameColumnDgGridOrDB, nameThis);
                    }
                }
            }
            else
            {
                DisplayGreenVegetarianLabel(nameThis, labelVeg, main);
                dgGrid.Rows.Clear();
                BezNazwyVegetarian();
            }

            DeleteDuplicate();

            seekUnsubscribe = false;

            ChangeButtonFilterColor();

            if (dgGrid.RowCount > 0)
            {
                dgGrid.Focus();
                if (SelectFirstRow())
                {
                    OneCliCK();
                }
            }
        }

        private void AllCheckboxAfterElse()
        {
            ClearThumbnails();
            CheckCheckBoxAndShowRemovalLabel();
        }

        private void AllCheckboxAfterIf(CheckBox nameThis, string nameColumnDgGridOrDB, Label nameVeg, Label main)
        {
            searchEngine.RemoveRowsAfterUncheckCheckBox(nameThis, nameColumnDgGridOrDB);

            HideLabelAboveDatagridView(panelMiddle, lblClearCheckBox);

            RemoveCheckBoxCheckedFromList(nameThis);
            RemoveColumnFromList(nameColumnDgGridOrDB);

            if (searchEngine.CheckBoxList.Count == 0)
            {
                if (!vege)
                {
                    if (!string.IsNullOrWhiteSpace(txtSeek.Text))
                    {
                        searchEngine.CompletedgGrid(txtSeek.Text);
                    }
                }
                else
                {
                    DisplayGreenVegetarianLabel(nameThis, nameVeg, main);
                    searchEngine.CompleteGrid(RecipesData.Vegetarian.ToString(), chcVegetarian);
                }
            }
            else
            {
                dgGrid.Rows.Clear();

                if (vege)
                {
                    DisplayGreenVegetarianLabel(nameThis, nameVeg, main);
                    BezNazwyVegetarian();
                    if (searchEngine.CheckBoxList.Count > 0)
                    {
                        DeleteDuplicate();
                    }
                }
                else
                {
                    BezNazwyNaRazie();
                }
            }

            DeleteDuplicate();

            if (searchEngine.CheckBoxList.Count > 0)
            {
                if (SelectFirstRow())
                {
                    OneCliCK();
                }
            }
        }

        List<string> numColumn = new List<string>();

        public List<CheckBox> AddCheckBoxCheckedToList(CheckBox name)
        {
            searchEngine.CheckBoxList.Add(name);
            return searchEngine.CheckBoxList;
        }

        public List<CheckBox> RemoveCheckBoxCheckedFromList(CheckBox name)
        {
            searchEngine.CheckBoxList.Remove(name);
            return searchEngine.CheckBoxList;
        }

        public List<string> AddColumnToList(string name)
        {
            numColumn.Add(name);
            return numColumn;
        }

        public List<string> RemoveColumnFromList(string name)
        {
            numColumn.Remove(name);
            return numColumn;
        }
        private void chcSnack_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcSnack, lblSnackVeg, lblSnack, RecipesData.SnackMeal.ToString());
        }

        private void chcDinner_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcDinner, lblDinnerVeg, lblDinner, RecipesData.DinnerMeal.ToString());
        }

        private void CompleteDataGridRow(RecipesBase r)
        {
            dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
        }

        private void chcSoup_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcSoup, lblSoupVeg, lblSoup, RecipesData.SoupMeal.ToString());
        }

        private void chcDessert_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcDessert, lblDessertVeg, lblDessert, RecipesData.DessertMeal.ToString());
        }

        private void chcDrink_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcDrink, lblDrinkVeg, lblDrink, RecipesData.DrinkMeal.ToString());
        }

        private void chcPreserves_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcPreserves, lblPrevervesVeg, lblPreserves, RecipesData.PreservesMeal.ToString());
        }

        private void chcSalad_CheckedChanged(object sender, EventArgs e)
        {
            GetAndHideRecipes(chcSalad, lblSaladVeg, lblSalad, RecipesData.SaladMeal.ToString());
        }

        private void chcFish_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcPasta_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFruits_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcMuschrooms_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcBird_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcMeat_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcEggs_CheckedChanged(object sender, EventArgs e)
        {
        }

        #endregion Checkbox

        //całe do przerobienia
        #region Filtrowanie

        private void fillGridFilterMeal(string columnBase, string _propName, CheckBox _name, string columnName, List<string> panelColumn)
        {
            if (_name.Checked)
            {
                foreach (var r in DbFunc<RecipesBase>.GetAll()) 
                {
                    for (int i = 0; i < panelColumn.Count; i++)
                    {
                        if ((int)SearchEngine.GetPropValue(r, _propName) == 1
                         && SearchEngine.GetPropValue(r, columnBase) == panelColumn[i])
                        {
                            if (panelColumn[i] == SearchEngine.GetPropValue(r, columnBase))
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

        private void FilterInPanel(CheckBox filterName, Panel panelName, string textInColumn)
        {

        }

        private void chcFilterCuisineAmerican_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterCuisinePolish_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterCuisineHungarian_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterCuisinePortuguese_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterCuisineFrench_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterCuisineAsian_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterCuisineItalian_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterCuisineGreek_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterCuisineSpanish_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterCuisineCzech_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFiltrLevelEasy_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterLevelHard_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void chcFilterLevelVeryHard_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterRatingOne_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterRatingTwo_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void chcFilterRatingThree_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void FilterTime(int numberInTheTable, string minutes)
        {
            zap[numberInTheTable] = from recipe in DbFunc<RecipesBase>.GetAll()
                                    where recipe.CategoryPreparationTime == minutes
                                    select recipe;
        }


        private void chcFilterTime30_CheckedChanged(object sender, EventArgs e)
        {
            FilterTime(0, "30 min");
        }


        private void chcFiltrTime60_CheckedChanged(object sender, EventArgs e)
        {
            FilterTime(1, "60 min");
        }

        private void chcFilterTime90_CheckedChanged(object sender, EventArgs e)
        {
            FilterTime(2, "90 min");
        }

        private void chcFilterTime900_CheckedChanged(object sender, EventArgs e)
        {
            FilterTime(3, "pow 90");
        }

        private void btnFilterOpen_Click(object sender, EventArgs e)
        {
            if (panelFilterLevel.Visible == false)
            {
                panelFiltrCuisine.Visible = true;
                panelFilterLevel.Visible = true;
                panelFilterRating.Visible = true;
                panelFilterTime.Visible = true;
                panelFiltrComponents.Visible = true;
                btnFilterOpen.Visible = false;
                btnFilterClose.Visible = true;
                btnFilter.Visible = true;
            }
        }

        private void BtnFilter_Click(object sender, EventArgs e)
        {
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
            panelFiltrComponents.Visible = false;
            btnFilter.Visible = false;
            btnFilterClose.Visible = false;
            btnFilterOpen.Visible = true;
        }

        #endregion Filtrowanie

        #region DatagridView


        private void dgGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            OneCliCK();
        }

        public void OneCliCK()
        {
            int row = dgGrid.CurrentCell.RowIndex;

            if (row >= 0)
            {
                CMSdelete.Visible = true;
                CMSexportOne.Visible = true;
                CMSSend.Visible = true;

                if (firstIsChecked)
                {
                    firstIsChecked = false;
                }
                else
                {
                    idDgGrid = Convert.ToInt32(dgGrid.Rows[row].Cells[0].Value);
                }

                Przypisanie(row, idDgGrid);
            }
        }

        private void Przypisanie(int row, int idGrid)
        {
            getExportId = DbFunc<RecipesBase>.GetById(idGrid);
            txtLittleName.Text = dgGrid.Rows[row].Cells[1].Value.ToString();
            ingredientColumnDgGridForm1 = dgGrid.Rows[row].Cells[2].Value.ToString();
            amountsOfIngredientsForm1 = dgGrid.Rows[row].Cells[3].Value.ToString();
            //W celu usunięcia RTF
            txtShortDescription.Rtf = dgGrid.Rows[row].Cells[2].Value.ToString();
            emailIngredient = txtShortDescription.Text;

            txtShortDescription.Rtf = dgGrid.Rows[row].Cells[5].Value.ToString();
            emailDescription = txtShortDescription.Text;

            if (dgGrid.Rows[row].Cells[4].Value.ToString().Contains(Stamp.StampsCharacters()))
            {
                txtShortDescription.Rtf = Stamp.RemoveCharacters(dgGrid.Rows[row].Cells[4].Value.ToString());
            }
            else
            {
                txtShortDescription.Rtf = dgGrid.Rows[row].Cells[4].Value.ToString();
            }

            descriptionForm1 = dgGrid.Rows[row].Cells[5].Value.ToString();
            numberOfPortionsForm1 = Convert.ToInt32(dgGrid.Rows[row].Cells[6].Value);

            if (dgGrid.Rows[row].Cells[7].Value.ToString() != Stamp.StampsCharacters())
            {
                lblCuisine.Text = dgGrid.Rows[row].Cells[7].Value.ToString();
            }

            if (dgGrid.Rows[row].Cells[8].Value.ToString() != Stamp.StampsCharacters())
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

            if (dgGrid.Rows[row].Cells[25].Value.ToString() == Stamp.StampsCharacters())
            {
                pbLittlePhoto.Image = Resources.przepisy;
            }
            else
            {
                pbLittlePhoto.ImageLocation = dgGrid.Rows[row].Cells[25].Value.ToString();
            }

            ingridients[7] = Convert.ToInt32(dgGrid.Rows[row].Cells[26].Value);

            if (dgGrid.Rows[row].Cells[27].Value.ToString() != Stamp.StampsCharacters())
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

        public void OneCliCK(int id)
        {
            try
            {
                CMSdelete.Visible = true;
                int row = dgGrid.CurrentCell.RowIndex;

                if (row >= 0)
                {
                    idDgGrid = Convert.ToInt32(dgGrid.Rows[id].Cells[0].Value);
                    Przypisanie(row, idDgGrid);

                    //getExportId = RecipesBase.GetById(idDgGrid);

                    //txtLittleName.Text = dgGrid.Rows[row].Cells[1].Value.ToString();
                    //ingredientColumnDgGridForm1 = dgGrid.Rows[row].Cells[2].Value.ToString();
                    //amountsOfIngredientsForm1 = dgGrid.Rows[row].Cells[3].Value.ToString();

                    //if (dgGrid.Rows[row].Cells[4].Value.ToString() == Stamp.StampsCharacters())
                    //{
                    //    txtShortDescription.Rtf = Stamp.RemoveCharacters(txtShortDescription.Text);
                    //}
                    //else
                    //{
                    //    txtShortDescription.Rtf = dgGrid.Rows[row].Cells[4].Value.ToString();
                    //}

                    //descriptionForm1 = dgGrid.Rows[row].Cells[5].Value.ToString();
                    //numberOfPortionsForm1 = Convert.ToInt32(dgGrid.Rows[row].Cells[6].Value);

                    //if (dgGrid.Rows[row].Cells[7].Value.ToString() != Stamp.StampsCharacters())
                    //{
                    //    lblCuisine.Text = dgGrid.Rows[row].Cells[7].Value.ToString();
                    //}

                    //if (dgGrid.Rows[row].Cells[8].Value.ToString() != Stamp.StampsCharacters())
                    //{
                    //    idRatingForm1 = dgGrid.Rows[row].Cells[8].Value.ToString();
                    //}

                    //if (idRatingForm1 == "1") pbStar1.Visible = true;
                    //else if (idRatingForm1 == "2")
                    //{
                    //    pbStar1.Visible = true;
                    //    pbStar2.Visible = true;
                    //}
                    //else if (idRatingForm1 == "3")
                    //{
                    //    pbStar1.Visible = true;
                    //    pbStar2.Visible = true;
                    //    pbStar3.Visible = true;
                    //}

                    //lblShortLevel.Text = dgGrid.Rows[row].Cells[9].Value.ToString();
                    //lblShortTime.Text = dgGrid.Rows[row].Cells[10].Value.ToString();

                    //MealAndIngredients();

                    //if (dgGrid.Rows[row].Cells[25].Value.ToString() == Stamp.StampsCharacters())
                    //{
                    //    pbLittlePhoto.Image = Resources.przepisy;
                    //}
                    //else
                    //{
                    //    pbLittlePhoto.ImageLocation = dgGrid.Rows[row].Cells[25].Value.ToString();
                    //}

                    //ingridients[7] = Convert.ToInt32(dgGrid.Rows[row].Cells[26].Value);

                    //if (dgGrid.Rows[row].Cells[27].Value.ToString() != Stamp.StampsCharacters())
                    //{
                    //    gramsColumnDgGridForm1 = dgGrid.Rows[row].Cells[27].Value.ToString();
                    //}

                    //pbLittlePhoto.Visible = true;
                    //txtLittleName.Visible = true;
                    //txtShortDescription.Visible = true;
                    //lblShortTime.Visible = true;
                    //lblShortLevel.Visible = true;
                    //lblCuisine.Visible = true;


                    //dgGrid.DefaultCellStyle.SelectionBackColor = Color.SlateGray;

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

        private void SearchGeneral(KeyEventArgs e)
        {
            searchEngine = new SearchEngine(txtSeek.Text, dgGrid);

            if (rememberSearchNameIfNewObject)
            {
                searchEngine.SearchName = 2;
            }

            if (txtSeek.Text.Length == 1 || e.KeyCode == Keys.Back)
            {
                dgGrid.Rows.Clear();
                searchEngine.CompletedgGrid();
            }

            searchEngine.Search(searchEngine.SearchName);
            seekUnsubscribe = true;
        }

        /// <summary>
        ///  Displays a label to remove checkboxes
        /// </summary>
        private void CheckCheckBoxAndShowRemovalLabel()
        {
            for (int i = 0; i < searchEngine.CheckBoxList.Count; i++)
            {
                if (searchEngine.CheckBoxList[i].Checked)
                {
                    HideLabelAboveDatagridView(panelMiddle, lblClearCheckBox);
                    break;
                }
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

        private void GetAndHideRecipes(CheckBox checkboxName, Label labelCheckboxNameVeg, Label labelCheckboxName, string elementInDataBase)
        {
            if (!DataBaseIsEmpty)
            {
                if (!checkboxName.Checked)
                {
                    AllCheckboxAfterIf(checkboxName, elementInDataBase, labelCheckboxNameVeg, labelCheckboxName);
                }
                else
                {
                    AllCheckboxElse(checkboxName, elementInDataBase, labelCheckboxNameVeg, labelCheckboxName);
                }

                AllCheckboxAfterElse();
            }
        }

        /// <summary>
        ///  Hide one of the two labels above the datagrid
        /// </summary>
        private void HideLabelAboveDatagridView(Control set, Label name)
        {
            foreach (Control element in set.Controls)
            {
                if (element is Label)
                {
                    HideOneLabelAboveDatagridViewInternalMethod(element);

                    if (element.Location.X + 30 >= name.Location.X || element.Location.X - 30 >= name.Location.X)
                    {
                        HideOneLabelAboveDatagridViewInternalMethod(element);
                    }
                }
            }
        }

        /// <summary>
        ///  Hide one of the two labels above the datagrid internal method
        /// </summary>
        private void HideOneLabelAboveDatagridViewInternalMethod(Control element)
        {
            if (dgGrid.Rows.Count > 0)
            {
                ((Label)element).Visible = true;
            }
            else
            {
                ((Label)element).Visible = false;
            }
        }

        /// <summary>
        /// Clear all visible records 
        /// </summary>
        public void ClearDgGrid()
        {
            if (dgGrid.RowCount > 0)
            {
                dgGrid.Rows.Clear();
            }

            CMSdelete.Visible = false;
            CMSexportOne.Visible = false;
        }

        /// <summary>
        /// Clear pictures and labels ( thumbnails window center )
        /// </summary>
        private void ClearThumbnails()
        {
            if (dgGrid.Rows.Count <= 0)
            {
                foreach (Control item in panelScreen.Controls)
                {
                    item.Visible = false;
                }

                idDgGrid = 0;
                pbFrame.Visible = true;
            }
        }

        /// <summary>
        /// Label Font Bold
        /// </summary>
        private void LabelBold(Label name)
        {
            name.Font = new Font("Verdana", 8F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(238)));
        }

        /// <summary>
        /// Label Font Slim
        /// </summary>
        /// <param name="name"></param>
        private void LabelSlim(Label name)
        {
            name.Font = new Font("Verdana", 8F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(238)));
        }

        /// <summary>
        /// Returns the number of records and sets the correct Polish name
        /// </summary>
        public void Statistic()
        {
            if (chcStstistic.Checked)
            {
                int count = DbFunc<RecipesBase>.GetCount();

                for (int i = 0; i <= count; i += 10)
                {
                    if (count - 1 == 0 || count - 1 >= 5 + i) lblCulinary.Text = "Przepisów";
                    else if (count - 1 == 1 + i) lblCulinary.Text = "Przepis";
                    else if (count - 1 > 1 + i && count - 1 < 5 + i && i > 20) lblCulinary.Text = "Przepisy";
                    else lblCulinary.Text = "Przepisów";
                }

                lblStatistic.Text = count.ToString();
                lblStatistic.Visible = true;
                lblCulinary.Visible = true;
            }
            else
            {
                lblStatistic.Visible = false;
                lblCulinary.Visible = false;
            }
        }

        /// <summary>
        /// Export of a single file to xml
        /// </summary>
        public void ExportSingleFile()
        {
            if (idDgGrid > 0)
            {
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    export.ExportDatabase(saveFileDialog1.FileName, getExportId);
                }
            }
            else
            {
                MessageBox.Show("Wybierz przepis do eksportu");
            }
        }

        /// <summary>
        /// Delete single data from the database
        /// </summary>
        /// <param name="numberId"></param>
        public void RemoveSingeFileFromDataBase(int numberId)
        {
            try
            {
                if (MessageBox.Show("Czy na pewno usunąć Plik? \nOperacja nie do odwrócenia", "Uwaga!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    var s = DbFunc<RecipesBase>.GetById(numberId);
                    DbFunc<RecipesBase>.DeleteSingleFile(s.Id);

                    MessageBox.Show("Dokument został usunięty");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Checks internet connection
        /// </summary>
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

        /// <summary>
        /// Create DataGridView and hides unnecessary columns
        /// </summary>
        public void CreateDataGridView()
        {
            dgGrid.Rows.ToString().ToUpper();

            dgGrid.Columns.Add("Id", "Id");
            dgGrid.Columns.Add("RecipesName", "RecipesName");
            dgGrid.Columns.Add("Ingredients", "Ingredients");
            dgGrid.Columns.Add("AmountsMeal", "AmountsMeal");
            dgGrid.Columns.Add("ShortDescription", "ShortDescription");
            dgGrid.Columns.Add("LongDescription", "LongDescription");
            dgGrid.Columns.Add("NumberPortions", "NumberPortions");

            dgGrid.Columns.Add("CategoryCuisines", "CategoryCuisines");
            dgGrid.Columns.Add("CategoryRating", "CategoryRating");
            dgGrid.Columns.Add("CategoryDifficultLevel", "CategoryDifficultLevel");
            dgGrid.Columns.Add("CategoryPreparationTime", "CategoryPreparationTime");

            dgGrid.Columns.Add("SnackMeal", "SnackMeal");
            dgGrid.Columns.Add("DinnerMeal", "DinnerMeal");
            dgGrid.Columns.Add("SoupMeal", "SoupMeal");
            dgGrid.Columns.Add("DessertMeal", "DessertMeal");
            dgGrid.Columns.Add("DrinkMeal", "DrinkMeal");
            dgGrid.Columns.Add("PreservesMeal", "PreservesMeal");
            dgGrid.Columns.Add("SaladMeal", "SaladMeal");

            dgGrid.Columns.Add("IdFishIngredients", "IdFishIngredients");
            dgGrid.Columns.Add("IdPastaIngredients", "IdPastaIngredients");
            dgGrid.Columns.Add("IdFruitsIngredients", "IdFruitsIngredients");
            dgGrid.Columns.Add("IdMuschroomsIngredients", "IdMuschroomsIngredients");
            dgGrid.Columns.Add("IdBirdIngredients", "IdBirdIngredients");
            dgGrid.Columns.Add("IdMeatIngredients", "IdMeatIngredients");
            dgGrid.Columns.Add("IdEggsIngredients", "IdEggsIngredients");
            dgGrid.Columns.Add("PhotoLinkLocation", "PhotoLinkLocation");
            dgGrid.Columns.Add("Vegetarian", "Vegetarian");
            dgGrid.Columns.Add("Grams", "Grams");

            for (int i = 0; i < dgGrid.ColumnCount; i++)
            {
                if (i == 1)
                {
                    continue;
                }
                else
                {
                    dgGrid.Columns[i].Visible = false;
                }
            }
        }

        /// <summary>
        /// Removes duplicate rows in DataGrigView
        /// </summary>
        private void DeleteDuplicate()
        {
            dgGrid.Sort(new RowComparer(SortOrder.Ascending));

            for (int i = dgGrid.RowCount - 1; i > 0; i--)
            {
                if (Convert.ToInt32(dgGrid.Rows[i].Cells[0].Value) == Convert.ToInt32(dgGrid.Rows[i - 1].Cells[0].Value))
                {
                    dgGrid.Rows.Remove(dgGrid.Rows[i]);
                }
                else
                {
                    continue;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void eksportujPlikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportSingleFile();
        }

        private void exportujBazęDanychToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                export.ExportDatabase(saveFileDialog1.FileName);
            }
        }

        #endregion Function

        #region vegetarian

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
            if (!DataBaseIsEmpty)
            {
                if (rememberSearchNameIfNewObject)
                {
                    searchEngine.SearchName = 2;
                }
                if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && txtSeek.Text != string.Empty)
                {
                    if (searchEngine.CheckBoxList.Count > 0)
                    {
                        dgGrid.Rows.Clear();

                        if (chcVegetarian.Checked)
                        {
                            BezNazwyVegetarian();
                        }
                        else
                        {
                            BezNazwyNaRazie();
                        }

                        if (dgGrid.Rows.Count > 0)
                        {
                            SelectFirstRow();
                        }
                        else
                        {
                            ClearThumbnails();
                        }
                    }
                    else
                    {
                        dgGrid.Rows.Clear();
                        if (chcVegetarian.Checked)
                        {
                            BezNazwyVegetarian();
                            if (searchEngine.CheckBoxList.Count > 0)
                            {
                                DeleteDuplicate();
                            }

                            if (searchEngine.CompleteGridVegetarian(RecipesData.Vegetarian.ToString(), chcVegetarian, txtSeek.Text))
                            {
                                if (SelectFirstRow())
                                {
                                    OneCliCK();
                                }
                            }
                            else
                            {
                                ClearThumbnails();
                            }
                        }
                        else
                        {
                            bool listHasBeenPrinted = false;

                            listHasBeenPrinted = searchEngine.CompletedgGrid(txtSeek.Text);
                            DeleteDuplicate();

                            if (listHasBeenPrinted)
                            {
                                if (SelectFirstRow())
                                {
                                    OneCliCK();
                                }
                            }
                            else
                            {
                                ClearThumbnails();
                            }
                        }
                    }
                }
                else if (txtSeek.Text == "" && e.KeyCode != Keys.Down && e.KeyCode != Keys.Up)
                {
                    if (searchEngine.CheckBoxList.Count > 0)
                    {
                        BezNazwyNaRazie();

                        if (dgGrid.Rows.Count > 0)
                        {
                            DeleteDuplicate();
                            SelectFirstRow();
                        }
                    }
                    else
                    {
                        searchEngine.CompletedgGrid();

                        if (dgGrid.Rows.Count > 0)
                        {
                            DeleteDuplicate();
                            SelectFirstRow();
                        }
                    }
                }
            }
        }

        private void BezNazwyNaRazie()
        {
            int i = 0;
            foreach (var checkboxName in searchEngine.CheckBoxList)
            {
                searchEngine.CompleteGrid(numColumn[i], txtSeek.Text);
                i++;
            }
            if (searchEngine.CheckBoxList.Count > 0)
            {
                DeleteDuplicate();
            }
        }

        public void DisplayGreenVegetarianLabel(CheckBox checkboxName, Label veg, Label main)
        {
            if (chcVegetarian.Checked && checkboxName.Checked)
            {
                veg.ForeColor = System.Drawing.Color.White;
                veg.Visible = true;
                main.GreenLabel(checkboxName.Name);
            }
            else if (chcVegetarian.Checked == false || checkboxName.Checked == false)
            {
                veg.Visible = false;
                main.WhiteLabel(checkboxName.Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void BezNazwyVegetarian()
        {
            int i = 0;
            foreach (var checkboxName in searchEngine.CheckBoxList)
            {
                searchEngine.CompleteGridVegetarian(numColumn[i], chcVegetarian, txtSeek.Text);

                i++;
            }
        }

        bool vege = false;
        private void chcVegetarian_CheckedChanged(object sender, EventArgs e)
        {
            if (!DataBaseIsEmpty)
            {
                if (chcVegetarian.Checked)
                {
                    vege = true;

                    if (searchEngine.CheckBoxList.Count > 0)
                    {
                        dgGrid.Rows.Clear();
                        BezNazwyVegetarian();

                        if (searchEngine.CheckBoxList.Count > 0)
                        {
                            DeleteDuplicate();
                        }

                        foreach (var item in searchEngine.CheckBoxList)
                        {
                            Vegetarian.ChangeLabelColorForVeg(panelRighCentre, item.Name);
                            Vegetarian.ChangeLabelColorForVeg(panelLeftCenter, item.Name);
                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtSeek.Text))
                        {
                            searchEngine.CompleteGrid(RecipesData.Vegetarian.ToString(), chcVegetarian);
                        }
                        else
                        {
                            dgGrid.Rows.Clear();
                            searchEngine.CompleteGrid(RecipesData.Vegetarian.ToString(), txtSeek.Text);
                        }
                    }

                    lblVegetarian.GreenLabel(chcVegetarian.Name);
                }
                else
                {
                    vege = false;

                    if (searchEngine.CheckBoxList.Count > 0)
                    {
                        BezNazwyNaRazie();
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(txtSeek.Text))
                        {
                            dgGrid.Rows.Clear();
                            lblVegetarian.WhiteLabel();
                        }
                        else
                        {
                            searchEngine.CompletedgGrid(txtSeek.Text);
                            DeleteDuplicate();
                            SelectFirstRow();
                        }
                    }

                    foreach (var item in searchEngine.CheckBoxList)
                    {
                        Vegetarian.ChangeLabelColorForVeg(panelRighCentre, item.Name);
                        Vegetarian.ChangeLabelColorForVeg(panelLeftCenter, item.Name);
                    }

                    lblVegetarian.WhiteLabel();
                }
            }
        }

        #endregion Vegetarian

        int newSelectionStart = 0;
        private void txtSeek_TextChanged(object sender, EventArgs e)
        {
            txtSeek.Text = txtSeek.Text.ToUpper();
            txtSeek.SelectionStart = newSelectionStart + 1;

        }

        private void txtSeek_KeyPress(object sender, KeyPressEventArgs e)
        {
            newSelectionStart = txtSeek.SelectionStart;
        }
    }
}
