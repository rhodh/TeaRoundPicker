using FluentValidation;
using WebAPI.Dto;

namespace WebAPI.Validation
{
    public class UserDtoValidation : AbstractValidator<CreateUserDto>
    {
        public UserDtoValidation()
        {
            RuleFor(x => x.FirstName).NotEmpty();
            RuleFor(x => x.LastName).NotEmpty();
        }
    }
}
