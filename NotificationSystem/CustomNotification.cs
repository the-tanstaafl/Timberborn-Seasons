using System;

namespace FloodSeason.NotificationSystem;

public class CustomNotification
{
    public string Description { get; }
    public int Year { get; }
    public int Month { get; }
    public int Day { get; }

    public CustomNotification(string description,Guid subject ,int year, int month, int day)
    {
        Description = description;
        Year = year;
        Month = month;
        Day = day;
    }
}