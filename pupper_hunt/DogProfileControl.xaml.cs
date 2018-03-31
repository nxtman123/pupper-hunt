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
    /// Interaction logic for DogProfileControl.xaml
    /// </summary>
    public partial class DogProfileControl : UserControl
    {
        public DogProfileControl()
        {
            InitializeComponent();
        }

        private void DogBreed_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.Instance().NavigateToDogScreen(sender, e);
        }
    }
}
