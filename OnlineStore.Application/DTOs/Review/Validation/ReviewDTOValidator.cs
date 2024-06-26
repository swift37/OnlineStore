﻿using FluentValidation;

namespace OnlineStore.Application.DTOs.Review.Validation
{
    public class ReviewDTOValidator : AbstractValidator<ReviewDTO>
    {
        public ReviewDTOValidator()
        {
            RuleFor(r => r.Id)
                .GreaterThan(0);

            RuleFor(r => r.Name)
                .MaximumLength(32);

            RuleFor(r => r.CreationDate)
                .NotEqual(default(DateTime));

            RuleFor(r => r.LastChangeDate)
                .NotEqual(default(DateTime));

            RuleFor(r => r.ProductId)
                .GreaterThan(0);

            RuleFor(r => r.Rating)
                .GreaterThanOrEqualTo(1)
                .LessThanOrEqualTo(5);

            RuleFor(r => r.Content)
                .MaximumLength(256);
        }
    }
}
