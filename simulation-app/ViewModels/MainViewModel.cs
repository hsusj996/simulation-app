using NativeBridge.Implementations;
using NativeBridge.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_app.Infrastructure
{
    internal class MainViewModel
    {
        private readonly IDataAccess _dataAccess;

        public MainViewModel()
        {
            _dataAccess = new DataAccessWrapper();
        }

        public void TestReadWrite()
        {
            string path = "test.txt";
            _dataAccess.WriteToFile(path, "Hello from C# to C++!");
            string content = _dataAccess.ReadFromFile(path);
            Console.WriteLine($"[NativeLib 읽은 데이터] {content}");
        }
    }
}
