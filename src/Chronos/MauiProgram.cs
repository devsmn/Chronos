using Chronos.Data.SQLite;
using Chronos.Data.SQLite.Provider;
using Chronos.Data.SQLite.Provider.Content.DataStore;
using Chronos.Data.Stores;
using Chronos.DataModel;
using Chronos.DataModel.Core;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Reflection;

namespace Chronos.UI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif
        new SQLiteProvider().Initialize();

        TaskHelper.RunAsSync(() => DataStore.InitializeAsync());

        return builder.Build();
	}
}
