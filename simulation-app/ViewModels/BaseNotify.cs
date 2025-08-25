using System.ComponentModel;

namespace simulation_app.ViewModels
{
    public abstract class BaseNotify : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n)
        {
            var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n));
        }
    }
}
