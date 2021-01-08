using LiteDB;

namespace CulinaryRecipes
{
    class Db
    {
        public static LiteDatabase connect()
        {
            return new LiteDatabase("mydata.db");
        }
    }
}
