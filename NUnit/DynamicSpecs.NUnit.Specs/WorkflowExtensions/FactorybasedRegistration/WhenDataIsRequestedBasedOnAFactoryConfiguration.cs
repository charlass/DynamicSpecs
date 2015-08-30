﻿namespace DynamicSpecs.NUnit.Specs.WorkflowExtensions.FactorybasedRegistration
{
    using FluentAssertions;

    using global::NUnit.Framework;

    public class WhenDataIsRequestedBasedOnAFactoryConfiguration : Specifies<object>, IRequestData
    {
        [Test]
        public void Then_the_information_is_provided()
        {
            this.Data.ShouldBeEquivalentTo(DataProvider.Data);
        }

        public int Data { get; set; }
    }
}