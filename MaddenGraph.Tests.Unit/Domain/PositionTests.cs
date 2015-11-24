using System.ComponentModel;
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
    }
}