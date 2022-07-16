using System;

namespace Common.Exceptions
{
    public class BoardGameException : Exception
    {
        public BoardGameException() { }

        public BoardGameException(string message) : base(message) { }

        public BoardGameException(string message, Exception inner) : base(message, inner) { }
    }
}