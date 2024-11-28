using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{


    public class Folder
    {
        public bool HasContents { get; set; }

        public string LockContext { get; set; }

        public LockState LockState { get; set; }

        public IList<ModelLock> ModelLocksInProgress { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public long Size { get; set; }
    }
}
