using System;
using System.ServiceModel.Channels;

namespace RedisTransportChannel
{
    public class RedisMessageBindingElement : MessageEncodingBindingElement
    {
        private MessageVersion _version = MessageVersion.None;

        public RedisMessageBindingElement()
        {
            Console.WriteLine("RedisMessageBindingElement: RedisMessageBindingElement");
        }

        public RedisMessageBindingElement(RedisMessageBindingElement bindingElement)
        {
            Console.WriteLine("RedisMessageBindingElement: RedisMessageBindingElement(RedisMessageBindingElement bindingElement)");
            _version = bindingElement._version;
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
            Console.WriteLine("RedisMessageBindingElement: Clone");
            return new RedisMessageBindingElement(this);
        }

        #endregion

        #region Overrides of MessageEncodingBindingElement

        /// <summary>
        /// When overridden in a derived class, creates a factory for producing message encoders.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.MessageEncoderFactory"/> used to produce message encoders.
        /// </returns>
        public override MessageEncoderFactory CreateMessageEncoderFactory()
        {
            Console.WriteLine("RedisMessageBindingElement: MessageEncoderFactory CreateMessageEncoderFactory");
            return new RedisMessageFactory(this);
        }

        /// <summary>
        /// When overridden in a derived class, gets or sets the message version that can be handled by the message encoders produced by the message encoder factory.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.MessageVersion"/> used by the encoders produced by the message encoder factory.
        /// </returns>
        public override MessageVersion MessageVersion
        {
            get
            {
                Console.WriteLine("RedisMessageBindingElement: MessageVersion MessageVersion get");
                return _version;
            }
            set
            {
                Console.WriteLine("RedisMessageBindingElement: MessageVersion MessageVersion set");
                _version = value;
            }
        }

        #endregion

        /// <summary>
        /// Initializes a channel factory for producing channels of a specified type from the binding context.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.IChannelFactory`1"/> of type <paramref name="TChannel"/> initialized from the <paramref name="context"/>.
        /// </returns>
        /// <param name="context">The <see cref="T:System.ServiceModel.Channels.BindingContext"/> that provides context for the binding element. </param><typeparam name="TChannel">The type of channel the factory builds.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="context"/> is null.</exception>
        public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisMessageBindingElement: BuildChannelFactory");
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelFactory<TChannel>();
        }

        /// <summary>
        /// Returns a value that indicates whether the binding element can build a channel factory for a specific type of channel.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.ServiceModel.Channels.IChannelFactory`1"/> of type <paramref name="TChannel"/> can be built by the binding element; otherwise, false.
        /// </returns>
        /// <param name="context">The <see cref="T:System.ServiceModel.Channels.BindingContext"/> that provides context for the binding element. </param><typeparam name="TChannel">The type of channel the channel factory produces.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="context"/> is null.</exception>
        public override bool CanBuildChannelFactory<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisMessageBindingElement: CanBuildChannelFactory");
            return context.CanBuildInnerChannelFactory<TChannel>();
        }

        /// <summary>
        /// Initializes a channel listener to accept channels of a specified type from the binding context.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.IChannelListener`1"/> of type <see cref="T:System.ServiceModel.Channels.IChannel"/> initialized from the <paramref name="context"/>.
        /// </returns>
        /// <param name="context">The <see cref="T:System.ServiceModel.Channels.BindingContext"/> that provides context for the binding element.</param><typeparam name="TChannel">The type of channel the listener is built to accept.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="context"/> is null.</exception>
        public override IChannelListener<TChannel> BuildChannelListener<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisMessageBindingElement: BuildChannelListener");
            context.BindingParameters.Add(this);
            return context.BuildInnerChannelListener<TChannel>();
        }

        /// <summary>
        /// Returns a value that indicates whether the binding element can build a listener for a specific type of channel.
        /// </summary>
        /// <returns>
        /// true if the <see cref="T:System.ServiceModel.Channels.IChannelListener`1"/> of type <see cref="T:System.ServiceModel.Channels.IChannel"/> can be built by the binding element; otherwise, false.
        /// </returns>
        /// <param name="context">The <see cref="T:System.ServiceModel.Channels.BindingContext"/> that provides context for the binding element. </param><typeparam name="TChannel">The type of channel the listener accepts.</typeparam><exception cref="T:System.ArgumentNullException"><paramref name="context"/> is null.</exception>
        public override bool CanBuildChannelListener<TChannel>(BindingContext context)
        {
            Console.WriteLine("RedisMessageBindingElement: CanBuildChannelListener");
            context.BindingParameters.Add(this);
            return context.CanBuildInnerChannelListener<TChannel>();
        }
    }
}