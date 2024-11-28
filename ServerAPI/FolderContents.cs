using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{


    public class FolderContents
    {
        public long DriveFreeSpace { get; set; }

        public long DriveSpace { get; set; }

        public IList<File> Files { get; set; }

        public IList<Folder> Folders { get; set; }

        public string LockContext { get; set; }

        public LockState LockState { get; set; }

        public IList<ModelLock> ModelLocksInProgress { get; set; }

        public IList<Model> Models { get; set; }

        public string Path { get; set; }
    }
}

