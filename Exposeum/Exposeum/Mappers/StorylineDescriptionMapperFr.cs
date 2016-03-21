using System;
using System.Collections.Generic;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Exposeum.Tables;

namespace Exposeum.Mappers
{
    public class StorylineDescriptionMapperFr
    {
        private readonly StoryLineDescriptionFrTdg _tdg;
        private static StorylineDescriptionMapperFr _instance; 

        private StorylineDescriptionMapperFr()
        {
            _tdg = StoryLineDescriptionFrTdg.GetInstance(); 
        }

        public static StorylineDescriptionMapperFr GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineDescriptionMapperFr();
            return _instance; 
        }

        public StoryLineDescriptionFr DescriptionModelToTable(StorylineDescription model)
        {
            StoryLineDescriptionFr table = new StoryLineDescriptionFr
            {
                Id = model.StoryLineDescriptionId,
                Title = model.Title, 
                Description = model.Description
            };
            
            return table;
        }

        public StorylineDescription DescriptionTableToModel(StoryLineDescriptionFr table)
        {
            StorylineDescription model = new StorylineDescription
            {
                StoryLineDescriptionId = table.Id,
                Description = table.Description,
                Title = table.Title,
                Language = Models.Language.Fr
            };
            return model;

        }

        public void AddDescription(StorylineDescription model)
        {
            _tdg.Add(DescriptionModelToTable(model));
        }

        public StorylineDescription GetDescription(int id)
        {
            return DescriptionTableToModel(_tdg.GetStoryLineDescriptionFr(id));
        }

        public void UpdateDescription(StorylineDescription model)
        {
            _tdg.Update(DescriptionModelToTable(model));
        }
    }
}