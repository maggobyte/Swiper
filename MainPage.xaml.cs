using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Swiper

{
    public partial class MainPage : ContentPage
    {
        private List<string> imageFiles;
        private int currentImageIndex = 0;
        private int nameIndex = 0;

        private List<string> nameList;
        private Random random;

        private List<string> descriptionList;
        public MainPage()
        {
            InitializeComponent();

            imageFiles = new List<string> { "cat1.jpg", "cat2.jpg", "cat3.jpg" };
            nameList = new List<string> { "Tom", "Harry", "Paul" };
            descriptionList = new List<string> { "I love walking along the beach, and eating mice", "My favorite thing to do is kill", "Looking for the perfect soulmate!" };

            random = new Random();

        }
        private void LoadImage_Click(object sender, EventArgs e)
        {
            if (imageFiles.Count == 0) return;

            topcat.Source = ImageSource.FromFile(imageFiles[currentImageIndex]);
            currentImageIndex = (currentImageIndex + 1) % imageFiles.Count;

            string randomName = ChangeName(nameList);
            namecat.Text = randomName;

            string randomDescription = ChangeDescription(descriptionList);
            descriptionImage.Text = randomDescription;
            
        }
        private string ChangeName(List<string> nameList)
        {
            int index = random.Next(nameList.Count);
            return nameList[index];
        }
        private string ChangeDescription(List<string> descriptionList)
        {
            int index = random.Next(descriptionList.Count);
            return descriptionList[index];
        }

    }
}