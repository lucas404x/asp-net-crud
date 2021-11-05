using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.Data;
using TodoList.Models;
using TodoList.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace TodoList.Controllers 
{
    public class TasksController : Controller 
    {
        private AppDbContext _db;

        public TasksController(AppDbContext appDbContext) 
        {
            _db = appDbContext;
        }

        [HttpGet("Tasks/")]
        public async Task<IActionResult> TasksAsync() 
        {
            TasksViewModel tasksViewModel = new TasksViewModel();
            tasksViewModel.Tasks = await _db.Tasks.AsNoTracking().ToListAsync();
            return View(tasksViewModel);
        }


        [HttpGet("Tasks/Add")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("Tasks/Add")]
        public async Task<IActionResult> PostAsync([FromForm] AddTaskModel task) 
        {
            await _db.AddAsync(new TaskModel 
            {
                Title = task.Title,
                Description = task.Description,
                Done = false,
                LastTimeUpdated = System.DateTime.Now
            });

            await _db.SaveChangesAsync();

            return Redirect("/Tasks");
        }

        [HttpGet("Tasks/Edit/{id}")]
        public async Task<IActionResult> EditAsync(string id) 
        {
            TaskModel task = await _db.Tasks.AsNoTracking().FirstOrDefaultAsync((task) => task.Id.Equals(int.Parse(id)));
            if (task == null) 
            {
                return NotFound();
            }

            return View(task);
        }

        [HttpPost("Tasks/Edit/{id}")]
        public async Task<IActionResult> EditAsync(string id, [FromForm] TaskModel task) 
        {
            int taskId = int.Parse(id);
            TaskModel dbEntity = await _db.Tasks.FirstOrDefaultAsync((_task) => _task.Id.Equals(taskId));
            if (dbEntity == null) { return NotFound(); }

            dbEntity.Title = task.Title;
            dbEntity.Description = task.Description;
            dbEntity.Done = task.Done;

            await _db.SaveChangesAsync();

            return Redirect("/Tasks");
        }
    }
}