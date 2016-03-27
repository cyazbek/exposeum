
using NUnit.Framework;
using Exposeum.Tables;
using Exposeum.TDGs;
using SQLite;

namespace UnitTests
{
    [TestFixture]
    public class UserTdgTest
    {
        public readonly User SetObject = new User();
        public User TestObject;
        public readonly UserTdg tdg = UserTdg.GetInstance();
        public SQLiteConnection Db = DbManager.GetInstance().GetConnection();

        [SetUp]
        public void Setup()
        {
            SetObject.Id = 1;
            SetObject.Language = 0;
            SetObject.Visitor = 0;
        }

        [Test]
        public void GetInstanceUserTdgTest()
        {
            Assert.NotNull(UserTdg.GetInstance());
        }

        [Test]
        public void AddUserTest()
        {
            tdg.Add(SetObject);
            TestObject = Db.Get<User>(SetObject.Id);
            Assert.IsTrue(tdg.Equals(TestObject, SetObject));
        }

        [Test]
        public void GetUserTest()
        {
            tdg.Add(SetObject);
            TestObject = tdg.Get(SetObject.Id);
            Assert.IsTrue(this.tdg.Equals(TestObject, SetObject));
        }

        [Test]
        public void UpdateUserTest()
        {
            TestObject = new User
            {
                Id = 3,
                Visitor = 1,
                Language = 1
            };
            
            tdg.Add(TestObject);

            TestObject.Language = 0;
            tdg.Update(TestObject);

            TestObject = tdg.Get(TestObject.Id);
            Assert.AreEqual(0, TestObject.Language);
        }
    }
}