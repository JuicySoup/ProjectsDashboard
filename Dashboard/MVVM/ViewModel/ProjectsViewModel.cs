using Dashboard.Commands;
using Dashboard.MVVM.Exceptions;
using Dashboard.MVVM.Model;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;

namespace Dashboard.MVVM.ViewModel
{
    class ProjectsViewModel
    {
        public ObservableCollection<Project> Projects { get; set; }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Client> Clients { get; set; }

        public ICommand NewCategory { get; }
        public int ProjectsCount { get; set; }

        public int InProgressCount { get; set; }

        public int DoneCount { get; set; }

        public ProjectsViewModel()
        {
            Projects = new ObservableCollection<Project>();
            Clients = new ObservableCollection<Client>();
            Categories = new ObservableCollection<Category>();
            UpdateCategories();
            ProjectsCount += Projects.Count;
            NewCategory = new NewCategoryCommand(this);
        }

        public void AddCategory(Category category)
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

        public void UpdateCategories()
        {
            try
            {
                foreach (string dirs in Directory.GetDirectories(Properties.Settings1.Default.ProjectPath))
                {
                    string name = dirs.Remove(0, dirs.LastIndexOf('\\') + 1);
                    AddCategory(new Category()
                    {
                        Name = name
                    });
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("The process failed: {0}", e.ToString());
            }
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
