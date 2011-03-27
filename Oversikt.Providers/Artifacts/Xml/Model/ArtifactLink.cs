using System.Runtime.Serialization;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Artifacts.Xml.Model
{
    [DataContract(Namespace = "http://schemas.skaar/oversikt")]
    public class ArtifactLink:IArtifactLink
    {
        [DataMember(EmitDefaultValue = false,IsRequired = false,Order = 0)]
        public LinkDirection Direction { get; set; }
        [DataMember(IsRequired = true, Order = 1)]
        public LinkType Type { get; set; }
    }
}