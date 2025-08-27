// Navigation/NavigationStore.cs
using simulation_app.ViewModels;
using System;

namespace simulation_app.Navigation
{
    public class NavigationStore
    {
        public BaseNotify CurrentViewModel { get; private set; }
        public event Action CurrentViewModelChanged;

        public void Set(BaseNotify vm)
        {
            CurrentViewModel = vm;
            CurrentViewModelChanged?.Invoke();
        }
    }
}
