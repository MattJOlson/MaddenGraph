using System;
using MaddenGraph.Util;

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
            if (tag < 0 || 4 < tag) {
                throw new ArgumentException($"Eligible receiver must have a tag between 0 and 4, found {tag}");
            }
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