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

        public static void RenameFile(string OldDirectory, string NewDirectory, List<IFixFile> Files)
        {
            foreach (var FileName in Files)
            {
             //  File.Copy(OldDirectory + "\\" + FileName.Name, NewDirectory + "\\" + FileName.NewName);
            }

        }
    }
}
