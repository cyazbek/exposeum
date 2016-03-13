using SQLite;

namespace Exposeum.TDGs
{
    public abstract class TDG
    {
        SQLiteConnection _db = Tables.DBManager.GetInstance().getConnection();
    }
}