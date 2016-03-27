using System.Collections.Generic;
using Exposeum.Tables;
using Java.Security;

namespace Exposeum.TDGs
{
    public class ExhibitionContentListTDG:Tdg
    {
        private static ExhibitionContentListTDG _instance;


        private ExhibitionContentListTDG()
        {
        }

        public static ExhibitionContentListTDG GetInstance()
        {
            if(_instance==null)
                _instance = new ExhibitionContentListTDG();
            return _instance; 
        }

        public void Add(ExhibitionContentList content)
        {
            Db.InsertOrReplace(content);
        }

        public void AddList(List<ExhibitionContentList> exhibitionList)
        {
            foreach (var x in exhibitionList)
            {
                Db.InsertOrReplace(x);
            }
        }

        public void AddList(List<int> exhibitionList, int poiId)
        {
            foreach (var x in exhibitionList)
            {
                
                Db.InsertOrReplace(new ExhibitionContentList()
                {
                    PoiId = poiId,
                    ExhibitionContentId = x
                });
            }
        }

        public ExhibitionContentList Get(int id)
        {
            return Db.Get<ExhibitionContentList>(id);
        }

        public List<int> GetList(int id)
        {
            List<int> list = new List<int>(); 
            List<ExhibitionContentList> exhibitionList = new List<ExhibitionContentList>(Db.Table<ExhibitionContentList>());

            foreach (var x in exhibitionList)
            {
                if(x.PoiId==id)
                    list.Add(x.ExhibitionContentId);
            }
            return list;
        }

        public void Update(ExhibitionContentList content)
        {
            Db.Update(content);
        }

        public void UpdateList(List<ExhibitionContentList> list)
        {
            foreach (var x in list)
            {
                Db.Update(x);
            }
        }
    }
}