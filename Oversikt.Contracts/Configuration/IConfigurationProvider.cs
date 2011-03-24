namespace Skaar.Oversikt.Contracts.Configuration
{
    /// <summary>
    /// Contains access to configuration settings
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Fetches a configuration setting if it exists
        /// </summary>
        /// <typeparam name="T">The type of configuration value</typeparam>
        /// <param name="key">The key the value is stored by</param>
        /// <param name="scope">The scope of the configuration</param>
        /// <returns>May retun <c>null</c> if no configuration exists.</returns>
        T Get<T>(string key, ConfigurationScope scope) where T:IConfigurationSetting;
    }
}