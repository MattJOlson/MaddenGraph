namespace MaddenGraph.Domain.Builders
{
    public class ReceiverBuilder
    {
        private FormationBuilder _parent;
        private int? _ofs = null;

        public ReceiverBuilder(FormationBuilder parent)
        {
            _parent = parent;
        }

        public ReceiverBuilder(FormationBuilder parent, int ofs)
        {
            _parent = parent;
            _ofs = ofs;
        }

        public ReceiverBuilder At(int ofs)
        {
            return new ReceiverBuilder(_parent, ofs);
        }

        public FormationBuilder On()
        {
            return new FormationBuilder(_parent, _ofs);
        }

        public FormationBuilder Off()
        {
            return new FormationBuilder(_parent, _ofs);
        }
    }
}