using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace CulinaryRecipes
{
    class PreparationTime
    {
        public int Id;
        public string TimeName;

        public static List<PreparationTime> categoryPreparationTime = new List<PreparationTime>
        {
            new PreparationTime {Id=1,TimeName="do30" },
            new PreparationTime {Id=2,TimeName="do60" },
            new PreparationTime {Id=3,TimeName="do90" },
            new PreparationTime {Id=4,TimeName="pow90" },
        };
    }
}
