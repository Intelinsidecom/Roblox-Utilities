using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace RobloxUtilities
{
    public class ConsoleArguments
    {
        public string Argument { get; set; }
        public string Value { get; set; }
    }
    public class Helper
    {
        static List<ConsoleArguments> list = new List<ConsoleArguments>();
        public static string[] AppArgs;
        public string FixPaths(string path)
        {
            if (path == null || path.Trim() == "")
                return path;

            return path.Trim()
                       .Trim('"')
                       .Trim('\0', '\r', '\n');
        }

        public string[] FixPathsInArrays(string[] paths)
        {
            if (paths == null)
                return null;

            for (int i = 0; i < paths.Length; i++)
            {
                if (paths[i] == null)
                    continue;

                paths[i] = paths[i]
                    .Trim()
                    .Trim('"')
                    .Trim('\0', '\r', '\n');
            }

            return paths;
        }

        public void ProcessArgs(string[] Args)
        {
            List<string> tempArgs = new List<string>();
            if (AppArgs == null)
            {
                for (int i = 0; i < Args.Length; i++)
                {
                    if (Contains(Args[i], "--", 0))
                    {
                        tempArgs.Add(Args[i]);
                    }
                }
                for (int i = 0; i < tempArgs.Count; i++)
                {
                    AppArgs[i] = tempArgs[i];
                }
                //ProcessArgumentValues(AppArgs);
            }
            /*foreach (string Argument in AppArgs)
            {
                Console.WriteLine(Argument);
            }*/
            ProcessArgumentValues(AppArgs);
        }

        public void ProcessArgumentValues(string[] Args)
        {
            /*foreach (string Argument in Args)
            {
            Console.WriteLine(Argument);
            }*/
            if (Contains(String.Concat(Args), "--", 0)) {
                for (int i = 0; i < Args.Length; i++)
                {
                    if (!Contains(Args[i], "--", 0))
                    {
                        list.Add(new ConsoleArguments
                        {
                            Argument = Args[i - 1],
                            Value = Args[i]
                        });
                    }
                }
            }
            /*foreach (ConsoleArguments entry in list)
            {
                Console.WriteLine("Argument: " + entry.Argument + " | Value: " + entry.Value);
            }*/

        }

        public string ReturnArgumentValue(string Arg)
        {
            foreach (ConsoleArguments entry in list)
            {
                if (entry.Argument == Arg)
                {
                    return entry.Value;
                }
            }

            return null;
        }

        public bool WereAnyArgsSpecified()
        {
            if (AppArgs == null || AppArgs.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool ArgsContain(string Arg)
        {
            if (AppArgs != null)
            {
                ProcessArgs(AppArgs);
            }
            if (AppArgs == null)
            {
                return false;
            }

            return Array.IndexOf(AppArgs, Arg) >= 0;
        }

        public bool Contains(String str, String substring,
                            StringComparison comp)
        {
            if (substring == null)
                throw new ArgumentNullException("substring",
                                             "substring cannot be null.");
            else if (!Enum.IsDefined(typeof(StringComparison), comp))
                throw new ArgumentException("comp is not a member of StringComparison",
                                         "comp");

            return str.IndexOf(substring, comp) >= 0;
        }

        public int CheckOccurences(string text, string character)
        {
            int count = 0;
            foreach (char c in text)
            {
                if (c.ToString() == character)
                {
                    count++;
                }
            }
            return count;
        }

        public string ProcessArg(string Text)
        {
            string ProcessText = Text;
            var charsToRemove = new string[] { "@", ",", ".", ";", "'", "=" };
            foreach (var c in charsToRemove)
            {
                ProcessText = ProcessText.Replace(c, string.Empty);
            }
            return ProcessText;
        }

        public string CheckArgValue(string Arg)
        {
            foreach (ConsoleArguments entry in list)
            {
                if (entry.Argument == Arg)
                {
                    return entry.Value;
                }
            }

            return null;
        }

        public bool CheckAllArgNames(string[] Args)
        {
            string[] ValidArgs = {"--SaveLocation", "--GenKeys", "--SignFile"};
            List<string> AllArgs = new List<string>(Args);
            foreach (string Arg in Args) {
                for (int i = 0; i < ValidArgs.Length; i++)
                {
                    if (Arg.Contains(ValidArgs[i]))
                    {
                        return true;
                    }
                }
                return false;
            }
            return false;
        }
        
    }
}
