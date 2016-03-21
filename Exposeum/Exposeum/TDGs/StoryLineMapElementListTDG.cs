using Exposeum.Tables;
using System.Collections.Generic;

namespace Exposeum.TDGs
{
    public class StoryLineMapElementListTdg : Tdg
    {
        private static StoryLineMapElementListTdg _instance;

        private StoryLineMapElementListTdg() { }

        public static StoryLineMapElementListTdg GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineMapElementListTdg();
            return _instance;
        }

        public void Add(StoryLineMapElementList item)
        {
            Db.InsertOrReplace(item);
        }

        public void Update(StoryLineMapElementList item)
        {
            Db.Update(item);
        }
        public StoryLineMapElementList GetStoryLineMapElementList(int id)
        {
            return Db.Get<StoryLineMapElementList>(id);
        }

        public List<int> GetAllStorylineMapElements(int storylineId)
        {
            List<StoryLineMapElementList> listMapElementsId = new List<StoryLineMapElementList>(Db.Table<StoryLineMapElementList>());
            List<int> mapElementsId = new List<int>(); 

            foreach (var x in listMapElementsId)
            {
                if(x.StoryLineId==storylineId)
                    mapElementsId.Add(x.MapElementId);
            }

            return mapElementsId;
        }
        public bool Equals(StoryLineMapElementList list1, StoryLineMapElementList list2)
        {
            if (list1.Id == list2.Id && list1.MapElementId == list2.MapElementId && list1.StoryLineId == list2.StoryLineId)
                return true;
            return false;
        }
    }
}