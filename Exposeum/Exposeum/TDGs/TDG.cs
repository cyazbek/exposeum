using SQLite;

namespace Exposeum.TDGs
{
    public abstract class Tdg
    {
        public SQLiteConnection Db = Tables.DbManager.GetInstance().GetConnection();
    }
}