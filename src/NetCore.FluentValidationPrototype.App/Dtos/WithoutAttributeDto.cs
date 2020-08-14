using System;

namespace NetCore.FluentValidationPrototype.App.Dtos
{
    public sealed class WithoutAttributeDto
    {
        public string Foo { get; set; }
        public string Bar { get; set; }
        public int Fizz { get; set; }
        public int? Buzz { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
