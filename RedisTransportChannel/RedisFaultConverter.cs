using System;
using System.ServiceModel.Channels;

namespace RedisSendChannel
{
    public class RedisFaultConverter : FaultConverter
    {
        #region Overrides of FaultConverter

        protected override bool OnTryCreateException(Message message, MessageFault fault, out Exception exception)
        {
            throw new NotImplementedException();
        }

        protected override bool OnTryCreateFaultMessage(Exception exception, out Message message)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}