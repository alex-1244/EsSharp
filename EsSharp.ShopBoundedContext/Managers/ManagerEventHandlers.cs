using EsSharp.ShopBoundedContext.Managers.Events;

namespace EsSharp.ShopBoundedContext.Managers
{
	public partial class Manager
	{
		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic)@event);
		}

		private void HandleEvent(ManagerCreated @event)
		{
			this.Name = @event.Name;
		}

		private void HandleEvent(ManagerAssigned @event)
		{
			this._orders.Add(@event.Order);
		}

		private void HandleEvent(ManagerUnassigned @event)
		{
			this._orders.Remove(@event.Order);
		}

		private void HandleEvent(ManagerClosedOrder @event) { }
	}
}
