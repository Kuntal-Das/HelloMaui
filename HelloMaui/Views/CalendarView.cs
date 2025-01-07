namespace HelloMaui.Views;

public class CalendarView : View, ICalendarView
{
    public static BindableProperty FirstDayOfWeekProperty = BindableProperty.Create(nameof(FirstDayOfWeek),
        typeof(DayOfWeek), typeof(CalendarView), DayOfWeek.Monday);

    public static BindableProperty MinDateProperty =
        BindableProperty.Create(nameof(MinDate), typeof(DateTime), typeof(CalendarView), DateTime.MinValue);

    public static BindableProperty MaxDateProperty =
        BindableProperty.Create(nameof(MaxDate), typeof(DateTime), typeof(CalendarView), DateTime.MaxValue);

    public static BindableProperty SelectedDateProperty = BindableProperty.Create(nameof(SelectedDate),
        typeof(DateTime), typeof(CalendarView), DateTime.MinValue);

    public DayOfWeek FirstDayOfWeek
    {
        get => (DayOfWeek)GetValue(FirstDayOfWeekProperty);
        set => SetValue(FirstDayOfWeekProperty, value);
    }

    public DateTimeOffset MinDate
    {
        get => (DateTimeOffset)GetValue(MinDateProperty);
        set => SetValue(MinDateProperty, value);
    }

    public DateTimeOffset MaxDate
    {
        get => (DateTimeOffset)GetValue(MaxDateProperty);
        set => SetValue(MaxDateProperty, value);
    }

    public DateTimeOffset? SelectedDate
    {
        get => (DateTimeOffset?)GetValue(SelectedDateProperty);
        set => SetValue(SelectedDateProperty, value);
    }

    public event EventHandler<SelectedDateChangedEventArgs>? SelectedDateChanged;

    void ICalendarView.OnSelectedDateChanged(DateTimeOffset? selectedDate)
    {
        SelectedDateChanged?.Invoke(this, new SelectedDateChangedEventArgs(selectedDate));
    }
}

public class SelectedDateChangedEventArgs(DateTimeOffset? selectedDate) : EventArgs
{
    public DateTimeOffset? SelectedDate { get; } = selectedDate;
}