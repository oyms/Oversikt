using System.Collections.Generic;

namespace Skaar.Oversikt.Contracts.Artifacts
{
    /// <summary>
    /// The result returned from a serialization
    /// </summary>
    public interface IDeserializationResult
    {
        /// <summary>
        /// The <see cref="IArtifactStream.Id"/> value from the stream attempted to deserialize.
        /// </summary>
        string Id { get; }
        /// <summary>
        /// If <c>true</c>, the <see cref="Result"/> may safely be used.
        /// </summary>
        bool Success { get; }
        /// <summary>
        /// The parsed artifact
        /// </summary>
        IArtifact Result { get; }
        /// <summary>
        /// If <see cref="Success"/> is <c>false</c>, this collection may contain warning messages.
        /// </summary>
        IEnumerable<string> Warnings { get; }
    }
}