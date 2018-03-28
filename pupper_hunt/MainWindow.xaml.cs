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

        public MainWindow()
        {
            InitializeComponent();
            LoginScreen.Visibility = Visibility.Hidden;
            CreateAccountScreen.Visibility = Visibility.Hidden;
            SignUpScreen.Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Hidden;
            ProfileScreen.Visibility = Visibility.Hidden;
            DogInformationScreen.Visibility = Visibility.Hidden;
            BusinessProfileScreen.Visibility = Visibility.Hidden;
            NewsFeedScreen.Visibility = Visibility.Hidden;
            SearchScreen.Visibility = Visibility.Hidden;
            HelpScreen.Visibility = Visibility.Hidden;
            Ribbon.Visibility = Visibility.Hidden;

            mAccountManager = new AccountManager();
            mAccountManager.AddAccount("john", "smith", Account.Type.BusinessOwner);
            mCurrentAccount = null;

            // Starting Screen
            WelcomeScreen.Visibility = Visibility.Visible;

            mGridStack = new Stack<Grid>();
            mGridStack.Push(WelcomeScreen);
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
            mGridStack.Peek().Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;
            mGridStack.Push(LoginScreen);
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
                NewsFeedScreen.Visibility = Visibility.Visible;
                mGridStack.Push(NewsFeedScreen);
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
        private void Ribbon_BackButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            mGridStack.Pop();
            if (mGridStack.Count > 0)
            {
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

        private void Ribbon_FeedButton_Click(object sender, RoutedEventArgs e)
        {
            if (mGridStack.Peek() != NewsFeedScreen)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
                NewsFeedScreen.Visibility = Visibility.Visible;
                mGridStack.Push(NewsFeedScreen);
            }
        }

        private void Ribbon_SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (mGridStack.Peek() != SearchScreen)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
                SearchScreen.Visibility = Visibility.Visible;
                mGridStack.Push(SearchScreen);
            }
        }

        private void Ribbon_ProfileButton_Click(object sender, RoutedEventArgs e)
        {
            if (mGridStack.Peek() != ProfileEditScreen)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
                ProfileScreen.Visibility = Visibility.Visible;
                mGridStack.Push(ProfileScreen);
            }
        }

        private void Ribbon_HelpButton_Click(object sender, RoutedEventArgs e)
        {
            if (mGridStack.Peek() != HelpScreen)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
                HelpScreen.Visibility = Visibility.Visible;
                mGridStack.Push(HelpScreen);
            }
        }


        private void RefreshRibbonButtons()
        {
            // do stuff about making the buttons clickable
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
        }

        private void UpdateAccountProfile()
        {
            mCurrentAccount.UpdateProfile(ProfileEdit_UserName.Text, ProfileEdit_UserBio.Text);
        }

        #endregion

        private void Back(object sender, MouseButtonEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            mGridStack.Pop();
            if (mGridStack.Count > 0)
            {
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

        

    }
}
