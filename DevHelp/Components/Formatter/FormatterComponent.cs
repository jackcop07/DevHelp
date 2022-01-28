using DevHelp.Enums;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace DevHelp.Components.Formatter
{
    public partial class FormatterComponent
    {
        private string input = string.Empty;

        [Inject] ISnackbar Snackbar { get; set; }
        [Parameter] public FormatterEnum FormatterType { get; set; }
        public string Output { get; set; } = string.Empty;


        public string Input
        {
            get
            {
                return input;
            }
            set
            {
                input = value;
                FormatDisabled = string.IsNullOrEmpty(Input) ? true : false;
                if (FormatDisabled) Output = string.Empty;
                StateHasChanged();
            }
        }
        public bool FormatDisabled { get; set; } = true;


        private void Format()
        {
            switch (FormatterType)
            {
                case FormatterEnum.Xml:
                    FormatXml();
                    break;
                case FormatterEnum.Json:
                    FormatJson();
                    break;
                default:
                    break;
            }

        }

        private void ClearInput()
        {
            Input = string.Empty;
        }

        private void CopyText()
        {
            if (!string.IsNullOrEmpty(Output))
            {
                Clipboard.SetTextAsync(Output);
                Snackbar.Add("JSON Copied", Severity.Info);
            }

        }

        private void FormatJson()
        {
            try
            {
                var options = new JsonSerializerOptions()
                {
                    WriteIndented = true
                };

                var jsonElement = JsonSerializer.Deserialize<JsonElement>(Input);

                Output = JsonSerializer.Serialize(jsonElement, options);
            }
            catch (JsonException)
            {
                Snackbar.Add("Invalid JSON", Severity.Error);
            }
        }

        private void FormatXml()
        {
            try
            {
                var stringBuilder = new StringBuilder();

                var element = XElement.Parse(Input);

                var settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Indent = true;
                settings.NewLineOnAttributes = true;

                using (var xmlWriter = XmlWriter.Create(stringBuilder, settings))
                {
                    element.Save(xmlWriter);
                }

                Output = stringBuilder.ToString();
            }
            catch (XmlException)
            {
                Snackbar.Add("Invalid XML", Severity.Error);
            }
        }
    }
}
