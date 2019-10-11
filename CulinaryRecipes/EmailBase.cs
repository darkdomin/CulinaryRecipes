namespace CulinaryRecipes
{
    public class EmailBase
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }

        public static void add(EmailBase objekt)
        {
            var db = Db.connect();
            var col = db.GetCollection<EmailBase>("EmailBase");
            col.Insert(objekt);
        }

        public static dynamic getAll(string nameBase)
        {
            var db = Db.connect();
            var col = db.GetCollection<EmailBase>(nameBase);
            return col.FindAll();

        }

        //znajdz po numerze ID
        public static EmailBase getById(int id)
        {
            var db = Db.connect();
            var col = db.GetCollection<EmailBase>("EmailBase");
            return col.FindById(id);
        }

        public static void del(int id)
        {
            var db = Db.connect();
            var col = db.GetCollection<EmailBase>("EmailBase");
            col.Delete(id);
        }

        public static void update(EmailBase p)
        {
            var db = Db.connect();
            var col = db.GetCollection<EmailBase>("EmailBase");
            col.Update(p);
        }

        public static void ClearDb()
        {
            using (var db = Db.connect())
            {
                db.DropCollection("EmailBase");
                db.Shrink();
            }
        }
    }
}
