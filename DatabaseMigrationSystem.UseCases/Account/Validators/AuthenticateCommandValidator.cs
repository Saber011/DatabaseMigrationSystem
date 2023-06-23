using DatabaseMigrationSystem.UseCases.Account.Commands;
using FluentValidation;

namespace DatabaseMigrationSystem.UseCases.Account.Validators;

public class AuthenticateCommandValidator: AbstractValidator<AuthenticateCommand>
{
    public AuthenticateCommandValidator()
    {
        RuleFor(x => x.Login).
            NotEmpty()
           .Length(3, 20);
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(4);
    }
}