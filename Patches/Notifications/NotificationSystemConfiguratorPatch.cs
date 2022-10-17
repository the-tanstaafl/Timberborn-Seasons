namespace FloodSeason.Patches.Notifications;

/*[HarmonyPatch(typeof(NotificationSystemConfigurator), nameof(NotificationSystemConfigurator.Configure))]
public class NotificationSystemConfiguratorPatch
{
    static bool Prefix(IContainerDefinition containerDefinition)
    {
        containerDefinition.Bind<SeasonNotificationBus>().AsSingleton();
        //containerDefinition.Bind<NotificationObjectSerializer>().AsSingleton();
        //containerDefinition.Bind<NotificationSaver>().ToInstance(GetInstanceFromPrefab<NotificationSaver>());
        return false;
    }
}*/