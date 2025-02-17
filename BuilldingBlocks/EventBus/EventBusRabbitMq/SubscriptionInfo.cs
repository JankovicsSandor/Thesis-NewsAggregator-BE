﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EventBusRabbitMQ
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        public class SubscriptionInfo
        {

            public Type HandlerType { get; }

            private SubscriptionInfo(Type handlerType)
            {
                HandlerType = handlerType;
            }
            public static SubscriptionInfo Typed(Type handlerType)
            {
                return new SubscriptionInfo(handlerType);
            }
        }
    }
}
