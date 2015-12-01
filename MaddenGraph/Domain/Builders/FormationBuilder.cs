using System;
using System.Collections.Generic;
using System.Linq;
using MaddenGraph.Util;

namespace MaddenGraph.Domain.Builders
{
    public class FormationBuilder
    {
        private int _receivers = 0;
        private List<Pt> _weak = new List<Pt>();
        private List<Pt> _strong = new List<Pt>();
        private QbPos _qbPos = QbPos.Unspecified;
        private List<Pt> _line;

        public enum QbPos { Unspecified, UnderCenter, Shotgun }

        private int Players => 5 + _receivers + (_qbPos == QbPos.Unspecified ? 0 : 1);
        private List<Pt> _players => _line.Concat(_weak).Concat(_strong).ToList(); // FIXME: missing QB

        public FormationBuilder()
        {
            _line = Enumerable.Range(-2, 5)
                              .Select(x => x * 2) // offset along line
                              .Select(x => Pt.At(x, 0))
                              .ToList();
        }

        public ReceiverBuilder WithReceiver()
        {
            return new ReceiverBuilder(this);
        }

        public QuarterbackBuilder WithQuarterback()
        {
            return new QuarterbackBuilder(this);
        }

        public BackfieldBuilder WithBack()
        {
            return new BackfieldBuilder(this);
        }

        public Formation BuildFormation()
        {
            if (_qbPos == QbPos.Unspecified) {
                throw new FormationBuilderException("Formation must have a quarterback");
            }
            if (Players < 11) {
                throw new FormationBuilderException($"Formation must have 11 players ({Players} specified)");
            }

            return new Formation(_weak, _strong);
        }

        public FormationBuilder WithReceiverAt(Pt pos)
        {
            var clone = Clone();
            clone._receivers++;

            if(PlayerIsAlreadyOn(pos))
                throw new FormationBuilderException($"Specified player at {pos} intersects another player");

            if (pos.X < 0) {
                clone._weak.AddIfLegal(pos);
            } else {
                clone._strong.AddIfLegal(pos);
            }

            return clone;
        }

        private bool PlayerIsAlreadyOn(Pt pos)
        {
            return _players.Exists(p => p == pos);
        }

        public FormationBuilder WithQb(QbPos position)
        {
            var clone = Clone();
            clone._qbPos = position;
            return clone;
        }

        public FormationBuilder WithBackAt(int x, int y)
        {
            var clone = Clone();
            clone._receivers++;
            return clone;
        }

        private FormationBuilder Clone()
        {
            var clone = new FormationBuilder {
                _weak = _weak,
                _strong = _strong,
                _qbPos = _qbPos,
                _receivers = _receivers
            };
            return clone;
        }
    }
}