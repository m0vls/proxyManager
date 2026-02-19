using Microsoft.Extensions.Logging;
using proxyManager.Platforms.Android;
using proxyManager.Services.Implementations;
using proxyManager.Services.Interfaces;

namespace proxyManager
{
    public static class MauiProgram
    {
        // Добавляем конфиг(и) в DI контейнер
        private static void AddConfigs(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<Config>();
        }
        // Добавляем сервисы в DI контейнер
        private static void AddServices(MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IVpnService, AndroidVpnService>();
            builder.Services.AddSingleton<IPermissionRequesterService, AndroidPermissionRequesterService>();
        }
        // Добавляем ViewModels в DI контейнер
        private static void AddViewModels(MauiAppBuilder builder)
        {
            //builder.Services.AddTransient<MainPage>();
        }
        // Добавляем страницы в DI контейнер
        private static void AddPages(MauiAppBuilder builder)
        {
            builder.Services.AddTransient<MainPage>();
        }
        // Добавляем DEBUG/Logging в DI контейнер
        private static void AddDebug(MauiAppBuilder builder)
        {
            builder.Logging.AddDebug();
        }

        private static void ApplyGlobalApplicationExceptionsHandling()
        {

            Android.Runtime.AndroidEnvironment.UnhandledExceptionRaiser += (s, e) =>
            {
                if (e.Exception is ApplicationException ex)
                {
                    // тут показываем ошибку, т.д.
                    e.Handled = true;
                }
                else
                {
                    e.Handled = false;
                }
            };
        }


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

            AddConfigs(builder);
            AddServices(builder);
            AddViewModels(builder);
            AddPages(builder);

#if DEBUG
            AddDebug(builder);
#endif

            ApplyGlobalApplicationExceptionsHandling();

            return builder.Build();
        }

    }

}
