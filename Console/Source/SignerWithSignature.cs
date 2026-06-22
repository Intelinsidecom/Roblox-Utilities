using System;
using System.Collections.Generic;
using System.Text;
using RobloxUtilities;
using System.IO;

namespace ConsoleApp
{
    class SignerWithSignature
    {
        public string sigsec = "Retro";
        Helper helper = new Helper();
        Menu menu = new ConsoleApp.Menu();
        Signer signer = new Signer();
        public void HandleFileSigningWithUserInput()
        {
            Console.Clear();
            Console.WriteLine("Which Signature type are you choosing?");
            Console.WriteLine("");
            Console.WriteLine("1) Retro (%DATA% (Used between 2010-2013))");
            Console.WriteLine("2) Legacy (--rbxsig%DATA% (Used Between 2013-2020))");
            Console.WriteLine("3) Modern (--rbxsig2%DATA% (Used Since 2018))");
            Console.WriteLine("4) Latest (--rbxsig4%DATA% (Used Since 2020))");
            Console.WriteLine("");
            string keyInfo = Console.ReadKey(true).Key.ToString();
            //Console.WriteLine(keyInfo);
            if (keyInfo == "D1")
            {
                sigsec = "Retro";
                HandleSigningProcessWithUserInput();
            }
            else if (keyInfo == "D2")
            {
                sigsec = "Legacy";
                HandleSigningProcessWithUserInput();
            }
            else if (keyInfo == "D3")
            {
                sigsec = "Modern";
                HandleSigningProcessWithUserInput();
            }
            else if (keyInfo == "D4")
            {
                sigsec = "Latest";
                HandleSigningProcessWithUserInput();
            }
        }

        public void HandleSigningProcessWithUserInput()
        {
            Console.WriteLine("Enter the filenames you want to sign, must be seperated by spaces");
            string[] Filenames = helper.FixPathsInArrays(Console.ReadLine().Split(' '));
            Console.WriteLine("");
            Console.WriteLine("Enter where you want them to be saved? Press Enter to save where the exe is. (All invalid directories will result in the file saving where the exe is)");
            string SaveLoc = Console.ReadLine();
            Console.WriteLine("");
            Console.WriteLine("Enter the location of the PrivateBlob export");
            string PrivateBlobLoc = Console.ReadLine();

            signer.WasItAnError = false;
            try
            {
                signer.Sign(Filenames, sigsec, helper.FixPaths(PrivateBlobLoc), helper.FixPaths(SaveLoc));
                if (Directory.Exists((helper.FixPaths(SaveLoc))))
                {
                    Console.WriteLine("");
                    Console.WriteLine("Signing Succeded, the files can be found here " + SaveLoc);
                    Console.WriteLine("Press any key to continue to Main Menu");
                    Console.ReadKey();
                    menu.MainMenu();
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("Signing Succeded, the files can be found where the executable is");
                    Console.WriteLine("Press any key to continue to Main Menu");
                    Console.ReadKey();
                    menu.MainMenu();
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                Console.WriteLine("");
                Console.WriteLine("An error occured, press any key to go back to the main menu.");
                Console.ReadKey();
                menu.MainMenu();
            }
            if (signer.WasItAnError == true)
            {
                Console.WriteLine("An error occured, press any key to go back to the main menu.");
                Console.ReadKey();
                menu.MainMenu();
            }
            signer.WasItAnError = false;
            menu.MainMenu();
        }

        public void HandleSigningProcessNoInput()
        {
            sigsec = (helper.CheckArgValue("--Signature"));
            string fileArg = helper.CheckArgValue("--File");

            if (string.IsNullOrEmpty(sigsec))
            {
                throw new Exception("--Signature argument not found");
            }

            if (string.IsNullOrEmpty(fileArg))
            {
                throw new Exception("--File argument not found");
            }

            string[] Filenames = helper.FixPathsInArrays(fileArg.Split(' '));
            string SaveLoc = (helper.CheckArgValue("--SaveLocation"));
            if (SaveLoc == null || SaveLoc == "")
            {
                SaveLoc = "";
            }

            string PrivateBlobLoc = (helper.CheckArgValue("--PrivateBlobLocation"));

            if (string.IsNullOrEmpty(PrivateBlobLoc))
            {
                throw new Exception("--PrivateBlobLocation argument not found");
            }

            signer.WasItAnError = false;
            try
            {
                signer.Sign(Filenames, sigsec, helper.FixPaths(PrivateBlobLoc), helper.FixPaths(SaveLoc));
                if (Directory.Exists((helper.FixPaths(SaveLoc))))
                {
                    Console.WriteLine("Signing Succeded, the files can be found here " + SaveLoc);
                }
                else
                {
                    Console.WriteLine("Signing Succeded, the files can be found where the app is");
                }
            }
            catch (Exception Error)
            {
                Console.WriteLine(Error);
                Console.WriteLine("An error occured.");
            }
            if (signer.WasItAnError == true)
            {
                Console.WriteLine("An error occured.");
            }
            signer.WasItAnError = false;
        }
    }
}
