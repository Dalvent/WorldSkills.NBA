using System;

namespace NBAManagement
{
    [Serializable]
    public class LoginInvalidException : Exception
    {
        public LoginInvalidException() { }
        public LoginInvalidException(string message) : base(message) { }
        public LoginInvalidException(string message, Exception inner) : base(message, inner) { }
        protected LoginInvalidException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
