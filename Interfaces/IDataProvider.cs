using TestTask.Models;

namespace TestTask.Interfaces
{
    public interface IDataProvider
    {
        public  Task<List<Folder>> GetFolderAsync(string path);

    }
}
