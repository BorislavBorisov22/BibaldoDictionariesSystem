using System;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Logs
{
    public class UserLog
    {
        private const int MaxUserNameLength = 20;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxUserNameLength)]
        public string Username { get; set; }

        [Required]
        public string CommandName { get; set; }

        [Required]
        public DateTime ExecutionDate { get; set; }
    }
}
