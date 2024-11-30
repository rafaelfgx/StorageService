namespace StorageService;

public sealed class AppSettings
{
    public string Directory { get; set; }

    public IEnumerable<string> Extensions { get; set; } = new List<string>();
}
