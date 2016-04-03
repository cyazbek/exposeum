using System.IO.MemoryMappedFiles;
using Exposeum.Models;

namespace Exposeum.Observers
{
    public abstract class LanguageObserver
    {
        protected LanguageSubject Subject; 
        public abstract void Update();

    }
}