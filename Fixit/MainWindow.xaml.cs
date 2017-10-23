using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MessageBox = System.Windows.MessageBox;
using Path = System.IO.Path;

namespace Fixit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            LoadFiles(ConfigurationManager.AppSettings["Path"]);
            newPathSet(ConfigurationManager.AppSettings["NewPath"]);
            //this.ActiveControl = NewNameColumn.cell
        }

        private void btnOldPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Select Path:";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFiles(fbd.SelectedPath);
            }
        }

        private void LoadFiles(string path)
        {
            txtPath.Text = path;

            /*
             * Got one of the answers in Stackoverflow to help here
             * This way it will only load the files specified.
             * https://stackoverflow.com/questions/163162/can-you-call-directory-getfiles-with-multiple-filters
             * 
             */
            var files = new List<IFixFile>();
            var lastFile = "";
            foreach (string imageFile in Directory.GetFiles(path))
            {
                files.Add(new IFixFile(Path.GetFileName(imageFile), Path.GetFileName(imageFile), Path.GetFileName(imageFile)));
                lastFile = imageFile;
            }
            var displayName = Path.GetFileName(lastFile).Split('_');
            try
            {
                FileName.Content = displayName[0] + "\uff3f" + displayName[1];
            }
            catch
            {
                FileName.Content = "Files not in the NAME__NUMBER sequence.";
            }
            CreateTable(files);
        }
        List<IFixFile> myListOfFile = new List<IFixFile>();
        private void CreateTable(List<IFixFile> fileList)
        {
           
            myListOfFile = IFixFile.JustLastName(fileList);
            FileListTable.ItemsSource = myListOfFile;
            //FileListTable.ItemsSource = IFixFile.JustLastName(fileList);
            //FileListTable.Columns[0].IsReadOnly = true;
        }

        private void DefaultPath_Click(object sender, RoutedEventArgs e)
        {
            System.Configuration.Configuration config =
                ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Path"].Value = txtPath.Text;
            config.AppSettings.Settings["NewPath"].Value = NewPathText.Text;

            config.Save(ConfigurationSaveMode.Modified);

            MessageBox.Show("Default Path changed successfully.", "Confirmation", MessageBoxButton.OK);
        }

        private void OpenNewPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.ShowNewFolderButton = false;
            fbd.Description = "Select Path:";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                newPathSet(fbd.SelectedPath);

            }
        }

        private void newPathSet(string path)
        {
            NewPathText.Text = path;

        }

        private void ApplyChanges_Click(object sender, RoutedEventArgs e)
        {
            Renamer.RenameFile(txtPath.Text, NewPathText.Text, myListOfFile);
        }
    }

    class IFixFile
    {
        public string Name { get; set; }
        public string NewName { get; set; }
        public string RealName { get; set; }

        public IFixFile(string name, string newname, string realName)
        {
            this.Name = name;
            this.NewName = newname;
            this.RealName = realName;
        }

        public static List<IFixFile> JustLastName(List<IFixFile> FileList)
        {
            try
            {
                foreach (var File in FileList)
                {
                    File.Name = (File.Name.Split('_')[2]).Split('.')[0];
                    File.NewName = (File.NewName.Split('_')[2]).Split('.')[0];
                }
                return FileList;
            }
            catch
            {
                foreach (var File in FileList)
                {
                    File.Name = File.Name.Split('.')[0];
                    File.NewName = File.NewName.Split('.')[0];
                }
                return FileList;
            }
        }
    }
}
