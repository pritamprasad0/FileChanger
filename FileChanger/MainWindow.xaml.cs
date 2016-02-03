using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Media;


namespace FileChanger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private readonly System.Windows.Forms.FolderBrowserDialog _openDialog = 
            new System.Windows.Forms.FolderBrowserDialog();

        private string _folderName;
        

        public MainWindow()
        {
            InitializeComponent();

            
            //Add code here

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

        
    }
}
