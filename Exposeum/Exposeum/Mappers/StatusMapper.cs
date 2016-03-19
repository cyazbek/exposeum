using Exposeum.Models;

namespace Exposeum.Mappers
{
    public class StatusMapper
    {
        private static StatusMapper _instance; 

        private StatusMapper()
        {

        }

        public static StatusMapper GetInstance()
        {
            if (_instance == null)
                _instance = new StatusMapper();
            return _instance;
        }

        public Status StatusTableToModel(int id)
        {
            switch(id)
            {
                case 0:
                    return Status.IsVisited;
                case 1:
                    return Status.InProgress;
                default:
                    return Status.IsNew;                 
            }
        }

        public int StatusModelToTable(Status status)
        {
            switch(status)
            {
                case Status.IsVisited:
                    return 0;
                case Status.InProgress:
                    return 1;
                default:
                    return 2;
            }
        }
    }
}