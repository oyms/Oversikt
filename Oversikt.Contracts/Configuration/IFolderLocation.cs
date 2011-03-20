namespace Skaar.Oversikt.Contracts.Configuration
{
    /// <summary>
    /// The path to a file system folder
    /// </summary>
    public interface IFolderLocation:IConfigurationType
    {
        string Path { get; }
    }
}