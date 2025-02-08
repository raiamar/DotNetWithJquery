using System.Text.Json.Serialization;

public class CommandResponse : Response
{
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public List<string> Errors { get; set; } = new List<string>();
}

public class Response
{
    public bool Success { get; set; } = true;
    public string Message { get; set; } = "success";
}