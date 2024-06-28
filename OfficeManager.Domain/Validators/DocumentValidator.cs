using FluentValidation;
using OfficeManager.Domain.Entities;
using OfficeManager.Domain.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeManager.Domain.Validators
{
    public class DocumentValidator : AbstractValidator<Document>
    {
        public DocumentValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage(Resource.DocumentIdIsEmpty);

            RuleFor(x => x.Url)
                .NotEmpty()
                .WithMessage(Resource.DocumentUrlIsEmpty);

            RuleFor(x => x.Name)
                 .NotEmpty()
                 .WithMessage(Resource.DocumentNameIsEmpty);

            RuleFor(x => x.CaseId)
                .NotEmpty()
                .WithMessage(Resource.DocumentCaseIdIsEmpty);
        }
    }
}
