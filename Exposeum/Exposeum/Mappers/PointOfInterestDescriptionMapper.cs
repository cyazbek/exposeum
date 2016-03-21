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
        private readonly PoiDescriptionEnTdg _poiDescriptionEnTdg;
        private readonly PoiDescriptionFrTdg _poiDescriptionFrTdg;
        private PoiDescriptionEn _poiDescriptionEn;
        private PoiDescriptionFr _poiDescriptionFr;

        private PointOfInterestDescriptionMapper()
        {
            _poiDescriptionEnTdg = PoiDescriptionEnTdg.GetInstance();
            _poiDescriptionFrTdg = PoiDescriptionFrTdg.GetInstance();
        }

        public static PointOfInterestDescriptionMapper GetInstance()
        {
            if (_instance == null)
                _instance = new PointOfInterestDescriptionMapper();
            return _instance;
        }

        public void AddPointOfInterestDescription(PointOfInterestDescription poiDescription)
        {
            if (poiDescription.Language == Language.Fr)
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
            if (poiDescription.Language == Language.Fr)
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

            if (User.GetInstance().Language == Language.Fr)
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

        public PointOfInterestDescription PointOfInterestDescriptionTableToModelEn(PoiDescriptionEn poiDescriptionFr)
        {
            PointOfInterestDescription pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                Id = poiDescriptionFr.Id,
                Title = poiDescriptionFr.Title,
                Summary = poiDescriptionFr.Summary,
                Description = poiDescriptionFr.Description,
                Language = User.GetInstance().Language
            };
            return pointOfInterestDescriptionModel;
        }

        public PointOfInterestDescription PointOfInterestDescriptionTableToModelFr(PoiDescriptionFr poiDescriptionFr)
        {
            PointOfInterestDescription pointOfInterestDescriptionModel = new PointOfInterestDescription
            {
                Id = poiDescriptionFr.Id,
                Title = poiDescriptionFr.Title,
                Summary = poiDescriptionFr.Summary,
                Description = poiDescriptionFr.Description,
                Language = User.GetInstance().Language
            };
            return pointOfInterestDescriptionModel;
        }

        public PoiDescriptionFr PoiDescriptionModelToTableFr(PointOfInterestDescription poiDescription)
        {
            PoiDescriptionFr poiDescriptionTable = new PoiDescriptionFr
            {
                Id = poiDescription.Id,
                Title = poiDescription.Title,
                Summary = poiDescription.Summary,
                Description = poiDescription.Description
            };

            return poiDescriptionTable;
        }

        public PoiDescriptionEn PoiDescriptionModelToTableEn(PointOfInterestDescription poiDescription)
        {
            PoiDescriptionEn poiDescriptionTable = new PoiDescriptionEn
            {
                Id = poiDescription.Id,
                Title = poiDescription.Title,
                Summary = poiDescription.Summary,
                Description = poiDescription.Description
            };

            return poiDescriptionTable;
        }
    }
}
