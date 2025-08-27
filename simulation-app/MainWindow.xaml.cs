// MainWindow.xaml.cs
using System;
using System.Windows;
using simulation_app.Navigation;
using simulation_app.Services;
using simulation_app.ViewModels;

namespace simulation_app
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // 간단 조립(DI 없이 수동)
            var store = new NavigationStore();

            // factories
            Func<SimulatorViewModel> simFactory = () => new SimulatorViewModel(); // 기존 MainViewModel 개명
            Func<ManualInputsViewModel> manFactory = () => new ManualInputsViewModel(new MockInputLoader());

            // services
            var goSimulator = new NavigationService<SimulatorViewModel>(store, simFactory);
            var goManual = new NavigationService<ManualInputsViewModel>(store, manFactory);

            // 초기 화면
            store.Set(simFactory());

            DataContext = new MainWindowViewModel(store, goSimulator, goManual);
        }
    }
}
