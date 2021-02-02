using DiscussionMVCAppOaks.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.Models
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private ApplicationDbContext database;
        public DepartmentRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public List<Department> ListAllDepartments()
        {
            List<Department> departments = database.Departments.ToList();
            return departments;
        }
    }
}
