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
    /// Interaction logic for NotificationControl.xaml
    /// </summary>
    public partial class NotificationControl : UserControl
    {
        public NotificationControl()
        {
            InitializeComponent();
        }

        public void Initialize(Notification notification)
        {
            Image.Source = notification.ImageSource;
            Info.Text = notification.Content;
            TimeSpan difference = DateTime.Now - notification.Date;
            Date.Text = difference.Minutes.ToString() + " min ago";
            
        }
    }
}
