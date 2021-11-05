using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TodoList.Data;
using TodoList.Models;
using Microsoft.EntityFrameworkCore;
using TodoList.ViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TodoList.Controllers
{
    public class HomeController : Controller
    {
        private const int LASTEST_TASKS_LIMIT = 5;
        private readonly AppDbContext _db;
        private readonly ILogger<HomeController> _logger;
        public HomeController(AppDbContext db, ILogger<HomeController> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            List<TaskModel> tasks = await _db.Tasks.AsNoTracking().ToListAsync();
            if (tasks == null || tasks.Count == 0) 
            {
                return View(new List<TaskModel>());
            }

            bool existsTasksEnough = tasks.Count >= LASTEST_TASKS_LIMIT;

            tasks = tasks.GetRange(existsTasksEnough ? tasks.Count - LASTEST_TASKS_LIMIT : 0, 
                                   existsTasksEnough ? LASTEST_TASKS_LIMIT : tasks.Count);
            tasks.Reverse();

            return View(tasks);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
