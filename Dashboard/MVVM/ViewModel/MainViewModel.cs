using Dashboard.Core;

namespace Dashboard.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand CategoriesViewCommand { get; set; }

        public RelayCommand SettingsViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public CategoriesViewModel CategoriesVM { get; set; }

        public SettingsViewModel SettingsVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();

            SettingsVM = new SettingsViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            CategoriesViewCommand = new RelayCommand(o =>
            {
                CategoriesVM = new CategoriesViewModel();
                CurrentView = CategoriesVM;
            });

            SettingsViewCommand = new RelayCommand(o =>
            {
                CurrentView = SettingsVM;
            });
        }
    }
}
