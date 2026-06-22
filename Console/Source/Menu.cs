using System;
using System.Collections.Generic;
using System.Text;
using RobloxUtilities;

namespace ConsoleApp
{
    public class Menu
    {
        public void MainMenu()
        {
            SignerWithSignature Sig = new ConsoleApp.SignerWithSignature();
            SignatureGen SigGen = new ConsoleApp.SignatureGen();
            Console.Clear();
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("1. Generate Signature for use with Roblox Clients");
            Console.WriteLine("2. Sign data with generated Public Key");
            string keyInfo = Console.ReadKey(true).Key.ToString();
            if (keyInfo == "D1")
            {
                SigGen.SignatureGenerationWithUserInput();
            } else if (keyInfo == "D2")
            {
                Sig.HandleFileSigningWithUserInput();
            } else
            {
                MainMenu();
            }
        }
    }
}
