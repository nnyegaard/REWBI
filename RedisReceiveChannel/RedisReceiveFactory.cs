using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace RedisReceiveChannel
{
    /// <summary>
    /// ChannelFactory
    /// </summary>
    public class RedisReceiveFactory : ChannelFactoryBase<IRequestChannel>
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contex"></param>
        public RedisReceiveFactory(BindingContext contex)
            : base(contex.Binding)
        {
            Console.WriteLine("Factory: Constructing Factory");
        }


        #region Overrides of CommunicationObject

        /// <summary>
        /// Inserts processing on a communication object after it transitions into the opening state which must complete within a specified interval of time.
        /// </summary>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the on open operation has to complete before timing out.</param><exception cref="T:System.InvalidOperationException">The communication object is not in a <see cref="F:System.ServiceModel.CommunicationState.Opened"/> or <see cref="F:System.ServiceModel.CommunicationState.Opening"/> state and cannot be modified.</exception><exception cref="T:System.ObjectDisposedException">The communication object is in a <see cref="F:System.ServiceModel.CommunicationState.Closing"/> or <see cref="F:System.ServiceModel.CommunicationState.Closed"/> state and cannot be modified.</exception><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException">The communication object is in a <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state and cannot be modified.</exception><exception cref="T:System.TimeoutException">The default interval of time that was allotted for the operation was exceeded before the operation was completed.</exception>
        protected override void OnOpen(TimeSpan timeout)
        {
            Console.WriteLine("Factory: OnOpen");
        }

        /// <summary>
        /// Inserts processing on a communication object after it transitions to the opening state due to the invocation of an asynchronous open operation.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.IAsyncResult"/> that references the asynchronous on open operation. 
        /// </returns>
        /// <param name="timeout">The <see cref="T:System.Timespan"/> that specifies how long the on open operation has to complete before timing out.</param><param name="callback">The <see cref="T:System.AsyncCallback"/> delegate that receives notification of the completion of the asynchronous on open operation.</param><param name="state">An object, specified by the application, that contains state information associated with the asynchronous on open operation.</param><exception cref="T:System.InvalidOperationException">The communication object is not in a <see cref="F:System.ServiceModel.CommunicationState.Opened"/> or <see cref="F:System.ServiceModel.CommunicationState.Opening"/> state and cannot be modified.</exception><exception cref="T:System.ObjectDisposedException">The communication object is in a <see cref="F:System.ServiceModel.CommunicationState.Closing"/> or <see cref="F:System.ServiceModel.CommunicationState.Closed"/> state and cannot be modified.</exception><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException">The communication object is in a <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state and cannot be modified.</exception><exception cref="T:System.TimeoutException">The default interval of time that was allotted for the operation was exceeded before the operation was completed.</exception>
        protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Completes an asynchronous operation on the open of a communication object.
        /// </summary>
        /// <param name="result">The <see cref="T:System.IAsyncResult"/> that is returned by a call to the <see cref="M:System.ServiceModel.Channels.CommunicationObject.OnEndOpen(System.IAsyncResult)"/> method.</param><exception cref="T:System.InvalidOperationException">The communication object is not in a <see cref="F:System.ServiceModel.CommunicationState.Opened"/> or <see cref="F:System.ServiceModel.CommunicationState.Opening"/> state and cannot be modified.</exception><exception cref="T:System.ObjectDisposedException">The communication object is in a <see cref="F:System.ServiceModel.CommunicationState.Closing"/> or <see cref="F:System.ServiceModel.CommunicationState.Closed"/> state and cannot be modified.</exception><exception cref="T:System.ServiceModel.CommunicationObjectFaultedException">The communication object is in a <see cref="F:System.ServiceModel.CommunicationState.Faulted"/> state and cannot be modified.</exception><exception cref="T:System.TimeoutException">The default interval of time that was allotted for the operation was exceeded before the operation was completed.</exception>
        protected override void OnEndOpen(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Overrides of ChannelFactoryBase<IRequestChannel>

        /// <summary>
        /// When implemented in a derived class, provides an extensibility point when creating channels.
        /// </summary>
        /// <returns>
        /// A channel of type X with the specified addresses.
        /// </returns>
        /// <param name="address">The <see cref="T:System.ServiceModel.EndpointAddress"/> of the remote endpoint to which the channel sends messages.</param><param name="via">The <see cref="T:System.Uri"/> that contains the transport address to which messages are sent on the output channel.</param>
        protected override IRequestChannel OnCreateChannel(EndpointAddress address, Uri via)
        {
            Console.WriteLine("Factory: OnCreateChannel");
            return new RedisReceiveChannel(via);
        }

        #endregion
    }
}