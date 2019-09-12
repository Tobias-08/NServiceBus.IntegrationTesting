﻿using NServiceBus;
using System;
using System.IO;
using System.Linq;

namespace MyOtherService
{
    public class MyOtherServiceConfiguration : EndpointConfiguration
    {
        public MyOtherServiceConfiguration()
            : base("MyOtherService")
        {
            var scanner = this.AssemblyScanner();
            scanner.IncludeOnly("MyOtherService.dll", "MyMessages.dll");

            this.UseSerialization<NewtonsoftSerializer>();
            this.UsePersistence<LearningPersistence>();
            var transportConfig = this.UseTransport<LearningTransport>();

            this.SendFailedMessagesTo("error");

            Conventions()
                .DefiningMessagesAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages"))
                .DefiningEventsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages.Events"))
                .DefiningCommandsAs(t => t.Namespace != null && t.Namespace.EndsWith(".Messages.Commands"));
        }
    }
}
