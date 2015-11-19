using System;

namespace MaddenGraph.Domain.Builders
{
    public class FormationBuilder
    {
        private int _receivers = 0;
        private int _weak = 0;
        private int _strong = 0;

        public FormationBuilder() { }

        // callback ctor: builders for eligible receivers at the line
        public FormationBuilder(FormationBuilder parent, int? ofs)
        {
            if(ofs == null) throw new ArgumentNullException("Didn't initialize receiver");

            _receivers = parent._receivers + 1;
            if (ofs < 0) {
                _weak = parent._weak + 1;
                _strong = parent._strong;
            } else {
                _weak = parent._weak;
                _strong = parent._strong + 1;
            }
        }

        // callback ctor: builders for noneligible receivers
        public FormationBuilder(FormationBuilder parent)
        {
            _receivers = parent._receivers;
            _weak = parent._weak;
            _strong = parent._strong;
        }

        // callback ctor: builders for backfield receivers
        public FormationBuilder(FormationBuilder parent, int x, int y)
        {
            _receivers = parent._receivers + 1;
            _weak = parent._weak;
            _strong = parent._strong;
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
            return new Formation(_weak, _strong);
        }
    }
}