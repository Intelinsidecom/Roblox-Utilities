using System.Collections.Generic;
using System.Text;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography;

namespace RobloxUtilities
{
    public class KeyGen {
        public bool WasItAnError = false;
        public string GenKeys(string Selection)
        {
            RSACryptoServiceProvider rSACryptoServiceProvider = new RSACryptoServiceProvider(1024);
            byte[] inArray = rSACryptoServiceProvider.ExportCspBlob(false);
            byte[] array = rSACryptoServiceProvider.ExportCspBlob(true);
            rSACryptoServiceProvider.Clear();
            int pcbEncoded = 0;
            if (!CryptoAPI.CryptEncodeObject(65536, 43, array, null, ref pcbEncoded))
            {
                WasItAnError = true;
                return(Marshal.GetLastWin32Error().ToString());
            }
            byte[] array2 = new byte[pcbEncoded];
            if (!CryptoAPI.CryptEncodeObject(65536, 43, array, array2, ref pcbEncoded))
            {
                WasItAnError = true;
                return (Marshal.GetLastWin32Error().ToString());
            }
            else
            {
                if (Selection == "Public")
                {
                    return (Convert.ToBase64String(inArray));
                }
                if (Selection == "PrivateBlob")
                {
                    return (Convert.ToBase64String(array));
                }
                if (Selection == "PrivateKey")
                {
                    return ("-----BEGIN RSA PRIVATE KEY-----\r\n" + Convert.ToBase64String(array2) + "\r\n-----END RSA PRIVATE KEY-----");
                }

                /*
                File.WriteAllText("PublicKeyBlob.txt", Convert.ToBase64String(inArray));
                File.WriteAllText("PrivateKeyBlob.txt", Convert.ToBase64String(array));
                File.WriteAllText("PrivateKey.pem", "-----BEGIN RSA PRIVATE KEY-----\r\n" + Convert.ToBase64String(array2) + "\r\n-----END RSA PRIVATE KEY-----");*/
            }
            return ("");
        }
        public string GenerateAResult(string type, string Text, bool Print)
        {
            WasItAnError = false;
            string result = GenKeys(type);
            if (Print)
            {
                Console.WriteLine(Text);
            }
            if (WasItAnError)
            {
                Console.WriteLine("An Error Occurred, here is the message log: " + result);
                WasItAnError = false;
            }
            else
            {
                if (Print)
                {
                    Console.WriteLine(result);
                }
            }
            if (Print)
            {
                Console.WriteLine();
            }
            return(result);
        }
        public string WriteAResult(string type, string Text)
        {
            WasItAnError = false;
            string result = GenKeys(type);
            if (WasItAnError)
            {
                Console.WriteLine("An Error Occurred, here is the message log: " + result);
                WasItAnError = false;
            }
            return (result);
        }
    }
}
