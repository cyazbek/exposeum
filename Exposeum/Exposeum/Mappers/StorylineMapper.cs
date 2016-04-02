using Exposeum.TDGs;
using Exposeum.Models;
using System.Collections.Generic;

namespace Exposeum.Mappers
{
    public class StorylineMapper
    {
        private static StorylineMapper _instance;
        private readonly StorylineTdg _storylineTdg;
        private readonly MapElementsMapper _mapElementsMapper;
        private readonly StoryLineDescriptionMapper _storylineDescriptionMapper;
        private readonly StatusMapper _statusMapper;
        private readonly PointOfInterestMapper _PoiMapper;
        private StorylineMapper()
        {
            _storylineTdg = StorylineTdg.GetInstance();
            _mapElementsMapper = MapElementsMapper.GetInstance();
            _storylineDescriptionMapper = StoryLineDescriptionMapper.GetInstance();
            _statusMapper = StatusMapper.GetInstance();
            _PoiMapper = PointOfInterestMapper.GetInstance();
        }

        public static StorylineMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StorylineMapper();
            return _instance;
        }

        public Tables.Storyline StorylineModelToTable(StoryLine storylineModel)
        {
            Tables.Storyline storyline = new Tables.Storyline
            {
                Id = storylineModel.StorylineId,
                Duration = storylineModel.Duration,
                ImagePath = storylineModel.ImgPath,
                FloorsCovered = storylineModel.FloorsCovered,
                LastVisitedPoi = storylineModel.LastPointOfInterestVisited.Id,
                Status = _statusMapper.StatusModelToTable(storylineModel.Status)
            };
            return storyline; 
        }

        public List<StoryLine> GetAllStorylines()
        {
            List<Tables.Storyline> tableList = _storylineTdg.GetAllStorylines();
            List<StoryLine> modelList = new List<StoryLine>();
            foreach(var x in tableList)
            {
                modelList.Add(StorylineTableToModel(x));
            }
            return modelList; 
        }

        public void UpdateStorylinesList(List<StoryLine> list)
        {
            foreach(var x in list)
            {
                UpdateStoryline(x);
            }
        }

        public StoryLine StorylineTableToModel(Tables.Storyline storylineTable)
        {

            StoryLine storyline = new StoryLine
            {
                StorylineId = storylineTable.Id,
                ImgPath = storylineTable.ImagePath,
                Duration = storylineTable.Duration,
                FloorsCovered = storylineTable.FloorsCovered,
                StorylineDescription = _storylineDescriptionMapper.GetStoryLineDescription(storylineTable.DescriptionId),
                LastPointOfInterestVisited = _PoiMapper.Get(storylineTable.LastVisitedPoi),
                MapElements = _mapElementsMapper.GetAllElementByStorylineId(storylineTable.Id),
                Status = _statusMapper.StatusTableToModel(storylineTable.Status)
            };
            return storyline; 
        }

        public StoryLine GetStoryline(int id)
        {
            Tables.Storyline storylineTable=_storylineTdg.GetStoryline(id);
            StoryLine storyline = StorylineTableToModel(storylineTable);
            return storyline;
        }

        public void UpdateStoryline(StoryLine storyline)
        {
            Tables.Storyline storylineTable = StorylineModelToTable(storyline);
            List<MapElement> list = storyline.MapElements;
            StorylineDescription description = storyline.StorylineDescription;
            _storylineTdg.Update(storylineTable);
            _mapElementsMapper.UpdateList(list);
            _storylineDescriptionMapper.UpdateStoryLineDescription(description);
        }

        public void AddStoryline(StoryLine storyline)
        {
            Tables.Storyline storylineTable = StorylineModelToTable(storyline);
            List<MapElement> list = storyline.MapElements;
            StorylineDescription description = storyline.StorylineDescription;
            _storylineTdg.Add(storylineTable);
            _mapElementsMapper.AddList(list);
            _storylineDescriptionMapper.AddStoryLineDescription(description);
        }

        public bool Equals(List<StoryLine> list1, List<StoryLine> list2)
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