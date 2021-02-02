using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using DiscussionMVCAppOaks.Models.PermitModel;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models
{
    public class WVUEmployee : ApplicationUser
    {
        public string EmployeeNameAndDepartment
        {
            get { return (Fullname + " in " + Department.DepartmentName); }
        }

        [Required]
        public int DepartmentID { get; set; }

        [ForeignKey("DepartmentID")]
        public Department Department { get; set; }

        public int? PermitID { get; set; }

        [ForeignKey("PermitID")]
        public Permit Permit { get; set; }

        public WVUEmployee() { }

        public WVUEmployee(string firstname, string lastname,
            string email, string phoneNumber, string password, int departmentID) :
            base(firstname, lastname, email, phoneNumber, password)
        {
            this.DepartmentID = departmentID;
        }

    }

}
