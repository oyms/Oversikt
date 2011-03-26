namespace Skaar.Oversikt.Contracts.Artifacts
{
    /// <summary>
    /// Provides functionality to parse a stream containing an artifact, and return an artifact, and to serialize it back into a stream.
    /// </summary>
    public interface IArtifactSerializer
    {
        IDeserializationResult Deserialize(IArtifactStream stream);
    }
}