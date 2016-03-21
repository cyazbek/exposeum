using Exposeum.TDGs;
using Exposeum.TempModels;
using System.Collections.Generic;

namespace Exposeum.Mappers
{
    public class StorylineMapper
    {
        private static StorylineMapper _instance;
        private readonly StorylineTdg _storylineTdg;
        private readonly MapElementsMapper _mapElementsMapper;
        private readonly StorylineDescriptionMapper _storylineDescriptionMapper;
        private readonly StatusMapper _statusMapper; 
        private StorylineMapper()
        {
            _storylineTdg = StorylineTdg.GetInstance();
            _mapElementsMapper = MapElementsMapper.GetInstance();
            _storylineDescriptionMapper = StorylineDescriptionMapper.GetInstance();
            _statusMapper = StatusMapper.GetInstance();
        }

        public static StorylineMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineMapper();
            return _instance;
        }

        public Tables.Storyline StorylineModelToTable(Storyline storylineModel)
        {
            Tables.Storyline storyline = new Tables.Storyline
            {
                Id = storylineModel.StorylineId,
                Audience = storylineModel.IntendedAudience,
                Duration = storylineModel.Duration,
                Image = storylineModel.ImageId,
                FloorsCovered = storylineModel.FloorsCovered,
                LastVisitedPoi = storylineModel.LastVisitedMapElement.Id,
                Status = _statusMapper.StatusModelToTable(storylineModel.Status)
            };
            return storyline; 
        }

        public List<Storyline> GetAllStorylines()
        {
            List<Tables.Storyline> tableList = _storylineTdg.GetAllStorylines();
            List<Storyline> modelList = new List<Storyline>();
            foreach(var x in tableList)
            {
                modelList.Add(StorylineTableToModel(x));
            }
            return modelList; 
        }

        public void UpdateStorylinesList(List<Storyline> list)
        {
            foreach(var x in list)
            {
                UpdateStoryline(x);
            }
        }

        public Storyline StorylineTableToModel(Tables.Storyline storylineTable)
        {

            Storyline storyline = new Storyline
            {
                StorylineId = storylineTable.Id,
                ImageId = storylineTable.Image,
                Duration = storylineTable.Duration,
                FloorsCovered = storylineTable.FloorsCovered,
                IntendedAudience = storylineTable.Audience,
                StorylineDescription = _storylineDescriptionMapper.GetDescription(storylineTable.DescriptionId),
                LastVisitedMapElement = _mapElementsMapper.GetMapElement(storylineTable.LastVisitedPoi),
                MapElements = _mapElementsMapper.GetAllMapElementsFromStoryline(storylineTable.Id),
                Status = _statusMapper.StatusTableToModel(storylineTable.Status)
            };
            return storyline; 
        }

        public Storyline GetStoryline(int id)
        {
            Tables.Storyline storylineTable=_storylineTdg.GetStoryline(id);
            Storyline storyline = StorylineTableToModel(storylineTable);
            return storyline;
        }

        public void UpdateStoryline(Storyline storyline)
        {
            Tables.Storyline storylineTable = StorylineModelToTable(storyline);
            List<MapElement> list = storyline.MapElements;
            StorylineDescription description = storyline.StorylineDescription;
            _storylineTdg.Update(storylineTable);
            _mapElementsMapper.UpdateMapElementList(list);
            _storylineDescriptionMapper.UpdateDescription(description);
        }

        public void AddStoryline(Storyline storyline)
        {
            Tables.Storyline storylineTable = StorylineModelToTable(storyline);
            List<MapElement> list = storyline.MapElements;
            StorylineDescription description = storyline.StorylineDescription;
            _storylineTdg.Add(storylineTable);
            _mapElementsMapper.AddMapElementList(list);
            _storylineDescriptionMapper.AddDescription(description);
        }

        public bool Equals(List<Storyline> list1, List<Storyline> list2)
        {
            bool result = false;
            if (list1.Count == list2.Count)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (Equals(list1[i], list2[i]))
                        result = true;
                    else
                        return false;
                }
                return result;
            }
            return false;
        }
    }
}