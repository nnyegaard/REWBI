using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;

namespace RedisTransportChannel
{
    public class RedisMessageEncoder : MessageEncoder
    {
        private MessageVersion _version;

        public RedisMessageEncoder(RedisMessageFactory factory)
        {
            Console.WriteLine("RedisMessageEncoder: RedisMessageEncoder");
            _version = factory.MessageVersion;
        }

        #region Overrides of MessageEncoder

        /// <summary>
        /// When overridden in a derived class, reads a message from a specified stream.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> that is read from the stream specified.
        /// </returns>
        /// <param name="stream">The <see cref="T:System.IO.Stream"/> object from which the message is read.</param><param name="maxSizeOfHeaders">The maximum size of the headers that can be read from the message.</param><param name="contentType">The Multipurpose Internet Mail Extensions (MIME) message-level content-type.</param>
        public override Message ReadMessage(Stream stream, int maxSizeOfHeaders, string contentType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, reads a message from a specified stream.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.Message"/> that is read from the stream specified.
        /// </returns>
        /// <param name="buffer">A <see cref="T:System.ArraySegment`1"/> of type <see cref="T:System.Byte"/> that provides the buffer from which the message is deserialized.</param><param name="bufferManager">The <see cref="T:System.ServiceModel.Channels.BufferManager"/> that manages the buffer from which the message is deserialized.</param><param name="contentType">The Multipurpose Internet Mail Extensions (MIME) message-level content-type.</param>
        public override Message ReadMessage(ArraySegment<byte> buffer, BufferManager bufferManager, string contentType)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, writes a message to a specified stream.
        /// </summary>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> to write to the <paramref name="stream"/>.</param><param name="stream">The <see cref="T:System.IO.Stream"/> object to which the <paramref name="message"/> is written.</param>
        public override void WriteMessage(Message message, Stream stream)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, writes a message of less than a specified size to a byte array buffer at the specified offset.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.ArraySegment`1"/> of type byte that provides the buffer to which the message is serialized.
        /// </returns>
        /// <param name="message">The <see cref="T:System.ServiceModel.Channels.Message"/> to write to the message buffer.</param><param name="maxMessageSize">The maximum message size that can be written.</param><param name="bufferManager">The <see cref="T:System.ServiceModel.Channels.BufferManager"/> that manages the buffer to which the message is written.</param><param name="messageOffset">The offset of the segment that begins from the start of the byte array that provides the buffer.</param>
        public override ArraySegment<byte> WriteMessage(Message message, int maxMessageSize, BufferManager bufferManager, int messageOffset)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// When overridden in a derived class, gets the MIME content type used by the encoder.
        /// </summary>
        /// <returns>
        /// The content type that is supported by the message encoder.
        /// </returns>
        public override string ContentType
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// When overridden in a derived class, gets the media type value that is used by the encoder.
        /// </summary>
        /// <returns>
        /// The media type that is supported by the message encoder.
        /// </returns>
        public override string MediaType
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// When overridden in a derived class, gets the message version value that is used by the encoder.
        /// </summary>
        /// <returns>
        /// The <see cref="T:System.ServiceModel.Channels.MessageVersion"/> that is used by the encoder.
        /// </returns>
        public override MessageVersion MessageVersion
        {
            get
            {
                Console.WriteLine("RedisMessageEncoder: MessageVersion MessageVersion");
                return _version;
            }
        }

        #endregion
    }
}
