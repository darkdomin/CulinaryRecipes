using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CulinaryRecipes
{
    class Serialization
    {
        FileStream _fs;
        List<RecipesBase> _ls;
        XmlSerializer _xs;

        public Serialization(List<RecipesBase> ls, XmlSerializer xs)
        {
            this._ls = ls;
            this._xs = xs;
        }

        /// <summary>
        /// Export all files from database to XML
        /// </summary>
        /// <param name="accessPath"></param>
        public void ExportDatabase(string accessPath)
        {
            try
            {
                _fs = new FileStream(accessPath, FileMode.Create, FileAccess.Write);

                foreach (var r in RecipesBase.GetAll("RecipesBase"))
                {
                    _ls.Add(new RecipesBase(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams));
                }

                _xs.Serialize(_fs, _ls);
                _fs.Close();

                MessageBox.Show("Eksport bazy danych zakończył się sukcesem");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Export one selected file from database to XML
        /// </summary>
        /// <param name="accessPath"></param>
        /// <param name="IdFromDataGridView"></param>
        public void ExportDatabase(string accessPath, RecipesBase IdFromDataGridView)
        {
            try
            {
                _fs = new FileStream(accessPath, FileMode.Create, FileAccess.Write);

                _ls.Add(new RecipesBase(IdFromDataGridView.Id, IdFromDataGridView.RecipesName, IdFromDataGridView.Ingredients, IdFromDataGridView.AmountsMeal, IdFromDataGridView.ShortDescription, IdFromDataGridView.LongDescription, IdFromDataGridView.NumberPortions, IdFromDataGridView.CategoryCuisines, IdFromDataGridView.CategoryRating, IdFromDataGridView.CategoryDifficultLevel, IdFromDataGridView.CategoryPreparationTime, IdFromDataGridView.SnackMeal, IdFromDataGridView.DinnerMeal, IdFromDataGridView.SoupMeal, IdFromDataGridView.DessertMeal, IdFromDataGridView.DrinkMeal, IdFromDataGridView.PreservesMeal, IdFromDataGridView.SaladMeal, IdFromDataGridView.IdFishIngredients, IdFromDataGridView.IdPastaIngredients, IdFromDataGridView.IdFruitsIngredients, IdFromDataGridView.IdMuschroomsIngredients, IdFromDataGridView.IdBirdIngredients, IdFromDataGridView.IdMeatIngredients, IdFromDataGridView.IdEggsIngredients, IdFromDataGridView.PhotoLinkLocation, IdFromDataGridView.Vegetarian, IdFromDataGridView.Grams));

                _xs.Serialize(_fs, _ls);
                _fs.Close();

                MessageBox.Show("Eksport pliku zakończył się sukcesem.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}


