using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using RobloxUtilities;

namespace ConsoleApp
{
    class SignatureGen
    {
        Menu menu = new ConsoleApp.Menu();
        Helper helper = new Helper();
        public RobloxUtilities.KeyGen genkey = new RobloxUtilities.KeyGen();
        public void SignatureGenerationWithUserInput()
        {
                RobloxUtilities.KeyGen genkey = new RobloxUtilities.KeyGen();
                Console.Clear();
                // i dont even understand how the magic in signer.cs works at all. 
                genkey.GenerateAResult("Public", "Here is your Public Blob Key which needs to be replaced in the client:", true);
                genkey.GenerateAResult("PrivateBlob", "Here is your Private Blob Key which is the private key that gets used to validate public key (i have no idea on specifics)", true);
                genkey.GenerateAResult("PrivateKey", "Here is your Private Key which gets encoded with RSA and... no idea.", true);

                Console.WriteLine("Would you like to get the Results saved to a files too? Y / no for any other Key");
                string keyInfo2 = Console.ReadKey(true).Key.ToString();
                Console.WriteLine("");
                if (keyInfo2 == "Y" || keyInfo2 == "y")
                {
                    try
                    {
                        Console.WriteLine("type in the location where you will want to save the result files and press enter.");
                        Console.WriteLine("if no path is specified, it will default to executable location");
                        string path = Console.ReadLine();
                        path = helper.FixPaths(path);
                        File.WriteAllText(path + "\\" + "PublicKeyBlob.txt", genkey.WriteAResult("Public", "Here is your Public Blob Key which needs to be replaced in the client:"));
                        File.WriteAllText(path + "\\" + "PrivateKeyBlob.txt", genkey.WriteAResult("PrivateBlob", "Here is your Private Blob Key which should be used to.. I dont know for what exactly lol."));
                        File.WriteAllText(path + "\\" + "PrivateKey.pem", genkey.WriteAResult("PrivateKey", "Here is your Private Key which should be used to sign the data response in the webserver."));
                    }
                    catch (Exception Error)
                    {
                        Console.WriteLine(Error);
                        Console.WriteLine("Saving Failed. Press Any Key to go back to Main Menu");
                        Console.ReadKey();
                        menu.MainMenu();
                    }
                    Console.WriteLine("Saving Succeeded. Check the files out where the Executable is placed in. Press Any Key to go back to Main Menu");
                    Console.ReadKey();
                    menu.MainMenu();
                } else
                {
                    menu.MainMenu();
                }
        }

        public void SignatureGenerationWithNoUserInput()
        {
            if (helper.ArgsContain("--PrintToConsole"))
            {

                genkey.GenerateAResult("Public", "Here is your Public Blob Key which needs to be replaced in the client:", true);
                genkey.GenerateAResult("PrivateBlob", "Here is your Private Blob Key which is the private key that gets used to validate public key (i have no idea on specifics)", true);
                genkey.GenerateAResult("PrivateKey", "Here is your Private Key which gets encoded with RSA and... no idea.", true);
            }
            else
            {
                Console.WriteLine(helper.CheckArgValue("--SaveLocation"));
                string path = helper.CheckArgValue("--SaveLocation");
                path = helper.FixPaths(path);
                Console.WriteLine(path);
                Console.WriteLine(Directory.Exists(path));
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Keys were saved to where Executable is because path is wrong or not set at all.");
                    File.WriteAllText("PublicKeyBlob.txt", genkey.WriteAResult("Public", "Here is your Public Blob Key which needs to be replaced in the client:"));
                    File.WriteAllText("PrivateKeyBlob.txt", genkey.WriteAResult("PrivateBlob", "Here is your Private Blob Key which should be used to.. I dont know for what exactly lol."));
                    File.WriteAllText("PrivateKey.pem", genkey.WriteAResult("PrivateKey", "Here is your Private Key which should be used to sign the data response in the webserver."));
                }
                else
                {
                    File.WriteAllText(path + "\\" + "PublicKeyBlob.txt", genkey.WriteAResult("Public", "Here is your Public Blob Key which needs to be replaced in the client:"));
                    File.WriteAllText(path + "\\" + "PrivateKeyBlob.txt", genkey.WriteAResult("PrivateBlob", "Here is your Private Blob Key which should be used to.. I dont know for what exactly lol."));
                    File.WriteAllText(path + "\\" + "PrivateKey.pem", genkey.WriteAResult("PrivateKey", "Here is your Private Key which should be used to sign the data response in the webserver."));
                }
            }
        }
    }
}
