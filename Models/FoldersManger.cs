using TestTask.Interfaces;

namespace TestTask.Models
{
    public class FoldersManger: IFoldersManager
    {
        public List<Folder> Folders { get; set; }
        public string currentPath { get; set; }
    }
}
