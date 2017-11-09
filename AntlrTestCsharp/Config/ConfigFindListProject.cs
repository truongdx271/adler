using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrTestCsharp.Config
{
    public class ConfigFindListProject
    {
        public ConfigFindListProject(string solutionPath)
        {
            findListProject(solutionPath);
        }

        private List<string> projectList;

        public List<string> getListProject()
        {
            return projectList;
        }

        private void findListProject(string solutionPath)
        {
            if (solutionPath == null)
            {
                return;
            }
            projectList = new List<string>();
            using (StreamReader sr = new StreamReader(solutionPath))
            {
                string line;
                string[] tmpLine;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (String.IsNullOrEmpty(line))
                    {
                        continue;
                    }
                    if (line.StartsWith("Project"))
                    {
                        tmpLine = line.Split(',');
                        if (tmpLine.Length == 3)
                        {
                            projectList.Add(tmpLine[1]);
                        }
                    }
                }
            }
        }
    }
}
