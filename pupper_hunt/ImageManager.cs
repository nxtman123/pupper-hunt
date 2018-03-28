using HelperMethods481;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace pupper_hunt
{
    class ImageManager
    {
        private static Dictionary<string, BitmapImage> mImages;

        public static BitmapSource GetImageSource(string imageName)
        {
            if (mImages == null)
            {
                mImages = AssemblyManager.GetImageDictionaryFromEmbeddedResources();
            }

            if (mImages.ContainsKey(imageName))
            {
                return mImages[imageName];
            }
            return null; // TODO: Return a default image 
        }
    }
}
