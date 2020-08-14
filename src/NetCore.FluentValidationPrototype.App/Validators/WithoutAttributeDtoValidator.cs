using FluentValidation;
using NetCore.FluentValidationPrototype.App.Dtos;
using System;
using System.Diagnostics;

namespace NetCore.FluentValidationPrototype.App.Validators
{
    [DebuggerStepThrough]
    public sealed class WithoutAttributeDtoValidator : AbstractValidator<WithoutAttributeDto>
    {
        public WithoutAttributeDtoValidator()
        {
            RuleFor(dto => dto.Foo)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage("Foo should not be null")
                .NotEmpty()
                    .WithMessage("Foo should not be empty")
                .MaximumLength(100)
                    .WithMessage("Foo should have a max length of 100");

            RuleFor(dto => dto.Bar)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                    .When(dto => dto.Bar != null, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Bar should not be empty")
                .MaximumLength(100)
                    .When(dto => dto.Bar != null, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Bar should have a max length of 100");

            RuleFor(dto => dto.Fizz)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage("Fizz should not be null")
                .InclusiveBetween(1, 100)
                    .WithMessage("Fizz should have a value between 1 and 100");

            RuleFor(dto => dto.Buzz)
                .Cascade(CascadeMode.Stop)
                .InclusiveBetween(1, 100)
                    .When(dto => dto.Buzz.HasValue, ApplyConditionTo.CurrentValidator)
                    .WithMessage("Buzz should have a value between 1 and 100");

            RuleFor(dto => dto.StartDate)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage("Start date should not be null")
                .InclusiveBetween(new DateTime(2000, 1, 1), new DateTime(3000, 1, 1))
                    .WithMessage("Start date should have a value between 2000-01-01 and 3000-01-01");

            RuleFor(dto => dto.EndDate)
                .Cascade(CascadeMode.Stop)
                .NotNull()
                    .WithMessage("End date should not be null")
                .InclusiveBetween(new DateTime(2000, 1, 1), new DateTime(3000, 1, 1))
                    .WithMessage("End date should have a value between 2000-01-01 and 3000-01-01");
        }
    }
}
