using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using StoryLineDescription = Exposeum.TempModels.StorylineDescription;
using User = Exposeum.Models.User;

namespace Exposeum.Mappers
{
    public class StoryLineDescriptionMapper
    {
        private static StoryLineDescriptionMapper _instance;
        private readonly StoryLineDescriptionEnTdg _storyLineDescriptionEnTdg;
        private readonly StoryLineDescriptionFrTdg _storyLineDescriptionFrTdg;
        private StoryLineDescriptionEn _storyLineDescriptionEn;
        private StoryLineDescriptionFr _storyLineDescriptionFr;

        private StoryLineDescriptionMapper()
        {
            _storyLineDescriptionEnTdg = StoryLineDescriptionEnTdg.GetInstance();
            _storyLineDescriptionFrTdg = StoryLineDescriptionFrTdg.GetInstance();
        }

        public static StoryLineDescriptionMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineDescriptionMapper();
            return _instance;
        }

        public void AddStoryLineDescription(StoryLineDescription storyLineDescription)
        {
            if (storyLineDescription.Language == Language.Fr)
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
            if (storyLineDescription.Language == Language.Fr)
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

            if (User.GetInstance().Language == Language.Fr)
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
                Id = storyLineDescription.StoryLineDescriptionId,
                Title = storyLineDescription.Title,
                Description = storyLineDescription.Description
            };

            return storyLineDescriptionTable;
        }

        public StoryLineDescriptionEn StoryLineDescriptionModelToTableEn(StoryLineDescription storyLineDescription)
        {
            StoryLineDescriptionEn storyLineDescriptionTable = new StoryLineDescriptionEn
            {
                Id = storyLineDescription.StoryLineDescriptionId,
                Title = storyLineDescription.Title,
                Description = storyLineDescription.Description
            };

            return storyLineDescriptionTable;
        }

        public StoryLineDescription StorylineDescriptionTableToModelEn(StoryLineDescriptionEn storyLineDescription)
        {
            StoryLineDescription storyLineDescriptionModel = new StoryLineDescription
            {
                StoryLineDescriptionId = storyLineDescription.Id,
                Title = storyLineDescription.Title,
                Description = storyLineDescription.Description,
                Language = Language.En
            };

            return storyLineDescriptionModel;
        }

        public StoryLineDescription StorylineDescriptionTableToModelFr(StoryLineDescriptionFr storyLineDescription)
        {
            StoryLineDescription storyLineDescriptionModel = new StoryLineDescription
            {
                StoryLineDescriptionId = storyLineDescription.Id,
                Title = storyLineDescription.Title,
                Description = storyLineDescription.Description,
                Language = Language.Fr
            };

            return storyLineDescriptionModel;
        }
    }
}