using System;

namespace OpenApi
{
    [Serializable]
    public class ApiResponse<TResult>
    {
        /// <summary>
        /// Error code
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Supplementary message
        /// </summary>
        public string Details { get; set; }
        /// <summary>
        /// return value
        /// </summary>
        public TResult Result { get; set; }
        public ApiResponse() { }
        public ApiResponse(
            string code,
            string message,
            string details = null)
        {
            Code = code;
            Message = message;
            Details = details;
        }

        public ApiResponse(
            string code,
            TResult result,
            string message = "OK")
        {
            Code = code;
            Result = result;
            Message = message;
        }
    }
}
