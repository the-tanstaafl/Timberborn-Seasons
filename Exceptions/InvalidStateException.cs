using System;

namespace FloodSeason.Exceptions;

public class InvalidStateException : Exception
{
    public InvalidStateException(string message) : base(message)
    {
    }
}