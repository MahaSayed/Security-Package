using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class RailFence : ICryptographicTechnique<string, int>
    {
        public int Analyse(string plainText, string cipherText)
        {
            int key = -1;

            plainText = plainText.ToUpper();
            cipherText = cipherText.ToUpper();
            char end = cipherText[1];

            int index = 1;
            for (int i = 0; i < plainText.Length; i++)
            {
                key++;
                if (plainText[i] == end && plainText[i + 1] != cipherText[index])
                    break;
            }

            return key;
        }

        public string Decrypt(string cipherText, int key)
        {
            cipherText = cipherText.ToUpper();
            string pt = "";

            if (cipherText.Contains(" "))
            {
                cipherText = cipherText.Replace(" ", "");
            }


            int lentext = cipherText.Length;


            while (lentext % key != 0)
            {
                cipherText += " ";
                lentext++;
            }
            int cols = lentext / key;


            char[,] array = new char[key, cols];

            int count = 0;

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    array[i, j] = cipherText[count];
                    count++;

                }
            }

            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Console.Write(array[i, j]);

                }
                Console.WriteLine();
            }

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    if (array[j, i].Equals(' '))
                        break;
                    pt += array[j, i];
                }
            }


            return pt;
        }

        public string Encrypt(string plainText, int key)
        {

            plainText = plainText.ToUpper();
            string cp = "";

            if (plainText.Contains(" "))
            {
                plainText = plainText.Replace(" ", "");
            }


            int lentext = plainText.Length;


            while (lentext % key != 0)
            {
                plainText += " ";
                lentext++;
            }
            int cols = lentext / key;


            char[,] array = new char[key, cols];

            int count = 0;


            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < key; j++)
                {
                    array[j, i] = plainText[count];

                    count++;
                }
            }


            for (int i = 0; i < key; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (array[i, j].Equals(' '))
                        break;
                    cp += array[i, j];
                    Console.Write(array[i, j]);

                }
                Console.WriteLine();
            }

            return cp;
        }
    }
}
