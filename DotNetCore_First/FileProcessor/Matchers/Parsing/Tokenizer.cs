using System;
using System.Collections.Generic;
using System.IO;
using FileProcessor.Extensions;

namespace FileProcessor.Matchers.Parsing
{
    public class Tokenizer
    {
        public IEnumerable<IToken> Apply(string criteria)
        {
            string buffer = string.Empty;
            bool inBrace = false;
            foreach (var c in criteria)
            {
                switch (c)
                {
                    case char _ when c == Path.DirectorySeparatorChar:
                        if (buffer == "**")
                        {
                            yield return new MultiDirectoryMatch();
                            buffer = string.Empty;
                        }
                        else if (inBrace) buffer += c;
                        else
                        {
                            if (buffer.Length > 0)
                                yield return new StringLiteral(buffer);
                            buffer = string.Empty;
                            yield return new PathSeparator();
                        }
                        break;
                    case '{':
                        if(buffer.Length>0)
                            yield return new StringLiteral(buffer);
                        buffer = string.Empty;
                        inBrace = true;
                        buffer += c;
                        break;
                    case '}':
                        buffer += c;
                        yield return ParseParameter(buffer);
                        buffer = string.Empty;
                        inBrace = false;
                        break;
                    default:
                        buffer += c;
                        break;
                }
            }
            if(buffer=="**")
                yield return new MultiDirectoryMatch();
            else if (buffer.Length>0)
                yield return new StringLiteral(buffer);
        }
        private IToken ParseParameter(string text)
        {
            text = text.Replace("{", "").Replace("}", "");
            if (!text.Contains(":"))
                return new Parameter(text);
            var nameAndOp = text.SubstringBefore("(").Split(":");
            var args = text.SubstringAfter("(");
            args = args.Substring(0, args.Length - 1);
            return new ParameterConstraint(nameAndOp[0],nameAndOp[1],args);
        }
    }
}
