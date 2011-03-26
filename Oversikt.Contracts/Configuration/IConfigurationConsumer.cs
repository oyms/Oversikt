using System.Collections.Generic;

namespace Skaar.Oversikt.Contracts.Configuration
{
    /// <summary>
    /// Used on providers requiring configuration to be queried from user or system.
    /// </summary>
    public interface IConfigurationConsumer
    {
        IEnumerable<IConfigurationDefinition> RequiresConfiguration();
    }
}