using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QServer
{
    class QServerException : Exception
    {
        public QServerException(string message, Exception inner)
            : base(message, inner)
        {
        }
        public QServerException(string message)
            : base(message)
        {
        }
    }

    class InvalidRequestFormat : Exception
    {
        public InvalidRequestFormat(string message)
            : base(message)
        {
        }
    }

    class DBException : Exception
    {
        public DBException(string message)
            : base(message)
        {
        }
    }
}
