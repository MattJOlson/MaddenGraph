﻿using FluentAssertions;
using MaddenGraph.Domain;
using MaddenGraph.Domain.Builders;
using NUnit.Framework;

namespace MaddenGraph.Tests.Unit.Domain
{
    [TestFixture]
    class FormationTests
    {
        private Formation BuildDoubles()
        {
            return new FormationBuilder()
                .WithReceiver().At(-14).On()
                .WithReceiver().At(-10).Off()
                .WithReceiver().At(6).On()
                .WithReceiver().At(12).Off()
                .WithQuarterback().UnderCenter()
                .WithBack().At(-7, 0)
                .BuildFormation();
        }

        [Test]
        public void formations_are_created_with_eleven_player_positions()
        {
            BuildDoubles().Positions.Count.Should().Be(11);
        }

        [Test]
        public void default_formations_are_created_with_five_eligible_receivers()
        {
            BuildDoubles().EligibleReceivers.Count.Should().Be(5);
        }

        [Test]
        public void formation_construction_respects_per_side_receiver_params()
        {
            var formation = BuildDoubles();

            formation.WeakSideReceivers.Count.Should().Be(2);
            formation.StrongSideReceivers.Count.Should().Be(2);
            formation.BackfieldReceivers.Count.Should().Be(1);
        }

        [Test]
        public void formation_construction_allows_quads_formations_too()
        {
            var formation = new Formation(1,4);

            formation.WeakSideReceivers.Count.Should().Be(1);
            formation.StrongSideReceivers.Count.Should().Be(4);
            formation.BackfieldReceivers.Count.Should().Be(0);
        }

        [Test]
        public void all_eligible_receivers_in_a_formation_really_are_eligible()
        {
            var formation = new Formation(2,2);

            foreach (var p in formation.EligibleReceivers) {
                p.IsEligible.Should().BeTrue();
            }
        }
    }
}
