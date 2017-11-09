using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace AntlrTestCsharp.parser
{
    public class FindUsingLibOfClass : CSharpParserBaseListener
    {
        CSharpParser parser;
        private List<string> listUsingLib = new List<string>();

        public FindUsingLibOfClass(CSharpParser parser)
        {
            this.parser = parser;
        }

        public override void EnterUsing_directives([NotNull] CSharpParser.Using_directivesContext context)
        {
            //listUsingLib.Add(context.GetChild(1).GetText());
            int childcount = context.ChildCount;
            for(int i = 0; i < childcount; i++)
            {
                listUsingLib.Add(context.GetChild(i).GetChild(1).GetText());
            }
        }

        public List<string> getListUsing()
        {
            return listUsingLib;
        }
    }
}
