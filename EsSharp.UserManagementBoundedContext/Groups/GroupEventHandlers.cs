using EsSharp.UserManagementBoundedContext.Groups.Events;

namespace EsSharp.UserManagementBoundedContext.Groups
{
	public partial class Group
	{
		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic)@event);
		}

		private void HandleEvent(AdministrationPermissionSet @event)
		{
			this.Administration = @event.PermissionValue;
		}

		private void HandleEvent(GroupCreated @event)
		{
			this.Name = @event.Name;
		}

		private void HandleEvent(UserAdded @event)
		{
			this._users.Add(@event.User);
		}
	}
}
