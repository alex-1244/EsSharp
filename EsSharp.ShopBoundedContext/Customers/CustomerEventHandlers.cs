using EsSharp.ShopBoundedContext.Customers.Events;

namespace EsSharp.ShopBoundedContext.Customers
{
	public partial class Customer
	{
		protected override void HandleInternal(IEvent @event)
		{
			this.HandleEvent((dynamic)@event);
		}

		private void HandleEvent(CustomerCreated @event)
		{
			this.Name = @event.Name;
			this.UserId = @event.UserId;
		}

		private void HandleEvent(CustomerCreatedOrder @event)
		{
			this._orders.Add(@event.Order);
		}

		private void HandleEvent(BalanceFunded @event)
		{
			this.Balance += @event.Summ;
		}

		private void HandleEvent(OrderWasPayed @event)
		{
			@event.Order.OrderPayed();
			this.Balance -= @event.Order.Price;
		}
	}
}
