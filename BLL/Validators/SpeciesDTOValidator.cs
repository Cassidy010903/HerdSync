using HerdSync.Shared.DTO.Animal;

namespace BLL.Validators
{
    public class SpeciesDTOValidator : AbstractValidator<AnimalDTO>
    {
        public SpeciesDTOValidator()
        {
            RuleFor(x => x.DisplayIdentifier).NotEmpty().WithMessage("Cow Number is required.");
            //RuleFor(x => x.spd_Weight).GreaterThan(0).WithMessage("Weight must be positive.");
            //RuleFor(x => x.spd_Species).Equal("Cow").WithMessage("Species must be Cow.");
            // Add more rules as needed
        }
    }
}