using System.Collections.Generic;
using Skaar.Oversikt.Contracts.Artifacts;

namespace Oversikt.Providers.Artifacts
{
    class DeSerializationResult:IDeserializationResult
    {
        private readonly string id;

        private readonly bool success;

        private readonly IArtifact result;

        private readonly IEnumerable<string> warnings;

        public DeSerializationResult(string id, bool success, IArtifact result, IEnumerable<string> warnings)
        {
            this.id = id;
            this.warnings = warnings;
            this.result = result;
            this.success = success;
        }

        string IDeserializationResult.Id
        {
            get { return id; }
        }

        bool IDeserializationResult.Success
        {
            get { return success; }
        }

        IArtifact IDeserializationResult.Result
        {
            get { return result; }
        }

        IEnumerable<string> IDeserializationResult.Warnings
        {
            get { return warnings; }
        }
    }
}