using System;
using System.Xml.Linq;

namespace Oversikt.Tests.Builders
{
    public class ArtifactXmlBuilder
    {
        public ArtifactXmlBuilder()
        {
            this.WithId(Guid.NewGuid().ToString()).WithTitle(Guid.NewGuid().ToString());
        }
        private XElement id;
        private XElement title;
        private readonly XNamespace ns = XNamespace.Get("http://schemas.skaar/oversikt");
        public override string ToString()
        {
            var root = new XElement(ns + "Artifact");
            if(id!=null)root.Add(id);
            if(title!=null)root.Add(title);
            return root.ToString(SaveOptions.OmitDuplicateNamespaces);
        }
        public ArtifactXmlBuilder WithId(string value)
        {
            id=new XElement(ns+"Id",value);
            return this;
        }
        public ArtifactXmlBuilder WithTitle(string value)
        {
            title=new XElement(ns + "Title", value);
            return this;   
        }
    }
}