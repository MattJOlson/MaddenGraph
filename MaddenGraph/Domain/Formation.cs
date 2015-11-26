﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using MaddenGraph.Util;

namespace MaddenGraph.Domain
{
    public class Formation
    {
        public Formation(List<Pt> weak, List<Pt> strong)
        {
            var backfield = 5 - (strong.Count + weak.Count);

            Func<int, Position> makeEligible = i => Position.Eligible(Pt.O, i);
            Func<int, Position> makeIneligible = i => Position.Ineligible(Pt.O);
            Func<Position, Position> moveOffLine = p => Position.Eligible(Pt.At(0, -1), p.Tag);

            StrongSideReceivers = strong.Select((pt, i) => Position.Eligible(pt, i)).ToList();
            WeakSideReceivers = weak.Select((pt, i) => Position.Eligible(pt, i)).ToList();
            BackfieldReceivers = Enumerable.Range(strong.Count + weak.Count, backfield)
                .Select(makeEligible)
                .Select(moveOffLine)
                .ToList();
            EveryoneElse = Enumerable.Range(5, 6).Select(makeIneligible).ToList();
        }

        public List<Position> Positions => EligibleReceivers.Concat(EveryoneElse).ToList();
        public List<Position> EligibleReceivers => WeakSideReceivers.Concat(StrongSideReceivers).Concat(BackfieldReceivers).ToList();

        public List<Position> StrongSideReceivers { get; }
        public List<Position> WeakSideReceivers { get; }
        public List<Position> BackfieldReceivers { get; }
        public List<Position> EveryoneElse { get; } 
    }
}