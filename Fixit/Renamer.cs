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
                var SplitName = FileName.RealName.Split('_');
                string myFilename = "";
                for(int i = 0; i < SplitName.Length - 1 ; i++)
                {
                    myFilename += SplitName[i];

                    myFilename += "_";
                }
                myFilename += FileName.NewName;
                SplitName = FileName.RealName.Split('.');
                myFilename += "." + SplitName[1];
                //First lets find out what is the extension.
                //FileName.RealName.Split('.')[1];
                File.Copy(OldDirectory + "\\" + FileName.RealName, NewDirectory + "\\" + myFilename);
            }

        }
    }
}
