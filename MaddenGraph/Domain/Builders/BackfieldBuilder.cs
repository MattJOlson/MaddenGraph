using System;

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
            if (4 < Math.Abs(x)) {
                throw new FormationBuilderException($"Back at ({x}, {y}) is outside tackle box");
            }

            if (-1 < y) {
                throw new FormationBuilderException($"Back at ({x}, {y}) is on or ahead of line");
            }

            return _parent.WithBackAt(x, y);
        }
    }
}