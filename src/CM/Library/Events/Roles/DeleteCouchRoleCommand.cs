﻿using System;
using System.Security.Claims;
using CM.Library.DataModels;
using MediatR;

namespace CM.Library.Events.Roles
{
	public class DeleteCouchRoleCommand : IRequest<PersonDataModel>
	{
        public string PersonId { get; set; }

		public ClaimsPrincipal ClaimsPrincipal { get; set; }


        public DeleteCouchRoleCommand(string personId, ClaimsPrincipal claimsPrincipal)
        {
            PersonId = personId;
            ClaimsPrincipal = claimsPrincipal;
        }
    }
}

