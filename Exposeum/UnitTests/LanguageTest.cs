using System;
using NUnit.Framework;
using EstimoteSdk;
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
			Language.setLanguage ("fr"); 
			Assert.AreSame (Language.getLanguage (), "fr");
		}
		[Test()]
		public void setLanguageToEnglish()
		{
			Language.setLanguage ("en"); 
			Assert.AreSame (Language.getLanguage (), "en");
		}
		[Test()]
		public void toogleLanguageTestFromEnglish()
		{
			Language.setLanguage ("en"); 
			Language.toogleLanguage (); 
			Assert.AreSame (Language.getLanguage (), "fr");

		}
		[Test()]
		public void toogleLanguageTestFromFrench()
		{
			Language.setLanguage ("fr"); 
			Language.toogleLanguage (); 
			Assert.AreSame (Language.getLanguage (), "en");
		}
		[Test]
		public void Fail ()
		{
			Assert.False (true);
		}

		[Test]
		[Ignore ("another time")]
		public void Ignore ()
		{
			Assert.True (false);
		}

		[Test]
		public void Inconclusive ()
		{
			Assert.Inconclusive ("Inconclusive");
		}

	}
}

