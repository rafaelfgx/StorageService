namespace StorageService;

[Route("files")]
[ApiController]
public class FileController(IFileService fileService) : ControllerBase
{
    [HttpDelete("{id}")]
    public void Delete(Guid id) => fileService.Delete(id);

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAsync(Guid id)
    {
        var file = await fileService.GetAsync(id);

        return file is null ? NotFound() : File(file.Bytes, file.ContentType, file.Name);
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(File file)
    {
        var result = await fileService.SaveAsync(file);

        return result.IsError ? BadRequest(result.Message) : Ok(result.Value);
    }
}
