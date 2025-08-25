using System.ComponentModel;

namespace simulation_app.Models
{
    public enum ValveType { In, Out }

    public class Valve : INotifyPropertyChanged
    {
        private string _name;
        private ValveType _type;
        private double _flowRate;  // L/s
        private bool _isOpen;

        public string Name { get => _name; set { _name = value; OnPropertyChanged("Name"); } }
        public ValveType Type { get => _type; set { _type = value; OnPropertyChanged("Type"); } }
        public double FlowRate { get => _flowRate; set { _flowRate = value; OnPropertyChanged("FlowRate"); } }
        public bool IsOpen { get => _isOpen; set { _isOpen = value; OnPropertyChanged("IsOpen"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n)); }
    }
}
