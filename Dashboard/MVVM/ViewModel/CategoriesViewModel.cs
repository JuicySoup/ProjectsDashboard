using Dashboard.Commands;
using Dashboard.Core;
using Dashboard.MVVM.Exceptions;
using Dashboard.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard.MVVM.ViewModel
{
    class CategoriesViewModel : ObservableObject
    {
        public ObservableCollection<Project> Projects { get; set; }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        public RelayCommand NewCategory { get; set; }

        public RelayCommand RefreshCategories { get; set; }

        private Color _selectedColor;

        public Color SelectedColor
        {
            get { return _selectedColor; }
            set 
            {
                _selectedColor = value;
                OnPropertyChanged(nameof(SelectedColor));
            }
        }

        public List<Color> Colors { get; set; }

        public void SetColors()
        {

            Colors.Add(new Color
            {
                Name = "Blue",
                Hex = "#1bc0ff",
                Hover = "#f4fcff"
            });

            Colors.Add(new Color
            {
                Name = "Pink",
                Hex = "#ffa6bc",
                Hover = "#fffbfc"
            });

            Colors.Add(new Color
            {
                Name = "Turquoise",
                Hex = "#50e3c1",
                Hover = "#f5fefb"
            });
            Colors.Add(new Color
            {
                Name = "Royal Blue",
                Hex = "#5a71d9",
                Hover = "#b4bad4"
            });
            Colors.Add(new Color
            {
                Name = "Orange",
                Hex = "#ff9d4c",
                Hover = "#fff7f0"
            });
            Colors.Add(new Color
            {
                Name = "Purple",
                Hex = "#aa61fe",
                Hover = "#f8f5fe"
            });
            Colors.Add(new Color
            {
                Name = "Green",
                Hex = "#b8e986",
                Hover = "#dbe6d1"
            });
        }

        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public CategoriesViewModel()
        {
            Categories = new ObservableCollection<Category>();
            Colors = new List<Color>();
            SetColors();
            Projects = new ObservableCollection<Project>();
            Clients = new ObservableCollection<Client>();
            UpdateCategories();

            NewCategory = new RelayCommand(o =>
            {
                string path = Properties.Settings1.Default.ProjectPath.ToString() + @"\" + CategoryName;
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                    Category data = new()
                    {
                        Name = CategoryName,
                        Created = DateTime.Now.ToString("yyyy-MM-dd"),
                        Color = SelectedColor
                    };
                    Categories.Add(data);
                    WriteSettingsFile(path, data);

                }
            });

            RefreshCategories = new RelayCommand(o =>
            {

            });


        }

        public static void WriteSettingsFile(string path, Category data)
        {
            try
            {
                using StreamWriter json = File.CreateText(@$"{path}\settings.json");
                JsonSerializer serializer = new();
                serializer.Serialize(json, data);
            }
            catch (Exception e)
            {

                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }

        public void AddToCategories(Category category)
        {
            foreach (Category existingCategory in Categories)
            {
                if (existingCategory.Conflicts(category))
                {
                    throw new CategoryConflictException(existingCategory, category);
                }
            }
            Categories.Add(category);
        }

        public void UpdateCategories()
        {
            try
            {
                foreach (string path in Directory.GetDirectories(Properties.Settings1.Default.ProjectPath))
                {
                    string jsonpath = @$"{path}\settings.json";
                    if (File.Exists(jsonpath))
                    {
                        using StreamReader json = File.OpenText(jsonpath);
                        JsonSerializer serializer = new();
                        Category category = (Category)serializer.Deserialize(json, typeof(Category));
                        AddToCategories(category);
                    }
                    else
                    {
                        string name = path.Remove(0, path.LastIndexOf('\\') + 1);

                        Category data = new()
                        {
                            Name = name,
                            Color = Colors.Find(color => color.Name == "Blue"),
                            Created = DateTime.Now.ToString("yyyy-MM-dd")
                        };

                        AddToCategories(data);
                        WriteSettingsFile(path, data);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
