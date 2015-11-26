using System;
using System.Collections.Generic;
using MaddenGraph.Util;

namespace MaddenGraph.Domain.Builders
{
    public static class FormationBuilderExtensions
    {
        public static void AddIfLegal(this List<Pt> receiversOnSide, Pt r)
        {
            receiversOnSide.Add(r);
            receiversOnSide.Sort((u, v) => Math.Abs(v.X) - Math.Abs(u.X));

            var onLine = receiversOnSide.FindAll(s => s.Y == 0);
            if (1 < onLine.Count) {
                var outside = onLine[0].X;
                var inside = onLine[1].X;
                throw new FormationBuilderException($"Receiver at {inside} covered by receiver at {outside}");
            }
        }
    }
}