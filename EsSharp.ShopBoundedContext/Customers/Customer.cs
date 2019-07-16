using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.ShopBoundedContext.Customers.Events;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.ShopBoundedContext.Customers
{
	public partial class Customer : Aggregate
	{
		public string Name { get; private set; }

		public Guid UserId { get; set; }

		public bool IsPremium { get; private set; }

		public int Balance { get; private set; }

		public IEnumerable<Order> Orders => this._orders.AsEnumerable();

		public override IEnumerable<IEvent> Events => base.Events.Concat(this._orders.SelectMany(x => x.Events));

		internal readonly IList<Order> _orders;

		public Customer(Guid id)
		{
			this.Id = id;
			this._orders = new List<Order>();
		}

		public Customer(Guid userId, string name) : this(Guid.NewGuid())
		{
			this.Name = name;
			this.UserId = userId;
			this.PublishEvent(new CustomerCreated(this.Id, name, userId));
		}

		public Guid CreateOrder()
		{
			var order = new Order(this);
			this._orders.Add(order);
			this.PublishEvent(new CustomerCreatedOrder(this.Id, order));

			return order.Id;
		}

		public void PayOrder(Guid orderId)
		{
			var order = this._orders.First(x=>x.Id == orderId);

			if (order.Price > this.Balance)
			{
				throw new AggregateException("insufficient balance");
			}

			order.Pay(this);

			this.Balance -= order.Price;
			this.PublishEvent(new OrderWasPayed(this.Id, this.Version, order, order.Price));
		}

		public void Fund(int summ)
		{
			if (summ <= 0)
			{
				throw new AggregateException("summ must be positive integer");
			}

			this.Balance += summ;
			this.PublishEvent(new BalanceFunded(this.Id, this.Version, summ));
		}
	}
}
