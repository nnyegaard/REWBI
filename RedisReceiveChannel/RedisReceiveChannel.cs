using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using ServiceStack.Redis;

namespace RedisReceiveChannel
{
    /// <summary>
    /// 
    /// </summary>
    public class RedisReceiveChannel : IRequestChannel
    {
        private readonly Uri _via;
        private CommunicationState _state = CommunicationState.Opened;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="via"></param>
        public RedisReceiveChannel(Uri via)
        {
            Console.WriteLine("Channel: Constructing channel from Factory");
            _via = via;
            _state = CommunicationState.Created;
        }

        #region Implementation of ICommunicationObject

        /// <summary>
        /// Causes a communication object to transition immediately from its current state into the closed state.  
        /// </summary>
        public void Abort()
        {
            Close(TimeSpan.Zero);
        }

        /// <summary>
        /// Causes a communication object to transition from its current state into the closed state.  
        /// </summary>
        /// <exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.Close"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default close timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public void Close()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Causes a communication object to transition from its current state into the closed state.  
        /// </summary>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.Close"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public void Close(TimeSpan timeout)
        {
            if (_state != CommunicationState.Closed)
            {
                _state = CommunicationState.Closing;
            }

            if (Closed != null)
                Closed(this, null);
        }

        /// <summary>
        /// Begins an asynchronous operation to close a communication object.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous close operation. 
        /// </returns>
        /// <param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous close operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous close operation.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public IAsyncResult BeginClose(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to close a communication object with a specified timeout.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous close operation.
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous close operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous close operation.</param><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException"><see cref="M:System.ServiceModel.ICommunicationObject.BeginClose"/> was called on an object in the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The specified timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to close gracefully.</exception>
        public IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
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
            Console.WriteLine("RedisReceiveChannel: Open()");
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
            Console.WriteLine("RedisReceiveChannel: Open()");
            _state = CommunicationState.Opened;
            if (Opened != null)
                Opened(this, null);
        }

        /// <summary>
        /// Begins an asynchronous operation to open a communication object.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous open operation. 
        /// </returns>
        /// <param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous open operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous open operation.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The default open timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public IAsyncResult BeginOpen(AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to open a communication object within a specified interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous open operation. 
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the send operation has to complete before timing out.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous open operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous open operation.</param><exception cref="T:System.ServiceModel.CommunicationException">The <see cref="T:System.ServiceModel.ICommunicationObject"/> was unable to be opened and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception><exception cref="T:System.TimeoutException">The specified timeout elapsed before the <see cref="T:System.ServiceModel.ICommunicationObject"/> was able to enter the <see cref="F:System.ServiceModel.CommunicationState.Opened"/> state and has entered the <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state.</exception>
        public IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
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
            get { return _state; }
        }

        /// <summary>
        /// Occurs when the communication object completes its transition from the closing state into the closed state.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Occurs when the communication object first enters the closing state.
        /// </summary>
        public event EventHandler Closing;

        /// <summary>
        /// Occurs when the communication object first enters the faulted state.
        /// </summary>
        public event EventHandler Faulted;

        /// <summary>
        /// Occurs when the communication object completes its transition from the opening state into the opened state.
        /// </summary>
        public event EventHandler Opened;

        /// <summary>
        /// Occurs when the communication object first enters the opening state.
        /// </summary>
        public event EventHandler Opening;

        /// <summary>
        /// Returns a typed object requested, if present, from the appropriate layer in the channel stack.
        /// </summary>
        /// <returns>
        /// The typed object <paramref name="T"/> requested if it is present or null if it is not.
        /// </returns>
        /// <typeparam name="T">The typed object for which the method is querying.</typeparam>
        public T GetProperty<T>() where T : class
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Implementation of IRequestChannel

        /// <summary>
        /// Sends a message-based request and returns the correlated message-based response. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> received in response to the request. 
        /// </returns>
        /// <param name="message">The request <see cref="T:System.ServiceModel.Channels.Message"/> to be transmitted.</param>
        public Message Request(Message message)
        {
            return Request(message, TimeSpan.Zero);
        }

        /// <summary>
        /// Sends a message-based request and returns the correlated message-based response within a specified interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> received in response to the request. 
        /// </returns>
        /// <param name="message">The request <see cref="T:System.ServiceModel.Channels.Message"/> to be transmitted.</param><param name="timeout">The <see cref="T:System.TimeSpan"/> that specifies the interval of time within which a response must be received.</param>
        public Message Request(Message message, TimeSpan timeout)
        {
            RedisClient client = null;
            string valueToGet = message.GetBody<string>();
            try
            {
                client = new RedisClient(_via);
            }
            catch (RedisException)
            {

                Console.WriteLine("ERROR: We could not connect to the redis server.");
            }

            if (client != null)
            {
                string value = client.GetValue(valueToGet);

                Message msg = Message.CreateMessage(message.Version, "Redis server Replay", value);
                return msg;
            }
            Message msgfail = Message.CreateMessage(message.Version, "ERROR", "ERROR: We could not connect to the redis server.");
            return msgfail;
        }

        /// <summary>
        /// Begins an asynchronous operation to transmit a request message to the reply side of a request-reply message exchange. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous message transmission. 
        /// </returns>
        /// <param name="message">The request <see cref="T:System.ServiceModel.Channels.Message"/> to be transmitted.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the completion of the asynchronous operation transmitting a request message.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous operation transmitting a request message.</param>
        public IAsyncResult BeginRequest(Message message, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Begins an asynchronous operation to transmit a request message to the reply side of a request-reply message exchange within a specified interval of time.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous message transmission. 
        /// </returns>
        /// <param name="message">The request <see cref="T:System.ServiceModel.Channels.Message"/> to be transmitted.</param><param name="timeout">The <see cref="T:System.TimeSpan"/> that specifies the interval of time within which a response must be received. (For defaults, see the Remarks section.)</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives the notification of the completion of the asynchronous operation transmitting a request message.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous operation transmitting a request message.</param>
        public IAsyncResult BeginRequest(Message message, TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes an asynchronous operation to return a message-based response to a transmitted request. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> received in response to the request. 
        /// </returns>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> returned by a call to the <see cref="Overload:System.ServiceModel.Channels.IInputChannel.BeginReceive"/> method. </param>
        public Message EndRequest(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the remote address to which the request channel sends messages. 
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.EndpointAddress"/> to which the request channel sends messages. 
        /// </returns>
        public EndpointAddress RemoteAddress
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the transport address to which the request is send.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.Uri"/> that contains the transport address to which the message is sent.
        /// </returns>
        public Uri Via
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }
}