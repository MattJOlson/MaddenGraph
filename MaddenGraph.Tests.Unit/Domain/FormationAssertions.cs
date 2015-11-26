using MaddenGraph.Domain;
using NUnit.Framework;

namespace MaddenGraph.Tests.Unit.Domain
{
    public static class FormationAssertions
    {
        public static FormationQbAsserter ShouldHaveQb(this Formation f)
        {
            return new FormationQbAsserter(f);
        }
    }

    public class FormationQbAsserter
    {
        private readonly Formation _f;

        public FormationQbAsserter(Formation f)
        {
            _f = f;
        }

        public FormationReceiverAsserter UnderCenter()
        {
            Assert.That(_f.Qb.IsUnderCenter(), Is.True, $"Expected QB under center but is {_f.Qb.Pos.X} yards back");
            return new FormationReceiverAsserter();
        }
    }

    public class FormationReceiverAsserter { }
}