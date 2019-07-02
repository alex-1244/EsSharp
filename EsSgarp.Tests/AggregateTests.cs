using System;
using EsSharp.Tests.TestEntities;
using EsSharp.Tests.TestEntities.Events;
using EsSharp.Tests.TestEntities.ProjectionBuilders;
using Xunit;

namespace EsSharp.Tests
{
	public class AggregateTests
	{
		[Fact]
		public void ProjectionBuilders_ShouldHandleEvents()
		{
			var tyreShop = new TyreShop();

			var car = new Car();
			tyreShop.ServiceCar(car);
			
			var pb = new CarProjectionBuilder();
			var pb1 = new TyreShopProjectionBuilder();
			pb.Handle(car);
			pb1.Handle(tyreShop);

			Assert.Equal(10, ((TyreChangedEvent)pb1.LastEvent).Tyre.Price);
			Assert.Equal(10, ((TyreChangedEvent)pb.LastEvent).Tyre.Price);
		}

		[Fact]
		public void DefaultSerializer_ShouldSerializeAndDeserializeData()
		{
			var ev = new TyreChangedEvent(Guid.NewGuid(), 0, new Tyre(), TyrePlacement.LeftFront);
			var serializer = new DefaultEventSerializer();
			var data = serializer.Serialize(ev);
			var @event = serializer.Deserialize<TyreChangedEvent>(data);

			Assert.Equal(TyrePlacement.LeftFront, @event.Placement);
		}

		[Fact]
		public void Aggregate_ShouldBeRebuildedFromSnapshot_WhenSnapshotDataAvailable()
		{
			var aggStore = new AggregateStore(new EventStore(new DefaultEventSerializer()), new DefaultAggregateSerializer());

			var car = new Car();
			car.ChangeTyre(new Tyre() { Price = 10 }, TyrePlacement.LeftBack);
			car.ChangeTyre(new Tyre() { Price = 11 }, TyrePlacement.RightBack);
			car.ChangeTyre(new Tyre() { Price = 12 }, TyrePlacement.RightBack);
			car.ChangeTyre(new Tyre() { Price = 13 }, TyrePlacement.RightBack);
			car.ChangeTyre(new Tyre() { Price = 14 }, TyrePlacement.RightBack);
			car.ChangeTyre(new Tyre() { Price = 15 }, TyrePlacement.RightFront);

			aggStore.SaveAggregate(car);

			var carFromSnapshot = aggStore.GetAggregate<Car>(car.Id);

			carFromSnapshot.ChangeTyre(new Tyre() { Price = 17 }, TyrePlacement.RightBack);
			carFromSnapshot.ChangeTyre(new Tyre() { Price = 16 }, TyrePlacement.RightFront);

			aggStore.SaveAggregate(carFromSnapshot);

			var carFromSnapshot2 = aggStore.GetAggregate<Car>(car.Id);

			Assert.Equal(8, carFromSnapshot2.Version);
			Assert.Equal(16, carFromSnapshot2.RightFrontTyre.Price);
		}
	}
}
