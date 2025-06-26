namespace AmadeusFlightApý.Models
{
    public class GenericResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }

        public static GenericResponse<T> SuccessResponse(T data, string message = "")
            => new GenericResponse<T> { Success = true, Message = message, Data = data };

        public static GenericResponse<T> ErrorResponse(string error, string message = "")
            => new GenericResponse<T> { Success = false, Error = error, Message = message };
    }
}
