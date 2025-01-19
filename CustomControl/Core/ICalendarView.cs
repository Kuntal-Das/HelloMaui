namespace CustomControl.Core;

public interface ICalendarView : IView
{
    DayOfWeek FirstDayOfWeek { get; set; }
    DateTimeOffset MinDate { get; set; }
    DateTimeOffset MaxDate { get; set; }
    DateTimeOffset? SelectedDate { get; set; }
    void OnSelectedDateChanged(DateTimeOffset? date);
}