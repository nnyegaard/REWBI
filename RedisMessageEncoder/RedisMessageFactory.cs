using System;
using System.ServiceModel.Channels;

namespace RedisTransportChannel
{
    public class RedisMessageFactory : MessageEncoderFactory
    {
        private RedisMessageEncoder _redisMessageEncoder;
        private MessageVersion _version;

        public RedisMessageFactory(RedisMessageBindingElement bindingElement)
        {
            Console.WriteLine("RedisMessageFactory: RedisMessageFactory");
            _version = bindingElement.MessageVersion;
        }

        /// <summary>
        /// When overridden in a derived class, gets the message encoder that is produced by the factory.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.MessageEncoder"/> used by the factory.
        /// </returns>
        public override MessageEncoder Encoder
        {
            get
            {
                Console.WriteLine("RedisMessageFactory: MessageEncoder Encoder");
                return _redisMessageEncoder ?? (_redisMessageEncoder = new RedisMessageEncoder(this));
            }
        }

        /// <summary>
        /// When overridden in a derived class, gets the message version that is used by the encoders produced by the factory to encode messages.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.MessageVersion"/> used by the factory.
        /// </returns>
        public override MessageVersion MessageVersion
        {
            get
            {
                Console.WriteLine("RedisMessageFactory: MessageVersion MessageVersion");
                return _version;
            }
        }
    }
}