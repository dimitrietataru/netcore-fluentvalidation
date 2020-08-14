using System;
using System.ComponentModel.DataAnnotations;

namespace NetCore.FluentValidationPrototype.App.Dtos
{
    public sealed class WithAttributeDto
    {
        [Required]
        [MaxLength(100)]
        public string Foo { get; set; }

        [MaxLength(100)]
        public string Bar { get; set; }

        [Required]
        [Range(1, 100)]
        public int Fizz { get; set; }

        [Range(1, 100)]
        public int? Buzz { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "2000-01-01", "3000-01-01")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Range(typeof(DateTime), "2000-01-01", "3000-01-01")]
        public DateTime EndDate { get; set; }
    }
}
