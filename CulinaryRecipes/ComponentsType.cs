using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CulinaryRecipes
{
    class ComponentsType
    {
            public int Id;
            public string ComponentName;

        public static List<ComponentsType> categoryComponentsType = new List<ComponentsType>
        {
            new ComponentsType {Id=1,ComponentName="ryby" },
            new ComponentsType {Id=2,ComponentName="kasze" },
            new ComponentsType {Id=3,ComponentName="owoce" },
            new ComponentsType {Id=4,ComponentName="grzyby" },
            new ComponentsType {Id=5,ComponentName="drob" },
            new ComponentsType {Id=6,ComponentName="mieso" },
            new ComponentsType {Id=7,ComponentName="jaja" },
        };
    }
}
