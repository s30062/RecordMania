namespace RecordMania.Services.Dtos;

public class CreateRecordDto {
    public int StudentId { get; set; }
    public int ProgrammingLanguageId { get; set; }
    public int? TaskId { get; set; }

    public string? TaskName { get; set; }
    public string? TaskDescription { get; set; }

    public double ExecutionTime { get; set; }
}