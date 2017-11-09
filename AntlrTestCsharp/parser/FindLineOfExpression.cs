using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime;

namespace AntlrTestCsharp.parser
{
    public class FindLineOfExpression : CSharpParserBaseListener
    {
        CSharpParser parser;
        public List<string> commandVar { get; set; }
        public List<string> queryVar { get; set; }
        public List<int> listExpressLine { get; set; }
        public ParserRuleContext treeContext;
        public FindLineOfExpression(CSharpParser parser, ParserRuleContext treeContext, List<int> listExpressLine, List<string> commandVar, List<string> queryVar)
        {
            this.parser = parser;
            if (commandVar != null)
            {
                this.commandVar = commandVar;
            }
            if (queryVar != null)
            {
                this.queryVar = queryVar;
            }
            this.listExpressLine = listExpressLine;
            this.treeContext = treeContext;
        }

        public override void EnterLocal_variable_declaration([NotNull] CSharpParser.Local_variable_declarationContext context)
        {
            if (queryVar == null)
            {
                return;
            }
            foreach (var item in queryVar)
            {
                if (context.GetChild(1).GetChild(0).GetText().Equals(item))
                {
                    if (!listExpressLine.Contains(context.Start.Line))
                    {
                        listExpressLine.Add(context.Start.Line);
                    }
                }
            }
        }
        public override void EnterAssignment([NotNull] CSharpParser.AssignmentContext context)
        {
            if (commandVar == null)
            {
                return;
            }
            foreach (var command in commandVar)
            {
                string partern = command + ".CommandText";
                if (context.GetText().Contains(partern))
                {
                    if (!listExpressLine.Contains(context.Start.Line))
                    {
                        listExpressLine.Add(context.Start.Line);
                        if (!context.GetChild(2).GetText().StartsWith("\""))
                        {
                            ParseTreeWalker methodWalker = new ParseTreeWalker();
                            FindCommandText commandListener = new FindCommandText(parser, context.GetChild(2).GetText());
                            methodWalker.Walk(commandListener, treeContext);
                            if (commandListener.getListLine().Count > 0)
                            {
                                foreach (var item in commandListener.getListLine())
                                {
                                    if (!listExpressLine.Contains(item))
                                    {
                                        listExpressLine.Add(item);
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }
    }
}
