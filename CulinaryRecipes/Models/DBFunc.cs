using LiteDB;
using System.Collections.Generic;

namespace CulinaryRecipes.Models
{
    public class DbFunc<T>
    {
        private static LiteCollection<T> Join()
        {
            var db = Db<T>.Connect();
            var col = db.GetCollection<T>();
            return col;
        }

        /// <summary>
        /// Add file to database
        /// </summary>
        /// <param name="objekt"></param>
        public static void Add(T objekt)
        {
            var col = Join();
            col.Insert(objekt);
        }

        /// <summary>
        /// Get all files
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> GetAll()
        {
            var col = Join();
            return col.FindAll();
        }


        /// <summary>
        /// Delete single file form Database
        /// </summary>
        /// <param name="id"></param>
        public static void DeleteSingleFile(int id)
        {
            var col = Join();
            col.Delete(id);
        }

        /// <summary>
        /// Find by id number
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetById(int id)
        {
            var col = Join();
            return col.FindById(id);
        }

        /// <summary>
        /// Update DataBase
        /// </summary>
        /// <param name="p"></param>
        public static void Update(T p)
        {
            var col = Join();
            col.Update(p);
        }

        /// <summary>
        /// Remove all files from DataBase
        /// </summary>
        public static void ClearDb(string nameDataBase)
        {
            using (var db = Db<T>.Connect())
            {
                db.DropCollection(nameDataBase);
                db.Shrink();
            }
        }

        /// <summary>
        /// Get the number of items in the database
        /// </summary>
        /// <returns></returns>
        public static int GetCount()
        {
            var col = Join();
            return col.Count();
        }
    }
}
