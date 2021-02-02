using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models
{
    public class Visitor : ApplicationUser
    {
        public string VisitorOrganization { get; set; }

        public Visitor() { }

        public Visitor(string firstname, string lastname, string email, string phoneNumber, string password, string visitorOrganization) : base(firstname, lastname, email, phoneNumber, password)
        {
            this.VisitorOrganization = visitorOrganization;

        }
    }
}
