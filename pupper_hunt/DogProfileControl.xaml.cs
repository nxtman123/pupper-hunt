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

        public void Initialize(DogProfile profile)
        { 
            (Image.Fill as ImageBrush).ImageSource = profile.ImageSource;
            DogName.Text = profile.Name;
            DogBreed.Text = profile.Breed.ToString();
            DogBio.Text = profile.Bio;
            (DogPersonality.Foreground as SolidColorBrush).Color 
                = (profile.Personality == pupper_hunt.DogPersonality.Friendly ? Color.FromRgb(0, 255, 0) 
                : (profile.Personality == pupper_hunt.DogPersonality.Nervous ? Color.FromRgb(255, 255, 0) : Color.FromRgb(255,0,0)));
            DogPersonality.Text = profile.Personality.ToString() ; 
        }
    }
}
