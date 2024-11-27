using System;

namespace MicroDotNet.Packages.Cqrs
{
    [Flags]
    public enum MessageLevel
    {
        None = 0,
        Information = 1 << 0,
        Warning = 1 << 1,
        Error = 1 << 2,
    }
}