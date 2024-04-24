using Microsoft.EntityFrameworkCore;
using ZiniTechERPSystem.Data;

namespace ZiniTechERPSystem.Components.Services
{
    public class LogService
    {
        private readonly ApplicationDbContext db;

        public LogService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateLog(AuditLog Log)
        {
            db.Logs.Add(Log);
            db.SaveChanges();
        }

        public List<AuditLog> GetLogs(double Page)
        {
            return db.Logs.Skip(((int)Page - 1) * 5).Take(5).Include(l => l.User).ToList();
        }

        public int GetTotalLogsCount()
        {
            return db.Logs.ToList().Count();
        }
    }
}
