using MaddenGraph.Domain;
using MaddenGraph.Domain.Builders;

namespace MaddenGraph.Tests.Unit.Domain
{
    public static class Formations {
        public static Formation Doubles()
        {
            return new FormationBuilder()
                .WithReceiver().At(-14).On()
                .WithReceiver().At(-10).Off()
                .WithReceiver().At(6).On()
                .WithReceiver().At(12).Off()
                .WithQuarterback().UnderCenter()
                .WithBack().At(0, -7)
                .BuildFormation();
        }

        public static Formation GunEmptyQuadsStrong()
        {
            return new FormationBuilder()
                .WithReceiver().At(-14).On()
                .WithReceiver().At(6).On()
                .WithReceiver().At(8).Off()
                .WithReceiver().At(10).Off()
                .WithReceiver().At(14).Off()
                .WithQuarterback().Shotgun()
                .BuildFormation();
        }
    }
}