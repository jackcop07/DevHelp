using DevHelp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelp.Pages
{
    public partial class Index
    {
        public List<DashboardItem> DashboardItems { get; }

        public Index()
        {
            DashboardItems = new List<DashboardItem>
            {
                new()
                {
                    Title = "Spotify",
                    Icon = "fab fa-spotify",
                    Link = "https://open.spotify.com/"
                },
                new()
                {
                    Title = "Google",
                    Icon = "fab fa-google",
                    Link = "https://www.google.co.uk"
                },
                new()
                {
                    Title = "GitHub",
                    Icon = "fab fa-github",
                    Link = "https://github.com/"
                },
                new()
                {
                    Title = "Azure",
                    Icon = "fab fa-windows",
                    Link = "https://portal.azure.com/#home"
                },
                new()
                {
                    Title = "",
                    Icon = "",
                    Link = ""
                },
                new()
                {
                    Title = "",
                    Icon = "",
                    Link = ""
                },
                new()
                {
                    Title = "",
                    Icon = "",
                    Link = ""
                },
                new()
                {
                    Title = "",
                    Icon = "",
                    Link = ""
                },
            };
        }

        private void StartSite(string link)
        {
            Process.Start(@"C:\Program Files\Google\Chrome\Application\chrome.exe", @$"{link}");
        }
    }
}
