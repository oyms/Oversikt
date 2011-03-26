namespace Skaar.Oversikt.Contracts.Configuration
{
    /// <summary>
    /// Defines a configuration item stored in a configuration store
    /// </summary>
    public interface IConfigurationDefinition
    {
        ConfigurationScope Scope { get; }
        ConfigurationType Type { get; }
        string Name { get; }
        string Description { get; }
    }
}