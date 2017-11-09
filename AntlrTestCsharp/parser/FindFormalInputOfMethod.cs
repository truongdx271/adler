using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;

namespace AntlrTestCsharp.parser
{
    public class FindFormalInputOfMethod : CSharpParserBaseListener
    {
        CSharpParser parser;

        public FindFormalInputOfMethod(CSharpParser parser)
        {
            this.parser = parser;
        }

        public override void EnterFormal_parameter_list([NotNull] CSharpParser.Formal_parameter_listContext context)
        {
            ParserRuleContext ancesstor = (ParserRuleContext)context.Parent;
            //for (int i = 0; i < context.ChildCount; i++)
            //{
            //    Console.WriteLine("Child "+i+": " + context.GetChild(i).GetText());
            //    for (int j = 0; j < context.GetChild(i).ChildCount; j++)
            //    {
            //        Console.WriteLine("Child " + i + "." + j + ": " + context.GetChild(i).GetChild(j).GetText());
            //    }
            //}
            //context = context.GetChild(0);

            int childLength = context.GetChild(0).ChildCount;
            string type = "";
            string varId = "";
            for (int i = 0; i < childLength; i = i + 2)
            {
                IParseTree childTree = context.GetChild(0).GetChild(i).GetChild(0);
                //Console.Out.WriteLine(childTree.GetText());
                //Console.WriteLine(childTree.ChildCount);
                //Console.WriteLine(childTree.GetType().ToString());
                if (childTree is CSharpParser.Arg_declarationContext)
                {
                    for (int j = 0; j < childTree.ChildCount; j++)
                    {
                        if (childTree.GetChild(j) is CSharpParser.TypeContext)
                        {
                            type = childTree.GetChild(j).GetText();
                            Console.WriteLine(type);
                        }
                        else if (childTree.GetChild(j) is CSharpParser.IdentifierContext)
                        {
                            varId = childTree.GetChild(j).GetText();
                            Console.WriteLine(varId);
                        }
                    }
                }
                /*
                for (int j = 0; j < childTree.ChildCount; j++)
                {
                    //if(childTree.GetChild(j) is CSharpParser.TypeContext)
                    //{
                    //    type = childTree.GetChild(j).GetText();
                    //    Console.Out.WriteLine(type);
                    //}else if(childTree.GetChild(j) is CSharpParser.Variable_declaratorContext)
                    //{
                    //    varId = childTree.GetChild(j).GetText();
                    //    Console.Out.WriteLine(varId);
                    //}

                    Console.WriteLine(childTree.GetChild(j).GetText());
                    Console.WriteLine(childTree.GetChild(j).ChildCount);
                    for(int k = 0; k < childTree.GetChild(j).ChildCount; k++)
                    {
                        Console.WriteLine(childTree.GetChild(j).GetChild(k).GetText());
                        Console.WriteLine(childTree.GetChild(j).GetChild(k).GetType().ToString());
                    }
                    
                }*/


                /*
                ParserRuleContext parrentOfTree = (ParserRuleContext)context.Parent;
                while (!(parrentOfTree is CSharpParser.Method_declarationContext) && !(parrentOfTree is CSharpParser.Constructor_declarationContext))
                {
                    try
                    {
                        parrentOfTree = (ParserRuleContext)parrentOfTree.Parent;
                        if (parrentOfTree == null) { return; }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                if (parrentOfTree is CSharpParser.Method_declarationContext)
                {
                    //for (int k = 0; k < parrentOfTree.Parent.ChildCount; k++)
                    //{
                    Console.WriteLine("Method name: " + parrentOfTree.GetChild(0).GetText());
                    Console.WriteLine("output type: " + parrentOfTree.Parent.GetChild(0).GetText());
                    Console.WriteLine(parrentOfTree.Parent.Parent.Parent.Parent.Parent.Parent.GetType().ToString());
                    //}

                }
                parrentOfTree = (ParserRuleContext)parrentOfTree.Parent;
                while (!(parrentOfTree is CSharpParser.Class_definitionContext))
                {
                    try
                    {
                        parrentOfTree = (ParserRuleContext)parrentOfTree.Parent;
                        if (parrentOfTree == null) { return; }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                //Console.WriteLine(parrentOfTree.GetType().ToString());
                Console.WriteLine("Class name: " + parrentOfTree.GetChild(1).GetText());
                */
            }
        }
    }
}
