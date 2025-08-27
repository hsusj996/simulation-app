using System.ComponentModel;

namespace simulation_app.Models
{
    public class AnalogInput : INotifyPropertyChanged
    {
        string _name;
        double _value;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public double Value { get { return _value; } set { _value = value; OnPropertyChanged("Value"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n)); }
    }
}
