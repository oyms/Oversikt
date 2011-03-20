using System;
using System.Collections.Generic;

namespace Skaar.Oversikt
{
	/// <summary>
	/// Represents an artifact of any type 
	/// </summary>
	public interface IArtifact
	{
		string Id {get;}
		string Title {get;set;}
		string Description {get;set;}
		IList<IArtifactLink> Links {get;}
	}
}

