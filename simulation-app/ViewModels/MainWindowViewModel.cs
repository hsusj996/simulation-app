// ViewModels/MainWindowViewModel.cs
using simulation_app.Navigation;

namespace simulation_app.ViewModels
{
    public class MainWindowViewModel : BaseNotify
    {
        private readonly NavigationStore _store;
        public BaseNotify CurrentViewModel => _store.CurrentViewModel;

        public RelayCommand GoSimulatorCmd { get; }
        public RelayCommand GoManualCmd { get; }

        public MainWindowViewModel(NavigationStore store,
            INavigationService goSimulator,
            INavigationService goManual)
        {
            _store = store;

            _store.CurrentViewModelChanged += () => OnPropertyChanged(nameof(CurrentViewModel));

            GoSimulatorCmd = new RelayCommand(() => goSimulator.Navigate());
            GoManualCmd = new RelayCommand(() => goManual.Navigate());
        }
    }
}
