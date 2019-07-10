using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EsSharp.Projections;
using EsSharp.Storage;
using Microsoft.AspNetCore.Mvc;
using EsSharp.UserManagementBoundedContext.Users;
using UserDto = EsSharp.App.Models.User;

namespace EsSharp.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly EventStore _eventStore;
		private IEnumerable<ProjectionBuilder<User>> _projections;

		public UsersController(ProjectionsContainer projections, EventStore eventStore)
		{
			_eventStore = eventStore;
			this._projections = projections.GetProjectionBuilders<User>();
		}

		//// GET api/values
		//[HttpGet]
		//public ActionResult<IEnumerable<string>> Get()
		//{
		//	return new string[] { "value1", "value2" };
		//}

		//// GET api/values/5
		//[HttpGet("{id}")]
		//public ActionResult<string> Get(int id)
		//{
		//	return "value";
		//}

		// POST api/values
		[HttpPost]
		public Guid Post([FromBody] UserDto user)
		{
			var domainUser = new User(user.Name, user.FamilyName, user.Email);

			foreach (var projection in this._projections)
			{
				projection.HandleEvents(domainUser);
			}

			this._eventStore.Add(domainUser);
			this._eventStore.Commit();

			return domainUser.Id;
		}

		// PUT api/values/5
		[HttpPut("{id}")]
		public void Put(int id, [FromBody] string value)
		{
		}

		// DELETE api/values/5
		[HttpDelete("{id}")]
		public void Delete(int id)
		{
		}
	}
}
