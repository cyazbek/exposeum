using System;
using Exposeum.Models;
using Exposeum.Tables;
using Exposeum.TDGs;
using Exposeum.TempModels;
using PointOfInterestDescription = Exposeum.TempModels.PointOfInterestDescription;

namespace Exposeum.Mappers
{
    public class PointOfInterestDescriptionMapper
    {
        private static PointOfInterestDescriptionMapper _instance;
        private readonly PoiDescriptionEnTDG _poiDescriptionEnTdg;
        private readonly PoiDescriptionFrTDG _poiDescriptionFrTdg;
        private PoiDescriptionEn _poiDescriptionEn;
        private PoiDescriptionFr _poiDescriptionFr;

        private PointOfInterestDescriptionMapper()
        {
            _poiDescriptionEnTdg = PoiDescriptionEnTDG.GetInstance();
            _poiDescriptionFrTdg = PoiDescriptionFrTDG.GetInstance();
        }

        public static PointOfInterestDescriptionMapper GetInstance()
        {
            if (_instance == null)
                _instance = new PointOfInterestDescriptionMapper();
            return _instance;
        }

        public void AddPointOfInterestDescription(PointOfInterestDescription poiDescription)
        {
            if (poiDescription._language == Language.Fr)
            {
                _poiDescriptionFr = PoiDescriptionModelToTableFr(poiDescription);
                _poiDescriptionFrTdg.Add(_poiDescriptionFr);
            }
            else
            {
                _poiDescriptionEn = PoiDescriptionModelToTableEn(poiDescription);
                _poiDescriptionEnTdg.Add(_poiDescriptionEn);
            }
        }

        public void UpdatePointOfInterestDescription(PointOfInterestDescription poiDescription)
        {
            if (poiDescription._language == Language.Fr)
            {
                _poiDescriptionFr = PoiDescriptionModelToTableFr(poiDescription);
                _poiDescriptionFrTdg.Update(_poiDescriptionFr);
            }
            else
            {
                _poiDescriptionEn = PoiDescriptionModelToTableEn(poiDescription);
                _poiDescriptionEnTdg.Update(_poiDescriptionEn);
            }
        }

        public PointOfInterestDescription GetPointOfInterestDescription(int poiDescriptionId)
        {
            PointOfInterestDescription pointOfInterestDescriptionModel;

            if (User.GetInstance()._language == Language.Fr)
            {
                _poiDescriptionFr = _poiDescriptionFrTdg.GetPoiDescriptionFr(poiDescriptionId);
                pointOfInterestDescriptionModel = PointOfInterestDescriptionTableToModelFr(_poiDescriptionFr);
            }
            else
            {
                _poiDescriptionEn = _poiDescriptionEnTdg.GetPoiDescriptionEn(poiDescriptionId);
                pointOfInterestDescriptionModel = PointOfInterestDescriptionTableToModelEn(_poiDescriptionEn);
            }

            return pointOfInterestDescriptionModel;
        }

        public PointOfInterestDescription PointOfInterestDescriptionTableToModelEn(PoiDescriptionEn poiDescriptionEn)
        {
            PointOfInterestDescription pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = poiDescriptionEn.ID,
                _title = poiDescriptionEn.title,
                _summary = poiDescriptionEn.summary,
                _description = poiDescriptionEn.description,
                _language = User.GetInstance()._language
            };
            return pointOfInterestDescriptionModel;
        }

        public PointOfInterestDescription PointOfInterestDescriptionTableToModelFr(PoiDescriptionFr poiDescriptionFr)
        {
            PointOfInterestDescription pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                _id = poiDescriptionFr.ID,
                _title = poiDescriptionFr.title,
                _summary = poiDescriptionFr.summary,
                _description = poiDescriptionFr.description,
                _language = User.GetInstance()._language
            };
            return pointOfInterestDescriptionModel;
        }

        public PoiDescriptionFr PoiDescriptionModelToTableFr(PointOfInterestDescription poiDescription)
        {
            PoiDescriptionFr poiDescriptionTable = new PoiDescriptionFr
            {
                ID = poiDescription._id,
                title = poiDescription._title,
                summary = poiDescription._summary,
                description = poiDescription._description
            };

            return poiDescriptionTable;
        }

        public PoiDescriptionEn PoiDescriptionModelToTableEn(PointOfInterestDescription poiDescription)
        {
            PoiDescriptionEn poiDescriptionTable = new PoiDescriptionEn
            {
                ID = poiDescription._id,
                title = poiDescription._title,
                summary = poiDescription._summary,
                description = poiDescription._description
            };

            return poiDescriptionTable;
        }
    }
}
