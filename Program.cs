using Microsoft.EntityFrameworkCore;
using TechOpsDashboard.Data;
using TechOpsDashboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<TechOpsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<TechOpsDbContext>();

    if (!context.Technicians.Any())
    {
        var technicians = new List<Technician>
        {
            new Technician { FullName = "Marcus Reed", Specialty = "Mechanical", Shift = "1st Shift" },
            new Technician { FullName = "Angela Brooks", Specialty = "Electrical", Shift = "2nd Shift" },
            new Technician { FullName = "Derrick Cole", Specialty = "Controls", Shift = "3rd Shift" }
        };

        var equipment = new List<Equipment>
        {
            new Equipment { EquipmentName = "Conveyor_01", Area = "Inbound", EquipmentType = "Conveyor" },
            new Equipment { EquipmentName = "Lift_02", Area = "Storage", EquipmentType = "Lift" },
            new Equipment { EquipmentName = "Scanner_03", Area = "Outbound", EquipmentType = "Scanner" },
            new Equipment { EquipmentName = "Sorter_04", Area = "Sortation", EquipmentType = "Sorter" }
        };

        context.Technicians.AddRange(technicians);
        context.Equipment.AddRange(equipment);
        context.SaveChanges();

        context.WorkOrders.AddRange(
            new WorkOrder
            {
                Title = "Conveyor belt slipping",
                IssueDescription = "Inbound conveyor belt is slipping during heavy load cycles.",
                Priority = "High",
                Status = "Open",
                EquipmentId = equipment[0].Id,
                TechnicianId = technicians[0].Id,
                CreatedAt = DateTime.Now.AddHours(-5)
            },
            new WorkOrder
            {
                Title = "Scanner misread errors",
                IssueDescription = "Outbound scanner is producing intermittent barcode misreads.",
                Priority = "Medium",
                Status = "In Progress",
                EquipmentId = equipment[2].Id,
                TechnicianId = technicians[2].Id,
                CreatedAt = DateTime.Now.AddDays(-1)
            },
            new WorkOrder
            {
                Title = "Lift hydraulic inspection",
                IssueDescription = "Routine inspection needed for lift hydraulic system.",
                Priority = "Low",
                Status = "Completed",
                EquipmentId = equipment[1].Id,
                TechnicianId = technicians[1].Id,
                CreatedAt = DateTime.Now.AddDays(-3),
                CompletedAt = DateTime.Now.AddDays(-1)
            },
            new WorkOrder
            {
                Title = "Sorter motor overheating",
                IssueDescription = "Sortation motor temperature exceeded operating threshold.",
                Priority = "Critical",
                Status = "Open",
                EquipmentId = equipment[3].Id,
                TechnicianId = technicians[1].Id,
                CreatedAt = DateTime.Now.AddMinutes(-90)
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
