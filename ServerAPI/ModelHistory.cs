using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public class ModelHistory
    {
        public IList<HistoryItem> Items { get; set; }

        public string Path { get; set; }
    }
}

