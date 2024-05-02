using System.ComponentModel.DataAnnotations.Schema;

namespace ZiniTechERPSystem.Data
{
    public class AuditLog
    {
        public int Id { get; set; }

        public string EntityName { get; set; } = string.Empty;

        public string ActionType { get; set; } = string.Empty;

        public string? UserRole { get; set; } = string.Empty;

        public int? RowNumber { get; set; }

        public string? UserId { get; set; } = string.Empty;

        public ApplicationUser? User { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
