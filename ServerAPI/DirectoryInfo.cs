using System;
using System.Collections.Generic;

namespace ServerApi
{
    public class DirectoryInfo
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public bool Exists { get; set; }

        public int FolderCount { get; set; }

        public bool IsFolder { get; set; }

        public string LastModifiedBy { get; set; }

        public string LockContext { get; set; }

        public LockState LockState { get; set; }

        public int ModelCount { get; set; }

        public IList<ModelLock> ModelLocksInProgress { get; set; }

        public long ModelSize { get; set; }

        public string Path { get; set; }

        public long Size { get; set; }
    }
}
