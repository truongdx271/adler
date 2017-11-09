using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using AntlrTestCsharp.Object;
using System.Configuration;
using AntlrTestCsharp.Config;

namespace AntlrTestCsharp.parser
{
    public class ExtractClassParser : CSharpParserBaseListener
    {
        CSharpParser parser;
        //string varName = "sqx";
        List<string> signList;
        List<string> xmlList;
        List<string> badcryptList;
        public List<MethodContext> listMethodContext { get; set; }
        public List<ParserRuleContext> listXMLContext { get; set; }
        public List<ParserRuleContext> listBadCryptContext { get; set; }
        public ExtractClassParser(CSharpParser parser)
        {
            this.parser = parser;
            //DbQuery
            signList = new List<string>();
            //signList.Add("ExecuteNonQuery");
            //signList.Add("ExecuteNonQueryAsync");
            //signList.Add("ExecuteReader");
            //signList.Add("ExecuteReaderAsync");
            //signList.Add("ExecuteScalar");
            //signList.Add("ExecuteScalarAsync");
            //signList.Add("ExecuteXmlReader");
            //signList.Add("ExecuteXmlReaderAsync");
            //signList.Add("executequery");

            //string pathFile = ConfigurationManager.AppSettings["ExecuteSQL"];
            var resourceName = "AntlrTestCsharp.Resources.ExecuteSQL.txt";
            ConfigLoadItem loadSQLCase = new ConfigLoadItem(resourceName);
            signList = loadSQLCase.getListItem();
            //XML
            xmlList = new List<string>();
            //xmlList.Add("XmlReaderSettings");
            //xmlList.Add("XmlTextReader");
            //xmlList.Add("XmlDocument");
            //pathFile = ConfigurationManager.AppSettings["XML"];
            resourceName = "AntlrTestCsharp.Resources.XML.txt";
            ConfigLoadItem loadXmlCase = new ConfigLoadItem(resourceName);
            xmlList = loadXmlCase.getListItem();

            badcryptList = new List<string>();
            resourceName = "AntlrTestCsharp.Resources.BadCrypto.txt";
            ConfigLoadItem loadBadCryptCase = new ConfigLoadItem(resourceName);
            badcryptList = loadBadCryptCase.getListItem();


        }

        public List<MethodContext> getListMethod()
        {
            return this.listMethodContext;
        }

        public override void EnterClass_member_declaration([NotNull] CSharpParser.Class_member_declarationContext context)
        {
            //base.EnterClass_member_declaration(context);
        }

        public override void EnterMethod_declaration([NotNull] CSharpParser.Method_declarationContext context)
        {
            // parser.getto
            //Console.WriteLine(context.GetText());

        }

        public override void EnterLocal_variable_declaration([NotNull] CSharpParser.Local_variable_declarationContext context)
        {
            //for (int i = 0; i < context.ChildCount; i++)
            //{
            //    Console.WriteLine("Child " + i + " : " + context.GetChild(i).GetText());
            //    for (int j = 0; j < context.GetChild(i).ChildCount; j++)
            //    {
            //        Console.WriteLine("Child " + i + "." + j + " : " + context.GetChild(i).GetChild(j).GetText());
            //    }
            //    Console.WriteLine("====================");
            //}

            /*
            string type = "";
            string value = "";
            string varId = "";

            for (int i = 0; i < context.ChildCount; i++)
            {
                IParseTree childTree = context.GetChild(i);
                if (childTree is CSharpParser.TypeContext)
                {
                    type = childTree.GetText();
                }
                else if (childTree is CSharpParser.Local_variable_declaratorContext)
                {

                    varId = childTree.GetChild(0).GetText();
                    if (!varId.Equals(varName))
                    {
                        return;
                    }
                    //Console.WriteLine(childTree.Parent.Parent.Parent.Parent.Parent.Parent.GetText());
                    if (childTree.ChildCount >= 3)
                    {
                        value = childTree.GetChild(2).GetText();
                        int child = childTree.GetChild(2).ChildCount;
                        Console.WriteLine(child);
                        Console.WriteLine(childTree.GetChild(2).GetType().ToString());
                        Console.WriteLine("type: " + type + " | varId: " + varId + " | value: " + value);
                    }
                }

            }
            */


            // Console.WriteLine(context.GetText());
        }

        public override void EnterLiteralExpression([NotNull] CSharpParser.LiteralExpressionContext context)
        {
            //if (context.GetText().Equals("\"abc\""))
            //{
            //    //ParserRuleContext parent = (ParserRuleContext)context.Parent;
            //    //Console.WriteLine(parent.GetType().ToString());
            //    //Console.WriteLine(parent.GetText());
            //    Console.WriteLine(context.GetText());
            //}
        }

