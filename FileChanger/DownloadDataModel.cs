﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileChanger
{
    public class DownloadDataModel
    {
        public string FileName { get; set; }
        public string Url { get; set; }
        public string FileSize { get; set; }

        public static readonly string Emp3Website = "emp3world";

        public static string Mp3skullWebsite = "mp3skull";
    }
}
