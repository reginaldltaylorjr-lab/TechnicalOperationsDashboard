using TechOpsDashboard.Models;

namespace TechOpsDashboard.ViewModels
{
    public class DashboardViewModel
    {
        public List<WorkOrder> WorkOrders { get; set; } = new();

        public int TotalWorkOrders => WorkOrders.Count;
        public int OpenWorkOrders => WorkOrders.Count(w => w.Status == "Open");
        public int InProgressWorkOrders => WorkOrders.Count(w => w.Status == "In Progress");
        public int CompletedWorkOrders => WorkOrders.Count(w => w.Status == "Completed");
        public int CriticalWorkOrders => WorkOrders.Count(w => w.Priority == "Critical");
    }
}