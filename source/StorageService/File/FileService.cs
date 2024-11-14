namespace StorageService;

public sealed class FileService(AppSettings appSettings) : IFileService
{
    public void Delete(Guid id) => File.Delete(appSettings.Directory, id);

    public Task<File> GetAsync(Guid id) => File.GetAsync(appSettings.Directory, id);

    public async Task<Result<Guid>> SaveAsync(File file)
    {
        var validation = await ValidateAsync(file);

        if (validation.IsError) return Result<Guid>.Error(validation.Message);

        await file.SaveAsync(appSettings.Directory);

        return Result<Guid>.Success(file.Id);
    }

    private static Result GetResult(ValidationResult result) => result.IsValid ? Result.Success() : Result.Error(result.ToString());

    private static async Task<Result> ValidateAsync(File file) => GetResult(await new FileValidator().ValidateAsync(file));
}
