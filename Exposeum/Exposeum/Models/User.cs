using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Exposeum.Models
{
    public class User
    {
        public Language _language;
        public List<ButtonText> _currentButtonString;
        public List<ButtonText> _frenchButtonString = new List<ButtonText>();
        public List<ButtonText> _englishButtonString = new List<ButtonText>();
        public List<int> _currentImageList;
        public List<int> _frenchImageList = new List<int>();
        public List<int> _englishImageList = new List<int>();
        public Boolean _visitor;
        private static User _user = new User(); 
        private User()
        {
            _englishButtonString.Add(new ButtonText("WalkThroughButton", "Skip"));
            _englishButtonString.Add(new ButtonText("freeTour", "Free Visit"));
            _englishButtonString.Add(new ButtonText("storyLine", "Guided Tours"));
            _englishButtonString.Add(new ButtonText("languageButton", "FR"));
            _englishButtonString.Add(new ButtonText("storyLineDialogButton", "Begin Journey"));
            _englishButtonString.Add(new ButtonText("storyInProgress", "Story In Progress"));
            _englishButtonString.Add(new ButtonText("storyLineDialogButtonToResume", "Resume"));
            _englishButtonString.Add(new ButtonText("storyLineDialogButtonToReset", "Restart"));
            _englishButtonString.Add(new ButtonText("wrongPointButton", "Dismiss"));
            _englishButtonString.Add(new ButtonText("pause_text", "Pause"));
            _englishButtonString.Add(new ButtonText("confirm_pause", "Yes"));
            _englishButtonString.Add(new ButtonText("reject_pause", "No"));
            _englishButtonString.Add(new ButtonText("pointofinterest_popup_dismiss", "Dismiss"));
            _englishButtonString.Add(new ButtonText("PauseItem", "Pause"));
            
            _frenchButtonString.Add(new ButtonText("WalkThroughButton", "Sauter"));
            _frenchButtonString.Add(new ButtonText("freeTour", "Visite Libre"));
            _frenchButtonString.Add(new ButtonText("storyLine", "Tours Guidés"));
            _frenchButtonString.Add(new ButtonText("languageButton", "EN"));
            _frenchButtonString.Add(new ButtonText("storyLineDialogButton", "Commencer La Visite"));
            _frenchButtonString.Add(new ButtonText("storyInProgress", "Actuellement en cours"));
            _frenchButtonString.Add(new ButtonText("storyLineDialogButtonToResume", "Continuer"));
            _frenchButtonString.Add(new ButtonText("storyLineDialogButtonToReset", "Recommencer"));
            _frenchButtonString.Add(new ButtonText("wrongPointButton", "Fermer"));
            _frenchButtonString.Add(new ButtonText("pause_text", "Suspendre"));
            _frenchButtonString.Add(new ButtonText("confirm_pause", "Oui"));
            _frenchButtonString.Add(new ButtonText("reject_pause", "Non"));
            _frenchButtonString.Add(new ButtonText("pointofinterest_popup_dismiss", "Fermer"));
            _frenchButtonString.Add(new ButtonText("PauseItem", "Pauser"));

            _frenchImageList.Add(Resource.Drawable.first_fr);
            _frenchImageList.Add(Resource.Drawable.second_fr);
            _frenchImageList.Add(Resource.Drawable.third_fr);
            _frenchImageList.Add(Resource.Drawable.fourth_fr);

            _englishImageList.Add(Resource.Drawable.first);
            _englishImageList.Add(Resource.Drawable.second);    
            _englishImageList.Add(Resource.Drawable.third);    
            _englishImageList.Add(Resource.Drawable.fourth);    
        }
        public static  User GetInstance()
        {
            return _user; 
        }
        public string GetButtonText(string id)
        {
            String text = null;
            foreach (var x in _currentButtonString)
            {
                if (x._id.Equals(id))
                    text = x._text;
            }
            return text;
        }
        public List<int> GetImageList()
        {
            return this._currentImageList;
        }
        public void SwitchLanguage(Language language)
        {
            _language = language;
            SetupLanguage();
        }
        public void SetupLanguage()
        {
            if (_language == Language.FR)
            {
                _currentButtonString = _frenchButtonString;
                _currentImageList = _frenchImageList;
            }
            else
            {
                _currentButtonString = _englishButtonString;
                _currentImageList = _englishImageList;
            }
        }
        public void ToogleLanguage()
        {
            if (_language == Language.EN)
            {
                _language = Language.FR;
                SetupLanguage();
            }
                
            else
            {
                _language = Language.EN;
                SetupLanguage();
            }
        }

    }
}