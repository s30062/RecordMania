namespace src.DAL.Models;

public class ProgrammingLanguage {
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    
    public ICollection<Record> Records { get; set; } = new List<Record>();
}