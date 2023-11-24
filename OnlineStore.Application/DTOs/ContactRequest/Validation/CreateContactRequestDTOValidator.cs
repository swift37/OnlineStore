﻿using FluentValidation;

namespace OnlineStore.Application.DTOs.ContactRequest.Validation
{
    public class CreateContactRequestDTOValidator : AbstractValidator<CreateContactRequestDTO>
    {
        public CreateContactRequestDTOValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(c => c.Message)
                .MaximumLength(256);

            RuleFor(c => c.CreationDate)
                .NotEqual(default(DateTime));
        }
    }
}