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

        public List<AuditLog> GetLogs()
        {
            return db.Logs.Include(l => l.User).ToList();
        }
    }
}
