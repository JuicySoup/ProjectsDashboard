using Dashboard.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Commands
{
    class NewCategoryCommand : CommandBase
    {
        private readonly ProjectsViewModel _projectsViewModel;
        public NewCategoryCommand(ProjectsViewModel projectsViewModel)
        {
            _projectsViewModel = projectsViewModel;
        }
        public override void Execute(object parameter)
        {
            string path = @"C:\ProManager\Projects\Test";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }
    }
}
