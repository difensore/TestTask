using TestTask.Models;

namespace TestTask.Interfaces
{
    public interface IFoldersManager
    {
        public List<Folder> Folders { get; set; } 
        public string currentPath { get; set; }
    }
}
