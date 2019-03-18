using System.Collections.Generic;

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
