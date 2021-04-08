using System.Linq;

namespace ModelClassLibrary.ResponseModel
{
    public class ApiResponse
    {
        public ApiResponse()
        {
            Outcome = new OperationOutcome
            {
                Errors = Enumerable.Empty<string>(),
                Message = string.Empty,
                OpResult = OpResult.Success // optimistic 🤞
            };
        }
        public OperationOutcome Outcome { get; set; }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public ApiResponse():base()
        {
        }
        public T Data { get; set; }

        public Pagination Pagination { get; set; }
    }    
}
