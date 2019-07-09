using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EsSharp.ShopBoundedContext;
using EsSharp.ShopBoundedContext.Customers;
using EsSharp.ShopBoundedContext.Managers;
using EsSharp.ShopBoundedContext.Orders;
using EsSharp.Tests.TestEntities;
using EsSharp.UserManagementBoundedContext.Users;
using Xunit;

namespace EsSharp.Tests
{
	public class AppTests
	{
		[Fact]
		public void Events_ForOrder_ShouldBeSaved()
		{
			var manager = new Manager("Manager");

			var user = new User("alex", "T", "alex112244@gmail.com");
			var customer = new Customer(user.Id, user.Name);
			customer.Fund(100);
			var newOrderId = customer.CreateOrder();
			var order = customer.Orders.First(x => x.Id == newOrderId);
			order.SetManager(manager);
			order.AddProduct(new Product()
			{
				Price = 11,
				Type = "Cheeseburger"
			});

			customer.PayOrder(newOrderId);

			var es = new EventStore(new DefaultEventSerializer(), new EventDataStorage());

			foreach (var @event in customer.Events)
			{
				es.Add(@event);
			}

			foreach (var @event in manager.Events)
			{
				es.Add(@event);
			}

			es.Commit();

			var orderEvents = es.GetEventsForAggregate(order.Id);
			var orderFromEvents = new Order(order.Id);

			foreach (var @event in orderEvents)
			{
				orderFromEvents.Handle(@event);
			}

			Assert.Equal(11, orderFromEvents.Products.First().Price);
			Assert.True(orderFromEvents.IsClosed);
			Assert.True(orderFromEvents.IsPayed);
		}
	}
}