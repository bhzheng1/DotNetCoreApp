using System.Collections.Generic;
using NServiceBus;

namespace NSBusMessages
{
    public class CommandResult<T>:IMessage
    {
        public T Result { get; set; }

        public IEnumerable<string> Errors { get; set; }
        public CommandResult() { }

        public CommandResult(string error)
        {
            Errors = new List<string>(){error};
        }

        public CommandResult(T result)
        {
            Result = result;
            Errors=new List<string>();
        }
    }
}
