using SQLite;

namespace Exposeum.TDGs
{
    public abstract class TDG
    {
        public SQLiteConnection _db = Tables.DBManager.GetInstance().getConnection();
    }
}