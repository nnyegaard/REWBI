using System;
using System.ServiceModel.Channels;

namespace RedisReceiveChannel
{
    /// <summary>
    /// TransportBindingElement, last channel in our WCF stack
    /// </summary>
    public class RedisReceiveTransportBinding : TransportBindingElement
    {
        /// <summary>
        /// Are needed because we need to call the default and extra constructor for cloning
        /// </summary>
        public RedisReceiveTransportBinding()
        {
            Console.WriteLine("Binding: Constructing binding element");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        public RedisReceiveTransportBinding(RedisReceiveTransportBinding original)
        {
            Console.WriteLine("Binding: Copying the binding element");
        }

        #region Overrides of BindingElement

        /// <summary>
        /// When overridden in a derived class, returns a copy of the binding element object.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ServiceModel.Channels.BindingElement"/> object that is a deep clone of the original.
        /// </returns>
        public override BindingElement Clone()
        {
            Console.WriteLine("Binding: Cloning the binding element");
            return new RedisReceiveTransportBinding(this);
        }

        #endregion

        #region Overrides of TransportBindingElement

        /// <summary>
        /// Gets the URI scheme for the transport.
        /// </summary>
        /// <returns>
        /// Returns the URI scheme for the transport, which varies depending on what derived class implements this method.
        /// </returns>
        public override string Scheme
        {
            get { return "redis"; }
        }

        #endregion

        /// <summary>
        /// Gets a property from the specified <see cref="T:System.ServiceModel.Channels.BindingContext"/>.
        /// </summary>
        /// <returns>
        /// The property from the specified <see cref="T:System.ServiceModel.Channels.BindingContext"/>.
        /// </returns>
        /// <param name="context">A <see cref="T:System.ServiceModel.Channels.BindingContext"/>.</param><typeparam name="T">The property to get.</typeparam>
        public override T GetProperty<T>(BindingContext context)
        {
            Console.WriteLine("Binding: Querying property {0}", typeof(T));
            return context.GetInnerProperty<T>();
        }

        /// <summary>
        /// Returns a value that indicates whether the binding element can build a channel factory for a specific type of channel.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.ServiceModel.Channels.IChannelFactory`1"/> of type X can be built by the binding element; otherwise, false.
        /// </returns>
        /// <param name="context">The <see cref="T:System.ServiceModel.Channels.BindingContext"/> that provides context for the binding element. </param><typeparam name="TChannel">The type of channel the channel factory produces.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="context"/> is null.</exception>
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            Console.WriteLine("Binding: Querying if the binding element can build a channel factory of type {0}.", typeof(TChannel).Name);
            if (typeof(TChannel) == typeof(IRequestChannel))
                return true;
            return false;
        }

        /// <summary>
        /// Initializes a channel factory for producing channels of a specified type from the binding context.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.IChannelFactory`1"/> of type X initialized from the <paramref name="context"/>.
        /// </returns>
        /// <param name="context">The <see cref="T:System.ServiceModel.Channels.BindingContext"/> that provides context for the binding element. </param><typeparam name="TChannel">The type of channel the factory builds.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="context"/> is null.</exception>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            Console.WriteLine("Binding: Asking the binding element for a channel factory of type {0}.", typeof(TChannel).Name);
            return (IChannelFactory<TChannel>)new RedisReceiveFactory(context);
        }
    }
}
