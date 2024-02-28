using System.Runtime.Serialization;

namespace KingTransports.Common.Errors
{
    [Serializable]
    public class InvalidRequest : Exception
    {
        public InvalidRequest()
        {
        }

        public InvalidRequest(string message) : base(message)
        {
        }

        public InvalidRequest(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidRequest(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}