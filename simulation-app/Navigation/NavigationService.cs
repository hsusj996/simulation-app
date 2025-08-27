// Navigation/NavigationService.cs
using System;
using simulation_app.ViewModels;

namespace simulation_app.Navigation
{
    public class NavigationService<TViewModel> : INavigationService where TViewModel : BaseNotify
    {
        private readonly NavigationStore _store;
        private readonly Func<TViewModel> _factory;

        public NavigationService(NavigationStore store, Func<TViewModel> factory)
        {
            _store = store;
            _factory = factory;
        }

        public void Navigate()
        {
            _store.Set(_factory());
        }
    }
}
