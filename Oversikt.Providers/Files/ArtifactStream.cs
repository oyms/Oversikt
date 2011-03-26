using System.IO;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Files
{
    class ArtifactStream:IArtifactStream
    {
        public ArtifactStream(Stream stream,string id)
        {
            Stream = stream;
            Id = id;
        }

        public void Dispose()
        {
            if(Stream!=null)Stream.Dispose();
        }

        public Stream Stream { get; private set; }

        public string Id { get; private set; }
    }
}