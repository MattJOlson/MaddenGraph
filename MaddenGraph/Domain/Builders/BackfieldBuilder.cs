using System;
using MattUtils.Demands;

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
            Demand.That(Math.Abs(x) < 5)
                .OrThrow<FormationBuilderException>($"Back at ({x}, {y}) is outside tackle box");
            Demand.That(y < 0)
                .OrThrow<FormationBuilderException>($"Back at ({x}, {y}) is on or ahead of line");

            return _parent.WithBackAt(x, y);
        }
    }
}