using FluentValidation;
using HerdSync.Shared.DTO.Animal;

namespace BLL.Validators
{
    public class SpeciesDTOValidator : AbstractValidator<AnimalDTO>
    {
        public SpeciesDTOValidator()
        {
            RuleFor(x => x.DisplayIdentifier).NotEmpty().WithMessage("Cow Number is required.");
            // Add more rules as needed
        }
    }
}