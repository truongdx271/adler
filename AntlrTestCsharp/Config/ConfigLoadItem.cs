using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace AntlrTestCsharp.Config
{
    public class ConfigLoadItem
    {
        List<string> listItem = new List<string>();
        public ConfigLoadItem(string pathFile)
        {
            loadItemMethod(pathFile);
        }

        public List<string> getListItem()
        {
            return listItem;
        }

        private void loadItemMethod(string pathFile)
        {
            if (pathFile == null)
            {
                return;
            }
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(pathFile))
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

                    listItem.Add(line);

                }
            }
        }
    }
}
