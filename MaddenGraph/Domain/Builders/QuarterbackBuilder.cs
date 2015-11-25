namespace MaddenGraph.Domain.Builders
{
    public class QuarterbackBuilder
    {
        private readonly FormationBuilder _parent;

        public QuarterbackBuilder(FormationBuilder parent)
        {
            _parent = parent;
        }

        public FormationBuilder UnderCenter()
        {
            return _parent.WithQb(FormationBuilder.QbPos.UnderCenter);
        }

        public FormationBuilder Shotgun()
        {
            return _parent.WithQb(FormationBuilder.QbPos.Shotgun);
        }
    }
}