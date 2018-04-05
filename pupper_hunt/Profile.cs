using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace pupper_hunt
{
    public class Profile
    {
        public string Name { get; protected set; }
        public string Bio { get; protected set; }
        public ImageSource ImageSource { get; protected set; }

        public Profile()
        {
            Name = "";
            Bio = "";
            ImageSource = ImageManager.GetImageSource("userUnknownProfile");
        }

        public Profile(string name, string bio, ImageSource source)
        {
            Name = name;
            Bio = bio;
            ImageSource = source;
        }

        public void UpdateInfo(string name, string bio)
        {
            Name = name;
            Bio = bio;
        }
    }

    public enum DogBreed
    {
        Corgi,
        Husky,
        Lab,
        NUM
    }

    public enum DogPersonality
    {
        Aggressive,
        Nervous,
        Friendly,
        NUM
    }


    public class DogProfile : Profile
    {
        public DogBreed Breed;
        public DogPersonality Personality;

        public DogProfile(DogBreed breed, DogPersonality personality, string name, string bio)
            : base(name, bio, ImageManager.GetImageSource(breed.ToString().ToLower() + "Profile"))
        {
            Breed = breed;
            Personality = personality;
        }

        public void UpdateInfo(DogBreed breed, DogPersonality personality, string name, string bio)
        {
            ImageSource = ImageManager.GetImageSource(breed.ToString().ToLower() + "Profile");
            Breed = breed;
            Name = name;
            Bio = bio;
            Personality = personality;
        }
    }
}
