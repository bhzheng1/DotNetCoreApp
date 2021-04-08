using System.Collections.Generic;

namespace FileProcessor.Helper
{
    public class CommandResult<T>
    {
        public CommandResult(){ }

        public CommandResult(string error)
        {
            Errors = new List<string>(){error};
        }

        public CommandResult(T result)
        {
            Result = result;
            Errors = new List<string>();
        }

        public T Result { get; set; }
        public IEnumerable<string> Errors { get; set; }
    }
}
