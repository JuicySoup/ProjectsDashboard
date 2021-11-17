using Dashboard.Core;
using Dashboard.MVVM.Exceptions;
using Dashboard.MVVM.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dashboard.MVVM.ViewModel
{
    public class ProjectsViewModel : ObservableObject
    {
        private readonly Category _category;

        public string CategoryName => _category.Name;

        public int ProjectsCount { get; set; }

        public ObservableCollection<Project> Projects { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

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
        private string _projectName;

        public string ProjectName
        {
            get { return _projectName; }
            set { _projectName = value; }
        }

        private string _client;

        public string Client
        {
            get { return _client; }
            set { _client = value; }
        }

        
        public ICommand NewProject { get; }

        public ProjectsViewModel(Category category)
        {
            _category = category;
            Projects = new ObservableCollection<Project>();
            Colors = new Colors();
            NewProject = new RelayCommand(o =>
            {
                string path = $@"{Properties.Settings1.Default.CategoriesPath}\{CategoryName}\{ProjectName}";
                if (!Directory.Exists(path))
                {
                    System.Diagnostics.Trace.WriteLine("exists");
                    Directory.CreateDirectory(path);

                    Project data = new()
                    {
                        Name = ProjectName,
                        Created = DateTime.Now.ToString("yyyy-MM-dd"),
                        Color = SelectedColor,
                        Client = Client,
                        Details = "test"
                        
                    };
                    Projects.Add(data);
                    WriteSettingsFile(path, data);
                    ProjectsCount++;
                    OnPropertyChanged(nameof(ProjectsCount));

                }
            });
            UpdateProjects();
        }

        public static void WriteSettingsFile(string path, Project data)
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

        public void AddToProjects(Project project)
        {
            foreach (Project existingProject in Projects)
            {
                if (existingProject.Conflicts(project))
                {
                    throw new ProjectConflictException(existingProject, project);
                }
            }
            Projects.Add(project);
        }

        public void UpdateProjects()
        {
            try
            {
                foreach (string path in Directory.GetDirectories(@$"{Properties.Settings1.Default.CategoriesPath}\{CategoryName}"))
                {
                    string jsonpath = @$"{path}\settings.json";
                    if (File.Exists(jsonpath))
                    {
                        using StreamReader json = File.OpenText(jsonpath);
                        JsonSerializer serializer = new();
                        Project project = (Project)serializer.Deserialize(json, typeof(Project));
                        AddToProjects(project);
                    }
                    else
                    {
                        string name = path.Remove(0, path.LastIndexOf('\\') + 1);

                        Project data = new()
                        {
                            Name = name,
                            Color = Colors.ListOfColors.Find(color => color.Name == "Blue"),
                            Created = DateTime.Now.ToString("yyyy-MM-dd"),
                            Details = "",
                            Client = null,
                            InProgress = false

                        };
                        AddToProjects(data);
                        WriteSettingsFile(path, data);
                    }
                }
                ProjectsCount = Projects.Count;
            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
    }
}
