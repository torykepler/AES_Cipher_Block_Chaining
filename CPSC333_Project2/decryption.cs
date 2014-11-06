// Project Name: AES CBC with Cipher Text Stealing
// Class: CPSC333
// Names: Tory Kepler, Jon Clay
// Purpose: Implement CBC using AES encryption & decryption
// Module Info: Performs AES CBC decryption

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CPSC333_Project2
{
    class decryption
    {
        static void decrypt()
        {
            // Read in ciphertext
            string ciphertext = File.ReadAllText("ciphertext.txt");

            // Create AES object
            Aes myAes = Aes.Create();
            
            // Set AES parameters
            myAes.Mode = CipherMode.CBC;
            myAes.KeySize = 128;
            myAes.BlockSize = 128;

            // Initialize IV and Key
            byte[] IVbytearray = new byte[] {0x85,0x71,0xD1,0xAD,0xA2,0x4F,0xEC,0x59,0xDB,0xA6,0xEB,0xF8,0x19,0x43,0x7A,0x34};
            byte[] Keybytearray = new byte[] {0x54, 0x46, 0xF6, 0x09, 0xBF, 0xB0, 0x4D, 0xBD, 0xFF, 0xFC, 0x11, 0xC1, 0xE5, 0x36, 0x3E, 0xEA};
            
            // Set IV and Key
            myAes.IV = IVbytearray;
            myAes.Key = Keybytearray;

            // Call decrypt function
            string decrypted = DecryptString(myAes, StringToByteArray(ciphertext));

            // Print decrypted string
            Console.WriteLine("The String was: " + decrypted);
        }

        public static string DecryptString(Aes AesObj, byte[] encryptedText)
        {
            // Create decryptor object
            ICryptoTransform xfrm = AesObj.CreateDecryptor();
            
            // Get decrypted data
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
