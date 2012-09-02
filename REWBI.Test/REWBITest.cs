namespace REWBI.Test
{
    #region Using statement
    using System;
    using RedisReceiveChannel;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using NUnit.Framework;
    using RedisSendChannel;
    using RedisTransportChannel;
    #endregion
    [TestFixture]
    public class RewbiTest
    {
        [Test]
        public void CanBuildSendChannelFactory()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisSendTransportBinding());

            Assert.True(binding.CanBuildChannelFactory<IDuplexChannel>());
        }

        [Test]
        public void SendFactory()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisSendTransportBinding());
            EndpointAddress address = new EndpointAddress("redis://localhost:6379");

            IChannelFactory<IDuplexChannel> factory = binding.BuildChannelFactory<IDuplexChannel>();
            Assert.IsInstanceOf<RedisSendFactory>(factory);
            factory.Open();
            IDuplexChannel duplexChannel = factory.CreateChannel(address);
            Assert.IsInstanceOf<RedisSendChannel>(duplexChannel);
        }

        [Test]
        public void SendChannel()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisSendTransportBinding());
            EndpointAddress address = new EndpointAddress("redis://localhost:6379");

            IChannelFactory<IDuplexChannel> factory = binding.BuildChannelFactory<IDuplexChannel>();
            factory.Open();
            IDuplexChannel duplexChannel = factory.CreateChannel(address);
            Assert.IsInstanceOf<RedisSendChannel>(duplexChannel);
        }

        [Test]
        public void CanBuildReceiveChannelFactory()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisReceiveTransportBinding());

            Assert.True(binding.CanBuildChannelFactory<IRequestChannel>());
        }

        [Test]
        public void ReceiveFactory()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisReceiveTransportBinding());

            IChannelFactory<IRequestChannel> factory = binding.BuildChannelFactory<IRequestChannel>();
            Assert.IsInstanceOf<RedisReceiveFactory>(factory);
        }

        [Test]
        public void ReceiveChannel()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisReceiveTransportBinding());
            EndpointAddress address = new EndpointAddress("redis://localhost:6379");

            IChannelFactory<IRequestChannel> factory = binding.BuildChannelFactory<IRequestChannel>();
            factory.Open();
            IRequestChannel channel = factory.CreateChannel(address);
            Assert.IsInstanceOf<RedisReceiveChannel>(channel);
        }

        [Test]
        public void ReceiveMessage()
        {
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisReceiveTransportBinding());
            EndpointAddress address = new EndpointAddress("redis://localhost:6379");

            IChannelFactory<IRequestChannel> factory = binding.BuildChannelFactory<IRequestChannel>();
            factory.Open();
            IRequestChannel channel = factory.CreateChannel(address);
            channel.Open();
            Message requestMessage = Message.CreateMessage(MessageVersion.Default, "http://tempuri", "Key");
            Message replyMessage = channel.Request(requestMessage, TimeSpan.FromSeconds(5));
            StringAssert.Contains("Value",replyMessage.GetBody<string>());
        }
    }
}
