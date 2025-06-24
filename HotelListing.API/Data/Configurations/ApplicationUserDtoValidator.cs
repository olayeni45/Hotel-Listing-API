using FluentValidation;
using HotelListing.API.Models.Users;

namespace HotelListing.API.Data.Configurations
{
    public class ApplicationUserDtoValidator : AbstractValidator<ApplicationUserDto>
    {
        public ApplicationUserDtoValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6).WithMessage("Minimum length is 6 characters")
                .MaximumLength(15).WithMessage("Maximum length is 15 characters");
        }
    }
}
