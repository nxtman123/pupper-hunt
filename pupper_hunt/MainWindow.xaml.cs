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
            DogEditScreen.Visibility = Visibility.Hidden;
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
            lacy.UpdateProfile("Lacy", "I love dogs :)");
            Account boo = mAccountManager.AddAccount("Boo", "Ahhh", Account.Type.DogOwner);
            boo.UpdateProfile("Bo Peep", "Someone get me a ball to throw for my lab");
            Account ronny = mAccountManager.AddAccount("Peep a Sheep", "sheep", Account.Type.BusinessOwner);
            ronny.UpdateProfile("Ronny Coleman", "Looking for my pupper solemate");

            Event testEvent = johnSmith.HostEvent(Event.GetNextEventImage(), "Lets go to the park!", "Park", "Got 30 minutes lets do this", DateTime.Now);
            boo.AttendEvent(testEvent);
            ronny.AttendEvent(testEvent);
            lacy.AttendEvent(testEvent);

            testEvent = new Event(boo, Event.GetNextEventImage(), "Go play fetch!", "Field", "First one there wins", DateTime.Now);
            boo.AttendEvent(testEvent);
            ronny.AttendEvent(testEvent);
            johnSmith.AttendEvent(testEvent);
            lacy.AttendEvent(testEvent);

            testEvent = new Event(ronny, Event.GetNextEventImage(), "Who want's to go for a stroll?", "Crowfoot", "Only tame dogs", DateTime.Now);
            boo.AttendEvent(testEvent);
            johnSmith.AttendEvent(testEvent);
        }

        public static MainWindow Instance()
        {
            return mInstance;
        }
        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------SIGN UP FLOW-----------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region SignupFlow

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            CreateAccountScreen.Visibility = Visibility.Visible;
            AccountUsernameField.Text = "";
            AccountPasswordField.Text = "";
            AccountTypeSelector.SelectedIndex = 0;
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

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------LOGIN FLOW-------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region LoginFlow

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateLoginScreen();
            GoToScreen(LoginScreen);
            //{ // TEMP FLOW
            //    AccountManager.LoginResult result = mAccountManager.Login("john", "smith", out mCurrentAccount);
            //    mGridStack.Peek().Visibility = Visibility.Hidden;
            //    mGridStack.Clear();
            //    Ribbon.Visibility = Visibility.Visible;
            //    PopulateNewsFeed();
            //    GoToScreen(NewsFeedScreen);
            //}
        }

        private void PopulateLoginScreen()
        {
            UsernameField.Text = "";
            PasswordField.Password = "";
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
                PopulateNewsFeed();
                GoToScreen(NewsFeedScreen);
            }
            else if (result == AccountManager.LoginResult.IncorrectPassword)
            {
                //TODO: Display warning
            }
            else if (result == AccountManager.LoginResult.AccountNotFound)
            {
                //TODO: Display warning
            }
        }
        #endregion

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------RIBBON-----------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region Ribbon
        private void Ribbon_NewsFeedButton_Click(object sender, MouseButtonEventArgs e)
        {
            if (mGridStack.Peek() != NewsFeedScreen)
            {
                PopulateNewsFeed();
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
                PopulateProfileScreen(mCurrentAccount, false);
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

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------PROFILE----------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region Profile
        private void Profile_EditButton_Click(object sender, RoutedEventArgs e)
        {
            Ribbon.Visibility = Visibility.Hidden;
            mGridStack.Peek().Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Visible;
            PopulateProfileEditScreen();
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
            PopulateProfileScreen(mCurrentAccount, false);
            mGridStack.Peek().Visibility = Visibility.Visible;
        }

        private void PopulateProfileEditScreen()
        {
            ProfileEdit_UserImage.Source = mCurrentAccount.Profile.ImageSource;
            ProfileEdit_UserName.Text = mCurrentAccount.Profile.Name;
            ProfileEdit_UserBio.Text = mCurrentAccount.Profile.Bio;
        }

        private void PopulateDogEditScreen()
        {
            if (!DogEdit_BreedSelector.HasItems)
            {
                for (int i = 0; i < (int)DogBreed.NUM; ++i)
                {
                    DogEdit_BreedSelector.Items.Add(new ComboBoxItem() { Content = ((DogBreed)i).ToString() });
                }
            }
            if (!DogEdit_PersonalitySelector.HasItems)
            {
                for (int i = 0; i < (int)DogPersonality.NUM; ++i)
                {
                    DogEdit_PersonalitySelector.Items.Add(new ComboBoxItem() { Content = ((DogPersonality)i).ToString() });
                }
            }

            int tag = (int)DogEditScreen.Tag;
            DogProfile toEdit = tag == -1 ? null : (mCurrentAccount as DogOwnerAccount).Dogs[tag];

            DogEdit_Image.Source = toEdit != null ? toEdit.ImageSource : ImageManager.GetImageSource("corgiProfile");
            DogEdit_Name.Text = toEdit != null ? toEdit.Name : "";
            DogEdit_Bio.Text = toEdit != null ? toEdit.Bio : "";
            DogEdit_BreedSelector.SelectedIndex = toEdit != null ? (int)toEdit.Breed : 0;
            DogEdit_PersonalitySelector.SelectedIndex = toEdit != null ? (int)toEdit.Personality : 0;
        }

        private void DogEdit_SaveButton_Click(object sender, RoutedEventArgs e)
        {
            int dogIndex = (int)DogEditScreen.Tag;
            if (dogIndex == -1) // created a new pup!
            {
                (mCurrentAccount as DogOwnerAccount).AddDog((DogBreed)DogEdit_BreedSelector.SelectedIndex, (DogPersonality)DogEdit_PersonalitySelector.SelectedIndex, DogEdit_Name.Text, DogEdit_Bio.Text);
            }
            else
            {
                (mCurrentAccount as DogOwnerAccount).UpdateDog((int)DogEditScreen.Tag, (DogBreed)DogEdit_BreedSelector.SelectedIndex, (DogPersonality)DogEdit_PersonalitySelector.SelectedIndex, DogEdit_Name.Text, DogEdit_Bio.Text);
            }
            PopulateProfileScreen(mCurrentAccount, false);
            GoBack();
        }

        private void DogEdit_BreedSelector_Changed(object sender, EventArgs e)
        {
            ImageSource source = null;
            switch (DogEdit_BreedSelector.SelectedIndex)
            {
                case ((int)DogBreed.Corgi):
                    source = ImageManager.GetImageSource("corgiProfile");
                    break;
                case ((int)DogBreed.Husky):
                    source = ImageManager.GetImageSource("huskyProfile");
                    break;
                case ((int)DogBreed.Lab):
                    source = ImageManager.GetImageSource("labProfile");
                    break;
            }
            DogEdit_Image.Source = source;
        }

        private void PopulateProfileScreen(Account account, bool fromHotLink)
        {
            Profile_AddPupper.Visibility = !fromHotLink ? Visibility.Visible : Visibility.Hidden;
            Profile_EditButton.Visibility = !fromHotLink ? Visibility.Visible : Visibility.Hidden;
            Profile_BackButton.Visibility = !fromHotLink ? Visibility.Hidden : Visibility.Visible;
            Profile_Image.Source = account.Profile.ImageSource;
            Profile_Name.Text = account.Profile.Name;
            Profile_Bio.Text = account.Profile.Bio;
            DogOwnerAccount dogOwnerAccount = account as DogOwnerAccount;
            
            if (dogOwnerAccount != null)
            {
                Profile_PupperList.Children.Clear();
                List<DogProfile> dogs = dogOwnerAccount.Dogs;
                foreach (DogProfile dog in dogs)
                {
                    DogProfileControl dpc = new DogProfileControl() { HorizontalAlignment = HorizontalAlignment.Center };
                    dpc.Tag = Profile_PupperList.Children.Count;
                    Profile_PupperList.Children.Add(dpc);
                    
                    if (!fromHotLink)
                    {
                        dpc.MouseDown += Dpc_MouseDown;
                    }
                    dpc.Initialize(dog);
                }
            }
        }

        private void Dpc_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Handled) return;
            DogProfileControl dpc = sender as DogProfileControl;
            if (dpc != null)
            {
                DogEditScreen.Tag = dpc.Tag;
                Ribbon.Visibility = Visibility.Hidden;
                PopulateDogEditScreen();
                GoToScreen(DogEditScreen);
            }
        }

        private void Profile_AddPupper_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DogEditScreen.Tag = -1;
            Ribbon.Visibility = Visibility.Hidden;
            PopulateDogEditScreen();
            GoToScreen(DogEditScreen);
        }

        private void UpdateAccountProfile()
        {
            mCurrentAccount.UpdateProfile(ProfileEdit_UserName.Text, ProfileEdit_UserBio.Text);
        }

        #endregion

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------DOG INFO---------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region DogInfo
        public void NavigateToDogScreen(object sender, MouseButtonEventArgs e)
        {
            TextBlock tb = sender as TextBlock;
            if (tb != null)
            {
                string content = tb.Text;

                DogBreed breedToShow = DogBreed.Corgi;
                if (content.Equals(DogBreed.Corgi.ToString()))
                {
                    breedToShow = DogBreed.Corgi;
                }
                else if (content.Equals(DogBreed.Husky.ToString()))
                {
                    breedToShow = DogBreed.Husky;
                }
                else if (content.Equals(DogBreed.Lab.ToString()))
                {
                    breedToShow = DogBreed.Lab;
                }
                PopulateDogInformationScreen(breedToShow);
                GoToScreen(DogInformationScreen);
            }
        }

        private void PopulateDogInformationScreen(DogBreed dogBreed)
        {

        }

        #endregion

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------News Feed--------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region NewsFeed

        private void PopulateNewsFeed()
        {
            News_NotificationList.Children.Clear();

            foreach (Notification n in Notification.Notifications)
            {
                NotificationControl nc = new NotificationControl();
                nc.Initialize(n);
                nc.MouseDown += n.OnClick;
                News_NotificationList.Children.Add(nc);
            }
        }


        #endregion

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------Screen Navigation------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region NavigationFunctions
        public void NavigateToEventScreen(Event e)
        {
            Ribbon.Visibility = Visibility.Hidden;
            PopulateEventInfoScreen(e);
            GoToScreen(EventInfoScreen);
        }

        public void NavigateToProfileScreen(Account account)
        {
            Ribbon.Visibility = Visibility.Hidden; // If jumping to profile view from another screen, replace ribbon with a back button
            PopulateProfileScreen(account, true);
            GoToScreen(ProfileScreen);
        }

        private void GoBack(object sender, EventArgs e)
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

        private void ProfileLink_Click(object sender, MouseButtonEventArgs e)
        {
            Account account = (sender as TextBlock).Tag as Account;
            NavigateToProfileScreen(account);
        }

        #endregion

        //-----------------------------------------------------------------------------------------------------//
        //------------------------------------------Events-----------------------------------------------------//
        //-----------------------------------------------------------------------------------------------------//
        #region Events
        private void PopulateEventInfoScreen(Event toShow)
        {
            EventInfoScreen.Tag = toShow.Id;
            EventInfo_Image.Source = toShow.EventImageSource;
            EventInfo_Day.Text = toShow.EventTime.Day.ToString();
            EventInfo_Month.Text = Event.IntToMonth(toShow.EventTime.Day);
            EventInfo_HostedByLink.Text = toShow.EventCreator.Profile.Name;
            EventInfo_HostedByLink.Tag = toShow.EventCreator;
            EventInfo_Name.Text = toShow.EventName;
            EventInfo_Info.Text = toShow.EventDescription;
            EventInfo_AttendanceList.Children.Clear();
            foreach (Account attendee in toShow.EventAttendees)
            {
                ProfileIconControl pic = new ProfileIconControl();
                pic.Initialize(attendee);
                EventInfo_AttendanceList.Children.Add(pic);
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
                    CreateEvent_TimePicker.Items.Add(hour.ToString() + ":00" + " " + (isPM ? "PM" : "AM"));
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
                    SelectEventsRibbon("Hosting");
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

        private void CreateEvent(object sender, MouseButtonEventArgs e)
        {
            Ribbon.Visibility = Visibility.Hidden;
            PopulateCreateEventScreen();
            GoToScreen(CreateEventScreen);
        }

        private void EventInfo_EditButton_Click(object sender, RoutedEventArgs e)
        {
            Ribbon.Visibility = Visibility.Hidden;
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

            foreach (Event e in events)
            {
                EventControl ec = new EventControl() { HorizontalAlignment = HorizontalAlignment.Center };
                ec.Initialize(e);
                EventsScreen_EventList.Children.Add(ec);
            }
        }

        private void CreateEvent_CancelButton_Click(object sender, RoutedEventArgs e)
        {
            GoBack();
        }
        #endregion

        private void CreateNearbyEvents()
        {
            // make events
        }

        private DogOwnerAccount CreateFakeAccount()
        {
            DogOwnerAccount account = mAccountManager.AddAccount("john", "smith", Account.Type.DogOwner) as DogOwnerAccount;
            account.UpdateProfile("John Smith", "Best pupper owner you'll ever meet");
            account.AddDog(DogBreed.Husky, DogPersonality.Friendly, "Fido","Loves Fetch");
            account.AddDog(DogBreed.Lab, DogPersonality.Nervous, "Rover", "RUFFF!");
            return account;
        }
    }
}
