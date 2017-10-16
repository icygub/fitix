using System;
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

namespace Fixit {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow()
        {
            InitializeComponent();
            LoadFiles(ConfigurationManager.AppSettings["Path"]);
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
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
            FileListTable.Items.Clear();
            var Files = new List<iFixFile>();
            foreach (string imageFile in Directory.GetFiles(path))
            {
                Files.Add(new iFixFile(Path.GetFileName(imageFile), Path.GetFileName(imageFile)));
                //FileListTable.Items.Add(new iFixFile(Path.GetFileName(imageFile), Path.GetFileName(imageFile)));
                
            }
            FileListTable.ItemsSource = Files;
        }

        private void DefaultPath_Click(object sender, RoutedEventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings["Path"].Value = txtPath.Text;
            config.Save(ConfigurationSaveMode.Modified);
            MessageBox.Show("Default Path changed successfully.", "Confirmation", MessageBoxButton.OK);
        }

    }

}

class iFixFile
{
    public string Name { get; set; }
    public string NewName { get; set; }

    public iFixFile(string name, string newname)
    {
        this.Name = name;
        this.NewName = newname;
    }
}
