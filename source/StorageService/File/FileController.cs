namespace StorageService;

[Route("files")]
[ApiController]
public class FileController : ControllerBase
{
    private readonly IFileService _fileService;

    public FileController(IFileService fileService) => _fileService = fileService;

    [HttpDelete("{id:Guid}")]
    public void Delete(Guid id) => _fileService.Delete(id);

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var file = await _fileService.GetAsync(id);

        return file is null ? NotFound() : File(file.Bytes, file.ContentType, file.Name);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(File file)
    {
        var result = await _fileService.SaveAsync(file);

        return result.IsError ? BadRequest(result.Message) : Ok(result.Value);
    }
}
