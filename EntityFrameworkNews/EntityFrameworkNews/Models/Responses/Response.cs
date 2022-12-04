namespace EntityFrameworkNews.Models.Responses;

public class Response<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }

    public Response() { }

    public Response(bool success, T? data)
    {
        Success = success;
        Data = data;
    }
}
