using System;
using System.ComponentModel.DataAnnotations;

namespace DictionariesSystem.Models.Logs
{
    public class ExceptionLog
    {    
        [Key]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public DateTime LoggedOn { get; set; }
    }
}
