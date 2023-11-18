using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace InnostepIT.Framework.Core.Contract.Configuration.Exceptions
{
    [Serializable]
    [ExcludeFromCodeCoverage]
    public class ConfigurationNotFoundException : Exception
    {
        //
        // For guidelines regarding the creation of new exception types, see
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
        // and
        //    http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
        //

        public ConfigurationNotFoundException()
        {
        }

        public ConfigurationNotFoundException(string message) : base(message)
        {
        }

        public ConfigurationNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected ConfigurationNotFoundException(SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}
