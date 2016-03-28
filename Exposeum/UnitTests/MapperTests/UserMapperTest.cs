using Exposeum.Mappers;
using Exposeum.Models;
using Exposeum.TDGs;
using NUnit.Framework;
using User = Exposeum.Tables.User;

namespace UnitTests
{
    [TestFixture]
    public class UserMapperTest
    {
        private static UserMapper _instance;
        private Exposeum.Models.User _expected;
        private Exposeum.Models.User _userModel;
        private User _userTable;
        readonly UserTdg _tdg = UserTdg.GetInstance();

        [SetUp]
        public void SetUp()
        {
            _instance = UserMapper.GetInstance();
            _tdg.Db.DeleteAll<User>();

            _userModel = Exposeum.Models.User.GetInstance();

            _userTable = new User
            {
                Id = 1,
                Language = 1,
                Visitor = 1
            };
        }

        [Test]
        public void GetInstanceUserMapperTest()
        {
            Assert.NotNull(UserMapper.GetInstance());
        }

        [Test]
        public void AddUserTest()
        {
            _instance.AddUser(_userModel);
            _expected = _instance.GetUser(_userModel.Id);
            Assert.True(_userModel.Equals(_expected));
        }

        [Test]
        public void UpdateUserTest()
        {
            _tdg.Db.DeleteAll<User>();

            _userModel = Exposeum.Models.User.GetInstance();
            _userModel.Language = Language.En;
            _userModel.Visitor = false;
                
            _instance.AddUser(_userModel);

            _userModel.Language = Language.Fr;
            _instance.UpdateUser(_userModel);

            _expected = _instance.GetUser(_userModel.Id);
            Assert.AreEqual(Language.Fr, _expected.Language);
        }

        [Test]
        public void UserModelToTableTest()
        {
            _userModel = Exposeum.Models.User.GetInstance();
            _userModel.Id = 1;
            _userModel.Language = Language.En;
            _userModel.Visitor = true;

            User expected = _instance.ConvertToTable(_userModel);
            Assert.IsTrue(UserTdg.GetInstance().Equals(_userTable, expected));
        }

        [Test]
        public void UserTableToModelTest()
        {
            Exposeum.Models.User expected = _instance.ConvertToModel(_userTable);
            Assert.IsTrue(_userModel.Equals(expected));
        }

    }
}