using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HerdSync.Shared.DTO;

namespace BLL.Validators
{
    public class SpeciesDTOValidator : AbstractValidator<spd_Species_Detail_DTO>
    {
        public SpeciesDTOValidator()
        {
            RuleFor(x => x.spd_Number).NotEmpty().WithMessage("Cow Number is required.");
            RuleFor(x => x.spd_Weight).GreaterThan(0).WithMessage("Weight must be positive.");
            //RuleFor(x => x.spd_Species).Equal("Cow").WithMessage("Species must be Cow.");
            // Add more rules as needed
        }

    }
}
