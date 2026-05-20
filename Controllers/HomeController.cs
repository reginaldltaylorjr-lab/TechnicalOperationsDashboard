using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TechOpsDashboard.Data;
using TechOpsDashboard.Models;
using TechOpsDashboard.ViewModels;

namespace TechOpsDashboard.Controllers
{
    public class HomeController : Controller
    {
        private readonly TechOpsDbContext _context;

        public HomeController(TechOpsDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string searchTerm, string statusFilter, string priorityFilter, string areaFilter)
        {
            var workOrders = _context.WorkOrders
                .Include(w => w.Equipment)
                .Include(w => w.Technician)
                .ToList();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                workOrders = workOrders
                    .Where(w =>
                        (!string.IsNullOrEmpty(w.Title) &&
                         w.Title.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        ||
                        (!string.IsNullOrEmpty(w.IssueDescription) &&
                         w.IssueDescription.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        ||
                        (w.Equipment != null &&
                         !string.IsNullOrEmpty(w.Equipment.EquipmentName) &&
                         w.Equipment.EquipmentName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                        ||
                        (w.Technician != null &&
                         !string.IsNullOrEmpty(w.Technician.FullName) &&
                         w.Technician.FullName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    )
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(statusFilter))
            {
                workOrders = workOrders
                    .Where(w => !string.IsNullOrEmpty(w.Status) && w.Status == statusFilter)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(priorityFilter))
            {
                workOrders = workOrders
                    .Where(w => !string.IsNullOrEmpty(w.Priority) && w.Priority == priorityFilter)
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(areaFilter))
            {
                workOrders = workOrders
                    .Where(w => w.Equipment != null &&
                                !string.IsNullOrEmpty(w.Equipment.Area) &&
                                w.Equipment.Area == areaFilter)
                    .ToList();
            }

            var viewModel = new DashboardViewModel
            {
                WorkOrders = workOrders
            };

            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
