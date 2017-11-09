using AntlrTestCsharp.Object;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AntlrTestCsharp.Config
{
    public class ConfigLoadCase
    {
        private static string CONFIG_PATH = "";

        private List<ExpressionCase> listCase = new List<ExpressionCase>();

        public ConfigLoadCase(string pathFile)
        {
            loadConfigMethod(pathFile);
        }

        public List<ExpressionCase> getListCase()
        {
            return listCase;
        }

        private void loadConfigMethod(string pathFile)
        {
            if (pathFile == null)
            {
                return;
            }
            var assembly = Assembly.GetExecutingAssembly();
            using(Stream stream = assembly.GetManifestResourceStream(pathFile))
            using (StreamReader sr = new StreamReader(stream))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();
                    if (String.IsNullOrEmpty(line) || line.StartsWith("-"))
                    {
                        continue;
                    }
                    ExpressionCase tmpExpressionCase = new ExpressionCase(line);

                    if (tmpExpressionCase != null)
                    {
                        listCase.Add(tmpExpressionCase);
                    }
                }
            }
        }
    }
}
