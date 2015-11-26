using System;

namespace MaddenGraph.Domain.Builders
{
    public class FormationBuilderException : Exception
    {
        public FormationBuilderException(string msg) :base(msg) { }
    }
}