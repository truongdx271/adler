using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Object
{
    public class AspCommentObj
    {
        public int startPos { get; set; }
        public int endPos { get; set; }

        public AspCommentObj(int startPos, int endPos)
        {
            this.startPos = startPos;
            this.endPos = endPos;
        }
    }
}
