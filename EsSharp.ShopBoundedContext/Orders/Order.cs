using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.ShopBoundedContext.Customers;
using EsSharp.ShopBoundedContext.Managers;
using EsSharp.ShopBoundedContext.Orders.Events;

namespace EsSharp.ShopBoundedContext.Orders
{
	public partial class Order : Aggregate
	{
		private IList<Product> _products;
		public IEnumerable<Product> Products => _products.AsEnumerable();

		public Customer Customer { get; private set; }

		public Manager Manager { get; private set; }

		public bool IsClosed { get; private set; }

		public bool IsPayed { get; private set; }

		public int Price => this._products.Sum(x => x.Price);

		public Order(Guid id)
		{
			this.Id = id;
			this._products = new List<Product>();
		}

		public Order(Customer customer) : this(Guid.NewGuid())
		{
			this.Customer = customer;
			this.PublishEvent(new OrderCreated(this.Id, customer));
		}

		public void AddProduct(Product product)
		{
			this.ValidateOrder();
			this._products.Add(product);
			this.PublishEvent(new ProductAdded(this.Id, this.Version, product));
		}

		internal void Pay(Customer customer)
		{
			if (customer.Balance < this.Price)
			{
				throw new AggregateException("Insufficient funds");
			}

			this.IsPayed = true;
			this.IsClosed = true;

			this.PublishEvent(new OrderPayed(this.Id, this.Version));
		}

		public void SetManager(Manager manager)
		{
			this.ValidateOrder();
			this.Manager = manager;
			manager.AddOrder(this);

			this.PublishEvent(new ManagerAdded(this.Id, this.Version, manager));
		}

		public void Cancel(Customer customer)
		{
			if (this.Customer != customer)
			{
				throw new AggregateException("Customer can only close his own orders");
			}

			this.ValidateOrder();
			this.IsClosed = true;
			this.Manager.RemoveOrder(this);

			this.PublishEvent(new OrderCancelled(this.Id, this.Version));
		}

		private void ValidateOrder()
		{
			if (this.IsClosed)
			{
				throw new AggregateException("order is already closed");
			}
		}

		private void ValidateManager(Manager mgr)
		{
			if (this.Manager != mgr)
			{
				throw new AggregateException("Manager can only close orders he is assigned to");
			}
		}
	}
}
