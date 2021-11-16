using Dashboard.MVVM.Model;
using Dashboard.MVVM.ViewModel;
using Dashboard.Stores;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Dashboard
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            string path = @"C:\ProManager\Projects";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            NavigationStore navigationStore = new NavigationStore();

            navigationStore.CurrentViewModel = new HomeViewModel();

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(navigationStore)
            };
            MainWindow.Show();
            MainWindow.Show();
            base.OnStartup(e);
        }


    }
}
