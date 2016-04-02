using System;
using Android.Widget;
using Exposeum.Models;
using Exposeum.Mappers;

namespace Exposeum.Observers
{
    public class PointOfInterestDescriptionObserver:LanguageObserver
    {
        private readonly MapElementsMapper mapElementMapper; 


        public PointOfInterestDescriptionObserver()
        {
            User = User.GetInstance();
            User.Register(this);
            mapElementMapper = MapElementsMapper.GetInstance();
            map = Map.GetInstance();
        }

        public override void Update()
        {
            mapElementMapper.UpdateList(Map.GetInstance().MapElements);
            map.MapElements = mapElementMapper.GetAllMapElements();
        }
    }
}