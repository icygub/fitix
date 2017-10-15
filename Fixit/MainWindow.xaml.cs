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
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            fbd.Description = "Select Path";
            if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                LoadFiles(fbd.SelectedPath);
            }
        }

        private void FilesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string myPicture = "pack://application:,,,/File Renamer;component/Image/NoImage.jpg";
            if (FilesListBox.Items.Count != 0)
            {
                myPicture = (txtPath.Text + "\\" + FilesListBox.SelectedItem.ToString());
            }
            LoadImage(myPicture);
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
            FilesListBox.Items.Clear();
            string supportedExtensions = "*.jpg,*.gif,*.png,*.bmp,*.jpe,*.jpeg,*.tif,*.tiff";
            foreach (string imageFile in Directory.GetFiles(path).Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower())))
            {
                FilesListBox.Items.Add(Path.GetFileName(imageFile));
            }
        }

        private void DefaultPath_Click(object sender, RoutedEventArgs e)
        {
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings["Path"].Value = txtPath.Text;
            config.Save(ConfigurationSaveMode.Modified);
        }

        private void LoadImage(string path)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(path);
                bitmap.EndInit();
                PreviewImage.Stretch = Stretch.Fill;
                PreviewImage.Source = bitmap;
            }
            catch
            {
                /*BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("pack://application:,,,/File Renamer;component/Image/InvalidFile.jpg");
                bitmap.EndInit();
                PreviewImage.Stretch = Stretch.Fill;
                PreviewImage.Source = bitmap;*/
            }

        }
    }

}

