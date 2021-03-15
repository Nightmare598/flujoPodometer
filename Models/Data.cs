namespace apiProductor.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Data
    {
        [Key]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }
        [Required]
        public int Steps { get; set; }
    }
}
