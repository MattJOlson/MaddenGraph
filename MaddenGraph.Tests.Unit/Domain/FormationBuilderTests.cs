using System;
using System.Data.SqlClient;
using System.Linq;
using FluentAssertions;
using MaddenGraph.Domain.Builders;
using MaddenGraph.Util;
using NUnit.Framework;

namespace MaddenGraph.Tests.Unit.Domain
{
    [TestFixture]
    public class FormationBuilderTests
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void formation_builder_objects_when_asked_to_build_formation_with_too_few_receivers(int r)
        {
            var builder = BuilderWithReceivers(r).WithQuarterback().UnderCenter();
            Action ctor = () => builder.BuildFormation();

            ctor.ShouldThrow<FormationBuilderException>()
                .WithMessage($"Formation must have 11 players ({6+r} specified)");
        }

        [Test]
        public void formation_builder_objects_when_asked_to_build_formation_with_no_quarterback()
        {
            var builder = BuilderWithReceivers(5);
            Action ctor = () => builder.BuildFormation();

            ctor.ShouldThrow<FormationBuilderException>()
                .WithMessage("Formation must have a quarterback");
        }

        [Test]
        [Ignore("Not ready yet")]
        public void formation_builder_objects_when_eligible_receiver_is_covered()
        {
            Action ctor = () => new FormationBuilder()
                .WithReceiver().At(-14).On()
                .WithReceiver().At(-12).On();

            ctor.ShouldThrow<FormationBuilderException>()
                .WithMessage("Receiver at -12 covered by receiver at -14");
        }

        private FormationBuilder BuilderWithReceivers(int r)
        {
            var builder = new FormationBuilder();
            foreach (var i in Enumerable.Range(0, r)) {
                var rbdr = builder.WithReceiver().At(r - 14);
                builder = r == 0 ? rbdr.On() : rbdr.Off();
            }
            return builder;
        }
    }
}