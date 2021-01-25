using LiteDB;

namespace CulinaryRecipes
{
    class Db<T>
    {
        public static LiteDatabase Connect()
        {
            return new LiteDatabase("mydata.db");
        }
    }
}
