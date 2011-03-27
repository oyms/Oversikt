using System.Collections.Generic;

namespace Skaar.Oversikt.Contracts.Artifacts
{
	/// <summary>
	/// Represents an artifact of any type 
	/// </summary>
	public interface IArtifact
	{
	    string Title {get;set;}
	    string Description {get;set;}
        ArtifactType Type { get; set; }
	    IList<IArtifactLink> Links {get;}
	    string Id {get;}
	}
}

