using System.Collections.ObjectModel;
using System.ComponentModel;

namespace simulation_app.Models
{
    public class Tank : INotifyPropertyChanged
    {
        private double _capacity;  // L
        private double _level;     // L

        public double Capacity { get => _capacity; set { _capacity = value; OnPropertyChanged("Capacity"); } }
        public double Level { get => _level; set { _level = value; OnPropertyChanged("Level"); } }

        public ObservableCollection<Valve> Valves { get; } = new ObservableCollection<Valve>();
        public ObservableCollection<LevelSensor> Sensors { get; } = new ObservableCollection<LevelSensor>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n)); }
    }
}
