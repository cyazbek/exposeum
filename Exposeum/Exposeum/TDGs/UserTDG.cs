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

    }
}