using System.IO.MemoryMappedFiles;
using Exposeum.Models;

namespace Exposeum.Observers
{
    public abstract class LanguageObserver
    {
        protected User User { get; set; }
        protected Map map;

        public abstract void Update();

    }
}