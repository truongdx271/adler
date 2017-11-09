using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using AntlrTestCsharp.Object;
using AntlrTestCsharp.parser;
using System.Collections.Generic;

namespace AntlrTestCsharp.Tracer
{
    public class TraceForCase102
    {
        CSharpParser parser;
        ParserRuleContext treeContext;
        public MethodInfor methodInfor { get; set; }

        public List<VariableDefine> listFormalInput { get; set; }
        public List<VariableDefine> listLocalVar { get; set; }
        public List<VariableDefine> listGlobalVar { get; set; }
        public List<TempExpression> listExpression { get; set; }

        public bool isVulnMethod { get; set; }
        public bool tmpVulnMethod { get; set; }

        public TraceForCase102(CSharpParser parser, ParserRuleContext treeContext, List<TempExpression> listExpression)
        {
            this.parser = parser;
            this.treeContext = treeContext;
            this.listExpression = listExpression;
            this.isVulnMethod = false;

            ParseTreeWalker walkerMethod = new ParseTreeWalker();
            FindInputOfMethod findInput = new FindInputOfMethod(parser);
            walkerMethod.Walk(findInput, treeContext);

            this.listFormalInput = findInput.listVariable;
            this.listLocalVar = findInput.listLocalVariable;
            this.methodInfor = findInput.methodInfor;
            methodInfor.BaselineItem = 402;

            ParserRuleContext classContext = findInput.classTree;
            FindGlobalVariable findGlobalVar = new FindGlobalVariable(parser);
            walkerMethod.Walk(findGlobalVar, classContext);

            if (findGlobalVar.listGlobalVar != null)
            {
                this.listGlobalVar = findGlobalVar.listGlobalVar;
            }

            //FindVariableInMethod findVar = new FindVariableInMethod(parser,);
            //walkerMethod.Walk(findVar, treeContext);

            processTracer102(treeContext);
        }

        private void processTracer102(ParserRuleContext treeContext)
        {
            //int count = 0;
            List<TempExpression> listPoint = new List<TempExpression>();
            List<TempExpression> listResult = new List<TempExpression>();
            foreach (var item in listExpression)
            {
                for (int i = 0; i < listFormalInput.Count; i++)
                {
                    if (item.value.Equals(listFormalInput[i].name) && listFormalInput[i].type.Equals("string"))
                    {
                        /*
                        ParseTreeWalker walkerMethod = new ParseTreeWalker();
                        FindVariableInMethod findVar = new FindVariableInMethod(parser, item.value, item.line);
                        walkerMethod.Walk(findVar, treeContext);
                        if (findVar.isContain)
                        {
                        }
                        else
                        {

                            if (!listPoint.Contains(item))
                            {
                                listPoint.Add(item);
                            }
                            count += 1;
                           
                        }
                         */
                        if (!listPoint.Contains(item))
                        {
                            item.isVuln = true;
                            listPoint.Add(item);
                        }
                    }
                }
                for (int i = 0; i < listLocalVar.Count; i++)
                {
                    if (item.value.Equals(listLocalVar[i].name) && listLocalVar[i].type.Equals("string"))
                    {
                        if (!listPoint.Contains(item))
                        {
                            item.isVuln = true;
                            listPoint.Add(item);
                        }
                    }
                }
                if (listGlobalVar != null)
                {
                    for (int i = 0; i < listGlobalVar.Count; i++)
                    {
                        /*
                        if (item.value.Equals(listGlobalVar[i].name) && listGlobalVar[i].type.Equals("string") && isStatic(listGlobalVar[i].value))
                        {
                            //isVulnMethod = false;
                        }
                        else if (item.value.Equals(listGlobalVar[i].name) && listGlobalVar[i].type.Equals("string") && !isStatic(listGlobalVar[i].value))
                        {
                            
                            if (!listPoint.Contains(item))
                            {
                                listPoint.Add(item);
                            }
                            count += 1;
                            
                        }
                        */
                        if (item.value.Equals(listGlobalVar[i].name) && listGlobalVar[i].type.Equals("string"))
                        {
                            if (!isStatic(listGlobalVar[i].value))
                            {
                                item.isVuln = true;
                            }
                            else
                            {
                                item.isVuln = false;
                            }
                            if (!listPoint.Contains(item))
                            {
                                listPoint.Add(item);
                            }
                        }
                    }
                }
            }

            if (listPoint.Count > 0)
            {
                ParseTreeWalker walkerMethod = new ParseTreeWalker();
                FindVariableInMethod findVar = new FindVariableInMethod(parser, listPoint);
                walkerMethod.Walk(findVar, treeContext);
                foreach (var item in findVar.listExpress)
                {
                    if (item.isVuln)
                    {
                        listResult.Add(item);
                    }
                }
            }

            if (listResult.Count > 0)
            {
                isVulnMethod = true;
                this.methodInfor.listExp = listResult;
            }


            //if (count > 0) isVulnMethod = true;
            //this.methodInfor.listExp = listPoint;

        }

        private bool isStatic(string input)
        {
            if (input.Contains("\""))
            {
                //input = input.Trim();
                //int range = input.Length - input.IndexOf("=") - 1;
                //input = input.Substring(input.IndexOf("=") + 1, range);
                if (input.Contains("+"))
                {
                    string[] part = input.Split('+');
                    foreach (var item in part)
                    {
                        if (!item.EndsWith("\"")) { return false; }
                    }
                }
                if (input.EndsWith("\"")) { return true; }
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
