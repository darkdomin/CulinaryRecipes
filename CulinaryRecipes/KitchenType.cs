using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace CulinaryRecipes
{
    class KitchenType
    {
        public int Id;
        public string KitchenName;

       public static List<KitchenType> categoryKitchenType = new List<KitchenType>
        {
            new KitchenType {Id=1,KitchenName="amerykanska" },
            new KitchenType {Id=2,KitchenName="azjatycka" },
            new KitchenType {Id=3,KitchenName="czeska" },
            new KitchenType {Id=4,KitchenName="francuska" },
            new KitchenType {Id=5,KitchenName="grecka" },
            new KitchenType {Id=6,KitchenName="iberyjska" },
            new KitchenType {Id=7,KitchenName="polska" },
            new KitchenType {Id=8,KitchenName="wloska" },
        };
    }
}
