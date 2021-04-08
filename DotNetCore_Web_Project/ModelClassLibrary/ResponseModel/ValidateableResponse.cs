using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ModelClassLibrary.ResponseModel
{
    public class ValidateableResponse : ApiResponse
    {
        private readonly IList<string> _errorMessages;

        public ValidateableResponse(IList<string> errors = null) : base()
        {
            _errorMessages = errors ?? new List<string>();
        }

        public bool IsValidResponse => !_errorMessages.Any();

        public IReadOnlyCollection<string> Errors => new ReadOnlyCollection<string>(_errorMessages);
    }

    public class ValidateableResponse<T> : ValidateableResponse
    {
        public ValidateableResponse(IList<string> errors = null)
            : base(errors)
        {
        }

        public T Data { get; set; }
    }
}