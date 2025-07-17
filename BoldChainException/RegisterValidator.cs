

 using BoldChainBackendAPI.BoldChainModel.BoldChainDto;
 using FluentValidation;
    

    namespace BoldChainBackendAPI.BoldChainException
    {
        public class RegisterDtoValidator : AbstractValidator<RegisterDto>
        {
            public RegisterDtoValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .WithMessage("A valid email is required.");

                RuleFor(x => x.Password)
                    .NotEmpty()
                    .MinimumLength(8).WithMessage("Password must be at least 8 characters.")
                    .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                    .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                    .Matches("[0-9]").WithMessage("Password must contain at least one number.")
                    .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain at least one special character.");
            }
        }

        public class LoginDtoValidator : AbstractValidator<LoginDto>
        {
            public LoginDtoValidator()
            {
                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress();

                RuleFor(x => x.Password)
                    .NotEmpty();
            }
        }
    }


