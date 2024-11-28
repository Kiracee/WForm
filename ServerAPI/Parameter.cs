using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerApi
{

    public class Parameter
    {
        public string DisplayName { get; set; }

        public string Group { get; set; }

        public string Name { get; set; }

        public string Text { get; set; }

        public ParamType Type { get; set; }

        public ParamValueType? ValueType { get; set; }
    }
}

