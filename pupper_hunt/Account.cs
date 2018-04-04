using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pupper_hunt
{
    public class Account
    {
        public enum Type
        {
            DogOwner,
            BusinessOwner
        }

        public string AccountName { get; private set; }
        public string AccountPassword { get; private set; }
        public Type AccountType { get; private set; }
        public Profile Profile { get; protected set; }
        public List<Event> HostedEvents { get; protected set; }
        public List<Event> AttendingEvents { get; protected set; }

        protected Account(string accountName, string accountPassword, Type type)
        {
            AccountName = accountName;
            AccountPassword = accountPassword;
            AccountType = type;

            ImageSource source = null;
            if (type == Type.BusinessOwner)
            {
                source = ImageManager.GetImageSource("businessProfile");
            }
            else if (type == Type.DogOwner)
            {
                source = ImageManager.GetImageSource("dogOwnerProfile");
            }
            Profile = new Profile("", "", source);

            HostedEvents = new List<Event>();
            AttendingEvents = new List<Event>();
        }

        public void AttendEvent(Event toAttend)
        {
            AttendingEvents.Add(toAttend);
            toAttend.Attend(this);
        }

        public Event HostEvent(ImageSource image, string name, string description, string location, DateTime time)
        {
            Event hostedEvent = new Event(this, image, name, description, location, time);
            HostedEvents.Add(hostedEvent);
            return hostedEvent;
        }

        public void CancelHostedEvent(Event hostedEvent)
        {
            foreach (Account attendee in hostedEvent.EventAttendees)
            {
                attendee.RetractAttendance(hostedEvent);
            }
            HostedEvents.Remove(hostedEvent);
        }

        public void RetractAttendance(Event attending)
        {
            AttendingEvents.Remove(attending);
            attending.RemoveAttendee(this);
        }

        public void UpdateProfile(string name, string bio)
        {
            Profile.UpdateInfo(name, bio);
        }
    }

    public class BusinessAccount : Account
    {
        public string Address { get; private set; }
        public string BusinessName { get; private set; }
        public string Website { get; private set; }

        public BusinessAccount(string accountName, string accountPassword)
            : base(accountName, accountPassword, Type.BusinessOwner)
        {
            Address = "";
            BusinessName = "";
            Website = "";
        }

        public void UpdateAccountInfo(string address, string businessName, string website)
        {
            Address = address;
            BusinessName = businessName;
            Website = website;
        }
    }

    public class DogOwnerAccount : Account
    {
        public List<DogProfile> Dogs { get; private set; }

        public DogOwnerAccount(string accountName, string accountPassword)
            : base(accountName, accountPassword, Type.DogOwner)
        {
            Dogs = new List<DogProfile>();
        }

        public DogProfile AddDog(DogProfile.DogBreed breed, string name, string bio)
        {
            DogProfile newDog = new DogProfile(breed, DogProfile.FriendlinessRating.Good, name, bio);
            Dogs.Add(newDog);
            return newDog;
        }

        public void RemoveDog(int atIndex)
        {
            Dogs.RemoveAt(atIndex);
        }

        public void UpdateDog(int dogNumber, DogProfile.DogBreed breed, string name, string bio)
        {
            Dogs[dogNumber].UpdateInfo(breed, name, bio);
        }
    }


    public class AccountManager
    {
        public enum LoginResult
        {
            Success,
            AccountNotFound,
            IncorrectPassword
        }
        private Dictionary<string, Account> mAccounts;
        public AccountManager()
        {
            mAccounts = new Dictionary<string, Account>();
        }

        public Account AddAccount(string name, string password, Account.Type type)
        {
            Account newAccount = null;
            if (type == Account.Type.BusinessOwner)
            {
                newAccount = new BusinessAccount(name, password);
            }
            else
            {
                newAccount = new DogOwnerAccount(name, password);
            }
            mAccounts.Add(name, newAccount);
            return newAccount;
        }

        public bool AccountExists(string name)
        {
            return mAccounts.ContainsKey(name);
        }

        public LoginResult Login(string name, string password, out Account account)
        {
            account = null;
            if (mAccounts.ContainsKey(name))
            {
                if (mAccounts[name].AccountPassword.Equals(password))
                {
                    account = mAccounts[name];
                    return LoginResult.Success;
                }
                else
                {
                    return LoginResult.IncorrectPassword;
                }
            }
            else
            {
                return LoginResult.AccountNotFound;
            }
        }
    }
}
