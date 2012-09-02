using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceStack.Redis;

namespace RedisSendChannel
{

    public class RedisSendListener : ChannelListenerBase<IDuplexChannel>
    {
        private Uri _uri;
        private TransportBindingElement _bindingElement;
        private CommunicationState _state = CommunicationState.Created;
        private RedisNativeClient _client;

        /// <summary>
        /// Constructer, setting our fields
        /// </summary>
        /// <param name="bindingElement"></param>
        /// <param name="context"></param>
        public RedisSendListener(TransportBindingElement bindingElement, BindingContext context)
        {
            Console.WriteLine("RedisChannelListener: Constructing Listener");
            _bindingElement = bindingElement;
            _uri = new Uri(context.ListenUriBaseAddress, context.ListenUriRelativeAddress); 
        } 

        #region Overrides of CommunicationObject

        #region Async methods

        protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override void OnAbort()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Setting the state to closed
        /// </summary>
        /// <param name="timeout"></param>
        protected override void OnClose(TimeSpan timeout)
        {
            Console.WriteLine("RedisChannelListener: OnClose");
            _state = CommunicationState.Closed;
        }

        protected override void OnEndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }


        protected override void OnOpen(TimeSpan timeout)
        {
            Console.WriteLine("RedisChannelListener: OnOpen method");
            if (_uri == null)
                throw new InvalidOperationException("Some address is required before this Channel Listener can be used");
            _client = new RedisNativeClient();
            _client.Subscribe(new[] { "test" });
        }

        protected override void OnEndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of ChannelListenerBase

        #region Async methods

        protected override IAsyncResult OnBeginWaitForChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        protected override bool OnWaitForChannel(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        protected override bool OnEndWaitForChannel(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        public override Uri Uri
        {
            get
            {
                Console.WriteLine("RedisChannelListener: Uri");
                return _uri;
            }
        }

        #endregion

        #region Overrides of ChannelListenerBase<IDuplexChannel>

        #region Async methods

        protected override IAsyncResult OnBeginAcceptChannel(TimeSpan timeout, AsyncCallback callback, object state)
        {
            Console.WriteLine("RedisChannelListener: OnBeginAcceptChannel");
            /*
             * It should have been here we had a listener that we could get message
             */

            throw new NotImplementedException();
        }

        #endregion

        protected override IDuplexChannel OnAcceptChannel(TimeSpan timeout)
        {
            Console.WriteLine("RedisChannelListener: OnAcceptChannel");
            return _state != CommunicationState.Closed ? new RedisSendChannel(_uri, _bindingElement, _client) : null;
        }

        protected override IDuplexChannel OnEndAcceptChannel(IAsyncResult result)
        {
            _client = new RedisClient(_uri);
            Console.WriteLine("RedisChannelListener: OnEndAcceptChannel");
            return _state != CommunicationState.Closed ? new RedisSendChannel(_uri, _bindingElement, _client) : null;
        }

        #endregion


    }
}