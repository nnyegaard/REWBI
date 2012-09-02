using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using RedisTransportChannel;
using ServiceStack.Redis;

namespace RedisSendChannel
{
    public class RedisSendChannel : IDuplexChannel
    {
        private CommunicationState _state = CommunicationState.Opened;
        private TransportBindingElement _bindingElement;
        private readonly Uri _uri;
        private EndpointAddress _endpointAddress;
        private RedisNativeClient _client;

        /// <summary>
        /// Constructor used by our ChannelListener
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="bindingElement"></param>
        /// <param name="client"></param>
        public RedisSendChannel(Uri uri, TransportBindingElement bindingElement, RedisNativeClient client)
        {
            Console.WriteLine("RedisSendChannel: Constructing channel from listener");
            _bindingElement = bindingElement;
            _uri = uri;
            _client = client;
            _state = CommunicationState.Created;
        }

        /// <summary>
        /// Constructor used by our ChannelFactory
        /// </summary>
        /// <param name="via"></param>
        /// <param name="bindingElement"></param>
        public RedisSendChannel(Uri via, RedisSendTransportBinding bindingElement)
        {
            Console.WriteLine("RedisSendChannel: Constructing channel from factory");
            _bindingElement = bindingElement;
            _uri = via;
            _state = CommunicationState.Created;
        }

        #region Implementation of ICommunicationObject

        #region Async methods

        /// <summary>
        /// Not implementet
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implementet
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implementet
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Not implementet
        /// </summary>
        /// <param name="callback"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Close the channel now, with out cleaning up
        /// </summary>
        public void Abort()
        {
            Console.WriteLine("RedisSendChannel: Abort");
            Close(TimeSpan.Zero);
        }

        /// <summary>
        /// Should close the listener, but we don't have any
        /// </summary>
        public void Close()
        {
            Console.WriteLine("RedisSendChannel: Close");
        }

        /// <summary>
        /// Should close the listener, but we don't have any, but set the state to closed
        /// </summary>
        public void Close(TimeSpan timeout)
        {
            Console.WriteLine("RedisSendChannel: Close");
            if (_state != CommunicationState.Closed)
            {
                _state = CommunicationState.Closing;
            }

            if (Closed != null)
                Closed(this, null);       
        }

        /// <summary>
        /// Completes an asynchronous operation to close a communication object.
        /// </summary>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> that is returned by a call to the <see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> method.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public void EndClose(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes a communication object to transition from the created state into the opened state.  
        /// </summary>
        /// <exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default open timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public void Open()
        {
            Console.WriteLine("RedisSendChannel: Open()");
            _state = CommunicationState.Opened;
            if (Opened != null)
                Opened(this, null);
        }

        /// <summary>
        /// Causes a communication object to transition from the created state into the opened state within a specified interval of time.
        /// </summary>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The specified timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public void Open(TimeSpan timeout)
        {
            Console.WriteLine("RedisSendChannel: Open()");
            _state = CommunicationState.Opened;
            if (Opened != null)
                Opened(this, null);
        }

        /// <summary>
        /// Completes an asynchronous operation to open a communication object.
        /// </summary>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> that is returned by a call to the <see cref="M:System.ServiceModel.ICommunicationObject.BeginOpen"/> method.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public void EndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the current state of the communication-oriented object.
        /// </summary>
        /// <returns>
        /// The value of the <see cref="T:System.ServiceModel.CommunicationState"/> of the object.
        /// </returns>
        public CommunicationState State
        {
            get
            {
                Console.WriteLine("RedisSendChannel: State  " + _state);
                return _state;
            }
        }

        public event EventHandler Closed;
        public event EventHandler Closing;
        public event EventHandler Faulted;
        public event EventHandler Opened;
        public event EventHandler Opening;
        public T GetProperty<T>() where T : class
        {
            if (typeof(T) == typeof(FaultConverter))
                return new RedisFaultConverter() as T;
            throw new Exception("Error in GetProp RedisChannel");
        }

        #endregion

        #region Implementation of IInputChannel

        #region Async methods

