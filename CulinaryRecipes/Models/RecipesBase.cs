
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CulinaryRecipes
{
    public class RecipesBase
    {
        [XmlAttribute("Id")]
        public int Id { get; set; }
        [XmlElement("NazwaPrzepisu")]
        public string RecipesName { get; set; }
        [XmlElement("Składniki")]
        public string Ingredients { get; set; }
        [XmlElement("ilościSkladnikow")]
        public string AmountsMeal { get; set; }
        [XmlElement("StreszczeniePrzepisu")]
        public string ShortDescription { get; set; }
        [XmlElement("InstrukcjaPrzepsiu")]
        public string LongDescription { get; set; }
        [XmlElement("Porcje")]
        public int NumberPortions { get; set; }
        [XmlElement("RodzajKuchnia")]
        public string CategoryCuisines { get; set; }
        [XmlElement("Rating")]
        public string CategoryRating { get; set; }
        [XmlElement("Stopientrudnosci")]
        public string CategoryDifficultLevel { get; set; }
        [XmlElement("CzasPrzygotowania")]
        public string CategoryPreparationTime { get; set; }

        #region Meal
        [XmlElement("Snack")]
        public int SnackMeal { get; set; }
        [XmlElement("Dinner")]
        public int DinnerMeal { get; set; }
        [XmlElement("Soup")]
        public int SoupMeal { get; set; }
        [XmlElement("Dessert")]
        public int DessertMeal { get; set; }
        [XmlElement("Drink")]
        public int DrinkMeal { get; set; }
        [XmlElement("Preserves")]
        public int PreservesMeal { get; set; }
        [XmlElement("Salad")]
        public int SaladMeal { get; set; }
        #endregion
        #region Ingredients
        [XmlElement("IdRyby")]
        public int IdFishIngredients { get; set; }
        [XmlElement("IdSypkie")]
        public int IdPastaIngredients { get; set; }
        [XmlElement("IdOwoce")]
        public int IdFruitsIngredients { get; set; }
        [XmlElement("IdGrzyby")]
        public int IdMuschroomsIngredients { get; set; }
        [XmlElement("IdDrob")]
        public int IdBirdIngredients { get; set; }
        [XmlElement("IdMieso")]
        public int IdMeatIngredients { get; set; }
        [XmlElement("IdJaja")]
        public int IdEggsIngredients { get; set; }
        #endregion

        [XmlElement("Zdjecie")]
        public string PhotoLinkLocation { get; set; }
        [XmlElement("Wegetariańskie")]
        public int Vegetarian { get; set; }
        [XmlElement("Gramatura")]
        public string Grams { get; set; }

        public RecipesBase() { }

        public RecipesBase(int Id, string Name, string Ingredients, string Amounts, string ShortDescription, string LongDescription, int NumberPortions, string CategoryCuisines, string CategoryRating, string categoryDifficultLevel, string categoryPreparationTime, int snack, int dinner, int soup, int dessert, int drink, int preserves, int salad, int fish, int pasta, int fruits, int muschrooms, int bird, int meat, int eggs, string photoLinkLocation, int vegetarian, string grams)
        {
            this.Id = Id;
            this.RecipesName = Name;
            this.Ingredients = Ingredients;
            this.AmountsMeal = Amounts;
            this.ShortDescription = ShortDescription;
            this.LongDescription = LongDescription;
            this.NumberPortions = NumberPortions;
            this.CategoryCuisines = CategoryCuisines;
            this.CategoryRating = CategoryRating;
            this.CategoryDifficultLevel = categoryDifficultLevel;
            this.CategoryPreparationTime = categoryPreparationTime;

            this.SnackMeal = snack;
            this.DinnerMeal = dinner;
            this.SoupMeal = soup;
            this.DessertMeal = dessert;
            this.DrinkMeal = drink;
            this.PreservesMeal = preserves;
            this.SaladMeal = salad;
            this.IdFishIngredients = fish;
            this.IdPastaIngredients = pasta;
            this.IdFruitsIngredients = fruits;
            this.IdMuschroomsIngredients = muschrooms;
            this.IdBirdIngredients = bird;
            this.IdMeatIngredients = meat;
            this.IdEggsIngredients = eggs;
            this.PhotoLinkLocation = photoLinkLocation;
            this.Vegetarian = vegetarian;
            this.Grams = grams;
        }

        private static LiteDB.LiteCollection<RecipesBase> Join()
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase"); 
            return col;
        }

        /// <summary>
        /// Add file to database
        /// </summary>
        /// <param name="objekt"></param>
        public static void Add(RecipesBase objekt)
        {
            dynamic col = Join();
            col.Insert(objekt);
        }

        /// <summary>
        /// Get all files
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<RecipesBase> GetAll(string nameBase) 
        {
            var col = Join();
            return col.FindAll();
        }

        /// <summary>
        ///  Get all files
        /// </summary>
        /// <param name="nameBase"></param>
        /// <returns></returns>
        //public static dynamic GetAll(string nameBase)
        //{
        //    var col = Join();
        //    return col.FindAll();
        //}

        /// <summary>
        /// Delete single file form Database
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteSingleFile(int id)
        {
            var col = Join();
            col.Delete(id);
        }

        /// <summary>
        /// Find by id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RecipesBase GetById(int id)
        {
            var col = Join();
            return col.FindById(id);
        }

        /// <summary>
        /// Update DataBase
        /// </summary>
        /// <param name="p"></param>
        public static void Update(RecipesBase p)
        {
            var col = Join();
            col.Update(p);
        }

        /// <summary>
        /// Remove all files from DataBase
        /// </summary>
        public static void ClearDb()
        {
            using (var db = Db.connect())
            {
                db.DropCollection("RecipesBase");
                db.Shrink();
            }
        }

        /// <summary>
        /// Get the number of items in the database
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            var col = Join();
            return col.Count();
        }
    }
}
