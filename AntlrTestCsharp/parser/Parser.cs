using AntlrTestCsharp.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.parser
{
    public interface Parser
    {
        ClassObject parse(string code);
    }
}
