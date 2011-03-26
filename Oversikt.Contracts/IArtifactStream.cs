using System;
using System.IO;

namespace Skaar.Oversikt.Contracts
{
    /// <summary>
    /// An artifact transported to and from a repository
    /// </summary>
    public interface IArtifactStream:IDisposable
    {
        /// <summary>
        /// The content of the artifact
        /// </summary>
        Stream Stream { get; }
        /// <summary>
        /// The name or path to the artifact
        /// </summary>
        string Id { get; }
    }
}