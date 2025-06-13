using Microsoft.EntityFrameworkCore;
using RecordMania.DAL;
using RecordMania.Services.Dtos;
using src.DAL.Models;
using src.RecordMania.DAL;
using src.RecordMania.DAL.Models;
using src.RecordMania.Services.Interfaces;




public class RecordService : IRecordService
{
    private readonly RecordManiaContext _context;

    public RecordService(RecordManiaContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<RecordDto>> GetFilteredRecordsAsync(DateTime? created, int? languageId, int? taskId)
    {
        var query = _context.Records
            .Include(r => r.Student)
            .Include(r => r.ProgrammingLanguage)
            .Include(r => r.CodeTask) 
            .AsQueryable();

        if (created.HasValue)
            query = query.Where(r => r.Created.Date == created.Value.Date);

        if (languageId.HasValue)
            query = query.Where(r => r.ProgrammingLanguageId == languageId);

        if (taskId.HasValue)
            query = query.Where(r => r.TaskId == taskId);

        return await query
            .OrderByDescending(r => r.Created)
            .ThenBy(r => r.Student.LastName)
            .Select(r => new RecordDto
            {
                Id = r.Id,
                Language = r.ProgrammingLanguage.Name,
                TaskName = r.CodeTask.Name,
                TaskDescription = r.CodeTask.Description,
                StudentFirstName = r.Student.FirstName,
                StudentLastName = r.Student.LastName,
                Email = r.Student.Email,
                ExecutionTime = r.ExecutionTime,
                Created = r.Created
            })
            .ToListAsync();
    }

    public async Task<(RecordDto?, (int, string)?)> CreateRecordAsync(CreateRecordDto dto)
    {
        var student = await _context.Students.FindAsync(dto.StudentId);
        if (student == null)
            return (null, (404, "Student not found"));

        var lang = await _context.Languages.FindAsync(dto.ProgrammingLanguageId);
        if (lang == null)
            return (null, (404, "Programming language not found"));

        CodeTask? task = null;

        if (dto.TaskId.HasValue)
        {
            task = await _context.Tasks.FindAsync(dto.TaskId.Value);
            if (task == null)
                return (null, (404, "Task ID not found"));
        }
        else if (!string.IsNullOrWhiteSpace(dto.TaskName) && !string.IsNullOrWhiteSpace(dto.TaskDescription))
        {
            task = new CodeTask
            {
                Name = dto.TaskName!,
                Description = dto.TaskDescription!
            };
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
        }
        else
        {
            return (null, (400, "Missing task information"));
        }

        var record = new Record
        {
            StudentId = student.Id,
            ProgrammingLanguageId = lang.Id,
            TaskId = task.Id,
            ExecutionTime = dto.ExecutionTime,
            Created = DateTime.UtcNow
        };

        _context.Records.Add(record);
        await _context.SaveChangesAsync();

        return (new RecordDto
        {
            Id = record.Id,
            Language = lang.Name,
            TaskName = task.Name,
            TaskDescription = task.Description,
            StudentFirstName = student.FirstName,
            StudentLastName = student.LastName,
            Email = student.Email,
            ExecutionTime = record.ExecutionTime,
            Created = record.Created
        }, null);
    }
}