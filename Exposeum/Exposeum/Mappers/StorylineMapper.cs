using Exposeum.TDGs;
using Exposeum.TempModels;
using System.Collections.Generic;

namespace Exposeum.Mappers
{
    public class StorylineMapper
    {
        private static StorylineMapper _instance;
        private readonly StorylineTDG _storylineTdg;
        private readonly MapElementsMapper _mapElementsMapper;
        private readonly StoryLineDescriptionMapper _storylineDescriptionMapper;
        private readonly StatusMapper _statusMapper; 
        private StorylineMapper()
        {
            _storylineTdg = StorylineTDG.GetInstance();
            _mapElementsMapper = MapElementsMapper.GetInstance();
            _storylineDescriptionMapper = StoryLineDescriptionMapper.GetInstance();
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
                ID = storylineModel._storylineId,
                audience = storylineModel._intendedAudience,
                duration = storylineModel._duration,
                image = storylineModel._imageId,
                floorsCovered = storylineModel._floorsCovered,
                lastVisitedPoi = storylineModel._lastVisitedMapElement._id,
                status = _statusMapper.StatusModelToTable(storylineModel._status)
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
                _storylineId = storylineTable.ID,
                _imageId = storylineTable.image,
                _duration = storylineTable.duration,
                _floorsCovered = storylineTable.floorsCovered,
                _intendedAudience = storylineTable.audience,
                _storylineDescription = _storylineDescriptionMapper.GetStoryLineDescription(storylineTable.descriptionId),
                _lastVisitedMapElement = _mapElementsMapper.GetMapElement(storylineTable.lastVisitedPoi),
                _mapElements = _mapElementsMapper.GetAllMapElementsFromStoryline(storylineTable.ID),
                _status = _statusMapper.StatusTableToModel(storylineTable.status)
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
            List<MapElement> list = storyline._mapElements;
            StorylineDescription description = storyline._storylineDescription;
            _storylineTdg.Update(storylineTable);
            _mapElementsMapper.UpdateMapElementList(list);
            _storylineDescriptionMapper.UpdateStoryLineDescription(description);
        }

        public void AddStoryline(Storyline storyline)
        {
            Tables.Storyline storylineTable = StorylineModelToTable(storyline);
            List<MapElement> list = storyline._mapElements;
            StorylineDescription description = storyline._storylineDescription;
            _storylineTdg.Add(storylineTable);
            _mapElementsMapper.AddMapElementList(list);
            _storylineDescriptionMapper.AddStoryLineDescription(description);
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
            else return false;
        }
    }
}