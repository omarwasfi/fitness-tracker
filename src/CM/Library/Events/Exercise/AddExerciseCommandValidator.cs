﻿using System;
using CM.Library.DataModels;
using CM.Library.Queries.Person;
using CM.Library.Queries.Roles;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CM.Library.Events.Exercise
{
    public class AddExerciseCommandValidator : AbstractValidator<AddExerciseCommand>
    {
        private readonly IMediator _mediator;

        public AddExerciseCommandValidator(IMediator mediator)
        {
            _mediator = mediator;

            CascadeMode = CascadeMode.Stop;

            RuleFor(x => x).MustAsync(async (AddExerciseommand, cancellation) => {
                return await IsTheAuthorizedUserIsAdminstratorOrHROrCouch(AddExerciseommand.ClaimsPrincipal);
            }).WithMessage("You must be an Administrator or HR or couch");
        }

        private async Task<bool> IsTheAuthorizedUserIsAdminstratorOrHROrCouch(ClaimsPrincipal claimsPrincipal)
        {
            PersonDataModel authorizedPerson = await _mediator.Send(new GetTheAuthorizedPersonQuery(claimsPrincipal));

            List<IdentityRole> roles = await _mediator.Send(new GetPersonRolesQuery(authorizedPerson.Id));

            if (roles.Find(x => x.Id == "Administrator") != null)
            {
                return true;
            }
            else if (roles.Find(x => x.Id == "HR") != null)
            {
                return true;
            }
            else if (roles.Find(x => x.Id == "Couch") != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}

