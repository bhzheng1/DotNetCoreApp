using System.Collections.Generic;

namespace ModelClassLibrary.ResponseModel
{
    public class BadRequest400Payload
    {
        public string Title { get; } = "One or more validation errors occurred.";
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }
    }
}