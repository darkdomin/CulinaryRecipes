using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using System.Xml.Serialization;

namespace CulinaryRecipes
{
   public class MealType
    {
        public int Id;
        public string MealName;

        public static List<MealType> categoryMealType = new List<MealType>
        {
            new MealType {Id=1,MealName="przekaski" },
            new MealType {Id=2,MealName="obiady" },
            new MealType {Id=3,MealName="zupy" },
            new MealType {Id=4,MealName="desery" },
            new MealType {Id=5,MealName="napoje" },
            new MealType {Id=6,MealName="przetwory" },
            new MealType {Id=7,MealName="salatki" },
        };
    }
}
