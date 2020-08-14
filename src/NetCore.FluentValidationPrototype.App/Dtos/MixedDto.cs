using System;
using System.ComponentModel.DataAnnotations;

namespace NetCore.FluentValidationPrototype.App.Dtos
{
    public sealed class MixedDto
    {
        [MaxLength(100)]
        public string Foo { get; set; }

        [MaxLength(100)]
        public string Bar { get; set; }

        [Range(1, 100)]
        public int Fizz { get; set; }

        [Range(1, 100)]
        public int? Buzz { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }
    }
}
