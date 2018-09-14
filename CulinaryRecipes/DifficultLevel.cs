using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace CulinaryRecipes
{
    class DifficultLevel
    {
        public int Id;
        public string LevelName;

        static List<DifficultLevel> categoryDifficultLevel = new List<DifficultLevel>
        {
            new DifficultLevel {Id=1,LevelName="latwy" },
            new DifficultLevel {Id=2,LevelName="sredni" },
            new DifficultLevel {Id=3,LevelName="trudny" },
        };
    }
}
