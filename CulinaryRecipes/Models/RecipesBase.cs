using System.Xml.Serialization;

namespace CulinaryRecipes
{
    public class RecipesBase
    {
        [XmlAttribute("Id")]
        public int Id { get;  set; }
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
    }
}
