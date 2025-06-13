using src.RecordMania.DAL.Models;

namespace src.DAL.Models;


public class Record
{
    public int Id { get; set; }
    public double ExecutionTime { get; set; }
    public DateTime Created { get; set; }


    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    
    public int ProgrammingLanguageId { get; set; }
    public ProgrammingLanguage ProgrammingLanguage { get; set; } = null!;
    
    public int TaskId { get; set; }
    public CodeTask CodeTask { get; set; } = null!;
}
