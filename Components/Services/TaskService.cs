using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class TaskService
    {
        private readonly ApplicationDbContext db;

        public TaskService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public List<EmployeeTask> GetTasks()
        {
            return db.Tasks.Include(t => t.Asignee).ToList();
        }

        public void AddTask(EmployeeTask task)
        {
            db.Tasks.Add(task);
            db.SaveChanges();
        }

        public List<EmployeeTask> GetTasksByEmployeeId(string EmployeeId)
        {
            return db.Tasks.Where(t => t.AsigneeId == EmployeeId).Include(t => t.Asignee).ToList();
        }
    }
}
