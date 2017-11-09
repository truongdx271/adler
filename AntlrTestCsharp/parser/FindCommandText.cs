using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;

namespace AntlrTestCsharp.parser
{
    public class FindCommandText : CSharpParserBaseListener
    {
        CSharpParser parser;
        public string commandVar { get; set; }
        List<int> listLine;
        public FindCommandText(CSharpParser parser, string commandVar)
        {
            this.parser = parser;
            this.commandVar = commandVar;
            listLine = new List<int>();
        }

        public List<int> getListLine()
        {
            return this.listLine;
        }

        public override void EnterLocal_variable_declaration([NotNull] CSharpParser.Local_variable_declarationContext context)
        {
            //sua lai
            if (context.GetChild(1).GetChild(0).GetText().Equals(commandVar))
            {
                if (!listLine.Contains(context.Start.Line))
                {
                    listLine.Add(context.Start.Line);
                }
            }
        }

        public override void EnterAssignment([NotNull] CSharpParser.AssignmentContext context)
        {
            if (context.GetChild(0).GetText().Equals(commandVar))
            {
                if (!listLine.Contains(context.Start.Line))
                {
                    listLine.Add(context.Start.Line);
                }
            }
            //Console.WriteLine(context.GetChild(0).GetText());
        }
    }
}
