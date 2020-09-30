using System;
using System.Collections.Generic;

namespace User.Data.Database
{
    public partial class User
    {
        public int Id { get; set; }
        public string Uuid { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}
