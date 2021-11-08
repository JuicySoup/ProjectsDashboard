using Dashboard.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard.Commands
{
    class BrowseFoldersCommand : CommandBase
    {
        private readonly SettingsViewModel _settingsViewModel;
        public BrowseFoldersCommand(SettingsViewModel settingsViewModel)
        {
            _settingsViewModel = settingsViewModel;
        }
        public override void Execute(object parameter)
        {
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new();

            openFileDlg.SelectedPath = Properties.Settings1.Default.CategoriesPath;

            System.Windows.Forms.DialogResult result = openFileDlg.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                Properties.Settings1.Default.CategoriesPath = openFileDlg.SelectedPath;
                Properties.Settings1.Default.Save();
                _settingsViewModel.ProjectsPath = openFileDlg.SelectedPath;
            }
            else if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
        }


    }
}
