namespace TechOpsDashboard.Models
{
    public class WorkOrder
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;
        public string IssueDescription { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;

        public int EquipmentId { get; set; }
        public Equipment? Equipment { get; set; }

        public int TechnicianId { get; set; }
        public Technician? Technician { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? CompletedAt { get; set; }
    }
}
