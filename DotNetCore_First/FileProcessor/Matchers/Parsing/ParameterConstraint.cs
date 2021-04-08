using System;
using System.Collections.Generic;
using System.Text;

namespace FileProcessor.Matchers.Parsing
{
    public class ParameterConstraint:IToken
    {
        public string Name { get; }
        public string Op { get; }
        public string Args { get; }

        public ParameterConstraint(string name, string op, string args)
        {
            Name = name;
            Op = op;
            Args = args;
        }
    }
}
