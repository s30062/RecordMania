namespace RecordMania.Services.Dtos;

public class RecordDto {
    public int Id { get; set; }
    public string Language { get; set; } = null!;
    public string TaskName { get; set; } = null!;
    public string TaskDescription { get; set; } = null!;
    public string StudentFirstName { get; set; } = null!;
    public string StudentLastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public double ExecutionTime { get; set; }
    public DateTime Created { get; set; }
}