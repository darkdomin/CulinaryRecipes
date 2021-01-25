using CulinaryRecipes.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace CulinaryRecipes
{
    class Deserialization
    {
        FileStream _fs;
        List<RecipesBase>_ls;
        XmlSerializer _xs;
        int NewId;

        public Deserialization(List<RecipesBase> ls, XmlSerializer xs)
        {
            this._ls = ls;
            this._xs = xs;
        }

        /// <summary>
        /// Import XML file to database
        /// </summary>
        /// <param name="accessPath"></param>
        public void ImportFileFromXML(string accessPath)
        {
            try
            {
                _fs = new FileStream(accessPath, FileMode.Open, FileAccess.Read);
                _ls = (List<RecipesBase>)_xs.Deserialize(_fs);

                RecipesBase m = new RecipesBase();

                if (DbFunc<RecipesBase>.GetCount() <= 0)
                {
                    foreach (var r in _ls)
                    {
                        m.Id = r.Id; m.RecipesName = r.RecipesName; m.Ingredients = r.Ingredients; m.AmountsMeal = r.AmountsMeal; m.ShortDescription = r.ShortDescription; m.LongDescription = r.LongDescription; m.NumberPortions = r.NumberPortions; m.CategoryCuisines = r.CategoryCuisines; m.CategoryRating = r.CategoryRating; m.CategoryDifficultLevel = r.CategoryDifficultLevel; m.CategoryPreparationTime = r.CategoryPreparationTime; m.SnackMeal = r.SnackMeal; m.DinnerMeal = r.DinnerMeal; m.SoupMeal = r.SoupMeal; m.DessertMeal = r.DessertMeal; m.DrinkMeal = r.DrinkMeal; m.PreservesMeal = r.PreservesMeal; m.SaladMeal = r.SaladMeal; m.IdFishIngredients = r.IdFishIngredients; m.IdPastaIngredients = r.IdPastaIngredients; m.IdFruitsIngredients = r.IdFruitsIngredients; m.IdMuschroomsIngredients = r.IdMuschroomsIngredients; m.IdBirdIngredients = r.IdBirdIngredients; m.IdMeatIngredients = r.IdMeatIngredients; m.IdEggsIngredients = r.IdEggsIngredients; m.PhotoLinkLocation = r.PhotoLinkLocation; m.Vegetarian = r.Vegetarian; m.Grams = r.Grams;
                        DbFunc<RecipesBase>.Add(m);
                    }

                    MessageBox.Show("Baza danych została zaimportowana");
                    _fs.Close();
                }
                else
                {
                    MessageBox.Show("Baza danych przed importem musi zostać wyczyszczona.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Import single XML file to database
        /// </summary>
        /// <param name="accessPath"></param>
        public void ImportSingleFIleFromXML(string accessPath)
        {
            try
            {
                foreach (var r in DbFunc<RecipesBase>.GetAll())
                {
                    NewId = r.Id;
                }

                _fs = new FileStream(accessPath, FileMode.Open, FileAccess.Read);
                _ls = (List<RecipesBase>)_xs.Deserialize(_fs);

                RecipesBase m = new RecipesBase();

                foreach (var r in _ls)
                {
                    m.Id = NewId+1; m.RecipesName = r.RecipesName; m.Ingredients = r.Ingredients; m.AmountsMeal = r.AmountsMeal; m.ShortDescription = r.ShortDescription; m.LongDescription = r.LongDescription; m.NumberPortions = r.NumberPortions; m.CategoryCuisines = r.CategoryCuisines; m.CategoryRating = r.CategoryRating; m.CategoryDifficultLevel = r.CategoryDifficultLevel; m.CategoryPreparationTime = r.CategoryPreparationTime; m.SnackMeal = r.SnackMeal; m.DinnerMeal = r.DinnerMeal; m.SoupMeal = r.SoupMeal; m.DessertMeal = r.DessertMeal; m.DrinkMeal = r.DrinkMeal; m.PreservesMeal = r.PreservesMeal; m.SaladMeal = r.SaladMeal; m.IdFishIngredients = r.IdFishIngredients; m.IdPastaIngredients = r.IdPastaIngredients; m.IdFruitsIngredients = r.IdFruitsIngredients; m.IdMuschroomsIngredients = r.IdMuschroomsIngredients; m.IdBirdIngredients = r.IdBirdIngredients; m.IdMeatIngredients = r.IdMeatIngredients; m.IdEggsIngredients = r.IdEggsIngredients; m.PhotoLinkLocation = r.PhotoLinkLocation; m.Vegetarian = r.Vegetarian; m.Grams = r.Grams;
                    DbFunc<RecipesBase>.Add(m);
                }

                MessageBox.Show(m.RecipesName + "\n" + "został zaimportowany.", "PLIK ");
                _fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
