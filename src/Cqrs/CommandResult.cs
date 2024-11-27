using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MicroDotNet.Packages.Cqrs
{
    public class CommandResult
    {
        public CommandResult(
            int resultCode,
            IEnumerable<Message> messages)
        {
            this.ResultCode = resultCode;
            this.Messages = new ReadOnlyCollection<Message>(messages.ToList());
        }

        public int ResultCode { get; }

        public ReadOnlyCollection<Message> Messages { get; }
    }
}