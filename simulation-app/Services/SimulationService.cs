using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Threading;
using simulation_app.Models;

namespace simulation_app.Services
{
    public class SimulationService
    {
        private readonly Tank _tank;
        private readonly DispatcherTimer _timer;
        private readonly Stopwatch _sw = new Stopwatch();
        private const double DefaultIntervalSec = 0.1; // 100ms

        public bool IsRunning { get; private set; }

        public SimulationService(Tank tank)
        {
            _tank = tank;
            _timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(DefaultIntervalSec) };
            _timer.Tick += OnTick;
        }

        public void Start()
        {
            if (IsRunning) return;
            IsRunning = true;
            _sw.Restart();
            _timer.Start();
        }

        public void Stop()
        {
            if (!IsRunning) return;
            _timer.Stop();
            _sw.Stop();
            IsRunning = false;
        }

        public void Reset()
        {
            Stop();
            _tank.Level = 0;
            foreach (var v in _tank.Valves) v.IsOpen = false;
            foreach (var s in _tank.Sensors) s.IsOn = false;
        }

        private void OnTick(object sender, EventArgs e)
        {
            // 유량 계산
            double inflow = _tank.Valves.Where(v => v.IsOpen && v.Type == ValveType.In).Sum(v => v.FlowRate);
            double outflow = _tank.Valves.Where(v => v.IsOpen && v.Type == ValveType.Out).Sum(v => v.FlowRate);

            double dt = DefaultIntervalSec; // 간단히 고정 프레임 (Stopwatch 사용 가능)
            double newLevel = _tank.Level + (inflow - outflow) * dt;

            // 경계
            if (newLevel < 0) newLevel = 0;
            if (newLevel > _tank.Capacity) newLevel = _tank.Capacity;
            _tank.Level = newLevel;

            // 센서 업데이트
            foreach (var s in _tank.Sensors)
                s.IsOn = (_tank.Level >= s.TriggerLevel);
        }
    }
}
