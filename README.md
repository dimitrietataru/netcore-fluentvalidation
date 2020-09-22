# .NET Core - FluentValidation

### Install
``` powershell
PM> Install-Package FluentValidation -Version 9.2.2
PM> Install-Package FluentValidation.AspNetCore -Version 9.2.0
PM> Install-Package FluentValidation.DependencyInjectionExtensions -Version 9.2.0
```

### Configure
``` csharp
using FluentValidation;
using FluentValidation.AspNetCore;

public void ConfigureServices(IServiceCollection services)
{
    // ..

    services.AddControllers().AddFluentValidation();

    services.AddTransient<IValidator<TDto>, TDtoValidator>();
    
    // ..
}
```

### Integrate
``` csharp
public class WithAttributeDto
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

public class WithoutAttributeDto
{
    public string Foo { get; set; }
    public string Bar { get; set; }
    public int Fizz { get; set; }
    public int? Buzz { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}

public class WithoutAttributeDtoValidator : AbstractValidator<WithoutAttributeDto>
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
```

### Request/Response samples
``` json
[POST] /api/framework-validation/test
[POST] /api/fluent-validation/test

// Request
{
    "foo": "isRequired",
    "bar": null,
    "fizz": 24,
    "buzz": 42,
    "startDate": "2020-01-01T12:00:00.000Z",
    "endDate": "2020-12-31T12:00:00.000Z"
}

// Response
{
    "foo": "isRequired",
    "bar": null,
    "fizz": 24,
    "buzz": 42,
    "startDate": "2020-01-01T12:00:00Z",
    "endDate": "2020-12-31T12:00:00Z"
}
```
``` json
[POST] /api/framework-validation/test

// Request
{
    "foo": "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
    "bar": "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
    "fizz": -1,
    "buzz": -1,
    "startDate": "1999-12-31T23:59:59.999Z",
    "_endDate": "1999-12-31T23:59:59.999Z"
}

// Response
{
    "type": "..",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "..",
    "errors": {
        "Bar": [
            "The field Bar must be a string or array type with a maximum length of '100'."
        ],
        "Foo": [
            "The field Foo must be a string or array type with a maximum length of '100'."
        ],
        "Buzz": [
            "The field Buzz must be between 1 and 100."
        ],
        "Fizz": [
            "The field Fizz must be between 1 and 100."
        ],
        "EndDate": [
            "The field EndDate must be between 01/01/2000 00:00:00 and 01/01/3000 00:00:00."
        ],
        "StartDate": [
            "The field StartDate must be between 01/01/2000 00:00:00 and 01/01/3000 00:00:00."
        ]
    }
}
```
``` json
[POST] /api/fluent-validation/test

// Request
{
    "foo": "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
    "bar": "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
    "fizz": -1,
    "buzz": -1,
    "startDate": "1999-12-31T23:59:59.999Z",
    "_endDate": "1999-12-31T23:59:59.999Z"
}

// Response
{
    "type": "..",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "..",
    "errors": {
        "Bar": [
            "Bar should have a max length of 100"
        ],
        "Foo": [
            "Foo should have a max length of 100"
        ],
        "Buzz": [
            "Buzz should have a value between 1 and 100"
        ],
        "Fizz": [
            "Fizz should have a value between 1 and 100"
        ],
        "EndDate": [
            "End date should have a value between 2000-01-01 and 3000-01-01"
        ],
        "StartDate": [
            "Start date should have a value between 2000-01-01 and 3000-01-01"
        ]
    }
}
```
``` json
[POST] /api/fluent-validation/test-mixed

// Request
{
    "foo": "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
    "bar": "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901",
    "fizz": -1,
    "buzz": -1,
    "startDate": "1999-12-31T23:59:59.999Z",
    "_endDate": "1999-12-31T23:59:59.999Z"
}

// Response
{
    "type": "..",
    "title": "One or more validation errors occurred.",
    "status": 400,
    "traceId": "..",
    "errors": {
        "Bar": [
            "The field Bar must be a string or array type with a maximum length of '100'."
        ],
        "Foo": [
            "The field Foo must be a string or array type with a maximum length of '100'."
        ],
        "Buzz": [
            "The field Buzz must be between 1 and 100."
        ],
        "Fizz": [
            "The field Fizz must be between 1 and 100."
        ],
        "EndDate": [
            "End date should have a value between 2000-01-01 and 3000-01-01"
        ],
        "StartDate": [
            "Start date should have a value between 2000-01-01 and 3000-01-01"
        ]
    }
}
```
