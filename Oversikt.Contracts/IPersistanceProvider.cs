using System.IO;

namespace Skaar.Oversikt.Contracts
{
    /// <summary>
    /// This interface represents a provider with the ability to fetch and store streams to a peristant store (i.e. file system or database)
    /// </summary>
    public interface IPersistanceProvider
    {
        /// <summary>
        /// Fetches a stream from the peristent store based on the <paramref name="id"/>.
        /// </summary>
        Stream Get(string id);
    }
}