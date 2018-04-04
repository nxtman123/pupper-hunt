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
            e.Handled = true;
            MainWindow.Instance().NavigateToDogScreen(sender, e);
            
        }

        public void Initialize(ImageSource image, string name, string breed, string bio)
        { 
            (Image.Fill as ImageBrush).ImageSource = image;
            DogName.Text = name;
            DogBreed.Text = breed;
            DogBio.Text = bio;
        }
    }
}
