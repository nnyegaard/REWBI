namespace Service
{
    #region Using Statement
    using System;
    using System.ServiceModel;
    using System.ServiceModel.Channels;
    using RedisSendChannel;
    using RedisTransportChannel;
    using RedisReceiveChannel;
    #endregion
    internal class Program
    {
        public static void Main()
        {
            SendData();

            Console.WriteLine("Done sending. Press any key to start receiving");
            Console.ReadKey();

            ReceiveData();

            Console.WriteLine("Done, press any key to close");
            Console.ReadKey();
        }

        private static void SendData()
        {
            Console.WriteLine("Sending data:");
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisSendTransportBinding());
            EndpointAddress address = new EndpointAddress("redis://localhost:6379");


            IChannelFactory<IDuplexChannel> factory = binding.BuildChannelFactory<IDuplexChannel>();
            factory.Open();
            IDuplexChannel duplexChannel = factory.CreateChannel(address);
            duplexChannel.Open();

            Message requestMessage = Message.CreateMessage(MessageVersion.Default, "http://tempuri", "Key:Value");

            duplexChannel.Send(requestMessage);
        }

        private static void ReceiveData()
        {
            Console.WriteLine("Receiving data:");
            Binding binding = new CustomBinding(new RedisMessageBindingElement(), new RedisReceiveTransportBinding());
            EndpointAddress address = new EndpointAddress("redis://localhost:6379");


            IChannelFactory<IRequestChannel> factory = binding.BuildChannelFactory<IRequestChannel>();
            factory.Open();
            IRequestChannel channel = factory.CreateChannel(address);
            channel.Open();
            Message requestMessage = Message.CreateMessage(MessageVersion.Default, "http://tempuri", "Key");
            Message replyMessage = channel.Request(requestMessage, TimeSpan.FromSeconds(5));
            using (replyMessage)
            {
                Console.WriteLine("Processing reply: {0}", replyMessage.Headers.Action);
                Console.WriteLine("Reply: {0}", replyMessage.GetBody<string>());
            }
        }
    }
}