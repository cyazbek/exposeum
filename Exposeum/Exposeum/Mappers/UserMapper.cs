using Exposeum.Models;
using Exposeum.TDGs;

namespace Exposeum.Mappers
{
    public class UserMapper
    {
        private static UserMapper _instance;
        private UserTdg _tdg;
        private readonly User _user; 

        private UserMapper()
        {
            _tdg = UserTdg.GetInstance();
            _user = Models.User.GetInstance(); 
        }

        public static UserMapper GetInstance()
        {
            if(_instance == null)
                _instance = new UserMapper();
            return _instance;
        }

        public Tables.User UserModelToTable(User user)
        {
            int id = user.Id;
            int lang;
            int visited;

            if (user.Language == Models.Language.Fr)
                lang = 0;
            else lang = 1;

            if (user.Visitor)
                visited = 1;
            else visited = 0;

            return new Tables.User
            {
                Id = id,
                Language = lang,
                Visitor = visited
            };
        }

        public User UserTableToModel(Tables.User user)
        {
            Language language;
            bool visited;

            if (user.Language == 0)
            {
                language = Models.Language.Fr;
            }
            else language = Models.Language.En;

            if (user.Visitor == 0)
                visited = false;
            else visited = true;

            _user.Id = user.Id;
            _user.Language = language;
            _user.Visitor = visited;
            return _user;
        }

        public void AddUser(User user)
        {
            Tables.User tableUser = UserModelToTable(user);
            _tdg.Add(tableUser);
        }

        public User GetUser(int id)
        {
            return UserTableToModel(_tdg.Get(id));
        }

        public void UpdateUser(User user)
        {
            Tables.User tableUser = UserModelToTable(user);
            _tdg.Update(tableUser);
        }
    }
}