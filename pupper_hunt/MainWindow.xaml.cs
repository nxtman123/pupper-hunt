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

        public MainWindow()
        {
            InitializeComponent();
            LoginScreen.Visibility = Visibility.Hidden;
            SignUpScreen.Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Hidden;
            ProfileScreen.Visibility = Visibility.Hidden;
            DogInformationScreen.Visibility = Visibility.Hidden;
            BusinessProfileScreen.Visibility = Visibility.Hidden;
            FeedScreen.Visibility = Visibility.Hidden;
            Ribbon.Visibility = Visibility.Hidden;

            // Starting Screen
            WelcomeScreen.Visibility = Visibility.Visible;

            mGridStack = new Stack<Grid>();
            mGridStack.Push(WelcomeScreen);
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            LoginScreen.Visibility = Visibility.Visible;
            mGridStack.Push(LoginScreen);
        }

        private void SignupButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            SignUpScreen.Visibility = Visibility.Visible;
            mGridStack.Push(SignUpScreen);
        }

        private void ConfirmLoginButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            mGridStack.Clear();
            Ribbon.Visibility = Visibility.Visible;
            FeedScreen.Visibility = Visibility.Visible;
            mGridStack.Push(FeedScreen);
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
            if (mGridStack.Peek() != FeedScreen)
            {
                mGridStack.Peek().Visibility = Visibility.Hidden;
                FeedScreen.Visibility = Visibility.Visible;
                mGridStack.Push(FeedScreen);
            }
        }

        private void Ribbon_SearchButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Search Button Clicked but not yet implimented");
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
            Console.WriteLine("Help Button Clicked but not yet implimented");
        }

        private void Profile_EditButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Peek().Visibility = Visibility.Hidden;
            ProfileEditScreen.Visibility = Visibility.Visible;
            mGridStack.Push(ProfileEditScreen);
        }

        private void RefreshRibbonButtons()
        {
            // do stuff about making the buttons clickable
        }

        private void SaveProfileButton_Click(object sender, RoutedEventArgs e)
        {
            mGridStack.Pop(); // Don't want to navigate back to edit screen
            ProfileEditScreen.Visibility = Visibility.Hidden;
            if (mGridStack.Peek() == SignUpScreen)
            {
                mGridStack.Clear();
                mGridStack.Push(FeedScreen);
                Ribbon.Visibility = Visibility.Visible;
            }
            mGridStack.Peek().Visibility = Visibility.Visible;
        }
    }
}
