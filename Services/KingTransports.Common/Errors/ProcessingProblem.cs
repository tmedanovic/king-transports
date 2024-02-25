using System.Runtime.Serialization;

namespace KingTransports.Common.Errors
{
    [Serializable]
    public class ProcessingProblem : Exception
    {
        public ProcessingProblem()
        {
        }

        public ProcessingProblem(string message) : base(message)
        {
        }

        public ProcessingProblem(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ProcessingProblem(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}