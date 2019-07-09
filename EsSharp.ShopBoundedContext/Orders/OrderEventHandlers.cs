using EsSharp.ShopBoundedContext.Orders.Events;

namespace EsSharp.ShopBoundedContext.Orders
{
	public partial class Order
	{
		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic) @event);
		}

		private void HandleEvent(OrderCreated @event)
		{
			this.Customer = @event.Customer;
		}

		private void HandleEvent(OrderClosed @event)
		{
			this.IsClosed = true;
			this.Manager.RemoveOrder(this);
		}

		private void HandleEvent(ManagerAdded @event)
		{
			this.Manager = @event.Manager;
			@event.Manager.AddOrder(this);
		}

		private void HandleEvent(ManagerRemoved @event)
		{
			@event.Manager.RemoveOrder(this);
			this.Manager = null;
		}

		private void HandleEvent(OrderCancelled @event)
		{
			this.IsClosed = true;
			this.Manager.RemoveOrder(this);
		}

		private void HandleEvent(ProductAdded @event)
		{
			this._products.Add(@event.Product);
		}

		private void HandleEvent(OrderPayed @event)
		{
			this.IsPayed = true;
			this.IsClosed = true;
		}
	}
}
