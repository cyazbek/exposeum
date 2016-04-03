
using System.Collections.Generic;

namespace Exposeum.Observers
{
    public interface LanguageSubject
    {
        void Register(LanguageObserver o);
        void NotifyAll(); 
    }
}