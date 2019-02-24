using System.Text;
using System.Windows.Forms;

namespace CulinaryRecipes
{
    class SearchEngine
    {
         string txtSeek;
         DataGridView dgGrid;

        public  SearchEngine(string txtSeek, DataGridView dgGrid)
        {
            this.txtSeek = txtSeek;
            this.dgGrid = dgGrid;
        }


        public void FilldgGrid()
        {
            dgGrid.Rows.Clear();
            foreach (var r in RecipesBase.getAll())
            {
                dgGrid.Rows.Add(r.Id, r.RecipesName, r.Ingredients, r.AmountsMeal, r.ShortDescription, r.LongDescription, r.NumberPortions, r.CategoryCuisines, r.CategoryRating, r.CategoryDifficultLevel, r.CategoryPreparationTime, r.SnackMeal, r.DinnerMeal, r.SoupMeal, r.DessertMeal, r.DrinkMeal, r.PreservesMeal, r.SaladMeal, r.IdFishIngredients, r.IdPastaIngredients, r.IdFruitsIngredients, r.IdMuschroomsIngredients, r.IdBirdIngredients, r.IdMeatIngredients, r.IdEggsIngredients, r.PhotoLinkLocation, r.Vegetarian, r.Grams);
            }
        }

        public void Search(int number)
        {
            txtSeek = txtSeek.ToUpper();

            StringBuilder seek = new StringBuilder(txtSeek);

            FilldgGrid();

            string[] CopyDataGrid = new string[dgGrid.RowCount];

            for (int j = 0; j < dgGrid.RowCount; j++)
            {
                CopyDataGrid[j] = dgGrid.Rows[j].Cells[number].Value.ToString().ToUpper();
            }

            for (int i = 0; i < dgGrid.RowCount; i++)
            {
                if (txtSeek == "")
                {
                    FilldgGrid();
                }
                else if (!CopyDataGrid[i].Contains(txtSeek))
                {
                    dgGrid.Rows[i].Visible = false;
                }
                else if (CopyDataGrid[i].Contains(txtSeek))
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
}
