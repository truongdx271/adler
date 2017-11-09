using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using AntlrTestCsharp.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.parser
{
    public class FindQueryInMethod : CSharpParserBaseListener
    {
        CSharpParser parser;
        List<string> listCommand;
        public List<int> listExpressLine { get; set; }
        public List<string> queryVar { get; set; }
        public List<string> commandVar { get; set; }
        public FindQueryInMethod(CSharpParser parser,List<int> listExpressLine)
        {
            this.parser = parser;
            this.listExpressLine = listExpressLine;
            queryVar = new List<string>();
            commandVar = new List<string>();
            listCommand = new List<string>();
            //listCommand.Add("SqlCommand");
            //listCommand.Add("SqlDataAdapter");
            //string pathFile = ConfigurationManager.AppSettings["Command"];
            var resourceName = "AntlrTestCsharp.Resources.Command.txt";
            ConfigLoadItem loadCommand = new ConfigLoadItem(resourceName);
            listCommand = loadCommand.getListItem();
        }

        public override void EnterObjectCreationExpression([NotNull] CSharpParser.ObjectCreationExpressionContext context)
        {
            //context.getchild(1) la sqlcommand
            // child 2 ~> gia tri ~> bien khoi tao
            //context.GetChild(2).GetChild(1).GetText() ~> danh sach tham so
            //context.GetChild(2).GetChild(1).GetChild(0).GetText() -> string truyen vao == argumentcontext


            //Console.WriteLine(context.GetText());
            //Console.WriteLine(context.GetChild(2).GetText());
            //if (context.GetChild(2).ChildCount == 3) { 
            //Console.WriteLine(context.GetChild(2).GetChild(1).GetChild(0).GetType().ToString());
            //}

            foreach(var item in listCommand)
            {
                if (context.GetChild(1).GetText().Equals(item))
                {
                    if (context.GetChild(2).ChildCount == 3)
                    {
                        listExpressLine.Add(context.Start.Line);
                        //Console.WriteLine(context.GetChild(2).GetChild(1).GetText());
                        string query = context.GetChild(2).GetChild(1).GetChild(0).GetText();
                        if (!queryVar.Contains(query))
                        {
                            queryVar.Add(query);
                        }
                    }
                    else if (context.GetChild(2).ChildCount == 2)
                    {
                        ParserRuleContext parentOfTree = (ParserRuleContext)context.Parent;
                        while (!(parentOfTree is CSharpParser.Local_variable_declaratorContext))
                        {
                            try
                            {
                                parentOfTree = (ParserRuleContext)parentOfTree.Parent;
                                if (parentOfTree == null) { return; }
                            }
                            catch (Exception ex)
                            {
                                // Console.WriteLine(ex.Message);
                            }
                        }
                        if (parentOfTree is CSharpParser.Local_variable_declaratorContext)
                        {
                            //Console.WriteLine(parentOfTree.GetChild(0).GetText());
                            string command = parentOfTree.GetChild(0).GetText();
                            if (!commandVar.Contains(command))
                            {
                                commandVar.Add(command);
                            }
                        }
                    }
                }
            }
        }
    }
}
