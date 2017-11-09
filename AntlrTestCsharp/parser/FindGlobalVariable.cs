using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using AntlrTestCsharp.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.parser
{
    public class FindGlobalVariable : CSharpParserBaseListener
    {
        CSharpParser parser;
        public List<VariableDefine> listGlobalVar { get; set; }

        public FindGlobalVariable(CSharpParser parser)
        {
            this.parser = parser;
        }

        public override void EnterField_declaration([NotNull] CSharpParser.Field_declarationContext context)
        {
            //GLOBAL VAR

            //Console.WriteLine(context.GetChild(0).ChildCount);

            //Console.WriteLine(context.Parent.GetText());
            //Console.WriteLine(context.Parent.GetType().ToString());
            //Console.WriteLine(context.Parent.ChildCount);
            //Console.WriteLine(context.Parent.GetChild(0).GetText());
            //Console.WriteLine(context.Parent.GetChild(0).GetType().ToString());
            //Console.WriteLine(context.Parent.GetChild(1).GetText());
            //Console.WriteLine(context.Parent.GetChild(1).GetType().ToString());

            //if (context.Parent is CSharpParser.Typed_member_declarationContext)
            //{
            //    string type = "";
            //    string value = "";
            //    string varId = "";

            //    type = context.Parent.GetChild(0).GetText();


            //}
        }

        public override void EnterVariable_declarator([NotNull] CSharpParser.Variable_declaratorContext context)
        {
            string type = "";
            string varId = "";
            string value = "";
            if (context.GetChild(0) is CSharpParser.IdentifierContext && context.GetChild(2) is CSharpParser.Variable_initializerContext)
            {
                varId = context.GetChild(0).GetText();
                value = context.GetChild(2).GetText();
                type = context.Parent.Parent.Parent.GetChild(0).GetText();
            }

            VariableDefine newVar = new VariableDefine(type, varId, value);

            if (listGlobalVar != null)
            {
                if (!listGlobalVar.Any(x => x.name == newVar.name))
                {
                    listGlobalVar.Add(newVar);
                }
            }
            else
            {
                listGlobalVar = new List<VariableDefine>();
                listGlobalVar.Add(newVar);
            }

        }



    }
}
