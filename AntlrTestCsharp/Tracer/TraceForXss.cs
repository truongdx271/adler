using AntlrTestCsharp.Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Tracer
{
    public class TraceForXss
    {
        string code;
        List<AspCommentObj> listComment;
        public List<ItemObject> listItem { get; set; }
        public TraceForXss(string code, string pathFile)
        {
            this.code = code;
            listComment = new List<AspCommentObj>();
            listItem = new List<ItemObject>();
            ProcessTraceXss(code, pathFile);

        }

        private void ProcessTraceXss(string code, string pathFile)
        {
            string pattern = "<%--(.[\\s\\S]*?)--%>";
            Regex regex = new Regex(pattern);
            MatchCollection ma = regex.Matches(code);
            foreach (Match item in ma)
            {
                AspCommentObj comment = new AspCommentObj(item.Index, item.Index + item.Length);
                listComment.Add(comment);
            }
            //foreach (var item in listComment)
            //{
            //    Console.WriteLine(item.startPos + " | " + item.endPos);
            //}

            pattern = "<%#(?!(\\s*Server|HttpUtility))[^>]*|<%=(?!(\\s*Server|HttpUtility))[^>]*";
            Regex xssRegex = new Regex(pattern);
            MatchCollection xssList = xssRegex.Matches(code);

            if (xssList.Count > 0)
            {
                foreach (Match item in xssList)
                {
                    if (listComment.Count > 0)
                    {
                        bool iscommented = false;
                        foreach (var comment in listComment)
                        {
                            if (item.Index >= comment.startPos && item.Index < comment.endPos)
                            {
                                iscommented = true;
                            }
                        }
                        if (!iscommented)
                        {
                            int line = getLine(code, item.Index);
                            string value = beautyResult(item.Value);
                            if (value != "csrftoken")
                            {
                                //Console.WriteLine(" + " + line + " | " + value);
                                ItemObject obj = new ItemObject(602, value, null, pathFile, line, "WARNING");
                                listItem.Add(obj);
                            }
                            //Console.WriteLine(" + " + line + " | " + item.Value.Replace(" ", "").Replace("<", "").Replace("%", "").Replace("#", "").Replace("%", "").Replace("=", ""));
                        }
                    }
                    else
                    {
                        int line = getLine(code, item.Index);
                        string value = beautyResult(item.Value);
                        if (value != "csrftoken")
                        {
                            //Console.WriteLine(" + " + line + " | " + value);
                            ItemObject obj = new ItemObject(602, value, null, pathFile, line, "WARNING");
                            listItem.Add(obj);
                        }
                        //Console.WriteLine(" + " + line + " | " + item.Value.Replace(" ", "").Replace("<", "").Replace("%", "").Replace("#", "").Replace("%", "").Replace("=", ""));
                    }
                }
            }
        }

        private int getLine(string code, int index)
        {
            // string code2 = "adasdas\n fdgdfgdg\n dgdfgdfg";
            string frontCode = code.Substring(0, index);
            string[] lineArr = frontCode.Split('\n');
            return lineArr.Length;
        }

        private string beautyResult(string result)
        {
            result = result.Replace(" ", "").Replace("<", "").Replace("%", "").Replace("#", "").Replace("%", "").Replace("=", "");
            return result;
        }
    }
}