        /// <summary>
        /// Begins an asynchronous operation to receive a message that has a state object associated with it. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous message reception. 
        /// </returns>
        /// <param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the asynchronous operation completion.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous operation.</param>
        public IAsyncResult BeginReceive(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to receive a message that has a specified time out and state object associated with it. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous receive operation.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies the interval of time to wait for a message to become available.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the asynchronous operation completion.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous operation.</param><exception cref="T:System.TimeoutException">The specified <paramref name="timeout"/> is exceeded before the operation is completed.</exception><exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than zero.</exception>
        public IAsyncResult BeginReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to receive a message that has a specified time out and state object associated with it. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous receive operation.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies the interval of time to wait for a message to become available.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the asynchronous operation completion.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous operation.</param><exception cref="T:System.TimeoutException">The specified <paramref name="timeout"/> is exceeded before the operation is completed.</exception><exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than zero.</exception>
        public IAsyncResult BeginTryReceive(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous wait-for-a-message-to-arrive operation that has a specified time out and state object associated with it. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous operation to wait for a message to arrive.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies the interval of time to wait for a message to become available.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the asynchronous operation completion.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous operation.</param><exception cref="T:System.TimeoutException">The specified <paramref name="timeout"/> is exceeded before the operation is completed.</exception><exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than zero.</exception>
        public IAsyncResult BeginWaitForMessage(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Returns the message received, if one is available. If a message is not available, blocks for a default interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> received. 
        /// </returns>
        public Message Receive()
        {
            /*
             * Not getting called.
             */
            Console.WriteLine("RedisSendChannel: Message Receive()");
            RedisClient client = new RedisClient(_uri); //Init a client
            String value = client.GetValue("GetBytes"); //Get a value if a key. Here always test
            return Message.CreateMessage(MessageVersion.None, value); //Create a msg with the value
        }

        /// <summary>
        /// Returns the message received, if one is available. If a message is not available, blocks for a specified interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> received. 
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the receive operation has to complete before timing out and throwing a <see cref="T:System.TimeoutException"/>.</param><exception cref="T:System.TimeoutException">The specified <paramref name="timeout"/> is exceeded before the operation is completed.</exception><exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than zero.</exception>
        public Message Receive(TimeSpan timeout)
        {
           return Receive();
        }

        /// <summary>
        /// Completes an asynchronous operation to receive a message. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> received. 
        /// </returns>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> returned by a call to one of the <see cref="Overload:System.ServiceModel.Channels.IInputChannel.BeginReceive"/> methods.</param>
        public Message EndReceive(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tries to receive a message within a specified interval of time. 
        /// </summary>
        /// <returns>
        /// true if a message is received before the <paramref name="timeout"/> has been exceeded; otherwise false.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.IAsyncResult"/> returned by a call to one of the <see cref="Overload:System.ServiceModel.Channels.IInputChannel.BeginReceive"/> methods.</param><param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> received. </param><exception cref="T:System.TimeoutException">The specified <paramref name="timeout"/> is exceeded before the operation is completed.</exception><exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than zero.</exception>
        public bool TryReceive(TimeSpan timeout, out Message message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes the specified asynchronous operation to receive a message.
        /// </summary>
        /// <returns>
        /// true if a message is received before the specified interval of time elapses; otherwise false.
        /// </returns>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> returned by a call to the <see cref="M:System.ServiceModel.Channels.IInputChannel.BeginTryReceive(System.TimeSpan,System.AsyncCallback,System.Object)"/> method.</param><param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> received. </param>
        public bool EndTryReceive(IAsyncResult result, out Message message)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns a value that indicates whether a message has arrived within a specified interval of time.
        /// </summary>
        /// <returns>
        /// true if a message has arrived before the <paramref name="timeout"/> has been exceeded; otherwise false.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> specifies the maximum interval of time to wait for a message to arrive before timing out.</param><exception cref="T:System.TimeoutException">The specified <paramref name="timeout"/> is exceeded before the operation is completed.</exception><exception cref="T:System.ArgumentOutOfRangeException">The timeout specified is less than zero.</exception>
        public bool WaitForMessage(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes the specified asynchronous wait-for-a-message operation.
        /// </summary>
        /// <returns>
        /// true if a message has arrived before the <paramref name="timeout"/> has been exceeded; otherwise false.
        /// </returns>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> that identifies the <see cref="M:System.ServiceModel.Channels.IInputChannel.BeginWaitForMessage(System.TimeSpan,System.AsyncCallback,System.Object)"/> operation to finish, and from which to retrieve an end result.</param>
        public bool EndWaitForMessage(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the address on which the input channel receives messages. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.EndpointAddress"/> on which the input channel receives messages. 
        /// </returns>
        public EndpointAddress LocalAddress
        {
            get
            {
                Console.WriteLine("RedisSendChannel: EndpointAddress");
                return _endpointAddress;
            }
        }

        #endregion

        #region Implementation of IOutputChannel

        #region Async methods

        /// <summary>
        /// Begins an asynchronous operation to transmit a message to the destination of the output channel. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous message transmission. 
        /// </returns>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> being sent on the output channel. </param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate. </param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous send operation.</param>
        public IAsyncResult BeginSend(Message message, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to transmit a message to the destination of the output channel within a specified interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous send operation.
        /// </returns>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> being sent on the output channel.</param><param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the asynchronous operation send completion.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous send operation.</param>
        public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        #endregion

        /// <summary>
        /// Transmits a message to the destination of the output channel. 
        /// </summary>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> being sent on the output channel.</param>
        public void Send(Message message)
        {
            RedisNativeClient nativeClient = null;
            Console.WriteLine("RedisSendChannel: Send");

            string value = message.GetBody<string>(); // Get the body of our message
            /*
             * Ugly pull of our host to connect to. Should use a regularexpression maybe. The reason we need this is that RedisNativClient can't take a Uri as input
             * Bug in ServiceStack.Redis. It will always add: ':6379' that is the default port for redis
             */
            String[] db = _uri.ToString().Split(':'); 
            String[] db2 = db[1].Split('/');
            try
            {
                nativeClient = new RedisNativeClient(db2[2]); // Create a connection to redis with the host sendt from the channel stack
            }
            catch (RedisException)
            {
                
                Console.WriteLine("ERROR: We could not connect to the redis server.");
            }
            
            /*
             * End of the uglyness
             */

            string[] ourMessage = value.Split(':'); // Split our message op so we got key : value
            try
            {
                if (nativeClient != null)
                    nativeClient.Set(ourMessage[0], GetBytes(ourMessage[1])); // Send our data to Redis
            }
            catch (IndexOutOfRangeException e)
            {
                //We should throw the exception to a channel fault and handle it higher up in the code, but for now this is ok:
                Console.WriteLine("ERROR: We need a message with 'key:value' style.");
            }
           
        }

        /// <summary>
        /// Sends a message on the current output channel within a specified interval of time.
        /// </summary>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> being sent on the output channel.</param><param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param>
        public void Send(Message message, TimeSpan timeout)
        {
            Console.WriteLine("RedisSendChannel: Send timeout");
            Send(message);
        }

        /// <summary>
        /// Completes an asynchronous operation to transmit a message to the destination of the output channel. 
        /// </summary>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> returned by a call to the <see cref="Overload:System.ServiceModel.Channels.IOutputChannel.BeginSend"/>  method. </param>
        public void EndSend(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the destination of the service to which messages are sent out on the output channel. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.EndpointAddress"/> of the service to which the output channel sends messages. 
        /// </returns>
        public EndpointAddress RemoteAddress
        {
            get
            {
                Console.WriteLine("RedisSendChannel: EndpointAddress RemoteAddress");
                return _endpointAddress;
            }
        }

        /// <summary>
        /// Gets the URI that contains the transport address to which messages are sent on the output channel.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Uri"/> that contains the transport address to which messages are sent on the output channel.
        /// </returns>
        public Uri Via
        {
            get
            {
                Console.WriteLine("RedisSendChannel: Uri Via");
                return _uri;
            }
        }

        #endregion

        private byte[] GetBytes(String str)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            return bytes;
        }
    }
}
