using System;
using System.Collections.Generic;
using MaddenGraph.Util;
using MattUtils.Demands;

namespace MaddenGraph.Domain.Builders
{
    public static class FormationBuilderExtensions
    {
        public static void AddIfLegal(this List<Pt> receiversOnSide, Pt r)
        {
            receiversOnSide.Add(r);
            receiversOnSide.Sort((u, v) => Math.Abs(v.X) - Math.Abs(u.X));

            var onLine = receiversOnSide.FindAll(s => s.Y == 0);

            // TODO: It would be nice to be able to write something like this instead
//            Demand.That(onLine.Count < 2)
//                .OrThrow<FormationBuilderException>($"Receiver at {onLine[0].X} covered by receiver at {onLine[1].X}");
            if (1 < onLine.Count) {
                var outside = onLine[0].X;
                var inside = onLine[1].X;
                throw new FormationBuilderException($"Receiver at {inside} covered by receiver at {outside}");
            }
        }
    }
}