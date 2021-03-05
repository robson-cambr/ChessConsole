using System;

namespace ChessConsole
{
    class ChessConsoleException : Exception
    {
        public ChessConsoleException(string msg) : base(msg)
        {
        }
    }
}
