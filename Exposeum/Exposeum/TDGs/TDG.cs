using SQLite;
using Exposeum.Tables;

namespace Exposeum.TDGs
{
    public abstract class TDG
    {
        public SQLiteConnection _db = Tables.DBManager.GetInstance().GetConnection();
    }
}