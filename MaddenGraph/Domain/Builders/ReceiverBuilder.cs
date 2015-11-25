using MaddenGraph.Util;

namespace MaddenGraph.Domain.Builders
{
    public class ReceiverBuilder
    {
        private FormationBuilder _parent;
        private Pt _ofs;

        public ReceiverBuilder(FormationBuilder parent)
        {
            _parent = parent;
        }

        public ReceiverBuilder(FormationBuilder parent, Pt ofs)
        {
            _parent = parent;
            _ofs = ofs;
        }

        public ReceiverBuilder At(int ofs)
        {
            return new ReceiverBuilder(_parent, Pt.At(ofs, 0));
        }

        public FormationBuilder On()
        {
            return _parent.WithReceiverAt(Pt.At(_ofs.X, 0));
        }

        public FormationBuilder Off()
        {
            return _parent.WithReceiverAt(Pt.At(_ofs.X, -1));
        }
    }
}