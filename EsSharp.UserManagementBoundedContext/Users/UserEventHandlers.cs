using EsSharp.UserManagementBoundedContext.Users.Events;

namespace EsSharp.UserManagementBoundedContext.Users
{
	public partial class User
	{
		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic)@event);
		}

		private void HandleEvent(UserRegistered @event)
		{
			this.Name = @event.Name;
			this.FamilyName = @event.FamilyName;
			this.Email = @event.Email;
		}

		private void HandleEvent(UserSuspended @event)
		{
			this.IsActive = false;
		}

		private void HandleEvent(UserActivated @event)
		{
			this.IsActive = true;
		}
	}
}
