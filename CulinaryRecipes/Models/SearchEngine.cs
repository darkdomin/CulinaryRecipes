using CulinaryRecipes.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    class SearchEngine
    {
        string _txtSeek;
        DataGridView _dgGrid;
        string[] _sortedGridTab = new string[DbFunc<RecipesBase>.GetCount()];
        /// <summary>
        /// Name in Data Base
        /// </summary>
        private static string[] _nameColumnInDataGrid =
        {
            "SnackMeal",  "DinnerMeal", "SoupMeal",  "DessertMeal", "DrinkMeal", "PreservesMeal", "SaladMeal",
            "IdFishIngredients", "IdPastaIngredients", "IdFruitsIngredients", "IdMuschroomsIngredients", "IdBirdIngredients",
            "IdMeatIngredients", "IdEggsIngredients"//, "Vegetarian"
        };

        public SearchEngine(string txtSeek, DataGridView dgGrid)
        {
            this._txtSeek = txtSeek;
            this._dgGrid = dgGrid;
            CheckBoxList = new List<CheckBox>();
            SearchName = 1;
        }

        public SearchEngine(DataGridView dgGrid)
        {
            this._dgGrid = dgGrid;
            CheckBoxList = new List<CheckBox>();
            SearchName = 1;
        }

        #region Properties
        /// <summary>
        /// Gets or set the read and write checkboxes
        /// </summary>
        public List<CheckBox> CheckBoxList { get; set; }

        /// <summary>
        /// Gets or Set the search number. Search by name - 1, by ingredients 2.
        /// </summary>
        public int SearchName { get; set; }

        #endregion Properties

        #region Methods
        public void CompleteDataGridRow(RecipesBase r)
        {
            _dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
        }

        /// <summary>
        /// Complete the DataGridView with data from dataBase
        /// </summary>
        public void CompletedgGrid()
        {
            try
            {
                foreach (var r in DbFunc<RecipesBase>.GetAll())
                {
                    CompleteDataGridRow(r);
                }
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Complete DataGridView with data from dataBase
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CompletedgGrid(string text)
        {
            bool found = false;
            try
            {
                foreach (var r in DbFunc<RecipesBase>.GetAll())
                {
                    string[] tab = r.RecipesName.Split(' ');

                    if (SearchName == 1)
                    {
                        if (r.RecipesName.Contains(text))
                        {
                            foreach (var item in tab)
                            {
                                if (item.StartsWith(text) || r.RecipesName.StartsWith(text))
                                {
                                    CompleteDataGridRow(r);
                                    found = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (r.Ingredients.ToUpper().Contains(text))
                        {
                            CompleteDataGridRow(r);
                            found = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return found;
        }

        /// <summary>
        /// Complete the DatagridView data from the selected checkbox
        /// </summary>
        /// <param name="_propName"></param>
        /// <param name="_checkBoxName"></param>
        /// <param name="nazwa"></param>
        public void CompleteGrid(string propName, CheckBox _checkBoxName)
        {
            if (_checkBoxName.Checked)
            {
                foreach (var r in DbFunc<RecipesBase>.GetAll())
                {
                    if ((int)GetPropValue(r, propName) == 1)
                    {
                        CompleteDataGridRow(r);
                    }
                }
            }
        }

        /// <summary>
        /// Complete the DatagridView data from the selected checkbox or text
        /// </summary>
        /// <param name="_propName"></param>
        /// <param name="_checkBoxName"></param>
        /// <param name="nazwa"></param>
        public void CompleteGrid(string propName, string search)
        {
            try
            {
                foreach (var r in DbFunc<RecipesBase>.GetAll())
                {
                    string[] tab = r.RecipesName.Split(' ');

                    if (SearchName == 1)
                    {
                        if ((int)GetPropValue(r, propName) == 1 && (r.RecipesName.Contains(search) && r.RecipesName != string.Empty))
                        {
                            foreach (var item in tab)
                            {
                                if (item.StartsWith(search))
                                {
                                    CompleteDataGridRow(r);
                                }
                            }
                        }
                        else if ((int)GetPropValue(r, propName) == 1 && string.IsNullOrEmpty(search))
                        {
                            foreach (var item in tab)
                            {
                                if (item.StartsWith(search))
                                {
                                    CompleteDataGridRow(r);
                                }
                            }
                        }
                    }
                    else
                    {
                        if ((int)GetPropValue(r, propName) == 1 && (r.Ingredients.ToUpper().Contains(search)))
                        {
                            CompleteDataGridRow(r);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Complete the DatagridView data if they meet the 'vegeterian' condition from the selected checkbox or text
        /// </summary>
        /// <param name="_propName"></param>
        /// <param name="vegetarian"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        public bool CompleteGridVegetarian(string _propName, CheckBox vegetarian, string text)
        {
            bool found = false;
            try
            {
                if (vegetarian.Checked)
                {
                    foreach (var r in DbFunc<RecipesBase>.GetAll())
                    {
                        string[] tab = r.RecipesName.Split(' ');

                        if ((int)GetPropValue(r, _propName) == 1 && (int)GetPropValue(r, "Vegetarian") == 1)
                        {
                            if (SearchName == 1)
                            {
                                if (text != string.Empty)
                                {
                                    if (r.RecipesName.Contains(text))
                                    {
                                        foreach (var item in tab)
                                        {
                                            if (item.StartsWith(text))
                                            {
                                                CompleteDataGridRow(r);
                                                found = true;
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    CompleteDataGridRow(r);
                                    found = true;
                                }
                            }
                            else
                            {
                                if (r.Ingredients.ToUpper().Contains(text))
                                {
                                    CompleteDataGridRow(r);
                                    found = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return found;
        }

        /// <summary>
        /// Remove Rows after unchecking the checkbox.
        /// </summary>
        /// <param name="nameUncheckedCheckbox"></param>
        /// <param name="nameMain"></param>
        public void RemoveRowsAfterUncheckCheckBox(CheckBox nameUncheckedCheckbox, string nameMain)
        {
            bool delete;

            try
            {
                for (int i = _dgGrid.RowCount - 1; i >= 0; i--)
                {
                    delete = false;

                    for (int j = 0; j < _nameColumnInDataGrid.Length; j++)
                    {
                        if (Convert.ToInt32(_dgGrid.Rows[i].Cells[nameMain].Value) == 0 && nameUncheckedCheckbox.Checked == false) continue;

                        else if (Convert.ToInt32(_dgGrid.Rows[i].Cells[nameMain].Value) == 1 && nameUncheckedCheckbox.Checked) continue;

                        else if ((Convert.ToInt32(_dgGrid.Rows[i].Cells[nameMain].Value) == 1 && nameUncheckedCheckbox.Checked == false)
                            && (Convert.ToInt32(_dgGrid.Rows[i].Cells[_nameColumnInDataGrid[j]].Value) == 1
                           && nameUncheckedCheckbox.Checked))
                        {
                            delete = false;
                            break;
                        }
                        else
                        {
                            delete = true;
                        }
                    }

                    if (delete == true)
                    {
                        _dgGrid.Rows.Remove(_dgGrid.Rows[i]);
                    }
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        ///  Filtering results when entering data into the search engine
        /// </summary>
        /// <param name="number"></param>
        public void Search(int searchNumber)
        {
            try
            {
                for (int i = _dgGrid.RowCount - 1; i >= 0; i--)
                {
                    if (!_dgGrid.Rows[i].Cells[searchNumber].Value.ToString().ToUpper().Contains(_txtSeek))
                    {
                        _dgGrid.Rows.RemoveAt(i);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
        #endregion Methods
    }
}
