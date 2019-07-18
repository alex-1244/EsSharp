using System;
using System.Collections.Generic;
using System.Linq;
using EsSharp.App.Models;
using EsSharp.Projections;
using EsSharp.ShopBoundedContext;
using EsSharp.ShopBoundedContext.Customers;
using Microsoft.AspNetCore.Mvc;
using User = EsSharp.UserManagementBoundedContext.Users.User;

namespace EsSharp.App.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CustomerController : ControllerBase
	{
		private readonly SynchronizedAggregateStore _aggregateStore;

		public CustomerController(SynchronizedAggregateStore aggregateStore)
		{
			_aggregateStore = aggregateStore;
		}

		public Guid Create([FromBody] CustomerCreationModel customer)
		{
			var domainCustomer = new Customer(customer.UserId, customer.Name);

			this._aggregateStore.SaveAggregateAndEvents(domainCustomer);

			return domainCustomer.Id;
		}

		[HttpPut("{customerId}/fund")]
		public ActionResult FundBalance([FromRoute] Guid customerId, [FromBody] CustomerFundBalanceModel model)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			customer.Fund(model.Amount);

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok();
		}

		[HttpPut("{customerId}/promote")]
		public ActionResult Promote([FromRoute] Guid customerId)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			customer.Promote();

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok();
		}

		#region order_mgmt
		[HttpGet("{customerId}/orders")]
		public ActionResult<IEnumerable<OrderModel>> GetOrders([FromRoute] Guid customerId)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			return Ok(customer.Orders.Select(x => new OrderModel(x)));
		}

		[HttpPost("{customerId}/orders")]
		public ActionResult<Guid> CreateOrder([FromRoute] Guid customerId)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			var orderId = customer.CreateOrder();

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok(orderId);
		}

		[HttpPut("{customerId}/{orderId}")]
		public ActionResult<Guid> AddProduct([FromRoute] Guid customerId, [FromRoute] Guid orderId, [FromBody] ProductModel product)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			customer.Orders.First(x=>x.Id == orderId).AddProduct(new Product()
			{
				Price = product.Price,
				Type = product.Type
			});

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok(orderId);
		}

		[HttpPost("{customerId}/{orderId}/cancel")]
		public ActionResult CancelOrder([FromRoute] Guid customerId, [FromRoute] Guid orderId)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			customer.Orders.First(x => x.Id == orderId).Cancel(customer);

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok();
		}

		[HttpPost("{customerId}/{orderId}/pay")]
		public ActionResult PayOrder([FromRoute] Guid customerId, [FromRoute] Guid orderId)
		{
			var customer = this._aggregateStore.Get<Customer>(customerId);

			customer.PayOrder(orderId);

			this._aggregateStore.SaveAggregateAndEvents(customer);

			return Ok();
		}
		#endregion
	}
}
