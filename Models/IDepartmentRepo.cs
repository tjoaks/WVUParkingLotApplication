using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiscussionMVCAppOaks.Models
{
   public interface IDepartmentRepo
    {
        List<Department> ListAllDepartments();
    }
}
