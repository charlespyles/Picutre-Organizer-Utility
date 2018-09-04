using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using WinForms = System.Windows.Forms;
using System.Threading;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        System.Windows.Forms.FolderBrowserDialog fbd_srcfolder = new System.Windows.Forms.FolderBrowserDialog();
        System.Windows.Forms.FolderBrowserDialog fbd_dstfolder = new System.Windows.Forms.FolderBrowserDialog();
        Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();

        private void submitButton_Click(object sender, RoutedEventArgs e)
        {
            fbd_srcfolder.ShowNewFolderButton = true;
            DialogResult result = fbd_srcfolder.ShowDialog();
            if (result.ToString() == "OK")
            {
                txtbx_source.Text = fbd_srcfolder.SelectedPath;
            }            
        }

        private void submitButton2_Click(object sender, RoutedEventArgs e)
        {
            ofd.Title = "Open File List";
            ofd.ValidateNames = false;
            ofd.CheckFileExists = false;
            ofd.CheckPathExists = false;
            ofd.InitialDirectory = @"C:\";
            if (ofd.ShowDialog() == true)
            {
                txtbx_FileList.Text = ofd.FileName;
                txtbx_display.Text = File.ReadAllText(ofd.FileName);
            }
        }

        private void submitButton3_Click(object sender, RoutedEventArgs e)
        {
            fbd_dstfolder.ShowNewFolderButton = true;
            DialogResult result = fbd_dstfolder.ShowDialog();
            if (result.ToString() == "OK")
            {
                txtbx_destfolder.Text = fbd_dstfolder.SelectedPath;
            }
        }

        private void btn_Run_Click(object sender, RoutedEventArgs e)
        {
            string filename = txtbx_FileList.Text;
            //System.Windows.MessageBox.Show(filename);
            string srcfolder = txtbx_source.Text;
            //System.Windows.MessageBox.Show(srcfolder);
            string dstfolder = txtbx_destfolder.Text;
            //System.Windows.MessageBox.Show(dstfolder);
            pb_LengthyTaskProgress.Visibility = Visibility.Visible;
            lbl_Progress.Visibility = Visibility.Visible;

            string sourceFile;
            string destFile;
            if (System.IO.Directory.Exists(srcfolder))
            {
                lbl_Progress.Text = "In Progress...";   
                pb_LengthyTaskProgress.IsIndeterminate = true;
                string[] lines = File.ReadAllLines(@filename);
                foreach (string data in lines)
                {
                    sourceFile = System.IO.Path.Combine(srcfolder, data);
                    destFile = System.IO.Path.Combine(dstfolder, data);

                    System.IO.File.Copy(sourceFile, destFile, true);
                    //System.Windows.MessageBox.Show(line);
                }

                System.Windows.MessageBox.Show("File Transfer Complete!");
                lbl_Progress.Text = "Task Complete!";
                pb_LengthyTaskProgress.IsIndeterminate = false;
            }
            else
            {
                lbl_Progress.Text = "ERROR!";
                System.Windows.MessageBox.Show("Source Path does not exist");
            }

            pb_LengthyTaskProgress.Visibility = Visibility.Hidden;
            lbl_Progress.Visibility = Visibility.Hidden;

        }


        //Save for Later
        
    }
}
