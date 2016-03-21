using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.Mappers; 
using StoryLineDescription = Exposeum.TempModels.StorylineDescription;

namespace Exposeum.Mappers
{
    public class StorylineDescriptionMapper
    {
        private static StorylineDescriptionMapper _instance;
        private readonly StorylineDescriptionMapperEn _englishInstance;
        private readonly StorylineDescriptionMapperFr _frenchInstance; 

        private StorylineDescriptionMapper()
        {
            _englishInstance = StorylineDescriptionMapperEn.GetInstance();
            _frenchInstance = StorylineDescriptionMapperFr.GetInstance(); 
        }

        public static StorylineDescriptionMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineDescriptionMapper();
            return _instance; 
        }

        public void AddDescription(StoryLineDescription model)
        {
            if (model.Language.Equals(Models.Language.Fr))
                _frenchInstance.AddDescription(model);
            else _englishInstance.AddDescription(model);
        }

        public void UpdateDescription(StoryLineDescription model)
        {
            if (model.Language.Equals(Models.Language.Fr))
                _frenchInstance.UpdateDescription(model);
            else
                _englishInstance.UpdateDescription(model);
        }

        public StoryLineDescription GetDescription(int id)
        {
            Models.Language language = Models.User.GetInstance().Language;
            if (language.Equals(Models.Language.Fr))
                return _frenchInstance.GetDescription(id);
            else
                return _englishInstance.GetDescription(id);
        }



    }
}