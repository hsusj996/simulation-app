using System.ComponentModel;

namespace simulation_app.Models
{
    public class DigitalInput : INotifyPropertyChanged
    {
        string _name;
        bool _isOn;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public bool IsOn { get { return _isOn; } set { _isOn = value; OnPropertyChanged("IsOn"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n)); }
    }
}
