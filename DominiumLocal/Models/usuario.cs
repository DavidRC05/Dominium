using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DominiumLocal.Models
{
    public class usuario
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthdayDate { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Cpassword { get; set; }
    }

}