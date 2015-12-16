using System;
using MaddenGraph.Util;
using MattUtils.Demands;

namespace MaddenGraph.Domain
{
    public class Position
    {
        private Position(Pt pos, int tag)
        {
            Pos = pos;
            Tag = tag;
        }

        public static Position Eligible(Pt pos, int tag) // TODO: Is tag really an int?
        {
            Demand.That(0 <= tag && tag <= 4)
                .OrThrow<ArgumentException>($"Eligible receiver must have a tag between 0 and 4, found {tag}");
            return new Position(pos, tag); 
        }

        public static Position Ineligible(Pt pos)
        {
            return new Position(pos, -1);
        }

        public Pt Pos { get; }
        public int Tag { get; }
        public bool IsEligible => 0 <= Tag;
        public bool IsOnLine => Pos.Y == 0;

        public bool IsUnderCenter()
        {
            return Pos.Y == -1;
        }
    }
}