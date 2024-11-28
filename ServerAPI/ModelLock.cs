using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{
    public class ModelLock
    {
        public string Age { get; set; }

        public LockOption ModelLockOptions { get; set; }

        public LockType ModelLockType { get; set; }

        public string ModelPath { get; set; }

        public DateTime TimeStamp { get; set; }

        public string UserName { get; set; }
    }
}

