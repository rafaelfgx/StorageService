namespace StorageService;

public interface IFileService
{
    void Delete(Guid id);

    Task<File> GetAsync(Guid id);

    Task<Result<Guid>> SaveAsync(File file);
}
