namespace Icon.Api.Response
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            this.Succeeded = true;
        }
        public T Data { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
