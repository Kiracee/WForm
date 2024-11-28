using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public class LockedDescendents
    {
        public bool DescendentHasLockContext { get; set; }

        public IList<string> Items { get; set; }

        public string Path { get; set; }
    }
}

