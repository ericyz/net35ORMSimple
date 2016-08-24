using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleORM.CustomException {
    public class SimpleORMException : System.Exception, ISerializable {
        public SimpleORMException() { }

        public SimpleORMException(string message)
         : base(message) { }

        public SimpleORMException(string message, System.Exception inner) : base(message, inner) { }

        public SimpleORMException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
