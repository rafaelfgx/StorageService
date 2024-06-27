namespace StorageService;

public sealed class FileService : IFileService
{
    private readonly AppSettings _appSettings;

    public FileService(AppSettings appSettings) => _appSettings = appSettings;

    public void Delete(Guid id) => File.Delete(_appSettings.Directory, id);

    public Task<File> GetAsync(Guid id) => File.GetAsync(_appSettings.Directory, id);

    public async Task<Result<Guid>> SaveAsync(File file)
    {
        var validation = await ValidateAsync(file);

        if (validation.IsError) return Result<Guid>.Error(validation.Message);

        await file.SaveAsync(_appSettings.Directory);

        return Result<Guid>.Success(file.Id);
    }

    private static Result GetResult(ValidationResult result) => result.IsValid ? Result.Success() : Result.Error(result.ToString());

    private static async Task<Result> ValidateAsync(File file) => GetResult(await new FileValidator().ValidateAsync(file));
}
