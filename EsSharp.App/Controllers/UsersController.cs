using System;
using EsSharp.App.Models;
using EsSharp.Projections;
using Microsoft.AspNetCore.Mvc;
using User = EsSharp.UserManagementBoundedContext.Users.User;
using UserDto = EsSharp.App.Models.User;

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

		[HttpPost("create")]
		public Guid Create([FromBody] UserDto user)
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

		[HttpPost("suspend")]
		public ActionResult Suspend([FromBody] UserSuspendModel model)
		{
			var user = this._aggregateStore.Get<User>(model.UserId);

			user.Suspend(model.Reason);

			this._aggregateStore.SaveAggregateAndEvents(user);

			return Ok();
		}
	}
}
