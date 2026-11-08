using DalilAcademy.Core.DtOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace DalilAcademy.Core.Validators
{
    public class CreateStudentDtoValidator : AbstractValidator<CreateStudentDto>
    {
        public CreateStudentDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(254);
        }
    }
}