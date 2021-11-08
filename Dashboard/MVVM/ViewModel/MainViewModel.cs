using Dashboard.Commands;
using Dashboard.Core;
using Dashboard.Stores;
using System;
using System.Windows.Input;

namespace Dashboard.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        private readonly NavigationStore _navigationStore;

        public object CurrentViewModel => _navigationStore.CurrentViewModel;

        public ICommand SettingsViewCommand { get; }

        public ICommand CategoriesViewCommand { get; }

        public ICommand HomeViewCommand { get; }


        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;

            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;

            CategoriesViewCommand = new NavigateCommand<CategoriesViewModel>(navigationStore, () => new CategoriesViewModel(navigationStore));

            HomeViewCommand = new NavigateCommand<HomeViewModel>(navigationStore, () => new HomeViewModel());

            SettingsViewCommand = new NavigateCommand<SettingsViewModel>(navigationStore, () => new SettingsViewModel());
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
