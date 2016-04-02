using Exposeum.Mappers;
using Exposeum.Models;

namespace Exposeum.Observers
{
    public class StorylineDescriptionObserver:LanguageObserver
    {
        private readonly StorylineMapper _mapper; 
        public StorylineDescriptionObserver()
        {
            User = User.GetInstance();
            User.Register(this);
            map = Map.GetInstance();
            _mapper = StorylineMapper.GetInstance();
        }

        public override void Update()
        {
            _mapper.UpdateStorylinesList(map.Storylines);
            map.Storylines = _mapper.GetAllStorylines();
        }
    }
}