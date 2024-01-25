using Newtonsoft.Json;

namespace GATEWAY_SERVICE.Modals
{
    public class ApiResponse<T>
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("data")]
        public T Data { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonConstructor]
        public ApiResponse(bool success, T data, string message)
        {
            Success = success;
            Data = data;
            Message = message;
        }
        public ApiResponse() { }    

        public ApiResponse(bool success, string message)
            : this(success, default!, message) // Utilisation de default!
        {
        }

    }
}
