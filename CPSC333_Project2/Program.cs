// Project Name: AES CBC with Cipher Text Stealing
// Class: CPSC333
// Names: Tory Kepler, Jon Clay
// Purpose: Implement CBC using AES encryption
// Module Info: Performs AES CBC encryption

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace CPSC333_Project2
{
    class encryption
    {
        static void Main(string[] args)
        {            
            string plaintext = File.ReadAllText("plaintext.txt");

            // Remove all whitespace in plaintext prior to encryption
            plaintext = plaintext.Replace(" ", "");
            
            // Create Aes object
            Aes myAes = Aes.Create();

            // Set Mode to CBC, KeySize to 128, Blocksize to 128
            myAes.Mode = CipherMode.CBC;
            myAes.KeySize = 128;
            myAes.BlockSize = 128;

            // Initialize IV and Key
            byte[] IVbytearray = new byte[] {0x85,0x71,0xD1,0xAD,0xA2,0x4F,0xEC,0x59,0xDB,0xA6,0xEB,0xF8,0x19,0x43,0x7A,0x34};
            byte[] Keybytearray = new byte[] {0x54, 0x46, 0xF6, 0x09, 0xBF, 0xB0, 0x4D, 0xBD, 0xFF, 0xFC, 0x11, 0xC1, 0xE5, 0x36, 0x3E, 0xEA};

            // Set IV and Key on myAes
            myAes.IV = IVbytearray;
            myAes.Key = Keybytearray;

            // Get byte array for the plaintext
            byte[] inBlock = Encoding.ASCII.GetBytes(plaintext);

            // Pad the last block with 0x00
            while (inBlock.Length % 16 != 0)
            {
                Array.Resize(ref inBlock, inBlock.Length + 1);
                inBlock[inBlock.Length - 1] = 0x00;
            }

            // Call encrypt function, returns encrypted byte string
            byte[] encryptedString = EncryptString(myAes, inBlock);

            // Print encrypted bytes
            Console.WriteLine("Encrypted Bytes: " + BitConverter.ToString(encryptedString) + "\n");

            // Write encrypted bytes to text file
            File.WriteAllText("ciphertext.txt", BitConverter.ToString(encryptedString).Replace("-",""));

        }

        public static byte[] EncryptString(Aes AesObj, byte[] inBlock)
        {
            // Creates encryptor based on AesObj's settings (mode, key size, IV size, etc.)
            ICryptoTransform xfrm = AesObj.CreateEncryptor();

            // Get encrypted data
            byte[] outblock = xfrm.TransformFinalBlock(inBlock, 0, inBlock.Length);

            return outblock;
        }
    }
}
