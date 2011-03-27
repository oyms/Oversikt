namespace Skaar.Oversikt.Contracts.Artifacts
{
    /// <summary>
    /// The kind of artifact
    /// </summary>
    public enum ArtifactType
    {
        /// <summary>
        /// The artifact represents a project. Typically this is set on the root artifact in a project.
        /// </summary>
        Project,
        /// <summary>
        /// The artifact represents a product. This may be used on some of the root artifacts in a project.
        /// </summary>
        Product,
        /// <summary>
        /// The artifact represents a work item
        /// </summary>
        WorkItem,
        /// <summary>
        /// The artifact represents a task.
        /// </summary>
        Task
    }
}