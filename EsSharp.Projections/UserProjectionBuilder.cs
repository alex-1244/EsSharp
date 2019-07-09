using System;
using EsSharp.UserManagementBoundedContext.Users;
using EsSharp.UserManagementBoundedContext.Users.Events;

namespace EsSharp.Projections
{
	public class UserProjectionBuilder : ProjectionBuilder<User>
	{
		public UserProjectionBuilder() : base()
		{
		}

		public void Handle(UserActivated @event)
		{

		}

		public void Handle(UserRegistered @event)
		{

		}

		public void Handle(UserSuspended @event)
		{

		}
	}
}
