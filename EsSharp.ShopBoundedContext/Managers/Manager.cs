using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.ShopBoundedContext.Managers.Events;
using EsSharp.ShopBoundedContext.Orders;

namespace EsSharp.ShopBoundedContext.Managers
{
	[Serializable]
	public partial class Manager : AggregationRoot
	{
		private const int MaxOrdersLimit = 3;

		public string Name { get; private set; }

		public IEnumerable<Order> Orders => this._orders.AsEnumerable();

		public override IEnumerable<IEvent> Events => base.Events.Concat(this._orders.SelectMany(x => x.Events));

		protected override IEnumerable<Aggregate> NestedAggregates => this._orders;

		private readonly IList<Order> _orders;

		public Manager(Guid id)
		{
			this.Id = id;
			this._orders = new List<Order>();
		}

		public Manager(string name) : this(Guid.NewGuid())
		{
			this.Name = name;
			this.PublishEvent(new ManagerCreated(this.Id, name));
		}

		public void RemoveOrder(Order order)
		{
			this._orders.Remove(order);
			this.PublishEvent(new ManagerAssigned(this.Id, order));
		}

		public void AddOrder(Order order)
		{
			if (this._orders.Count >= MaxOrdersLimit)
			{
				throw new AggregateException($"orders limit is reached for manager {this.Name}");
			}

			this._orders.Add(order);
			this.PublishEvent(new ManagerUnassigned(this.Id, order));
		}
	}
}
