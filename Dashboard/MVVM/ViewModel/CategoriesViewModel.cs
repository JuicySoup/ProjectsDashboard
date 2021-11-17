using Dashboard.Commands;
using Dashboard.Core;
using Dashboard.MVVM.Exceptions;
using Dashboard.MVVM.Model;
using Dashboard.Stores;
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
    public class CategoriesViewModel : ObservableObject
    {

        public ObservableCollection<Category> Categories { get; set; }
        public RelayCommand NewCategory { get; set; }
        public RelayCommand RefreshCategories { get; set; }

        public Colors Colors { get; set; }

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

        private Category _selectedItem;

        public Category SelectedItem
        {
            get { return _selectedItem; }
            set 
            { 
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }


        private string _categoryName;

        public string CategoryName
        {
            get { return _categoryName; }
            set { _categoryName = value; }
        }

        public ICommand NavigateProjectsCommand { get; }

        public CategoriesViewModel(NavigationStore navigationStore)
        {
            NavigateProjectsCommand = new NavigateCommand<ProjectsViewModel>(navigationStore, () => new ProjectsViewModel(new Category { Name = SelectedItem.Name }));
            Categories = new ObservableCollection<Category>();
            Colors = new Colors();
            UpdateCategories();

            NewCategory = new RelayCommand(o =>
            {
                string path = $@"{Properties.Settings1.Default.CategoriesPath}\{CategoryName}";
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
                Categories.Clear();
                UpdateCategories();
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
                foreach (string path in Directory.GetDirectories(Properties.Settings1.Default.CategoriesPath))
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
                            Color = Colors.ListOfColors.Find(color => color.Name == "Blue"),
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
