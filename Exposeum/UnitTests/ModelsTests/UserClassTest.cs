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
        [Test()]
        public void SetLanguageToFrenchTest()
		{
            _language = Language.Fr;
            _user._language = _language; 
			Assert.AreEqual(_user._language.ToString(), Language.Fr.ToString());
		}
		[Test()]
		public void SetLanguageToEnglishTest()
		{
            _language = Language.En;
            _user._language = _language;
            Assert.AreEqual(_user._language.ToString(), Language.En.ToString());
        }

		[Test()]
		public void SwitchLanguageTest()
		{
            _user.SwitchLanguage(Language.Fr);
            Assert.AreEqual(_user._language.ToString(), Language.Fr.ToString());
        }
        [Test()]
        public void GetTextOfButtonTest()
        {
            List<ButtonText> buttonText = _user._frenchButtonString;
            _user.SwitchLanguage(Language.Fr);
            Assert.AreSame(_user._currentButtonString, buttonText);
        }
        [Test()]
        public void GetFrenchImageTest()
        {
            List<int> images = _user._frenchImageList;
            _user.SwitchLanguage(Language.Fr);
            Assert.AreSame(_user._currentImageList, images);
        }
        [Test()]
        public void GetEnglishImageTest()
        {
            List<int> images = _user._englishImageList;
            _user.SwitchLanguage(Language.En);
            Assert.AreSame(_user._currentImageList, images);
        }

        [Test()]
        public void SetUpLanguageTest()
        {
            List<ButtonText> buttonText = _user._englishButtonString;
            _user._language = Language.En;
            _user.SetupLanguage();
            Assert.AreSame(_user._currentButtonString, buttonText);
        }
        [Test()]
        public void ToogleLanguageTest()
        {
            Language language1 = Language.Fr;
            _user.SwitchLanguage(language1);
            _user.ToogleLanguage();
            Assert.AreNotEqual(_user._language.ToString(),language1.ToString());
        }


    }
}

