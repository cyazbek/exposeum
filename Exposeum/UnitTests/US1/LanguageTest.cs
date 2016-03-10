using System;
using NUnit.Framework;
using Android.App;
using Exposeum;
using System.Collections.Generic;
using Exposeum.Models; 

namespace UnitTests
{
	[TestFixture]
	public class LanguageTest
	{
		[Test()]
		public void setLanguageToFrench()
		{
			Language.SetLanguage ("fr"); 
			Assert.AreSame (Language.GetLanguage (), "fr");
		}
		[Test()]
		public void setLanguageToEnglish()
		{
			Language.SetLanguage ("en"); 
			Assert.AreSame (Language.GetLanguage (), "en");
		}
		[Test()]
		public void toogleLanguageTestFromEnglish()
		{
			Language.SetLanguage ("en"); 
			Language.ToogleLanguage (); 
			Assert.AreSame (Language.GetLanguage (), "fr");

		}
		[Test()]
		public void toogleLanguageTestFromFrench()
		{
			Language.SetLanguage ("fr"); 
			Language.ToogleLanguage (); 
			Assert.AreSame (Language.GetLanguage (), "en");
		}


	}
}

