using Dashboard.MVVM.Exceptions;
using Dashboard.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace Dashboard.MVVM.ViewModel
{
    class ProjectsViewModel
    {
        public ObservableCollection<Project> Projects { get; set; }
        public ObservableCollection<Client> Clients { get; set; }
        public int ProjectsCount { get; set; }

        public int InProgressCount { get; set; }

        public int DoneCount { get; set; }

        public ProjectsViewModel()
        {
            Projects = new ObservableCollection<Project>();
            Clients = new ObservableCollection<Client>();
            SeedProjects();
            ProjectsCount += Projects.Count;
        }

        public void AddProject(Project project)
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

        public void GetDirs(string foldername)
        {
            try
            {
                foreach (string dirs in Directory.GetDirectories($@"D:\Projects\{foldername}"))
                {
                    string name = dirs.Remove(0, dirs.LastIndexOf('\\') + 1);
                    AddProject(new Project()
                    {
                        ProjectName = name,
                        Category = foldername.ToLower()
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
        }
        public void SeedProjects()
        {
            GetDirs("FREELANCE");
            GetDirs("PERSONAL");
        }

        public void CreateProject(Project project)
        {
            foreach (Project existingProject in Projects)
            {
                if (existingProject.Conflicts(project))
                {
                    throw new ProjectConflictException(existingProject, project);
                }
            }
        }
    }
}
