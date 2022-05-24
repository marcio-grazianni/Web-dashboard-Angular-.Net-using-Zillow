namespace CDACommercial.PoC.Application.Api.DTO
{
    public class Response<T>
    {
        public bool Success { get => Data != null; }
        public T Data { get; set; }
        public string Error { get; set; }

    }
}