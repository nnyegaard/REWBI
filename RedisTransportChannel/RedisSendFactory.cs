using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace RedisSendChannel
{
    public class RedisSendFactory : ChannelFactoryBase<IDuplexChannel>
    {
        private readonly RedisSendTransportBinding _bindingElement;

        /// <summary>
        /// Setting our transportbindingelement
        /// </summary>
        /// <param name="bindingElement"></param>
        /// <param name="contex"></param>
        public RedisSendFactory(RedisSendTransportBinding bindingElement, BindingContext contex) : base(contex.Binding)
        {
            Console.WriteLine("RedisChannelFactory: Constructing Factory");
            _bindingElement = bindingElement;
        } 

        #region Overrides of CommunicationObject

        /// <summary>
        /// Opening the factory
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnOpen(TimeSpan timeout)
        {
            Console.WriteLine("RedisChannelFactory: OnOpen");
        }

        /// <summary>
        /// Async Open. Not implementet
        /// </summary>
        /// <param name="timeout"></param>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Event OnEndOpen. Not implementet
        /// </summary>
        /// <param name="result"></param>
        protected override void OnEndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of ChannelFactoryBase<IDuplexChannel>

        /// <summary>
        /// Will create and return a new RedisSendChannel
        /// </summary>
        /// <param name="address"></param>
        /// <param name="via"></param>
        /// <returns>RedisSendChannel</returns>
        protected override IDuplexChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            Console.WriteLine("RedisChannelFactory: OnCreateChannel");
            return new RedisSendChannel(via, _bindingElement);
        }

        #endregion
    }
}