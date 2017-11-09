using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using AntlrTestCsharp.Config;
using AntlrTestCsharp.Object;
using AntlrTestCsharp.parser;
using AntlrTestCsharp.Tracer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AntlrTestCsharp
{
    class Program
    {

        static void Main(string[] args)
        {
            //string solutionPath = @"E:\Project\FashionShop2.0\FashionShop2.0.sln";
            string solutionPath = @"C:\Users\dbui\Desktop\FashionShop2.0\FashionShop2.0.sln";
            List<string> listTmpProject = loadSolution(solutionPath);
            List<string> listProject = new List<string>();
            foreach (var item in listTmpProject)
            {
                if (!item.Contains(":"))
                {
                    string tmp = solutionPath.Substring(0, solutionPath.LastIndexOf('\\') + 1) + item;
                    listProject.Add(tmp);
                }
                else
                {
                    listProject.Add(item);
                }
            }


            //progress toan bo project
            //foreach (var project in listProject)
            //{
            //    Progress(project);
            //}


            //test module hoa
           
            List<string> listCs = new List<string>();
            List<string> listAscx = new List<string>();
            List<string> listAspx = new List<string>();
            List<string> listConfig = new List<string>();

            foreach (var item in listProject)
            {
                string[] fileAscx = Directory.GetFiles(item, "*.ascx", SearchOption.AllDirectories);
                string[] fileAspx = Directory.GetFiles(item, "*.aspx", SearchOption.AllDirectories);
                string[] fileCs = Directory.GetFiles(item, "*.cs", SearchOption.AllDirectories);
                string[] fileConfig = Directory.GetFiles(item, "Web.config", SearchOption.AllDirectories);

                //khoi tao danh sach file cs
                if (listCs == null || listCs.Count < 1)
                {
                    listCs = new List<string>();
                }
                foreach (var file in fileCs)
                {
                    listCs.Add(file);
                }

                //khoi tao danh sach file ascx
                if (listAscx == null || listAscx.Count < 1)
                {
                    listAscx = new List<string>();
                }
                foreach (var file in fileAscx)
                {
                    listAscx.Add(file);
                }

                //Khoi tao danh sach file aspx
                if (listAspx == null || listAspx.Count < 1)
                {
                    listAspx = new List<string>();
                }
                foreach (var file in fileAspx)
                {
                    listAspx.Add(file);
                }
                //Khoi tao danh sach file config
                if (listConfig == null || listConfig.Count < 1)
                {
                    listConfig = new List<string>();
                }
                foreach (var file in fileConfig)
                {
                    listConfig.Add(file);
                }
            }
            List<ItemObject> finalResult = new List<ItemObject>();

            //Scan SQL
            foreach (var item in listCs)
            {
                finalResult = scanSQL(item, finalResult);
            }
            //Scan XXE
            foreach(var item in listCs)
            {
                finalResult = scanXXE(item, finalResult);
            }
            //Scan ghi log nhay cam
            foreach(var item in listCs)
            {
                finalResult = scanLogging(item, finalResult); 
            }
            //Scan ma hoa yeu
            foreach (var item in listCs)
            {
                finalResult = scanBadCrypt(item, finalResult);
            }
            //Scan file upload
            foreach (var item in listCs)
            {
                finalResult = scanFileUpload(item, finalResult);
            }
            //Scan XSS
            foreach (var item in listAspx)
            {
                finalResult = scanXSS(item, finalResult);
            }
            foreach (var item in listAscx)
            {
                finalResult = scanXSS(item, finalResult);
            }
            //Scan file Config
            foreach (var item in listConfig)
            {
                finalResult = scanDebug(item, finalResult);
            }

            if (finalResult != null)
            {
                //Console.WriteLine("ok");
                printResult(finalResult);
            }

            Console.ReadLine();

        }

        /*
        /// <summary>
        /// Module quet toan bo project
        /// </summary>
        /// <param name="prPath"></param>

        private static void Progress(string prPath)
        {
            // Truyen duong dan Project

            //string pathFile = @"E:\Project\FashionShop2.0";
            //Lay cac file Cs
            string[] fileCS = Directory.GetFiles(prPath, "*.cs", SearchOption.AllDirectories);
            //Lay cac file ascx
            string[] fileAscx = Directory.GetFiles(prPath, "*.ascx", SearchOption.AllDirectories);
            //Lay cac file aspx
            string[] fileAspx = Directory.GetFiles(prPath, "*.aspx", SearchOption.AllDirectories);
            //Lay file Web.config
            string[] fileConfig = Directory.GetFiles(prPath,"Web.config", SearchOption.AllDirectories);
            //Khoi tao list ket qua
            List<ItemObject> listResult = new List<ItemObject>();

            /* TEST FILE SOLUTION
            string solutionDir = @"E:\C# Project\AntlrTestCsharp\AntlrTestCsharp.sln";
            ConfigFindListProject config = new ConfigFindListProject(solutionDir);
            List<string> listProject = config.getListProject();

            foreach (var item in listProject)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("========================");
            
            //Console.WriteLine("\n+++SQL Injection+++");
            //Console.WriteLine("=======================================================");
            foreach (var filename in fileCS)
            {
                //if (filename.Contains("SanPhamController"))
                //{
                string code = readFile2(filename);

                CSharpLexer lexer = new CSharpLexer(new AntlrInputStream(code));
                lexer.RemoveErrorListeners();
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                CSharpParser parser = new CSharpParser(tokens);

                IParseTree tree = parser.compilation_unit();
                ParseTreeWalker walker = new ParseTreeWalker();
                ExtractClassParser listener = new ExtractClassParser(parser);
                //FindGlobalVariable listener = new FindGlobalVariable(parser);

                walker.Walk(listener, tree);
                //}
                //Main tracer

                //sql
                if (listener.listMethodContext != null)
                {
                    //Console.WriteLine(filename);
                    List<MethodContext> listMethod = listener.getListMethod();
                    foreach (var method in listMethod)
                    {
                        ParseTreeWalker methodWalker = new ParseTreeWalker();
                        FindQueryInMethod queryListener = new FindQueryInMethod(parser, method.lineList);
                        methodWalker.Walk(queryListener, method.context);
                        //if (queryListener.queryVar != null)
                        //{
                        //    method.queryVar = queryListener.queryVar;
                        //}
                        //method.lineList = queryListener.listExpressLine;

                        FindLineOfExpression lineListener = new FindLineOfExpression(parser, method.context, queryListener.listExpressLine, queryListener.commandVar, queryListener.queryVar);
                        methodWalker.Walk(lineListener, method.context);

                        method.lineList = lineListener.listExpressLine;

                        FindUsedMethodInClass methodListener = new FindUsedMethodInClass(parser, method);
                        methodWalker.Walk(methodListener, method.context);
                        //List<MethodInfor> listResult = methodListener.listResult;
                        //print(listResult);

                        if (methodListener.listResult != null)
                        {
                            foreach (var item in methodListener.listResult)
                            {
                                ItemObject obj = new ItemObject(item.BaselineItem, item.methodName, item.listExp, filename, item.startLine, "FAIL");
                                listResult.Add(obj);
                            }
                        }
                    }
                }
                //end


                //xxe
                if (listener.listXMLContext != null)
                {
                    //Console.WriteLine("\n+++XXE+++");
                    //Console.WriteLine("=======================================================");
                    //Console.WriteLine(filename);
                    List<ParserRuleContext> listMethod = listener.listXMLContext;
                    foreach (var method in listMethod)
                    {
                        ParseTreeWalker methodWalker = new ParseTreeWalker();
                        FindXXEInMethod methodListener = new FindXXEInMethod(parser);
                        methodWalker.Walk(methodListener, method);
                        if (methodListener.isVuln)
                        {
                            //Console.WriteLine(" + " + methodListener.tmpMethod.startLine + " | " + methodListener.tmpMethod.methodName);
                            ItemObject obj = new ItemObject(methodListener.tmpMethod.BaselineItem, methodListener.tmpMethod.methodName, null, filename, methodListener.tmpMethod.startLine, "FAIL");
                            listResult.Add(obj);
                        }
                    }
                }


                //upload

                FindUploadInMethod uploadListener = new FindUploadInMethod(parser);
                walker.Walk(uploadListener, tree);
                if (uploadListener.listMethod != null)
                {
                    foreach (var item in uploadListener.listMethod)
                    {
                        ItemObject obj = new ItemObject(item.BaselineItem, item.methodName, null, filename, item.startLine, "WARNING");
                        listResult.Add(obj);
                    }
                }
                //if(up)


            }

            //Console.WriteLine("\n+++XSS+++");
            //Console.WriteLine("=======================================================");
            foreach (var fileName in fileAscx)
            {
                //Console.WriteLine(fileName);
                string code = readFile2(fileName);
                //Console.WriteLine(code.Length);
                TraceForXss tracer = new TraceForXss(code, fileName);
                if (tracer.listItem != null)
                {
                    foreach (var item in tracer.listItem)
                    {
                        listResult.Add(item);
                    }
                }
            }

            foreach (var fileName in fileAspx)
            {
                string code = readFile2(fileName);
                TraceForXss tracer = new TraceForXss(code, fileName);
                if (tracer.listItem != null)
                {
                    foreach (var item in tracer.listItem)
                    {
                        listResult.Add(item);
                    }
                }

            }


            if (listResult != null)
            {
                printResult(listResult);
            }
        }
*/
        //Module quet SQL
        private static List<ItemObject> scanSQL(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }

            string code = readFile2(fileName);

            CSharpLexer lexer = new CSharpLexer(new AntlrInputStream(code));
            lexer.RemoveErrorListeners();
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);

            IParseTree tree = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();
            ExtractClassParser listener = new ExtractClassParser(parser);
            //FindGlobalVariable listener = new FindGlobalVariable(parser);

            walker.Walk(listener, tree);
            //}
            //Main tracer

            //sql
            if (listener.listMethodContext != null)
            {
                //Console.WriteLine(filename);
                List<MethodContext> listMethod = listener.getListMethod();
                foreach (var method in listMethod)
                {
                    ParseTreeWalker methodWalker = new ParseTreeWalker();
                    FindQueryInMethod queryListener = new FindQueryInMethod(parser, method.lineList);
                    methodWalker.Walk(queryListener, method.context);
                    FindLineOfExpression lineListener = new FindLineOfExpression(parser, method.context, queryListener.listExpressLine, queryListener.commandVar, queryListener.queryVar);
                    methodWalker.Walk(lineListener, method.context);
                    method.lineList = lineListener.listExpressLine;
                    FindUsedMethodInClass methodListener = new FindUsedMethodInClass(parser, method);
                    methodWalker.Walk(methodListener, method.context);
                    if (methodListener.listResult != null)
                    {
                        foreach (var item in methodListener.listResult)
                        {
                            ItemObject obj = new ItemObject(item.BaselineItem, item.methodName, item.listExp, fileName, item.startLine, "FAIL");
                            listResult.Add(obj);
                        }
                    }
                }
            }
            return listResult;
        }

        //Module quet XXE
        private static List<ItemObject> scanXXE(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }

            string code = readFile2(fileName);

            CSharpLexer lexer = new CSharpLexer(new AntlrInputStream(code));
            lexer.RemoveErrorListeners();
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);

            IParseTree tree = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();
            ExtractClassParser listener = new ExtractClassParser(parser);
            //FindGlobalVariable listener = new FindGlobalVariable(parser);

            walker.Walk(listener, tree);

            if (listener.listXMLContext != null)
            {
                List<ParserRuleContext> listMethod = listener.listXMLContext;
                foreach (var method in listMethod)
                {
                    ParseTreeWalker methodWalker = new ParseTreeWalker();
                    FindXXEInMethod methodListener = new FindXXEInMethod(parser);
                    methodWalker.Walk(methodListener, method);
                    if (methodListener.isVuln)
                    {
                        ItemObject obj = new ItemObject(methodListener.tmpMethod.BaselineItem, methodListener.tmpMethod.methodName, null, fileName, methodListener.tmpMethod.startLine, "FAIL");
                        listResult.Add(obj);
                    }
                }
            }

            return listResult;
        }

        //Module quet Ma hoa yeu
        private static List<ItemObject> scanBadCrypt(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }

            string code = readFile2(fileName);

            CSharpLexer lexer = new CSharpLexer(new AntlrInputStream(code));
            lexer.RemoveErrorListeners();
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);

            IParseTree tree = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();
            ExtractClassParser listener = new ExtractClassParser(parser);
            //FindGlobalVariable listener = new FindGlobalVariable(parser);

            walker.Walk(listener, tree);

            if (listener.listBadCryptContext != null)
            {
                List<ParserRuleContext> listMethod = listener.listBadCryptContext;
                foreach (var method in listMethod)
                {
                    ParseTreeWalker methodWalker = new ParseTreeWalker();
                    FindBadCrypt methodListener = new FindBadCrypt(parser);
                    methodWalker.Walk(methodListener, method);
                    ItemObject obj = new ItemObject(methodListener.tmpMethod.BaselineItem, methodListener.tmpMethod.methodName, null, fileName, methodListener.tmpMethod.startLine, "FAIL");
                    listResult.Add(obj);
                }
            }

            return listResult;
        }

        //Module quet FileUpload
        private static List<ItemObject> scanFileUpload(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }
            string code = readFile2(fileName);

            CSharpLexer lexer = new CSharpLexer(new AntlrInputStream(code));
            lexer.RemoveErrorListeners();
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);

            IParseTree tree = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();
            FindUploadInMethod uploadListener = new FindUploadInMethod(parser);
            walker.Walk(uploadListener, tree);
            if (uploadListener.listMethod != null)
            {
                foreach (var item in uploadListener.listMethod)
                {
                    ItemObject obj = new ItemObject(item.BaselineItem, item.methodName, null, fileName, item.startLine, "WARNING");
                    listResult.Add(obj);
                }
            }

            return listResult;
        }

        //Module quet XSS
        private static List<ItemObject> scanXSS(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }

            string code = readFile2(fileName);

            TraceForXss tracer = new TraceForXss(code, fileName);
            if (tracer.listItem != null)
            {
                foreach (var item in tracer.listItem)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }

        //Module quet LOG
        private static List<ItemObject> scanLogging(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }
            string code = readFile2(fileName);

            CSharpLexer lexer = new CSharpLexer(new AntlrInputStream(code));
            lexer.RemoveErrorListeners();
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            CSharpParser parser = new CSharpParser(tokens);

            IParseTree tree = parser.compilation_unit();
            ParseTreeWalker walker = new ParseTreeWalker();
            FindLoggingInMethod uploadListener = new FindLoggingInMethod(parser);
            walker.Walk(uploadListener, tree);
            if (uploadListener.listMethod != null)
            {
                foreach (var item in uploadListener.listMethod)
                {
                    ItemObject obj = new ItemObject(item.BaselineItem, item.methodName, null, fileName, item.startLine, "FAIL");
                    listResult.Add(obj);
                }
            }
            return listResult;
        }

        //Module quet che do debug
        private static List<ItemObject> scanDebug(string fileName, List<ItemObject> listResult)
        {
            if (listResult == null || listResult.Count == 0)
            {
                listResult = new List<ItemObject>();
            }

            string code = readFile2(fileName);

            TraceForMisconfig tracer = new TraceForMisconfig(code, fileName);
            if (tracer.listItem != null)
            {
                foreach (var item in tracer.listItem)
                {
                    listResult.Add(item);
                }
            }
            return listResult;
        }

        //Module ho tro
        private static void printResult(List<ItemObject> listResult)
        {
            foreach (var item in listResult)
            {
                Console.WriteLine(item.identify + " | " + item.displayTxt + " | " + item.pathFile + " | " + item.lineNumber + " | " + item.result);
                if (item.listExp != null)
                {
                    foreach (var point in item.listExp)
                    {
                        Console.WriteLine(" + " + point.value + " | " + point.line);
                    }
                }
            }

            Console.WriteLine("Total vuln: " + listResult.Count);
        }
        private static void print(List<MethodInfor> listResult)
        {
            if (listResult != null)
            {
                foreach (var item in listResult)
                {
                    Console.WriteLine(item.className + "|" + item.methodName + "|" + item.startLine);
                    foreach (var point in item.listExp)
                    {
                        Console.WriteLine(" + " + point.line + " | " + point.value);
                    }

                }
            }
            else
            {
                return;
            }
        }
        private static string readFile(string filePath)
        {
            StringBuilder sb = new StringBuilder();
            foreach (string line in File.ReadLines(filePath))
            {
                sb.Append(line);
            }
            return sb.ToString();
        }
        private static string readFile2(string filePath)
        {
            byte[] encoded = File.ReadAllBytes(filePath);
            string result = System.Text.Encoding.UTF8.GetString(encoded);
            return result;
        }
        private static List<string> loadSolution(string pathFile)
        {
            if (pathFile == null)
            {
                return null;
            }

            using (StreamReader sr = new StreamReader(pathFile))
            {
                List<string> listProject = new List<string>();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (String.IsNullOrEmpty(line) || !line.StartsWith("Project("))
                    {
                        continue;
                    }
                    string[] tmpProjectArr = line.Split(',');
                    if (tmpProjectArr.Length == 3)
                    {
                        string tmpProject = tmpProjectArr[1];
                        tmpProject = tmpProject.Substring(tmpProject.IndexOf('"') + 1, tmpProject.LastIndexOf('\\') - tmpProject.IndexOf('"') - 1);
                        listProject.Add(tmpProject);
                    }
                }
                return listProject;
            }
        }
    }
}
