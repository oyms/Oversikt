namespace Skaar.Oversikt.Contracts.Artifacts
{
    /// <summary>
    /// A link between two artifacts has an inherent direction; <see cref="In"/> or <see cref="Out"/>.
    /// </summary>
    public enum LinkDirection
    {
        /// <summary>
        /// This artifact links to another artifact
        /// </summary>
        Out=0,
        /// <summary>
        /// Another artifact links to this
        /// </summary>
        In=1
    }
}