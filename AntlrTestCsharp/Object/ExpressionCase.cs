using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Object
{
    public class ExpressionCase
    {
        public string signal { get; set; }
        public string partition { get; set; }

        public ExpressionCase(string lineDescription)
        {
            if (lineDescription == null && lineDescription.Contains(":"))
            {
                return;
            }

            string[] partDescription = lineDescription.Split(':');

            if (partDescription.Length < 2)
            {
                return;
            }
            this.signal = partDescription[0];
            this.partition = partDescription[1];
        }
    }
}
