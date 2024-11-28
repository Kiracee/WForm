using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public enum LockOption
    {
        Read = 1,
        Write = 2,
        NonExclusiveReadOrWrite = 0x80
    }
}

