using System.Collections.Generic;
using simulation_app.Models;

namespace simulation_app.Services
{
    public interface IInputLoader
    {
        IEnumerable<DigitalInput> LoadDigitalInputs();
        IEnumerable<AnalogInput> LoadAnalogInputs();
        IEnumerable<SerialInput> LoadSerialInputs();
    }
}
