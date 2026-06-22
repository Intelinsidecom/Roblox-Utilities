using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RobloxUtilities
{
    public class Signer
    {
        Helper helper = new Helper();
        public bool WasItAnError = false;
        public bool Sign(string[] Path1, string format, string PrivateKeyBlobPath, string SaveLocation)
        {
            if (format == "Retro")
            {
                format = "%{0}%{1}";
            }
            else if (format == "Legacy")
            {
                format = "--rbxsig%{0}%{1}";
            }
            else if (format == "Modern")
            {
                format = "--rbxsig2%{0}%{1}";
            }
            else if (format == "Latest")
            {
                format = "--rbxsig4%{0}%{1}";
            }
            else if (format == "Latest")
            {
                format = "--rbxsig4%{0}%{1}";
            }
            else if (format == null)
            {
                format = "--rbxsig%{0}%{1}";
            }
            //format = Convert.ToBoolean(new FileIniDataParser().ReadFile("Signer.ini").GetKey("UseNewSignatureFormat")) ? "--rbxsig%{0}%{1}" : "%{0}%{1}";
            byte[] keyBlob = Convert.FromBase64String(File.ReadAllText(PrivateKeyBlobPath));
            SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
            RSACryptoServiceProvider rsacryptoServiceProvider = new RSACryptoServiceProvider(1024);
            rsacryptoServiceProvider.ImportCspBlob(keyBlob);
            if (SaveLocation == "")
            {
                foreach (string text in Path1)
                {
                    string text2 = "\r\n" + File.ReadAllText(text);
                    byte[] inArray = rsacryptoServiceProvider.SignData(Encoding.ASCII.GetBytes(text2), sha1CryptoServiceProvider);
                    try
                    {
                        Console.WriteLine(text + ".signed");
                        File.WriteAllText(text + ".signed", string.Format(format, Convert.ToBase64String(inArray), text2));

                        WasItAnError = false;
                        return true;
                    }
                    catch (Exception Error)
                    {
                        Console.WriteLine(Error);
                        WasItAnError = true;
                        return false;
                    }
                }
            }
            else
            {
                foreach (string text in Path1)
                {
                    string text2 = "\r\n" + File.ReadAllText(text);
                    string TextFilename = Path.GetFileName(text);
                    byte[] inArray = rsacryptoServiceProvider.SignData(Encoding.ASCII.GetBytes(text2), sha1CryptoServiceProvider);
                    try
                    {
                        if (!(Directory.Exists(SaveLocation)))
                        {
                            Directory.CreateDirectory(SaveLocation);
                        }
                        Console.WriteLine(SaveLocation + "\\" + TextFilename + ".signed");
                        File.WriteAllText(SaveLocation + "\\" + TextFilename + ".signed", string.Format(format, Convert.ToBase64String(inArray), text2));

                        WasItAnError = false;
                        return true;
                    }
                    catch (Exception Error)
                    {
                        Console.WriteLine(Error);
                        WasItAnError = true;
                        return false;
                    }
                }
                sha1CryptoServiceProvider.Clear();
                rsacryptoServiceProvider.Clear();
                WasItAnError = true;
                return false;
            }
            return false;
        }
    }
}
