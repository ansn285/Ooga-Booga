using System.Collections;

namespace Tutorials
{
    public abstract class TutorialsBase
    {
        public abstract IEnumerator Init();
        public abstract IEnumerator Execute();
        public abstract IEnumerator Exit();
    }
}