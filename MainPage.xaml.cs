using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace Swiper

{
    public partial class MainPage : ContentPage
    {
        private List<string> imageFiles;
        private List<string> nameList;
        private List<string> descriptionList;

        private int currentImageIndex = 0;
        private Random random;
        public MainPage()
        {
            InitializeComponent();
            random = new Random();
            InitializeAsync();
        }
        private async void InitializeAsync()
        {
            await LoadDataAsync();
        }
        private async Task LoadDataAsync()
        {
            try
            {
                string jsonFilePath = Path.Combine(FileSystem.AppDataDirectory, "data.json");

                if (!File.Exists(jsonFilePath))
                {
                    using var stream = await FileSystem.OpenAppPackageFileAsync("data.json");
                    using var reader = new StreamReader(stream);
                    string json = await reader.ReadToEndAsync();
                    var data = JsonSerializer.Deserialize<DataModel>(json);

                    imageFiles = data.images;
                    nameList = data.names;
                    descriptionList = data.descriptions;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load data from JSON file: {ex.Message}");
            }
        }
        private async void LoadImage_Click(object sender, EventArgs e)
        {
            approveBtn.IsEnabled = false;
            denyBtn.IsEnabled = false;
            if (imageFiles.Count == 0 || nameList.Count == 0 || descriptionList.Count == 0) return;

            topcat.Source = ImageSource.FromFile(imageFiles[currentImageIndex]);

            string randomName = nameList[random.Next(nameList.Count)];
            string randomDescription = descriptionList[random.Next(descriptionList.Count)];

            namecat.Text = randomName;
            descriptionImage.Text = randomDescription;

            currentImageIndex = (currentImageIndex + 1) % imageFiles.Count;

            await Task.Delay(300);
            approveBtn.IsEnabled = true;
            denyBtn.IsEnabled = true;
        }
    }
    public class DataModel
    {
        public List<string> images { get; set; }
        public List<string> names { get; set; }
        public List<string> descriptions { get; set; }


    }
}