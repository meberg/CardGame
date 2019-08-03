using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CardGame.ClassLibrary
{
    public class UserScore
    {
        [Required]
        [ForeignKey("User")]
        [Column(Order=1)]
        public int UserId { get; set; }
        public User User { get; set; }

        [Required]
        [Key]
        [Column(Order = 2)]
        public int GameId { get; set; }
        public int Score { get; set; }
    }
}
