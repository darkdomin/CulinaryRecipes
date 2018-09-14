using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;

namespace CulinaryRecipes
{
    class Db
    {
        public static dynamic connect()
        {
            return new LiteDatabase("mydata.db");
        }
    }
}
