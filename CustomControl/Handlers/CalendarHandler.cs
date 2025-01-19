using CustomControl.Core;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
#if WINDOWS
using Microsoft.UI.Xaml.Controls;
using Calendar = Microsoft.UI.Xaml.Controls.CalendarView;
using DayOfWeek = Windows.Globalization.DayOfWeek;
#elif ANDROID
using Calendar = Android.Widget.CalendarView;
#elif IOS || MACCATALYST
using Foundation;
using ObjCRuntime;
using UIKit;
#endif

namespace CustomControl.Handlers;

public class CalendarHandler
#if WINDOWS
    : ViewHandler<ICalendarView, Calendar>
#elif ANDROID
    : ViewHandler<ICalendarView, Calendar>
#elif IOS || MACCATALYST
    : ViewHandler<ICalendarView, UICalendarView>, IDisposable
#endif
{
    public CalendarHandler(IPropertyMapper mapper, CommandMapper? commandMapper = null) : base(mapper, commandMapper)
    {
    }

    public CalendarHandler() : this(PropertyMapper, CommandMapper)
    {
    }

    public static IPropertyMapper<ICalendarView, CalendarHandler> PropertyMapper =
        new PropertyMapper<ICalendarView, CalendarHandler>(ViewMapper)
        {
            [nameof(ICalendarView.FirstDayOfWeek)] = MapFirstDayOfWeek,
            [nameof(ICalendarView.MinDate)] = MapMinDate,
            [nameof(ICalendarView.MaxDate)] = MapMaxDate,
            [nameof(ICalendarView.SelectedDate)] = MapSelectedDate,
        };

    public static CommandMapper<ICalendarView, CalendarHandler> CommandMapper = new(ViewCommandMapper);

#if WINDOWS
    protected override Calendar CreatePlatformView()
    {
        return new Calendar();
    }

    protected override void ConnectHandler(Calendar platformView)
    {
        base.ConnectHandler(platformView);
        platformView.SelectedDatesChanged += SelectedDatesChanged;
    }

    protected override void DisconnectHandler(Calendar platformView)
    {
        platformView.SelectedDatesChanged -= SelectedDatesChanged;
        base.DisconnectHandler(platformView);
    }

    static void MapFirstDayOfWeek(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.FirstDayOfWeek = (DayOfWeek)virtualView.FirstDayOfWeek;
    }

    static void MapMinDate(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.MinDate = virtualView.MinDate;
    }

    static void MapMaxDate(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.MaxDate = virtualView.MaxDate;
    }

    static void MapSelectedDate(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.SelectedDates.Clear();
        if (virtualView.SelectedDate is not null)
        {
            handler.PlatformView.SelectedDates.Add(virtualView.SelectedDate.Value);
            handler.PlatformView.SetDisplayDate(virtualView.SelectedDate.Value);
        }
    }

    void SelectedDatesChanged(Calendar sender, CalendarViewSelectedDatesChangedEventArgs args)
    {
        PlatformView.SelectedDatesChanged -= SelectedDatesChanged;

        if (args.AddedDates.Count == 0)
        {
            VirtualView.SelectedDate = null;
        }

        if (args.AddedDates.Count > 0)
        {
            VirtualView.SelectedDate = args.AddedDates[0];
        }

        VirtualView.OnSelectedDateChanged(VirtualView.SelectedDate);

        PlatformView.SelectedDatesChanged += SelectedDatesChanged;
    }

#elif ANDROID
    protected override Calendar CreatePlatformView()
    {
        return new Calendar(Context);
    }

    protected override void ConnectHandler(Calendar platformView)
    {
        base.ConnectHandler(platformView);
        platformView.DateChange += HandleDateChanged;
    }

    protected override void DisconnectHandler(Calendar platformView)
    {
        base.DisconnectHandler(platformView);
        platformView.DateChange -= HandleDateChanged;
    }

    static void MapFirstDayOfWeek(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.FirstDayOfWeek = (int)virtualView.FirstDayOfWeek;
    }

    static void MapMinDate(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.MinDate = virtualView.MinDate.ToUnixTimeMilliseconds();
    }

    static void MapMaxDate(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.MaxDate = virtualView.MaxDate.ToUnixTimeMilliseconds();
    }

    static void MapSelectedDate(CalendarHandler handler, ICalendarView virtualView)
    {
        if (virtualView.SelectedDate is null)
        {
            return;
        }

        handler.PlatformView.SetDate(virtualView.SelectedDate.Value.ToUnixTimeMilliseconds(), true, true);
    }

    void HandleDateChanged(object? sender, Calendar.DateChangeEventArgs e)
    {
        PlatformView.DateChange -= HandleDateChanged;

        VirtualView.SelectedDate = new DateTime(e.Year, e.Month + 1, e.DayOfMonth, 0, 0, 0);
        VirtualView.OnSelectedDateChanged(VirtualView.SelectedDate);

        PlatformView.DateChange += HandleDateChanged;
    }
#elif IOS || MACCATALYST
    UICalendarSelection? _calendarSelection;

    ~CalendarHandler()
    {
        Dispose(false);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected override UICalendarView CreatePlatformView()
    {
        return new UICalendarView();
    }

    protected override void ConnectHandler(UICalendarView platformView)
    {
        base.ConnectHandler(platformView);

        _calendarSelection = new UICalendarSelectionSingleDate(new CalendarSelectionSingleDateDelegate(VirtualView));
    }

    protected virtual void Dispose(bool disposing)
    {
        ReleaseUnmanagedResources();

        if (disposing)
        {
            _calendarSelection?.Dispose();
            _calendarSelection = null;
        }
    }

    static void MapSelectedDate(CalendarHandler handler, ICalendarView virtualView)
    {
        if (handler._calendarSelection is UICalendarSelectionSingleDate calendarSelection)
        {
            MapSingleDateSelection(calendarSelection, virtualView);
        }
    }

    static void MapSingleDateSelection(UICalendarSelectionSingleDate calendarSelection, ICalendarView virtualView)
    {
        if (virtualView.SelectedDate is null)
        {
            calendarSelection.SetSelectedDate(null, true);
            return;
        }

        calendarSelection.SetSelectedDate(new NSDateComponents
        {
            Day = virtualView.SelectedDate.Value.Day,
            Month = virtualView.SelectedDate.Value.Month,
            Year = virtualView.SelectedDate.Value.Year
        }, true);
    }


    static void MapFirstDayOfWeek(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.Calendar.FirstWeekDay = (nuint)virtualView.FirstDayOfWeek;
    }

    static void MapMinDate(CalendarHandler handler, ICalendarView virtualView)
    {
        SetDateRange(handler, virtualView);
    }

    static void MapMaxDate(CalendarHandler handler, ICalendarView virtualView)
    {
        SetDateRange(handler, virtualView);
    }

    static void SetDateRange(CalendarHandler handler, ICalendarView virtualView)
    {
        var fromDateComponents = virtualView.MinDate.Date.ToNSDate();
        var toDateComponents = virtualView.MaxDate.Date.ToNSDate();

        var calendarViewDateRange = new NSDateInterval(fromDateComponents, toDateComponents);
        handler.PlatformView.AvailableDateRange = calendarViewDateRange;
    }

    void ReleaseUnmanagedResources()
    {
        // TODO release unmanaged resources here
    }

    sealed class CalendarSelectionSingleDateDelegate(ICalendarView calendarView)
        : IUICalendarSelectionSingleDateDelegate
    {
        public NativeHandle Handle { get; }

        public void Dispose()
        {
        }

        public void DidSelectDate(UICalendarSelectionSingleDate calendarSelection, NSDateComponents? date)
        {
            calendarSelection.SelectedDate = date;
            calendarView.SelectedDate = date?.Date.ToDateTime();
            calendarView.OnSelectedDateChanged(date?.Date.ToDateTime());
        }
    }
#endif
}