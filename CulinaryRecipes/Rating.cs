using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiteDB;

namespace CulinaryRecipes
{
    class Rating
    {
        public int Id;
        public string RatingName;

        public static List<Rating> categoryRating = new List<Rating>
        {
            new Rating {Id=1,RatingName="gwiazdka" },
            new Rating {Id=2,RatingName="gwiazdkaDwa" },
            new Rating {Id=3,RatingName="gwiazdkaTrzy" },
        };
        
       
    }
}
