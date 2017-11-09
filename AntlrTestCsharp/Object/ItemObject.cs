using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Object
{
    public class ItemObject
    {
        public int identify { get; set; }
        public string displayTxt { get; set; }
        public string pathFile { get; set; }
        public string displayPath { get; set; }
        public int lineNumber { get; set; }
        public List<TempExpression> listExp { get; set; }
        public string result { get; set; }

        public ItemObject(int identify, string displayTxt, List<TempExpression> listExp, string pathFile, int lineNumber, string result)
        {
            this.identify = identify;
            this.displayTxt = displayTxt;
            this.listExp = listExp;
            this.pathFile = pathFile;
            this.lineNumber = lineNumber;
            this.result = result;
        }
    }
}
