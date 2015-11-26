using System;
using FluentAssertions;
using MaddenGraph.Domain;
using MaddenGraph.Util;
using NUnit.Framework;

namespace MaddenGraph.Tests.Unit.Domain
{
    [TestFixture]
    public class PositionTests
    {
        [Test]
        public void positions_on_the_line_of_scrimmage_are_detected_correctly()
        {
            var pos = Position.Eligible(Pt.O, 0);
            pos.IsOnLine.Should().BeTrue();
        }

        [Test]
        public void positions_off_the_line_are_detected_correctly()
        {
            var pos = Position.Eligible(Pt.At(0, -1), 0);
            pos.IsOnLine.Should().BeFalse();
        }

        [TestCase(-1, "Eligible receiver must have a tag between 0 and 4, found -1")]
        [TestCase(0, null)]
        [TestCase(1, null)]
        [TestCase(2, null)]
        [TestCase(3, null)]
        [TestCase(4, null)]
        [TestCase(5, "Eligible receiver must have a tag between 0 and 4, found 5")] // XXX: Precludes six-receiver tackle-eligible plays
        public void eligible_receivers_must_have_tag_between_0_and_4(int tag, string msg)
        {
            Action ctor = () => Position.Eligible(Pt.O, tag);

            if (msg == null) {
                ctor.ShouldNotThrow();
            } else {
                ctor.ShouldThrow<ArgumentException>()
                    .WithMessage(msg);
            }
        }

        [TestCase(-1, true)]
        [TestCase(-5, false)]
        public void position_under_center_should_only_be_one_yard_deep(int depth, bool expected)
        {
            var pos = Position.Ineligible(Pt.At(0, depth));
            pos.IsUnderCenter().Should().Be(expected);
        }
    }
}