using TestTask.Interfaces;
using TestTask.Models;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services
{
    public class DataProvider : IDataProvider
    {
        
        private IDbFolder _dbFolder;
        public DataProvider(IDbFolder dbFolder)
        {
            _dbFolder = dbFolder;
        }
        public async Task<List<Folder>> GetFolderAsync(string path)
        {
            return  await _dbFolder.GetFolderAsync(path);
        }
    }
}
