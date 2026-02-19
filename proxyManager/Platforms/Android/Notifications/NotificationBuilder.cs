using Android.App;
using Android.Content;
using Android.OS;

namespace proxyManager.Platforms.Android.Notifications;

public static class NotificationBuilder
{
    private static Context context => Platform.AppContext;

    private static void CreateNotificationChannel(NotificationBuilderParams param)
    {
        // Каналы появились только в Android 8.0 (API 26)
        if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
        {
            var channelId = param.ChannelId;
            var channelName = param.ChannelName;
            var importance = param.Importance; // Чтобы не надоедать звуком

            var channel = new NotificationChannel(channelId, channelName, importance)
            {
                Description = param.Description
            };

            var notificationManager = context.GetSystemService(Context.NotificationService)
                as NotificationManager;

            if (notificationManager is null)
                throw new NotSupportedException(
                        $"Service \"NotificationService\" ({Context.NotificationService})" +
                        "does not exist in the current app context"
                        );

            notificationManager.CreateNotificationChannel(channel);
        }
    }
    private static Notification.Builder CreateNotificationBuilder(NotificationBuilderParams param)
    {
        var channelId = param.ChannelId;
        var notificationBuilder = new Notification.Builder(context, channelId)
            .SetContentTitle(param.ContentTitle)
            .SetContentText(param.ContentText)
            .SetSmallIcon(param.SmallIcon)
            .SetOngoing(param.Ongoing);
        if (param.BuilderSetupFunc is not null)
            notificationBuilder = param.BuilderSetupFunc(notificationBuilder);
        return notificationBuilder;
    }

    public static Notification BuildNotification(NotificationBuilderParams param)
    {
        CreateNotificationChannel(param);

        Notification.Builder notificationBuilder = CreateNotificationBuilder(param);
        return notificationBuilder.Build();
    }
}
