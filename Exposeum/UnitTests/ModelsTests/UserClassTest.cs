using Exposeum.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace UnitTests
{
	[TestFixture]
	public class UserClassTest
    {
        readonly User _user = User.GetInstance();
        Language _language;

        [Test]
        public void GetInstanceUserTest()
        {
            Assert.NotNull(User.GetInstance());
        }

        [Test()]
        public void SetLanguageToFrenchTest()
		{
            _language = Language.Fr;
            _user.Language = _language; 
			Assert.AreEqual(_user.Language.ToString(), Language.Fr.ToString());
		}

        [Test()]
		public void SetLanguageToEnglishTest()
		{
            _language = Language.En;
            _user.Language = _language;
            Assert.AreEqual(_user.Language.ToString(), Language.En.ToString());
        }

		[Test()]
		public void SwitchLanguageTest()
		{
            _user.SwitchLanguageTo(Language.Fr);
            Assert.AreEqual(_user.Language.ToString(), Language.Fr.ToString());
        }
        [Test()]
        public void GetTextOfButtonTest()
        {
            List<ButtonText> buttonText = _user.FrenchButtonString;
            _user.SwitchLanguageTo(Language.Fr);
            Assert.AreSame(_user.CurrentButtonString, buttonText);
        }
        [Test()]
        public void GetFrenchImageTest()
        {
            List<int> images = _user.FrenchImageList;
            _user.SwitchLanguageTo(Language.Fr);
            Assert.AreSame(_user.CurrentImageList, images);
        }
        [Test()]
        public void GetEnglishImageTest()
        {
            List<int> images = _user.EnglishImageList;
            _user.SwitchLanguageTo(Language.En);
            Assert.AreSame(_user.CurrentImageList, images);
        }

        [Test()]
        public void SetUpLanguageTest()
        {
            List<ButtonText> buttonText = _user.EnglishButtonString;
            _user.Language = Language.En;
            _user.SetupLanguage();
            Assert.AreSame(_user.CurrentButtonString, buttonText);
        }
        [Test()]
        public void ToogleLanguageTest()
        {
            Language language1 = Language.Fr;
            _user.SwitchLanguageTo(language1);
            _user.ToogleLanguage();
            Assert.AreNotEqual(_user.Language.ToString(),language1.ToString());
        }


    }
}

