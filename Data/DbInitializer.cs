using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Models;
using DiscussionMVCAppOaks.Models.LotModel;
using DiscussionMVCAppOaks.Models.LotTypeModel;
using DiscussionMVCAppOaks.Models.PermitModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace DiscussionMVCAppOaks.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            ApplicationDbContext database
                = services.GetRequiredService<ApplicationDbContext>();

            RoleManager<IdentityRole> roleManager
                = services.GetRequiredService<RoleManager<IdentityRole>>();

            UserManager<ApplicationUser> userManager
                = services.GetRequiredService<UserManager<ApplicationUser>>();


            if (!database.Roles.Any())
            {
                IdentityRole role = new IdentityRole("WVUEmployee");
                await roleManager.CreateAsync(role);

                role = new IdentityRole("ParkingEmployee");
                await roleManager.CreateAsync(role);

                role = new IdentityRole("Visitor");
                await roleManager.CreateAsync(role);

            }

            if (!database.Departments.Any())
            {
                Department department = new Department("MIS", "1601 University Ave, Morgantown, WV 26506");
                database.Departments.Add(department);
                database.SaveChanges();

                department = new Department("Mechanical Engineering", "1306 Evansdale Dr, Morgantown, WV 26506");
                database.Departments.Add(department);
                database.SaveChanges();

                department = new Department("Pediatrics", "1 Medical Center Dr, Morgantown, WV 26506");
                database.Departments.Add(department);
                database.SaveChanges();

                department = new Department("Mechanical Engineering", "1306 Evansdale Dr, Morgantown, WV 26506");
                database.Departments.Add(department);
                database.SaveChanges();

                department = new Department("WVU Parking Office", "Maiden Ln, Morgantown, WV 26506");
                database.Departments.Add(department);
                database.SaveChanges();
            }

            if (!database.ApplicationUsers.Any())
            {

                Visitor visitor = new Visitor("Test", "Visitor1", "TestVisitor1@wvu.edu",
                    "3040000001", "TestVisitor1", "Mylan");
                visitor.EmailConfirmed = true;
                await userManager.CreateAsync(visitor);
                await userManager.AddToRoleAsync(visitor, "Visitor");

                WVUEmployee employee = new WVUEmployee("Test", "WVUEmployee1", "TestWVUEmployee1@wvu.edu",
                    "3040000002", "TestWVUEmployee1", 1);
                employee.EmailConfirmed = true;
                await userManager.CreateAsync(employee);
                await userManager.AddToRoleAsync(employee, "WVUEmployee");

                employee = new WVUEmployee("Test", "WVUEmployee2", "TestWVUEmployee2@wvu.edu",
                    "3040000022", "TestWVUEmployee2", 2);
                employee.EmailConfirmed = true;
                await userManager.CreateAsync(employee);
                await userManager.AddToRoleAsync(employee, "WVUEmployee");

                employee = new WVUEmployee("Test", "WVUEmployee3", "TestWVUEmployee3@wvu.edu",
                    "3040000023", "TestWVUEmployee3", 3);
                employee.EmailConfirmed = true;
                await userManager.CreateAsync(employee);
                await userManager.AddToRoleAsync(employee, "WVUEmployee");

                WVUEmployee parkingEmployee = new WVUEmployee("Test", "ParkingEmployee", "TestParkingEmployee1@wvu.edu",
                    "3040000003", "TestParkingEmployee1", 5);
                employee.EmailConfirmed = true;
                await userManager.CreateAsync(parkingEmployee);
                await userManager.AddToRoleAsync(parkingEmployee, "WVUEmployee");
                await userManager.AddToRoleAsync(parkingEmployee, "ParkingEmployee");

            }


            if (!database.Lots.Any())
            {
                Lot lot = new Lot("1", "Art Museum Education Center", "2 Fine Arts Dr, Morgantown, WV 26505", 140);
                lot.CurrentlyOccupiedSpots = 140;
                database.Lots.Add(lot);
                database.SaveChanges();

                lot = new Lot("4", "Student Rec Center", "2001 Rec Center Dr, Morgantown, WV 26505", 225);
                database.Lots.Add(lot);
                database.SaveChanges();

                lot = new Lot("7", "College of B&E",
                            "600 University Ave, Morgantown, WV 26506", 150);
                database.Lots.Add(lot);
                database.SaveChanges();


                lot = new Lot("10", "WVU Hospital",
                            "1 Medical Center Dr, Morgantown, WV 26506", 300);
                database.Lots.Add(lot);
                database.SaveChanges();

                lot = new Lot("12", "WVU Urgent Care", "301 Suncrest Towne Centre Drive, Morgantown, WV 26505", 200);
                database.Lots.Add(lot);
                database.SaveChanges();

                lot = new Lot("15", "WVU Medicine", "6040 University Town Centre Dr, Morgantown, WV 26501",
                    300);
                lot.CurrentlyOccupiedSpots = 250;
                database.Lots.Add(lot);
                database.SaveChanges();
            }
            if (!database.LotTypes.Any())
            {
                LotType lotType = new LotType("Short Term Paid Lot");
                database.LotTypes.Add(lotType);
                database.SaveChanges();

                lotType = new LotType("Free");
                database.LotTypes.Add(lotType);
                database.SaveChanges();

                lotType = new LotType("Permit");
                database.LotTypes.Add(lotType);
                database.SaveChanges();
            }

            if (!database.LotStatuses.Any())
            {
                CultureInfo cultureInfo = new CultureInfo("en-US");
                DateTime startTime = DateTime.Parse("8:00 AM", cultureInfo, DateTimeStyles.NoCurrentDateDefault);
                DateTime endTime = DateTime.Parse("4:00 PM", cultureInfo, DateTimeStyles.NoCurrentDateDefault);

                LotStatus lotStatus = new LotStatus("Weekday", startTime, endTime, 0.50, 1, 1);
                database.LotStatuses.Add(lotStatus);
                database.SaveChanges();

                lotStatus = new LotStatus("Weekday", startTime, endTime, 0.50, 2, 1);
                database.LotStatuses.Add(lotStatus);
                database.SaveChanges();

                lotStatus = new LotStatus("Weekday", startTime, endTime, 0.00, 3, 3);
                database.LotStatuses.Add(lotStatus);
                database.SaveChanges();

                startTime = DateTime.Parse("4:01 PM", cultureInfo, DateTimeStyles.NoCurrentDateDefault);
                endTime = DateTime.Parse("7:59 AM", cultureInfo, DateTimeStyles.NoCurrentDateDefault);

                lotStatus = new LotStatus("Weekday", startTime, endTime, 0.00, 1, 2);
                database.LotStatuses.Add(lotStatus);
                database.SaveChanges();

                lotStatus = new LotStatus("Weekday", startTime, endTime, 0.00, 2, 2);
                database.LotStatuses.Add(lotStatus);
                database.SaveChanges();

                lotStatus = new LotStatus("Weekday", startTime, endTime, 0.00, 3, 2);
                database.LotStatuses.Add(lotStatus);
                database.SaveChanges();

            }

            if (!database.Permits.Any())
            {
                DateTime permitStartDate = new DateTime(2020, 1, 1);
                DateTime permitEndDate = new DateTime(2020, 12, 31);

                WVUEmployee user = database.WVUEmployees
                                .Where(e => e.Email == "TestWVUEmployee1@wvu.edu")
                                .FirstOrDefault();
                string userID = user.Id;
                string parkingEmployeeID = user.Id;

                Permit permit = new Permit(500.00, permitStartDate, permitEndDate, userID, parkingEmployeeID);
                database.Permits.Add(permit);
                database.SaveChanges();

                user.PermitID = permit.PermitID;
                database.WVUEmployees.Update(user);
                database.SaveChanges();


                //user = database.WVUEmployees
                //              .Where(e => e.Email == "TestWVUEmployee2@wvu.edu")
                //              .FirstOrDefault();
                //userID = user.Id;

                //permit = new Permit(500.00, permitStartDate, permitEndDate, userID);
                //database.Permits.Add(permit);
                //database.SaveChanges();

                //user.PermitID = permit.PermitID;
                //database.WVUEmployees.Update(user);
                //database.SaveChanges();


                //user = database.WVUEmployees
                //               .Where(e => e.Email == "TestWVUEmployee3@wvu.edu")
                //               .FirstOrDefault();
                //userID = user.Id;

                //permit = new Permit(500.00, permitStartDate, permitEndDate, userID);
                //database.Permits.Add(permit);
                //database.SaveChanges();

                //user.PermitID = permit.PermitID;
                //database.WVUEmployees.Update(user);
                //database.SaveChanges();

                //user = database.WVUEmployees
                //           .Where(e => e.Email == "TestParkingEmployee1@wvu.edu")
                //           .FirstOrDefault();
                //userID = user.Id;

                //permit = new Permit(500.00, permitStartDate, permitEndDate, userID);
                //database.Permits.Add(permit);
                //database.SaveChanges();

                //user.PermitID = permit.PermitID;
                //database.WVUEmployees.Update(user);

                user = database.WVUEmployees
                .Where(e => e.Email == "TestParkingEmployee1@wvu.edu")
                .FirstOrDefault();

                userID = user.Id;

                permit = new Permit(500.00, permitStartDate, permitEndDate, userID, parkingEmployeeID);
                database.Permits.Add(permit);
                database.SaveChanges();

                user.PermitID = permit.PermitID;  // wvuemployee object - am now in the object oriented world (c#)

                database.WVUEmployees.Update(user);  //  how do i get into the database world?


                database.SaveChanges();


            }


        }
    }
}

