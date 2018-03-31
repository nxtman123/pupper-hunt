using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pupper_hunt
{
    public class Event
    {
        private static readonly uint NUM_EVENT_PICTURES = 5;
        private static uint NumEventsCreated = 0;
        public Account EventCreator { get; private set; }
        public ImageSource EventImageSource { get; private set; }
        public string EventName { get; private set; }
        public string EventDescription { get; private set; }
        public string EventLocation { get; private set; }
        public DateTime EventTime { get; private set; }
        public HashSet<Account> EventAttendees { get; private set; }

        public Event(Account creator, ImageSource source, string name, string description, string location, DateTime time)
        {
            EventCreator = creator;
            EventImageSource = source;
            EventName = name;
            EventDescription = description;
            EventLocation = location;
            EventTime = time;
            EventAttendees = new HashSet<Account>();
            NumEventsCreated++;
        }

        public static ImageSource GetNextEventImage()
        {
            return ImageManager.GetImageSource("event" + ((NumEventsCreated % NUM_EVENT_PICTURES) + 1).ToString());
        }

        public void Attend(Account attendee)
        {
            if (!EventAttendees.Contains(attendee))
            {
                EventAttendees.Add(attendee);
            }
        }

        public bool IsAttending(Account account)
        {
            return EventAttendees.Contains(account);
        }

        public void RemoveAttendee(Account account)
        {
            EventAttendees.Remove(account);
        }

        public void CancelEvent()
        {
            foreach (Account attendee in EventAttendees)
            {
                attendee.RetractAttendance(this);
            }
            EventAttendees.Clear();
        }
    }
}
