using HelperMethods481;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Stack<Grid> mGridStack;
        private AccountManager mAccountManager;
        private Account mCurrentAccount;
        private List<Event> mNearbyEvents;

        private static MainWindow mInstance;

        public MainWindow()
        {
            InitializeComponent();
            mInstance = this;

            mAccountManager = new AccountManager();
            Account johnSmith = CreateFakeAccount();
            mCurrentAccount = null;

            mNearbyEvents = new List<Event>();
            CreateNearbyEvents();

            mGridStack = new Stack<Grid>();
            mGridStack.Push(WelcomeScreen);

            // Starting Screen
            WelcomeScreen.Visibility = Visibility.Visible;

            LoginScreen.Visibility = Visibility.Hidden;
            CreateAccountScreen.Visibility = Visibility.Hidden;
            CreateEventScreen.Visibility = Visibility.Hidden;
            SignUpScreen.Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Hidden;
            ProfileScreen.Visibility = Visibility.Hidden;
            DogInformationScreen.Visibility = Visibility.Hidden;
            BusinessProfileScreen.Visibility = Visibility.Hidden;
            NewsFeedScreen.Visibility = Visibility.Hidden;
            EventsScreen.Visibility = Visibility.Hidden;
            HelpScreen.Visibility = Visibility.Hidden;
            EventInfoScreen.Visibility = Visibility.Hidden;
            Ribbon.Visibility = Visibility.Hidden;
            RefreshRibbonButtons();

            Account lacy = mAccountManager.AddAccount("Lacy loo who", "nope", Account.Type.DogOwner);
            Account boo = mAccountManager.AddAccount("Boo", "Ahhh", Account.Type.DogOwner);
            Account peep = mAccountManager.AddAccount("Peep a Sheep", "sheep", Account.Type.DogOwner);

            Event testEvent = johnSmith.HostEvent(Event.GetNextEventImage(), "Lets go to the park!", "Park", "Got 30 minutes lets do this", DateTime.Now);
            boo.AttendEvent(testEvent);
            peep.AttendEvent(testEvent);

            testEvent = new Event(boo, Event.GetNextEventImage(), "Go play fetch!", "Field", "First one there wins", DateTime.Now);
            boo.AttendEvent(testEvent);
            peep.AttendEvent(testEvent);
            johnSmith.AttendEvent(testEvent);

            testEvent = new Event(peep, Event.GetNextEventImage(), "Who want's to go for a stroll?", "Crowfoot", "Only tame dogs", DateTime.Now);
            boo.AttendEvent(testEvent);
            johnSmith.AttendEvent(testEvent);
        }

        public static MainWindow Instance()
        {
            return mInstance;
        }

        #region SignupFlow

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            CreateAccountScreen.Visibility = Visibility.Visible;
            mGridStack.Push(CreateAccountScreen);
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            string accountName = AccountUsernameField.Text;
            string password = AccountPasswordField.Text;
            string accountTypeString = AccountTypeSelector.Text;

            if (accountName.Equals("") || password.Equals("") || accountTypeString.Equals(""))
            {
                AccountWarning.Visibility = Visibility.Visible;
                AccountWarning.Text = "Please Provide All Information";
            }
            else if (mAccountManager.AccountExists(accountName))
            {
                AccountWarning.Visibility = Visibility.Visible;
                AccountWarning.Text = "Account Name Already Exists";
            }
            else
            {
                AccountWarning.Visibility = Visibility.Hidden;
                Account.Type type = Account.Type.BusinessOwner;
                if (accountTypeString.Equals("Dog Owner")) type = Account.Type.DogOwner;
                else if (accountTypeString.Equals("Business Owner")) type = Account.Type.BusinessOwner;
                mCurrentAccount = mAccountManager.AddAccount(accountName, password, type);

                mGridStack.Peek().Visibility = Visibility.Hidden;
                mGridStack.Clear();
                mGridStack.Push(ProfileEditScreen);
                ProfileEditScreen.Visibility = Visibility.Visible;
                PopulateProfileEditScreen();
            }
        }

        private void DogOwnerSelectButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Visible;
            mGridStack.Push(ProfileEditScreen);
        }

        private void BusinessOwnerSelectButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Visible;
            mGridStack.Push(ProfileEditScreen);
        }

        private void DogEnthusiastSelectButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Visible;
            mGridStack.Push(ProfileEditScreen);
        }
        #endregion

        #region LoginFlow

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            //GoToScreen(LoginScreen);
            { /// REMOVE LATER
                AccountManager.LoginResult result = mAccountManager.Login("john", "smith", out mCurrentAccount);
                mGridStack.Peek().Visibility = Visibility.Hidden;
                mGridStack.Clear();
                Ribbon.Visibility = Visibility.Visible;
                GoToScreen(NewsFeedScreen);
            }
        }


        private void ConfirmLoginButton_Click(object sender, RoutedEventArgs e)
        {
            string userName = UsernameField.Text.ToLower();
            string password = PasswordField.Password.ToLower();

            AccountManager.LoginResult result = mAccountManager.Login(userName, password, out mCurrentAccount);
            if (result == AccountManager.LoginResult.Success)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
                mGridStack.Clear();
                Ribbon.Visibility = Visibility.Visible;
                GoToScreen(NewsFeedScreen);
            }
            else if (result == AccountManager.LoginResult.IncorrectPassword)
            {

            }
            else if (result == AccountManager.LoginResult.AccountNotFound)
            {

            }
        }
        #endregion

        #region Ribbon
        private void Ribbon_NewsFeedButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (mGridStack.Peek() != NewsFeedScreen)
            {
                GoToScreen(NewsFeedScreen);
            }
        }

        private void Ribbon_EventsButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (mGridStack.Peek() != EventsScreen)
            {
                SelectEventsRibbon("NearBy");
                GoToScreen(EventsScreen);
            }
        }

        private void Ribbon_ProfileButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (mGridStack.Peek() != ProfileEditScreen)
            {
                PopulateProfileScreen();
                GoToScreen(ProfileScreen);
            }
        }

        private void Ribbon_DogInfoButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (mGridStack.Peek() != HelpScreen)
            {
                GoToScreen(DogInformationScreen);
            }
        }

        private void RefreshRibbonButtons()
        {
            Ribbon_NewsFeedIcon_Mask.Visibility = mGridStack.Peek() == NewsFeedScreen ? Visibility.Hidden : Visibility.Visible;
            Ribbon_EventsIcon_Mask.Visibility = mGridStack.Peek() == EventsScreen ? Visibility.Hidden : Visibility.Visible;
            Ribbon_ProfileIcon_Mask.Visibility = mGridStack.Peek() == ProfileScreen ? Visibility.Hidden : Visibility.Visible;
            Ribbon_DogInfoIcon_Mask.Visibility = mGridStack.Peek() == DogInformationScreen ? Visibility.Hidden : Visibility.Visible;
        }

        #endregion

        #region Profile
        private void Profile_EditButton_Click(object sender, RoutedEventArgs e)
        {
            Ribbon.Visibility = Visibility.Hidden;
            mGridStack.Peek().Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Visible;
            mGridStack.Push(ProfileEditScreen);
        }

        private void SaveProfileButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateAccountProfile();
            ProfileEditScreen.Visibility = Visibility.Hidden;
            mGridStack.Pop(); // Don't want to navigate back to edit screen
            if (mGridStack.Count == 0)
            {
                mGridStack.Push(ProfileScreen); // TODO: What is first screen after creating account
            }
            
            Ribbon.Visibility = Visibility.Visible;
            PopulateProfileScreen();
            mGridStack.Peek().Visibility = Visibility.Visible;
        }

        private void PopulateProfileEditScreen()
        {
            ProfileEdit_UserImage.Source = mCurrentAccount.Profile.ImageSource;
            ProfileEdit_UserName.Text = mCurrentAccount.Profile.Name;
            ProfileEdit_UserBio.Text = mCurrentAccount.Profile.Bio;
        }

        private void PopulateProfileScreen()
        {
            Profile_Image.Source = mCurrentAccount.Profile.ImageSource;
            Profile_Name.Text = mCurrentAccount.Profile.Name;
            Profile_Bio.Text = mCurrentAccount.Profile.Bio;
            DogOwnerAccount dogOwnerAccount = mCurrentAccount as DogOwnerAccount;
            if (dogOwnerAccount != null)
            {
                int counter = 0;
                List<DogProfile> dogs = dogOwnerAccount.Dogs;
                foreach (var child in Profile_DogsList.Children)
                {
                    DogProfileControl dpc = child as DogProfileControl;
                    if (dogs.Count > counter)
                    {
                        dpc.Visibility = Visibility.Visible;
                        dpc.Image.Source = dogs[counter].ImageSource;
                        dpc.DogName.Text = dogs[counter].Name;
                        dpc.DogBreed.Text = dogs[counter].Breed.ToString();
                        dpc.DogBio.Text = dogs[counter].Bio;
                    }
                    else
                    {
                        dpc.Visibility = Visibility.Hidden;
                    }
                    ++counter;
                }
            }
           
        }

        private void UpdateAccountProfile()
        {
            mCurrentAccount.UpdateProfile(ProfileEdit_UserName.Text, ProfileEdit_UserBio.Text);
        }

        #endregion

        public void NavigateToDogScreen(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb != null)
            {
                string content = tb.Text;

                DogProfile.DogBreed breedToShow = DogProfile.DogBreed.Corgi;
                if (content.Equals(DogProfile.DogBreed.Corgi.ToString()))
                {
                    breedToShow = DogProfile.DogBreed.Corgi;
                }
                else if (content.Equals(DogProfile.DogBreed.Husky.ToString()))
                {
                    breedToShow = DogProfile.DogBreed.Husky;
                }
                else if (content.Equals(DogProfile.DogBreed.Lab.ToString()))
                {
                    breedToShow = DogProfile.DogBreed.Lab;
                }
                PopulateDogInformationScreen(breedToShow);
                GoToScreen(DogInformationScreen);
            }
        }

        private void PopulateDogInformationScreen(DogProfile.DogBreed dogBreed)
        {

        }

        public void NavigateToEventScreen(Event e)
        {
            Ribbon.Visibility = Visibility.Hidden;
            PopulateEventInfoScreen(e);
            GoToScreen(EventInfoScreen);
        }


        private void PopulateEventInfoScreen(Event toShow)
        {
            EventInfoScreen.Tag = toShow.Id;
            EventInfo_Image.Source = toShow.EventImageSource;
            EventInfo_Day.Text = toShow.EventTime.Day.ToString();
            EventInfo_Month.Text = Event.IntToMonth(toShow.EventTime.Day);
            EventInfo_HostName.Text = toShow.EventCreator.AccountName;
            EventInfo_Info.Text = toShow.EventDescription;
            EventInfo_AttendanceList.Items.Clear();
            foreach (Account attendee in toShow.EventAttendees)
            {
                EventInfo_AttendanceList.Items.Add(new TextBlock() { Text = attendee.AccountName });
            }

            EventInfo_Attend.Visibility = toShow.EventCreator == mCurrentAccount ? Visibility.Hidden : Visibility.Visible;
            EventInfo_Attend.Content = toShow.IsAttending(mCurrentAccount) ? "Cancel" : "Attend!";
            EventInfo_EditButton.Visibility = toShow.EventCreator == mCurrentAccount ? Visibility.Visible : Visibility.Hidden;
        }

        private void PopulateCreateEventScreen(Event e = null)
        {
            CreateEvent_Image.Source = e == null ? Event.GetNextEventImage() : e.EventImageSource; // Each time an event is created it increments a count and returns the next event image we have
            CreateEvent_WarningBanner.Visibility = Visibility.Hidden;
            CreateEvent_NameInput.Text = e == null ? "" : e.EventName;
            CreateEvent_LocationInput.Text = e == null ? "" : e.EventLocation;
            CreateEvent_DescriptionInput.Text = e == null ? "" : e.EventDescription;
            CreateEvent_DayPicker.SelectedDate = e == null ? DateTime.Now : e.EventTime;
            if (!CreateEvent_TimePicker.HasItems)
            {
                for (int i = 0; i < 24; ++i)
                {
                    int hour = i;
                    bool isPM = false;
                    if (hour > 12)
                    {
                        hour = hour - 12;
                        isPM = true;
                    }
                    CreateEvent_TimePicker.Items.Add(hour.ToString() + ":00"+ " " + (isPM ? "PM" : "AM"));
                    CreateEvent_TimePicker.Items.Add(hour.ToString() + ":30" + " " + (isPM ? "PM" : "AM"));
                }
            }
            CreateEvent_TimePicker.SelectedIndex = e == null ? DateTime.Now.Hour * 2 : e.EventTime.Hour * 2;
            CreateEventScreen.Tag = e == null ? -1 : e.Id;
        }

        private void CreateEvent_CreateButton_Click(object sender, RoutedEventArgs e)
        {
            string warningTag = "";
            if (CreateEvent_NameInput.Text.Equals("")) warningTag = "Please Enter an Event Name";
            if (CreateEvent_LocationInput.Text.Equals("")) warningTag = "Please Enter a Location";
            if (CreateEvent_DescriptionInput.Text.Equals("")) warningTag = "Please Enter a Description";
            DateTime? selectedDate = CreateEvent_DayPicker.SelectedDate;
            string[] timeSelection = CreateEvent_TimePicker.Text.Split(' ');
            uint hour = uint.Parse(timeSelection[0].Split(':')[0]);
            uint minutes = uint.Parse(timeSelection[0].Split(':')[1]);
            bool isPM = timeSelection[1].Equals("PM");
            selectedDate.Value.AddHours(hour + (isPM ? 12 : 0));
            selectedDate.Value.AddMinutes(minutes);
            if (selectedDate.Value.CompareTo(DateTime.Now.AddHours(-1)) < 0)
            {
                warningTag = "Please Enter a Future Date";
            }

            if (warningTag.Equals("")) // entered out all information
            {
                if ((int)CreateEventScreen.Tag == -1) // created a new event
                {
                    mCurrentAccount.HostEvent(CreateEvent_Image.Source, CreateEvent_NameInput.Text, CreateEvent_DescriptionInput.Text, CreateEvent_LocationInput.Text, selectedDate.Value);
                    GoBack();
                }
                else // edited an event
                {
                    Event toUpdate = Event.GetEvent((int)CreateEventScreen.Tag);
                    toUpdate.Update(CreateEvent_NameInput.Text, CreateEvent_DescriptionInput.Text, CreateEvent_LocationInput.Text, selectedDate.Value);
                    mGridStack.Pop().Visibility = Visibility.Hidden;
                    PopulateCreateEventScreen(toUpdate); 
                    mGridStack.Peek().Visibility = Visibility.Visible; // go back to the eventinfo screen
                }

            }
            else
            {
                CreateEvent_WarningBanner.Text = warningTag;
                CreateEvent_WarningBanner.Visibility = Visibility.Visible;
            }
        }

        private void CreateEvent_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void CreateEvent(object sender, RoutedEventArgs e)
        {
            PopulateCreateEventScreen();
            GoToScreen(CreateEventScreen);
        }

        private void CreateNearbyEvents()
        {
            // make events
        }

        private DogOwnerAccount CreateFakeAccount()
        {
            DogOwnerAccount account = mAccountManager.AddAccount("john", "smith", Account.Type.DogOwner) as DogOwnerAccount;
            account.AddDog();
            account.AddDog();
            return account;
        }

        private void GoToScreen(Grid screen, bool refreshRibbon = true)
        {
            if (mGridStack.Count > 0)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
            }
            mGridStack.Push(screen);
            screen.Visibility = Visibility.Visible;
            if (refreshRibbon)
            {
                RefreshRibbonButtons();
            }
        }

        private void GoBack(object sender, RoutedEventArgs e)
        {
            GoBack();
        }

        private void GoBack()
        {
            mGridStack.Pop().Visibility = Visibility.Hidden;
            if (mGridStack.Count > 0)
            {
                Ribbon.Visibility = Visibility.Visible; // Back is only ever called from info screens so returning screen will have ribbon
                mGridStack.Peek().Visibility = Visibility.Visible;
            }
            else
            {
                // back at login screen. Hide Ribbon
                Ribbon.Visibility = Visibility.Hidden;
                WelcomeScreen.Visibility = Visibility.Visible;
                mGridStack.Push(WelcomeScreen);
            }
        }

        private void EventInfo_EditButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateCreateEventScreen(Event.GetEvent((int)EventInfoScreen.Tag));
            GoToScreen(CreateEventScreen);
        }

        private void EventInfo_AttendToggle_Click(object sender, RoutedEventArgs e)
        {
            Event attending = Event.GetEvent((int)EventInfoScreen.Tag);
            if (attending.IsAttending(mCurrentAccount))
            {
                mCurrentAccount.RetractAttendance(attending);
                EventInfo_Attend.Content = "Attend!";       
            }
            else
            {
                mCurrentAccount.AttendEvent(attending);
                EventInfo_Attend.Content = "Cancel";
            }
            PopulateEventInfoScreen(attending);
        }

        private void EventsScreen_Toggle_EventList(object sender, MouseButtonEventArgs e)
        {
            string tag = (string)(sender as TextBlock).Tag;
            if ((string)EventsScreen.Tag != tag)
            {
                EventsScreen.Tag = tag;
                SelectEventsRibbon(tag);
            }
        }

        private void SelectEventsRibbon(string tag)
        {
            EventsScreen_Ribbon_Nearby_Mask.Visibility = Visibility.Visible;
            EventsScreen_Ribbon_Attending_Mask.Visibility = Visibility.Visible;
            EventsScreen_Ribbon_Hosting_Mask.Visibility = Visibility.Visible;
            

            EventsScreen_EventList.Children.Clear();
            List<Event> events = new List<Event>();
            if (tag.Equals("NearBy"))
            {
                EventsScreen_Ribbon_Nearby_Mask.Visibility = Visibility.Hidden;
                events = Event.GetAllEvents();
            }
            else if (tag.Equals("Attending"))
            {
                EventsScreen_Ribbon_Attending_Mask.Visibility = Visibility.Hidden;
                events = mCurrentAccount.AttendingEvents;
            }
            else if (tag.Equals("Hosting"))
            {
                EventsScreen_Ribbon_Hosting_Mask.Visibility = Visibility.Hidden;
                events = mCurrentAccount.HostedEvents;
            }

            double stackWidth = EventsScreen_EventList.Width;

            foreach( Event e in events)
            {
                EventControl ec = new EventControl() { HorizontalAlignment = HorizontalAlignment.Center };
                ec.Initialize(e);
                EventsScreen_EventList.Children.Add(ec);
            }
            
        }
    }
}
