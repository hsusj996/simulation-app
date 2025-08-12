namespace simulation_app.Services
{
    public interface IDataService
    {
        string ReadFromFile(string path);
        bool WriteToFile(string path, string content);
    }
}
