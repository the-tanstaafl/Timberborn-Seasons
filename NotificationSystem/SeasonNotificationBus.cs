using System;
using FloodSeason.Weather;
using Timberborn.EntitySystem;
using UnityEngine;

namespace FloodSeason.NotificationSystem;

public class SeasonNotificationBus
{
    private readonly SeasonCycleTrackerService _seasonCycleTrackerService;

    public event EventHandler<CustomNotificationEventArgs> NotificationPosted;
    
    public SeasonNotificationBus(SeasonCycleTrackerService seasonCycleTrackerService)
    {
        _seasonCycleTrackerService = seasonCycleTrackerService;
    }
    
    public void Post(string description, GameObject subject)
    {
        Guid entityId = subject.GetComponent<EntityComponent>().EntityId;
        var notification = new CustomNotification(description, entityId, _seasonCycleTrackerService.Year, _seasonCycleTrackerService.Month, _seasonCycleTrackerService.Day);
        EventHandler<CustomNotificationEventArgs> notificationPosted = NotificationPosted;
        if (notificationPosted == null)
            return;
        notificationPosted(this, new CustomNotificationEventArgs(notification));
    }
}