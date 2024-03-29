﻿using BlazorSozluk.Common.Models.RequestModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Api.Application.Features.Commands.User.Login
{
    public class LoginUserComandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserComandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotNull()
                .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
                .WithMessage("{PropertyName} not a valid email address");

            RuleFor(x => x.Password)
                .NotNull()
                .MinimumLength(4)
                .WithMessage("{PropertyName} should at least be 4 characters");
        }
    }
}
