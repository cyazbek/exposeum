using System;
using System.Collections.Generic;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Exposeum.Tables;

namespace Exposeum.Mappers
{
    public class StorylineDescriptionMapperEn
    {
        private readonly StoryLineDescriptionEnTdg _tdg;
        private static StorylineDescriptionMapperEn _instance; 

        private StorylineDescriptionMapperEn()
        {
            _tdg = StoryLineDescriptionEnTdg.GetInstance(); 
        }

        public static StorylineDescriptionMapperEn GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineDescriptionMapperEn();
            return _instance; 
        }

        public StoryLineDescriptionEn DescriptionModelToTable(StorylineDescription model)
        {
            StoryLineDescriptionEn table = new StoryLineDescriptionEn
            {
                Id = model.StoryLineDescriptionId,
                Title = model.Title, 
                Description = model.Description
            };
            
            return table;
        }

        public StorylineDescription DescriptionTableToModel(StoryLineDescriptionEn table)
        {
            StorylineDescription model = new StorylineDescription
            {
                StoryLineDescriptionId = table.Id,
                Description = table.Description,
                Title = table.Title,
                Language = Models.Language.En
            };
            return model;

        }

        public void AddDescription(StorylineDescription model)
        {
            _tdg.Add(DescriptionModelToTable(model));
        }

        public StorylineDescription GetDescription(int id)
        {
            return DescriptionTableToModel(_tdg.GetStoryLineDescriptionEn(id));
        }

        public void UpdateDescription(StorylineDescription model)
        {
            _tdg.Update(DescriptionModelToTable(model));
        }
    }
}