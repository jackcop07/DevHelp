using Microsoft.AspNetCore.Components.WebView.Maui;
using Microsoft.Extensions.Configuration;
using MudBlazor.Services;

namespace DevHelp;

public static class MauiProgram
{
	public static MauiApp CreateMauiAppAsync()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.RegisterBlazorMauiWebView()
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddBlazorWebView();
		builder.Services.AddMudServices();


		return builder.Build();
	}
}
