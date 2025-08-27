using System;
using System.Collections.ObjectModel;
using System.Linq;
using simulation_app.Models;
using simulation_app.Services;

namespace simulation_app.ViewModels
{
    public class SimulatorViewModel : BaseNotify
    {
        public Tank Tank { get; } = new Tank();
        public SimulationService Sim { get; }

        // 뷰 바인딩용 계산 프로퍼티
        private const double TankPixelHeight = 360.0;
        public double WaterHeight
        {
            get
            {
                if (Tank.Capacity <= 0) return 0;
                return TankPixelHeight * (Tank.Level / Tank.Capacity);
            }
        }

        public ObservableCollection<Valve> Valves => Tank.Valves;
        public ObservableCollection<LevelSensor> Sensors => Tank.Sensors;

        public RelayCommand StartCmd { get; }
        public RelayCommand StopCmd { get; }
        public RelayCommand ResetCmd { get; }
        public RelayCommand AddInValveCmd { get; }
        public RelayCommand AddOutValveCmd { get; }
        public RelayCommand AddSensorCmd { get; }
        public RelayCommand RemoveSelectedValveCmd { get; }
        public RelayCommand RemoveSelectedSensorCmd { get; }

        private Valve _selectedValve;
        public Valve SelectedValve
        {
            get => _selectedValve;
            set { _selectedValve = value; OnPropertyChanged("SelectedValve"); }
        }

        private LevelSensor _selectedSensor;
        public LevelSensor SelectedSensor
        {
            get => _selectedSensor;
            set { _selectedSensor = value; OnPropertyChanged("SelectedSensor"); }
        }

        public SimulatorViewModel()
        {
            // 초기값
            Tank.Capacity = 1000;
            Tank.Level = 0;
            Tank.Valves.Add(new Valve { Name = "In-1", Type = ValveType.In, FlowRate = 8 });
            Tank.Valves.Add(new Valve { Name = "Out-1", Type = ValveType.Out, FlowRate = 5 });
            Tank.Sensors.Add(new LevelSensor { Name = "S-600", TriggerLevel = 600 });
            Tank.Sensors.Add(new LevelSensor { Name = "S-300", TriggerLevel = 300 });

            // Simulation
            Sim = new SimulationService(Tank);

            // Commands
            StartCmd = new RelayCommand(() => Sim.Start(), () => !Sim.IsRunning && Tank.Capacity > 0);
            StopCmd = new RelayCommand(() => Sim.Stop(), () => Sim.IsRunning);
            ResetCmd = new RelayCommand(() => { Sim.Reset(); RaiseLayoutChanges(); });

            AddInValveCmd = new RelayCommand(() => Valves.Add(new Valve { Name = "In-" + (Valves.Count(v => v.Type == ValveType.In) + 1), Type = ValveType.In, FlowRate = 5 }));
            AddOutValveCmd = new RelayCommand(() => Valves.Add(new Valve { Name = "Out-" + (Valves.Count(v => v.Type == ValveType.Out) + 1), Type = ValveType.Out, FlowRate = 5 }));
            AddSensorCmd = new RelayCommand(() => Sensors.Add(new LevelSensor { Name = "S-" + (Sensors.Count + 1), TriggerLevel = Math.Min(Tank.Capacity, (Sensors.Count + 1) * 100) }));

            RemoveSelectedValveCmd = new RelayCommand(() => { if (SelectedValve != null) Valves.Remove(SelectedValve); });
            RemoveSelectedSensorCmd = new RelayCommand(() => { if (SelectedSensor != null) Sensors.Remove(SelectedSensor); });

            // 모델 변경 시 뷰 갱신: Level/Capacity가 바뀌면 WaterHeight 재계산 필요
            Tank.PropertyChanged += (s, e) =>
            {
                if (e.PropertyName == "Level" || e.PropertyName == "Capacity")
                    OnPropertyChanged("WaterHeight");
            };
        }

        private void RaiseLayoutChanges()
        {
            OnPropertyChanged("WaterHeight");
        }
    }
}
