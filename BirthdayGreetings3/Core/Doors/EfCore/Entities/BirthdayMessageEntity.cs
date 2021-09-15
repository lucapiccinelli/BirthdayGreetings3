using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayGreetings3.Core.Doors.EfCore.Entities
{
    public class BirthdayMessageEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public String Firstname { get; set; }
        [Required]
        public String Lastname { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}