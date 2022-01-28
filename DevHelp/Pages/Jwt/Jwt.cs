using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevHelp.Pages.Jwt
{
    public partial class Jwt
    {
        private string input = string.Empty;

        [Inject] ISnackbar Snackbar { get; set; }
        public string Header { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;

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
                if (FormatDisabled)
                {
                    Header = string.Empty;
                    Body = string.Empty;
                }

                StateHasChanged();
            }
        }
        public bool FormatDisabled { get; set; } = true;


        private void ClearInput()
        {
            Input = string.Empty;
        }

        private void Decode()
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(Input);

                Header = "{\n";
                foreach (var element in token.Header)
                {
                    Header += $"\t{element.Key} : {element.Value}\n";
                }
                Header += "}";


                Body = "{\n";
                foreach (var element in token.Payload)
                {
                    Body += $"\t{element.Key} : {element.Value}\n";
                }
                Body += "}";
            }
            catch (Exception)
            {
                Snackbar.Add("Invalid JWT", Severity.Error);
            }
        }

    }
}
