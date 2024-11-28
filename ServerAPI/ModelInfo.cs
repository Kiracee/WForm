using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public class ModelInfo
    {
        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

        public string LastModifiedBy { get; set; }

        public Guid ModelGUID { get; set; }

        public long ModelSize { get; set; }

        public string Path { get; set; }

        public long SupportSize { get; set; }
    }
}

