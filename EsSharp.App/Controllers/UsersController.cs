using System;
using EsSharp.App.Models;
using EsSharp.Projections;
using EsSharp.UserManagementBoundedContext.Users;
using Microsoft.AspNetCore.Mvc;

namespace EsSharp.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly SynchronizedAggregateStore _aggregateStore;

		public UsersController(SynchronizedAggregateStore aggregateStore)
		{
			_aggregateStore = aggregateStore;
		}

		public Guid Create([FromBody] UserCreationModel user)
		{
			var domainUser = new User(user.Name, user.FamilyName, user.Email);

			this._aggregateStore.SaveAggregateAndEvents(domainUser);

			return domainUser.Id;
		}

		[HttpPost("{userId}/activate")]
		public ActionResult Activate([FromRoute] Guid userId)
		{
			var user = this._aggregateStore.Get<User>(userId);

			user.Activate();

			this._aggregateStore.SaveAggregateAndEvents(user);

			return Ok();
		}

		[HttpPost("{userId}/suspend")]
		public ActionResult Suspend([FromRoute] Guid userId, [FromBody] UserSuspendModel model)
		{
			var user = this._aggregateStore.Get<User>(userId);

			user.Suspend(model.Reason);

			this._aggregateStore.SaveAggregateAndEvents(user);

			return Ok();
		}
	}
}
