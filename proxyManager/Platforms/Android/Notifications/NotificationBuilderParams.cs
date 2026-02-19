using Android.App;

namespace proxyManager.Platforms.Android.Notifications;

public class NotificationBuilderParams
{
    public string ChannelId { get; set; } = "vpn_service_channel";
    public string ChannelName { get; set; } = "VPN Connection Status";
    public NotificationImportance Importance { get; set; } = NotificationImportance.Low;
    public string Description { get; set; } = "Уведомление о работе прокси-менеджера";

    public string ContentTitle { get; set; } = "Proxy Manager";
    public string ContentText { get; set; } = "VPN запущен и фильтрует трафик";
    public int SmallIcon { get; set; } = Resource.Drawable.ic_arrow_back_black_24;
    public bool Ongoing { get; set; } = true;
    public Func<Notification.Builder, Notification.Builder>? BuilderSetupFunc { get; set; } = null;
}
