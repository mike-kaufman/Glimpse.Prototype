﻿using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace Glimpse.Agent
{
    public abstract class BaseMessagePublisher : IMessagePublisher
    {
        public abstract Task PublishMessage(IMessage message);

        protected IMessageEnvelope ConvertMessage(IMessage message)
        {
            // TODO: Probably want to convert the message to JSON at this point
            var newMessage = new MessageEnvelope();
            newMessage.Type = message.GetType().FullName;
            newMessage.Message = JsonConvert.SerializeObject(message, Formatting.None);

            return newMessage;
        }
    }
}