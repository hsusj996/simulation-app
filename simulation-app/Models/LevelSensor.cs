using System.ComponentModel;

namespace simulation_app.Models
{
    public class LevelSensor : INotifyPropertyChanged
    {
        private string _name;
        private double _triggerLevel; // L
        private bool _isOn;

        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }
        public double TriggerLevel { get => _triggerLevel; set { _triggerLevel = value; OnPropertyChanged("TriggerLevel"); } }
        public bool IsOn { get => _isOn; set { _isOn = value; OnPropertyChanged("IsOn"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n)); }
    }
}
