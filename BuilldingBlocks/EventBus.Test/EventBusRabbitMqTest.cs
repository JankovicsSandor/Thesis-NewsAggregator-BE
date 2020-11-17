using EventBusRabbitMQ;
using NUnit.Framework;
using System;

namespace EventBus.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Event_Bus_RabbitMq_Contructor_Null_Parameters()
        {

            Assert.Throws<ArgumentNullException>(() => new EventBusRabbitMQ.EventBusRabbitMQ(null, null, null, null));
        }
    }
}