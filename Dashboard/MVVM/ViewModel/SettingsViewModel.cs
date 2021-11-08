using Dashboard.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using System.Windows.Input;
using Dashboard.Commands;

namespace Dashboard.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {
        private string _projectsPath;

        public string ProjectsPath
        {
            get { return _projectsPath; }
            set 
            { 
                _projectsPath = value;
                OnPropertyChanged(nameof(ProjectsPath));
            }
        }


        public ICommand BrowseCommand { get; }

        public SettingsViewModel()
        {
            ProjectsPath = Properties.Settings1.Default.CategoriesPath;

            BrowseCommand = new BrowseFoldersCommand(this);
        }
    }
}
