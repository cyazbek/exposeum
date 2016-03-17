using Exposeum.Tables;
using System.Collections.Generic;
using System.Linq; 
namespace Exposeum.TDGs
{
    public class StoryLineMapElementListTDG : TDG
    {
        private static StoryLineMapElementListTDG _instance;

        private StoryLineMapElementListTDG() { }

        public static StoryLineMapElementListTDG GetInstance()
        {
            if (_instance == null)
                _instance = new StoryLineMapElementListTDG();
            return _instance;
        }

        public void Add(StoryLineMapElementList item)
        {
            _db.Insert(item);
        }

        public void Update(StoryLineMapElementList item)
        {
            _db.Update(item);
        }
        public StoryLineMapElementList GetStoryLineMapElementList(int id)
        {
            return _db.Get<StoryLineMapElementList>(id);
        }

        public List<int> GetAllStorylineMapElements(int storylineId)
        {
            List<StoryLineMapElementList> listMapElementsId = new List<StoryLineMapElementList>(_db.Table<StoryLineMapElementList>());
            List<int> storyLineMapElementsId = new List<int>(); 

            foreach (var x in listMapElementsId)
            {
                if(x.storyLineId==storylineId)
                    storyLineMapElementsId.Add(x.mapElementId);
            }

            return storyLineMapElementsId;
        }
        public bool Equals(StoryLineMapElementList list1, StoryLineMapElementList list2)
        {
            if (list1.ID == list2.ID && list1.mapElementId == list2.mapElementId && list1.storyLineId == list2.storyLineId)
                return true;
            else return false;
        }

    }
}