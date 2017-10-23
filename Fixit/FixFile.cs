using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fixit
{
    class FixFile
    {
        public string Name { get; set; }
        public string NewName { get; set; }
        public string RealName { get; set; }

        public FixFile(string name, string newname, string realName)
        {
            this.Name = name;
            this.NewName = newname;
            this.RealName = realName;
        }

        public static List<FixFile> JustLastName(List<FixFile> FileList)
        {
           
                foreach (var File in FileList)
                {
                    try
                    {
                    File.Name = (File.Name.Split('_')[2]).Split('.')[0];
                    File.NewName = (File.NewName.Split('_')[2]).Split('.')[0];
                    }
                    catch
                    {
                            File.Name = File.Name.Split('.')[0];
                            File.NewName = File.NewName.Split('.')[0];
                    }
            }
                return FileList;


        }
    }
}
