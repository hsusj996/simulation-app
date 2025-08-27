using System.Collections.Generic;
using simulation_app.Models;

namespace simulation_app.Services
{
    public class MockInputLoader : IInputLoader
    {
        public IEnumerable<DigitalInput> LoadDigitalInputs()
        {
            return new[]
            {
                new DigitalInput{ Name="DI_PumpRun" , IsOn=false },
                new DigitalInput{ Name="DI_ValveOpen", IsOn=true  },
                new DigitalInput{ Name="DI_Alarm"    , IsOn=false },
            };
        }
        public IEnumerable<AnalogInput> LoadAnalogInputs()
        {
            return new[]
            {
                new AnalogInput{ Name="AI_TankLevel" , Value=123.4 },
                new AnalogInput{ Name="AI_Pressure"  , Value=2.15  },
                new AnalogInput{ Name="AI_Temperature", Value=24.7 },
            };
        }
        public IEnumerable<SerialInput> LoadSerialInputs()
        {
            return new[]
            {
                new SerialInput{ Name="SI_CMD_1", Text="START" },
                new SerialInput{ Name="SI_CMD_2", Text="STATUS?" },
                new SerialInput{ Name="SI_CMD_3", Text="" }
            };
        }
    }
}
