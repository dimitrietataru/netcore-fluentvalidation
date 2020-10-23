using Bogus;
using FluentValidation;
using FluentValidation.TestHelper;
using NetCore.FluentValidationPrototype.App.Dtos;
using NetCore.FluentValidationPrototype.App.Validators;
using System;
using Xunit;

namespace NetCore.FluentValidationPrototype.App.Test.Validators
{
    [Trait("Validator", nameof(WithoutAttributeDto))]
    public sealed class WithoutAttributeDtoValidatorTest
    {
        private readonly IValidator<WithoutAttributeDto> dtoValidator;
        private readonly Faker faker;

        public WithoutAttributeDtoValidatorTest()
        {
            dtoValidator = new WithoutAttributeDtoValidator();
            faker = new Faker();
        }

        [Fact]
        internal void GivenValidateCalledWhenFooIsNullOrEmptyThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.Foo = faker.PickRandom(null, string.Empty);

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.Foo);
        }

        [Fact]
        internal void GivenValidateCalledWhenFooLengthIsNotInRangeThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.Foo = faker.Random.String2(minLength: 101, maxLength: 999);

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.Foo);
        }

        [Fact]
        internal void GivenValidateCalledWhenBarIsEmptyThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.Bar = string.Empty;

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.Bar);
        }

        [Fact]
        internal void GivenValidateCalledWhenBarLengthIsNotInRangeThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.Bar = faker.Random.String2(minLength: 101, maxLength: 999);

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.Bar);
        }

        [Fact]
        internal void GivenValidateCalledWhenFizzIsNotInRangeThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.Fizz = faker.PickRandom(faker.Random.Int(max: 0), faker.Random.Int(min: 101));

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.Fizz);
        }

        [Fact]
        internal void GivenValidateCalledWhenBuzzIsNotInRangeThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.Buzz = faker.PickRandom(faker.Random.Int(max: 0), faker.Random.Int(min: 101));

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.Buzz);
        }

        [Fact]
        internal void GivenValidateCalledWhenStartDateIsNotInRangeThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.StartDate = faker.PickRandom(
                faker.Date.Past(refDate: new DateTime(2000, 1, 1)),
                faker.Date.Future(refDate: new DateTime(3000, 1, 1)));

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.StartDate);
        }

        [Fact]
        internal void GivenValidateCalledWhenEndDateIsNotInRangeThenAuditFails()
        {
            // Arrange
            var dto = GenerateFakeDto();
            dto.EndDate = faker.PickRandom(
                faker.Date.Past(refDate: new DateTime(2000, 1, 1)),
                faker.Date.Future(refDate: new DateTime(3000, 1, 1)));

            // Act
            var result = dtoValidator.TestValidate(dto);

            // Assert
            result.ShouldHaveValidationErrorFor(_ => _.EndDate);
        }

        private WithoutAttributeDto GenerateFakeDto()
        {
            return new Faker<WithoutAttributeDto>()
                .RuleFor(
                    property => property.Foo,
                    func => func.Random.String2(minLength: 1, maxLength: 100))
                .RuleFor(
                    property => property.Bar,
                    func => func.Random.String2(minLength: 1, maxLength: 100))
                .RuleFor(
                    property => property.Fizz,
                    func => func.Random.Int(min: 1, max: 100))
                .RuleFor(
                    property => property.Buzz,
                    func => func.Random.Int(min: 1, max: 100))
                .RuleFor(
                    property => property.StartDate,
                    func => func.Date.Between(new DateTime(2000, 1, 1), new DateTime(3000, 1, 1)))
                .RuleFor(
                    property => property.EndDate,
                    func => func.Date.Between(new DateTime(2000, 1, 1), new DateTime(3000, 1, 1)))
                .StrictMode(ensureRulesForAllProperties: true)
                .Generate();
        }
    }
}
