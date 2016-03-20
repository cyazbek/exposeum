using Exposeum.Tables;
using Exposeum.TDGs;
using StoryLineDescription = Exposeum.TempModels.StorylineDescription;

namespace Exposeum.Mappers
{
    public class StorylineDescriptionMapper
    {
        private static StorylineDescriptionMapper _instance;
        private StoryLineDescriptionEnTDG _storyLineDescriptionEnTdg = StoryLineDescriptionEnTDG.GetInstance();
        private StoryLineDescriptionFrTDG _storyLineDescriptionFrTdg = StoryLineDescriptionFrTDG.GetInstance();
        private Models.User _user;

        private StorylineDescriptionMapper()
        {
            _user = Models.User.GetInstance();
        }

        public static StorylineDescriptionMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineDescriptionMapper();
            return _instance;
        }

        //Method to convert from english table to model
        public StoryLineDescriptionEn DescriptionModelToTableEn(StoryLineDescription model)
        {
            StoryLineDescriptionEn table = new StoryLineDescriptionEn
            {
                ID = model._storyLineDescriptionId,
                title = model._title,
                description = model._description
            };
            return table;
        }

        public StoryLineDescriptionFr DescriptionModelToTableFr(StoryLineDescription model)
        {
            StoryLineDescriptionFr table = new StoryLineDescriptionFr
            {
                ID = model._storyLineDescriptionId,
                title = model._title,
                description = model._description
            };
            return table;
        }

        public StoryLineDescription DescriptionTableToModelFr(StoryLineDescriptionFr table)
        {
            StoryLineDescription model = new StoryLineDescription
            {
                _title = table.title,
                _storyLineDescriptionId = table.ID,
                _description = table.description,
                _language = Models.Language.Fr
            };
            return model;
        }

        public StoryLineDescription DescriptionTableToModelEn(StoryLineDescriptionEn table)
        {
            StoryLineDescription model = new StoryLineDescription
            {
                _title = table.title,
                _storyLineDescriptionId = table.ID,
                _description = table.description,
                _language = Models.Language.En
            };
            return model;
        }

        public void AddDescription(StoryLineDescription model)
        {
            if (model._language.Equals(Models.Language.Fr))
            {
                _storyLineDescriptionFrTdg.Add(DescriptionModelToTableFr(model));
            }
            else
                _storyLineDescriptionEnTdg.Add(DescriptionModelToTableEn(model));
        }

        public StoryLineDescription GetDescription(int id)
        {
            if (_user._language.Equals(Models.Language.Fr))
            {
                return DescriptionTableToModelFr(_storyLineDescriptionFrTdg.GetStoryLineDescriptionFr(id));
            }
            else
                return DescriptionTableToModelEn(_storyLineDescriptionEnTdg.GetStoryLineDescriptionEn(id));
        }

        public void UpdateDescription(StoryLineDescription model)
        {
            if (model._language.Equals(Models.Language.Fr))
            {
                _storyLineDescriptionFrTdg.Update(DescriptionModelToTableFr(model));
            }
            else
                _storyLineDescriptionEnTdg.Update(DescriptionModelToTableEn(model));
        }



    }
}