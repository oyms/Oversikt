using System.Diagnostics.Contracts;

namespace Skaar.Oversikt.Contracts.Configuration
{
    /// <summary>
    /// The scope of the configuration item.
    /// User or Shared, Application or Project
    /// </summary>
    public struct ConfigurationScope
    {
        private bool projectScope;
        private bool userScope;
       
        public static ConfigurationScope UserProject
        {
            get { return new ConfigurationScope {userScope = true, projectScope = true}; }
        }

        public static ConfigurationScope UserApplication
        {
            get { return new ConfigurationScope {userScope = true, projectScope = false}; }
        }

        public static ConfigurationScope SharedProject
        {
            get { return new ConfigurationScope {userScope = false, projectScope = true}; }
        }

        public static ConfigurationScope SharedApplication
        {
            get { return new ConfigurationScope {userScope = false, projectScope = false}; }
        }

        public bool IsUserScope
        {
            get { return userScope; }
        }

        public bool IsApplicationScope
        {
            get { return !IsProjectScope; }
        }

        public bool IsSharedScope
        {
            get { return !IsUserScope; }
        }

        public bool IsProjectScope
        {
            get { return projectScope; }
        }

        public override int GetHashCode()
        {
            return userScope.GetHashCode() + 32*projectScope.GetHashCode();
        }

        public static bool operator ==(ConfigurationScope a, ConfigurationScope b)
        {
            return a.userScope == b.userScope && a.projectScope == b.projectScope;
        }

        public static bool operator !=(ConfigurationScope a, ConfigurationScope b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            if (obj is ConfigurationScope)
            {
                return this == (ConfigurationScope) obj;
            }
            return false;
        }

        public override string ToString()
        {
            return string.Format("{0}/{1}", (userScope ? "User" : "Shared"), (projectScope ? "Project" : "Application"));
        }
    }
}