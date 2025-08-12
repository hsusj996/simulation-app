using NativeBridge.Interfaces;
using NativeBridge.Implementations;
using System;

namespace simulation_app.Services
{
    public class NativeDataService : IDataService
    {
        private readonly IDataAccess _dataAccess;

        public string ReadFromFile(string path)
        {
            throw new NotImplementedException();
        }

        public bool WriteToFile(string path, string content)
        {
            throw new NotImplementedException();
        }
    }
}
