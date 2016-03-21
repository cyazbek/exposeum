using System;
using System.Collections.Generic;
using Exposeum.TDGs;
using Exposeum.TempModels;
using Exposeum.Tables;

namespace Exposeum.Mappers
{
    public class StorylineDescriptionMapperFr
    {
        private readonly StoryLineDescriptionFrTDG _tdg;
        private static StorylineDescriptionMapperFr _instance; 

        private StorylineDescriptionMapperFr()
        {
            _tdg = StoryLineDescriptionFrTDG.GetInstance(); 
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
                ID = model._storyLineDescriptionId,
                title = model._title, 
                description = model._description
            };
            
            return table;
        }

        public StorylineDescription DescriptionTableToModel(StoryLineDescriptionFr table)
        {
            StorylineDescription model = new StorylineDescription
            {
                _storyLineDescriptionId = table.ID,
                _description = table.description,
                _title = table.title,
                _language = Models.Language.Fr
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