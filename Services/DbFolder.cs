using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask.Interfaces;
using TestTask.Models;

namespace TestTask.Services
{
    public class DbFolder : IDbFolder
    {
        private FolderContext db;
        public DbFolder(FolderContext context)
        {
            db = context;
            Folder PrimarySources = new Folder() { Name = "Primary%Sources" };
            Folder SecondarySources = new Folder() { Name = "Secondary%Sources" };
            Folder Process = new Folder() { Name = "Process" };
            Folder FinalProcess = new Folder() { Name = "Final%Process" };
            Folder Evidence = new Folder() { Name = "Evidence" };
            db.AddRange(PrimarySources, SecondarySources, Process, FinalProcess, Evidence);
            db.SaveChanges();
            Folder Resources = new Folder() { Name = "Resources", Entity = $"{PrimarySources.Id},{SecondarySources.Id}" };
            Folder GraphicProduct = new Folder() { Name = "Graphic%Product", Entity = $"{Process.Id},{FinalProcess.Id}" };
            db.AddRange(Resources, GraphicProduct);
            db.SaveChanges();
            Folder CreatingDigitalImages = new Folder() { Name = "Creating%Digital%Images", Entity = $"{Evidence.Id},{Resources.Id},{GraphicProduct.Id}" };
            db.Add(CreatingDigitalImages);
            db.SaveChanges();
        }
        public async Task<List<Folder>> GetFolderAsync(string path)
        {
            string[] Foldernames = path.Split('/');
            Folder c = db.Folders.First(x => x.Name == Foldernames[0]);
            Folder currentFolder = c;
            List<Folder> folderList = new List<Folder> { };
            if (Foldernames.Length == 1)
            {
                folderList.Add(c);
                if (currentFolder.Entity != null)
                {
                    foreach (var item in currentFolder.Entity.Split(','))
                    {
                        var _id = Int32.Parse(item);
                        folderList.Add(await db.Folders.FirstAsync(x => x.Id == _id));
                    }
                    return folderList;
                }
                return folderList;
            }
            for (int i = 1; i < Foldernames.Length; i++)
            {
                string[] folderId = currentFolder.Entity.Split(',');
                foreach (string id in folderId)
                {
                    int firstId = db.Folders.First(x => x.Name == Foldernames[i]).Id;
                    if (firstId == Int32.Parse(id))
                    {
                        currentFolder = await db.Folders.FirstAsync(x => x.Name == Foldernames[i]);
                        break;
                    }
                }
                if (currentFolder == c)
                {
                    currentFolder = null;
                    break;
                }
            }
            if (currentFolder == null)
            {
                return folderList;
            }
            folderList.Add(currentFolder);
            if (currentFolder.Entity != null)
            {
                foreach (var item in currentFolder.Entity.Split(','))
                {
                    var _id = Int32.Parse(item);
                    folderList.Add(await db.Folders.FirstAsync(x => x.Id == _id));
                }
            }
            return folderList;
        }
    }
}
