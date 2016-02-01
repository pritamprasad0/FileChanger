using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

using Microsoft.Win32;


namespace FileChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Forms.FolderBrowserDialog openDialog = 
            new System.Windows.Forms.FolderBrowserDialog();

        private string folderName =null;
        

        public MainWindow()
        {
            InitializeComponent();

            
            //Add code here

        }

        private void SourceButton_Click(object sender, RoutedEventArgs e)
        {
           var result = openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var textfromDialog = openDialog.SelectedPath;
                SourceTextBox.Text = textfromDialog;
                folderName = Path.GetFileName(SourceTextBox.Text);
            }
        }

        private void DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            DestinationTextBox.Foreground = Brushes.Black;
            var result = openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DestinationTextBox.Text = openDialog.SelectedPath;
            }
        }

        private void MakeJunctionButton_Click(object sender, RoutedEventArgs e)
        {
            var str = new StringBuilder();
            str.Append("/C mklink /J ");
            str.Append("\"");
            str.Append(DestinationTextBox.Text);
            str.Append("\\");
            str.Append(folderName);
            str.Append("\" ");
            str.Append("\"");
            str.Append(SourceTextBox.Text);
            str.Append("\"");
            //var sourcePath = SourceTextBox.Text;
            //var destinationPath = DestinationTextBox.Text +"\\"+folderName;
            //var command = "/C mklink /J " + '"'+destinationPath+'"'+ " " + '"'+sourcePath+'"';
            Process.Start("cmd", str.ToString());
        }

        private void Junction_Loaded(object sender, RoutedEventArgs e)
        {
            DestinationTextBox.Foreground = Brushes.Gray;
            DestinationTextBox.Text = @"D:\OneDrive";
        }

        
    }
}
