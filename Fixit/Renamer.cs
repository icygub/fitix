using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixit
{
    class Renamer
    {

        public void RenameFile(string OldDirectory, string NewDirectory, List<string> CurrentName, List<string> NewName)
        {
            foreach (var FileName in CurrentName)
            {
               // File.Copy(OldDirectory + FileName, NewDirectory + NewName[CurrentName.IndexOf(FileName)]);
            }

        }
    }
}
