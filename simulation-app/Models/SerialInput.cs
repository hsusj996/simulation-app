using System.ComponentModel;

namespace simulation_app.Models
{
    public class SerialInput : INotifyPropertyChanged
    {
        string _name;
        string _text;

        public string Name { get { return _name; } set { _name = value; OnPropertyChanged("Name"); } }
        public string Text { get { return _text; } set { _text = value; OnPropertyChanged("Text"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string n) { var h = PropertyChanged; if (h != null) h(this, new PropertyChangedEventArgs(n)); }
    }
}
