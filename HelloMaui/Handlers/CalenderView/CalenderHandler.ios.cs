#if IOS

using Foundation;
using HelloMaui.Views;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using ObjCRuntime;
using UIKit;

namespace HelloMaui.Handlers.CalenderView;

public partial class CalendarHandler : ViewHandler<ICalendarView, UICalendarView>, IDisposable
{
    private UICalendarSelection? _calendarSelection;

    protected override UICalendarView CreatePlatformView()
    {
        return new UICalendarView();
    }

    protected override void ConnectHandler(UICalendarView platformView)
    {
        base.ConnectHandler(platformView);
        _calendarSelection = new UICalendarSelectionSingleDate(new CalenderSelectionSingleDateDelegate(VirtualView));
    }

    private static void MapFirstDayOfWeek(CalendarHandler handler, ICalendarView virtualView)
    {
        handler.PlatformView.Calendar.FirstWeekDay = (UIntPtr)virtualView.FirstDayOfWeek;
    }

    private static void MapMinDate(CalendarHandler handler, ICalendarView virtualView)
    {
        SetDateRange(handler, virtualView);
    }

    private static void MapMaxDate(CalendarHandler handler, ICalendarView virtualView)
    {
        SetDateRange(handler, virtualView);
    }

    private static void MapSelectedDate(CalendarHandler handler, ICalendarView virtualView)
    {
        if (handler._calendarSelection is UICalendarSelectionSingleDate calendarSelection)
        {
            MapSingleDateSelection(calendarSelection, virtualView);
        }
    }

    private static void MapSingleDateSelection(UICalendarSelectionSingleDate calendarSelection,
        ICalendarView virtualView)
    {
        if (calendarSelection is null)
        {
            calendarSelection.SetSelectedDate(null, true);
        }

        calendarSelection.SetSelectedDate(new NSDateComponents()
        {
            Day = virtualView.SelectedDate.Value.Day,
            Month = virtualView.SelectedDate.Value.Month,
            Year = virtualView.SelectedDate.Value.Year
        }, true);
    }

    static void SetDateRange(CalendarHandler handler, ICalendarView virtualView)
    {
        var fromDateComponents = virtualView.MinDate.Date.ToNSDate();
        var toDateComponents = virtualView.MaxDate.Date.ToNSDate();

        var calenderViewDateRange = new NSDateInterval(fromDateComponents, toDateComponents);
        handler.PlatformView.AvailableDateRange = calenderViewDateRange;
    }
    // protected override void DisconnectHandler(UICalendarView platformView)
    // {
    //     base.DisconnectHandler(platformView);
    //     _calendarSelection?.Dispose();
    //     _calendarSelection = null;
    // }

    private void ReleaseUnmanagedResources()
    {
        // DisconnectHandler(PlatformView);
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

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~CalendarHandler()
    {
        Dispose(false);
    }


    sealed class CalenderSelectionSingleDateDelegate(ICalendarView calendarView)
        : IUICalendarSelectionSingleDateDelegate
    {
        public NativeHandle Handle { get; }

        public void Dispose()
        {
        }

        public void DidSelectDate(UICalendarSelectionSingleDate calenderSelection, NSDateComponents? date)
        {
            calenderSelection.SelectedDate = date;
            calendarView.SelectedDate = date?.Date.ToDateTime();
            calendarView.OnSelectedDateChanged(date?.Date.ToDateTime());
        }
    }
}


#endif