using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    class FileDownloader : IFileDownloader // used with InstallerHelper
    {
        public void DownloadFile(string url, string path)
        {
            var client = new WebClient();
            client.DownloadFile(url, path); // All this file knows about it is that it downloads some file from url in the path, 
            //So, we don't get those high parameters like customer and installer name.
        }
    }
}
