namespace BlogBuilder
{
    public enum ExitCode
    {
        Success = 0,
        InvalidParameters = 1,
        ShowHelp = 2,
        DirectoryDoesNotExist = 3,
        UnknownError = 100
    }
}