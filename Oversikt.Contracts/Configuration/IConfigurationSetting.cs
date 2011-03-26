namespace Skaar.Oversikt.Contracts.Configuration
{
    /// <summary>
    /// A configuration setting value
    /// </summary>
    public interface IConfigurationSetting
    {
        /// <summary>
        /// The kind of configuration. This determines what UI to expose when querying user for settings.
        /// </summary>
        ConfigurationType Type { get; }
    }
}