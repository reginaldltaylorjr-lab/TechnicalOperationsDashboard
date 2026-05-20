namespace TechOpsDashboard.Models
{
    public class Technician
    {
        public int Id { get; set; }

        public string FullName { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string Shift { get; set; } = string.Empty;

        public List<WorkOrder> WorkOrders { get; set; } = new();
    }
}
