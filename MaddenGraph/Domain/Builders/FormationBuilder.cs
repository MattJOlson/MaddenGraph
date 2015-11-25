using System;
using MaddenGraph.Util;

namespace MaddenGraph.Domain.Builders
{
    public class FormationBuilder
    {
        private int _receivers = 0;
        private int _weak = 0;
        private int _strong = 0;
        private QbPos _qbPos = QbPos.Unspecified;

        public enum QbPos { Unspecified, UnderCenter, Shotgun }

        private int Players => 5 + _receivers + (_qbPos == QbPos.Unspecified ? 0 : 1);

        public FormationBuilder() { }

        // callback ctor: builders for eligible receivers at the line
        public FormationBuilder(FormationBuilder parent, Pt ofs)
        {
            _receivers = parent._receivers + 1;
            if (ofs.X < 0) {
                _weak = parent._weak + 1;
                _strong = parent._strong;
                _qbPos = parent._qbPos;
            } else {
                _weak = parent._weak;
                _strong = parent._strong + 1;
                _qbPos = parent._qbPos;
            }
        }

        // callback ctor: builders for quarterbacks
        public FormationBuilder(FormationBuilder parent, QbPos position)
        {
            _receivers = parent._receivers;
            _weak = parent._weak;
            _strong = parent._strong;
            _qbPos = position;
        }

        // callback ctor: builders for backfield receivers
        public FormationBuilder(FormationBuilder parent, int x, int y)
        {
            _receivers = parent._receivers + 1;
            _weak = parent._weak;
            _strong = parent._strong;
            _qbPos = parent._qbPos;
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
    }

    public class FormationBuilderException : Exception
    {
        public FormationBuilderException(string msg) :base(msg) { }
    }
}