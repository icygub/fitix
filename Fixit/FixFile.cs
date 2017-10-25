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
        private string _NewName;

        public string NewName
        {
            get { return _NewName; }
            set
            {
                _NewName = value;
                int myCount = 0;
                if (_NewName.Contains(@"[a-zA-Z]"))
                {
                    myCount += 1;
                }
                                    myCount += Name.Length;
                if (NewName.Length < myCount)
                {
                    for (int i = 0; NewName.Length < Name.Length; i++)
                    {
                        NewName = "0" + NewName;
                    }
                }
            }
        }
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
