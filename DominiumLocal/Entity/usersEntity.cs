﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DominiumLocal.Entity
{
    public class usersEntity
    {
        [Key] public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public string Cpassword { get; set; }
        public int Rol {  get; set; }
        public string ProfilePicture { get; set; }
        public string Description { get; set; }
    }
}