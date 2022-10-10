using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TestTask.Models;
using TestTask.Interfaces; 

namespace TestTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IDataProvider _provider;
        public HomeController(ILogger<HomeController> logger, IDataProvider dp)
        {
            _logger = logger;
            _provider = dp;
        }
        [HttpGet("{**path}")]
        public IActionResult Index(string path)
        {
            if (path==null)
            {
                return RedirectToAction( "Index","Home", new {path= "Creating%Digital%Images" });                
            }
            List<Folder> folder = _provider.GetFolderAsync(path).Result;
            return View(folder);
        }       

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}