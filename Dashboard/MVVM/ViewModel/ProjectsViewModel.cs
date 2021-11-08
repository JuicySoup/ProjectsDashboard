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

namespace Dashboard.MVVM.ViewModel
{
    public class ProjectsViewModel : ObservableObject
    {
        public ObservableCollection<Project> Projects { get; set; }
        public ObservableCollection<Client> Clients { get; set; }


        public List<Color> Colors { get; set; }

        public ProjectsViewModel()
        {
            Projects = new ObservableCollection<Project>();
            Colors = new List<Color>();
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

        //public void UpdateProjects()
        //{
        //    try
        //    {
        //        foreach (string path in Directory.GetDirectories(Properties.Settings1.Default.CategoriesPath))
        //        {
        //            string jsonpath = @$"{path}\settings.json";
        //            if (File.Exists(jsonpath))
        //            {
        //                using StreamReader json = File.OpenText(jsonpath);
        //                JsonSerializer serializer = new();
        //                Project project = (Project)serializer.Deserialize(json, typeof(Project));
        //                AddToProjects(project);
        //            }
        //            else
        //            {
        //                string name = path.Remove(0, path.LastIndexOf('\\') + 1);

        //                Project data = new()
        //                {
        //                    Name = name,
        //                    Color = Colors.Find(color => color.Name == "Blue"),
        //                    Created = DateTime.Now.ToString("yyyy-MM-dd")
        //                };

        //                AddToProjects(data);
        //                WriteSettingsFile(path, data);
        //            }
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine("The process failed: {0}", e.ToString());
        //    }
        //}
    }
}
