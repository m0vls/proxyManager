using proxyManager.Services.Interfaces;

public class AndroidPermissionRequesterService : IPermissionRequesterService
{
    protected static async Task CheckNotificationPermission()
    {
        if (OperatingSystem.IsAndroidVersionAtLeast(33))
        {
            var status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();
            if (status != PermissionStatus.Granted)
            {
                await Permissions.RequestAsync<Permissions.PostNotifications>();
            }
        }
    }
    public async Task RequesterRequiredPermissions()
    {
        await CheckNotificationPermission();
    }
}
