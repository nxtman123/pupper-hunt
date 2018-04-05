using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pupper_hunt
{
    class BreedInfo
    {
        public int Adaptable { get; }
        public int Friendly { get; }
        public int Healthy { get; }
        public int Trainable { get; }
        public int Energy { get; }
        public string About { get; }
        public string Name { get; }

        public BreedInfo(DogBreed breed)
        {
            switch (breed)
            {
                case (DogBreed.Corgi):
                    Adaptable = 4;
                    Friendly = 4;
                    Healthy = 3;
                    Trainable = 3;
                    Energy = 4;
                    Name = "Pembrooke Corgi";
                    About = "Originally bred to herd cattle, sheep, and horses, the Pembroke Welsh Corgi is an active and intelligent dog breed. Easy to train and eager to learn, Pembrokes are great with children and other pets, and you can find them in four different coat colors and markings.";
                    break;
                case (DogBreed.Husky):
                    Adaptable = 2;
                    Friendly = 5;
                    Healthy = 3;
                    Trainable = 4;
                    Energy = 5;
                    Name = "Siberian Husky";
                    About = "The Siberian Husky is a beautiful dog breed with a thick coat that comes in a multitude of colors and markings. This athletic, intelligent dog can be independent and challenging for first-time dog owners. Huskies put the “H” in Houdini.";
                    break;
                case (DogBreed.Lab):
                    Adaptable = 3;
                    Friendly = 5;
                    Healthy = 4;
                    Trainable = 4;
                    Energy = 5;
                    Name = "Labrador Retriever";
                    About = "The Labrador Retriever was bred to be both a friendly companion and a useful working dog breed. The Labrador Retriever is a good-natured and hard working dog, and he’s America’s most popular breed.";
                    break;
            }
        }

    }
}
