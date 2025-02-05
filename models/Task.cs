public class Task
{
    public required int Id {get; set;}
    public required string Description {get; set;}
    public Status Status {get; set;}
    public DateTime CreatedAt {get; set;}
    public DateTime UpdatedAt {get; set;}
}