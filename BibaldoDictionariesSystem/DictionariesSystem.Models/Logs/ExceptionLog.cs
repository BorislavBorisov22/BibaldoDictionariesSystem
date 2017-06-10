using System;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Logs
{
    public class ExceptionLog
    {    
        private const int MaxMessageLength = 200;

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(MaxMessageLength)]
        public string Message { get; set; }

        [Required]
        public DateTime LoggedOn { get; set; }
    }
}
