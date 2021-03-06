﻿using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using Clipboard = System.Windows.Clipboard;
using Control = System.Windows.Controls.Control;


namespace FileChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly FolderBrowserDialog _openDialog = 
            new FolderBrowserDialog();

        private string _folderName;
        

        public MainWindow()
        {
            InitializeComponent();

            

            

        }

        /// <summary>
        /// Event Wwhen Source Button Is clicked in Junction tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SourceButton_Click(object sender, RoutedEventArgs e)
        {
           var result = _openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                var textfromDialog = _openDialog.SelectedPath;
                SourceTextBox.Text = textfromDialog;
                _folderName = Path.GetFileName(SourceTextBox.Text);
            }
        }

        /// <summary>
        /// Event when Destination Button is clicked in Junction Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DestinationButton_Click(object sender, RoutedEventArgs e)
        {
            DestinationTextBox.Foreground = Brushes.Black;
            var result = _openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                DestinationTextBox.Text = _openDialog.SelectedPath;
            }
        }

        /// <summary>
        /// Event when Make junction button is clicked in Junction Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MakeJunctionButton_Click(object sender, RoutedEventArgs e)
        {
            var str = new StringBuilder();
            str.Append("/C mklink /J ");
            str.Append("\"");
            str.Append(DestinationTextBox.Text);
            str.Append("\\");
            str.Append(_folderName);
            str.Append("\" ");
            str.Append("\"");
            str.Append(SourceTextBox.Text);
            str.Append("\"");
            Process.Start("cmd", str.ToString());
        }

        /// <summary>
        /// Event when Junction Tab is Loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Junction_Loaded(object sender, RoutedEventArgs e)
        {
            DestinationTextBox.Foreground = Brushes.Gray;
            DestinationTextBox.Text = @"D:\OneDrive";
        }

        /// <summary>
        /// Event when Browse Button is clicked in Hide Tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HideBrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var result = _openDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                HideTextBox.Text = _openDialog.SelectedPath;
            }
        }

       private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            SearchAndProcess.GetResults(DownloadTextBox.Text);
        }

        private void DownloadButton_Click(object sender, RoutedEventArgs e)
        {
            //SearchAndProcess.DownloadSong(DownloadListBox.SelectedItem.ToString());
            var model = (DownloadDataModel)DownloadDataGrid.SelectedItem;
            SearchAndProcess.DownloadSong(model.Url);
        }

        private void Row_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DownloadButton_Click(sender, e);
        }

        private void DownloadTab_Loaded(object sender, RoutedEventArgs e)
        {
            //var dataGridRowStyle = new Style(typeof(DataGridRow));
            //dataGridRowStyle.Setters.Add(new EventSetter(MouseDoubleClickEvent, new MouseButtonEventHandler(DownloadDataGrid_MouseDoubleClick)));
            //DownloadDataGrid.RowStyle = dataGridRowStyle;

            
            DownloadProgrssBar.Visibility = Visibility.Hidden;
            DownloadingFileLabel.Visibility = Visibility.Hidden;

            AddDataToListBox();

        }

        private void AddDataToListBox()
        {
            WebsiteComboBox.Items.Add("emp3world");
            WebsiteComboBox.Items.Add("Mp3Skull");
            WebsiteComboBox.Items.Add("Temp");
            WebsiteComboBox.SelectedIndex = 0;
        }

        private void DownloadTextBox_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DownloadTextBox.Text = Clipboard.GetText();
        }

        private void TextBox_KeyEnterUpdate(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SearchAndProcess.GetResults(DownloadTextBox.Text);
            }
        }


    }
}
