using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public class ServerProperties
    {
        public int MaximumFolderPathLength { get; set; }

        public int MaximumModelNameLength { get; set; }

        public IList<Role> ServerRoles { get; set; }

        public IList<string> Servers { get; set; }
    }
}

