using System;
using System.Collections.Generic;
using System.Linq;

namespace ModelClassLibrary.ResponseModel
{
    public class ServiceResponse<T> : ServiceResponse
    {
        private T _result;

        public T Result
        {
            get
            {
                if (Errors != null && Errors.Any())
                { throw new Exception(string.Join(",", Errors)); }

                return _result;
            }
            set => _result = value;
        }

        public Pagination Pagination { get; set; }

    }

    public class ServiceResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public static ServiceResponse<T> FromResult<T>(T result) => new ServiceResponse<T> { Result = result };
    }
}