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

        [TestCase(-14, -12, true)]
        [TestCase(-12, 6, false)]
        [TestCase(12, 6, true)]
        public void formation_builder_objects_when_eligible_receiver_is_covered(int outside, int inside, bool expectCovered)
        {
            Action ctor = () => new FormationBuilder()
                .WithReceiver().At(outside).On()
                .WithReceiver().At(inside).On();

            if (expectCovered) {
                ctor.ShouldThrow<FormationBuilderException>()
                    .WithMessage($"Receiver at {inside} covered by receiver at {outside}");
            } else {
                ctor.ShouldNotThrow();
            }
        }

        // NB: This ASSumes that players are 1yd in diameter and are spaced at 1yd intervals
        // This also indirectly and kind of obscurely specifies the default position of the offensive line
        [TestCase(-6, false)]
        [TestCase(-4, true)]
        [TestCase(-2, true)]
        [TestCase(0, true)]
        [TestCase(2, true)]
        [TestCase(4, true)]
        [TestCase(6, false)]
        public void formation_builder_objects_when_a_receiver_sits_on_a_lineman(int offset, bool expectException)
        {
            Action ctor = () => new FormationBuilder()
                .WithReceiver().At(offset).On();

            if (expectException) {
                ctor.ShouldThrow<FormationBuilderException>()
                    .WithMessage($"Specified player at ({offset},0) intersects another player");
            } else {
                ctor.ShouldNotThrow();
            }
        }

        [Test]
        public void formation_builder_objects_when_two_receivers_intersect()
        {
            Action ctor = () => new FormationBuilder()
                .WithReceiver().At(-10).Off()
                .WithReceiver().At(-10).Off();

            ctor.ShouldThrow<FormationBuilderException>()
                .WithMessage("Specified player at (-10,-1) intersects another player");
        }

        [Test]
        public void formation_builder_objects_when_receiver_placed_within_tackle_box()
        {
            Action ctor = () => new FormationBuilder()
                .WithReceiver().At(-4).Off();

            ctor.ShouldThrow<FormationBuilderException>()
                .WithMessage("Receiver at (-4,-1) is within tackle box (define as a back)");
        }

        [Test]
        public void formation_builder_objects_when_backs_placed_outside_tackles()
        {
            Action ctor = () => new FormationBuilder()
                .WithBack().At(-5, -5);

            ctor.ShouldThrow<FormationBuilderException>()
                .WithMessage("Back at (-5, -5) is outside tackle box");
        }

        [TestCase(-1, false)]
        [TestCase(0, true)]
        [TestCase(1, true)]
        public void formation_builder_objects_when_backs_placed_on_or_ahead_of_line(int depth, bool expectThrow)
        {
            Action ctor = () => new FormationBuilder()
                .WithBack().At(0, depth);

            if (expectThrow) {
                ctor.ShouldThrow<FormationBuilderException>()
                    .WithMessage($"Back at (0, {depth}) is on or ahead of line");
            } else {
                ctor.ShouldNotThrow<FormationBuilderException>();
            }
        }

        // TODO:
        // * Backs sitting on each other, QB incl.

        private FormationBuilder BuilderWithReceivers(int r)
        {
            var builder = new FormationBuilder();
            foreach (var i in Enumerable.Range(0, r)) {
                var rbdr = builder.WithReceiver().At(i - 14);
                builder = i == 0 ? rbdr.On() : rbdr.Off();
            }
            return builder;
        }
    }
}