namespace FloodSeason.NotificationSystem;

public class CustomNotificationEventArgs
{
    public CustomNotification Notification { get; }

    public CustomNotificationEventArgs(CustomNotification notification)
    {
        Notification = notification;
    }
}