        //public override void EnterConstant_declaration([NotNull] CSharpParser.Constant_declarationContext context)
        //{
        //    Console.WriteLine(context.GetText());
        //}
        //public override void EnterVariable_initializer([NotNull] CSharpParser.Variable_initializerContext context)
        //{
        //    Console.WriteLine(context.GetText());
        //}

        //public override void EnterLocal_variable_initializer([NotNull] CSharpParser.Local_variable_initializerContext context)
        //{
        //    Console.WriteLine(context.GetText());
        //}

        public override void EnterVariable_declarator([NotNull] CSharpParser.Variable_declaratorContext context)
        {
            //Console.WriteLine(context.Parent.GetText());
            //Console.WriteLine(context.GetChild(2).GetType().ToString());
            //Console.WriteLine(context.GetChild(2).ChildCount);
        }
        public override void EnterField_declaration([NotNull] CSharpParser.Field_declarationContext context)
        {
            //GLOBAL VAR
            /*
            Console.WriteLine(context.Parent.GetText());
            Console.WriteLine(context.Parent.GetType().ToString());
            Console.WriteLine(context.Parent.ChildCount);
            Console.WriteLine(context.Parent.GetChild(0).GetText());
            Console.WriteLine(context.Parent.GetChild(1).GetText());
            */
        }

        public override void EnterExpression([NotNull] CSharpParser.ExpressionContext context)
        {
            foreach (var item in signList)
            {
                if (context.GetText().Contains(item))
                {
                    //Console.WriteLine(context.GetText());
                    ParserRuleContext parentOfTree = (ParserRuleContext)context.Parent;
                    while (!(parentOfTree is CSharpParser.Method_declarationContext))
                    {
                        try
                        {
                            parentOfTree = (ParserRuleContext)parentOfTree.Parent;
                            if (parentOfTree == null) { return; }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (parentOfTree is CSharpParser.Method_declarationContext)
                    {
                        if (listMethodContext != null)
                        {
                            if (!listMethodContext.Any(x => x.startLine == parentOfTree.Start.Line))
                            {
                                List<int> tmpLine = new List<int>();
                                tmpLine.Add(context.Start.Line);
                                MethodContext tmpMethod = new MethodContext(parentOfTree, tmpLine, parentOfTree.Start.Line);
                                listMethodContext.Add(tmpMethod);
                            }
                            else
                            {
                                //update them vi tri 
                                //listMethodContext.Where(x =>x.startLine == parentOfTree.Start.Line)
                            }
                        }
                        else
                        {
                            this.listMethodContext = new List<MethodContext>();
                            List<int> tmpLine = new List<int>();
                            tmpLine.Add(context.Start.Line);
                            MethodContext tmpMethod = new MethodContext(parentOfTree, tmpLine, parentOfTree.Start.Line);
                            this.listMethodContext.Add(tmpMethod);
                        }
                    }
                }
            }
            foreach (var item in xmlList)
            {
                if (context.GetText().Contains(item))
                {
                    //Console.WriteLine(context.GetText());
                    ParserRuleContext parentOfTree = (ParserRuleContext)context.Parent;
                    while (!(parentOfTree is CSharpParser.Method_declarationContext))
                    {
                        try
                        {
                            parentOfTree = (ParserRuleContext)parentOfTree.Parent;
                            if (parentOfTree == null) { return; }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (parentOfTree is CSharpParser.Method_declarationContext)
                    {
                        if (listXMLContext != null)
                        {
                            if (!listXMLContext.Any(x => x.Start.Line == parentOfTree.Start.Line))
                            {
                                this.listXMLContext.Add(parentOfTree);
                            }
                        }
                        else
                        {
                            this.listXMLContext = new List<ParserRuleContext>();
                            this.listXMLContext.Add(parentOfTree);
                        }
                    }
                }
            
            }
            foreach (var item in badcryptList)
            {
                if (context.GetText().Contains(item))
                {
                    //Console.WriteLine(context.GetText());
                    ParserRuleContext parentOfTree = (ParserRuleContext)context.Parent;
                    while (!(parentOfTree is CSharpParser.Method_declarationContext))
                    {
                        try
                        {
                            parentOfTree = (ParserRuleContext)parentOfTree.Parent;
                            if (parentOfTree == null) { return; }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                    }
                    if (parentOfTree is CSharpParser.Method_declarationContext)
                    {
                        if (listBadCryptContext != null)
                        {
                            if (!listBadCryptContext.Any(x => x.Start.Line == parentOfTree.Start.Line))
                            {
                                this.listBadCryptContext.Add(parentOfTree);
                            }
                        }
                        else
                        {
                            this.listBadCryptContext = new List<ParserRuleContext>();
                            this.listBadCryptContext.Add(parentOfTree);
                        }
                    }
                }

            }
        }

    }
}
