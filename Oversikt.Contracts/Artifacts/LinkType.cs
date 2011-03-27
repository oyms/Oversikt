namespace Skaar.Oversikt.Contracts.Artifacts
{
    /// <summary>
    /// A link between two artifacts describes the relationship between the two.
    /// </summary>
    public enum LinkType
    {
        /// <summary>
        /// This artifact is a child to the other
        /// </summary>
        Child,
        /// <summary>
        /// This artifact is parent to the other
        /// </summary>
        Parent,
        /// <summary>
        /// This artifact is dependent on the other
        /// </summary>
        Dependency,
        /// <summary>
        /// This artifact is a result of the other artifact has been broken down into smaller parts
        /// </summary>
        BreakDown,
        /// <summary>
        /// The link is only to indicate an informal link.
        /// </summary>
        Information
    }
}