using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
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
        public string Vegetarian { get; set; }
        [XmlElement("Gramatura")]
        public string Grams { get; set; }

        public RecipesBase()
        {

        }

        public RecipesBase(int Id, string Name, string Ingredients, string Amounts, string ShortDescription, string LongDescription, int NumberPortions, string CategoryCuisines, string CategoryRating, string categoryDifficultLevel, string categoryPreparationTime, int snack, int dinner, int soup, int dessert, int drink, int preserves, int salad, int fish, int pasta, int fruits, int muschrooms, int bird, int meat, int eggs, string photoLinkLocation,string vegetarian,string grams)
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

        public static void add(RecipesBase objekt)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            col.Insert(objekt);
        }
        //wypełnij
        public static dynamic getAll()
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            return col.FindAll();

        }
        //usuń
        public static void del(int id)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            col.Delete(id);
        }
        //znajdz po numerze ID
        public static RecipesBase getById(int id)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            return col.FindById(id);
        }
        //aktualizuj
        public static void update(RecipesBase p)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            col.Update(p);
        }

        public static void Export(string sciezka)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            var json = JsonSerializer.Serialize(new BsonArray(db.Engine.Find("RecipesBase")));
        }

        public static void Import(string sciezka)
        {
            var db = Db.connect();
            var col = db.GetCollection<RecipesBase>("RecipesBase");
            db.Engine.Insert(col, JsonSerializer.Deserialize(sciezka).AsArray.ToArray());
        }

        public static void ClearDb()
        {
            using (var db = Db.connect())
            {
                db.DropCollection("RecipesBase");
                db.Shrink();
            }
        }
        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }
    }
}