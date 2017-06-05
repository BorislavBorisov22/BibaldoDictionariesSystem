using DictionariesSystem.Models.Logs.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Logs
{
    public class UserLog
    {
        private const int MaxUserNameLength = 20;
        private const int MaxMessageLength = 200;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxUserNameLength)]
        public string Username { get; set; }

        [Required]
        [MaxLength(MaxMessageLength)]
        public string Message { get; set; }

        [Required]
        public DateTime LoggedOn { get; set; }

        [Required]
        public UserAction Action { get; set; }
    }
}
