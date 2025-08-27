using System.Collections.ObjectModel;
using simulation_app.Models;
using simulation_app.Services;

namespace simulation_app.ViewModels
{
    public class ManualInputsViewModel : BaseNotify
    {
        public ObservableCollection<DigitalInput> DigitalInputs { get; } = new ObservableCollection<DigitalInput>();
        public ObservableCollection<AnalogInput> AnalogInputs { get; } = new ObservableCollection<AnalogInput>();
        public ObservableCollection<SerialInput> SerialInputs { get; } = new ObservableCollection<SerialInput>();

        public RelayCommand ReloadCmd { get; }
        public RelayCommand ResetCmd { get; }

        private readonly IInputLoader _loader;

        public ManualInputsViewModel(IInputLoader loader)
        {
            _loader = loader;
            Reload();

            ReloadCmd = new RelayCommand(Reload);
            ResetCmd = new RelayCommand(ResetValuesOnly);
        }

        private void Reload()
        {
            DigitalInputs.Clear(); AnalogInputs.Clear(); SerialInputs.Clear();
            foreach (var d in _loader.LoadDigitalInputs()) DigitalInputs.Add(d);
            foreach (var a in _loader.LoadAnalogInputs()) AnalogInputs.Add(a);
            foreach (var s in _loader.LoadSerialInputs()) SerialInputs.Add(s);
        }

        // 인메모리 상태만 초기화 (목록은 유지)
        private void ResetValuesOnly()
        {
            foreach (var d in DigitalInputs) d.IsOn = false;
            foreach (var a in AnalogInputs) a.Value = 0.0;
            foreach (var s in SerialInputs) s.Text = "";
        }
    }
}
