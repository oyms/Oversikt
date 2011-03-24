using NUnit.Framework;
using Scope = Skaar.Oversikt.Contracts.Configuration.ConfigurationScope;

namespace Oversikt.Tests.Configuration
{
    public class ConfigurationScopeTests
    {
        [Test]
        public void SharedProjectFactoryMethod_HasCorrectState()
        {
            var target = Scope.SharedProject;
            Assert.That(target.IsSharedScope,Is.True);
            Assert.That(target.IsUserScope,Is.False);
            Assert.That(target.IsApplicationScope,Is.False);
            Assert.That(target.IsProjectScope,Is.True);
        }
        [Test]
        public void UserProjectFactoryMethod_HasCorrectState()
        {
            var target = Scope.UserProject;
            Assert.That(target.IsSharedScope, Is.False);
            Assert.That(target.IsUserScope, Is.True);
            Assert.That(target.IsApplicationScope, Is.False);
            Assert.That(target.IsProjectScope, Is.True);
        }
        [Test]
        public void SharedApplicationFactoryMethod_HasCorrectState()
        {
            var target = Scope.SharedApplication;
            Assert.That(target.IsSharedScope, Is.True);
            Assert.That(target.IsUserScope, Is.False);
            Assert.That(target.IsApplicationScope, Is.True);
            Assert.That(target.IsProjectScope, Is.False);
        }
        [Test]
        public void UserApplicationFactoryMethod_HasCorrectState()
        {
            var target = Scope.UserApplication;
            Assert.That(target.IsSharedScope, Is.False);
            Assert.That(target.IsUserScope, Is.True);
            Assert.That(target.IsApplicationScope, Is.True);
            Assert.That(target.IsProjectScope, Is.False);
        }

        [Test]
        public void ToString_SharedProject_ProperString()
        {
            //Arrange
            var expected = "Shared/Project";
            var target = Scope.SharedProject;
            //Act
            var result = target.ToString();
            //Assert
            Assert.That(result,Is.EqualTo(expected));
        }
        [Test]
        public void ToString_SharedApplication_ProperString()
        {
            //Arrange
            var expected = "Shared/Application";
            var target = Scope.SharedApplication;
            //Act
            var result = target.ToString();
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void ToString_UserProject_ProperString()
        {
            //Arrange
            var expected = "User/Project";
            var target = Scope.UserProject;
            //Act
            var result = target.ToString();
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }
        [Test]
        public void ToString_UserApplication_ProperString()
        {
            //Arrange
            var expected = "User/Application";
            var target = Scope.UserApplication;
            //Act
            var result = target.ToString();
            //Assert
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void EqualsOperator_DifferentConstructors_False()
        {
            var a = Scope.SharedApplication;
            bool result=false;
            result |= a == Scope.SharedProject;
            result |= a == Scope.UserApplication;
            result |= a == Scope.UserProject;

            Assert.That(result,Is.False);
        }
        [Test]
        public void Equals_DifferentConstructors_False()
        {
            var a = Scope.SharedApplication;
            bool result = false;
            result |= a.Equals(Scope.SharedProject);
            result |= a.Equals(Scope.UserApplication);
            result |= a.Equals(Scope.UserProject);

            Assert.That(result, Is.False);
        }

        [Test]
        public void EqualsOperator_SameConstructors_True()
        {
            bool result = true;
            // ReSharper disable EqualExpressionComparison
            result &= Scope.SharedApplication == Scope.SharedApplication;
            result &= Scope.SharedProject == Scope.SharedProject;
            result &= Scope.UserApplication == Scope.UserApplication;
            result &= Scope.UserProject == Scope.UserProject;
            // ReSharper restore EqualExpressionComparison
            Assert.That(result,Is.True);
        }
    }
}
