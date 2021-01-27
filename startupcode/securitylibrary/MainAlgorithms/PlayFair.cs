using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityLibrary
{
    public class PlayFair : ICryptographicTechnique<string, string>
    {
        /// <summary>
        /// The most common diagrams in english (sorted): TH, HE, AN, IN, ER, ON, RE, ED, ND, HA, AT, EN, ES, OF, NT, EA, TI, TO, IO, LE, IS, OU, AR, AS, DE, RT, VE
        /// </summary>
        /// <param name="plainText"></param>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public char[,] matrix = new char[5, 5];
        public bool[] visited = new bool[30];
        public int k = 0;
        public bool tobreak = true;
        public char tominusorplus = 'a';
        public string plain = "";
        public string encryptout = "";
        public string decrypted = "";
        public void matrixfill(string text, string key)
        {
            for (int i = 0; i < 5; i++)
            {
                if (tobreak == false) break;
                for (int j = 0; j < 5; j++)
                {
                    if (tobreak == false) break;
                    if (k == key.Length)
                    {
                        int index = j;
                        for (int u = i; u < 5; u++)
                        {
                            for (int v = index; v < 5; v++)
                            {
                                if (tominusorplus == 'j')
                                {
                                    //    Console.WriteLine("dakhl hena awel if  ");
                                    v--;
                                    tominusorplus++;
                                    continue;
                                    //   Console.WriteLine(" el v be ->" + v);
                                    //      Console.WriteLine(" el aa be ->" + aa);
                                }
                                if (visited[tominusorplus - 'a'] == false)
                                {
                                    //         Console.WriteLine("dakhl hena tany if ");
                                    matrix[u, v] = tominusorplus;
                                }
                                else
                                {
                                    //         Console.WriteLine("dakhl hena talet if  ");
                                    v--;
                                }
                                //Console.WriteLine(aa);

                                tominusorplus++;
                            }
                            index = 0;
                        }
                        tobreak = false;
                        break;
                    }
                    if (visited[(key[k] - 'a')] == false)
                    {
                        matrix[i, j] = key[k];
                        visited[(key[k] - 'a')] = true;
                        k++;
                    }
                    else
                    {
                        k++;
                        j--;
                    }
                    if (tobreak == false) break;
                }
                if (tobreak == false) break;
            }
        }
        public void writingmatrix(string text, string key)
        {
            Console.WriteLine("Key Matrix  :   ");
            Console.WriteLine("=================");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                    Console.Write(matrix[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------");
        }
        public string encryptionwithconditions(string plainText, string key)
        {
            int firstcharinrow = 0, firstcharincol = 0, secondcharinrow = 0, secondcharincol = 0, firstencinrow = 0, firstencincol = 0, secondencinrow = 0, secondencincol = 0;
            for (int i = 0; i < plainText.Length; i += 2)
            {
                if (i + 1 == plainText.Length)
                {
                    plain += plainText[i];
                    break;
                }
                else if (plainText[i] == plainText[i + 1])
                {
                    plain += plainText[i];
                    plain += 'x';
                    i--;
                }
                else
                {
                    plain += plainText[i];
                    plain += plainText[i + 1];
                }
            }
            if (plain.Length % 2 == 1)
            {
                plain += 'x';
            }
            for (int t = 0; t < plain.Length; t += 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (plain[t] == matrix[i, j])
                        {
                            firstcharinrow = i;
                            firstcharincol = j;
                        }
                        else if (plain[t + 1] == matrix[i, j])
                        {
                            secondcharinrow = i;
                            secondcharincol = j;
                        }
                    }
                }
                // --------> Conditions <---------------------
                if (firstcharinrow == secondcharinrow) // lw el bashawat fe nfs el row  nfs el row 
                {
                    firstencinrow = firstcharinrow;
                    secondencinrow = secondcharinrow;
                    firstencincol = (firstcharincol + 1) % 5;
                    secondencincol = (secondcharincol + 1) % 5;
                }
                else if (firstcharincol == secondcharincol) // lw el bashawat fe nfs el coloumn 
                {
                    firstencincol = firstcharincol;
                    secondencincol = secondcharincol;
                    firstencinrow = (firstcharinrow + 1) % 5;
                    secondencinrow = (secondcharinrow + 1) % 5;
                }
                else // lw wla da wla da yeb2a moatarareen n3eml el rectangle b2a 
                {
                    firstencinrow = firstcharinrow;
                    firstencincol = secondcharincol;
                    secondencinrow = secondcharinrow;
                    secondencincol = firstcharincol;
                }
                encryptout += matrix[firstencinrow, firstencincol];
                encryptout += matrix[secondencinrow, secondencincol];
            }
            Console.Write("plain text : ");
            Console.WriteLine(plain);
            Console.Write("cipher text :  ");
            Console.WriteLine(encryptout);
            return encryptout;
        }
        public string decryptionwithconditions(string cipherText, string key)
        {
            //  int first_char_row = 0, first_char_coloum = 0, Second_char_row = 0, Second_char_coloum = 0;
            //  int firstEnc_char_row = 0, firstEnc_char_coloum = 0, SecondEnc_char_row = 0, SecondEnc_char_coloum = 0;
            int firstcharinrow = 0, firstcharincol = 0, secondcharinrow = 0, secondcharincol = 0, firstencinrow = 0, firstencincol = 0, secondencinrow = 0, secondencincol = 0;
            matrixfill(cipherText, key);
            string PlainPairs = "";
            for (int t = 0; t < cipherText.Length; t += 2)
            {
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (Char.ToLower(cipherText[t]) == matrix[i, j])
                        {
                            firstcharinrow = i;
                            firstcharincol = j;
                        }
                        else if (Char.ToLower(cipherText[t + 1]) == matrix[i, j])
                        {
                            secondcharinrow = i;
                            secondcharincol = j;
                        }
                    }
                }
                if (firstcharinrow == secondcharinrow)//Two Characters in the same Raw 
                {
                    firstencinrow = firstcharinrow;
                    secondencinrow = secondcharinrow;
                    firstencincol = (firstcharincol + 4) % 5;
                    secondencincol = (secondcharincol + 4) % 5;
                }
                else if (firstcharincol == secondcharincol) // Two Characters in the Same coloum
                {
                    firstencincol = firstcharincol;
                    secondencincol = secondcharincol;
                    firstencinrow = (firstcharinrow + 4) % 5;
                    secondencinrow = (secondcharinrow + 4) % 5;
                }
                else
                {
                    firstencinrow = firstcharinrow;
                    firstencincol = secondcharincol;
                    secondencinrow = secondcharinrow;
                    secondencincol = firstcharincol;
                }
                PlainPairs += matrix[firstencinrow, firstencincol];
                PlainPairs += matrix[secondencinrow, secondencincol];
            }
            Console.WriteLine("Key Matrix : ");
            Console.WriteLine("================");
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------");
            for (int i = 0; i < PlainPairs.Length; i += 2)
            {
                // if next character is X (Added) 
                if (PlainPairs[i + 1] == 'x')
                {
                    // iF X is the last character  in the string (the end of string ) 
                    if (i + 1 == PlainPairs.Length - 1)
                    {
                        // we will add the previous Character 
                        decrypted += PlainPairs[i];
                    }
                    else
                    {
                        // example : oxo
                        if (PlainPairs[i] == PlainPairs[i + 2])
                        {
                            decrypted += PlainPairs[i];
                        }
                        else
                        {
                            decrypted += PlainPairs[i];
                            decrypted += PlainPairs[i + 1];
                        }
                    }
                }
                else
                {
                    decrypted += PlainPairs[i];
                    decrypted += PlainPairs[i + 1];
                }

            }
            Console.Write("Plain Text  : ");
            Console.WriteLine(PlainPairs);
            Console.Write("Cipher Text : ");
            Console.WriteLine(decrypted);
            return decrypted;
        }
        public string Analyse(string plainText)
        {
            throw new NotImplementedException();
        }
        public string Analyse(string plainText, string cipherText)
        {
            throw new NotSupportedException();
        }
        public string Decrypt(string cipherText, string key)
        {
            string output2 = decryptionwithconditions(cipherText, key);
            return output2;
            throw new NotImplementedException();
        }
        public string Encrypt(string plainText, string key)
        {
            matrixfill(plainText, key);
            writingmatrix(plainText, key);
            string output = encryptionwithconditions(plainText, key);
            return output;
            throw new NotImplementedException();
        }
    }
}
