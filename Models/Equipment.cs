namespace TechOpsDashboard.Models
{
    public class Equipment
    {
        public int Id { get; set; }

        public string EquipmentName { get; set; } = string.Empty;
        public string Area { get; set; } = string.Empty;
        public string EquipmentType { get; set; } = string.Empty;

        public List<WorkOrder> WorkOrders { get; set; } = new();
    }
}
