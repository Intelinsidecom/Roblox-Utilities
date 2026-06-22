using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RobloxUtilities;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu menu = new ConsoleApp.Menu();
            Helper helper = new Helper();
            Helper.AppArgs = args;
            helper.ProcessArgs(args);
            /*for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine(args[i]);
            }*/

            if (helper.WereAnyArgsSpecified() == false || !helper.CheckAllArgNames(args) && !helper.Contains(string.Join("", args), "", 0))
            {
                menu.MainMenu();
            }
            else
            {
                PrintHelp();
            }

            if (helper.ArgsContain("--GenKeys") && !helper.ArgsContain("--SignFile") && !helper.ArgsContain("--help") && !helper.ArgsContain("-help"))
            {
                SignatureGen KeyGen = new SignatureGen();
                KeyGen.SignatureGenerationWithNoUserInput();
            }
            if (helper.ArgsContain("--SignFile") && !helper.ArgsContain("--help") && !helper.ArgsContain("-help"))
            {
                SignerWithSignature Sig = new ConsoleApp.SignerWithSignature();
                Sig.HandleSigningProcessNoInput();
            }

            if (helper.ArgsContain("--help") || helper.ArgsContain("-help"))
            {
                PrintHelp();
            }
        } // static void Main(string[] args)

        static void PrintHelp()
        {
            Console.WriteLine("Roblox Utilities, Simple C# utility which combines all utilities someone could need when messing with local server hosting or something else.");
            Console.WriteLine("This App supports interactive mode which is recommended to be used. Command line arguments are recommended for automatization.");
            Console.WriteLine("");
            Console.WriteLine("List of arguments:");
            Console.WriteLine("--GenKeys - Generates Signature Keys which are used by Roblox Clients to authenticate scripts from the website.\nGenerates Public, Private blobs, Public one going into the client and private one\n should be used to sign the scripts inside your webserver.\nPrivate Key is Private Blob but just converted to RSA, ANKS... (Finish this later).");
            Console.WriteLine("--GenKeys needs --SaveLocation argument to decide where to save the generated keys. if no is provided, it saves to where the executable is. if you want to print the results, pass the --PrintToConsole argument");
            Console.WriteLine("--SignFile - Signs a selected file or files to a specific locations using the PrivateBlob. it appends the rbxsig signature for the user too.");
            Console.WriteLine("--SignFile requires --PrivateBlobLocation to find the generated Private Blob. --File to know which file to sign. --Signature to decide which signature type to use. --SaveLocation is only used to specify where to save.");
            Console.WriteLine("");
            Console.WriteLine("Note that all arguments must start with -- and their values must not contain -- mentions in order to not confuse the parser.");
        }
    } // class Program
} // namespace UtilityConsole
