using Exposeum.Tables;
using System.Collections.Generic;
using System.Linq; 
namespace Exposeum.TDGs
{
    public class StoryLineMapElementListTDG:TDG
    {
        private static StoryLineMapElementListTDG _instance;

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
        public List<int> GetStorylineMapElements(int storylineId)
        {
            List<int> listMapElementsId = new List<int>();
            var query = _db.Table<StoryLineMapElementList>().Where(v => v.storyLineId.Equals(storylineId));
            foreach (var StoryLineMapElementList in query)
            {
                listMapElementsId.Add(StoryLineMapElementList.mapElementId);
            }
            return listMapElementsId;
        }

    }
}