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
