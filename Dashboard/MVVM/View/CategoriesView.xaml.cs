﻿using Dashboard.MVVM.ViewModel;
using Dashboard.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Dashboard.MVVM.View
{
    /// <summary>
    /// Interaction logic for ProjectsView.xaml
    /// </summary>
    public partial class CategoriesView : UserControl
    {
        public CategoriesView()
        {
            InitializeComponent();
            NavigationStore navigationStore = new();
            DataContext = new CategoriesViewModel(navigationStore);
        }

        private void OnShowModalClick(object sender, RoutedEventArgs e)
        {
            Modal.IsOpen = true;
        }
        private void OnCloseModalClick(object sender, RoutedEventArgs e)
        {
            Modal.IsOpen = false;
        }
    }
}
