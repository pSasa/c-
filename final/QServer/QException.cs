using System;

namespace QServer
{
    sealed class QServerException : Exception
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

    sealed class InvalidRequestFormat : Exception
    {
        public InvalidRequestFormat(string message)
            : base(message)
        {
        }
    }

    sealed class DBException : Exception
    {
        public DBException(string message)
            : base(message)
        {
        }
    }
}
