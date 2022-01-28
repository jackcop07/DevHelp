using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelp.Pages.MarkDown
{
    public partial class MarkDown
    {
        private string input;

        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;
                CreateMarkdown();
            }
        }

        public string Output { get; set; }

        public MarkDown()
        {

        }


        private void CreateMarkdown()
        {
            Output = Markdown.ToHtml(Input);

        }
    }
}
