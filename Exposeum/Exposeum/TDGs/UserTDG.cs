using Exposeum.Tables; 

namespace Exposeum.TDGs
{
    public class UserTdg:Tdg
    {
        private static UserTdg _instance;

        private UserTdg() { }

        public static UserTdg GetInstance()
        {
            if (_instance == null)
                _instance = new UserTdg();
            return _instance; 
        }

        public User Get(int id)
        {
            return Db.Get<User>(id);
        }

        public void Add(User user)
        {
            Db.InsertOrReplace(user);
        }

        public void Update(User user)
        {
            Db.Update(user);
        }

        public bool Equals(User user1, User user2)
        {
            if (user1.Id == user2.Id && user1.Visitor == user2.Visitor && user1.Language == user2.Language)
                return true;
            return false;
        }

        public int GetSize()
        {
            return Db.Table<User>().Count();
        }

    }
}