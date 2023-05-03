namespace EntityFrameworkNews.Models.Responses;

public class Response<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string QueryTime { get; set; }

    public Response() { }

    public Response(bool success, T? data, string queryTime = "0")
    {
        Success = success;
        Data = data;
        QueryTime = queryTime;
    }
}
