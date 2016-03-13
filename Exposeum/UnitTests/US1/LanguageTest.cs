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
			Language1.SetLanguage ("fr"); 
			Assert.AreSame (Language1.GetLanguage (), "fr");
		}
		[Test()]
		public void setLanguageToEnglish()
		{
			Language1.SetLanguage ("en"); 
			Assert.AreSame (Language1.GetLanguage (), "en");
		}
		[Test()]
		public void toogleLanguageTestFromEnglish()
		{
			Language1.SetLanguage ("en"); 
			Language1.ToogleLanguage (); 
			Assert.AreSame (Language1.GetLanguage (), "fr");

		}
		[Test()]
		public void toogleLanguageTestFromFrench()
		{
			Language1.SetLanguage ("fr"); 
			Language1.ToogleLanguage (); 
			Assert.AreSame (Language1.GetLanguage (), "en");
		}


	}
}

