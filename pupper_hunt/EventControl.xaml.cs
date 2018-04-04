using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pupper_hunt
{
    /// <summary>
    /// Interaction logic for EventControl.xaml
    /// </summary>
    public partial class EventControl : UserControl
    {
        public Event EventRef;

        public EventControl()
        {
            InitializeComponent();
        }

        public void Initialize(Event e)
        {
            EventRef = e;
            EventImage.Source = EventRef.EventImageSource;
            EventMonth.Text = Event.IntToMonth(EventRef.EventTime.Month);
            EventDay.Text = EventRef.EventTime.Day.ToString();
            EventName.Text = EventRef.EventName;
            EventLocation.Text = EventRef.EventLocation;
            EventAttendance.Text = 
                (EventRef.EventAttendees.Count + 1 /* +1 for host*/).ToString() 
                + (EventRef.EventAttendees.Count > 0 ? " people " : " person ")
                + "attending!";
        }

        private void EventImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance().NavigateToEventScreen(EventRef);
        }


    }
}
