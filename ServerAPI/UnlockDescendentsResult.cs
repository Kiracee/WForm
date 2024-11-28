using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public class UnlockDescendentsResult
    {
        public IList<string> FailedItems { get; set; }

        public string Path { get; set; }
    }
}
