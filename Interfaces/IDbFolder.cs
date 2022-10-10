using TestTask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask.Interfaces
{
    public interface IDbFolder
    {
        public Task<List<Folder>> GetFolderAsync(string path);
    }
}
