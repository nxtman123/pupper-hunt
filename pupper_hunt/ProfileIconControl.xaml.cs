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
    /// Interaction logic for ProfileIconControl.xaml
    /// </summary>
    public partial class ProfileIconControl : UserControl
    {
        public ProfileIconControl()
        {
            InitializeComponent();
        }

        public void Initialize(Account account)
        {
            Tag = account;
            Name.Text = account.Profile.Name;
            (Image.Fill as ImageBrush).ImageSource = account.Profile.ImageSource;
        }

        private void ProfileIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow mainWindow = MainWindow.Instance();
            mainWindow.NavigateToProfileScreen(Tag as Account);
        }
    }
}
