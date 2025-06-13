using Microsoft.AspNetCore.Mvc;
using RecordMania.Services.Dtos;
using src.RecordMania.Services.Interfaces;

namespace RecordMania.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordsController : ControllerBase {
    private readonly IRecordService _recordService;

    public RecordsController(IRecordService recordService) {
        _recordService = recordService;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] DateTime? created, [FromQuery] int? languageId, [FromQuery] int? taskId) {
        var records = await _recordService.GetFilteredRecordsAsync(created, languageId, taskId);
        return Ok(records);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateRecordDto dto) {
        var (record, error) = await _recordService.CreateRecordAsync(dto);

        if (error != null)
            return StatusCode(error.Value.Item1, error.Value.Item2);

        return CreatedAtAction(nameof(Get), new { id = record!.Id }, record);
    }
}