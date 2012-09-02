using System;
using System.ServiceModel.Channels;

namespace RedisSendChannel
{
    public class RedisSendTransportBinding : TransportBindingElement
    {
        /// <summary>
        /// Standard constructor
        /// </summary>
        public RedisSendTransportBinding()
        {
            Console.WriteLine("RedisTransportBindingElement: Constructing binding element");
        }

        /// <summary>
        /// Constructur used for cloning the bindingelement
        /// </summary>
        /// <param name="original"></param>
        public RedisSendTransportBinding(RedisSendTransportBinding original)
        {
            
            Console.WriteLine("RedisTransportBindingElement: Copying the binding element");
        }

        /// <summary>
        /// Will return a new RedisTransportBindingElement
        /// </summary>
        /// <returns>RedisTransportBindingElement</returns>
        public override BindingElement Clone()
        {
            Console.WriteLine("RedisTransportBindingElement: Cloning the binding element");
            return new RedisSendTransportBinding(this);
        }

        /// <summary>
        /// Will return a typed object such as an interface or status from the appropriate layer in the channel
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <returns>T</returns>
        public override T GetProperty<T>(BindingContext context)
        {
            Console.WriteLine("RedisTransportBindingElement: Querying property {0}", typeof(T));
            return context.GetInnerProperty<T>();
        }

        /// <summary>
        /// Will check if the channel we are trying to build is of the correct type, before bilding the channelfactory
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns>true or false</returns>
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisTransportBindingElement: Querying if the binding element can build a channel factory of type {0}.", typeof(TChannel).Name);
            if (typeof(TChannel) == typeof(IDuplexChannel))
                return true;
            return false;
        }

        /// <summary>
        /// Will check if the channel we are trying to build is of the correct type, before bilding the channellistener
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns>true or false</returns>
        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisTransportBindingElement: Querying if the binding element can build a listener of type {0}.", typeof(TChannel).Name);
            if (typeof(TChannel) == typeof(IDuplexChannel))
                return true;
            return false;
        }

        /// <summary>
        /// Will build a channelfactory of type RedisChannelFactory
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns>RedisChannelFactory</returns>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisTransportBindingElement: Asking the binding element for a channel factory of type {0}.", typeof(TChannel).Name);
            return (IChannelFactory<TChannel>)new RedisSendFactory(this, context);
        }

        /// <summary>
        /// Will build a channellistener of type RedisChannelListener
        /// </summary>
        /// <typeparam name="TChannel"></typeparam>
        /// <param name="context"></param>
        /// <returns>RedisChannelListener</returns>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisTransportBindingElement: Asking the binding element for a listener of type {0}.", typeof(TChannel).Name);
            return (IChannelListener<TChannel>)new RedisSendListener(this, context);
        }

        /// <summary>
        /// Our schema in our binding, like http://, but redis:// here
        /// </summary>
        public override string Scheme
        {
            get
            {
                Console.WriteLine("RedisTransportBindingElement: Scheme");
                return "redis";
            }
        }

        
    }
}