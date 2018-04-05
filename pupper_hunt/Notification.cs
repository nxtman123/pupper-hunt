using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pupper_hunt
{
    public abstract class Notification
    {
        public static List<Notification> Notifications = new List<Notification>();

        public ImageSource ImageSource { get; protected set; }
        public string Content { get; protected set; }
        public DateTime Date { get; protected set; }
        protected Notification(ImageSource imageSource, string content)
        {
            ImageSource = imageSource;
            Content = content;
            Date = DateTime.Now;
        }

        public abstract void OnClick(object sender, EventArgs e);

        public static void PostNotification(Notification notification)
        {
            Notifications.Add(notification);
        }
    }

    public class DogNotification : Notification
    {
        private Account mOwner;
        public DogNotification(Account owner, DogProfile dogProfile)
            : base(dogProfile.ImageSource, owner.Profile.Name + " got a new pupper " + dogProfile.Name + " !")
        {
            mOwner = owner;
        }

        public override void OnClick(object sender, EventArgs e)
        {
            MainWindow main = MainWindow.Instance();
            main.NavigateToProfileScreen(mOwner);
        }
    }

    public class NewUserNotification : Notification
    {
        private Account mUser;
        public NewUserNotification(Account user)
            : base(user.Profile.ImageSource, user.Profile.Name + " just joined PupperHunt! Say hi :)!")
        {
            mUser = user;
        }

        public override void OnClick(object sender, EventArgs e)
        {
            MainWindow main = MainWindow.Instance();
            main.NavigateToProfileScreen(mUser);
        }
    }

    public class NewEventNotification : Notification
    {
        private Event mEvent;
        public NewEventNotification(Event e)
            : base(e.EventImageSource, e.EventCreator.Profile.Name + " just created a new event. Check it out!")
        {
            mEvent = e;
        }

        public override void OnClick(object sender, EventArgs e)
        {
            MainWindow main = MainWindow.Instance();
            main.NavigateToEventScreen(mEvent);
        }
    }

    public class GoingToEventNotification : Notification
    {
        private Event mEvent;
        public GoingToEventNotification(Account account, Event e)
            : base(e.EventImageSource, account.Profile.Name + " is going to " + e.EventName + "!")
        {
            mEvent = e;
        }

        public override void OnClick(object sender, EventArgs e)
        {
            MainWindow main = MainWindow.Instance();
            main.NavigateToEventScreen(mEvent);
        }
    }
}
