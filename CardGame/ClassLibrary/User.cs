using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CardGame.ClassLibrary
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        [Required]
        public DateTime AccountCreationDate { get; set; }
        public decimal NumberOfHoursPlayed { get; set; }
    }


}
