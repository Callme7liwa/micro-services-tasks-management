﻿namespace USER_SERVICE.Modals
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public ApiResponse(bool success, T data, string message)
        {
            Success = success;
            Data = data;
            Message = message;
        }

        public ApiResponse(bool success, string message)
            : this(success, default!, message) 
        {
        }

    }
}
