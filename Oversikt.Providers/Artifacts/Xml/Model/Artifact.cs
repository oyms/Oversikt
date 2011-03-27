using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Artifacts.Xml.Model
{
    [DataContract(Namespace = "http://schemas.skaar/oversikt")]
    public class Artifact : IArtifact
    {
        [DataMember(IsRequired = true,Order = 0)]
        public string Id { get; set; }

        [DataMember(IsRequired = true,Order = 1)]
        public string Title { get; set; }

        #region Description

        [DataMember(IsRequired = false,EmitDefaultValue = false,Order = 2)]
        public XmlDocument DescriptionContent { get; set; }

        string IArtifact.Description
        {
            get { return DescriptionContent.InnerXml; }
            set
            {
                DescriptionContent = new XmlDocument();
                DescriptionContent.LoadXml(value);
            }
        }

        #endregion

        [DataMember(EmitDefaultValue = false,IsRequired = false,Order = 3)]
        public ArtifactType Type { get; set; }
        #region Links
        [DataMember(IsRequired = false,EmitDefaultValue = false,Order = 4)]
        public List<ArtifactLink> Links{get;set;}
        IList<IArtifactLink> IArtifact.Links
        {
            get { return Links.Cast<IArtifactLink>().ToList(); }
        }
        #endregion

    }
}