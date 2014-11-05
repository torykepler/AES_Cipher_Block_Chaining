using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CPSC333_Project2
{
    class Program
    {
        static void Main(string[] args)
        {            
            string plaintext = File.ReadAllText("plaintext.txt");
            string ciphertext = File.ReadAllText("ciphertext.txt");

            plaintext = plaintext.Replace(" ", "");
            
            Aes myAes = Aes.Create();
            myAes.Mode = CipherMode.CBC;
            myAes.KeySize = 128;
            myAes.BlockSize = 128;
            byte[] IVbytearray = new byte[] {0x85,0x71,0xD1,0xAD,0xA2,0x4F,0xEC,0x59,0xDB,0xA6,0xEB,0xF8,0x19,0x43,0x7A,0x34};
            byte[] Keybytearray = new byte[] {0x54, 0x46, 0xF6, 0x09, 0xBF, 0xB0, 0x4D, 0xBD, 0xFF, 0xFC, 0x11, 0xC1, 0xE5, 0x36, 0x3E, 0xEA};
            myAes.IV = IVbytearray;
            myAes.Key = Keybytearray;

            string IV = "";
            string Key = "";

            byte[] inBlock = Encoding.ASCII.GetBytes(plaintext);

            int i = 0;

            //Console.WriteLine(inBlock.Length);

            foreach (byte b in inBlock)
            {
                Console.WriteLine(b);
            }

            while (i < inBlock.Length)
            {

                i += 128;
            }

            IV = BitConverter.ToString(myAes.IV);
            Key = BitConverter.ToString(myAes.Key);

            Console.WriteLine("IV (Hex) : " + IV);
            Console.WriteLine("Key (Hex) : " + Key + "\n");

            byte[] encryptedString = EncryptString(myAes, plaintext);
            string decryptedString = DecryptString(myAes, encryptedString);

            string decrypted = DecryptString(myAes, StringToByteArray(ciphertext));

            Console.WriteLine("Encrypted Bytes: " + BitConverter.ToString(encryptedString) + "\n");

            File.WriteAllText("ciphertext.txt", BitConverter.ToString(encryptedString).Replace("-",""));

            Console.WriteLine("The String was: " + decrypted);
        }

        public static byte[] EncryptString (Aes AesObj, string plaintext)
        {
            byte[] inBlock = Encoding.ASCII.GetBytes(plaintext);
            ICryptoTransform xfrm = AesObj.CreateEncryptor();
            byte[] outblock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outblock;
        }

        public static string DecryptString(Aes AesObj, byte[] encryptedText)
        {
            ICryptoTransform xfrm = AesObj.CreateDecryptor();
            byte[] outBlock = xfrm.TransformFinalBlock(encryptedText, 0, encryptedText.Length);

            return Encoding.ASCII.GetString(outBlock);
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
