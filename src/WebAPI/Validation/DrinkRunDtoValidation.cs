using Domain.Dto;
using FluentValidation;

namespace WebAPI.Validation
{
    public class DrinkRunDtoValidation : AbstractValidator<DrinkRunDto>
    {
        public DrinkRunDtoValidation()
        {
            RuleFor(x => x.Participants).NotNull().NotEmpty();
        }
    }
}
