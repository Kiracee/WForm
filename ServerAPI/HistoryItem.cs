using System;

namespace ServerApi
{


    public class HistoryItem
    {
        public string Comment { get; set; }

        public DateTime Date { get; set; }

        public long ModelSize { get; set; }

        public long SupportSize { get; set; }

        public string User { get; set; }

        public int VersionNumber { get; set; }
    }
}

