using AntlrTestCsharp.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System.Text.RegularExpressions;

namespace AntlrTestCsharp.parser
{
    public class FindInputOfMethod : CSharpParserBaseListener
    {
        CSharpParser parser;
        public List<VariableDefine> listVariable { get; set; }
        public List<VariableDefine> listLocalVariable { get; set; }
        public MethodInfor methodInfor { get; set; }
        public ParserRuleContext classTree { get; set; }

        public FindInputOfMethod(CSharpParser parser)
        {
            this.parser = parser;
        }

        public override void EnterFormal_parameter_list([NotNull] CSharpParser.Formal_parameter_listContext context)
        {
            ParserRuleContext ancesstorOfTree = (ParserRuleContext)context.Parent;

            if (!(ancesstorOfTree is CSharpParser.Method_declarationContext))
            {
                return;
            }

            int childLength = context.GetChild(0).ChildCount;
            string type = "";
            string varId = "";
            for (int i = 0; i < childLength; i = i + 2)
            {
                IParseTree childTree = context.GetChild(0).GetChild(i).GetChild(0);
                if (childTree is CSharpParser.Arg_declarationContext)
                {
                    for (int j = 0; j < childTree.ChildCount; j++)
                    {
                        if (childTree.GetChild(j) is CSharpParser.TypeContext)
                        {
                            type = childTree.GetChild(j).GetText();
                        }
                        else if (childTree.GetChild(j) is CSharpParser.IdentifierContext)
                        {
                            varId = childTree.GetChild(j).GetText();
                        }
                    }
                    if (string.IsNullOrEmpty(type) || string.IsNullOrEmpty(varId))
                    {
                        return;
                    }
                    VariableDefine variableDefine = new VariableDefine(type, varId);
                    if (listVariable != null)
                    {
                        if (!(listVariable.Any(x => x.name == variableDefine.name)))
                        {
                            listVariable.Add(variableDefine);
                        }
                    }
                    else
                    {
                        listVariable = new List<VariableDefine>();
                        listVariable.Add(variableDefine);
                    }
                    methodInfor = new MethodInfor();
                    List<string> paramx = new List<string>();
                    foreach (var item in listVariable)
                    {
                        paramx.Add(item.name);
                    }
                    methodInfor.listArgs = paramx;
                    int startLine = ancesstorOfTree.Start.Line;
                    methodInfor.startLine = startLine;
                    ParserRuleContext parrentOfTree = (ParserRuleContext)childTree.Parent;
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
                        methodInfor.methodName = parrentOfTree.GetChild(0).GetText();
                    }
                    else if (parrentOfTree is CSharpParser.Constructor_declarationContext)
                    {
                        methodInfor.methodName = parrentOfTree.GetChild(0).GetText();
                    }
                    parrentOfTree = (ParserRuleContext)parrentOfTree.Parent;
                    if (parrentOfTree is CSharpParser.Typed_member_declarationContext)
                    {
                        methodInfor.outputType = parrentOfTree.GetChild(0).GetText();
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
                    if (parrentOfTree is CSharpParser.Class_definitionContext)
                    {
                        classTree = parrentOfTree;
                        methodInfor.className = parrentOfTree.GetChild(1).GetText();
                    }

                }
            }
        }

        public override void EnterLocal_variable_declaration([NotNull] CSharpParser.Local_variable_declarationContext context)
        {
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
                    //Console.WriteLine(childTree.Parent.Parent.Parent.Parent.Parent.Parent.GetText());
                    if (childTree.ChildCount >= 3)
                    {
                        value = childTree.GetChild(2).GetText();
                    }
                }
            }
            VariableDefine newVar = new VariableDefine(type, varId, value);
            if (!isString(newVar.value))
            {
                if (listLocalVariable != null)
                {
                    if (!listLocalVariable.Any(x => x.name == varId))
                    {
                        listLocalVariable.Add(newVar);
                    }
                }
                else
                {
                    listLocalVariable = new List<VariableDefine>();
                    listLocalVariable.Add(newVar);
                }
            }
        }

        private bool isString(string value)
        {
            string pattern = "^\".*\"$";
            Regex regex = new Regex(pattern);
            Match ma = regex.Match(value);
            if (string.IsNullOrEmpty(ma.Value))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
