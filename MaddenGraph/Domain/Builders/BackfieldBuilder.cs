namespace MaddenGraph.Domain.Builders
{
    public class BackfieldBuilder
    {
        private readonly FormationBuilder _parent;

        public BackfieldBuilder(FormationBuilder parent)
        {
            _parent = parent;
        }

        public FormationBuilder At(int x, int y)
        {
            return new FormationBuilder(_parent, x, y);
        }
    }
}