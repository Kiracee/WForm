
    using System.Collections.Generic;

namespace ServerApi
{

    public class Model
    {
        public string LockContext { get; set; }

        public LockState LockState { get; set; }

        public IList<ModelLock> ModelLocksInProgress { get; set; }

        public long ModelSize { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public int ProductVersion { get; set; }

        public int SupportSize { get; set; }
    }
}

