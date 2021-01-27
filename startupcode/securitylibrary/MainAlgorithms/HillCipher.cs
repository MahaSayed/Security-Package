using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    /// <summary>
    /// The List<int> is row based. Which means that the key is given in row based manner.
    /// </summary>
    public class HillCipher :  ICryptographicTechnique<List<int>, List<int>>
    {
        public List<int> Analyse(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }


        public List<int> Decrypt(List<int> cipherText, List<int> key)
        {
            throw new NotImplementedException();
        }
        static void getKeyMatrix(List<int> key,
                           int[,] keyMatrix)
        {
            //int k = 0;
            double length = key.Count;
            int matrixlength = Convert.ToInt32(Math.Sqrt(length));
            /* for (int i = 0; i < matrixlength; i++)
             {
                 for (int j = 0; j < matrixlength; j++)
                 {
                     keyMatrix[i, j] = (key[k]) % 65;
                     k++;
                 }
             }
             */
            int r = 0;
            for (int i = 0; i < matrixlength; i++)
            {
                for (int j = 0; j < matrixlength; j++)
                {
                    keyMatrix[i, j] = key[r];
                    r++;
                }
            }
            /*
            int k = 0;
            int r = 0;
            for (int j = 0; j < matrixlength; j++)
            {
                k = 0;
                while (k != matrixlength)
                {
                    keyMatrix[k, j] = key[r];
                    r++;
                    k++;
                }
            }
            for (int i = 0; i < matrixlength; i++)
            {
                for (int j = 0; j < matrixlength; j++)
                    Console.Write(keyMatrix[i, j] + " ");
                Console.WriteLine();
            }
            /*
            int x = 0;
            for (int j = 0; j < matrixlength; j++)
            {
                x = 0;
                while (x != matrixlength)
                {
                    keyMatrix[x, j] = (key[k]) % 65;
                    k++;
                    x++;
                }
            }
            k = 0;
            for (int i = 0; i < matrixlength; i++)
            {
                for (int j = 0; j < matrixlength; j++)
                {
                    keyMatrix[i, j] = (key[k]) % 65;
                    k++;

                    Console.Write(+ keyMatrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            */
        }

        static List<int> encrypt(int[,] cipherMatrix,
                            int[,] keyMatrix,
                            int[,] messageVector, int numberofrows, int numberofcolumns, int messagelength, int keylength, List<int> key, int matrixlength)
        {
            //Console.WriteLine("EL NUMBER OF ROWS YA BASHA " + numberofrows);
            // Console.WriteLine("EL NUMBER OF COLOUMNS YA BASHA " + numberofcolumns);
            List<int> cipher = new List<int>();
            for (int i = 0; i < numberofrows; i++)
            {
                int k = i;
                for (int j = 0; j < numberofcolumns; j++)
                {
                    // Console.WriteLine(" DAKHL HENA YA BASHAAAAA ");
                    cipherMatrix[i, j] = 0;
                    for (int x = 0; x < matrixlength; x++)
                    {
                        cipherMatrix[i, j] += keyMatrix[i, x] * messageVector[x, j];
                    }
                    cipherMatrix[i, j] = cipherMatrix[i, j] % 26;
                    Console.WriteLine(cipherMatrix[i, j] + "  ");
                    k++;
                }
                if (k == numberofrows)
                    break;
            }
            for (int j = 0; j < numberofcolumns; j++)
            {
                for (int i = 0; i < numberofrows; i++)
                {
                    cipher.Add(cipherMatrix[i, j]);
                }
            }
            for (int i = 0; i < cipher.Count; i++)
            {
                Console.Write(cipher[i] + " ");
            }
            String CipherText = "";
            Console.WriteLine("elmessage length ya basha " + messagelength);
            for (int i = 0; i < numberofrows; i++)
            {
                for (int j = 0; j < numberofcolumns; j++)
                    CipherText += (char)(cipherMatrix[i, j] + 65);
            }
            Console.Write("Ciphertext: " + CipherText + " ");
            /*
            int k = 0;
            for (int j = 0; j < numberofcolumns; j++)
            {
                k = 0;
                while (k != numberofrows)
                {
                    cipherMatrix[k, j] +=  keyMatrix[i, x] * messageVector[x, j];
                    k++;
                }
                cipherMatrix[i, j] = cipherMatrix[i, j] % 26;
                Console.WriteLine(cipherMatrix[i, j] + "  ");
            }
            */
            return cipher; 
        }

        static List<int> hillcipher(List<int> plainText, List<int> key)
        {

            int keylength = key.Count;
            int matrixlength = Convert.ToInt32(Math.Sqrt(keylength));
            int[,] keyMatrix = new int[matrixlength, matrixlength];
            getKeyMatrix(key, keyMatrix);
            int numberofrows = matrixlength;
            int messagelength = plainText.Count;
            //  Console.WriteLine("EL MESSAGE LENGTH YA BVASHA " + messagelength + " ");
            //  Console.WriteLine(" EL KEY LENGTH YA BASHA " + keylength + " ");
            int numberofcoloumns = Convert.ToInt32(messagelength / matrixlength);
            //  Console.WriteLine("EL NUMBER OF COLOUMNS T7T YA BASHA " + numberofcoloumns);
            int[,] messageVector = new int[numberofrows, numberofcoloumns];

            //   Console.WriteLine("EL NESSAGE LENGTH AHO " + messagelength + " ");
            int l = 0;
            int r = 0;
            int k = 0;
            for (int j = 0; j < numberofcoloumns; j++)
            {
                k = 0;
                while (k != numberofrows)
                {
                    messageVector[k, j] = plainText[r];
                    r++;
                    k++;
                }
            }
            for (int i = 0; i < numberofrows; i++)
            {
                for (int j = 0; j < numberofcoloumns; j++)
                    Console.Write(messageVector[i, j] + " ");
                Console.WriteLine();
            }
            int[,] cipherMatrix = new int[numberofrows, numberofcoloumns];
            List<int> tosend = new List<int>();
            tosend = encrypt(cipherMatrix, keyMatrix, messageVector, numberofrows, numberofcoloumns, messagelength, keylength, key, matrixlength);
            return tosend;
        }

        public List<int> Encrypt(List<int> plainText, List<int> key)
        {
            List<int> result = new List<int>();
            result = hillcipher(plainText, key);            
            List<int> cipherText = new List<int>();
            if (key.Count == 4)
            {
                for (int i = 0; i < plainText.Count; i += 2)
                {
                    int c1, c2 = 0;
                    c1 = (plainText[i] * key[0] + plainText[i + 1] * key[1]) % 26;
                    c2 = (plainText[i] * key[2] + plainText[i + 1] * key[3]) % 26;
                    cipherText.Add(c1);
                    cipherText.Add(c2);
                }
            }
            else if (key.Count == 9)
            {
                for (int i = 0; i < plainText.Count; i += 3)
                {
                    int c1, c2, c3 = 0;
                    c1 = key[0] * plainText[i] + key[1] * plainText[i + 1] + key[2] * plainText[i + 2];
                    c2 = key[3] * plainText[i] + key[4] * plainText[i + 1] + key[5] * plainText[i + 2];
                    c3 = key[6] * plainText[i] + key[7] * plainText[i + 1] + key[8] * plainText[i + 2];
                    c1 %= 26;
                    c2 %= 26;
                    c3 %= 26;
                    cipherText.Add(c1);
                    cipherText.Add(c2);
                    cipherText.Add(c3);
                }
            }
            return cipherText;
            return result;
            throw new NotImplementedException();
        }


        public List<int> Analyse3By3Key(List<int> plainText, List<int> cipherText)
        {
            throw new NotImplementedException();
        }

    }
}
