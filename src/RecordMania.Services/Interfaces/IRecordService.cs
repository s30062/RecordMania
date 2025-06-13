using RecordMania.Services.Dtos;

namespace src.RecordMania.Services.Interfaces;

public interface IRecordService {
    Task<IEnumerable<RecordDto>> GetFilteredRecordsAsync(DateTime? created, int? languageId, int? taskId);
    Task<(RecordDto? Record, (int StatusCode, string Message)? Error)> CreateRecordAsync(CreateRecordDto dto);
}