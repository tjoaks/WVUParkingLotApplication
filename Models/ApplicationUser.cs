using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionMVCAppOaks.Models
{
    public class ApplicationUser : IdentityUser
    {

        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string Fullname
        {

            get { return (Firstname + " " + Lastname); }

        }

        public ApplicationUser()
        {
        }

        public ApplicationUser(string firstname, string lastname, string email, string phoneNumber, string password)
        {

            this.Firstname = firstname;
            this.Lastname = lastname;
            this.Email = email;
            this.UserName = email;
            this.PhoneNumber = phoneNumber;

            PasswordHasher<ApplicationUser> passwordHasher =
                new PasswordHasher<ApplicationUser>();
            this.PasswordHash = passwordHasher.HashPassword(this, password);

            this.SecurityStamp = Guid.NewGuid().ToString();


        }



    }
}
