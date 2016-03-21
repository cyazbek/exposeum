using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using StoryLineDescription = Exposeum.TempModels.StorylineDescription;

namespace Exposeum.Mappers
{
    public class StoryLineDescriptionMapper
    {
        private static StoryLineDescriptionMapper _instance;
        private readonly StoryLineDescriptionEnTDG _storyLineDescriptionEnTdg;
        private readonly StoryLineDescriptionFrTDG _storyLineDescriptionFrTdg;
        private StoryLineDescriptionEn _storyLineDescriptionEn;
        private StoryLineDescriptionFr _storyLineDescriptionFr;

        private StoryLineDescriptionMapper()
        {
            _storyLineDescriptionEnTdg = StoryLineDescriptionEnTDG.GetInstance();
            _storyLineDescriptionFrTdg = StoryLineDescriptionFrTDG.GetInstance();
        }

        public static StoryLineDescriptionMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionMapper();
            return _instance;
        }

        public void AddStoryLineDescription(StoryLineDescription storyLineDescription)
        {
            if (storyLineDescription._language == Language.Fr)
            {
                _storyLineDescriptionFr = StoryLineDescriptionModelToTableFr(storyLineDescription);
                _storyLineDescriptionFrTdg.Add(_storyLineDescriptionFr);
            }
            else
            {
                _storyLineDescriptionEn = StoryLineDescriptionModelToTableEn(storyLineDescription);
                _storyLineDescriptionEnTdg.Add(_storyLineDescriptionEn);
            }
        }

        public void UpdateStoryLineDescription(StoryLineDescription storyLineDescription)
        {
            if (storyLineDescription._language == Language.Fr)
            {
                _storyLineDescriptionFr = StoryLineDescriptionModelToTableFr(storyLineDescription);
                _storyLineDescriptionFrTdg.Update(_storyLineDescriptionFr);
            }
            else
            {
                _storyLineDescriptionEn = StoryLineDescriptionModelToTableEn(storyLineDescription);
                _storyLineDescriptionEnTdg.Update(_storyLineDescriptionEn);
            }
        }

        public StoryLineDescription GetStoryLineDescription(int storyLineDescriptionId)
        {
            StoryLineDescription storyLineDescriptionModel;

            if (User.GetInstance()._language == Language.Fr)
            {
                _storyLineDescriptionFr = _storyLineDescriptionFrTdg.GetStoryLineDescriptionFr(storyLineDescriptionId);
                storyLineDescriptionModel = StorylineDescriptionTableToModelFr(_storyLineDescriptionFr);
            }
            else
            {
                _storyLineDescriptionEn = _storyLineDescriptionEnTdg.GetStoryLineDescriptionEn(storyLineDescriptionId);
                storyLineDescriptionModel = StorylineDescriptionTableToModelEn(_storyLineDescriptionEn);
            }

            return storyLineDescriptionModel;
        }

        public StoryLineDescriptionFr StoryLineDescriptionModelToTableFr(StoryLineDescription storyLineDescription)
        {
            StoryLineDescriptionFr storyLineDescriptionTable = new StoryLineDescriptionFr
            {
                ID = storyLineDescription._storyLineDescriptionId,
                title = storyLineDescription._title,
                description = storyLineDescription._description
            };

            return storyLineDescriptionTable;
        }

        public StoryLineDescriptionEn StoryLineDescriptionModelToTableEn(StoryLineDescription storyLineDescription)
        {
            StoryLineDescriptionEn storyLineDescriptionTable = new StoryLineDescriptionEn
            {
                ID = storyLineDescription._storyLineDescriptionId,
                title = storyLineDescription._title,
                description = storyLineDescription._description
            };

            return storyLineDescriptionTable;
        }

        public StoryLineDescription StorylineDescriptionTableToModelEn(StoryLineDescriptionEn storyLineDescription)
        {
            StoryLineDescription storyLineDescriptionModel = new StoryLineDescription
            {
                _storyLineDescriptionId = storyLineDescription.ID,
                _title = storyLineDescription.title,
                _description = storyLineDescription.description,
                _language = User.GetInstance()._language
            };

            return storyLineDescriptionModel;
        }

        public StoryLineDescription StorylineDescriptionTableToModelFr(StoryLineDescriptionFr storyLineDescription)
        {
            StoryLineDescription storyLineDescriptionModel = new StoryLineDescription
            {
                _storyLineDescriptionId = storyLineDescription.ID,
                _title = storyLineDescription.title,
                _description = storyLineDescription.description,
                _language = User.GetInstance()._language
            };

            return storyLineDescriptionModel;
        }
    }
}