using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tesseract;

namespace DevHelp.Pages.Ocr
{
    public partial class DropZone : IAsyncDisposable
    {
        [Inject] ISnackbar Snackbar { get; set; }
        [Inject] IJSRuntime JSRuntime { get; set; }
        public string Output { get; set; }

        ElementReference dropZoneElement;
        InputFile inputFile;

        IJSObjectReference _module;
        IJSObjectReference _dropZoneInstance;



        private void FindText(byte[] input)
        {
            using (var engine = new TesseractEngine(@"C:\Repos\DevHelp\DevHelp\Tesseract", "eng"))
            {
                engine.SetVariable("user_defined_dpi", "70"); //set dpi for supressing warning
                using var img = Pix.LoadFromMemory(input);
                using var page = engine.Process(img);
                Output = page.GetText();
            }
        }

        private void CopyText()
        {
            if (!string.IsNullOrEmpty(Output))
            {
                Clipboard.SetTextAsync(Output);
                Snackbar.Add("Text Copied", Severity.Info);
            }

        }

        

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                // Load the JS file
                _module = await JSRuntime.InvokeAsync<IJSObjectReference>("import", "./javascript/DropZone.js");

                // Initialize the drop zone
                _dropZoneInstance = await _module.InvokeAsync<IJSObjectReference>("initializeFileDropZone", dropZoneElement, inputFile.Element);
            }
        }

        // Called when a new file is uploaded
        async Task OnChange(InputFileChangeEventArgs e)
        {
            using var stream = e.File.OpenReadStream();
            using var ms = new MemoryStream();
            await stream.CopyToAsync(ms);

            var msArray = ms.ToArray();

            FindText(msArray);
        }

        // Unregister the drop zone events
        public async ValueTask DisposeAsync()
        {
            if (_dropZoneInstance != null)
            {
                //await _dropZoneInstance.InvokeVoidAsync("dispose");
                await _dropZoneInstance.DisposeAsync();
            }

            if (_module != null)
            {
                await _module.DisposeAsync();
            }
        }
    }
}
