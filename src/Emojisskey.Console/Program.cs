using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace Emojisskey.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            int Success(string message)
            {
                ForegroundColor = ConsoleColor.Green;
                Error.WriteLine(message);
                return 0;
            }

            int Fail(string message)
            {
                ForegroundColor = ConsoleColor.Red;
                Error.WriteLine(message);
                return 1;
            }

            int NoEnoughArguments() =>
                Fail("No enough arguments.");

            bool TryMapArguments(out IDictionary<string, string> result, params string[] requiredKeys)
            {
                result = new Dictionary<string, string>
                {
                    { "name", "" }, // TODO
                    { "output", "{name}_{index:d3}" }
                };
                return requiredKeys.All(result.ContainsKey);
            }

            if (args.Length < 1)
                return NoEnoughArguments();

            switch (args[0].ToLowerInvariant())
            {
                case "conv":
                case "convert":
                case "create":
                case "gen":
                case "generate":
                case "new":
                {
                    if (TryMapArguments(out var map))
                        return NoEnoughArguments();

                    var outputTemplate = map["output"]
                        .Replace("\0", "")
                        .Replace("{{", "\0")
                        .Replace("{name", "{0")
                        .Replace("{index", "{1")
                        .Replace("\0", "{{");
                    string FileName(int index) =>
                        string.Format(outputTemplate, map["name"], index);

                    return Success("Generation success.");
                }
                default:
                {
                    return Fail("Unknown command.");
                }
            }
        }
    }
}
