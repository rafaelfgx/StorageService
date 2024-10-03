namespace StorageService;

public sealed class FileValidator : AbstractValidator<File>
{
    public FileValidator()
    {
        RuleFor(file => file.Name).Must(Name);
        RuleFor(file => file.Bytes).NotEmpty();
    }

    private static bool Name(string name)
    {
        return Path.HasExtension(name) && name.IndexOfAny(Path.GetInvalidFileNameChars()) < 0 && !name.StartsWith('.');
    }
}
