using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HtmlAgilityPack;
using Microsoft.VisualBasic;

namespace FileChanger
{
    internal static class SearchAndProcess
    {
        public static void GetResults(string searchquery)
        {
            if (((MainWindow) Application.Current.MainWindow).WebsiteComboBox.Text == DownloadDataModel.Emp3Website)
            {
                DownloadFromemp3World(searchquery);
            }
            else if (((MainWindow)Application.Current.MainWindow).WebsiteComboBox.Text == DownloadDataModel.Mp3skullWebsite);
            {
                DownloadFromMp3Skull(searchquery);
            }
        }

        private static void DownloadFromMp3Skull(string searchquery)
        {
        }

        private static void DownloadFromemp3World(string searchquery)
        {
            ((MainWindow) Application.Current.MainWindow).DownloadDataGrid.Items.Clear();
            var resultsBuffer = new byte[8192];
            var searchString = new StringBuilder();
            searchString.Append("http://emp3world.biz/search/");
            searchString.Append(searchquery.Trim().Replace(" ", "_"));
            searchString.Append("_mp3_download.html");

            var request = (HttpWebRequest) WebRequest.Create(searchString.ToString());
            var response = (HttpWebResponse) request.GetResponse();
            var resultStream = response.GetResponseStream();

            var sb = new StringBuilder();
            var count = 0;

            do
            {
                if (resultStream != null)
                    count = resultStream.Read(resultsBuffer, 0, resultsBuffer.Length);
                if (count != 0)
                {
                    var tempString = Encoding.ASCII.GetString(resultsBuffer, 0, count);
                    sb.Append(tempString);
                }
            } while (count > 0);

            var webPageSource = sb.ToString();
            var html = new HtmlDocument {OptionOutputAsXml = true};
            html.LoadHtml(webPageSource);
            var doc = html.DocumentNode;
            var divResultBox = doc.SelectSingleNode("//div[@id='results_box']");
            var songDivs = divResultBox.SelectNodes(".//div[@class='song_item']");
            foreach (var songsDiv in songDivs)
            {
                var linkDiv = songsDiv.SelectSingleNode(".//a[@class='btn' and @rel='nofollow']");
                var nameDiv = songsDiv.SelectSingleNode(".//span[@id='song_title']");
                var sizeDiv = songsDiv.SelectSingleNode(".//div[@class='song_size']");
                var songName = nameDiv.InnerText;
                if (songName == string.Empty)
                {
                    songName = "Unknown";
                }
                var size = sizeDiv.InnerText.Trim();
                if (size == string.Empty)
                {
                    size = "Unknown";
                }
                var uri = linkDiv.GetAttributeValue("href", "Not Found");
                if (uri.Contains(".mp3"))
                {
                    ((MainWindow)Application.Current.MainWindow).DownloadDataGrid.Items.Add(new DownloadDataModel(){FileName = songName,Url = uri,FileSize = size});
                }
                
            }
        }

        public static void DownloadSong(string selectedItem)
        {
            var uri = new Uri(selectedItem);
            var name = Interaction.InputBox("Enter Filename", "Save File As", "Unknown");
            if (name == string.Empty || name == null)
            {
                name = Path.GetFileName(uri.AbsolutePath);
            }
            else
            {
                name = name + ".mp3";
            }

            ((MainWindow) Application.Current.MainWindow).FileNameLabel.Content = name;
            ((MainWindow) Application.Current.MainWindow).DownloadingFileLabel.Visibility = Visibility.Visible;
            ((MainWindow) Application.Current.MainWindow).DownloadProgrssBar.Visibility = Visibility.Visible;
            var client = new WebClient();
            client.DownloadProgressChanged += DownloadFileProgressChanged;
            client.DownloadFileCompleted += DownloadCompleted;

            client.DownloadFileAsync(uri, name);
        }

        private static void DownloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            var result = MessageBox.Show("Download Completed", "", MessageBoxButton.OK);
            if (result == MessageBoxResult.OK)
            {
                ((MainWindow) Application.Current.MainWindow).DownloadProgrssBar.Value = 0;
                ((MainWindow) Application.Current.MainWindow).FileNameLabel.Content = "";
                ((MainWindow) Application.Current.MainWindow).DownloadingFileLabel.Visibility = Visibility.Hidden;
                ((MainWindow) Application.Current.MainWindow).DownloadProgrssBar.Visibility = Visibility.Hidden;
            }
        }


        private static void DownloadFileProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            ((MainWindow) Application.Current.MainWindow).DownloadProgrssBar.Value = e.ProgressPercentage;
        }
    }
}