using Microsoft.EntityFrameworkCore;
using System.Linq;
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

        public List<AuditLog> GetLogs(double Page, int TotalRows)
        {
            return db.Logs.Skip(((int)Page - 1) * TotalRows).Take(TotalRows).Include(l => l.User).ToList();
        }

        public int GetTotalLogsCount()
        {
            return db.Logs.ToList().Count();
        }
    }
}
