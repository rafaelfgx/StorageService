namespace StorageService;

public sealed record File(Guid Id, string Name, byte[] Bytes)
{
    public string ContentType
    {
        get
        {
            new FileExtensionContentTypeProvider().TryGetContentType(Name, out var contentType);

            return contentType;
        }
    }

    public static void Delete(string directory, Guid id)
    {
        var fileInfo = GetFileInfo(directory, id);

        if (fileInfo is null) return;

        System.IO.File.Delete(fileInfo.FullName);
    }

    public static async Task<File> GetAsync(string directory, Guid id)
    {
        var fileInfo = GetFileInfo(directory, id);

        if (fileInfo is null) return null;

        var bytes = await System.IO.File.ReadAllBytesAsync(fileInfo.FullName);

        return new File(id, fileInfo.Name, bytes);
    }

    public static FileInfo GetFileInfo(string directory, Guid id)
    {
        return new DirectoryInfo(directory).GetFiles(string.Concat(id, ".*"), SearchOption.AllDirectories).SingleOrDefault();
    }

    public async Task SaveAsync(string directory)
    {
        Directory.CreateDirectory(directory);

        var name = string.Concat(Id, Path.GetExtension(Name));

        var path = Path.Combine(directory, name);

        await System.IO.File.WriteAllBytesAsync(path, Bytes);
    }
}
