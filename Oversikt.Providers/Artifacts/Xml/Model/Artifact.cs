using System.Collections.Generic;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Artifacts.Xml.Model
{

    public class Artifact : IArtifact
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public List<IArtifactLink> Links{get;set;}
        IList<IArtifactLink> IArtifact.Links
        {
            get { return Links; }
        }
    }
}