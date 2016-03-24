using System;
using System.Collections.Generic;

namespace Exposeum.Models
{
    public class User
    {
        public Language Language;
        public List<ButtonText> CurrentButtonString;
        public List<ButtonText> FrenchButtonString = new List<ButtonText>();
        public List<ButtonText> EnglishButtonString = new List<ButtonText>();
        public List<int> CurrentImageList;
        public List<int> FrenchImageList = new List<int>();
        public List<int> EnglishImageList = new List<int>();
        public Boolean Visitor;
        private static readonly User _user = new User(); 
        private User()
        {
            EnglishButtonString.Add(new ButtonText("WalkThroughButton", "Skip"));
            EnglishButtonString.Add(new ButtonText("freeTour", "Free Visit"));
            EnglishButtonString.Add(new ButtonText("storyLine", "Guided Tours"));
            EnglishButtonString.Add(new ButtonText("languageButton", "FR"));
            EnglishButtonString.Add(new ButtonText("storyLineDialogButton", "Begin Journey"));
            EnglishButtonString.Add(new ButtonText("storyInProgress", "Story In Progress"));
            EnglishButtonString.Add(new ButtonText("storyLineDialogButtonToResume", "Resume"));
            EnglishButtonString.Add(new ButtonText("storyLineDialogButtonToReset", "Restart"));
            EnglishButtonString.Add(new ButtonText("wrongPointButton", "Dismiss"));
            EnglishButtonString.Add(new ButtonText("pause_text", "Pause"));
            EnglishButtonString.Add(new ButtonText("confirm_pause", "Yes"));
            EnglishButtonString.Add(new ButtonText("reject_pause", "No"));
            EnglishButtonString.Add(new ButtonText("pointofinterest_popup_dismiss", "Dismiss"));
            EnglishButtonString.Add(new ButtonText("PauseItem", "Pause"));
            EnglishButtonString.Add(new ButtonText("LanguageItem", "Language"));
            EnglishButtonString.Add(new ButtonText("QRScannerItem", "Scan QR"));
            EnglishButtonString.Add(new ButtonText("TourModeTitle", "Tour Mode"));
            EnglishButtonString.Add(new ButtonText("StorylinesListTitle", "Storylines"));


            FrenchButtonString.Add(new ButtonText("WalkThroughButton", "Sauter"));
            FrenchButtonString.Add(new ButtonText("freeTour", "Visite Libre"));
            FrenchButtonString.Add(new ButtonText("storyLine", "Tours Guid�s"));
            FrenchButtonString.Add(new ButtonText("languageButton", "EN"));
            FrenchButtonString.Add(new ButtonText("storyLineDialogButton", "Commencer La Visite"));
            FrenchButtonString.Add(new ButtonText("storyInProgress", "Actuellement en cours"));
            FrenchButtonString.Add(new ButtonText("storyLineDialogButtonToResume", "Continuer"));
            FrenchButtonString.Add(new ButtonText("storyLineDialogButtonToReset", "Recommencer"));
            FrenchButtonString.Add(new ButtonText("wrongPointButton", "Fermer"));
            FrenchButtonString.Add(new ButtonText("pause_text", "Suspendre"));
            FrenchButtonString.Add(new ButtonText("confirm_pause", "Oui"));
            FrenchButtonString.Add(new ButtonText("reject_pause", "Non"));
            FrenchButtonString.Add(new ButtonText("pointofinterest_popup_dismiss", "Fermer"));
            FrenchButtonString.Add(new ButtonText("PauseItem", "Pauser"));
            FrenchButtonString.Add(new ButtonText("LanguageItem", "Langue"));
            FrenchButtonString.Add(new ButtonText("QRScannerItem", "Scanner QR"));
            FrenchButtonString.Add(new ButtonText("TourModeTitle", "Mode de Tour"));
            FrenchButtonString.Add(new ButtonText("StorylinesListTitle", "Histoire"));



            FrenchImageList.Add(Resource.Drawable.first_fr);
            FrenchImageList.Add(Resource.Drawable.second_fr);
            FrenchImageList.Add(Resource.Drawable.third_fr);
            FrenchImageList.Add(Resource.Drawable.fourth_fr);

            EnglishImageList.Add(Resource.Drawable.first);
            EnglishImageList.Add(Resource.Drawable.second);    
            EnglishImageList.Add(Resource.Drawable.third);    
            EnglishImageList.Add(Resource.Drawable.fourth);    
        }
        public static  User GetInstance()
        {
            return _user; 
        }
        public string GetButtonText(string id)
        {
            String text = null;
            foreach (var x in CurrentButtonString)
            {
                if (x.Id.Equals(id))
                    text = x.Text;
            }
            return text;
        }
        public List<int> GetImageList()
        {
            return CurrentImageList;
        }
        public void SwitchLanguage(Language language)
        {
            Language = language;
            SetupLanguage();
        }
        public void SetupLanguage()
        {
            if (Language == Language.Fr)
            {
                CurrentButtonString = FrenchButtonString;
                CurrentImageList = FrenchImageList;
            }
            else
            {
                CurrentButtonString = EnglishButtonString;
                CurrentImageList = EnglishImageList;
            }
        }
        public void ToogleLanguage()
        {
            if (Language == Language.En)
            {
                Language = Language.Fr;
                SetupLanguage();
            }
                
            else
            {
                Language = Language.En;
                SetupLanguage();
            }
        }

    }
}